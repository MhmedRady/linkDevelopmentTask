using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewProject.Domain;

namespace NewProject.Application;

public class CreateUserInput 
{
    public string FullName { get; set; }
    public string? PasswordHash { get; set; }
    public string? ConfirmPassword { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public Guid CourseId { get; set; }
    public bool is_active { get; set; } = true;
    
    
    //public Guid? DepartmentId { get; set; }
    //public string RoleName { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
