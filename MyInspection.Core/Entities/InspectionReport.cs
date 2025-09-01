using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class InspectionReport
    {
        [Key]
        public Guid ReportID { get; set; }
        public string PurchaseOrderNumber { get; set; } = string.Empty;
        public string? SBLOrderNumber { get; set; }
        public string? SupplierName { get; set; }
        public string? ManufacturerName { get; set; }
        public DateTime? InspectionDate { get; set; }
        public string? FactoryRepresentativeName { get; set; }
        public string OverallStatus { get; set; } = "Draft";
        public string? GeneralRemarks { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedAt { get; set; }

        // Navigation properties for relationships
        public int? CustomerID { get; set; }
        public Customer? Customer { get; set; }

        public int? InspectorUserID { get; set; }
        public User? Inspector { get; set; }

        public ReportDetails? ReportDetails { get; set; }
        public ICollection<ReportCriteriaStatus> CriteriaStatuses { get; set; } = new List<ReportCriteriaStatus>();
        public ICollection<ReportQuantityItem> QuantityItems { get; set; } = new List<ReportQuantityItem>();
        public ICollection<ReportDefect> Defects { get; set; } = new List<ReportDefect>();
        public ICollection<ReportMedia> MediaFiles { get; set; } = new List<ReportMedia>();
    }
}
