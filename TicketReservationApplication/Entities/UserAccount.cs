using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace TicketReservationApplication.Entities
{
    [Index(nameof(Email), IsUnique = true)]
    public class UserAccount
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Field is required.")]
        [StringLength(50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        [StringLength(50)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field is required.")]
        [StringLength(100)]
        public string Password { get; set; }

        [StringLength(50)]
        public string Role { get; set; }
    }
}
