namespace ATTS.Model
{
    public struct ValidationMessage
    {
        public ValidationMessage(bool isValid)
            : this()
        {
            this.IsValid = isValid;
        }

        public string ErrorMessage { get; set; }
        public bool IsValid { get; set; }

        //Used this not that nice solution to create default initialized properties without using parameterized ctor
        public static ValidationMessage Create()
        {
            return new ValidationMessage(true);
        }
    }
}