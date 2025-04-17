using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using studyEnglishWords.Dto;
using studyEnglishWords.Models;
using studyEnglishWords.Service;

namespace studyEnglishWords.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        
        public UserController(UserService userService)
        {
            _userService = userService; // Подключаем сервис
        }

        [HttpPost("register")] // POST-запрос для регистрации
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userService.AddUserAsync(userDto);
            return Ok(new { message = "Пользователь успешно зарегистрирован" });
        }

        
        [HttpPost("login")]
        public IActionResult Login(LoginDto loginDto)
        {
            var user = _userService.Authenticate(loginDto.Email, loginDto.Password);
    
            if (user == null)
                return Unauthorized("Неверный email или пароль");
    
            // Создаём простую сессию без JWT
            HttpContext.Session.SetInt32("UserId", user.Id);
            HttpContext.Session.SetString("UserEmail", user.UniqueEmail);
    
            // Возвращаем информацию о пользователе
            return Ok(new { 
                Id = user.Id, 
            Email = user.UniqueEmail, 
                FirstName = user.FirstName,
                Message = "Вход выполнен успешно"
            });
        }
        
        [Authorize]
        [HttpGet("current")]
        public IActionResult GetCurrentUser()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _userService.GetById(int.Parse(userId));
        
            return Ok(user);
        }
    }
}