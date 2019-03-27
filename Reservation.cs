using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab04
{
    class Reservation
    {
        public string PlaceName;
        public string Time;
        public float Latitude;
        public float Longitude;
        public string City;
        public Reservation(string PlaceName, string Time, float Latitude, float Longitude, string City)
        {
            this.PlaceName = PlaceName;
            this.Time = Time;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.City = City;
        }
    }
}