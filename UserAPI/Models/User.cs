using Library.UserEntities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models
{
    public class User
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public RoleType Role { get; set; }
        
        public SubscriptionDTO? Subscription { get; private set; }
        public ICollection<OrderDTO>? Orders { get; private set; }
        public ICollection<BookDTO>? Basket { get; private set; }

        public User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.Now;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Role = RoleType.GUEST;

            Subscription = new SubscriptionDTO();
            Orders = new HashSet<OrderDTO>();
            Basket = new HashSet<BookDTO>();
        }
        public User(
            Guid? id, string firstName, string? lastName, DateTime dateOfBirth, string email,
            string phoneNumber, RoleType role, SubscriptionDTO? subscription,
            ICollection<OrderDTO>? orders, ICollection<BookDTO>? basket
            ) {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;

            Subscription = subscription;
            Orders = orders;
            Basket = basket;
        }
    }
}
