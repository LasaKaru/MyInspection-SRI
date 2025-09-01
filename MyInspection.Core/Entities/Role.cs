using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace MyInspection.Core.Entities
{
    // Extending IdentityRole gives us Id and Name
    public class Role : IdentityRole<int> // Using 'int' for the primary key
    {
    }
}
