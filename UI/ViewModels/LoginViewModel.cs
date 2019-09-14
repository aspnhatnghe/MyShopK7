using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class LoginViewModel
    {
        [MaxLength(30)]
        [Required]
        public string Username { get; set; }
        [MaxLength(100)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
    }
}
