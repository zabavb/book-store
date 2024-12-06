namespace Library.OrderEntities
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> BookIds { get; set; } = new List<Guid>();

        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public float Price { get; set; }
        public DeliveryType Delivery { get; set; }
        public float DeliveryPrice { get; set; }
        public DateTime DeliveryDate { get; set; } = DateTime.Now;
        public DateTime DeliveryTime { get; set; }  // + 2 days, in case of Nova Post

    }
}