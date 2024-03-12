using LoginAndChatRealTime.Entities;
using LoginAndChatRealTime.Helper;
using LoginAndChatRealTime.Interfaces;

namespace LoginAndChatRealTime.Services
{
    public class RoomService : IRoomService
    {
        public List<Room> GetRoomsByUserId(int userId)
        {
            using (var db = new MyDbContext())
            {
                var rooms = db.Rooms
                    .Where(r => r.UserRooms.Any(ur => ur.UserId == userId))
                    .ToList();

                return rooms;
            }
        }
    }
}
