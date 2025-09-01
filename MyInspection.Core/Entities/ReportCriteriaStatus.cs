using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class ReportCriteriaStatus
    {
        [Key]
        public int ReportCriteriaID { get; set; }
        public string Status { get; set; } = "Pending";
        public string? Comments { get; set; }

        // Foreign keys
        public Guid ReportID { get; set; }
        public int CriteriaID { get; set; }

        // Navigation properties
        public InspectionReport InspectionReport { get; set; } = null!;
        public MasterCriteria MasterCriteria { get; set; } = null!;
    }
}
