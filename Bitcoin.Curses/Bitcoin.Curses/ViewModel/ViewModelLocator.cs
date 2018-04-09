/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Bitcoin.Curses"
                           x:Key="Locator" />
  </Application.Resources>

  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using Bitcoin.Curses.Services;
using Bitcoin.Curses.Services.Interfaces;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using Xamarin.Forms;

namespace Bitcoin.Curses.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    [Android.Runtime.Preserve(AllMembers = true)]
    internal class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            //services
            SimpleIoc.Default.Register<IRateSettingsApplyService, RateSettingsApplyService>();
            SimpleIoc.Default.Register<ICustomCurrencySymbolService, CustomCurrencySymbolService>();
            SimpleIoc.Default.Register<IDataProvideService, DataProvideService>();
            SimpleIoc.Default.Register<IBitcoinDataService, BitcoinDataService>();
            SimpleIoc.Default.Register<ICurrencyNavigateService, CurrencyNavigateService>();
            SimpleIoc.Default.Register<INetworkService, NetworkService>();

            var dbPath = DependencyService.Get<IFileHelper>().GetLocalFilePath("db.sqlite");//"SQLite.db3");//

            //sqllite
            SimpleIoc.Default.Register<ICacheService>(() => new DatabaseService(dbPath));

            //viewModels
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public static void Cleanup()
        {
            // ToDo: Clear the ViewModels
            ServiceLocator.Current.GetInstance<MainViewModel>().Cleanup();
        }
    }
}