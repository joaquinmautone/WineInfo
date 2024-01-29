using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WineInfo.API.Models
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public UserDto(
            int userId,
            string userName,
            string password,
            string firstName,
            string lastName)
        {
            UserId = userId;
            UserName = userName;
            Password = password;
            FirstName = firstName;
            LastName = lastName;
        }

    }
}
