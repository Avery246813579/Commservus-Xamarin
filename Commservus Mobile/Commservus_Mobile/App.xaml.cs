using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Commservus_Mobile
{
    public partial class App : Application
    {
        public static String PAGE = "MAIN";
        public static NavigationPage HOME_PAGE;
        public static NavigationPage ME_PAGE;

        public App()
        {
            //InitializeComponent();

            MainPage = new Commservus_Mobile.MainPage();
            if (Commservus_Mobile.MainPage.redirect)
            {
                HOME_PAGE = new NavigationPage(new Commservus_Mobile.HomePage());
                ME_PAGE = new NavigationPage(new Commservus_Mobile.MePage_());
                MainPage = HOME_PAGE;
            }
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

        protected bool OnBackButtonPressed()
        {
            App.Current.MainPage = new Commservus_Mobile.MainPage();

            return true;
        }
    }
}
