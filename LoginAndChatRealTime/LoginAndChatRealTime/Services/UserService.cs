using LoginAndChatRealTime.Entities;
using LoginAndChatRealTime.Helper;
using LoginAndChatRealTime.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LoginAndChatRealTime.Services
{
    public class UserService : IUserSerivce
    {
        public User? GetUser(string userName, string password)
        {
            using (var db = new MyDbContext())
            {
                var user = db.Users.SingleOrDefault(u => u.UserName == userName && u.Password == password);
                return user;
            }
        }

        public User GetUser(int id)
        {
            using (var db = new MyDbContext())
            {
                var user = db.Users.Include(u => u.UserRooms).ThenInclude(ur => ur.Room)
                    .SingleOrDefault(u => u.UserId == id);
                return user;
            }
        }

    }
}
