using Microsoft.EntityFrameworkCore;
using UserApi.Models;

namespace UserApi.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;

        public UserRepository(UserDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);

            if(user == null)
            {
                return null;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user; 
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetWithEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => String.Equals(u.Email, email)); 
        }

        public async Task<bool> DoesUserExistWithEmail(string email)
        {
            return await _context.Users.AnyAsync(u => String.Equals(u.Email, email));
        }
    }
}
