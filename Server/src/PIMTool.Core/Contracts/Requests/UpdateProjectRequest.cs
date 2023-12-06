using System.ComponentModel.DataAnnotations;
using PIMTool.Core.Attributes;

namespace PIMTool.Core.Contracts.Requests;
public class UpdateProjectRequest 
{
    public decimal Id{get; set;}
    [Required]
    public decimal GroupId { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
    
    [Required]
    [MaxLength(50)]
    public string Customer { get; set; } = null!;
    
    [Required]
    [MaxLength(3)]
    public string Status { get; set; } = null!;

    [Required]
    [DateValidation(nameof(EndDate), "End Date must be after Start Date")]
    public DateTime StartDate { get; set; }

    public DateTime? EndDate { get; set; }

    public string[] Visas { get; set; } = null!;
}