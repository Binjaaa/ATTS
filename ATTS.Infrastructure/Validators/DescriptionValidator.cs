using ATTS.Contracts.Validators;
using ATTS.Model;

namespace ATTS.Infrastructure.Validators
{
    public class DescriptionValidator : IDescriptionValidator
    {
        public ValidationMessage Validate(string account)
        {
            var validationMessage = ValidationMessage.Create();

            if (string.IsNullOrWhiteSpace(account))
            {
                validationMessage.IsValid = false;
                validationMessage.ErrorMessage = "The Decription is missing.";
            }

            return validationMessage;
        }
    }
}