using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Commservus_Mobile
{
    public partial class MainPage : ContentPage
    {
        private AbsoluteLayout layout; internal static string fontFamily = Device.OnPlatform("MarkerFelt-Thin", "Droid Sans Mono", "Comic Sans MS");
        public static bool redirect = false;

        public MainPage()
        {
            /**
             * Background Manipulations 
             **/
            BackgroundColor = Util.GREY;
            layout = new AbsoluteLayout();



            /**
            * Borders 
            **/
            BoxView leftBorder = new BoxView
            {
                Color = Util.GREEN,
                WidthRequest = 150,
                HeightRequest = 100,
            };
            AbsoluteLayout.SetLayoutFlags(leftBorder, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(leftBorder, new Rectangle(-.1f, 1f, .55f, .15f));

            BoxView rightBorder = new BoxView
            {
                Color = Util.GREEN,
                WidthRequest = 150,
                HeightRequest = 100,
            };

            AbsoluteLayout.SetLayoutFlags(rightBorder, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(rightBorder, new Rectangle(1.1f, 1.01f, .55f, .16f));



            /**
             * Login Button 
             **/
            BoxView loginBox = new BoxView
            {
                Color = Util.GREEN,
                WidthRequest = 150,
                HeightRequest = 100,
            };

            AbsoluteLayout.SetLayoutFlags(loginBox, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(loginBox, new Rectangle(-.1f, 1.01f, .55f, .15f));

            Label loginLbl = new Label
            {
                Text = "Login",
                FontSize = 35,
                FontFamily = fontFamily,
                TextColor = Color.White,
            };

            AbsoluteLayout.SetLayoutFlags(loginLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(loginLbl, new Rectangle(.15f, .95f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            var loginTapGestureRecognizer = new TapGestureRecognizer();
            loginTapGestureRecognizer.Tapped += (s, e) =>
            {
                App.Current.MainPage = new LoginPage();
                App.PAGE = "LOGIN";
            };

            loginBox.GestureRecognizers.Add(loginTapGestureRecognizer);
            loginLbl.GestureRecognizers.Add(loginTapGestureRecognizer);



            /**
             * Signup Button 
             **/
            BoxView signUpBox = new BoxView
            {
                Color = Util.ORANGE,
                WidthRequest = 150,
                HeightRequest = 100,
            };

            AbsoluteLayout.SetLayoutFlags(signUpBox, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(signUpBox, new Rectangle(1.1f, 1.01f, .55f, .15f));

            Label signUpLbl = new Label
            {
                Text = "Sign Up",
                FontSize = 36,
                FontFamily = fontFamily,
                TextColor = Color.White,
            };
            AbsoluteLayout.SetLayoutFlags(signUpLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(signUpLbl, new Rectangle(.85f, .95f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            var signUpTapGestureRecognizer = new TapGestureRecognizer();
            signUpTapGestureRecognizer.Tapped += (s, e) =>
            {
                App.Current.MainPage = new SignUpPage();
                App.PAGE = "SIGNUP";
            };
            signUpBox.GestureRecognizers.Add(signUpTapGestureRecognizer);
            signUpLbl.GestureRecognizers.Add(signUpTapGestureRecognizer);

            /**
             * Compiling Components 
             **/
            layout = new AbsoluteLayout
            {
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),

                Children =
                    {
                        loginBox,
                        loginLbl,
                        signUpBox,
                        signUpLbl
                    }
            };

            this.Content = layout;

            try
            {
                Util.Response vResponse = Util.request(new Util.Request
                {
                    METHOD = Method.GET,
                    TARGET = "valid",
                    DATA = new { }
                });

                if (vResponse.success.ToLower() == "true")
                {
                    signUpLbl.FontSize = 8;
                    signUpLbl.Text = "We logging in";
                    redirect = true;

                    api.APIHandler.ValidResponse response = JsonConvert.DeserializeObject<api.APIHandler.ValidResponse>(vResponse.raw);
                    api.APIHandler.myAccount = response.data;

                    //App.Current.MainPage = new Commservus_Mobile.SignUpPage();
                }
                else
                {
                    //App.Current.MainPage = new Commservus_Mobile.MainPage();
                }

                api.APIHandler.loadFeed();
            }
            catch (Exception ex)
            {
                signUpLbl.FontSize = 8;
                signUpLbl.Text = ex.Message;
            }
        }
    }
}
