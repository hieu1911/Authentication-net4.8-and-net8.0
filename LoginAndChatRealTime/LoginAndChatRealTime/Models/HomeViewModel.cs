using LoginAndChatRealTime.Entities;

namespace LoginAndChatRealTime.Models
{
    public class HomeViewModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; }

        public List<Room> Rooms { get; set; }
    }
}
