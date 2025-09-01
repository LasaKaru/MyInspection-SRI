using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class ReportMedia
    {
        [Key]
        public int MediaID { get; set; }
        public string S3BucketName { get; set; } = string.Empty;
        public string S3ObjectKey { get; set; } = string.Empty;
        public string MediaType { get; set; } = string.Empty;
        public string? FriendlyName { get; set; }
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;

        // Foreign key
        public Guid ReportID { get; set; }
        public InspectionReport InspectionReport { get; set; } = null!;
    }
}
