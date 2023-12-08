using PIMTool.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIMTool.Core.Contracts.Response
{
    public class GroupResponse
    {
        public IEnumerable<Group> groups { get; set; }
    }
}
