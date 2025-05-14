using System.ComponentModel.DataAnnotations;

namespace Talabat.APIs.DTOs
{
    public class RegisterDTO
    {
        [Required]
        public string DisplayName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }

        [Required]
        [RegularExpression("(?=^.{6,10}$)(?=.*\\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[!@#$%^&amp;*()_+}{&qout;:;'?/&gt;.&lt;,])(?!.*\\s).*$",
            ErrorMessage = "Password Must Have 1_UpperCase , 1_LowerCase , 1_Number , 1_NonAlphanumeric and at least 6_Characers ")]
        public string Password { get; set; }

    }
}
