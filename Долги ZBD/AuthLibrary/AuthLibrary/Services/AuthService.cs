using AuthLibrary.Contexts;
using AuthLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthLibrary.Services
{
    public class AuthService(CinemaDbContext context)
    {
        private readonly CinemaDbContext _context = context;
        private string HashPassword(string password)
            => BCrypt.Net.BCrypt.HashPassword(password);

        public async Task<bool> RegisterUserAsync(string login, string password)
        {
            var selectUser = await FindUserByLoginAsync(login);

            if (selectUser is not null) 
                return false;

            CinemaUser user = new()
            {
                Login = login,
                HashPassword = HashPassword(password),
                UserRole = await GetRoleByNameAsync("Посетитель")
            };

            try
            {
                await AddUsersAsync(user);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<CinemaUser?> AuthenticateUserAsync(string login, string password)
        {
            var user = await FindUserByLoginAsync(login);

            if (user is null)
            {
                return null; 
            }

            if (user.DateUnlock.HasValue && user.DateUnlock > DateTime.Now)
            {
                return null;
            }

            if (BCrypt.Net.BCrypt.Verify(password, user.HashPassword))
            {
                user.NumberAttempsPassword = 0; 
                return user; 
            }
            else
            {
                user.NumberAttempsPassword++;
                if (user.NumberAttempsPassword >= 3)
                {
                    user.DateUnlock = DateTime.Now.AddMinutes(1); 
                }
                return null; 
            }
        }

        public async Task<CinemaUserRole?> GetUserRoleByLoginAsync(string login)
        {
            var user = await _context.CinemaUsers
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Login == login);
            return user?.UserRole;
        }

        public async Task<IEnumerable<CinemaPrivilege>?> GetUserPrivilegeByLoginAsync(string login)
        {
            var role = await GetUserRoleByLoginAsync(login);
            return role?.Privileges.ToList();
        }

        public async Task<IEnumerable<CinemaPrivilege>?> GetUserPrivilegeByRoleAsync(CinemaUserRole role)
        {
            var selectRole = await GetRoleByNameAsync(role.Name);
            return selectRole?.Privileges.ToList();
        }

        private async Task AddUsersAsync(CinemaUser user)
        {
            await _context.CinemaUsers.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        private async Task<CinemaUser?> FindUserByLoginAsync(string login)
            => await _context.CinemaUsers
            .FirstOrDefaultAsync(u => u.Login == login);

        private async Task<CinemaUserRole?> GetRoleByNameAsync(string name)
            => await _context.CinemaUserRoles
            .FirstOrDefaultAsync(r => r.Name == name);
    }
}
