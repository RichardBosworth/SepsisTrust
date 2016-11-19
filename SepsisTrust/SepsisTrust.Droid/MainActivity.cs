using Android.App;
using Android.Content.PM;
using Android.OS;
using FFImageLoading.Forms.Droid;
using FormsPlugin.Iconize.Droid;
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Iconize;
using Plugin.Iconize.Fonts;
using Prism.Unity;
using SepsisTrust.Droid.IconFont;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SepsisTrust.Droid
{
    [Activity( Label = "SepsisTrust", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation )]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate( Bundle bundle )
        {
            TabLayoutResource = Resource.Layout.tabs;
            ToolbarResource = Resource.Layout.toolbar;

            CurrentPlatform.Init();
            CachedImageRenderer.Init();

            base.OnCreate(bundle);

            Iconize.With(new FontAwesomeModule())
                   .With(new IoniconsModule())
                   .With(new MedicalFontModule());

            Forms.Init(this, bundle);

            IconControls.Init(Resource.Id.toolbar, Resource.Id.sliding_tabs);

            LoadApplication(new App(new AndroidInitializer()));
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes( IUnityContainer container )
        {
        }
    }
}