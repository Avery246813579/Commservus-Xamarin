using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Commservus_Mobile
{
    public class MePage_ : ContentPage
    {
        private Layout layout;

        public MePage_()
        {
            Title = "Me";
            BackgroundColor = Util.GREY;

            //first section
            var myAccountTap = new TapGestureRecognizer();
            myAccountTap.Tapped += (sender, e) =>
            {
            };
            MePageItem myAccountItem = new MePageItem("Joe Smoe", myAccountTap);
            //myAccountItem.GestureRecognizers.Add(myAccountTap);
            var mySocialGroupTap = new TapGestureRecognizer();
            mySocialGroupTap.Tapped += (sender, e) =>
            {
                try
                {
                    Navigation.PushAsync(new Commservus_Mobile.AccountPage() { });
                }
                catch (Exception ex)
                {
                }
            };

            MePageItem mySocialGroupsItem = new MePageItem("My Test Item", mySocialGroupTap);
            var myDataProfileTap = new TapGestureRecognizer();
            myDataProfileTap.Tapped += (sender, e) =>
            {

                Navigation.PushAsync(new Commservus_Mobile.AccountPage() { });
            };

            mySocialGroupsItem.GestureRecognizers.Add(myDataProfileTap);

            MePageItem myDataProfilesItem = new MePageItem("My Other Test Item", myDataProfileTap);

            StackLayout upperStackLayout = new StackLayout
            {
                Spacing = 5,
                BackgroundColor = Color.Transparent,
                Children = {
                    myAccountItem,
                    mySocialGroupsItem,
                    myDataProfilesItem,
                },
            };
            AbsoluteLayout.SetLayoutFlags(upperStackLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(upperStackLayout,
                                           new Rectangle(0f, 0f, 1f, 1f));

            RoundedBoxView upperBackground = new RoundedBoxView()
            {
                BackgroundColor = Util.LIGHTGREY,
                CornerRadius = 10,
            };
            AbsoluteLayout.SetLayoutFlags(upperBackground, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(upperBackground,
                                           new Rectangle(0f, 0f, 1f, 1f));

            AbsoluteLayout upperLayout = new AbsoluteLayout
            {
                BackgroundColor = Color.Transparent,
                Children = {
                    upperBackground,
                    upperStackLayout,
                },
            };
            AbsoluteLayout.SetLayoutFlags(upperLayout, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(upperLayout,
                                           new Rectangle(.1f, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            //second section
            var upgradeTap = new TapGestureRecognizer();
            upgradeTap.Tapped += (sender, e) =>
            {
                //App.Current.MainPage = new PCT_Xamarin.
            };
            MePageItem upgradeItem = new MePageItem("Upgrade to Premium", upgradeTap);
            var termsTap = new TapGestureRecognizer();
            termsTap.Tapped += (sender, e) =>
            {
                //App.Current.MainPage = new PCT_Xamarin.
            };
            MePageItem termsItem = new MePageItem("Terms & Agreement", termsTap);
            var contactUsTap = new TapGestureRecognizer();
            contactUsTap.Tapped += (sender, e) =>
            {
            };
            MePageItem contactUsItem = new MePageItem("Contact Us", contactUsTap);

            RoundedBoxView lowerBackground = new RoundedBoxView
            {
                BackgroundColor = Util.LIGHTGREY,
                CornerRadius = 10,
            };
            AbsoluteLayout.SetLayoutFlags(lowerBackground, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(lowerBackground,
                                           new Rectangle(0f, 0f, 1f, 1f));

            StackLayout lowerStack = new StackLayout
            {
                BackgroundColor = Color.Transparent,
                Children = {
                    upgradeItem,
                    termsItem,
                    contactUsItem,
                },
            };
            AbsoluteLayout.SetLayoutFlags(lowerStack, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(lowerStack,
                                           new Rectangle(0f, 0f, 1f, 1f));

            AbsoluteLayout lowerLayout = new AbsoluteLayout
            {
                BackgroundColor = Color.Transparent,
                Children = {
                    lowerBackground,
                    lowerStack,
                },
            };

            var logoutTap = new TapGestureRecognizer();
            logoutTap.Tapped += (sender, e) =>
            {
                Util.logout();

                App.PAGE = "MAIN";
                App.Current.MainPage = new Commservus_Mobile.MainPage();
            };


            RoundedBoxView logoutOuter = new RoundedBoxView
            {
                BackgroundColor = Util.LIGHTGREY,
                CornerRadius = 10,
            };

            logoutOuter.GestureRecognizers.Add(logoutTap);

            AbsoluteLayout.SetLayoutFlags(logoutOuter, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(logoutOuter,
                                           new Rectangle(0f, 0f, 1f, 1f));

            RoundedBoxView logoutInner = new RoundedBoxView
            {
                BackgroundColor = Util.LIGHTGREY,
                CornerRadius = 10,
            };
            AbsoluteLayout.SetLayoutFlags(logoutOuter, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(logoutOuter,
                                           new Rectangle(0f, 0f, .99f, .99f));

            logoutInner.GestureRecognizers.Add(logoutTap);

            Label logoutLbl = new Label
            {
                Text = "Log Out",
                FontSize = 16,
                TextColor = Color.Red,
            };
            AbsoluteLayout.SetLayoutFlags(logoutLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(logoutLbl,
                                           new Rectangle(.5f, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));


            logoutLbl.GestureRecognizers.Add(logoutTap);

            AbsoluteLayout logoutLayout = new AbsoluteLayout
            {
                BackgroundColor = Color.Transparent,
                Children = {
                    logoutOuter,
                    logoutLbl,
                }
            };


            StackLayout contentLayout = new StackLayout
            {
                Spacing = 20,
                Children = {
                    upperLayout,
                    lowerLayout,
                    logoutLayout,
                },
            };
            AbsoluteLayout.SetLayoutFlags(contentLayout, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(contentLayout,
                                           new Rectangle(0f, .48f, 1f, .84f));


            //footer
            Footer footer = new Footer();
            footer.meLbl.TextColor = Util.GREEN;
            AbsoluteLayout.SetLayoutFlags(footer, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(footer, new Rectangle(0f, 1f, 1f, .1f));


            this.layout = new AbsoluteLayout
            {
                Children = {
                    contentLayout,
                    footer,
                },
            };

            this.Content = this.layout;
        }
    }
}