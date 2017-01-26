using ATTS.Contracts.Parser;
using ATTS.Contracts.Services;
using ATTS.Contracts.Validators;
using ATTS.Contracts.ViewModels;
using ATTS.Infrastructure.Extensions;
using ATTS.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace ATTS.Infrastructure.ViewModels
{
    public class UploaderViewModel : ViewModelBase, IUploaderViewModel
    {
        private string _selectedFilePath;
        private string _progressMessage;
        private int _progressPercentage;
        private RelayCommand _uploadCommand;
        private RelayCommand _openFileSelectionDialogCommand;
        private ObservableCollection<ValidationResult> _validationResults;

        private long _totalLineCount;
        private long _totalLinesCommited;
        private long _invalidLineCount;
        private bool _isLoadingVisible;
        private TimeSpan _elapsedUploadTime;

        private readonly ITransactionFileParserStrategyResolver _transactionFileParserResolver;
        private readonly ITransactionValidator _transactionValidator;
        private readonly ISqlBatchInsertService _sqlBatchInsertService;
        private readonly IFileOpenService _fileOpenService;

        public UploaderViewModel(ITransactionFileParserStrategyResolver transactionFileParserResolver, ITransactionValidator transactionValidator, ISqlBatchInsertService sqlBatchInsertService, IFileOpenService fileOpenService)
        {
            if (transactionFileParserResolver == null)
                throw new ArgumentNullException("transactionFileParserResolver");

            if (transactionValidator == null)
                throw new ArgumentNullException("transactionValidator");

            if (sqlBatchInsertService == null)
                throw new ArgumentNullException("sqlBatchInsertService");

            if (fileOpenService == null)
                throw new ArgumentNullException("fileOpenService");

            this._transactionFileParserResolver = transactionFileParserResolver;
            this._transactionValidator = transactionValidator;
            this._sqlBatchInsertService = sqlBatchInsertService;
            this._fileOpenService = fileOpenService;

            this._isLoadingVisible = false;
        }

        public string SelectedFilePath
        {
            get { return _selectedFilePath; }
            set { Set(() => SelectedFilePath, ref _selectedFilePath, value); }
        }

        public string ProgressMessage
        {
            get { return _progressMessage; }
            set { Set(() => ProgressMessage, ref _progressMessage, value); }
        }

        public int ProgressPercentage
        {
            get { return _progressPercentage; }
            set { Set(() => ProgressPercentage, ref _progressPercentage, value); }
        }

        public ObservableCollection<ValidationResult> ValidationResults
        {
            get { return _validationResults; }
            set { Set(() => ValidationResults, ref _validationResults, value); }
        }

        public long TotalLineCount
        {
            get { return _totalLineCount; }
            set { Set(() => TotalLineCount, ref _totalLineCount, value); }
        }

        public long TotalLinesCommited
        {
            get { return _totalLinesCommited; }
            set { Set(() => TotalLinesCommited, ref _totalLinesCommited, value); }
        }

        public long InvalidLineCount
        {
            get { return _invalidLineCount; }
            set { Set(() => InvalidLineCount, ref _invalidLineCount, value); }
        }

        public bool IsLoadingVisiblle
        {
            get { return _isLoadingVisible; }
            private set { Set(() => IsLoadingVisiblle, ref _isLoadingVisible, value); }
        }

        public TimeSpan ElapsedUploadTime
        {
            get { return _elapsedUploadTime; }
            private set { Set(() => ElapsedUploadTime, ref _elapsedUploadTime, value); }
        }

        public RelayCommand UploadCommand
        {
            get { return _uploadCommand ?? (_uploadCommand = new RelayCommand(OnUploadCommandExecute, UploadCommandCanExecute)); }
        }

        public RelayCommand OpenFileSelectionDialogCommand
        {
            get { return _openFileSelectionDialogCommand ?? (_openFileSelectionDialogCommand = new RelayCommand(OnOpenFileSelectionDialogCommandExecute, OpenFileSelectionDialogCommandCanExecute)); }
        }

        private void OnOpenFileSelectionDialogCommandExecute()
        {
            this.SelectedFilePath = _fileOpenService.OpenFile();
        }

        /// <summary>
        /// User should be able upload only when selected a file and there is no uploading in progress
        /// </summary>
        /// <returns></returns>
        private bool UploadCommandCanExecute()
        {
            return (!string.IsNullOrWhiteSpace(this.SelectedFilePath) && this.IsLoadingVisiblle == false) && !this.IsLoadingVisiblle;
        }

        private bool OpenFileSelectionDialogCommandCanExecute()
        {
            return this.IsLoadingVisiblle == false;
        }

        public async void OnUploadCommandExecute()
        {
            this.IsLoadingVisiblle = true;

            Stopwatch sw = new Stopwatch();
            sw.Start();

            this.ProgressMessage = "Reading file...";
            var parserStrategy = this._transactionFileParserResolver.Resolve(GetFileTypeByFilePath(this.SelectedFilePath));
            var parsedRows = parserStrategy.Parse(this.SelectedFilePath);

            this.ProgressMessage = "Validating content...";
            var validationResult = await Task.Run(() => this._transactionValidator.Validate(parsedRows));

            this.ProgressMessage = "Commiting to database...";
            var totalRowsCopied = await Task.Run(() => this._sqlBatchInsertService.InsertBatchAsync(validationResult.ValidLines.AsDataTable(), new Progress<int>(p => this.ProgressPercentage = p)));

            this.ProgressMessage = "We are done.";
            this.ProgressPercentage = 100;

            this.TotalLinesCommited = totalRowsCopied;
            this.TotalLineCount = validationResult.TotalLineCount;
            this.InvalidLineCount = validationResult.ValidationErrorCount;

            this.ValidationResults = new ObservableCollection<ValidationResult>(validationResult.ValidationErrors);

            sw.Stop();
            Debug.WriteLine("-------" + sw.Elapsed.ToString());

            this.ElapsedUploadTime = sw.Elapsed;
            this.IsLoadingVisiblle = false;
        }

        private TransactionFileParserTypeEnum GetFileTypeByFilePath(string filePath)
        {
            var retVal = TransactionFileParserTypeEnum.None;

            if (string.IsNullOrWhiteSpace(filePath)) return retVal;

            var extension = Path.GetExtension(filePath).Trim('.');

            if (!string.IsNullOrWhiteSpace(extension))
            {
                retVal = (TransactionFileParserTypeEnum)Enum.Parse(typeof(TransactionFileParserTypeEnum), extension, true);
            }

            return retVal;
        }
    }
}