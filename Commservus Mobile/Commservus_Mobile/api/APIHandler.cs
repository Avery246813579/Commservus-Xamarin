using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commservus_Mobile.api
{
    public class APIHandler
    {
        public static Account myAccount;
        public static FeedResponse myFeed = new FeedResponse
        {
            events = new List<Event>
            {
                new Event
                {
                    NAME = "Project Night",
                    ORGANIZATION_NAME = "Danny's Place",
                    START_TIME = "5:30PM - 6:30PM (EST) on May 18, 2017"
                },
                new Event
                {
                    NAME = "Dodgeball Tournament",
                    ORGANIZATION_NAME = "Acton Boxborough High School",
                    START_TIME = "5:30PM - 6:30PM (EST) on May 18, 2017"
                }
            },
            organizations = new List<Organization>
            {
                 new Organization
                 {
                     NAME = "Danny's Place",
                     USERNAME = "DPlace"
                 },
                 new Organization
                 {
                     NAME = "Acton Boxborough High School",
                     USERNAME = "ABRHS"
                 },
                 new Organization
                 {
                     NAME = "Frostbyte",
                     USERNAME = "FBYTE"
                 }
            }
        };

        public static void loadFeed()
        {
            //Util.Response vResponse = Util.request(new Util.Request
            //{
            //    METHOD = Method.GET,
            //    TARGET = "feed",
            //    DATA = new { }
            //});

            //if (vResponse.success.ToLower() == "true")
            //{

            //    api.APIHandler.FeedResponse response = JsonConvert.DeserializeObject<FeedResponse>(vResponse.raw);
            //    api.APIHandler.myFeed = response;
            //}
        }

        public static bool updateAccount(String display, String bio)
        {
            Util.Response vResponse = Util.request(new Util.Request
            {
                METHOD = Method.PATCH,
                TARGET = "account",
                DATA = new { DISPLAY_NAME = display, BIO = bio }
            });

            if (vResponse.success.ToLower() == "true")
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public struct ValidResponse
        {
            public Account data { get; set; }
            public String success { get; set; }
            public int code { get; set; }
            public String message { get; set; }
        }

        public struct FeedResponse
        {
            public String success { get; set; }
            public int code { get; set; }
            public String message { get; set; }
            public List<Event> events { get; set; }
            public List<Organization> organizations { get; set; }
        }

        public struct Account
        {
            public int ID { get; set; }
            public String USERNAME { get; set; }
            public String EMAIL { get; set; }
            public String LOGO { get; set; }
            public String BIO { get; set; }
            public String DISPLAY_NAME { get; set; }
            public String DATE_CREATED { get; set; }
            public String DATE_UPDATED { get; set; }
            public Boolean CONFIRMED { get; set; }
        }

        public struct Event
        {
            public int ID { get; set; }
            public String CANCELLED_DATE { get; set; }
            public int ACCOUNT_ID { get; set; }
            public String DESCRIPTION { get; set; }
            public String END_TIME { get; set; }
            public String LOCATION { get; set; }
            public String NAME { get; set; }
            public int ORGANIZATION_ID { get; set; }
            public String START_TIME { get; set; }
            public String TAGS { get; set; }
            public String ORGANIZATION_NAME { get; set; }
            public int TYPE { get; set; }
        }

        public struct Organization
        {
            public int ACCOUNT_ID { get; set; }
            public String DATE_CREATED { get; set; }
            public String DESCRIPTION { get; set; }
            public int ID { get; set; }
            public String NAME { get; set; }
            public int TYPE { get; set; }
            public String USERNAME { get; set; }
        }
    }
}
