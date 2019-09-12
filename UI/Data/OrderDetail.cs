using System.ComponentModel.DataAnnotations.Schema;

namespace UI.Data
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public double UnitPrice { get; set; }
        public byte Discount { get; set; }
        public double Price => UnitPrice * (100 - Discount) / 100.0;
        public int Quantity { get; set; }
        public double Total => Quantity * Price;

    }
}