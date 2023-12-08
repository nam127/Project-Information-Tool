using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PIMTool.Core.Domain.Enums
{
    public static class ProjectStatus
    {
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public enum EnumStatus
        {
            None,
            NEW,
            PLA,
            INP,
            FIN
        }
    }
}
