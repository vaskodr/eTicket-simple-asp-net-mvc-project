namespace finalTicket.Models;

public class Ticket
{
    public int Id { get; set; }
    public string EventName { get; set; }
    public DateTime EventDate { get; set; }
    public decimal Price { get; set; }
    
    // Add Movie navigation property
    public int MovieId { get; set; }
    public Movie Movie { get; set; }
}