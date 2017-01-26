using ATTS.Infrastructure.IoC;
using Ninject;
using System.Reflection;
using System.Windows;

namespace ATTS.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            IoCKernel.Init(new BootstrapperModule());

            base.OnStartup(e);
        }
    }
}