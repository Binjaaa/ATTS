using ATTS.Contracts.Services;
using ATTS.Contracts.Validators;
using ATTS.Model;
using System;

namespace ATTS.Infrastructure.Validators
{
    public class CurrencyCodeValidator : ICurrencyCodeValidator
    {
        private readonly ICurrencyCodeService _currencyCodeService;

        public CurrencyCodeValidator(ICurrencyCodeService currencyCodeService)
        {
            if (currencyCodeService == null)
                throw new ArgumentNullException("currencyCodeService");

            this._currencyCodeService = currencyCodeService;
        }

        /// <summary>
        /// Valiate currency code against ISO 4217
        /// </summary>
        /// <returns></returns>
        public ValidationMessage Validate(string currencyCode)
        {
            var validationMessage = ValidationMessage.Create();

            if (string.IsNullOrWhiteSpace(currencyCode))
            {
                validationMessage.IsValid = false;
                validationMessage.ErrorMessage = string.Format("Currency code value is missing");
            }
            else if (!_currencyCodeService.IsCurrencyCodeValid(currencyCode))
            {
                validationMessage.IsValid = false;
                validationMessage.ErrorMessage = string.Format("Not a valid currency code: {0}", currencyCode);
            }

            return validationMessage;
        }
    }
}