using ATTS.Contracts.Validators;
using ATTS.Model;

namespace ATTS.Infrastructure.Validators
{
    public class TransactionValueValidator : ITransactionValueValidator
    {
        public ValidationMessage Validate(decimal? transactionValue)
        {
            var validationMessage = ValidationMessage.Create();

            if (!transactionValue.HasValue)
            {
                validationMessage.IsValid = false;
                validationMessage.ErrorMessage = "The Value column in transaction is not filled.";
            }
            else if (transactionValue.Value < 0)
            {
                validationMessage.IsValid = false;
                validationMessage.ErrorMessage = "The Value is less than 0.";
            }

            return validationMessage;
        }
    }
}