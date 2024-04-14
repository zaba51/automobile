using automobile.Models;
using backend.Entities;

namespace backend.Services
{
    public class CatalogService : ICatalogService
    {
        private readonly ICatalogRepository _catalogRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public CatalogService(
            ICatalogRepository catalogRepository,
            IWebHostEnvironment hostEnvironment
        )
        {
            _catalogRepository = catalogRepository;
            _hostEnvironment = hostEnvironment;
        }

        public async Task AddCatalogItemAsync(AddCatalogItemDTO item, IFormFile file)
        {

            var existingModel = await _catalogRepository.GetModelByQuery((model) => (
                model.CarCompany.Name == item.Model.CarCompany.Name &&
                model.Name == item.Model.Name &&
                model.Power == item.Model.Power &&
                model.Gear == item.Model.Gear &&
                model.DoorCount == item.Model.DoorCount &&
                model.SeatCount == item.Model.SeatCount &&
                model.Engine == item.Model.Engine &&
                model.Color == item.Model.Color
            ));
            CatalogItem newItem;

            if (existingModel != null)
            {
                newItem = new CatalogItem()
                {
                    ModelId = existingModel.Id,
                    Price = item.Price,
                    SupplierId = item.SupplierId,
                    LocationId = item.LocationId
                };
            }
            else
            {
                await FillNewModel(item, file);

                _catalogRepository.AddModel(item.Model);

                await _catalogRepository.SaveChangesAsync();

                var model = await _catalogRepository.GetModelByQuery((model) => model == item.Model);

                newItem = new CatalogItem()
                {
                    ModelId = model.Id,
                    Price = item.Price,
                    SupplierId = item.SupplierId,
                    LocationId = item.LocationId
                };
            }

            _catalogRepository.AddCatalogItem(newItem);

            await _catalogRepository.SaveChangesAsync();

        }

        private async Task FillNewModel(AddCatalogItemDTO item, IFormFile file)
        {
            string[] paths = { _hostEnvironment.WebRootPath, "assets", "cars" };
            string relativeFilePath = await UploadService.Upload(file, paths);

            var company = await _catalogRepository.GetCarCompanyByQuery((company) => company.Name == item.Model.CarCompany.Name);

            if (company == null) throw new Exception("No such company");

            item.Model.ImageUrl = relativeFilePath;
            item.Model.CarCompany.Id = company.Id;
        }
    }
}
