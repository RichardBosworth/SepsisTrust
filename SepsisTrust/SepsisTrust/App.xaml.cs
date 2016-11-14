using Guidelines.IO;
using Prism.Unity;
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

            NavigationService.NavigateAsync("MainPage?title=Hello%20from%20Xamarin.Forms");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterTypeForNavigation<MainPage>();
            Container.RegisterTypeForNavigation<EditPatientCharacteristicPage>("CharacteristicPage");
            Container.RegisterTypeForNavigation<GuidelinePage>();
            Container.RegisterTypeForNavigation<GuidelinesNavigationPage>("GNav");
        }
    }
}
