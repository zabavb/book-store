using System.ComponentModel.DataAnnotations;


namespace Library.OrderEntities
{
    public enum OrderStatus
    {
        PENDING,
        PROCESSING,
        PAYMENT,
        TRANSIT,
        COMPLETED,
    }
}
