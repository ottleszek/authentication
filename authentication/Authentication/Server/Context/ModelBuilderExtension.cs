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

            Guid testAdminId = new Guid("26de36b7-76f5-4f17-8f9d-44eb429f151b");
            Guid testViewerId = new Guid("a25f5bad-f18a-4640-978b-0103709338d5");
            Guid testUser1= new Guid("16165c01-2233-4cf4-a26a-05c954c1c7eb");
            Guid testUser2 = new Guid("ed1d0da9-402f-47d9-a7f1-2acbc734804f");
            Guid testUser3 = new Guid("25123c14-562d-48fe-bd37-9b9c35ee8902");
            Guid testUser4 = new Guid("52242f76-2cd6-4892-bb62-4f8db50e9669");
            Guid testUser5 = new Guid("3bb60287-4615-4aef-bbda-94d867d38445");

            List<User> users = new List<User>
            {
                new User
                {
                    Id = testAdminId,
                    LastName = "Teszt",
                    FirstName = "Admin",
                    Email = "admin@teszt.hu",
                    UserRoleId = testAdminRoleId,
                    IsRegisteredUser=false,
                    ProfilImageTimeStamp="1705944872"
                },
                new User
                {
                    Id = testViewerId,
                    LastName = "Teszt",
                    FirstName = "Elek",
                    Email = "teszt@teszt.hu",
                    UserRoleId = testViewerRoleId,
                    IsRegisteredUser=false
                },
                new User
                {
                    Id = testUser1,
                    LastName = "User1",
                    FirstName = "User1",
                    Email = "user1@teszt.hu",
                    UserRoleId = testViewerRoleId,
                    IsRegisteredUser=true
                },
                new User
                {
                    Id = testUser2,
                    LastName = "User2",
                    FirstName = "User2",
                    Email = "user2@teszt.hu",
                    UserRoleId = testViewerRoleId,
                    IsRegisteredUser=true
                },
                new User
                {
                    Id = testUser3,
                    LastName = "User3",
                    FirstName = "User3",
                    Email = "user3@teszt.hu",
                    UserRoleId = testViewerRoleId,
                    IsRegisteredUser=true
                },
                new User
                {
                    Id = testUser4,
                    LastName = "User4",
                    FirstName = "User4",
                    Email = "user4@teszt.hu",
                    UserRoleId = testViewerRoleId,
                    IsRegisteredUser=true
                },
                new User
                {
                    Id = testUser5,
                    LastName = "User5",
                    FirstName = "User5",
                    Email = "user5@teszt.hu",
                    UserRoleId = testViewerRoleId,
                    IsRegisteredUser=true
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
