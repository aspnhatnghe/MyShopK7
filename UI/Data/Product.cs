using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UI.Data
{
    [Table("Product")]
    public class Product
    {
        public int ProductId { get; set; }
        [MaxLength(50)]
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        [Range(0, 50)] //percent %
        public byte Discount { get; set; }
        public int Amount { get; set; }
        public bool HasDiscount => Discount > 0;
        public double SalePrice => Price * (100 - Discount) / 100;
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public int? SupplierId { get; set; }
        [ForeignKey("SupplierId")]
        public Manufacturer Manufacturer { get; set; }
        public string MainImage { get; set; }
    }

    [Table("ProductImage")]
    public class ProductImage
    {
        public int Id { get; set; }
        [MaxLength(150)]
        public string FileName { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
    }
}
