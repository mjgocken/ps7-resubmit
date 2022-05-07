using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWARM.Shared.DTO
{
    public class CourseDTO
    {
        public int CourseNo { get; set; }
        public string Description { get; set; }
        public decimal? Cost { get; set; }
        public int? Prerequisite { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int SchoolId { get; set; }
        public int? PrerequisiteSchoolId { get; set; }
    }
}
