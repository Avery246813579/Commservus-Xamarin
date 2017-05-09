using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Commservus_Mobile
{
    public partial class LoginPage : ContentPage
    {
        private AbsoluteLayout layout;
        private Label statusLabel;
        private Entry user, pass;

        public LoginPage()
        {
            /**
             * Background Manipulations 
             **/
            BackgroundColor = Color.White;
            layout = new AbsoluteLayout();
            double width = Application.Current.MainPage.Width;



            /**
             * Status Page 
             **/
            statusLabel = new Label
            {
                Text = "",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Util.GREY,
                Font = Font.SystemFontOfSize(18)
            };

            AbsoluteLayout.SetLayoutFlags(statusLabel, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(statusLabel, new Rectangle(.5f, .3f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));



            /**
             * Username Entry 
             **/
            user = new Entry
            {
                Placeholder = "Username",
                WidthRequest = width - 20,
            };

            AbsoluteLayout.SetLayoutFlags(user, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(user, new Rectangle(0, .4f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));



            /**
             * Password Entry 
             **/
            pass = new Entry
            {
                IsPassword = true,
                Placeholder = "Password",
                WidthRequest = width - 20,
            };

            AbsoluteLayout.SetLayoutFlags(pass, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(pass, new Rectangle(0, .5f, WidthRequest, AbsoluteLayout.AutoSize));



            /**
             * Forgot Password Label 
             **/
            Label forgotPassLbl = new Label
            {
                Text = "Forgot password? Click here.",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Util.GREY,
                Font = Font.SystemFontOfSize(18)
            };

            var forgotPassTapGestureRecognizer = new TapGestureRecognizer();
            forgotPassTapGestureRecognizer.Tapped += (s, e) =>
            {
                App.PAGE = "FORGOT";
                App.Current.MainPage = new ForgotPass();
            };

            forgotPassLbl.GestureRecognizers.Add(forgotPassTapGestureRecognizer);

            AbsoluteLayout.SetLayoutFlags(forgotPassLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(forgotPassLbl, new Rectangle(.5f, .55f, layout.Width, AbsoluteLayout.AutoSize));



            /**
             * Login Button 
             **/
            BoxView login = new BoxView
            {
                BackgroundColor = Util.GREEN,

            };

            var loginTapGestureRecognizer = new TapGestureRecognizer();
            loginTapGestureRecognizer.Tapped += (s, e) =>
            {
                this.login();
            };

            Label loginLbl = new Label
            {
                Text = "Login",
                TextColor = Color.White,
                FontSize = 36,
            };

            login.GestureRecognizers.Add(loginTapGestureRecognizer);
            loginLbl.GestureRecognizers.Add(loginTapGestureRecognizer);
            AbsoluteLayout.SetLayoutFlags(login, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(login, new Rectangle(0f, .7f, 1f, .1f));
            AbsoluteLayout.SetLayoutFlags(loginLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(loginLbl, new Rectangle(.5f, .7f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));



            /**
             * Signup Label 
             **/
            Label signUpLbl = new Label
            {
                Text = "Don't have an account? Sign up here.",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Util.GREY,
                Font = Font.SystemFontOfSize(18)
            };

            var signUpTapGestureRecognizer = new TapGestureRecognizer();
            signUpTapGestureRecognizer.Tapped += (s, e) =>
            {
                App.Current.MainPage = new SignUpPage();
            };

            signUpLbl.GestureRecognizers.Add(signUpTapGestureRecognizer);

            AbsoluteLayout.SetLayoutFlags(signUpLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(signUpLbl, new Rectangle(.5f, .76f, layout.Width, AbsoluteLayout.AutoSize));



            /**
             * Component Loading 
             **/
            layout = new AbsoluteLayout
            {
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),

                Children =
                    {
                        user,
                        pass,
                        forgotPassLbl,
                        login,
                        loginLbl,
                        signUpLbl,
                        statusLabel
                    }
            };

            user.Completed += (sender, e) =>
            {
                pass.Focus();
            };

            pass.Completed += (sender, e) =>
            {
                this.login();
            };

            this.Content = layout;
        }

        public void login()
        {
            //statusLabel.Text = "Login Attempt";

            Util.Response response = Util.request(new Util.Request
            {
                TARGET = "login",
                METHOD = RestSharp.Method.POST,
                DATA = new
                {
                    USERNAME = user.Text,
                    PASSWORD = pass.Text
                }
            });

            if (response.success == "True")
            {
                statusLabel.Text = "Logged in:";
                foreach (var key in response.headers)
                {
                    if (key.ToString().Contains("Set-Cookie"))
                    {
                        var cookies = Util.parseCookies(key.Value.ToString());

                        Application.Current.Properties["CID"] = cookies["CID"].VALUE;
                        Application.Current.Properties["SID"] = cookies["SID"].VALUE;
                        Application.Current.SavePropertiesAsync();

                        App.PAGE = "MEPAGE";
                        App.Current.MainPage = new Commservus_Mobile.MePage_();
                    }
                }
            }
            else
            {
                statusLabel.Text = response.raw;
            }
        }
    }
}