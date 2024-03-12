using System.ComponentModel.DataAnnotations;

namespace LoginAndChatRealTime.Entities
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }

        public string Content { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }

        public DateTime Timestamp { get; set; }
    }
}
