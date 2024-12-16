using Library.OrderEntities;

namespace OrderApi.Models
{
    internal class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> BookIds { get; set; }

        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public float Price { get; set; }
        public DeliveryType Delivery { get; set; }
        public float DeliveryPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        public OrderStatus Status { get; set; }

        public Order()
        {
            BookIds = new List<Guid>();
            Region = string.Empty;
            City = string.Empty;
            Address = string.Empty;
            OrderDate = DateTime.Now;
            Status = OrderStatus.RECEIVED;
        }
    }
}
