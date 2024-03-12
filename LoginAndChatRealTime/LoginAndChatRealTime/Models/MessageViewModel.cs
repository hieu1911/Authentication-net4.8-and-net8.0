using LoginAndChatRealTime.Entities;

namespace LoginAndChatRealTime.Models
{
    public class MessageViewModel
    {
        public User User { get; set; }

        public Room Room { get; set; }

        public List<Message> Messages { get; set; }
    }
}
