using Authentication.Server.Datas.Entities;
using Authentication.Shared.Models;
using LibraryPassword;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Server.Context
{
    public static class ModelBuilderExtension
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {

            // IdentityRole
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Name = "viewer",
                    NormalizedName = "VIEWER"
                },
                new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "ADMIN"
                }
            );

            // Teszt role            
            List<UserRole> userRoles = new List<UserRole>
            {
                new UserRole
                {
                    Id = Guid.NewGuid(),
                    EnglishName = "admin",
                    Name = "adminisztrátor",
                },
                new UserRole
                {
                    Id = Guid.NewGuid(),
                    EnglishName = "viewer",
                    Name = "látogató"
                }
            };

            modelBuilder.Entity<UserRole>().HasData(userRoles);

            // Passwords
            PasswordManager pm = new();
            string testAdminHashPassword = pm.HashPasword("Test@123");
            string testViewerHashPassword = pm.HashPasword("Test@123");


            // Test user
            Guid testAdminRoleId = userRoles.Where(role => role.EnglishName == "admin").Select(role => role.Id).First();
            Guid testViewerRoleId=userRoles.Where(role => role.EnglishName == "viewer").Select(role => role.Id).First();

            Guid testAdminId = Guid.NewGuid();
            Guid testViewerId = Guid.NewGuid();

            List<User> users = new List<User>
            {
                new User
                {
                    Id = testAdminId,
                    LastName = "Teszt",
                    FirstName = "Admin",
                    Email = "admin@teszt.hu",
                    UserRoleId = testAdminRoleId
                },

                new User
                {
                    Id = testViewerId,
                    LastName = "Teszt",
                    FirstName = "Elek",
                    Email = "teszt@teszt.hu",
                    UserRoleId = testViewerRoleId
                }
            };

            modelBuilder.Entity<User>().HasData(users);

            // Teszt user password
            List<UserIdentification> userIdentifications = new List<UserIdentification>
            {
                new UserIdentification
                {
                    Id=testAdminId,
                    Password=testAdminHashPassword,
                    EmailVerified = true,
                },
                new UserIdentification
                {
                    Id=testViewerId,
                    Password = testViewerHashPassword,
                    EmailVerified = true,
                }
            };

            modelBuilder.Entity<UserIdentification>().HasData(userIdentifications);

        }
    }
}
