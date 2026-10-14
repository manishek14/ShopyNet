using System.ComponentModel.DataAnnotations;

namespace Shop.Api.ViewModels.Auth
{
    public class ResetPasswordViewModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Token { get; set; } = string.Empty;

        [Required(ErrorMessage = "کلمه عبور جدید را وارد کنید")]
        [MinLength(6, ErrorMessage = "کلمه عبور باید بیشتر از 5 کاراکتر باشد")]
        public string NewPassword { get; set; } = string.Empty;

        [Required(ErrorMessage = "تکرار کلمه عبور را وارد کنید")]
        [Compare(nameof(NewPassword), ErrorMessage = "کلمه‌های عبور یکسان نیستند")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}