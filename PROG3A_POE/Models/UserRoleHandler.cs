using Microsoft.AspNetCore.Authorization;

namespace PROG3A_POE.Models
{
    public class UserRoleHandler : AuthorizeAttribute
    {
        public UserRoleHandler(params string[] roles)
        {
            Roles = string.Join(",", roles);
        }
    }
}
