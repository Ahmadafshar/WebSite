using System.ComponentModel.DataAnnotations;

namespace BaseCMS.Models
{
    public class Account
    {
        [Display(Name = "ID:")]
        public long ID { get; set; }

        [Display(Name = "Email:")]
        public string Email { get; set; }

        [Display(Name = "Role:")]
        public string Role { get; set; }

        [Display(Name = "Active:")]
        public bool Active { get; set; }

        [Required(ErrorMessage = "User name is required")]
        [Display(Name = "User name:")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password:")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required")]
        [Display(Name = "Confirm Password:")]
        public string ConfirmPassword { get; set; }
    }
}