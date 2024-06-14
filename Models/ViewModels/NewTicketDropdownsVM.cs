namespace finalTicket.Models.ViewModels;

public class NewTicketDropdownsVM
{
    public NewTicketDropdownsVM()
    {
        Movies = new List<Movie>();
    }
    
    public List<Movie> Movies { get; set; }
}