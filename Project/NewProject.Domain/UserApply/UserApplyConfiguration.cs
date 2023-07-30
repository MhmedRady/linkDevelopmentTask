using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain
{
    public class UserApplyConfiguration : IEntityTypeConfiguration<UserApply>
    {
        public void Configure(EntityTypeBuilder<UserApply> builder)
        {
            builder.HasOne(skills => skills.JobTitle).WithMany(JT => JT.UserApplies).HasForeignKey("JobTitleId");
        }
    }
}
