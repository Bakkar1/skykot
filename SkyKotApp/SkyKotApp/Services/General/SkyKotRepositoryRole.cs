using KotClassLibrary.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace SkyKotApp.Services.General
{
    public partial class SkyKotPartialRepository
    {
        public SelectList GetRoles()
        {
            return new SelectList(roleManager.Roles, "Id", "Name");
        }
        public string GetRoleName(string UserId)
        {
            var identityUserRole = context.UserRoles.Where(us => us.UserId == UserId).FirstOrDefault();

            return identityUserRole == null ? "" : identityUserRole.RoleId;
        }
        public void DeleteOldRole(string UserId)
        {
            var userRole = context.UserRoles.Where(us => us.UserId == UserId).FirstOrDefault();
            if (userRole != null)
            {
                context.UserRoles.Remove(userRole);
                context.SaveChanges();
            }

        }
        public async Task UpdateRole(CustomUser identityUser, string roleId)
        {
            DeleteOldRole(identityUser.Id);

            var role = await roleManager.FindByIdAsync(roleId);
            if (role != null)
            {
                await userManager.AddToRoleAsync(identityUser, role.Name);
            }
        }
    }
}
