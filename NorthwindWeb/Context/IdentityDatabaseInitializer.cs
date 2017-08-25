﻿using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using NorthwindWeb.Models;

namespace NorthwindWeb.Context
{

    /// <summary>
    /// Initializa with Users and Roles.
    /// </summary>
    public class IdentityDatabaseInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {

        /// <summary>
        /// Seed database ; fill tables.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ApplicationDbContext context)
        {

            CreateRolesandUsers(context);

            base.Seed(context);
        }

        private void CreateRolesandUsers(ApplicationDbContext context)
        {

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


                // In Startup iam creating first Admin Role and creating a default Admin User    
                if (!roleManager.RoleExists("Admins"))
                {

                    // first we create Admin rool   
                    var role = new IdentityRole();
                    role.Name = "Admins";
                    roleManager.Create(role);

                    //Here we create a Admin super user who will maintain the website                  

                    var user = new ApplicationUser();
                    user.UserName = "admin";
                    user.Email = "admin@gmail.com";
                    AddUser(userManager, user, "123456");

                    //-create a user for tests
                    var testUser = new ApplicationUser();
                    testUser.UserName = "tester";
                    testUser.Email = "Tester_1@gmail.com";
                    AddUser(userManager, testUser, "Tester_1");
                }

                // creating Creating Manager role    
                if (!roleManager.RoleExists("Managers"))
                {
                    var role = new IdentityRole();
                    role.Name = "Managers";
                    roleManager.Create(role);

                }

                // creating Creating Employee role    
                if (!roleManager.RoleExists("Employees"))
                {
                    var role = new IdentityRole();
                    role.Name = "Employees";
                    roleManager.Create(role);

                }
            
        }

        private static void AddUser(UserManager<ApplicationUser> userManager, ApplicationUser user, string userPWD)
        {
            var chkUser = userManager.Create(user, userPWD);

            //Add default User to Role Admin   
            if (chkUser.Succeeded)
            {
                var resultForAdd = userManager.AddToRole(user.Id, "Admins");

            }
        }
    }
}