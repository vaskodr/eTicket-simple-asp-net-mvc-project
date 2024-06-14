namespace finalTicket.Models;

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime ReleaseDate { get; set; }
    public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
}