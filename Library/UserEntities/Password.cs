using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UserEntities
{
    public class Password
    {
        public Guid? PasswordId { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public Guid UserId { get; set; }

        public Password()
        {
            PasswordHash = string.Empty;
            PasswordSalt = string.Empty;
        }
        public Password(Guid? passwordId, string passwordHash, string passwordSalt, Guid userId)
        {
            PasswordId = passwordId;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            UserId = userId;
        }
    }
}
