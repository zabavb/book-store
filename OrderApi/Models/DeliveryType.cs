using System.ComponentModel.DataAnnotations;

namespace Library.OrderEntities
{
    internal enum DeliveryType
    {
        [Display(Name = "Bookoria")]
        BOOKORIA,

        [Display(Name = "Nova Post")]
        NOVA_POST,

        [Display(Name = "UKR post")]
        UKR_POST
    }
}