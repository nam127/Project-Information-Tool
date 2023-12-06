using PIMTool.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace PIMTool.Core.Domain.Entities
{
    public class Project : BaseEntity
    {
        public decimal ProjectNumber { get; set; }
        public string Name { get; set; } = null!;
        public string Customer {  get; set; } = null!;
        public ProjectStatus.EnumStatus Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal GroupId { get; set; }
        public Group? Group { get; set; }
        public ICollection<ProjectEmployee>? ProjectEmployees { get; set; }
    }
}