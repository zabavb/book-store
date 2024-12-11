using Library.UserEntities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models
{
    public class User
    {
        [Key]
        public Guid? Id { get; set; }
        [Required, MaxLength(50), DataType(DataType.Text)]
        public string FirstName { get; set; }
        [Required, MaxLength(50), DataType(DataType.Text)]
        public string? LastName { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(15), Phone]
        public string PhoneNumber { get; set; }
        [Required]
        public RoleType Role { get; set; }
        public Guid? SubscriptionId { get; private set; }
        public SubscriptionDTO? Subscription { get; private set; }
        
        public ICollection<OrderDTO> Orders { get; private set; }
        public ICollection<Guid> OrderIds { get; private set; }

        [NotMapped]
        public ICollection<BookDTO> Basket { get; private set; }

        public User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.Now;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Role = RoleType.GUEST;

            Subscription = new();
            
            Orders = new HashSet<OrderDTO>();
            OrderIds = new HashSet<Guid>();
            
            Basket = new HashSet<BookDTO>();
        }
        public User(
            Guid? id, string firstName, string? lastName, DateTime dateOfBirth, string email,
            string phoneNumber, RoleType role, Guid? subscriptionId, SubscriptionDTO? subscription,
            ICollection<OrderDTO> orders, ICollection<Guid> orderIds, ICollection<BookDTO> basket
            ) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;

            SubscriptionId = subscriptionId;
            Subscription = subscription;
            
            OrderIds = orderIds;
            Orders = orders;

            Basket = basket;
        }
    }
}
