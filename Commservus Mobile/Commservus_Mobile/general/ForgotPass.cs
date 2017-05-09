using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Commservus_Mobile
{
    public partial class ForgotPass : ContentPage
    {
        private AbsoluteLayout layout;
        private Label statusLabel;
        private Entry user;

        public ForgotPass()
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
            AbsoluteLayout.SetLayoutBounds(statusLabel, new Rectangle(.5f, .4f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));



            /**
             * Username Entry 
             **/
            user = new Entry
            {
                Placeholder = "Username or Email",
                WidthRequest = width - 20,
            };

            AbsoluteLayout.SetLayoutFlags(user, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(user, new Rectangle(0, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));



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
                this.forgot();
            };

            Label loginLbl = new Label
            {
                Text = "Forgot Password",
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
             * Component Loading 
             **/
            layout = new AbsoluteLayout
            {
                Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5),

                Children =
                    {
                        user,
                        login,
                        loginLbl,
                        statusLabel
                    }
            };

            user.Completed += (sender, e) =>
            {
                this.forgot();
            };

            this.Content = layout;
        }

        public void forgot()
        {
            Util.Response response = Util.request(new Util.Request
            {
                TARGET = "forgot",
                METHOD = RestSharp.Method.POST,
                DATA = new
                {
                    ID = user.Text
                }
            });

            if (response.success == "True")
            {
                statusLabel.Text = "Logged in:";
                foreach (var key in response.headers)
                {
                    if (key.ToString().Contains("Set-Cookie"))
                    {
                        statusLabel.Text = "Instructions sent to your email";
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