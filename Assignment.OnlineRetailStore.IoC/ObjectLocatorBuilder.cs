using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Assignment.OnlineRetailStore.IoC
{
    public class ObjectLocatorBuilder
    {    
        private ContainerBuilder _builder = new ContainerBuilder();
        private IContainer _container = null;


        public void RegisterTypeAsInterface<T, I>()
        {
            EnsureBuilder();
            _builder.RegisterType<T>().As<I>();
        }

        public void RegisterInstanceAsInterface<T>(T instance) where T : class
        {
            EnsureBuilder();
            _builder.RegisterInstance(instance);
        }

        public void RegisterLambdaAsInterface<T>(Func<T> expr) where T : class
        {
            EnsureBuilder();
            _builder.Register<T>(c => expr());
        }

        public void RegisterLambdaAsInterface<T>(Func<object[], T> expr) where T : class
        {
            EnsureBuilder();
            _builder.Register<T>((c, parameters) =>
            {
                var args = parameters
                    .OfType<PositionalParameter>()
                    .Select(p => p.Value)
                    .ToArray();

                return expr(args);
            });
        }

        public void RegisterAssemblyTypes(Assembly assembly, Func<Type, bool> predicate)
        {
            EnsureBuilder();
            _builder.RegisterAssemblyTypes(assembly)
                .Where(predicate)
                .AsImplementedInterfaces();
        }

        public void RegisterApiControllers(params Assembly[] controllerAssemblies)
        {
            EnsureBuilder();
            _builder.RegisterApiControllers(controllerAssemblies);
        }

        public void RegisterForWebApi(Owin.IAppBuilder owinApp, System.Web.Http.HttpConfiguration webApi)
        {
            EnsureBuilderContainer();
            var scope = _container.BeginLifetimeScope();
            webApi.DependencyResolver = new AutofacWebApiDependencyResolver(scope);
            owinApp.UseAutofacMiddleware(scope);
            //owinApp.UseAutofacWebApi(webApi); // TODO: Adding needed "Autofac.WebApi2.Owin" nuget package causes "System.Web.Http.Owin" dependency error
        }

        public void RegisterForWebApi(System.Web.Http.HttpConfiguration webApi)
        {
            EnsureBuilderContainer();
            webApi.DependencyResolver = new AutofacWebApiDependencyResolver(_container.BeginLifetimeScope());
        }

        public void RegisterMVCControllers(Assembly controllerAssemblie)
        {
            EnsureBuilder();
            _builder.RegisterControllers(controllerAssemblie);
            EnsureBuilderContainer();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(_container));
        }

        private void EnsureBuilder()
        {
            if (_builder == null)
                _builder = new ContainerBuilder();
        }

        private void EnsureBuilderContainer()
        {
            if (_builder == null)
                return; // Nothing to do. No new registrations since previous call to EnsureContainer.
            if (_container == null)
                _container = _builder.Build();
            else
                _builder.Update(_container);

            _builder = null;
        }

        internal IContainer Build()
        {
            EnsureBuilderContainer();
            return _container;
        }
    }
}
