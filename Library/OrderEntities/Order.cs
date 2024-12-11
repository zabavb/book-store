using System.ComponentModel.DataAnnotations;

namespace Library.OrderEntities
{
    public class Order
    {
        [Required]
        public Guid OrderId { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [Required]  
        public List<Guid> BookIds { get; set; } = new List<Guid>();

        [Required]
        [StringLength(20)]
        public string Region { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string City { get; set; } = string.Empty;
        [Required]
        [StringLength(20)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public float Price { get; set; }
        [Required]
        public DeliveryType Delivery { get; set; }
        [Required]
        public float DeliveryPrice { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime DeliveryTime { get; set; }  // + 2 days, in case of Nova Post

        public OrderStatus Status { get; set; } = OrderStatus.RECEIVED;
    }
}
