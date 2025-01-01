using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace TicketReservationApplication.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Field is required.")]
        [StringLength(50)]
        //[EmailAddress(ErrorMessage ="Enter proper address")]
        [RegularExpression(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
            ErrorMessage = "Please Enter Valid Email.")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Minimum 6 and maximum 50 characters allowed")]
        [DataType(DataType.Password)]
        public string Password { get; set; }



        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}
