using System.ComponentModel.DataAnnotations;

namespace zelal_tunc_211216069.Models
{
    public class LoginViewModel
    {
        [Required (ErrorMessage ="Lütfen bir kullanıcı adı giriniz.")]
        [StringLength(25)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Lütfen bir şifre giriniz.")]
        [MaxLength(16)]
        [MinLength(5)]
        public string Password { get; set; }
    }
}
