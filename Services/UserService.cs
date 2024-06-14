using finalTicket.Data;
using finalTicket.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace finalTicket.Services.Interface;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(AppDbContext context, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<List<ApplicationUser>> GetUsersInRoleAsync(string roleName)
    {
        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        if (role == null)
        {
            return new List<ApplicationUser>();
        }

        var userIds = await _context.UserRoles
            .Where(ur => ur.RoleId == role.Id)
            .Select(ur => ur.UserId)
            .ToListAsync();

        var users = await _context.Users
            .Where(u => userIds.Contains(u.Id))
            .ToListAsync();

        return users;
    }
}