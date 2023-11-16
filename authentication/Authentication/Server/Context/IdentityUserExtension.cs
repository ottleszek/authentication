using Microsoft.AspNetCore.Identity;

namespace Authentication.Server.Context
{
    public static class IdentityUserExtension
    {
        public static async void AddIdentityUserTestData(this UserManager<IdentityUser> userManager)
        {

            string email = "admin@teszt.hu";
            if (await userManager.FindByEmailAsync(email) is null)
            {
                var user = new IdentityUser();
                user.UserName = email;
                user.Email = email;

                await userManager.CreateAsync(user);
                await userManager.AddToRoleAsync(user, "admin");
            }

            email = "teszt@teszt.hu";
            if (await userManager.FindByEmailAsync(email) is null)
            {
                var user = new IdentityUser();
                user.UserName = email;
                user.Email = email;

                await userManager.CreateAsync(user);
                await userManager.AddToRoleAsync(user, "viewer");
            }

        }
    }
}
