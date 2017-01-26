using ATTS.Model;

namespace ATTS.Contracts.Validators
{
    public interface IDescriptionValidator
    {
        ValidationMessage Validate(string description);
    }
}