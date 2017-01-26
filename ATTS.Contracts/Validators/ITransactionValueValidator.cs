using ATTS.Model;

namespace ATTS.Contracts.Validators
{
    public interface ITransactionValueValidator
    {
        ValidationMessage Validate(decimal? value);
    }
}