namespace OrderApi.Models
{
    public class DeliveryType
    {
        public Guid DeliveryId { get; set; }
        public string ServiceName { get; set; }

        public DeliveryType()
        {
            ServiceName = string.Empty;
        }
    }
}
