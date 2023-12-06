using System.ComponentModel.DataAnnotations;

namespace PIMTool.Core.Attributes;
public class DateValidationAttribute : ValidationAttribute
{
    private string _DateInput {get; set;}
    private string _ErrorMessage {get; set;}
    public DateValidationAttribute(string dateInput, string errorMessage) : base(errorMessage)
    {
        _DateInput = dateInput;
        _ErrorMessage = errorMessage;
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var invalidMessage = string.IsNullOrEmpty(_ErrorMessage)? ErrorMessage : "Invalid Date";
        var endDate = validationContext.ObjectType.GetProperty(_DateInput)?.GetValue(validationContext.ObjectInstance);
        if(endDate is null || value is null)
        {
            return new ValidationResult(invalidMessage);
        }

        var date = (DateTime)endDate;

        var startDate = DateTime.Parse(value.ToString());

        return (startDate < date) ? ValidationResult.Success : new ValidationResult(invalidMessage);
    }
    
}