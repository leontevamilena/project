using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MarketLibrary.Contexts;
using MarketLibrary.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MarketLibrary.Services
{
    public class AuthService(MarketDbContext context)
    {
        // Внедрение зависимости контекста базы данных через конструктор
        private readonly MarketDbContext _context = context;

        // Секретный ключ для подписи JWT-токенов
        public static readonly string _secretKey = "12345678123456781234567812345678";

        /// <summary>
        /// Генерация JWT-токена для пользователя
        /// </summary>
        public string GenerateToken(User user)
        {
            // Создание ключа безопасности на основе секретного ключа
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

            // Создание учетных данных для подписи токена с использованием алгоритма HMAC-SHA256
            var authority = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // Формирование утверждений (claims) для включения в токен
            var claims = new Claim[]
            {
            // Идентификатор пользователя
            new(ClaimTypes.NameIdentifier, user.UserId.ToString()), 
            
            // Логин пользователя
            new(ClaimTypes.Name, user.Login),  
            
            // Роль пользователя 
            new(ClaimTypes.Role, user.Role.Name)
            };

            // Создание JWT-токена с указанными параметрами
            var token = new JwtSecurityToken(
                signingCredentials: authority, // Учетные данные для подписи
                claims: claims,                // Утверждения (claims)
                expires: DateTime.UtcNow.AddMinutes(15) // Время жизни токена - 15 минут
                );

            // Преобразование токена в строковый формат JWT
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        /// <summary>
        /// Аутентификация пользователя по логину и паролю
        /// </summary>
        public string AuthUser(string login, string password)
        {
            // Поиск пользователя в базе данных
            var user = _context.Users
                .Include(u => u.Role)  // Загрузка связанной сущности Role
                .FirstOrDefault(u => u.Login == login && u.Password == password);

            // Если пользователь не найден или пароль неверный, возвращаем null
            if (user is null)
                return null;

            // Генерация токена для аутентифицированного пользователя
            return GenerateToken(user);
        }
    }
}
