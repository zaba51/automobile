using backend.Entities;
using backend.Exceptions;
using backend.Models;

namespace backend.Services
{
    public class ReservationService: IReservationService
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICatalogRepository _catalogRepository;
        private readonly KafkaProducerService _kafkaProducer;
        public ReservationService(
            IReservationRepository reservationRepository,
            IUserRepository userRepository,
            ICatalogRepository catalogRepository,
            KafkaProducerService kafkaProducer
        )
        {
            _userRepository = userRepository;
            _reservationRepository = reservationRepository;
            _catalogRepository = catalogRepository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<(Reservation NewReservation, CatalogItem CatalogItem)> AddReservationAsync(int userId, AddReservationDTO reservation)
        {
            var transaction = _reservationRepository.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);

            var allServices = await _catalogRepository.GetAdditionalServices();
            var additionalServices = allServices.Where(c => reservation.AdditionalServiceIds.Contains(c.Id));

            var newReservation = new Reservation()
            {
                CatalogItemId = reservation.CatalogItemId,

                UserId = reservation.UserId,

                BeginTime = reservation.BeginTime,

                EndTime = reservation.EndTime,

                DriversDetails = new DriversDetails()
                {
                    Name = reservation.DriversDetails.Name,
                    Surname = reservation.DriversDetails.Surname,
                    Country = reservation.DriversDetails.Country,
                    Number = reservation.DriversDetails.Number
                }
            };
            newReservation.AdditionalServices.AddRange(additionalServices);

            var catalogItem = await _catalogRepository.GetItemByQuery(item => item.Id == newReservation.CatalogItemId);

            if (catalogItem == null)
                throw new ElementNotFoundException();

            bool canAddReservation = await CanAddReservation(newReservation, catalogItem);
            if (!canAddReservation)
                throw new CancellationExpiredException();

            _reservationRepository.AddReservation(userId, newReservation);

            await _reservationRepository.SaveChangesAsync();

            transaction.Commit();

            return (newReservation, catalogItem);
        }

         private async Task<bool> CanAddReservation(Reservation newReservation, CatalogItem catalogItem)
    {
        IEnumerable<CatalogItem>? availableItems = await _catalogRepository
        .GetMatchingCatalogItemsAsync(
            newReservation.BeginTime,
            (newReservation.EndTime - newReservation.BeginTime).Hours,
            catalogItem.LocationId
        );

        // bool isAvailable = availableItems.Any(i => i.Id == newReservation.Id);
        bool isAvailable = availableItems.Contains(catalogItem);

        return isAvailable;
    }
    }
}