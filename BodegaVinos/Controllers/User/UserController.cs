using BodegaVinos.Common.Dtos;
using BodegaVinos.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BodegaVinos.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public IActionResult GetUser()
        {
            return Ok(_userService.GetUser());
        }

        [HttpPost]
        public IActionResult createUser(CreateUserDto newUser)
        {
            return Ok(_userService.createUser(newUser));
        }
    }
}
