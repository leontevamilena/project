using MarketLibrary.Contexts;
using MarketLibrary.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    public class ShoesController(MarketDbContext context) : Controller
    {
        // Внедрение зависимости контекста базы данных через конструктор
        private readonly MarketDbContext _context = context;

        /// <summary>
        /// Получение списка всей обуви
        /// </summary>

        // Определение GET запроса по маршруту: api/Shoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Shoe>>> GetShoes()
            // Асинхронное получение всех записей из таблицы Shoes
            => await _context.Shoes.ToListAsync();

        /// <summary>
        /// Получение конкретной модели обуви по артикулу
        /// </summary>

        // Определение GET запроса по маршруту: api/Shoes/{article}
        [HttpGet("{article}")]
        public async Task<ActionResult<Shoe>> GetShoe(string article)
        {
            // Поиск модели обуви по артикулу
            var shoe = await _context.Shoes.FirstOrDefaultAsync(s => s.Article == article);

            // Проверка существования модели
            if (shoe is null)
            {
                // Возврат 404, если обувь с таким артикулом не найдена
                return NotFound();
            }

            // Возврат найденной модели обуви
            return shoe;
        }

        /// <summary>
        /// Полное обновление модели обуви
        /// </summary>

        [HttpPut("{id}")]
        public async Task<IActionResult> PutShoe(int id, Shoe shoe)
        {
            // Проверка соответствия идентификатора в маршруте и в объекте
            if (id != shoe.ShoeId)
            {
                // Возврат 400 при несоответствии идентификаторов
                return BadRequest();
            }

            // Установка состояния сущности как измененной для отслеживания EF Core
            _context.Entry(shoe).State = EntityState.Modified;

            try
            {
                // Попытка сохранить изменения в базе данных
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Обработка исключения параллельного доступа к данным
                if (!ShoeExists(id))
                {
                    // Возврат 404, если запись была удалена другим пользователем
                    return NotFound();
                }
                else
                {
                    // Повторное выбрасывание исключения, если причина другая
                    throw;
                }
            }

            // Успешное обновление - возврат 204 No Content
            return NoContent();
        }

        /// <summary>
        /// Создание новой модели обуви
        /// </summary>

        [HttpPost]
        public async Task<ActionResult<Shoe>> PostShoe(Shoe shoe)
        {
            // Добавление новой модели в контекст
            _context.Shoes.Add(shoe);
            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();

            // Возврат 201 Created с заголовком Location и созданной моделью
            return CreatedAtAction("GetShoe", new { article = shoe.Article }, shoe);
        }

        /// <summary>
        /// Удаление модели обуви
        /// </summary>

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoe(int id)
        {
            // Поиск модели по идентификатору
            var shoe = await _context.Shoes.FindAsync(id);

            // Проверка существования модели
            if (shoe is null)
            {
                // Возврат 404, если модель не найдена
                return NotFound();
            }

            // Удаление модели из контекста
            _context.Shoes.Remove(shoe);
            // Сохранение изменений в базе данных
            await _context.SaveChangesAsync();

            // Успешное удаление - возврат 204 No Content
            return NoContent();
        }

        /// <summary>
        /// Проверка существования модели обуви по идентификатору
        /// </summary>
        private bool ShoeExists(int id)
        {
            // Проверка наличия записи с указанным идентификатором
            return _context.Shoes.Any(e => e.ShoeId == id);
        }
    }
}
