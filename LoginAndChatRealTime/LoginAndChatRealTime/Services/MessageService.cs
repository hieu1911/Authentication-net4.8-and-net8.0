using LoginAndChatRealTime.Entities;
using LoginAndChatRealTime.Helper;
using LoginAndChatRealTime.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoginAndChatRealTime.Services
{
    public class MessageService : IMessageSerivce
    {
        public void CreateMessage(int userId, string roomName, string content)
        {
            using (var db = new MyDbContext())
            {
                var room = db.Rooms.SingleOrDefault(r => r.RoomName == roomName);

                var message = new Message()
                {
                    UserId = userId,
                    RoomId = room.RoomId,
                    Content = content,
                    Timestamp = DateTime.Now
                };

                db.Messages.Add(message);
                db.SaveChanges();
            }
        }

        public List<Message> GetMessages(int roomId)
        {
            using (var db = new MyDbContext())
            {
                var messages = db.Messages
                    .Include(m => m.User)
                    .Include(m => m.Room)
                    .Where(m => m.RoomId == roomId)
                    .OrderBy(m => m.Timestamp)
                    .ToList();

                return messages;
            }
        }
    }
}
