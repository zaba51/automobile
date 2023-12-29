using backend.Entities;
using Microsoft.EntityFrameworkCore;
using backend.Models.Shared;

namespace backend.DbContexts
{
    public class AutomobileContext : DbContext
    {
        public DbSet<CatalogItem> CatalogItems {get; set; }
        public DbSet<Model> Models {get; set; }

        public AutomobileContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Model>()
                .Property(m => m.Company)
                .HasConversion<string>();

            modelBuilder.Entity<Model>()
                .Property(m => m.Gear)
                .HasConversion<string>();

            modelBuilder.Entity<Model>()
                .Property(m => m.Engine)
                .HasConversion<string>();
                
            modelBuilder.Entity<Model>().HasData(
                new Model()
                {
                    Id = 1,
                    Company = Company.FIAT,
                    Name = "Panda",
                    Power = 69,
                    Gear = Gear.AUTOMATIC,
                    DoorCount = 5,
                    SeatCount = 5,
                    Engine = Engine.HYBRID,
                    Color = "White",
                    ImageUrl = "https://flib.carshow360.net/700/900/705993b922662ed39c6.webp"
                },
                new Model()
                {
                    Id = 2,
                    Company = Company.TOYOTA,
                    Name = "Corolla",
                    Power = 140,
                    Gear = Gear.AUTOMATIC,
                    DoorCount = 5,
                    SeatCount = 5,
                    Engine = Engine.HYBRID,
                    Color = "Silver",
                    ImageUrl = "https://cfm.pl/wp-content/uploads/2021/04/toyota-corolla-sd-srebrna-dlugoterminowy-glowne-cfm.jpg"
                },
                new Model()
                {
                    Id = 3,
                    Company = Company.TOYOTA,
                    Name = "Corolla",
                    Power = 140,
                    Gear = Gear.MANUAL,
                    DoorCount = 5,
                    SeatCount = 6,
                    Engine = Engine.GASOLINE,
                    Color = "White",
                    ImageUrl = "https://media.ed.edmunds-media.com/toyota/corolla/2023/oem/2023_toyota_corolla_sedan_xse_fq_oem_1_600.jpg"
                }
            );

            modelBuilder.Entity<CatalogItem>().HasData(
                new CatalogItem()
                {
                    Id = 1,
                    ModelId = 1,
                    Price = 50,
                    Supplier = "Europcar"
                },
                new CatalogItem()
                {
                    Id = 2,
                    ModelId = 2,
                    Price = 80,
                    Supplier = "Express"
                },
                new CatalogItem()
                {
                    Id = 3,
                    ModelId = 3,
                    Price = 60,
                    Supplier = "Budget"
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}