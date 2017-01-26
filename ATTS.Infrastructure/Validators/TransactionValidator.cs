using ATTS.Contracts.Validators;
using ATTS.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ATTS.Infrastructure.Validators
{
    public class TransactionValidator : ITransactionValidator
    {
        private readonly ITransactionLineValidator _transactionLineValidator;

        public TransactionValidator(ITransactionLineValidator transactionLineValidator)
        {
            if (transactionLineValidator == null)
                throw new ArgumentNullException("transactionLineValidator");

            this._transactionLineValidator = transactionLineValidator;
        }

        public TransactionValidationResult Validate(IEnumerable<TransactionLine> lines)
        {
            var result = TransactionValidationResult.Create();

            //Don't want to enumerate through the enumerator twice
            int lineCount = 0;

            foreach (var line in lines)
            {
                lineCount++;

                var validationResult = _transactionLineValidator.Validate(line);
                if (validationResult.HasError)
                {
                    result.ValidationErrors.Add(validationResult);
                }
                else
                {
                    result.ValidLines.Add(line);
                }
            }

            Debug.WriteLine(string.Format("ValidationErrors capacity: {0}", result.ValidationErrors.Capacity));
            Debug.WriteLine(string.Format("ValidLines capacity: {0}", result.ValidLines.Capacity));

            return result;
        }
    }
}