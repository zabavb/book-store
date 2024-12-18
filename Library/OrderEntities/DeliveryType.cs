using System.ComponentModel.DataAnnotations;

namespace Library.OrderEntities
{
    public class DeliveryType
    {
        [Required]
        [Key]
        public Guid id {  get; set; }

        [Required(AllowEmptyStrings = false)]
        [StringLength(50, ErrorMessage = "Service name should be less than 50 characters.")]
        public string ServiceName { get; set; }

        DeliveryType() 
        { 
            ServiceName = string.Empty;
        }
    }
}