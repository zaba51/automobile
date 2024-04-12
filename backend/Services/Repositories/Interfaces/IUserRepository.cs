using System.Data;
using System.Linq.Expressions;
using backend.Entities;
using backend.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Services {
    public interface IUserRepository: IAutomobileRepository
    {
        Task<bool> UserExistsAsync(int userId);
        Task<User> GetSingleUserAsync(int userId);
        Task<User?> GetUserByUsernameAsync(string email);
        void AddUser(User user);
        Task<User?> GetUserByCredentialsAsync(string? email, string? password);
        Task<User?> GetUserByQuery(Expression<Func<User, bool>>  predicate);
    }
}