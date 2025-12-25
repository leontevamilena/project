using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketLibrary.Contexts;
using MarketLibrary.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(MarketDbContext context) : ControllerBase
    {
        // Внедрение зависимости контекста базы данных через конструктор
        private readonly MarketDbContext _context = context;

        /// <summary>
        /// Получение всех заказов конкретного пользователя
        /// </summary>

        [Authorize]
        [HttpGet("user/{login}")]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrdersByUser(string login)
        {
            // Загрузка заказов из базы данных 
            var orders = await _context.Orders
                .Include(o => o.User)  // Подключаем данные о пользователе
                .Include(o => o.ShoeOrders)  // Подключаем связующие данные о товарах в заказе
                    .ThenInclude(so => so.Shoe)  // Подключаем детальную информацию об обуви
                .Where(o => o.User.Login == login)  // Фильтруем по логину пользователя
                .ToListAsync();  // Асинхронное выполнение запроса

            // Возврат списка заказов со статусом 200 OK
            return Ok(orders);
        }

        /// <summary>
        /// Обновление информации о заказе
        /// </summary>

        // Метод доступен только пользователям с ролями admin или manager
        [Authorize(Roles = "admin,manager")]
        [HttpPut("{orderId}")]
        public async Task<IActionResult> UpdateOrder(int orderId, [FromBody] Order updatedOrder)
        {
            // Поиск заказа по идентификатору
            var order = await _context.Orders.FindAsync(orderId);

            // Проверка существования заказа
            if (order is null)
                return NotFound();  // Возврат 404, если заказ не найден

            // Обновление статуса заказа, если передан новый статус и он отличается от текущего
            if (!string.IsNullOrEmpty(updatedOrder.Status) && updatedOrder.Status != order.Status)
                order.Status = updatedOrder.Status;

            // Обновление даты доставки, если передана новая дата и она отличается от текущей
            if (updatedOrder.DeliveryDate != default && updatedOrder.DeliveryDate != order.DeliveryDate)
                order.DeliveryDate = updatedOrder.DeliveryDate;

            // Отметка сущности как измененной для последующего сохранения
            _context.Update(order);
            // Асинхронное сохранение изменений в базе данных
            await _context.SaveChangesAsync();

            // Возврат 204 No Content при успешном обновлении
            return NoContent();
        }
    }
}

