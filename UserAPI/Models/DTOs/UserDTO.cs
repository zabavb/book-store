using Library.UserEntities;

namespace UserAPI.Models.DTOs
{
    public class UserDTO
    {
        public Guid? Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public RoleType Role { get; set; }

        public UserDTO()
        {
            FullName = string.Empty;
            Email = string.Empty;
            Role = RoleType.GUEST;
        }
        public UserDTO(Guid? id, string fullName, string? lastName, string email, RoleType role)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            Role = role;
        }
    }
}
