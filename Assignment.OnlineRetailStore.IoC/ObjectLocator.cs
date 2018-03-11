using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.OnlineRetailStore.IoC
{
    public class ObjectLocator
    { 
        private static object _syncRoot = new object();
        private static IContainer _container = null;
        private static ObjectLocatorBuilder _builder = null;

        private static readonly object[] _noParams = new object[0];

        public static T CreateInstance<T>(string defaultImplementation) where T : class
        {
            return CreateInstance<T>(defaultImplementation, _noParams);
        }

        public static T CreateInstance<T>(string defaultImplementation, params object[] args) where T : class
        {
            EnsureContainer();

            object instance;

            _container.TryResolveService(
                new Autofac.Core.TypedService(typeof(T)),
                args.Select((item, index) => new PositionalParameter(index, item)),
                out instance);

            if (instance == null)
            {
                Type defaultInstance = Type.GetType(defaultImplementation);
                if (defaultInstance == null)
                    throw new ApplicationException("Cannot find implementation for '" + typeof(T).FullName + "': '" + defaultImplementation + "'");
                instance = Activator.CreateInstance(defaultInstance, args) as T;
            }
            return instance as T;
        }

        public static T CreateInstance<T>(Func<T> defaultImplementation, params object[] args) where T : class
        {
            if (args == null)
                args = _noParams;

            EnsureContainer();

            object instance;

            _container.TryResolveService(
                new Autofac.Core.TypedService(typeof(T)),
                args.Select((item, index) => new PositionalParameter(index, item)),
                out instance);

            if (instance == null)
            {
                instance = defaultImplementation();
                if (instance == null)
                    throw new ApplicationException("Cannot find implementation for '" + typeof(T).FullName + "'");
            }
            return instance as T;
        }

        public void Build()
        {
            EnsureContainer();
        }

        private static void EnsureContainer()
        {
            lock (_syncRoot)
            {
                if (_builder == null)
                {
                    _container = new ContainerBuilder().Build();
                }
                else
                {
                    _container = _builder.Build();
                }
            }
        }

        public static ObjectLocatorBuilder RegisterBuilder(ObjectLocatorBuilder builder)
        {
            lock (_syncRoot)
            {
                if (_container != null)
                {
                    _container.Dispose();
                    _container = null;
                }

                _builder = builder;
                return builder;
            }
        }
    }
}
