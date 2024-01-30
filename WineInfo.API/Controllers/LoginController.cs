using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using AutoMapper;
using WineInfo.Services;
using WineInfo.API.Models;
using WineInfo.Entities;

namespace WineInfo.API.Controllers
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public LoginController(IConfiguration configuration, IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost]
        public async Task<ActionResult<string>> LoginAsync(LoginDto userCredentials)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // validate the username/password
            var user = await ValidateUserCredentials(
                userCredentials.UserName,
                userCredentials.Password);

            if (user == null)
            {
                return Unauthorized();
            }

            var tokenToReturn = this.CreateAccessTokenAsync(userCredentials.UserName, userCredentials.Password, user);
            
            return Ok(tokenToReturn);
        }

        private string CreateAccessTokenAsync(string userName, string password, UserDto user)
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]));
            var signingCredentials = new SigningCredentials(
                securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("sub", user.UserId.ToString()));
            claimsForToken.Add(new Claim("name", user.FirstName));
            claimsForToken.Add(new Claim("lastname", user.LastName));

            var jwtSecurityToken = new JwtSecurityToken(
                _configuration["Authentication:Issuer"],
                _configuration["Authentication:Audience"],
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            string tokenToReturn = new JwtSecurityTokenHandler()
               .WriteToken(jwtSecurityToken);

            return tokenToReturn;
        }

        private async Task<UserDto> ValidateUserCredentials(string userName, string password)
        {
            var response = await _userService.FindUserByNameAndPasswordAsync(userName, password);
            UserDto userDto = null;

            if (response.Success)
                userDto = _mapper.Map<UserDto>(response.User);

            return userDto;
        }
    }
}
