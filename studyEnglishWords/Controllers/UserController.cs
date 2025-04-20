using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _userService.AddUserAsync(userDto);
                return Ok(new { message = "Пользователь успешно зарегистрирован" });
            }
            catch (DbUpdateException dbEx)
            {
                // проверка на уникальности email
                if (dbEx.InnerException != null && dbEx.InnerException.Message.Contains("users_unique_email_key"))
                {
                    return Conflict(new { message = "Пользователь с таким email уже существует" }); //Conflict вернёт ошибку 409 где на фронте есть обработка этого. 409 - ошибка валидации
                }
                return StatusCode(500, new { message = "Ошибка базы данных" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
            }
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