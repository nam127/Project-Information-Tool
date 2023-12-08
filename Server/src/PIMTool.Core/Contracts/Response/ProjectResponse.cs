using PIMTool.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Contracts.Response
{
    public class ProjectResponse
    {
        public IEnumerable<Project> Projects { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
    }
}
