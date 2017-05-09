using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PCLStorage;
using System.IO;
using FFImageLoading.Forms;

namespace Commservus_Mobile
{
    public partial class AccountPage : ContentPage
    {
        EntryCell DISPLAY_NAME, BIO, EMAIL;
        EntryCellPassword PASSWORD, CONFIRM, CURRENT;

        public AccountPage()
        {
            Title = "My Account";

            ToolbarItem save = new ToolbarItem()
            {
                Text = "Save",
                Command = new Command((obj) =>
                {
                    if(api.APIHandler.updateAccount(DISPLAY_NAME.Text, BIO.Text))
                    {
                        api.APIHandler.myAccount.BIO = BIO.Text;
                        api.APIHandler.myAccount.DISPLAY_NAME = DISPLAY_NAME.Text;

                        DisplayAlert("Information Updated", "Your information has been updated", "Nice!");
                    }else
                    {
                        DisplayAlert("Information Updated Error", "Your information was not updated for some reason", "Not Nice :(");
                    }
                })
            };

            ToolbarItems.Add(save);

            Content = new TableView
            {
                Root = new TableRoot() {
                    new TableSection ("Information") {
                        (DISPLAY_NAME = new EntryCell {Label = "Display Name", Text = api.APIHandler.myAccount.DISPLAY_NAME}),
                        (BIO = new EntryCell {Label = "Bio", Text = api.APIHandler.myAccount.BIO})
                    },
                    new TableSection ("Email") {
                        (EMAIL = new EntryCell {Label = "Email", Text = api.APIHandler.myAccount.EMAIL}),
                    },
                    new TableSection ("Password") {
                        (PASSWORD = new EntryCellPassword ( "Password", "Password" )),
                        (CONFIRM = new EntryCellPassword ( "Confirm Password", "Password" )),
                        (CURRENT = new EntryCellPassword ( "Current Password", "Password" ))
                    }
                },

                HasUnevenRows = true,
                Intent = TableIntent.Form
            };
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



///**
// * Profile Picture 
// **/
//var source = "https://s3.amazonaws.com/frostbyte/1.png";

//            if (api.APIHandler.myAccount.LOGO != "")
//            {
//                source = "https://s3.amazonaws.com/frostbyte/" + api.APIHandler.myAccount.LOGO;
//            }

//            var beachImage = new CachedImage()
//            {
//                HorizontalOptions = LayoutOptions.Center,
//                VerticalOptions = LayoutOptions.Center,
//                WidthRequest = 100,
//                HeightRequest = 100,
//                CacheDuration = TimeSpan.FromDays(30),
//                DownsampleToViewSize = true,
//                RetryCount = 0,
//                RetryDelay = 250,
//                TransparencyEnabled = false,
//                LoadingPlaceholder = "loading.png",
//                ErrorPlaceholder = "error.png",
//                Source = source
//            };

//var iconTap = new TapGestureRecognizer();
//iconTap.Tapped += async(s, e) =>
//            {
//                try
//                {

//                    var action = await DisplayActionSheet(null, "Cancel", null, "Take Photo", "Choose existing photo");
//statusLabel.Text = action;

//                    if (action == "Take Photo")
//                    {
//                        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
//                        {
//                            DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
//                            return;
//                        }

//                        var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
//                        {
//                            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
//                            Directory = "Sample",
//                            Name = "test.jpg",
//                            AllowCropping = true,
//                            SaveToAlbum = true
//                        });

//                        if (file == null)
//                            return;

//                        using (var memoryStream = new MemoryStream())
//                        {
//                            file.GetStream().CopyTo(memoryStream);

//byte[] newImage = ImageResizer.ResizeImage(memoryStream.ToArray(), 100, 100);
//beachImage.Source = ImageSource.FromStream(() => new MemoryStream(newImage));

//                            Util.Response response = Util.imageRequest(new Util.Request
//                            {
//                                TARGET = "profilepic",
//                                METHOD = RestSharp.Method.POST,
//                                FILE = newImage
//                            });

//file.Dispose();

//                            if (response.success == "True")
//                            {
//                                statusLabel.Text = response.raw;
//                            }
//                            else
//                            {
//                                statusLabel.Text = response.raw;
//                            }
//                        }
//                    }

//                    if (action == "Choose existing photo")
//                    {
//                        if (!CrossMedia.Current.IsPickPhotoSupported)
//                        {
//                            DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
//                            return;
//                        }

//                        var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
//                        {
//                            PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
//                        });


//                        if (file == null)
//                        {
//                            DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
//                            return;
//                        }

//                        beachImage.Source = ImageSource.FromStream(() =>
//                        {
//                            var stream = file.GetStream();
//                            return stream;
//                        });

//                        try
//                        {
//                            using (var memoryStream = new MemoryStream())
//                            {
//                                file.GetStream().CopyTo(memoryStream);
//file.Dispose();

//                                Util.Response response = Util.imageRequest(new Util.Request
//                                {
//                                    TARGET = "profilepic",
//                                    METHOD = RestSharp.Method.POST,
//                                    FILE = memoryStream.ToArray()
//                                });

//                                if (response.success == "True")
//                                {
//                                    statusLabel.Text = response.raw;
//                                }
//                                else
//                                {
//                                    statusLabel.Text = response.raw;
//                                }
//                            }
//                        }
//                        catch (Exception ex)
//                        {
//                            statusLabel.Text = ex.Message;
//                        }

//                    }
//                }
//                catch (Exception ex)
//                {
//                    statusLabel.Text = ex.Message;
//                }
//            };

//            beachImage.GestureRecognizers.Add(iconTap);

//            AbsoluteLayout.SetLayoutFlags(beachImage, AbsoluteLayoutFlags.PositionProportional);
//            AbsoluteLayout.SetLayoutBounds(beachImage, new Rectangle(.0f, .0f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
