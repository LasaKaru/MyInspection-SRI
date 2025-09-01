using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyInspection.Core.Entities
{
    public class MasterCriteria
    {
        [Key]
        public int CriteriaID { get; set; }
        public string CriteriaName { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public bool IsEnabled { get; set; } = true;
    }
}
