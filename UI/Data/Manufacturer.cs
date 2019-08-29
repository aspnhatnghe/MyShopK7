using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Data
{
    [Table("Manufacturer")]
    public class Manufacturer
    {
        public int Id { get; set; }
        [MaxLength(150)]
        [Required]
        public string Name { get; set; }
        [MaxLength(150)]
        public string Address { get; set; }
        [MaxLength(50)]
        [Required]
        public string Phone { get; set; }
        [MaxLength(150)]
        public string Email { get; set; }
    }
}
