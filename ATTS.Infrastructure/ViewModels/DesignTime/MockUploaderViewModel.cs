using ATTS.Contracts.ViewModels;
using ATTS.Model;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;

namespace ATTS.Infrastructure.ViewModels.DesignTime
{
    public class MockUploaderViewModel : IUploaderViewModel
    {
        public MockUploaderViewModel()
        {
            this.ProgressMessage = "Launching escape pods...";
            this.ProgressPercentage = 50;
            this.UploadedItemCount = 123456789;
            this.InitValidationResult();

            this.TotalLineCount = 99999;
            this.TotalLinesCommited = 99999;
            this.InvalidLineCount = 99999;
        }

        public string ProgressMessage { get; set; }
        public int ProgressPercentage { get; set; }
        public string SelectedFilePath { get; set; }

        public RelayCommand UploadCommand
        {
            get { throw new NotImplementedException(); }
        }

        public long UploadedItemCount { get; set; }

        public ObservableCollection<Model.ValidationResult> ValidationResults { get; set; }

        private void InitValidationResult()
        {
            var validationResult1 = new ValidationResult();
            validationResult1.Add(new ValidationMessage(false) { ErrorMessage = "nope1 nope nope" });

            var validationResult2 = new ValidationResult();
            validationResult2.Add(new ValidationMessage(false) { ErrorMessage = "nope2 nope nope" });
            validationResult2.Add(new ValidationMessage(false) { ErrorMessage = "nope the pope!" });

            var validationResult3 = new ValidationResult();
            validationResult3.Add(new ValidationMessage(false) { ErrorMessage = "new hope?" });

            this.ValidationResults = new ObservableCollection<Model.ValidationResult>
            {
                validationResult1,
                validationResult2,
                validationResult3
            };
        }

        public long TotalLineCount { get; set; }

        public long TotalLinesCommited { get; set; }

        public long InvalidLineCount { get; set; }

        public bool IsLoadingVisiblle
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public TimeSpan ElapsedUploadTime
        {
            get { throw new NotImplementedException(); }
        }
    }
}