using System.ComponentModel.DataAnnotations;


namespace Library.OrderEntities
{
    public enum OrderStatus
    {
        RECEIVED,
        PROCESSING,
        PAYMENT,
        TRANSIT,
        DELIVERED,
    }
}
