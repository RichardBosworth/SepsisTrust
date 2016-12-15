using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AzureData;
using Guidelines.IO;
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using Microsoft.WindowsAzure.MobileServices.Sync;
using PCLStorage;
using Prism.Navigation;
using Prism.Unity;
using SepsisTrust.Model.Azure;
using SepsisTrust.Model.Storage;
using SepsisTrust.Model.User;
using SepsisTrust.Views;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SepsisTrust
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }

        protected override void OnInitialized()
        {
            InitializeComponent();

            StaticAzureService.Initialize();
            StaticAzureService.EnableLocalStorage(store => store.DefineTable<Guideline>(), store => store.DefineTable<ClinicalArea>());

            NavigationService.NavigateAsync("/LoadingPage/");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<IJsonObjectStreamReader, JsonObjectStreamReader>();
            Container.RegisterType<IJsonObjectStreamWriter, JsonObjectStreamWriter>();
            Container.RegisterType<IFileStreamRetriever, FileStreamRetriever>();

            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<EditPatientCharacteristicPage>("CharacteristicPage");
            Container.RegisterTypeForNavigation<GuidelinePage>();
            Container.RegisterTypeForNavigation<SelectClinicalAreaPage>();
            Container.RegisterTypeForNavigation<GuidelinesNavigationPage>("GNav");
            Container.RegisterTypeForNavigation<EditUserDetailsPage>("EditUser");
            Container.RegisterTypeForNavigation<LoadingPage>();
        }
    }
}
