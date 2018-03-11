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
       
        private void EnsureBuilder()
        {
            if (_builder == null)
                _builder = new ContainerBuilder();
        }

        private void EnsureBuilderContainer()
        {
            if (_builder == null)
                return;
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
