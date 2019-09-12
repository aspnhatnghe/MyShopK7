using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using UI.Common;

namespace UI.Data
{
    [Table("Coupon")]
    public class Coupon
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string CouponCode{ get; set; }
        [Range(0, 20)]
        public byte? Discount { get; set; }
        public double? Voucher { get; set; }
        public DateTime ExpireDate { get; set; }
        public CouponStatus Status { get; set; }
    }
}
