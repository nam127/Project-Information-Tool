namespace PIMTool.Core.Constants;

public static class ExceptionMessageConstantsException 
{
    public const string BUSINESS_MESSAGE = "Cannot delete project with status not NEW";
    public const string DUPPLICATE_NUMBER_MESSAGE = "The project number already existed. Please select a different project number";
    public const string PROJECT_NOT_FOUND = "The project is not found";
    public const string CONCURRENCY_UPDATE_MESSAGE = "Unable to save changes. The project was updated by another user.";
}