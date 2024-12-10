using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.UserEntities
{
    public enum RoleType
    {
        [Display(Name = "admin")]
        ADMIN,
        [Display(Name = "moderator")]
        MODERATOR,
        [Display(Name = "user")]
        USER,
        [Display(Name = "guest")]
        GUEST
    }
}
