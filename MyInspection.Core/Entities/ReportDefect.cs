using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class ReportDefect
    {
        public int DefectID { get; set; }
        public string DefectDescription { get; set; } = string.Empty;
        public int CriticalCount { get; set; }
        public int MajorCount { get; set; }
        public int MinorCount { get; set; }

        // Foreign key
        public Guid ReportID { get; set; }
        public InspectionReport InspectionReport { get; set; } = null!;
    }
}
