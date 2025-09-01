using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class ReportCheckpointAnswer
    {
        [Key]
        public int AnswerID { get; set; }
        public string? AnswerValue { get; set; }
        public int? QuantitySampled { get; set; }
        public string? Comments { get; set; }

        // Foreign keys
        public Guid ReportID { get; set; }
        public int CheckpointID { get; set; }

        // Navigation properties
        public InspectionReport InspectionReport { get; set; } = null!;
        public Checkpoint Checkpoint { get; set; } = null!;
    }
}
