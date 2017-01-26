using ATTS.Model;

namespace ATTS.Contracts.Validators
{
    public interface IAccountValidator
    {
        ValidationMessage Validate(string account);
    }
}