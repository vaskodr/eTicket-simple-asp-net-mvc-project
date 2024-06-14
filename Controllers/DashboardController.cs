using finalTicket.Models.ViewModels;
using finalTicket.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using finalTicket.Models;
using finalTicket.Services.Interface;

namespace finalTicket.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly IOrderService _orderService;
        private readonly UserManager<ApplicationUser> _userManager;

        public DashboardController(ITicketService ticketService, IOrderService orderService, UserManager<ApplicationUser> userManager)
        {
            _ticketService = ticketService;
            _orderService = orderService;
            _userManager = userManager;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> UserDashboard()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return View(tickets);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> BuyTicket(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            var model = new BuyTicketViewModel
            {
                TicketId = ticket.Id,
                EventName = ticket.EventName,
                EventDate = ticket.EventDate,
                Price = ticket.Price
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BuyTicket(BuyTicketViewModel model)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(model.TicketId);
            var user = await _userManager.GetUserAsync(User);

            if (ticket == null || user == null)
            {
                return NotFound();
            }

            if (user.Balance >= ticket.Price)
            {
                user.Balance -= ticket.Price;
                await _userManager.UpdateAsync(user);

                await _orderService.CreateOrderAsync(user.Id, ticket.Id, ticket.Price);

                return RedirectToAction(nameof(UserDashboard));
            }

            ModelState.AddModelError("", "Insufficient balance to purchase this ticket.");
            return View(model);
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> ManageBalance()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var model = new ManageBalanceViewModel
            {
                CurrentBalance = user.Balance
            };

            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "User")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ManageBalance(ManageBalanceViewModel model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            user.Balance += model.AddAmount;
            await _userManager.UpdateAsync(user);

            return RedirectToAction(nameof(UserDashboard));
        }

        [Authorize(Roles = "User")]
        public async Task<IActionResult> MyOrders()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var orders = await _orderService.GetOrdersByUserIdAsync(user.Id);
            return View(orders);
        }
        
        // [Authorize(Roles = "User")]
        // public async Task<IActionResult> FilterTickets(string eventName)
        // {
        //     var tickets = await _ticketService.GetAllTicketsAsync();
        //
        //     if (!string.IsNullOrEmpty(eventName))
        //     {
        //         tickets = tickets.Where(t => t.EventName.Contains(eventName)).ToList();
        //     }
        //
        //     return View(tickets);
        // }
    }
}
