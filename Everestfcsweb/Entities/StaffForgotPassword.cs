using System.ComponentModel.DataAnnotations;

namespace Everestfcsweb.Entities
{
    public class StaffForgotPassword
    {
        [Required(ErrorMessage ="Email Address Is Required!")]
        public string? EmailAddress { get; set; }
    }
}
