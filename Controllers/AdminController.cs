using finalTicket.Data;
using finalTicket.Models;
using finalTicket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using finalTicket.Services.Interface;

namespace finalTicket.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public AdminController(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }
        

        public async Task<IActionResult> UserOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return View(orders);
        }
        
        public async Task<IActionResult> Users()
        {
            var users = await _userService.GetUsersInRoleAsync("User");
            return View(users);
        }
    }
}