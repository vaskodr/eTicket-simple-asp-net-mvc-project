using finalTicket.Models;

namespace finalTicket.Services.Interface;

public interface IUserService
{
    Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName);
}