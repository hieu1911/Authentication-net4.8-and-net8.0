using LoginAndChatRealTime.Entities;

namespace LoginAndChatRealTime.Interfaces
{
    public interface IMessageSerivce
    {
        public List<Message> GetMessages(int roomId);

        public void CreateMessage(int userId, string roomName, string content);
    }
}
