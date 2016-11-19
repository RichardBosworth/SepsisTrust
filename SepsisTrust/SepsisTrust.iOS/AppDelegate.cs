using FFImageLoading.Forms.Touch;
using FormsPlugin.Iconize.iOS;
using Foundation;
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Iconize;
using Plugin.Iconize.Fonts;
using Prism.Unity;
using SepsisTrust.iOS.FontIcons;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace SepsisTrust.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register( "AppDelegate" )]
    public class AppDelegate : FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching( UIApplication app, NSDictionary options )
        {
            Forms.Init();
            CurrentPlatform.Init();
            CachedImageRenderer.Init();

            Iconize.With(new FontAwesomeModule())
                   .With(new IoniconsModule())
                   .With(new MedicalFontModule());
            IconControls.Init();


            LoadApplication(new App(new iOSInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class iOSInitializer : IPlatformInitializer
    {
        public void RegisterTypes( IUnityContainer container )
        {
        }
    }
}