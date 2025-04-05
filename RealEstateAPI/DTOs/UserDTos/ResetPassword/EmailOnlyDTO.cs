using System.ComponentModel.DataAnnotations;

namespace RealEstateAPI.DTOs.UserDTos.ResetPassword
{
    public class EmailOnlyDTO

    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
