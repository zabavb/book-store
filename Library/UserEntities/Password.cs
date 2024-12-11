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
        [Key]
        public Guid PasswordId { get; set; }
        [Required, MaxLength(30), DataType(DataType.Text)]
        public string PasswordHash { get; set; }
        [Required, MaxLength(4), DataType(DataType.Text)]
        public string PasswordSalt { get; set; }
        [Required]
        public Guid UserId { get; set; }

        public Password()
        {
            PasswordHash = string.Empty;
            PasswordSalt = string.Empty;
        }
        public Password(Guid passwordId, string passwordHash, string passwordSalt, Guid userId)
        {
            PasswordId = passwordId;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            UserId = userId;
        }
    }
}
