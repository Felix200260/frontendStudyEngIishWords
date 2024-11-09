using Microsoft.AspNetCore.Mvc;
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
            if (userDto == null)
            {
                return BadRequest("Данных нет");
            }

            await _userService.AddUserAsync(userDto);
            return Ok(new { message = "Пользователь успешно зарегался" });
        }
    }
}