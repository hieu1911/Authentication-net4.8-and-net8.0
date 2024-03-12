using LoginAndChatRealTime.Entities;

namespace LoginAndChatRealTime.Interfaces
{
    public interface IUserSerivce
    {
        public User? GetUser(string userName, string password);

        public User GetUser(int id);
    }
}
