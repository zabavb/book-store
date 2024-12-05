using System.ComponentModel.DataAnnotations;

namespace BookApi.Models
{
    public enum Language
    {
        [Display(Name = "Англійська")] ENGLISH,
        [Display(Name = "Українська")] UKRAINIAN,
        [Display(Name = "Іспанська")] SPANISH,
        [Display(Name = "Французька")] FRENCH,
        [Display(Name = "Німецька")] GERMAN,
        [Display(Name = "Інша")] OTHER
    }
}
