using finalTicket.Models;
using finalTicket.Models.ViewModels;

namespace finalTicket.Services.Interface;

public interface ITicketService
{
    Task AddNewTicketAsync(NewTicketVM data);
    Task UpdateTicketAsync(NewTicketVM data);
    Task<NewTicketDropdownsVM> GetNewTicketDropdownsValues();
    Task<NewTicketVM> GetTicketByIdAsync(int id);
    Task<List<Ticket>> GetAllTicketsAsync();
    
}