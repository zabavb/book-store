using Library.BookEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UserEntities
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public RoleType Role { get; set; } = RoleType.GUEST;
        public Guid SubscriptionId { get; set; }
        public DateTime SubscriptionEndDate { get; set; }

        public List<Book> Basket { get; set; } = new();
        public List<Guid> OrderIds { get; set; } = new();
    }
}
