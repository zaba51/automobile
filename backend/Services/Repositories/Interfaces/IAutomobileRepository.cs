using System.Data;
using System.Linq.Expressions;
using backend.Entities;
using backend.Models;
using Microsoft.EntityFrameworkCore.Storage;

namespace backend.Services {
    public interface IAutomobileRepository
    {
        Task<bool> SaveChangesAsync();
        Task<IEnumerable<Location>> GetLocations();
        IDbContextTransaction BeginTransaction(IsolationLevel isolationLevel);
    }
}
