using System.Data;
using System.Linq.Expressions;
using automobile.Helpers;
using backend.DbContexts;
using backend.Entities;
using backend.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Services {
    public class UserRepository: AutomobileRepository, IUserRepository
    { 
        public UserRepository(AutomobileContext context)
            :base(context)
        {}

        public async Task<bool> UserExistsAsync(int userId)
        {
            return await _context.Users.AnyAsync(u => u.Id == userId);
        }

        public async Task<User?> GetUserByCredentialsAsync(string? email, string? password)
        {
            return await _context.Users.Where(u => u.Email == email && u.Password == password).FirstOrDefaultAsync();
        }

        public async Task<User?> GetSingleUserAsync(int userID)
        {
            return await _context.Users.Where(u => u.Id == userID).FirstOrDefaultAsync();
        }

        public async Task<User?> GetUserByUsernameAsync(string email) {
            return await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<User?> GetUserByQuery(Expression<Func<User, bool>>  predicate) {
            return await _context.Users
                .Where(predicate)
                .FirstOrDefaultAsync();
        }
    }
}