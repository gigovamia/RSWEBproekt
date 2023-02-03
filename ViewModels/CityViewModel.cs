using Microsoft.AspNetCore.Mvc.Rendering;
using proekt.Models;

namespace proekt.ViewModels
{
    public class CityViewModel
    {
        public List<Hotel>? Hotels { get; set; }
        public List<GuestHouse>? GuestHouses { get; set; }
        public List<Visit>? Visits { get; set; }
        public SelectList? Cities { get; set; }
        public string? HotelCity { get; set; }
        public string? SearchString { get; set; }
    }
}
