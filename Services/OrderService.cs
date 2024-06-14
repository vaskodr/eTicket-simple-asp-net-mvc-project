using finalTicket.Data;
using finalTicket.Models;
using Microsoft.EntityFrameworkCore;
using finalTicket.Services.Interface;

namespace finalTicket.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateOrderAsync(string userId, int ticketId, decimal amount)
        {
            var order = new Order
            {
                UserId = userId,
                TicketId = ticketId,
                Amount = amount,
                OrderDate = DateTime.UtcNow
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Include(o => o.Ticket)
                .Include(o => o.User)
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }
        
        public async Task<List<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Ticket)
                .Include(o => o.User)
                .ToListAsync();
        }
    }
}