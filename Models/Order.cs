namespace finalTicket.Models;

public class Order
{
    public int Id { get; set; }
    public string UserId { get; set; } // Changed to string to match ApplicationUser.Id
    public decimal Amount { get; set; }
    public ApplicationUser User { get; set; }
    public int TicketId { get; set; }
    public Ticket Ticket { get; set; }
    public DateTime OrderDate { get; set; }
}