using Library.BookEntities;
using Library.OrderEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UserEntities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public RoleType Role { get; set; }
        public Guid PasswordId { get; set; }
        public Password Password { get; set; }
        public Guid? SubscriptionId { get; set; }
        public Subscription? Subscription { get; set; }
        
        // ========================= Basket ================================
        
        public ICollection<Guid>? OrderIds { get; set; }
        public ICollection<Order>? Orders { get; set; }

        public User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.Now;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Role = RoleType.GUEST;
            Password = new();
            Subscription = new();
            OrderIds = new HashSet<Guid>();
            Orders = new HashSet<Order>();
        }
        public User(
            Guid userId, string firstName, string? lastName, DateTime dateOfBirth,
            string email, string phoneNumber, RoleType role
        ) {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
            Password = new();
            Subscription = new();
            OrderIds = new HashSet<Guid>();
            Orders = new HashSet<Order>();
        }

        public User(
            Guid userId, string firstName, string? lastName, DateTime dateOfBirth, string email,
            string phoneNumber, RoleType role, Guid passwordId, Password password, Guid? subscriptionId,
            Subscription? subscription, ICollection<Guid>? orderIds, ICollection<Order>? orders
        )
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
            PasswordId = passwordId;
            Password = password;
            SubscriptionId = subscriptionId;
            Subscription = subscription;
            OrderIds = orderIds;
            Orders = orders;
        }
    }
}
