using PIMTool.Core.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIMTool.Dtos;

public class ProjectDto
{
    public decimal Id {get; set; }
    public decimal ProjectNumber { get; set; }
    public string Customer { get; set; }
    public string Status { get; set; } = nameof(ProjectStatus.EnumStatus.NEW);
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public decimal GroupId { get; set; }
}