using ATTS.Contracts.Parser;
using ATTS.Contracts.Services;
using ATTS.Contracts.Validators;
using ATTS.Contracts.ViewModels;
using ATTS.Contracts.Views;
using ATTS.Infrastructure.Parser;
using ATTS.Infrastructure.Validators;
using ATTS.Infrastructure.Validators.Dummy;
using ATTS.Infrastructure.ViewModels;
using ATTS.Service;
using Ninject.Modules;

namespace ATTS.View
{
    public class BootstrapperModule : NinjectModule
    {
        public override void Load()
        {
            //Services
            Bind<IFileOpenService>().To<FileOpenService>();
            Bind<ISqlBatchInsertService>().To<SqlBatchInsertService>();
            Bind<ICurrencyCodeService>().To<CurrencyCodeService>();

            //Validators
            Bind<IAccountValidator>().To<AccountValidator>();
            Bind<IDescriptionValidator>().To<DescriptionValidator>();
            Bind<ICurrencyCodeValidator>().To<CurrencyCodeValidator>();
            Bind<ITransactionValueValidator>().To<TransactionValueValidator>();

            Bind<ITransactionLineValidator>().To<TransactionLineValidator>();
            Bind<ITransactionValidator>().To<TransactionValidator>();

            //For performance test purposes, every item is valid during the parse.
            //Bind<ITransactionValidator>().To<TransactionDummyValidator>();

            //Strategies
            Bind<ITransactionFileParserStrategy>().To<CsvParserStrategy>();
            Bind<ITransactionFileParserStrategy>().To<ExcelParserStrategy>();
            Bind<ITransactionFileParserStrategyResolver>().To<TransactionFileParserStrategyResolver>();

            //ViewModels
            Bind<IMainViewModel>().To<MainViewModel>().InSingletonScope();
            Bind<IUploaderViewModel>().To<UploaderViewModel>().InSingletonScope();

            //Views
            Bind<IMainView>().To<MainWindow>();
        }
    }
}