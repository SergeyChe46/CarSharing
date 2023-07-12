using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarSharing.Entities.Car;

namespace CarSharing.Core.Entities.Car
{
    public class CarViewModel
    {
        public string Brand { get; set; }
        public string Model { get; set; }
        public int BirthYear { get; set; }
    }
}
