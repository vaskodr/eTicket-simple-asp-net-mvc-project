using finalTicket.Models;

namespace finalTicket.Services.Interface;

public interface IOrderService
{
    Task CreateOrderAsync(string userId, int ticketId, decimal amount);
    Task<List<Order>> GetOrdersByUserIdAsync(string userId);
    
    Task<List<Order>> GetAllOrdersAsync();
}