using Guidelines.IO;
using Microsoft.Practices.Unity;
using Prism.Unity;
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

            NavigationService.NavigateAsync("GNav/MainPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<IJsonObjectStreamReader, JsonObjectStreamReader>();
            Container.RegisterType<IJsonObjectStreamWriter, JsonObjectStreamWriter>();
            Container.RegisterType<IFileStreamRetriever, FileStreamRetriever>();

            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<EditPatientCharacteristicPage>("CharacteristicPage");
            Container.RegisterTypeForNavigation<GuidelinePage>();
            Container.RegisterTypeForNavigation<GuidelinesNavigationPage>("GNav");
            Container.RegisterTypeForNavigation<EditUserDetailsPage>("EditUser");

        }
    }
}
