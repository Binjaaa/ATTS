using ATTS.Model;
using System.Collections.Generic;

namespace ATTS.Contracts.Parser
{
    public interface ITransactionFileParserStrategy
    {
        IEnumerable<TransactionLine> Parse(string filePath);

        TransactionFileParserTypeEnum Name { get; }
    }
}