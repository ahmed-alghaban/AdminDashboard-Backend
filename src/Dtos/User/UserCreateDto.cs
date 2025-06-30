using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace AdminDashboard.src.Dtos.User
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "First name is required")]
        public required string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is required")]
        public required string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number")]
        public required string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Role is required")]
        public Guid RoleId { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character")]
        public required string PasswordHash { get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}