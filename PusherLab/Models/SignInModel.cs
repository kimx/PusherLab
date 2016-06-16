using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PusherLab.Models
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Required")]
        [DisplayName("User Name:")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Required")]
        [DisplayName("Custom user data:")]
        public string CustomUserData { get; set; }
    }
}