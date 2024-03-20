using Microsoft.AspNetCore.Identity;

namespace Application.DTOs
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
