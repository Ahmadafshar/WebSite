using System.ComponentModel.DataAnnotations;

namespace BaseCMS.Models
{
    public class Login
    {
        [Required(ErrorMessage = "نام کاربری لازم است")]
        [Display(Name = "نام کاربری ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "رمز عبور لازم است")]
        [Display(Name = "رمز عبور ")]
        public string Password { get; set; }
    }
}