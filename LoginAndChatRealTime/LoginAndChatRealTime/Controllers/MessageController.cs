using LoginAndChatRealTime.Interfaces;
using LoginAndChatRealTime.Models;
using LoginAndChatRealTime.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace LoginAndChatRealTime.Controllers
{
    [Authorize]
    public class MessageController : Controller
    {
        private readonly IHttpContextAccessor _context;

        private readonly IMessageSerivce _messageService;

        public MessageController(IHttpContextAccessor context, IMessageSerivce messageService) 
        {
            _context = context;
            _messageService = messageService;
        }

        public IActionResult Index(string roomName, int roomId)
        {
            var context = _context.HttpContext;

            var userId = int.Parse(context.User.FindFirst(type: "Id").Value);
            var userName = context.User.FindFirst(ClaimTypes.Name).Value;

            var messages = _messageService.GetMessages(roomId);

            var user = new User()
            {
                UserId = userId,
                UserName = userName
            };

            var room = new Room()
            {
                RoomId = roomId,
                RoomName = roomName
            };

            var messageViewModel = new MessageViewModel()
            {
                User = user,
                Room = room,
                Messages = messages
            };

            return View(messageViewModel);
        }
    }
}
