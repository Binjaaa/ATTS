using ATTS.Model;

namespace ATTS.Contracts.Validators
{
    public interface ICurrencyCodeValidator
    {
        ValidationMessage Validate(string currencyCode);
    }
}