using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATTS.Model
{
    public struct TransactionValidationResult
    {
        public List<TransactionLine> ValidLines { get; set; }

        public List<ValidationResult> ValidationErrors { get; set; }

        public int TotalLineCount
        {
            get { return this.ValidationErrors.Count + this.ValidLines.Count; }
        }

        public int ValidationErrorCount
        {
            get { return this.ValidationErrors.Count; }
        }

        public TransactionValidationResult(List<TransactionLine> validLines, List<ValidationResult> validationErrors)
            : this()
        {
            this.ValidLines = validLines;
            this.ValidationErrors = validationErrors;
        }

        //Used this not that nice solution to create default initialized properties without using parameterized ctor
        public static TransactionValidationResult Create()
        {
            return new TransactionValidationResult(new List<TransactionLine>(), new List<ValidationResult>());
        }
    }
}
