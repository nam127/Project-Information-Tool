using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Domain.Entities
{
    public class ProjectEmployee : BaseEntity
    {
        public decimal ProjectId {  get; set; }
        public decimal EmployeeId { get; set; }
        public Project? Project { get; set; }
        public Employee? Employee { get; set; }
    }
}
