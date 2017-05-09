using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Commservus_Mobile
{
    public partial class SignUpPage : ContentPage
    {
        private AbsoluteLayout layout;
        private Label statusLabel;
        private Entry display, email, user, pass, rePass;

        public SignUpPage()
        {
            /**
             * Background stuff 
             **/
            double width = Application.Current.MainPage.Width;
            BackgroundColor = Color.White;
            layout = new AbsoluteLayout();



            /**
             * Status Label 
             **/
            statusLabel = new Label
            {
                Text = "",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Util.GREY,
                Font = Font.SystemFontOfSize(18)
            };

            AbsoluteLayout.SetLayoutFlags(statusLabel, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(statusLabel, new Rectangle(.5f, .1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));



            /**
             * Data Entryies 
             **/
            display = new Entry
            {
                Placeholder = "Display Name",
                WidthRequest = width,
            };

            AbsoluteLayout.SetLayoutFlags(display, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(display, new Rectangle(0, .2f, layout.Width, AbsoluteLayout.AutoSize));

            email = new Entry
            {
                Placeholder = "Email",
                WidthRequest = width,
            };

            AbsoluteLayout.SetLayoutFlags(email, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(email, new Rectangle(0, .3f, layout.Width, AbsoluteLayout.AutoSize));

            user = new Entry
            {
                Placeholder = "Username",
                WidthRequest = width,
            };

            AbsoluteLayout.SetLayoutFlags(user, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(user, new Rectangle(0, .4f, layout.Width, AbsoluteLayout.AutoSize));

            pass = new Entry
            {
                IsPassword = true,
                Placeholder = "Password",
                WidthRequest = width,
            };

            AbsoluteLayout.SetLayoutFlags(pass, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(pass, new Rectangle(0, .5f, layout.Width, AbsoluteLayout.AutoSize));

            rePass = new Entry
            {
                IsPassword = true,
                Placeholder = "Confirm Password",
                WidthRequest = width,
            };

            AbsoluteLayout.SetLayoutFlags(rePass, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(rePass, new Rectangle(0, .6f, layout.Width, AbsoluteLayout.AutoSize));



            /**
             * Status Label & Button 
             **/
            BoxView signUp = new BoxView
            {
                BackgroundColor = Util.GREEN,
            };

            Label signUpLbl = new Label
            {
                Text = "Sign Up!",
                TextColor = Color.White,
                FontSize = 36,
            };

            AbsoluteLayout.SetLayoutFlags(signUp, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(signUp, new Rectangle(0f, .8f, 1f, .1f));

            var signUpTapGestureRecognizer = new TapGestureRecognizer();
            signUpTapGestureRecognizer.Tapped += (s, e) =>
            {
                this.signup();
            };


            signUp.GestureRecognizers.Add(signUpTapGestureRecognizer);

            AbsoluteLayout.SetLayoutFlags(signUpLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(signUpLbl, new Rectangle(.5f, .8f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            signUpLbl.GestureRecognizers.Add(signUpTapGestureRecognizer);



            /**
             * Login Label 
             **/
            Label loginLbl = new Label
            {
                Text = "Already have an account? Login Here!",
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Util.GREY,
                Font = Font.SystemFontOfSize(18)
            };

            var loginTapGestureRecognizer = new TapGestureRecognizer();
            loginTapGestureRecognizer.Tapped += (s, e) =>
            {
                App.Current.MainPage = new LoginPage();
            };

            loginLbl.GestureRecognizers.Add(loginTapGestureRecognizer);

            AbsoluteLayout.SetLayoutFlags(loginLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(loginLbl, new Rectangle(.5f, .86f, layout.Width, AbsoluteLayout.AutoSize));



            /**
             * Layout 
             **/
            layout = new AbsoluteLayout
            {
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),

                Children =
                    {
                        statusLabel,
                        display,
                        email,
                        user,
                        pass,
                        rePass,
                        signUp,
                        signUpLbl,
                        loginLbl,
                    }
            };

            display.Completed += (sender, e) =>
            {
                email.Focus();
            };

            email.Completed += (sender, e) =>
            {
                user.Focus();
            };

            user.Completed += (sender, e) =>
            {
                pass.Focus();
            };

            pass.Completed += (sender, e) =>
            {
                rePass.Focus();
            };

            rePass.Completed += (sender, e) =>
            {
                this.signup();
            };

            this.Content = layout;
        }

        public void signup()
        {
            statusLabel.Text = "Signup Attempt";

            Util.Response response = Util.request(new Util.Request
            {
                TARGET = "register",
                METHOD = RestSharp.Method.POST,
                DATA = new
                {
                    USERNAME = user.Text,
                    EMAIL = email.Text,
                    PASSWORD = pass.Text,
                    DISPLAY_NAME = display.Text
                }
            });

            if (response.success == "True")
            {
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


//if (!CrossMedia.Current.IsPickPhotoSupported)
//{
//    DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
//    return;
//}

//var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
//{
//    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
//});


//if (file == null)
//{
//    DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
//    return;
//}

//image.Source = ImageSource.FromStream(() =>
//{
//    var stream = file.GetStream();
//    file.Dispose();
//    return stream;
//});

//if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
//{
//    DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
//    return;
//}

//var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
//{
//    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
//    Directory = "Sample",
//    Name = "test.jpg"
//});

//if (file == null)
//    return;

//DisplayAlert("File Location", file.Path, "OK");

//image.Source = ImageSource.FromStream(() =>
//{
//    var stream = file.GetStream();
//    file.Dispose();
//    return stream;
//});
