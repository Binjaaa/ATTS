using ATTS.Model;

namespace ATTS.Contracts.Parser
{
    public interface ITransactionFileParserStrategyResolver
    {
        ITransactionFileParserStrategy Resolve(TransactionFileParserTypeEnum fileParserType);
    }
}