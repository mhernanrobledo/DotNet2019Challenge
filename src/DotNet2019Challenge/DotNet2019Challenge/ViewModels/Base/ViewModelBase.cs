using DotNet2019Challenge.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DotNet2019Challenge.ViewModels.Base
{
    public abstract class ViewModelBase : BindableObject
    {
        protected readonly INavigationService NavigationService;

        public ViewModelBase()
        {
            NavigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
        }

        public virtual Task InitializeAsync(object navigationData) => Task.FromResult(false);
    }
}
