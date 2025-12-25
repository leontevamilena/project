using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MarketLibrary.Services;
using MarketLibrary.Contexts;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(AuthService authService) : ControllerBase
    {
        // Внедрение зависимости сервиса аутентификации через конструктор
        private readonly AuthService _authService = authService;

        /// <summary>
        /// Обработка запроса на аутентификацию пользователя
        /// </summary>
        [HttpPost("login")]
        public ActionResult Post(string login, string password)
        {
            // Вызов сервиса для аутентификации пользователя и получения токена
            var token = _authService.AuthUser(login, password);

            // Проверка результата аутентификации
            return token == null
                ? Unauthorized()  // Возврат 401, если токен не получен (неверные учетные данные)
                : Ok(new { token });  // Возврат 200 OK с токеном в теле ответа
        }
    }
}
