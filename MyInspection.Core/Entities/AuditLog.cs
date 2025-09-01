using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class AuditLog
    {
        public long LogID { get; set; }
        public string ActivityType { get; set; } = string.Empty;
        public string LogDetails { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Foreign key (nullable, as a user could be deleted)
        public int? UserID { get; set; }
        public User? User { get; set; }
    }
}
