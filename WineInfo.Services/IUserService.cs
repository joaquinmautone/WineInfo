using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WineInfo.Entities;
using WineInfo.Services.Communication;

namespace WineInfo.Services
{
    public interface IUserService
    {
        Task<UserResponse> AddUserAsync(User mesurement);
        Task<UserResponse> FindUserByNameAndPasswordAsync(string userName, string password);
    }
}
