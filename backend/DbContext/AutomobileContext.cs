using backend.Entities;
using Microsoft.EntityFrameworkCore;
using backend.Models.Shared;

namespace backend.DbContexts
{
    public class AutomobileContext : DbContext
    {
        public DbSet<CatalogItem> CatalogItems {get; set; }
        public DbSet<Model> Models {get; set; }
        public DbSet<Reservation> Reservations {get; set; }
        public DbSet<DriversDetails> DriversDetails {get; set; }
        public DbSet<User> Users {get; set; }
        public DbSet<Supplier> Suppliers {get; set; }
        public DbSet<Location> Locations {get; set; }
        public DbSet<CarCompany> CarCompanies {get; set; }

        public DbSet<AdditionalService> AdditionalServices {get; set;}

        public AutomobileContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Model>()
            //     .Property(m => m.Company)
            //     .HasConversion<string>();

            modelBuilder.Entity<Model>()
                .Property(m => m.Gear)
                .HasConversion<string>();

            modelBuilder.Entity<Model>()
                .Property(m => m.Engine)
                .HasConversion<string>();

             modelBuilder.Entity<AdditionalService>()
                .Property(m => m.ServiceCategory)
                .HasConversion<string>();

            modelBuilder.Entity<AdditionalService>().HasData(
                new AdditionalService()
                {
                    Id = 1,
                    Name = "Additional Insurance",
                    Description = "",
                    ServiceCategory =  ServiceCategory.INSURANCE,
                    Price  = 15,
                },
                 new AdditionalService()
                {
                    Id = 2,
                    Name = "Bike holder",
                    Description = "",
                    ServiceCategory =  ServiceCategory.BIKE_HOLDER,
                    Price  = 10,
                },
                new AdditionalService()
                {
                    Id = 3,
                    Name = "Child seat",
                    Description = "",
                    ServiceCategory =  ServiceCategory.CHILD_SEAT,
                    Price  = 15,
                },
                new AdditionalService()
                {
                    Id = 4,
                    Name = "Animal Carrier",
                    Description = "",
                    ServiceCategory =  ServiceCategory.ANIMAL_CARRIER,
                    Price  = 7,
                }
            );

            modelBuilder.Entity<CarCompany>().HasData(
                new CarCompany()
                {
                    Id = 1,
                    Code = "FIAT",
                    Name = "Fiat"
                },
                new CarCompany()
                {
                    Id = 2,
                    Code = "TOYOTA",
                    Name = "Toyota"
                }
            );

            modelBuilder.Entity<Model>().HasData(
                new Model()
                {
                    Id = 1,
                    CarCompanyId = 2,
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
                    CarCompanyId = 1,
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
                    CarCompanyId = 1,
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

            modelBuilder.Entity<Supplier>().HasData(
                new Supplier("Europcar")
                {
                    Id = 1,
                    LogoUrl = "https://www.europcar.com/_nuxt/img/europcar-signature-green@3x.a2d761a.png"
                },
                new Supplier("Enterpsie")
                {
                    Id = 2,
                    LogoUrl = "https://www.enterprise.com/content/experience-fragments/ecom/en/footer/master/_jcr_content/root/footer/footer/container/container/image.coreimg.png/1692607172448/logo-enterprise.png"
                }
            );

            modelBuilder.Entity<CatalogItem>().HasData(
                new CatalogItem()
                {
                    Id = 1,
                    ModelId = 1,
                    Price = 50,
                    SupplierId = 1,
                    LocationId = 1,
                },
                new CatalogItem()
                {
                    Id = 2,
                    ModelId = 2,
                    Price = 80,
                    SupplierId = 1,
                    LocationId = 1,
                },
                new CatalogItem()
                {
                    Id = 3,
                    ModelId = 3,
                    Price = 60,
                    SupplierId = 2,
                    LocationId = 1,
                }
            );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation()
                {
                    Id = 1,
                    CatalogItemId = 1,  // Use the appropriate CatalogItemId from your data
                    UserId = 1,  // Use the appropriate UserId from your data
                    BeginTime = DateTime.UtcNow.AddDays(1),
                    EndTime = DateTime.UtcNow.AddDays(3),
                    DriversDetailsId = 1  // Use the appropriate DriversDetailsId from your data
                },
                new Reservation()
                {
                    Id = 2,
                    CatalogItemId = 2,
                    UserId = 2,
                    BeginTime = DateTime.UtcNow.AddDays(5),
                    EndTime = DateTime.UtcNow.AddDays(8),
                    DriversDetailsId = 2
                }
            );

            modelBuilder.Entity<DriversDetails>().HasData(
                new DriversDetails()
                {
                    Id = 1,
                    Name = "John",
                    Surname = "Doe",
                    Country = "USA",
                    Number = "123456789"
                },
                new DriversDetails()
                {
                    Id = 2,
                    Name = "Jane",
                    Surname = "Smith",
                    Country = "Canada",
                    Number = "987654321"
                }
            );

            modelBuilder.Entity<User>().HasData(
                new User()
                {
                    Id = 1,
                    Email = "alice@example.com",
                    Password="pass1"
                },
                new User()
                {
                    Id = 2,
                    Email = "bob@example.com",
                    Role = "supplier",
                    Password="pass1"
                }
            );

            modelBuilder.Entity<Location>().HasData(
                new Location()
                {
                    Id = 1,
                    CityName = "Warsaw",
                    CountryName = "Poland"
                },
                 new Location()
                {
                    Id = 2,
                    CityName = "Prague",
                    CountryName = "Chech Republic"
                },
                 new Location()
                {
                    Id = 3,
                    CityName = "Cracow",
                    CountryName = "Poland"
                },
                 new Location()
                {
                    Id = 4,
                    CityName = "Gdansk",
                    CountryName = "Poland"
                },
                 new Location()
                {
                    Id = 5,
                    CityName = "Berlin",
                    CountryName = "Germany"
                },
                 new Location()
                {
                    Id = 6,
                    CityName = "Paris",
                    CountryName = "France"
                }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}