using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
namespace Commservus_Mobile
{
    class Event : ContentPage
    {
        public Event()
        {

            Label nameLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                FontAttributes = FontAttributes.Bold,
                Text = "Dodgeball Tournament",
                HorizontalOptions = LayoutOptions.Center
            };

            Label usernameLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Text = "Acton Boxborough Regional High School",
                HorizontalOptions = LayoutOptions.Center
            };

            Label organizationLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                Text = "5:30PM - 6:30PM (EST) on May 18, 2017",
                HorizontalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold,
            };

            Label descriptionLabel = new Label
            {
                FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)),
                Text = "We are hosting a dodgeball tournament and need people to help organize it! We would love kids in grades 9-12",
                HorizontalOptions = LayoutOptions.Center,
                HorizontalTextAlignment = TextAlignment.Center
            };



            Button joinButton = new Button
            {
                BackgroundColor = Color.FromHex("#3498DB"),
                BorderRadius = 0,
                TextColor = Color.White,
                Text = "Apply to Event",
                HeightRequest = 50,
            };

            StackLayout headerLayout = new StackLayout
            {
                Padding = 10,

                Children =
                        {
                            nameLabel,
                            usernameLabel,
                                                        organizationLabel,
                            descriptionLabel,
                            joinButton,
                }
            };

            ObservableCollection<Group> groupedItems = new ObservableCollection<Group>();
            Group group2 = new Group("Similar Events", "DS");
            foreach (var item in api.APIHandler.myFeed.events)
            {
                var iItem = new ItemCell() { BindingContext = new { FirstName = "John Doe", Mobile = "Xamarin" } };
                group2.Add(iItem);
            }

            try
            {
                var testItem = new ItemCell();
                testItem.labelName.Text = "Test abc";
                group2.Add(testItem);
                group2.Add(testItem);
                group2.Add(testItem);

            }
            catch (Exception ex)
            {

            }

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



            StackLayout testLayout = new StackLayout
            {

                Children =
                        {
                    headerLayout,
                    listview
                        }
            };


            //Grid grid = new Grid()
            //{
            //    Children =
            //    {
            //        nameLabel,
            //        usernameLabel,
            //        //buttonLayout
            //    },

            //    Padding = 10,
            //    RowSpacing = 10,
            //    ColumnSpacing = 10,
            //    //ColumnDefinitions = { new ColumnDefinition { Width = new GridLength(.5, GridUnitType.Star) }, new ColumnDefinition { Width = new GridLength(.1, GridUnitType.Star) }, new ColumnDefinition { Width = new GridLength(.4, GridUnitType.Star) } },
            //    //RowDefinitions = { new RowDefinition { Height = 50 }, new RowDefinition { Height = 50 } }

            //};


            ScrollView scrollView = new ScrollView()
            {
                Content = testLayout
            };


            Content = scrollView;
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

        public static List<api.APIHandler.Event> FAKE_STUFF = new List<api.APIHandler.Event>
        {
            new api.APIHandler.Event
            {
                NAME = "Community Service Awards Nights",
                START_TIME = "5:30PM - 6:30PM (EST) on May 18, 2017",
                ORGANIZATION_NAME = "Danny's Place"
            },
            new api.APIHandler.Event
            {
                NAME = "Food Pantry Visit",
                START_TIME = "5:30PM - 6:30PM (EST) on May 18, 2017",
                ORGANIZATION_NAME = "Acton Boxborough Regional High School"
            },
            new api.APIHandler.Event
            {
                NAME = "Big Huge Bake Sale",
                START_TIME = "5:30PM - 6:30PM (EST) on May 18, 2017",
                                ORGANIZATION_NAME = "Frostbyte"
            },
            new api.APIHandler.Event
            {
                NAME = "School Cleanup",
                START_TIME = "5:30PM - 6:30PM (EST) on May 18, 2017",
                                                ORGANIZATION_NAME = "Frostbyte"
            },
            new api.APIHandler.Event
            {
                NAME = "Big Huge Bake Sale",
                START_TIME = "5:30PM - 6:30PM (EST) on May 18, 2017"
            },
            new api.APIHandler.Event
            {
                NAME = "Big Huge Bake Sale",
                START_TIME = "5:30PM - 6:30PM (EST) on May 18, 2017"
            }
        };

        public static int number = 0;
        public static void change(ItemCell itemCell)
        {
            itemCell.labelThird.IsVisible = true;

            itemCell.labelName.BindingContext = new { Name = FAKE_STUFF[number].NAME };
            itemCell.labelDescription.BindingContext = new { Name = FAKE_STUFF[number].START_TIME };
            itemCell.labelThird.BindingContext = new { Name = FAKE_STUFF[number].ORGANIZATION_NAME };
            itemCell.type = 0;
            number++;
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
                labelName.BindingContext = new { Name = "John Doe" };
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

                Event.change(this);
            }

            protected override void OnTapped()
            {
                base.OnTapped();

                if (real)
                {
                    if (type == 0)
                    {
                        labelName.BindingContext = new { Name = organization.DATE_CREATED };
                    }
                    else if (type == 1)
                    {
                        labelName.BindingContext = new { Name = _event.LOCATION };
                    }
                }
            }
        }
    }
}
