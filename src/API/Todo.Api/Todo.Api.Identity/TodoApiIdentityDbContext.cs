using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Todo.Api.Identity.Models;

namespace Todo.Api.Identity
{
    public class TodoApiIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public TodoApiIdentityDbContext(DbContextOptions<TodoApiIdentityDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            string USER1_ID = "02174cf0-9412–4cfe-afbf-59f706d72cf6";
            string USER2_ID = "11111cf0-9412–4cfe-afbf-59f706d72cf7";
            string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

            //seed admin role
            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Name = "USER",
                NormalizedName = "USER",
                Id = ROLE_ID,
                ConcurrencyStamp = ROLE_ID
            });

            //create users
            var appUsers = new List<ApplicationUser> {
                new ApplicationUser{
                    Id = USER1_ID,
                    Email = "markoprojovic@gmail.com",
                    NormalizedEmail = "MARKOPROJOVIC@GMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = "Marko",
                    LastName = "Projovic",
                    UserName = "MarkoProjovic",
                    NormalizedUserName = "MARKOPROJOVIC"
                },
                 new ApplicationUser{
                    Id = USER2_ID,
                    Email = "markoprojovic+1@gmail.com",
                    NormalizedEmail = "MARKOPROJOVIC+1@GMAIL.COM",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    FirstName = "Petar",
                    LastName = "Petrovic",
                    UserName = "PetarPetrovic",
                    NormalizedUserName = "PETARPETROVIC"
                }
            };


            foreach (var user in appUsers)
            {
                //set users password
                PasswordHasher<ApplicationUser> ph = new PasswordHasher<ApplicationUser>();
                user.PasswordHash = ph.HashPassword(user, "Password1");
            }

            //seed user
            builder.Entity<ApplicationUser>().HasData(appUsers);

            var userRoles = new List<IdentityUserRole<string>>()
            {
                new IdentityUserRole<string>
                {
                    RoleId = ROLE_ID,
                    UserId = USER1_ID
                },
               new IdentityUserRole<string>
               {
                   RoleId = ROLE_ID,
                   UserId = USER2_ID
               }
            };

            //set users role to user
            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }
}