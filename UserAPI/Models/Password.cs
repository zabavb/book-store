using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models
{
    public class Password
    {
        public Guid? Id { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public UserDTO User { get; set; }

        public Password()
        {
            PasswordHash = string.Empty;
            PasswordSalt = string.Empty;
            User = new();
        }
        public Password(Guid? id, string passwordHash, string passwordSalt, UserDTO user)
        {
            Id = id;
            PasswordHash = passwordHash;
            PasswordSalt = passwordSalt;
            User = user;
        }
    }
}
