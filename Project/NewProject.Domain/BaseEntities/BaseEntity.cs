using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewProject.Domain.BaseEntities;

public class BaseEntity<TKey> : Entity<TKey>
{
    [Comment("Created At DateTime")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}
