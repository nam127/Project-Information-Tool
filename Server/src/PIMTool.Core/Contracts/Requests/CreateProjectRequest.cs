using PIMTool.Core.Attributes;
using PIMTool.Core.Domain.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PIMTool.Core.Contracts.Requests;
public class CreateProjectRequest
{
    [Required]
    public decimal ProjectNumber { get; set; }
    [Required]
    public string Customer { get; set; } = null!;
    [Required]
    [EnumDataType(typeof(ProjectStatus.EnumStatus))]
    [DefaultValue(nameof(ProjectStatus.EnumStatus.NEW))]
    public string Status { get; set; } = nameof(ProjectStatus.EnumStatus.NEW);
    [Required]
    public string Name { get; set; } = null!;
    [Required]
    [DateValidation(nameof(EndDate), "End Date must be after Start Date")]
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [Required]
    public decimal GroupId { get; set; }
    public string[] Visas { get; set; } = null!;
}

