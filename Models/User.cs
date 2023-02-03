using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace proekt.Models
{
    public class User
    {
        public int Id { get; set; }
        [Display(Name = "First Name")]
        public string? FirstName { get; set; }
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
      
        public string? FullName
        {
            get { return String.Format("{0} {1}", FirstName, LastName); }
        }
        public string? Email { get; set; }
     
    }
}
