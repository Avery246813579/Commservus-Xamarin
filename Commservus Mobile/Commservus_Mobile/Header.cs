using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace Commservus_Mobile
{
    public class Header : AbsoluteLayout
    {

        internal Label title;

        public Header(String str)
        {


            BoxView border = new BoxView
            {
                BackgroundColor = Util.GREY,
            };
            AbsoluteLayout.SetLayoutFlags(border, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(border,
                                           new Rectangle(0f, 0f, 1f, 1f));

            BoxView inner = new BoxView
            {
                BackgroundColor = Util.GREEN,//Color.White
            };
            AbsoluteLayout.SetLayoutFlags(inner, AbsoluteLayoutFlags.All);
            AbsoluteLayout.SetLayoutBounds(inner,
                                           new Rectangle(0f, 0f, 1f, .95f));

            this.title = new Label
            {
                Text = str,
                TextColor = Color.White,//Utilities.GREY,
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
            };
            AbsoluteLayout.SetLayoutFlags(title, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(title,
                                           new Rectangle(.5f, .5f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));



            this.Children.Add(border);
            this.Children.Add(inner);
            this.Children.Add(title);





        }

        public void setText(string text)
        {
            title.Text = text;
        }

        public void addText(string text)
        {
            title.Text += text;
        }

    }

}