using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "شماره تلفن را وارد کنید")]
        [MaxLength(11, ErrorMessage = "شماره تلفن نامعتبر است")]
        [MinLength(11, ErrorMessage = "شماره تلفن نامعتبر است")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
        public string Password { get; set; } = string.Empty;
    }
}