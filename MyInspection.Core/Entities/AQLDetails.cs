using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class AQLDetails
    {
        public int AQLDetailID { get; set; }
        public int LotSizeMin { get; set; }
        public int LotSizeMax { get; set; }
        public int SampleSize { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal MajorDefectLimit { get; set; }

        [Column(TypeName = "decimal(4, 2)")]
        public decimal MinorDefectLimit { get; set; }

        public int AcceptanceMajor { get; set; }
        public int RejectionMajor { get; set; }
        public int AcceptanceMinor { get; set; }
        public int RejectionMinor { get; set; }

        // Foreign key
        public int AQLLevelID { get; set; }
        public AQLLevel AQLLevel { get; set; } = null!;
    }
}
