using Abp.Authorization;
using BookingWeb.Authorization.Roles;
using BookingWeb.Authorization.Users;

namespace BookingWeb.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
