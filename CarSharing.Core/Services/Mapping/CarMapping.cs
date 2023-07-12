using System.Reflection;
using CarSharing.Core.Entities.Car;
using CarSharing.Entities.Car;
using Mapster;

namespace CarSharing.Core.Services.Mapping
{
    public static class CarMapping
    {
        public static void CarMapper(this IServiceCollection services)
        {
            TypeAdapterConfig<CarViewModel, Car>
               .NewConfig()
               .Map(dst => dst.Brand, src => new CarBrand { BrandName = src.Brand });
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());
        }
    }
}
