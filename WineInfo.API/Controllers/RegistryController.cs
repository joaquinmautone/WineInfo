using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using WineInfo.Services;
using WineInfo.API.Models;
using WineInfo.Entities;

namespace WineInfo.API.Controllers
{
    [Route("api/registry")]
    [ApiController]
    public class RegistryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public RegistryController(IUserService userService, IMapper mapper)
        {
            _userService = userService ?? throw new ArgumentNullException(nameof(userService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserAsync(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = _mapper.Map<UserDto, User>(userDto);
            var response = await _userService.AddUserAsync(user);

            if (!response.Success)
            {
                return BadRequest(response.Message);
            }

            userDto = _mapper.Map<UserDto>(response.User);

            return Ok(userDto);
        }
    }
}
