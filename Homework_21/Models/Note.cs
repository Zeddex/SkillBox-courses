using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Homework_21.Models
{
    public class Note
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "No name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "No surname")]
        public string Surname { get; set; }

        [Required(ErrorMessage = "No phone")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "No address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "No IBAN")]
        public string Iban { get; set; }
    }
}
