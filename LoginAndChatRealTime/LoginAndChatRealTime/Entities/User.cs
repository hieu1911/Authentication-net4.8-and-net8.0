using System.ComponentModel.DataAnnotations;

namespace LoginAndChatRealTime.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public ICollection<UserRooms> UserRooms { get; set; }

        public ICollection<Message> Messages { get; set; }
    }
}
