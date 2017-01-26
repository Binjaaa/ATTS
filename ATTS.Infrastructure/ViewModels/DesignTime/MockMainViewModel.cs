using ATTS.Contracts.ViewModels;
using GalaSoft.MvvmLight.CommandWpf;
using System;

namespace ATTS.Infrastructure.ViewModels.DesignTime
{
    public class MockMainViewModel : IMainViewModel
    {
        public MockMainViewModel()
        {
        }

        public IUploaderViewModel UploaderViewModel
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
    }
}