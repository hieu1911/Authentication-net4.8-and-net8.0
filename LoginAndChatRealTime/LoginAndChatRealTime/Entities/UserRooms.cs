using System.ComponentModel.DataAnnotations;

namespace LoginAndChatRealTime.Entities
{
    public class UserRooms
    {
        [Key]
        public int UserRoomId { get; set; }
        
        public int UserId { get; set; }

        public User User { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }
    }
}
