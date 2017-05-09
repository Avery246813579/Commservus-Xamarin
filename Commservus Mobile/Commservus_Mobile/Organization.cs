using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
namespace Commservus_Mobile
{
    class Organization : ContentPage
    {
        public Organization()
        {

            Label newLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = "Organization",
                HorizontalOptions = LayoutOptions.Center
            };

            Button joinButton = new Button
            {
                BackgroundColor = Color.FromHex("#3498DB"),
                BorderRadius = 0,
                TextColor = Color.White,
                Text = "Join",
                HeightRequest = 50,
            };

            StackLayout buttonLayout = new StackLayout
            {
                HeightRequest = 50,

                Children =
                        {
                            joinButton
                        }
            };


            Grid grid = new Grid()
            {
                Children =
                {
                    buttonLayout,
                    newLabel,
                    
                },

                Padding = 10,
                RowSpacing = 10,
                ColumnSpacing = 10,
                ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) }, new ColumnDefinition { Width = new GridLength(.1, GridUnitType.Star) }, new ColumnDefinition { Width = new GridLength(.4, GridUnitType.Star) } },
                RowDefinitions = { new RowDefinition { Height = 50 }, new RowDefinition { Height = 50 } }
               
            };

            Grid.SetRow(newLabel, 0);
            Grid.SetColumnSpan(newLabel, 3);

            Grid.SetRow(buttonLayout, 1);

            ScrollView scrollView = new ScrollView()
            {
                Content = grid
            };


            Content = scrollView;
        }
    }
}
