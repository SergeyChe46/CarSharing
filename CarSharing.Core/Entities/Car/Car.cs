using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace CarSharing.Entities.Car
{
    public class Car
    {
        public Guid Id { get; set; }
        public Guid BrandId { get; set; }
        public CarBrand Brand { get; set; } = null!;
        public string Model { get; set; }
        public int BirthYear { get; set; }
        public DateTime LastRentDate { get; set; }
        public DateTime RentedAt { get; set; }
        public DateTime NumberOfRentDays { get; set; }
        public IdentityUser RentByUser { get; set; }
    }
}
