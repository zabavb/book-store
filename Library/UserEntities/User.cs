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
        [Key]
        public Guid UserId { get; set; }
        [Required, MaxLength(50), DataType(DataType.Text)]
        public string FirstName { get; set; }
        [MaxLength(50), DataType(DataType.Text)]
        public string? LastName { get; set; }
        [Required, DataType(DataType.DateTime)]
        public DateTime DateOfBirth { get; set; }
        [Required, MaxLength(100), EmailAddress]
        public string Email { get; set; }
        [Required, MaxLength(15), Phone]
        public string PhoneNumber { get; set; }
        [Required]
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
