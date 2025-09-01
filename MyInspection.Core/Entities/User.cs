using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyInspection.Core.Entities
{
    // Extending IdentityUser gives us Id, UserName, Email, PasswordHash, etc.
    public class User : IdentityUser<int> // Using 'int' for the primary key
    {
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
