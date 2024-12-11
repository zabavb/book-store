using Library.Validators;
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
        [StringLength(100, ErrorMessage = "Region must be less than 100 characters.")]
        [RegularExpression("^[a-zA-Z\\s'-\\.]+$")]
        public string Region { get; set; } = string.Empty;
        [Required]
        [StringLength(100, ErrorMessage = "City must be less than 100 characters.")]
        [RegularExpression("^[a-zA-Z\\s'-\\.]+$")]
        public string City { get; set; } = string.Empty;
        [Required]
        [StringLength(255,ErrorMessage = "Address must be less than 255 characters.")]
        [RegularExpression("^[a-zA-Z0-9\\s,.-#]*$")]
        public string Address { get; set; } = string.Empty;

        [Required]
        [Range(0, float.MaxValue)]
        public float Price { get; set; }
        [Required]
        [EnumRange(typeof(DeliveryType))]
        public DeliveryType Delivery { get; set; }
        [Required]
        [Range(0, float.MaxValue)]
        public float DeliveryPrice { get; set; }

        public DateTime DeliveryDate { get; set; } = DateTime.Now;
        [Required]
        public DateTime DeliveryTime { get; set; }  // + 2 days, in case of Nova Post

        [EnumRange(typeof(OrderStatus))]
        public OrderStatus Status { get; set; } = OrderStatus.RECEIVED;
    }
}
