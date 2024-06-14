using finalTicket.Data;
using finalTicket.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;
using finalTicket.Models.ViewModels;
using finalTicket.Services.Interface;

namespace finalTicket.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TicketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly ITicketService _service;

        public TicketController(AppDbContext context, ITicketService service)
        {
            _context = context;
            _service = service;
        }

        // GET: Ticket
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets.Include(t => t.Movie).ToListAsync();
            return View(tickets);
        }
        
        // GET: Ticket/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Ticket/Create
        // GET: Ticket/Create
        public async Task<IActionResult> Create()
        {
            var ticketDropdownsData = await _service.GetNewTicketDropdownsValues();
            ViewBag.Movies = new SelectList(ticketDropdownsData.Movies, "Id", "Title");
            return View();
        }

        // POST: Ticket/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(NewTicketVM ticket)
        {
            if (!ModelState.IsValid)
            {
                var ticketDropdownData = await _service.GetNewTicketDropdownsValues();
                ViewBag.Movies = new SelectList(ticketDropdownData.Movies, "Id", "Title");
                return View(ticket);
            }

            await _service.AddNewTicketAsync(ticket);
            return RedirectToAction(nameof(Index));
        }

        // GET: Ticket/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var ticket = await _service.GetTicketByIdAsync(id);

            var ticketDropdownsData = await _service.GetNewTicketDropdownsValues();
            ViewBag.Movies = new SelectList(ticketDropdownsData.Movies, "Id", "Title");
            return View(ticket);

        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(NewTicketVM ticket)
        {
            if (!ModelState.IsValid)
            {
                var ticketDropdownsData = await _service.GetNewTicketDropdownsValues();
                ViewBag.Movies = new SelectList(ticketDropdownsData.Movies, "Id", "Title");
                return View(ticket);
            }

            await _service.UpdateTicketAsync(ticket);
            return RedirectToAction(nameof(Index));
        }

        // GET: Ticket/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
