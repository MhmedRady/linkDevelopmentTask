using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NewProject.Domain;
using NuGet.Protocol;


namespace NewProject.EntityFrameworkCore;

public class MainDbContext : IdentityDbContext<User, IdentityRole,string>
{
    public DbSet<JobTitle> JobTitles { get; set; }
    public DbSet<Skills> Skills { get; set; }
    public DbSet<ValidityDuration> ValidityDurations { get; set; }
    public DbSet<UserApply> UserApplies { get; set; }
    
    public MainDbContext(DbContextOptions options) : base(options){}
    protected override void OnModelCreating(ModelBuilder builder)
    {
        //Entities Configuration
        new UserConfiguration().Configure(builder.Entity<User>());
        new JobTitleConfiguration().Configure(builder.Entity<JobTitle>());
        new SkilesConfiguration().Configure(builder.Entity<Skills>());
        new UserApplyConfiguration().Configure(builder.Entity<UserApply>());
        new ValidityDurationConfiguration().Configure(builder.Entity<ValidityDuration>());
        
        base.OnModelCreating(builder);
    }
}
