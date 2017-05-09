using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using FFImageLoading.Forms.Droid;

namespace Commservus_Mobile.Droid
{
    [Activity(Label = "Commservus", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            CachedImageRenderer.Init();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            if (App.PAGE == "LOGIN")
            {
                App.Current.MainPage = new Commservus_Mobile.MainPage();
                return;
            }

            if(App.PAGE == "SIGNUP")
            {
                App.Current.MainPage = new Commservus_Mobile.MainPage();
                return;
            }

            if(App.PAGE == "FORGOT")
            {
                App.Current.MainPage = new Commservus_Mobile.LoginPage();
                return;
            }

            if(App.PAGE == "MEPAGE")
            {
                base.OnBackPressed();
                return;
            }

            base.OnBackPressed();
        }
    }
}

