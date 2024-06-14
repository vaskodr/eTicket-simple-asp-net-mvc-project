using Microsoft.AspNetCore.Identity;

namespace finalTicket.Models;

public class ApplicationUser : IdentityUser
{
    public decimal Balance { get; set; } = 0;
}