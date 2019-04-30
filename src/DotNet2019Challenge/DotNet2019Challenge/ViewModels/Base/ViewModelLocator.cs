using System;
using Autofac;
using DotNet2019Challenge.Services.Movies;
using DotNet2019Challenge.Services.Navigation;

namespace DotNet2019Challenge.ViewModels.Base
{
    public class ViewModelLocator
    {
        IContainer container;
        ContainerBuilder containerBuilder;

        public static ViewModelLocator Instance { get; } = new ViewModelLocator();

        public ViewModelLocator()
        {
            containerBuilder = new ContainerBuilder();

            // Services
            containerBuilder.RegisterType<NavigationService>().As<INavigationService>().SingleInstance();
            containerBuilder.RegisterType<MoviesService>().As<IMoviesService>();

            // ViewModels
            containerBuilder.RegisterType<MoviesViewModel>();

            if (container != null)
            {
                container.Dispose();
            }
        }

        public T Resolve<T>()
        {
            return container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return container.Resolve(type);
        }

        public void Register<TInterface, TImplementation>() where TImplementation : TInterface => containerBuilder.RegisterType<TImplementation>().As<TInterface>();

        public void Register<T>() where T : class => containerBuilder.RegisterType<T>();

        public void Build() => container = containerBuilder.Build();
    }
}
