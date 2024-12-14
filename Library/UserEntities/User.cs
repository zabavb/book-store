using Library.BookEntities;
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
        public Guid? SubscriptionId { get; set; }
        public ICollection<Guid>? OrderIds { get; private set; }

        public User()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            DateOfBirth = DateTime.Now;
            Email = string.Empty;
            PhoneNumber = string.Empty;
            Role = RoleType.GUEST;
            OrderIds = new HashSet<Guid>();
        }
        public User(
            Guid userId, string firstName, string? lastName, DateTime dateOfBirth, string email,
            string phoneNumber, RoleType role, Guid? subscriptionId, ICollection<Guid>? orderIds
        ) {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Email = email;
            PhoneNumber = phoneNumber;
            Role = role;
            SubscriptionId = subscriptionId;
            OrderIds = orderIds;
        }
    }
}
