using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Domain.Entities
{
    public class Group : BaseEntity
    {
        public decimal Group_Leader_Id { get; set; }
        public ICollection<Project> Projects { get; set;} = null!;
        public Employee? Employee { get; set; }
    }
}
