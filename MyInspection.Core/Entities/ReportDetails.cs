using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class ReportDetails
    {
        [Key]
        public int ReportDetailID { get; set; }
        public string? ProductDescription { get; set; }
        public string? StyleNumber { get; set; }
        public int? TotalQuantity { get; set; }
        public string? InspectionLocation { get; set; }
        public string? LCNumber { get; set; }

        // Foreign key back to the main report
        public Guid ReportID { get; set; }
        public InspectionReport InspectionReport { get; set; } = null!;
    }
}
