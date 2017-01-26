using System.Collections.Generic;

namespace ATTS.Model
{
    //TODO: use struct
    public class ValidationResult
    {
        public ValidationResult()
        {
            this.Errors = new List<ValidationMessage>();
        }

        public ValidationResult(int lineNumber)
            : this()
        {
            this.LineNumber = lineNumber;
        }

        public bool HasError { get; private set; }
        public int LineNumber { get; set; }

        public List<ValidationMessage> Errors { get; private set; }

        public void Add(ValidationMessage msg)
        {
            if (!msg.IsValid)
            {
                this.HasError = true;
                this.Errors.Add(msg);
            }
        }
    }
}