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
                var employerInstance = userManger.FindByNameAsync("employer").Result;
                var employeeInstance = userManger.FindByNameAsync("employee").Result;

                if (employeeInstance == null)
                {
                    employeeInstance = new AppUser
                    {
                        //FirstName = "John",
                        //LastName = "Smith",
                        Email = "john.smith@mail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+329813923",
                        PhoneNumberConfirmed = true,
                        UserName = "employee"
                    };
                    var result = userManger.CreateAsync(employeeInstance, app.Configuration["Passwords:EmployeePassword"]).Result;

                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);

                    if (!userManger.IsInRoleAsync(employeeInstance, employer.Name).Result)
                        _ = userManger.AddToRoleAsync(employeeInstance, employer.Name).Result;
                }

                if (employerInstance == null)
                {
                    employerInstance = new AppUser
                    {
                        Id = "041343ea-0f3d-458b-9fb6-7bd6700d69e8",
                        //FirstName = "Tom",
                        //LastName = "Smith",
                        Email = "tom.smith@mail.com",
                        EmailConfirmed = true,
                        PhoneNumber = "+329813923",
                        PhoneNumberConfirmed = true,
                        UserName = "employer"
                    };
                    var result = userManger.CreateAsync(employerInstance, app.Configuration["Passwords:EmployerPassword"]).Result;

                    if (!result.Succeeded)
                        throw new Exception(result.Errors.First().Description);

                    if (!userManger.IsInRoleAsync(employerInstance, employer.Name).Result)
                        _ = userManger.AddToRoleAsync(employerInstance, employer.Name).Result;
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
