namespace PIMTool.Core.Contracts.Requests;

public class DeleteProjectsRequest
{
    public IList<int> Ids{get; set;} = null!;
}