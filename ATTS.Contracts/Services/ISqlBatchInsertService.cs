using ATTS.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace ATTS.Contracts.Services
{
    public interface ISqlBatchInsertService
    {
        Task<long> InsertBatchAsync(DataTable dataTable, IProgress<int> progress);
    }
}