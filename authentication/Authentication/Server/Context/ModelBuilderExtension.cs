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
            Guid testUser1= Guid.NewGuid();
            Guid testUser2 = Guid.NewGuid();
            Guid testUser3 = Guid.NewGuid();
            Guid testUser4 = Guid.NewGuid();
            Guid testUser5 = Guid.NewGuid();

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
                },
                new User
                {
                    Id = testUser1,
                    LastName = "User1",
                    FirstName = "User1",
                    Email = "user1@teszt.hu",
                    UserRoleId = testViewerRoleId
                },
                new User
                {
                    Id = testUser2,
                    LastName = "User2",
                    FirstName = "User2",
                    Email = "user2@teszt.hu",
                    UserRoleId = testViewerRoleId
                },
                new User
                {
                    Id = testUser3,
                    LastName = "User3",
                    FirstName = "User3",
                    Email = "user3@teszt.hu",
                    UserRoleId = testViewerRoleId
                },
                new User
                {
                    Id = testUser4,
                    LastName = "User4",
                    FirstName = "User4",
                    Email = "user4@teszt.hu",
                    UserRoleId = testViewerRoleId
                },
                new User
                {
                    Id = testUser5,
                    LastName = "User5",
                    FirstName = "User5",
                    Email = "user5@teszt.hu",
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
