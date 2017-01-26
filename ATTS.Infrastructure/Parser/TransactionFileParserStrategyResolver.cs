using ATTS.Contracts.Parser;
using ATTS.Model;
using System;
using System.Linq;

namespace ATTS.Infrastructure.Parser
{
    public class TransactionFileParserStrategyResolver : ITransactionFileParserStrategyResolver
    {
        private readonly ITransactionFileParserStrategy[] _fileParserStrategies;

        public TransactionFileParserStrategyResolver(ITransactionFileParserStrategy[] fileParserStrategies)
        {
            if (fileParserStrategies == null)
                throw new ArgumentNullException("fileParserStrategies");

            this._fileParserStrategies = fileParserStrategies;
        }

        public ITransactionFileParserStrategy Resolve(TransactionFileParserTypeEnum fileParserType)
        {
            if (fileParserType == TransactionFileParserTypeEnum.None) return null;

            return _fileParserStrategies.FirstOrDefault(p => p.Name == fileParserType);
        }
    }
}