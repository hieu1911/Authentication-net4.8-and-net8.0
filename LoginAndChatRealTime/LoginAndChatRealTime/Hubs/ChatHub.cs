using LoginAndChatRealTime.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace LoginAndChatRealTime.Hubs
{
    [Authorize(AuthenticationSchemes = "CookieAuthentication")]
    public class ChatHub : Hub
    {
        private IMessageSerivce _messageService;

        private IUserSerivce _userService;

        private IHttpContextAccessor _context;

        public ChatHub(IMessageSerivce messageSerivce, IUserSerivce userSerivce, IHttpContextAccessor context) : base()
        {
            _messageService = messageSerivce;
            _userService = userSerivce;
            _context = context;
        }

        public async Task AddToGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task RemoveFromGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task SendMessageToGroup(string groupName, string message)
        {
            var context = _context.HttpContext;
            var userId = int.Parse(context.User.FindFirst(type: "Id").Value);
            var userName = context.User.FindFirst(ClaimTypes.Name).Value;

            await Clients.Group(groupName).SendAsync("ReceiveMessage", userName, message, userId);
            
            //_messageService.CreateMessage(userId, groupName, message);
        }

        public override Task OnConnectedAsync()
        {
            var context = _context.HttpContext;
            var userId = int.Parse(context.User.FindFirst(type: "Id").Value);

            var user = _userService.GetUser(userId);
            foreach (var room in user.UserRooms)
            {
                Groups.AddToGroupAsync(Context.ConnectionId, room.Room.RoomName);
            }

            return base.OnConnectedAsync();
        }
    }
}
