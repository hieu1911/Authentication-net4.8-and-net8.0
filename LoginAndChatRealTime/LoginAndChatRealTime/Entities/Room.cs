using System.ComponentModel.DataAnnotations;

namespace LoginAndChatRealTime.Entities
{
    public class Room
    {
        [Key]
        public int RoomId { get; set; }

        public string RoomName { get; set; }

        public ICollection<UserRooms> UserRooms { get; set; }

        public ICollection<Message> Messages { get; set; }
    }

}
