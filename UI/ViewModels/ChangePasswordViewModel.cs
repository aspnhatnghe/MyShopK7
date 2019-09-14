using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.ViewModels
{
    public class ChangePasswordViewModel
    {
        [MaxLength(100)]
        [Required]
        [DataType(DataType.Password)]
        [Display(Name ="Mật khẩu cũ")]
        public string OldPassword { get; set; }
        [MaxLength(100)]
        [Required]
        [Display(Name = "Mật khẩu mới")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }        
        [DataType(DataType.Password)]
        [Display(Name = "Nhập lại mật khẩu mới")]
        [Compare("NewPassword", ErrorMessage ="Không khớp")]
        public string ConfirmNewPassword { get; set; }
    }
}
