using Api.Interface;
using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Api.Web
{
    public class AutofacConfig
    {
        public static readonly AutofacConfig instance = new AutofacConfig();

        private IContainer container = null;

        public IContainer Container
        {
            get
            {
                if (container == null)
                {
                    RegisterService();
                }

                return container;
            }
        }

        public void Init()
        {
            RegisterService();
        }

        void RegisterService()
        {
            var builder = new ContainerBuilder();
            var baseType = typeof(IDependency);
            var assemblys = AppDomain.CurrentDomain.GetAssemblies().ToList();

            builder.RegisterAssemblyTypes(assemblys.ToArray())
                   .Where(t => baseType.IsAssignableFrom(t) && t != baseType)
                   .AsImplementedInterfaces().InstancePerLifetimeScope();

            container = builder.Build();
        }
    }
}