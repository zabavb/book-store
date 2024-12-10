using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UserAPI.Models
{
    public class Password
    {
        [Key]
        public Guid Id { get; set; }
        [Required, MaxLength(30), DataType(DataType.Text)]
        public string PasswordHash { get; set; }
        [Required, MaxLength(4), DataType(DataType.Text)]
        public string PasswordSalt { get; set; }
        [Required]
        public Guid UserId { get; set; }
        [NotMapped]
        public UserDTO? User { get; set; } = new();

        public Password()
        {
            PasswordHash = string.Empty;
            PasswordSalt = string.Empty;
        }
    }
}
