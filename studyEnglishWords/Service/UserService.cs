using studyEnglishWords.Dto;
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
            // Создаём объект User
            var user = new UserModal
            {
                FirstName = userDto.firstName,
                UniqueEmail = userDto.uniqueEmail,
                Password = userDto.password
            };

            // Добавляем пользователя в базу данных
            _context.Users.Add(user);
            Console.WriteLine($"Добавление пользователя: {user.FirstName}, {user.UniqueEmail}");
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("Данный пользователь с данной почтой уже зарегистрирован. Введите другую почту.");
                throw;
            }
        }
        
        public UserModal GetById(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
                throw new KeyNotFoundException($"Пользователь с ID {id} не найден");
    
            return user;
        }
        
        public UserModal? Authenticate(string email, string password)
        {
            // Поиск пользователя по email
            var user = _context.Users.FirstOrDefault(u => u.UniqueEmail == email);
    
            // Простая проверка пароля (без хэширования)
            if (user != null && user.Password == password)
            {
                return user;
            }
    
            return null;
        }


        private bool VerifyPassword(string password, string passwordHash)
        {
            // Здесь должна быть проверка хеша пароля
            // Например, с использованием BCrypt:
            // return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    
            // Временная реализация:
            return true; // Замените на реальную проверку
        }

    }
}