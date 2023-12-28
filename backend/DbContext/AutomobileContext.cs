using backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace backend.DbContexts
{
    public class AutomobileContext : DbContext
    {
        public DbSet<CatalogItem> CatalogItems {get; set; }

        public AutomobileContext(DbContextOptions options) : base(options)
        {
        }
    }
}