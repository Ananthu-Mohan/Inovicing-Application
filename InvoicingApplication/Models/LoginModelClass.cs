using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace InvoicingApplication.Models
{
    public class LoginModelClass
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }


    }
}
