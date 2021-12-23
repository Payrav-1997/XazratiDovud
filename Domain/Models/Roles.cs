using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class Roles : IdentityRole<int>
    {
        public Roles() { }
        public Roles (string name) : base(name) {}
    }
}
