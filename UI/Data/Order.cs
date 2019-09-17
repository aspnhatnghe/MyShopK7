using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UI.Common;

namespace UI.Data
{
    [Table("Order")]
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderedDate { get; set; }
        public DateTime? DeliveryDate { get; set; }
        public int CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Open;
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.COD;
        [MaxLength(20)]
        public string CouponCode { get; set; }
        public double Discount { get; set; }
        public double Price { get; set; }
        public double Tax { get; set; }
        public double FeeShip { get; set; }
        public double Total => Price + Tax + FeeShip - Discount;

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public string Receiver { get; set; }
        public string ShipTo { get; set; }
    }
}