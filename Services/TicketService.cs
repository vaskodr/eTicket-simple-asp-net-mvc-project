using finalTicket.Data;
using finalTicket.Models;
using finalTicket.Models.ViewModels;
using finalTicket.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace finalTicket.Services;

public class TicketService : ITicketService
{
    private readonly AppDbContext _context;

    public TicketService(AppDbContext context)
    {
        _context = context;
    }


    public async Task AddNewTicketAsync(NewTicketVM data)
    {
        var newTicket = new Ticket()
        {
            EventName = data.EventName,
            EventDate = data.EventDate,
            Price = data.Price,
            MovieId = data.MovieId
        };
        await _context.Tickets.AddAsync(newTicket);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateTicketAsync(NewTicketVM data)
    {
        var ticket = await _context.Tickets.FindAsync(data.Id);
        if (ticket != null)
        {
            ticket.EventName = data.EventName;
            ticket.EventDate = data.EventDate;
            ticket.Price = data.Price;
            ticket.MovieId = data.MovieId;

            _context.Tickets.Update(ticket);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<NewTicketDropdownsVM> GetNewTicketDropdownsValues()
    {
        var response = new NewTicketDropdownsVM()
        {
            Movies = await _context.Movies.OrderBy(n => n.Title).ToListAsync()
        };
        return response;
    }

    public async Task<NewTicketVM> GetTicketByIdAsync(int id)
    {
        var ticket = await _context.Tickets.FindAsync(id);
        if (ticket == null) return null;

        var ticketVM = new NewTicketVM()
        {
            Id = ticket.Id,
            EventName = ticket.EventName,
            EventDate = ticket.EventDate,
            Price = ticket.Price,
            MovieId = ticket.MovieId
        };

        return ticketVM;
    }
    
    public async Task<List<Ticket>> GetAllTicketsAsync()
    {
        return await _context.Tickets.Include(t => t.Movie).ToListAsync();
    }
    
}