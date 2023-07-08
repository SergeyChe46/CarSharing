using CarSharing.Context;
using CarSharing.Entities.Car;
using CarSharing.Repositories;

namespace CarSharing.Core.Repository
{
    public class CarRepository : BaseRepository<Car>
    {
        public CarRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
