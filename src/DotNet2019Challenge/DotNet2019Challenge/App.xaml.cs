using DotNet2019Challenge.Views;
using System.Threading.Tasks;
using Xamarin.Forms;
using DotNet2019Challenge.ViewModels;
using DotNet2019Challenge.ViewModels.Base;
using DotNet2019Challenge.Services.Navigation;

namespace DotNet2019Challenge
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            BuildDependencies();
            
            InitNavigation();

        }
        void BuildDependencies()
        {
            ViewModelLocator.Instance.Build();
        }

        Task InitNavigation()
        {
            var navigationService = ViewModelLocator.Instance.Resolve<INavigationService>();
            return navigationService.InitializeAsync();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
