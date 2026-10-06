using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "ایمیل را وارد کنید")]
        [EmailAddress(ErrorMessage = "ایمیل نامعتبر است")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "شماره تلفن را وارد کنید")]
        [MaxLength(11, ErrorMessage = "شماره تلفن نامعتبر است")]
        [MinLength(11, ErrorMessage = "شماره تلفن نامعتبر است")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "کلمه عبور را وارد کنید")]
        [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "تکرار کلمه عبور را وارد کنید")]
        [Compare(nameof(Password), ErrorMessage = "کلمه‌های عبور یکسان نیستند")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}