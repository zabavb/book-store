using System.ComponentModel.DataAnnotations;

namespace Library.OrderEntities
{
    public enum DeliveryType
    {
        [Display(Name = "Libro")]
        LIBRO,

        [Display(Name = "Nova Post")]
        NOVA_POST,

        [Display(Name = "UKR post")]
        UKR_POST
    }
}