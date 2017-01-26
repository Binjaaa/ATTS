using ATTS.Contracts.Validators;
using ATTS.Model;
using System;

namespace ATTS.Infrastructure.Validators
{
    public class TransactionLineValidator : ITransactionLineValidator
    {
        private readonly ICurrencyCodeValidator _currencyCodeValiator;
        private readonly IAccountValidator _accountValidator;
        private readonly IDescriptionValidator _descriptionValidator;
        private readonly ITransactionValueValidator _transactionValueValidator;

        public TransactionLineValidator(IAccountValidator accountValidator, IDescriptionValidator descriptionValidator, ICurrencyCodeValidator currencyCodeValidator, ITransactionValueValidator transactionValueValidator)
        {
            if (accountValidator == null)
                throw new ArgumentNullException("accountValidator");

            if (descriptionValidator == null)
                throw new ArgumentNullException("descriptionValidator");

            if (currencyCodeValidator == null)
                throw new ArgumentNullException("currencyCodeValidator");

            if (transactionValueValidator == null)
                throw new ArgumentNullException("transactionValueValidator");

            this._currencyCodeValiator = currencyCodeValidator;
            this._accountValidator = accountValidator;
            this._descriptionValidator = descriptionValidator;
            this._transactionValueValidator = transactionValueValidator;
        }

        public ValidationResult Validate(TransactionLine transactionLine)
        {
            //Account
            var accountResult = _accountValidator.Validate(transactionLine.Account);

            //Description
            var descriptionResult = _descriptionValidator.Validate(transactionLine.Description);

            //CurrencyCode
            var currencyCodeResult = _currencyCodeValiator.Validate(transactionLine.CurrencyCode);

            //Value
            var transactionValueResult = _transactionValueValidator.Validate(transactionLine.Value);

            var validationResult = new ValidationResult(transactionLine.LineNumber);

            validationResult.Add(accountResult);
            validationResult.Add(descriptionResult);
            validationResult.Add(currencyCodeResult);
            validationResult.Add(transactionValueResult);

            return validationResult;
        }
    }
}