using JobBoard.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace JobBoard.Identity.Data
{
    public static class Administration
    {
        public static void EnsureSystemAdministratorExist(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManger = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();

                var systemAdministrator = roleManager.FindByIdAsync("SystemAdministrator").Result;
                if (systemAdministrator == null)
                {
                    systemAdministrator = new IdentityRole() { Name = "SystemAdministrator" };
                    _ = roleManager.CreateAsync(systemAdministrator).Result;
                }

                var employer = roleManager.FindByIdAsync("Employer").Result;
                if (employer == null)
                {
                    employer = new IdentityRole() { Name = "Employer" };
                    _ = roleManager.CreateAsync(employer).Result;
                }

                var employee = roleManager.FindByIdAsync("Employee").Result;
                if (employee == null)
                {
                    employee = new IdentityRole() { Name = "Employee" };
                    _ = roleManager.CreateAsync(employee).Result;
                }

                var systemAdministratorInstance = userManger.FindByNameAsync("system_administrator").Result;
                var employerInstance1 = userManger.FindByNameAsync("employer").Result;
                var employeeInstance = userManger.FindByNameAsync("employee").Result;

                if (employeeInstance == null)
                {
                    employeeInstance = new AppUser
                    {
                        Id = "0c207243-5fb9-4a2d-9581-cab3e01b2609",
                        Email = "john.smith@mail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+329813923",
                        PhoneNumberConfirmed = true,
                        UserName = "employer"
                    };
                    var result = userManger.CreateAsync(employeeInstance, app.Configuration["Passwords:EmployeePassword"]).Result;

                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);

                    if (!userManger.IsInRoleAsync(employeeInstance, employer.Name).Result)
                        _ = userManger.AddToRoleAsync(employeeInstance, employer.Name).Result;
                }

                if (employerInstance1 == null)
                {
                    employerInstance1 = new AppUser
                    {
                        Id = "041343ea-0f3d-458b-9fb6-7bd6700d69e8",
                        Email = "tom.smith@mail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+329813923",
                        PhoneNumberConfirmed = true,
                        UserName = "employee"
                    };

                    var employerInstance2 = new AppUser
                    {
                        Id = "010c4d66-7268-44a9-a991-e6d5aadea719",
                        Email = "mark.smith@mail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+329813923",
                        PhoneNumberConfirmed = true,
                        UserName = "employer1"
                    };

                    var employerInstance3 = new AppUser
                    {
                        Id = "ad4d1381-49a1-460e-aee8-808a5b8ed2da",
                        Email = "john.smith@mail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+329813923",
                        PhoneNumberConfirmed = true,
                        UserName = "employer2"
                    };

                    var employerInstance4 = new AppUser
                    {
                        Id = "cc653465-e4a0-40fa-99dc-061841fbf76f",
                        Email = "alice.smith@mail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+329813923",
                        PhoneNumberConfirmed = true,
                        UserName = "employer3"
                    };

                    var result = userManger.CreateAsync(employerInstance1, app.Configuration["Passwords:EmployerPassword"]).Result;

                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);

                    if (!userManger.IsInRoleAsync(employerInstance1, employer.Name).Result)
                        _ = userManger.AddToRoleAsync(employerInstance1, employer.Name).Result;
                }

                if (systemAdministratorInstance == null)
                {
                    systemAdministratorInstance = new AppUser
                    {
                        //FirstName = "System",
                        //LastName = "Administrator",
                        Email = "admin@mail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+12029182132",
                        PhoneNumberConfirmed = true,
                        UserName = "system_administrator",
                    };
                    var result = userManger.CreateAsync(systemAdministratorInstance, app.Configuration["Passwords:SystemAdministratorPassword"]).Result;
                    
                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);

                    if (!userManger.IsInRoleAsync(systemAdministratorInstance, systemAdministrator.Name).Result)
                        _ = userManger.AddToRoleAsync(systemAdministratorInstance, systemAdministrator.Name).Result;
                }
            }
        }
    }
}
