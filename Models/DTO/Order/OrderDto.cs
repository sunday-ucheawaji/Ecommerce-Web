using EcommerceWeb.Models.Domain;
using EcommerceWeb.Models.DTO.Cart;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EcommerceWeb.Models.DTO.OrderDetailFolder;

namespace EcommerceWeb.Models.DTO.Order
{
    public class OrderDto
    {

        public enum PaymentStatusEnum
        {
            Pending = 'P',
            Complete = 'C',
            Failed = 'F'
        }

        public Guid OrderId { get; set; }

        public DateTime PlacedAt { get; set; } = DateTime.Now;

        [Required]
        [StringLength(1)]
        public PaymentStatusEnum PaymentStatus { get; set; } = PaymentStatusEnum.Pending;

        // Foreign Key relationship to customer
        public Guid CustomerId { get; set; }

        [NotMapped]
        public PaymentStatusEnum OrderPaymentStatusEnum
        {
            get => PaymentStatus;
            set => PaymentStatus = value;
        }

        // Navigation Properties
        public ICollection<OrderDetailDto>? OrderDetails { get; set; }
     
      
    }
}
