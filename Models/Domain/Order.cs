using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceWeb.Models.Domain
{
    public class Order
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
        public PaymentStatusEnum PaymentStatus { get; set; } = PaymentStatusEnum.Pending ;

        // Foreign Key relationship to customer
        public Guid CustomerId { get; set; }

        [NotMapped]
        public PaymentStatusEnum OrderPaymentStatusEnum
        {
            get => PaymentStatus;
            set => PaymentStatus = value;
        }

        // Navigation Properties
        public ICollection<OrderDetail>? OrderDetails { get; set; }
        public Customer Customer { get; set; }

    }
}
