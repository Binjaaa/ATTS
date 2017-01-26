using ATTS.Model;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;

namespace ATTS.Contracts.ViewModels
{
    public interface IUploaderViewModel
    {
        string ProgressMessage { get; set; }
        int ProgressPercentage { get; set; }

        string SelectedFilePath { get; set; }

        RelayCommand UploadCommand { get; }

        long TotalLineCount { get; set; }
        long TotalLinesCommited { get; set; }
        long InvalidLineCount { get; set; }

        ObservableCollection<ValidationResult> ValidationResults { get; set; }

        bool IsLoadingVisiblle { get; }

        TimeSpan ElapsedUploadTime { get; }
    }
}