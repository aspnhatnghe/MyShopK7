using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Models
{
    public class CartItem
    {
        [Key]
        public int MaHh { get; set; }
        public string TenHh { get; set; }        
        public string Hinh { get; set; }        
        public double DonGia { get; set; }
        public byte GiamGia { get; set; }
        public double GiaBan => DonGia * (100 - GiamGia) / 100.0;
        [Range(0, int.MaxValue)]
        public int SoLuong { get; set; }
        public double ThanhTien => SoLuong * GiaBan;
    }
}
