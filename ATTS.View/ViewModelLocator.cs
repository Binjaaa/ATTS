using ATTS.Contracts.ViewModels;
using ATTS.Infrastructure.IoC;
using ATTS.Infrastructure.ViewModels.DesignTime;
using GalaSoft.MvvmLight;

namespace ATTS.View
{
    public class ViewModelLocator : ViewModelBase, IViewModelLocator
    {
        public IMainViewModel MainWindowViewModel
        {
            get
            {
                if (IsInDesignMode)
                {
                    return new MockMainViewModel();
                }

                return IoCKernel.Get<IMainViewModel>();
            }
        }

        public IUploaderViewModel UploaderViewModel
        {
            get
            {
                if (IsInDesignMode)
                {
                    return new MockUploaderViewModel();
                }

                return IoCKernel.Get<IUploaderViewModel>();
            }
        }
    }
}