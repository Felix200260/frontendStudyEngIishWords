using studyEnglishWords.Models;

namespace studyEnglishWords.Service
{
    public class UserService
    {
        private readonly AppDbContext _context;
        
        public UserService(AppDbContext context)
        {
            _context = context; // Инициализируем контекст базы данных
        }

        public async Task AddUserAsync(UserDto userDto)
        {
            var user = new AppDbContext.User
            {
                First_name = userDto.First_name,
                UniqueEmail = userDto.UniqueEmail,
                Password = userDto.Password // todo Узнать как кэшировать пароль
            };

            _context.Users.Add(user);
            Console.WriteLine($"Добавление пользователя: {user.First_name}, {user.UniqueEmail}"); // Логируем данные
            await _context.SaveChangesAsync();
        }
    }
    
}

