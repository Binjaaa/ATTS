using ATTS.Contracts.ViewModels;
using GalaSoft.MvvmLight;
using System;

namespace ATTS.Infrastructure.ViewModels
{
    public class MainViewModel : ViewModelBase, IMainViewModel
    {
        private IUploaderViewModel _uploaderViewModel;

        public MainViewModel(IUploaderViewModel uploaderViewModel)
        {
            if (uploaderViewModel == null)
                throw new ArgumentNullException("uploaderViewModel");

            this.UploaderViewModel = uploaderViewModel;
        }

        public IUploaderViewModel UploaderViewModel
        {
            get { return _uploaderViewModel; }
            set { Set(() => UploaderViewModel, ref _uploaderViewModel, value); }
        }
    }
}