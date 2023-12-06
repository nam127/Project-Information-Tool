using PIMTool.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Domain.Objects
{
    public class FilterParameters : BaseParameters
    {
        public string? SearchTerm { get; set; }
        public ProjectStatus.EnumStatus Status { get; set; }
    }
}
