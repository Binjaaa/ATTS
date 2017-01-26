using ATTS.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATTS.Contracts.Validators
{
    public interface ITransactionLineValidator
    {
        ValidationResult Validate(TransactionLine transactionLine);
    }
}
