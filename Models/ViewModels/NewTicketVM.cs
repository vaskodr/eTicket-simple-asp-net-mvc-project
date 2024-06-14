using System.ComponentModel.DataAnnotations;
using Microsoft.VisualBasic.CompilerServices;

namespace finalTicket.Models.ViewModels;

public class NewTicketVM
{
    public int Id { get; set; }

    [Required] 
    public string EventName { get; set; }
    [Required]
    [DataType(DataType.Date)]
    public DateTime EventDate { get; set; }
    [Required]
    [DataType(DataType.Currency)]
    public decimal Price { get; set; }
    [Required]
    public int MovieId { get; set; }
    
}