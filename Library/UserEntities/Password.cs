using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UserEntities
{
    public class Password
    {
        public Guid PasswordId { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public Password()
        {
            PasswordHash = string.Empty;
            PasswordSalt = string.Empty;
            User = new();
        }
        public Password(Guid passwordId, string passwordHash, string passwordSalt, Guid userId, User user)
        {
            PasswordId = passwordId;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            UserId = userId;
            User = user;
        }
    }
}
