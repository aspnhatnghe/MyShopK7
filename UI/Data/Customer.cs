using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Data
{
    [Table("Customer")]
    public class Customer
    {
        public int CustomerId { get; set; }
        [MaxLength(100)]
        [Required]
        public string FullName { get; set; }
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Address { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        public bool PhoneConfirmed { get; set; }
        [MaxLength(30)]
        [Required]
        public string Username { get; set; }
        [MaxLength(100)]
        [Required]
        public string Password { get; set; }
        [MaxLength(20)]
        public string RandomKey { get; set; }
        public bool IsActive { get; set; }
    }
}
