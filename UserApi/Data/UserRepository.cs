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
            return await _context.Users.Include(i => i.Address).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users.AsNoTracking().Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            if(user.Address != null)
            {
                user.Address.User = user;
                await _context.Addresses.AddAsync(user.Address);
            }

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> DeleteAsync(int id)
        {
            var user = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == id);

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
            var userToUpdate = await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Id == user.Id);

            if (userToUpdate != null)
            {
                _context.Entry(userToUpdate).CurrentValues.SetValues(user);

                if (userToUpdate.Address != null && user.Address != null)
                {
                    userToUpdate.Address.Street = user.Address.Street;
                    userToUpdate.Address.City = user.Address.City;
                    userToUpdate.Address.State = user.Address.State;
                    userToUpdate.Address.Country = user.Address.Country;
                    userToUpdate.Address.ZipCode = user.Address.ZipCode;
                }
                else if(userToUpdate.Address == null && user.Address != null)
                {
                    _context.Addresses.Add(user.Address);
                    userToUpdate.Address = user.Address;
                }
                else if(userToUpdate.Address != null && user.Address == null)
                {
                    _context.Addresses.Remove(userToUpdate.Address);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task<User> GetWithEmailAsync(string email)
        {
            return await _context.Users.Include(u => u.Address).FirstOrDefaultAsync(u => String.Equals(u.Email, email)); 
        }

        public async Task<bool> DoesUserExistWithEmail(string email)
        {
            return await _context.Users.AnyAsync(u => String.Equals(u.Email, email));
        }
    }
}
