using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeightWatchers.data.Entities
{
    [Table("Subscriber")]
    public class Subscriber
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [MaxLength(100)]
        public string FirstName { get; set; }
        [MaxLength(100)]
        public string LastName { get; set; }
        [RegularExpression("^([a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,})$", ErrorMessage = "the email is not valid")]
        public string Email { get; set; }
        [MinLength(6)]
        [MaxLength(9)]
        public string Password { get; set; }

    }
}
