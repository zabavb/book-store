using System.ComponentModel.DataAnnotations;


namespace Library.OrderEntities
{
    public enum OrderStatus
    {
        [Display(Name = "Order Received")]
        RECEIVED,

        [Display(Name = "Processing")]
        PROCESSING,

        [Display(Name = "Awaiting Payment")]
        PAYMENT,

        [Display(Name = "In Transit")]
        TRANSIT,

        [Display(Name = "Delivered")]
        DELIVERED,
    }
}
