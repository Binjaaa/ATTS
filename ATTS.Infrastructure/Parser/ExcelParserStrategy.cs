using ATTS.Contracts.Parser;
using ATTS.Model;
using System;
using System.Collections.Generic;

namespace ATTS.Infrastructure.Parser
{
    public class ExcelParserStrategy : ITransactionFileParserStrategy
    {
        public IEnumerable<TransactionLine> Parse(string filePath)
        {
            throw new NotImplementedException();
        }

        public TransactionFileParserTypeEnum Name
        {
            get { return TransactionFileParserTypeEnum.Xlsx; }
        }
    }
}