using WineInfo.Entities;

namespace WineInfo.Services.Communication
{
    public class UserResponse : BaseResponse
    {
        public User User { get; private set; }

        public UserResponse(bool success, string message, User user) : base(success, message)
        {
            User = user;
        }
    }
}
