﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain
{
    public class ValidityDurationConfiguration : IEntityTypeConfiguration<ValidityDuration>
    {
        public void Configure(EntityTypeBuilder<ValidityDuration> builder)
        {
            builder.HasOne(duration => duration.JobTitle).WithOne(JT => JT.ValidityDuration);
        }
    }
}
