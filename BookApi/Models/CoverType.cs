using System.ComponentModel.DataAnnotations;

namespace BookApi.Models
{
    public enum CoverType
    {
        [Display(Name = "М'яка")] SOFT_COVER,
        [Display(Name = "Тверда")] HARDCOVER,
        [Display(Name = "На спіралі")] RING_BINDING,
        [Display(Name = "Шкіряна")] LEATHER,
        [Display(Name = "Суперобкладинка")] DUST_JACKET
    }
}
