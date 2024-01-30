using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WineInfo.API.Models
{
    public class UserDto
    {
        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
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
