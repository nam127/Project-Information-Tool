using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public string Visa { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public ICollection<ProjectEmployee>? ProjectEmployees { get; set; }
        public Group? Group { get; set; }
    }
}
