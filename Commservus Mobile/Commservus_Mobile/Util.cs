using Newtonsoft.Json;
using PCLStorage;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;

namespace Commservus_Mobile
{
    static class Util
    {
        public static Color GREEN = Color.FromHex("#4DB6AC"), GREY = Color.FromHex("#E0E0E0"), ORANGE = Color.FromHex("#EF6C00"), LIGHTGREY = Color.FromHex("f5f5f5"), BLACK = Color.FromHex("252525");

        internal static List<ContactItem> contacts = new List<ContactItem>
        {
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem(),
            new ContactItem()
        };

        public static Dictionary<string, Cookie> parseCookies(string raw)
        {
            Dictionary<string, Cookie> cookies = new Dictionary<string, Cookie>();
            string[] dSplit = raw.Split(',');

            for (var i = 0; i < dSplit.Length; i++)
            {
                string[] cSplit = dSplit[i].Split(';');
                string[] pSplit = cSplit[0].Split('=');

                cookies.Add(pSplit[0], new Cookie
                {
                    KEY = pSplit[0],
                    VALUE = pSplit[1],
                    RAW = dSplit[i]
                });
            }

            return cookies;
        }

        public static Response request(Request dRequest)
        {
            var client = new RestClient("https://commservus.com/api/");
            var request = new RestRequest(dRequest.TARGET, dRequest.METHOD);

            try
            {
                var message = "";
                foreach (var cookie in Util.getCookies())
                {
                    request.AddParameter(cookie.Key, cookie.Value, ParameterType.Cookie);
                }

                request.AddParameter("application/json", JsonConvert.SerializeObject(dRequest.DATA), ParameterType.RequestBody);
                IRestResponse<Response> response = client.Execute<Response>(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    foreach (Parameter name in response.Headers)

                    {
                        d.Add(name.Name, name.Value);
                    }

                    response.Data.headers = d;
                    response.Data.raw = response.Content;
                    response.Data.message += message;
                    return response.Data;

                }
                else
                {
                    return new Response
                    {
                        success = "False",
                        message = "DS",
                        raw = "SD"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    success = "False",
                    message = ex.Message,
                    raw = "SD"
                };
            }
        }

        public static Response imageRequest(Request dRequest)
        {
            var client = new RestClient("https://commservus.com/api/");
            var request = new RestRequest(dRequest.TARGET, dRequest.METHOD);

            try
            {
                var message = "";
                foreach (var cookie in Util.getCookies())
                {
                    request.AddParameter(cookie.Key, cookie.Value, ParameterType.Cookie);
                }

                request.AddFile("file", dRequest.FILE, "zzzzzzz.png");
                IRestResponse<Response> response = client.Execute<Response>(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    Dictionary<string, object> d = new Dictionary<string, object>();
                    foreach (Parameter name in response.Headers)

                    {
                        d.Add(name.Name, name.Value);
                    }

                    response.Data.headers = d;
                    response.Data.raw = response.Content;
                    response.Data.message += message;
                    return response.Data;

                }
                else
                {
                    return new Response
                    {
                        success = "False",
                        message = "DS",
                        raw = "SD"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Response
                {
                    success = "False",
                    message = ex.Message,
                    raw = "SD"
                };
            }
        }


        public static async Task<byte[]> ReadAllBytesAsync(this IFile file)
        {
            byte[] result = null;
            using (var stream = await file.OpenAsync(FileAccess.Read).ConfigureAwait(false))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    await stream.CopyToAsync(ms);
                    result = ms.ToArray();
                }
            }
            return result;
        }

        public static Dictionary<string, string> getCookies()
        {
            Dictionary<string, string> cookies = new Dictionary<string, string>();

            Application myApp = Application.Current;
            if (myApp.Properties.ContainsKey("SID"))
            {
                cookies.Add("SID", myApp.Properties["SID"].ToString());
            }

            if (myApp.Properties.ContainsKey("CID"))
            {
                cookies.Add("CID", myApp.Properties["CID"].ToString());
            }


            return cookies;
        }

        public static void logout()
        {
            Application myApp = Application.Current;
            if (myApp.Properties.ContainsKey("SID"))
            {
                myApp.Properties.Remove("SID");
            }

            if (myApp.Properties.ContainsKey("CID"))
            {
                myApp.Properties.Remove("CID");
            }

            Application.Current.SavePropertiesAsync();
        }

        public struct Request
        {
            public Method METHOD { get; set; }
            public string TARGET { get; set; }
            public Object DATA { get; set; }
            public byte[] FILE { get; set; }
            public Dictionary<string, string> COOKIES { get; set; }
        }

        public class Response
        {
            public string success { get; set; }
            public string code { get; set; }
            public Dictionary<string, object> headers { get; set; }
            public string message { get; set; }
            public Object data { get; set; }
            public string raw { get; set; }
        }

        public struct Cookie
        {
            public string RAW { get; set; }
            public string KEY { get; set; }
            public string VALUE { get; set; }
        }

        public struct Test
        {
            public string TEST { get; set; }
        }
    }
}
