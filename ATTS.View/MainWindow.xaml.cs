using ATTS.Contracts.Services;
using ATTS.Contracts.ViewModels;
using ATTS.Infrastructure.Validators;
using ATTS.Infrastructure.ViewModels;
using ATTS.Model;
using ATTS.Service;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using ATTS.Infrastructure.Extensions;
using System.Threading.Tasks;
using System.Data;
using System;
using ATTS.Contracts.Views;

namespace ATTS.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainView
    {
        private TransactionLineValidator validator = new TransactionLineValidator(new AccountValidator(), new DescriptionValidator(), new CurrencyCodeValidator(new CurrencyCodeService()), new TransactionValueValidator());

        public MainWindow()
        {
            InitializeComponent();
            //Stopwatch sw = new Stopwatch();
            //Stopwatch swRead = new Stopwatch();
            //Stopwatch swSelect = new Stopwatch();

            //sw.Start();

            //swRead.Start();
            //IExcelReaderService df = new ExcelReaderService();
            //var bigDataItems = df.ReadExcelToModel(@"d:\Gitter\ATTS\TestFileGenerator\bin\Debug\Transactions.xlsx");
            //swRead.Stop();


            //swSelect.Start();
            //var resssss = GetFinalREsult(bigDataItems);
            //swSelect.Stop();

            //SzandiMethod(resssss.AsDataTable());

            //sw.Stop();

            //string messsage = string.Format("Full process: {0}\n Read: {1}\n Convert:{2}", sw.Elapsed.ToString(), swRead.Elapsed.ToString(), swSelect.Elapsed.ToString());

            //MessageBox.Show(messsage);
        }

        private async Task SzandiMethod(DataTable dt)
        {
            ISqlBatchInsertService sql = new SqlBatchInsertService();
            await sql.InsertBatchAsync(dt, new Progress<int>(p => Debug.WriteLine(string.Format("{0}%", p))));
        }

        private IEnumerable<TransactionLine> GetFinalREsult(IEnumerable<TransactionLine> allLines)
        {
            foreach (var line in allLines)
            {
                var validationResult = validator.Validate(line);
                if (validationResult.HasError)
                {
                    yield return line;
                }
            }
        }
    }
}