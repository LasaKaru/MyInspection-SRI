using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class ReportOverride
    {
        [Key]
        public int OverrideID { get; set; }
        public string OriginalStatus { get; set; } = string.Empty;
        public string NewStatus { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
        public DateTime OverrideTimestamp { get; set; } = DateTime.UtcNow;

        // Foreign keys
        public Guid ReportID { get; set; }
        public int OverriddenByUserID { get; set; }

        // Navigation properties
        public InspectionReport InspectionReport { get; set; } = null!;
        public User OverriddenByUser { get; set; } = null!;
    }
}
