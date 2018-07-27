using InternetStore.BLL.Interfaces;
using InternetStore.BLL.Services;
using InternetStore.DAL.Interfaces;
using InternetStore.DAL.Repositories;
using Ninject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace InternetStore.BLL.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        private string connectionString;

        public NinjectDependencyResolver(string connectionString)
        {
            this.connectionString = connectionString;
            kernel = new StandardKernel();
            AddBindings();
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
        private void AddBindings()
        {
            //.InSingletonScope()
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().WithConstructorArgument(connectionString);
            kernel.Bind<IAccountService>().To<AccountService>();
            kernel.Bind<IProductService>().To<ProductService>();
            kernel.Bind<ICartService>().To<CartService>();
            //kernel.Bind<IOnlineService>().To<OnlineService>();
        }
    }
}
