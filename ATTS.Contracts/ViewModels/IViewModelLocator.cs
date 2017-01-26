namespace ATTS.Contracts.ViewModels
{
    public interface IViewModelLocator
    {
        IMainViewModel MainWindowViewModel { get; }

        IUploaderViewModel UploaderViewModel { get; }
    }
}