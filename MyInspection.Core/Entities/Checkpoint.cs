using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class Checkpoint
    {
        public int CheckpointID { get; set; }
        public string QuestionText { get; set; } = string.Empty;
        public string InputType { get; set; } = string.Empty;
        public string? SpecificationTolerance { get; set; }
        public int DisplayOrder { get; set; }
        public bool IsEnabled { get; set; } = true;

        // Foreign key
        public int CriteriaID { get; set; }
        public MasterCriteria MasterCriteria { get; set; } = null!;
    }
}
