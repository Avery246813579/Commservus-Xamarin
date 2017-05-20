using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Commservus_Mobile.organization
{
    class CreateOrganization : ContentPage
    {
        Entry NAME, USERNAME;
        Picker TYPE;
        Editor DESCRIPTION;

        public CreateOrganization()
        {
            Title = "Create Organization";

            Content = new StackLayout()
            {
                Padding = 10,
                Children =
                    {
                    (NAME = new Entry { Placeholder = "Placeholder Name" }),
                    (USERNAME = new Entry { Placeholder = "Placeholder Username"}),
                    (TYPE = new Picker
                    {
                        Title = "Organization Type",
                        Items = {
                            "Organization",
                            "School"
                        }
                    }),
                    new Label
                    {
                        Text = "Organization Description"
                    },
                    (DESCRIPTION = new Editor
                        {
                            HeightRequest = 100,
                            Text = ""
                        }),
                    new Button
                    {
                        BackgroundColor = Color.FromHex("#3498DB"),
                        BorderRadius = 0,
                        TextColor = Color.White,
                        Text = "Join",
                        HeightRequest = 50,
                        Command = new Command((obj) =>
                            {
                                Util.Response vResponse = Util.request(new Util.Request
                                {
                                    METHOD = Method.POST,
                                    TARGET = "organization",
                                    DATA = new { NAME = NAME.Text, DESCRIPTION = DESCRIPTION.Text, TYPE = TYPE.SelectedIndex, USERNAME = USERNAME.Text }
                                });

                                if (vResponse.success.ToLower() == "true")
                                {
                                    OrgS response = JsonConvert.DeserializeObject<OrgS>(vResponse.raw);

                                    DisplayAlert("Organization Create", "" + response.data.id, "Nice!");
                                }
                                else
                                {
                                    DisplayAlert("Organization NOT CREATED", vResponse.raw, "!");

                                }
                            })

                    }
                }
            };
        }

        public struct OrgS
        {
            public Org data { get; set; }
        }

        public struct Org
        {
            public int id { get; set; }
        }

        public class EntryCellPassword : ViewCell
        {
            public EntryCellPassword(string strLabel, string strPlaceholder)
            {
                Label label = new Label { Text = strLabel };
                Entry entry = new Entry
                {
                    Placeholder = strPlaceholder,
                    IsPassword = true,
                    HorizontalOptions = LayoutOptions.Fill,
                    VerticalOptions = LayoutOptions.Center
                };

                this.View = new StackLayout
                {
                    Padding = new Thickness(10, 20, 10, -10),
                    Orientation = StackOrientation.Horizontal,
                    Children = { label, entry }
                };
            }
        }
    }
}
