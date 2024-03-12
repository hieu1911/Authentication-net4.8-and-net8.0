using LoginAndChatRealTime.Entities;

namespace LoginAndChatRealTime.Interfaces
{
    public interface IRoomService
    {
        public List<Room> GetRoomsByUserId(int  userId);
    }
}
