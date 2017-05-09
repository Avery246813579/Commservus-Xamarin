using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Commservus_Mobile
{
    public partial class HomePage : ContentPage //change the icon to drop and such to just remove the one child
    {
        private Layout layout;
        private Footer footer;
        private static INavigation navigation;

        public HomePage()
        {
            Title = "Your Feed";
            navigation = Navigation;

            ToolbarItem save = new ToolbarItem()
            {
                Text = "Create",
                Command = new Command(async (obj) =>
                {
                    var action = await DisplayActionSheet("What do you want to create?", "Cancel", null, "Organization", "Event");

                    switch (action)
                    {
                        case "Cancel":
                            break;
                        case "Organization":
                            await DisplayAlert("Boop", "Organization", "Beep");
                            break;
                        case "Event":
                            await DisplayAlert("Boop", "Event", "Beep");
                            break;
                    }

                })
            };

            ToolbarItems.Add(save);


            footer = new Footer();
            ObservableCollection<Group> groupedItems = new ObservableCollection<Group>();
            Group group = new Group("Organizations", "SD");
            foreach (var item in api.APIHandler.myFeed.organizations)
            {
                var iItem = new ItemCell();
                iItem.Client.FirstName = item.NAME;
                iItem.Client.Mobile = item.USERNAME;
                group.Add(iItem);
            }

            Group group2 = new Group("Events", "DS");
            foreach (var item in api.APIHandler.myFeed.events)
            {
                var iItem = new ItemCell() { BindingContext = new { FirstName = "John Doe", Mobile = "Xamarin" } };
                iItem.Client.FirstName = item.NAME;
                iItem.Client.Mobile = item.LOCATION;
                group2.Add(iItem);
            }

            try
            {
                var testItem = new ItemCell();
                testItem.labelName.Text = "Test abc";
                group2.Add(testItem);
            } catch (Exception ex)
            {
                group.Name = ex.Message;
            }

            groupedItems.Add(group);
            groupedItems.Add(group2);

            ListView listview = new ListView()
            {
                HasUnevenRows = true,
                RowHeight = 80,
                IsGroupingEnabled = true,
                GroupDisplayBinding = new Binding("Name"),
                GroupShortNameBinding = new Binding("ShortName"),
                ItemTemplate = new DataTemplate(typeof(ItemCell)),
                GroupHeaderTemplate = new DataTemplate(typeof(HeaderView)),
                ItemsSource = groupedItems
            };

            //layout = new StackLayout
            //{
            //    Children =
            //    {
            //    },

            //    HorizontalOptions = LayoutOptions.CenterAndExpand,
            //    Orientation = StackOrientation.Horizontal,
            //    Spacing = 10,
            //    Padding = 10
            //};

            layout = new StackLayout()
            {
                Children =
                {
                    listview,
                    footer
                }
            };

            this.Content = layout;
        }

        public static bool org = true;
        public static int number = 0;

        public static void change(ItemCell itemCell)
        {
            if (org)
            {
                itemCell.labelName.BindingContext = new { Name = api.APIHandler.myFeed.organizations[number].NAME };
                itemCell.labelDescription.BindingContext = new { Name = api.APIHandler.myFeed.organizations[number].USERNAME };
                itemCell.organization = api.APIHandler.myFeed.organizations[number];
                itemCell.real = true;
                itemCell.type = 0;

                number++;
                if (api.APIHandler.myFeed.organizations.Count <= number)
                {
                    number = 0;
                    org = false;
                }
            }else
            {
                if (api.APIHandler.myFeed.events.Count <= number)
                {
                    itemCell.labelThird.IsVisible = false;
                    return;
                }


                itemCell.labelName.BindingContext = new { Name = api.APIHandler.myFeed.events[number].NAME };
                itemCell.labelDescription.BindingContext = new { Name = api.APIHandler.myFeed.events[number].ORGANIZATION_ID };
                itemCell._event = api.APIHandler.myFeed.events[number];
                itemCell.real = true;
                itemCell.type = 1;
                number++;
            }
        }

        public partial class Group : ObservableCollection<ItemCell>
        {
            public String Name { get; set; }
            public String ShortName { get; private set; }

            public Group(String Name, String ShortName)
            {
                this.Name = Name;
                this.ShortName = ShortName;
            }
        }

        public partial class HeaderView : ViewCell
        {
            public HeaderView()
            {
                this.Height = 50;
                var title = new Label
                {
                    Font = Font.SystemFontOfSize(NamedSize.Small, FontAttributes.Bold),
                    TextColor = Color.FromHex("#3498DB"),
                    VerticalOptions = LayoutOptions.Center
                };

                title.SetBinding(Label.TextProperty, "Name");

                View = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 50,
                    Orientation = StackOrientation.Horizontal,
                    Padding = 8,
                    Children = { title }
                };
            }
        }

        public static void move()
        {
            navigation.PushAsync(new Commservus_Mobile.Organization() { });
        }

        public class Client : INotifyPropertyChanged
        {
            private string _firstName;
            public string FirstName
            {
                get { return _firstName; }
                set
                {
                    _firstName = value;
                    OnPropertyChanged("FirstName");
                }
            }

            private string _mobile;
            public string Mobile
            {
                get { return _mobile; }
                set
                {
                    _mobile = value;
                    OnPropertyChanged("Mobile");
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;
            protected void OnPropertyChanged(string propertyName)
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        public partial class ItemCell : ViewCell
        {
            public String Name { get; set; }
            public Label labelName { get; set; }
            public Label labelDescription { get; set; }
            public Label labelThird { get; set; }
            public api.APIHandler.Organization organization { get; set; }
            public api.APIHandler.Event _event { get; set; }
            public bool real { get; set; }
            public int type { get; set; }


            public static readonly BindableProperty ClientProperty = BindableProperty.Create<ItemCell, Client>(p => p.Client, new Client());

            public Client Client
            {
                get { return (Client)GetValue(ClientProperty); }
                set { SetValue(ClientProperty, value); }
            }

            public ItemCell()
            {

                labelName = new Label()
                {
                    Text = "ASDDSA",
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    TextColor = Color.FromHex("#3498DB"),
                    LineBreakMode = LineBreakMode.NoWrap
                };
                labelDescription = new Label()
                {
                    Text = "Test Description bud",
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    LineBreakMode = LineBreakMode.WordWrap
                };

                labelThird = new Label()
                {
                    Text = "Test Description bud",
                    FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                    LineBreakMode = LineBreakMode.WordWrap
                };

                labelName.SetBinding(Label.TextProperty, "Name");
                labelName.BindingContext = new { Name = "John Doe"};
                labelDescription.SetBinding(Label.TextProperty, "Name");
                labelDescription.BindingContext = new { Name = "John Doe" };
                labelThird.SetBinding(Label.TextProperty, "Name");
                labelThird.BindingContext = new { Name = "ASDSAD" };

                StackLayout text = new StackLayout()
                {
                    Padding = 8,
                    Spacing = 4,
                    VerticalOptions = LayoutOptions.Center,
                    Children =
                    {
                        labelName,
                        labelDescription,
                        labelThird
                    }
                };

                Image image = new Image()
                {
                    VerticalOptions = LayoutOptions.Center,
                    Aspect = Aspect.AspectFill,
                    WidthRequest = 55,
                    HeightRequest = 55,
                    Source = "https://s3.amazonaws.com/frostbyte/1.png"
                };

                Grid grid = new Grid()
                {
                    Padding = new Thickness(10, 5),

                    Children =
                    {
                        //image,
                        text
                    }
                };

                this.View = grid;
            }


            protected override void OnAppearing()
            {
                base.OnAppearing();

                HomePage.change(this);
            }

            protected override void OnTapped()
            {
                base.OnTapped();

                if(real)
                {
                    move();

                    if (type == 0)
                    {
                        labelName.BindingContext = new { Name = organization.DATE_CREATED };
                    }else if(type == 1)
                    {
                        labelName.BindingContext = new { Name = _event.LOCATION };
                    }
                }
            }
        }
    }
}