using Abp.Authorization;
using ContentCMS.Authorization.Roles;
using ContentCMS.Authorization.Users;

namespace ContentCMS.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {
        }
    }
}
