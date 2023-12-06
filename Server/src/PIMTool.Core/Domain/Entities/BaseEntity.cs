namespace PIMTool.Core.Domain.Entities;

public abstract class BaseEntity
{
    public decimal Id { get; set; }
    public Guid Version {  get; set; }
}