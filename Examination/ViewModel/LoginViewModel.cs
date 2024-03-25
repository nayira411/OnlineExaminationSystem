using System.ComponentModel.DataAnnotations;

namespace Examination.ViewModel
{
    public partial class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
