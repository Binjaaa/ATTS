using ATTS.Model;
using System.Collections.Generic;

namespace ATTS.Contracts.Validators
{
    public interface ITransactionValidator
    {
        TransactionValidationResult Validate(IEnumerable<TransactionLine> lines);
    }
}