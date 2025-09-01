using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Application.DTOs
{
    public class StartInspectionResponseDto
    {
        public Guid ReportId { get; set; }
        public PrefilledDataDto PrefilledData { get; set; }
    }

    public class PrefilledDataDto
    {
        public string SblOrderNumber { get; set; }
        public string ProductDescription { get; set; }
        public string StyleNumber { get; set; }
        public int TotalQuantity { get; set; }
    }
}
