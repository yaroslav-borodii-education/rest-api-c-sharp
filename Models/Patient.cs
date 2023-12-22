using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MagicVilla_VillaAPI.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public required string FirstName { get; set; }    

        public string? LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public  string? Address { get; set; }
        
        public string? PhoneNumber { get; set; }

        public required string Email { get; set; }

        
    }
}
