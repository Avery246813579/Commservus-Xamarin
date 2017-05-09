using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Commservus_Mobile

{
    public class Footer : AbsoluteLayout
    {

        internal AbsoluteLayout contactBox, groupBox, notifBox, meBox;
        internal Label contactlbl, groupLbl, notifLbl, meLbl;
        Color GREY = Color.FromHex("#686D67");

        public Footer()
        {
            HeightRequest = 50;

            this.BackgroundColor = Color.White;

            //contact
            var contactTap = new TapGestureRecognizer();
            contactTap.Tapped += (s, e) =>
            {
                App.Current.MainPage = App.HOME_PAGE;
            };

            this.contactBox = new AbsoluteLayout
            {
                BackgroundColor = Color.White,//Utilities.GREY,
            };
            AbsoluteLayout.SetLayoutFlags(this.contactBox, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(this.contactBox,
                new Rectangle(0f, 0f, .25f, 1f));
            this.contactBox.GestureRecognizers.Add(contactTap);
            this.contactlbl = new Label
            {
                Text = "Home",
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = GREY,
            };
            AbsoluteLayout.SetLayoutFlags(this.contactlbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(this.contactlbl,
                new Rectangle(.5f, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            this.contactlbl.GestureRecognizers.Add(contactTap);
            this.contactBox.Children.Add(contactlbl);



            //group
            var groupTap = new TapGestureRecognizer();
            groupTap.Tapped += (s, e) =>
            {
                //App.Current.MainPage = new Dog;
            };
            this.groupBox = new AbsoluteLayout
            {
                BackgroundColor = Color.White,//Utilities.GREY,
            };
            AbsoluteLayout.SetLayoutFlags(this.groupBox, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(this.groupBox,
                new Rectangle(0.325f, 0f, .25f, 1f));
            this.groupBox.GestureRecognizers.Add(groupTap);

            this.groupLbl = new Label
            {
                Text = "Search",
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = GREY,
            };
            AbsoluteLayout.SetLayoutFlags(this.groupLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(this.groupLbl,
                new Rectangle(0.5f, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            this.groupLbl.GestureRecognizers.Add(groupTap);
            this.groupBox.Children.Add(groupLbl);

            //alerts
            var notifTap = new TapGestureRecognizer();
            notifTap.Tapped += (s, e) =>
            {
                    //App.Current.MainPage = new PermGroupPage(Utilities.testAccount);
                };
            this.notifBox = new AbsoluteLayout
            {
                BackgroundColor = Color.White,//Utilities.GREY,
            };
            AbsoluteLayout.SetLayoutFlags(this.notifBox, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(this.notifBox,
                new Rectangle(.665f, 0f, .25f, 1f));
            this.notifBox.GestureRecognizers.Add(notifTap);

            this.notifLbl = new Label
            {
                Text = "Alerts",
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = GREY,
            };
            AbsoluteLayout.SetLayoutFlags(this.notifLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(this.notifLbl,
                new Rectangle(.5f, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            this.notifBox.GestureRecognizers.Add(notifTap);
            this.notifBox.Children.Add(notifLbl);


            //me
            var meTap = new TapGestureRecognizer();
            meTap.Tapped += (s, e) =>
            {
                try
                {
                    App.Current.MainPage = App.ME_PAGE;
                }
                catch(Exception ex)
                {
                    notifLbl.FontSize = 7;
                    notifLbl.Text = ex.Message;
                }
            };
            this.meBox = new AbsoluteLayout
            {
                BackgroundColor = Color.White,//Utilities.GREY,
            };
            AbsoluteLayout.SetLayoutFlags(this.meBox, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(this.meBox,
                new Rectangle(1f, 0f, .25f, 1f));
            this.meBox.GestureRecognizers.Add(meTap);

            this.meLbl = new Label
            {
                Text = "Me",
                FontSize = 14,
                FontAttributes = FontAttributes.Bold,
                TextColor = GREY,
            };
            AbsoluteLayout.SetLayoutFlags(this.meLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(this.meLbl,
                new Rectangle(.5f, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            this.meBox.GestureRecognizers.Add(meTap);
            this.meBox.Children.Add(meLbl);


            this.Children.Add(contactBox);
            this.Children.Add(groupBox);
            this.Children.Add(notifBox);
            this.Children.Add(meBox);
        }
    }
}

