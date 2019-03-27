using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prolab04
{
    class User
    {
        public string ID { get; set; }
        public List<Reservation> Reservations = new List<Reservation>();
        public User(string ID)
        {
            this.ID = ID;
        }
        public void printReservations()
        {
            foreach (var reservation in Reservations)
            {
                Console.WriteLine("{0}, {1}, {2}, {3}, {4}", reservation.PlaceName, reservation.Time, reservation.Longitude, reservation.Latitude, reservation.City);
            }
        }
        public bool printFromPlace(string placeName, string categoryName)
        {
            foreach (var reservation in Reservations)
            {
                if (reservation.PlaceName == placeName)
                {
                    Console.WriteLine(ID + " Category Name: " + categoryName);
                    return true;
                }
            }
            return false;
        }
    }
}