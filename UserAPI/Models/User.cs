using Library.UserEntities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(50), DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required, MaxLength(50), DataType(DataType.Text)]
        public string LastName { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(15), Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public RoleType Role { get; set; }
        public Guid SubscriptionId { get; set; }
        public DateTime SubscriptionEndDate { get; set; }
        public Subscription? Subscription { get; set; }
        public ICollection<OrderDTO> Orders { get; private set; } = new HashSet<OrderDTO>();
        public ICollection<Guid> OrderIds { get; private set; } = new HashSet<Guid>();

        [NotMapped]
        public ICollection<BookDTO> Basket { get; private set; } = new HashSet<BookDTO>();

        public User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            DateOfBirth = DateTime.Now;
            Role = RoleType.GUEST;
        }
    }
}
