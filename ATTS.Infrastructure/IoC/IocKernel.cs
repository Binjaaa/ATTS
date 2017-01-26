using Ninject;
using Ninject.Modules;

namespace ATTS.Infrastructure.IoC
{
    public static class IoCKernel
    {
        private static IKernel kernel;

        public static void Init(params NinjectModule[] modules)
        {
            if (kernel == null)
            {
                kernel = new StandardKernel(modules);
            }
        }

        public static T Get<T>()
        {
            return kernel.Get<T>();
        }
    }
}