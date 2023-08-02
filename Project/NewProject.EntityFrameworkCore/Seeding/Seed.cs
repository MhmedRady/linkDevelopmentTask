using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewProject.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NewProject.EntityFrameworkCore.Seeding
{
    public class DataInitialize
    {
        public static async Task Initialize(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                MainDbContext context = serviceScope.ServiceProvider.GetService<MainDbContext>();
                ILogger<DataInitialize>? logger = applicationBuilder.ApplicationServices.GetRequiredService<ILogger<DataInitialize>>();
                
                await DataMigration(context, logger);
             
                if (!await context.Roles.AnyAsync())
                {
                    string[] roles = new string[] { "Admin", "User" };

                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                    if (!context.Roles.Any())
                    {
                        foreach (string role in roles)
                        {
                            if (!context.Roles.Any(r => r.Name == role))
                            {
                                try
                                {
                                    await roleManager.CreateAsync(new IdentityRole(role));
                                }
                                catch (Exception ex)
                                {
                                    logger.LogError(ex, ex.Message);
                                }
                            }
                        }
                    }
                }

                if (!context.Users.Any())
                {
                    var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();
                    if (await userManager.FindByEmailAsync("Admin@asp.net") is null)
                    {
                        User admin = new User()
                        {
                            Id = Guid.NewGuid().ToString(),
                            UserName = "Admin",
                            Email = "Admin@asp.net",
                            PhoneNumber = "01234567890",
                        };
                        try
                        {
                            var userRes = await userManager.CreateAsync(admin, "password@123");
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex, $"An error occured During Add Person {admin.UserName}");
                        }
                    }
                }

                if (!context.JobTitles.Any())
                {
                    var jobTitles = new List<JobTitle>()
                    {
                        new JobTitle()
                        {
                            Name = "Senior Full Stack Web Developer (.NET/Angular)",
                            Description = "We are looking for a Senior .NET developer that will be able to develop and support existing applications. The main scope of work will include everything from back-end to client-side code, using optimal and efficient technologies, frameworks, and patterns. Your primary responsibilities will be to design and develop these applications. Therefore, it’s essential that you are skilled at problem solving, solution design, and high-quality coding.",
                            Responsibilities = "<ul><li>Participate in requirements analysis<br></li><li>Collaborate with internal teams to produce software design and architecture</li><li>Write clean, scalable code using .NET programming languages</li><li>Test and deploy applications and systems</li><li>Revise, update, refactor and debug code</li><li>Develop documentation throughout the software development life cycle</li><li>Serve as an expert on applications and provide technical support</li><li>Analyzing requirements and designing new functionality<br></li></ul>",
                            MaximumApplications = 10,
                            JobCategory = "software engineer",
                            CreatedAt = DateTime.Now,
                            ValidityDuration = new ValidityDuration()
                            {
                                From = DateTime.Now,
                                To = DateTime.Today.AddDays(10)
                            },
                            Skills = new []
                            {
                                new Skills()
                                {
                                    Name = ".NeT",
                                },
                                new Skills()
                                {
                                    Name = "Angular",
                                },
                                new Skills()
                                {
                                    Name = "SqlServer",
                                },
                                new Skills()
                                {
                                    Name = "Javascript",
                                },
                            }
                        },
                        new JobTitle()
                        {
                            Name = "Front-end Developer (Angular)",
                            Description = "We are looking for a Senior .NET developer that will be able to develop and support existing applications. The main scope of work will include everything from back-end to client-side code, using optimal and efficient technologies, frameworks, and patterns. Your primary responsibilities will be to design and develop these applications. Therefore, it’s essential that you are skilled at problem solving, solution design, and high-quality coding.",
                            Responsibilities = "<ul><li>Participate in requirements analysis<br></li><li>Collaborate with internal teams to produce software design and architecture</li><li>Write clean, scalable code using .NET programming languages</li><li>Test and deploy applications and systems</li><li>Revise, update, refactor and debug code</li><li>Develop documentation throughout the software development life cycle</li><li>Serve as an expert on applications and provide technical support</li><li>Analyzing requirements and designing new functionality<br></li></ul>",
                            MaximumApplications = 10,
                            JobCategory = "software engineer",
                            CreatedAt = DateTime.Now,
                            ValidityDuration = new ValidityDuration()
                            {
                                From = DateTime.Now,
                                To = DateTime.Today.AddDays(5)
                            },
                            Skills = new []
                            {
                                new Skills()
                                {
                                    Name = "Angular",
                                },
                                new Skills()
                                {
                                    Name = "JavaScript",
                                },
                            }
                        },
                        new JobTitle()
                        {
                            Name = "Flutter Developer (Flutter)",
                            Description = "We are looking for a Senior .NET developer that will be able to develop and support existing applications. The main scope of work will include everything from back-end to client-side code, using optimal and efficient technologies, frameworks, and patterns. Your primary responsibilities will be to design and develop these applications. Therefore, it’s essential that you are skilled at problem solving, solution design, and high-quality coding.",
                            Responsibilities = "<ul><li>Participate in requirements analysis<br></li><li>Collaborate with internal teams to produce software design and architecture</li><li>Write clean, scalable code using .NET programming languages</li><li>Test and deploy applications and systems</li><li>Revise, update, refactor and debug code</li><li>Develop documentation throughout the software development life cycle</li><li>Serve as an expert on applications and provide technical support</li><li>Analyzing requirements and designing new functionality<br></li></ul>",
                            MaximumApplications = 10,
                            JobCategory = "software engineer",
                            CreatedAt = DateTime.Now,
                            ValidityDuration = new ValidityDuration()
                            {
                                From = DateTime.Now,
                                To = DateTime.Today.AddDays(3)
                            },
                            Skills = new []
                            {
                                new Skills()
                                {
                                    Name = "Dart",
                                },
                                new Skills()
                                {
                                    Name = "Flutter",
                                },
                            }
                        },
                        new JobTitle()
                        {
                            Name = "Graphic Designer",
                            Description = "We are looking for a Senior .NET developer that will be able to develop and support existing applications. The main scope of work will include everything from back-end to client-side code, using optimal and efficient technologies, frameworks, and patterns. Your primary responsibilities will be to design and develop these applications. Therefore, it’s essential that you are skilled at problem solving, solution design, and high-quality coding.",
                            Responsibilities = "<ul><li>Participate in requirements analysis<br></li><li>Collaborate with internal teams to produce software design and architecture</li><li>Write clean, scalable code using .NET programming languages</li><li>Test and deploy applications and systems</li><li>Revise, update, refactor and debug code</li><li>Develop documentation throughout the software development life cycle</li><li>Serve as an expert on applications and provide technical support</li><li>Analyzing requirements and designing new functionality<br></li></ul>",
                            MaximumApplications = 10,
                            JobCategory = "software engineer",
                            CreatedAt = DateTime.Now,
                            ValidityDuration = new ValidityDuration()
                            {
                                From = DateTime.Now,
                                To = DateTime.Today.AddDays(7)
                            },
                            Skills = new []
                            {
                                new Skills()
                                {
                                    Name = "Photoshop",
                                },
                            }
                        },
                    };
                    await context.JobTitles.AddRangeAsync(jobTitles);
                    await context.SaveChangesAsync();
                }
                
                if (!context.UserApplies.Any(u=>u.Email == "Mohamed.Rady@gmail.com") && context.JobTitles.Any())
                {
                    UserApply user = new UserApply()
                    {
                        Id = Guid.NewGuid(),
                        Name = "MohamedRady",
                        Email = "Mohamed.Rady@gmail.com",
                        MobileNumber = "01069364670",
                        JobTitleId = context.JobTitles.First().Id
                    };
                    try
                    {
                        var userRes = await context.UserApplies.AddAsync(user);
                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, $"An error occured During Add Person {user.Name}");
                    }
                }
            }
        }
        public static async Task<bool> DataMigration(MainDbContext context, ILogger<DataInitialize> logger)
        {
            try
            {
                await context.Database.MigrateAsync();
                return true;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occured During Migration");
                return false;
            }
        }
    }
}
