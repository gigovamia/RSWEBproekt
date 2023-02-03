using System.ComponentModel.DataAnnotations;

namespace proekt.ViewModels
{
    public class PictureVM
    {

        [Range(1, 5)]
        public int Rating { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required] public string Name { get; set; }


        [Required] public string? City { get; set; }


        [StringLength(60, MinimumLength = 3)]
        [Required] public string Address { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [StringLength(500, MinimumLength = 3)]
        public string Description { get; set; }

        public IFormFile ProfileImage { get; set; }
    }
}
