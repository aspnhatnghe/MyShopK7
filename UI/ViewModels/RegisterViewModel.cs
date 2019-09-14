using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class RegisterViewModel
    {
        [MaxLength(100)]
        [Required]
        public string FullName { get; set; }
        [MaxLength(100)]
        [Required]
        [Remote(controller: "Customer", action: "CheckEmailUnique", ErrorMessage = "Email đã được đăng ký")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public string Address { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(30)]
        [Required]
        [Remote(controller: "Customer", action: "CheckUsernameUnique", ErrorMessage = "Tên đăng nhập đã được đăng ký")]
        public string Username { get; set; }
        [MaxLength(100)]
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
