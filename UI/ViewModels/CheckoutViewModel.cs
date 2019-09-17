using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class CheckoutViewModel
    {
        [MaxLength(100)]
        [Required]
        public string FullName { get; set; }
        [MaxLength(100)]
        [Required]
        public string Email { get; set; }

        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(100)]
        public string Receiver { get; set; }
        public string ShipTo { get; set; }
    }
}
