using BloggingApplicationAPI.Models;
using BloggingApplicationAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BloggingApplicationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        // Register user
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.RegisterAsync(model.Username, model.Email, model.Password);
                    return Ok(new { Message = "User registered successfully!" });
                }
                catch (System.Exception ex)
                {
                    return BadRequest(new { Message = ex.Message });
                }
            }

            return BadRequest(ModelState);
        }

        // Login user
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var user = await _userService.LoginAsync(model.Username, model.Password);

                    // Optionally, you can store user info in session here
                    return Ok(new { Message = "Login successful!", User = user });
                }
                catch (System.Exception ex)
                {
                    return Unauthorized(new { Message = ex.Message });
                }
            }

            return BadRequest(ModelState);
        }
    }

    
}
