using proekt.Models;
using System.ComponentModel.DataAnnotations;

namespace proekt.ViewModels
{
    public class Favourite
    {
        public IList<Hotel> Hotels { get; set; }

        [Range(1, 5)]
        public int Rating { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required] public string Name { get; set; }

        [StringLength(30, MinimumLength = 3)]
        [Required] public string City { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required] public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }

        public string Picture { get; set; }

    }
}
