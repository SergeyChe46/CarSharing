using CarSharing.Context;
using CarSharing.Entities.Car;
using CarSharing.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarSharing.Core.Repository
{
    public class CarRepository : BaseRepository<Car>
    {
        public CarRepository(ApplicationContext context) : base(context)
        {
        }
        public override async Task<List<Car>> GetAll()
        {
            var cars = await _context.Cars
                .Select(car => new Car
                {
                    Id = car.Id,
                    BrandId = car.BrandId,
                    Brand = car.Brand
                })
                .ToListAsync();
            return cars;
        }
        public override async Task Create(Car entity)
        {
            CarBrand? brand = _context.Brands.FirstOrDefault(brand =>
                      brand.BrandName.ToLower() == entity.Brand.BrandName.ToLower());

            if (brand == null)
            {
                brand = new CarBrand
                {
                    Id = Guid.NewGuid(),
                    BrandName = entity.Brand.BrandName,
                };
            }
            try
            {
                entity.Brand = brand;
                await _context.Cars.AddAsync(entity);
            }
            catch (Exception ex)
            {
                // TODO
                // Здесь будет логгирование
                System.Console.WriteLine(ex.Message);
            }
        }

        public override async Task Update(Car entity)
        {
            var deletedCar = await _context.Cars
                .SingleOrDefaultAsync<Car>(car => car.Id == entity.Id);

            if (deletedCar != null)
            {
                try
                {
                    _context.Update<Car>(deletedCar);
                }
                catch (Exception ex)
                {
                    // Здесь будет логгирование
                    System.Console.WriteLine(ex.Message);
                }
            }
        }

    }
}
