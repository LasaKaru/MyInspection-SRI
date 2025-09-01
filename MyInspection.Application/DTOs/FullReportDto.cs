using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Application.DTOs
{
    public class FullReportDto
    {
        public Guid ReportId { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string SblOrderNumber { get; set; }
        public string ProductDescription { get; set; }
        public string OverallStatus { get; set; }
        public List<ReportQuantityItemDto> QuantityItems { get; set; } = new();
        public List<ReportDefectDto> Defects { get; set; } = new();
        // We'll add more properties here as we build out the update logic
    }

    public class ReportQuantityItemDto
    {
        public int Id { get; set; }
        public string StyleArticle { get; set; }
        public string PONumber { get; set; }
        public int OrderQuantity { get; set; }
        public int InspectedQtyPacked { get; set; }
        public int InspectedQtyNotPacked { get; set; }
        public int CartonsPacked { get; set; }
        public int CartonsNotPacked { get; set; }
    }

    public class ReportDefectDto
    {
        public int Id { get; set; }
        public string DefectDescription { get; set; }
        public int CriticalCount { get; set; }
        public int MajorCount { get; set; }
        public int MinorCount { get; set; }
    }
}
