using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetworkApi.Models;
using NetworkApi.Repository.IRepository;

namespace NetworkApi.Controllers
{
    //add route versioning instead of api/controller
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] User model)
        {
            var user = _userRepository.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return BadRequest(new { message = "This username or password is incorrect" });

            }
            return Ok(user);
        }

        //register handling
        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Register([FromBody] User model)
        {
            bool ifUserNameUnique = _userRepository.isUniqueUser(model.Username);
            if (!ifUserNameUnique)
            {
                return BadRequest(new { message = "This username is already used" });

            }

            var user = _userRepository.Register(model.Username, model.Password);

            if (user == null)
            {
                return BadRequest(new { message = "There was an error with the registration!" });

            }

            return Ok(user);
        }


    }
}