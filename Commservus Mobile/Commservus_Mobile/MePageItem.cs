using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Commservus_Mobile
{
    public class MePageItem : AbsoluteLayout
    {
        private RoundedBoxView tempIcon;
        private String itemName;

        public MePageItem(String str, TapGestureRecognizer tap)
        {
            this.itemName = str;
            this.HeightRequest = 50;
            this.WidthRequest = (new Commservus_Mobile.LoginPage()).WidthRequest;

            tempIcon = new RoundedBoxView
            {
                HeightRequest = 30,
                WidthRequest = 30,
                BackgroundColor = Util.GREY,
                CornerRadius = 15,
            };
            AbsoluteLayout.SetLayoutFlags(tempIcon, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(tempIcon,
                                           new Rectangle(.05f, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            Label title = new Label
            {
                Text = this.itemName,
                FontSize = 14,
                TextColor = Util.GREEN,
                HorizontalTextAlignment = TextAlignment.Start,
                WidthRequest = 250,
            };
            AbsoluteLayout.SetLayoutFlags(title, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(title,
                                           new Rectangle(.5f, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            RoundedBoxView viewBtnBackground = new RoundedBoxView
            {
                CornerRadius = 10,
                BackgroundColor = Util.ORANGE,
            };
            AbsoluteLayout.SetLayoutFlags(viewBtnBackground, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(viewBtnBackground,
                                           new Rectangle(.95f, .5f, .15f, .5f));

            Label viewLbl = new Label
            {
                Text = "View",
                TextColor = Util.LIGHTGREY,
                FontSize = 12,
                FontAttributes = FontAttributes.Bold,
            };
            AbsoluteLayout.SetLayoutFlags(viewLbl, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(viewLbl,
                                           new Rectangle(.91, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            viewLbl.GestureRecognizers.Add(tap);



            this.Children.Add(this.tempIcon);
            this.Children.Add(title);
            this.Children.Add(viewBtnBackground);
            this.Children.Add(viewLbl);
        }
    }
}