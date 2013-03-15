using System;

using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;

using Neocean.Domain.Data;
using Neocean.Infrastructure.Caching;
using Neocean.Services.Customer;
using System.Data.Entity;
using Neocean.Events;

namespace Neocean.Services
{
    public static class NeoceanStarter
    {
        /// <summary>
        /// 配置IOC
        /// </summary>
        /// <returns></returns>
        public static IDependencyResolver  CreateHostContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<NopNullCache>().As<ICacheManager>().SingleInstance();

            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();

            builder.Register<IDbContext>(c => new NeoObjectContext(dataProviderSettings.DataConnectionString)).SingleInstance();

            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).SingleInstance();

            builder.RegisterType<UserService>().As<IUserService>();

            return new AutofacDependencyResolver(builder.Build());
        }

    }
}
