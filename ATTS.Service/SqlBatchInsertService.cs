using ATTS.Contracts.Services;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace ATTS.Service
{
    public class SqlBatchInsertService : ISqlBatchInsertService
    {
        public Task<long> InsertBatchAsync(DataTable dataTable, IProgress<int> progress)
        {
            long totalRowsCopied = 0;

            if (dataTable != null && dataTable.Rows.Count != 0)
            {
                var totalRowCount = dataTable.Rows.Count;

                using (var connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DatabaseToStoreTransactions"].ConnectionString))
                {
                    connection.Open();
                    SqlTransaction transaction = connection.BeginTransaction();

                    using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    {
                        bulkCopy.SqlRowsCopied += (sender, e) =>
                        {
                            totalRowsCopied = e.RowsCopied;
                            progress.Report(((int)e.RowsCopied) * 100 / totalRowCount);
                        };
                        bulkCopy.NotifyAfter = 1;
                        bulkCopy.BatchSize = 5000;
                        bulkCopy.DestinationTableName = "dbo.Transactions";
                        try
                        {
                            bulkCopy.WriteToServer(dataTable);
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            connection.Close();

                            //TODO: Log to file
                            Console.Out.WriteLine(ex.ToString());
                        }
                    }

                    if (transaction.Connection != null)
                    {
                        transaction.Commit();
                    }
                }
            }

            return Task.FromResult<long>(totalRowsCopied);
        }
    }
}