using ATTS.Contracts.Validators;
using ATTS.Model;
using System.Collections.Generic;
using System.Linq;

namespace ATTS.Infrastructure.Validators.Dummy
{
    /// <summary>
    /// Performance test purpose,every item marked as valid, DON'T USE IN PROD!
    /// </summary>
    public class TransactionDummyValidator : ITransactionValidator
    {
        public Model.TransactionValidationResult Validate(IEnumerable<TransactionLine> lines)
        {
            return new Model.TransactionValidationResult()
            {
                ValidLines = lines.ToList(),
                ValidationErrors = new List<ValidationResult>()
            };
        }
    }
}