using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Domain;

namespace NewProject.Application;

public class UserDto 
{
    public string Id { get; set; }
    public string? UserName { get; set; }
    public string FullName { get; set; }
    
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public List<string> RolesName { get; set; }
    
    public bool is_active { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string _CreatedAt
    {
        get => CreatedAt.ToString(@"yyyy-MM-dd  hh:mm tt");
    }
}
