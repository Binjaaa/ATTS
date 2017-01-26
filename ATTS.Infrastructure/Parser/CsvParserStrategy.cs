using ATTS.Contracts.Parser;
using ATTS.Model;
using CsvHelper;
using CsvHelper.Configuration;
using System.Collections.Generic;
using System.IO;

namespace ATTS.Infrastructure.Parser
{
    public class CsvParserStrategy : ITransactionFileParserStrategy
    {
        public IEnumerable<TransactionLine> Parse(string filePath)
        {
            var csvReaderConfig = new CsvConfiguration
            {
                Delimiter = ";",
                HasHeaderRecord = false
            };

            using (var textReader = new StreamReader(filePath))
            {
                using (var csvReader = new CsvReader(textReader, csvReaderConfig))
                {
                    while (csvReader.Read())
                    {
                        var account = csvReader.GetField<string>(0);
                        var description = csvReader.GetField<string>(1);
                        var currencyCode = csvReader.GetField<string>(2);
                        var transactionValue = csvReader.GetField<decimal?>(3);
                        var lineNumber = csvReader.Row;

                        yield return new TransactionLine()
                        {
                            Account = account,
                            Description = description,
                            CurrencyCode = currencyCode,
                            Value = transactionValue,
                            LineNumber = lineNumber
                        };
                    }
                }
            }
        }

        public TransactionFileParserTypeEnum Name
        {
            get { return TransactionFileParserTypeEnum.Csv; }
        }
    }
}