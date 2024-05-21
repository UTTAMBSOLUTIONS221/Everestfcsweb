using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace Everestfcsweb.Models
{
    public class Loginviewmodel
    {
        [Required(ErrorMessage = "Email Address is required!")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter a valid email address")]
        [Display(Name = "Emailaddress")]
        public string? Emailaddress { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        [DataType(DataType.Password, ErrorMessage = "Enter a valid password")]
        [Display(Name = "Password")]
        public string? Password { get; set; }
    }
}
