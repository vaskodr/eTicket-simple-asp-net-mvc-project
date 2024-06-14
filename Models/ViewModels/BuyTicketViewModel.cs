using System.ComponentModel.DataAnnotations;

namespace finalTicket.Models.ViewModels;

public class BuyTicketViewModel
{
    public int TicketId { get; set; }

    [Display(Name = "Event Name")]
    public string EventName { get; set; }

    [Display(Name = "Event Date")]
    public DateTime EventDate { get; set; }

    [Display(Name = "Price")]
    public decimal Price { get; set; }
}