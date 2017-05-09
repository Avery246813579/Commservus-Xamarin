using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Commservus_Mobile
{

    public class ContactItem : AbsoluteLayout
    {
        private Label tempLogo, name, permGroup;
        private TapGestureRecognizer contactTap;
        private TapGestureRecognizer permGroupTap;

        public ContactItem()
        {
            //BackgroundColor = Utilities.GREEN;

            BoxView inner = new BoxView
            {
                BackgroundColor = Color.White,
            };
            AbsoluteLayout.SetLayoutFlags(inner, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(inner,
                new Rectangle(.01f, .01f, 1f, .98f));


            this.contactTap = new TapGestureRecognizer();
            this.contactTap.Tapped += (s, e) => {
                //App.Current.MainPage = new PCT_Xamarin.UserAccount(this.associatedUser);
            };
            this.permGroupTap = new TapGestureRecognizer();
            this.permGroupTap.Tapped += (s, e) => {
                //App.Current.MainPage = ***PERM GROUP PAGE***
            };

            RoundedBoxView tempPic = new RoundedBoxView
            {
                BackgroundColor = Util.GREY,
                CornerRadius = 35 / 2,
            };
            AbsoluteLayout.SetLayoutFlags(tempPic, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(tempPic,
                new Rectangle(.05f, 0f, 35, 35));
            tempPic.GestureRecognizers.Add(this.contactTap);

            string nameString =  "Test Name";
            if (nameString.Length > 22)
                nameString = nameString.Substring(0, 22) + "...";


            this.name = new Label
            {
                Text = nameString,
                FontSize = 18,
                HorizontalTextAlignment = TextAlignment.Start,
                TextColor = Util.GREEN,
                WidthRequest = 250,
            };
            AbsoluteLayout.SetLayoutFlags(this.name, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(this.name, new Rectangle(.5f, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            this.name.GestureRecognizers.Add(this.contactTap);

            this.permGroup = new Label
            {
                Text = "Dog",
                TextColor = Util.GREY,
                FontSize = 14,
                HorizontalTextAlignment = TextAlignment.Center,
            };
            AbsoluteLayout.SetLayoutFlags(this.permGroup, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(this.permGroup, new Rectangle(.9f, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            this.permGroup.GestureRecognizers.Add(this.permGroupTap);

            this.Children.Add(inner);
            this.Children.Add(tempPic);
            this.Children.Add(this.name);
            this.Children.Add(this.permGroup);
        }
    }
}