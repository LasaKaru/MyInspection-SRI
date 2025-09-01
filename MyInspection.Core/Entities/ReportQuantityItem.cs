using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class ReportQuantityItem
    {
        public int ReportQuantityItemID { get; set; }
        public string StyleArticle { get; set; } = string.Empty;
        public string? PONumber { get; set; }
        public int OrderQuantity { get; set; }
        public int InspectedQtyPacked { get; set; }
        public int InspectedQtyNotPacked { get; set; }
        public int CartonsPacked { get; set; }
        public int CartonsNotPacked { get; set; }

        // Foreign key
        public Guid ReportID { get; set; }
        public InspectionReport InspectionReport { get; set; } = null!;
    }
}
