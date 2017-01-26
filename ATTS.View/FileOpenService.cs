using ATTS.Contracts.ViewModels;
using Microsoft.Win32;

namespace ATTS.View
{
    public class FileOpenService : IFileOpenService
    {
        public string OpenFile()
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv"
            };

            string selectedFullFilePath = null;

            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                selectedFullFilePath = openFileDialog.FileName;
            }

            return selectedFullFilePath;
        }
    }
}