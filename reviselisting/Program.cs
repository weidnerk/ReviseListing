using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviselisting
{
    class Program
    {
        static void Main(string[] args)
        {
            //ReviseFixedPriceItem("223040988437", 99.95);

            EndFixedPriceItem("223040988359");
            EndFixedPriceItem("223040988437");
        }

        private static void ReviseFixedPriceItem(string itemID, double price)
        {

            //create the context
            ApiContext context = new ApiContext();

            //set the User token
            string token = AppSettingsHelper.Token;
            context.ApiCredential.eBayToken = token;

            //set the server url
            string endpoint = AppSettingsHelper.Endpoint;
            context.SoapApiServerUrl = endpoint;

            //enable logging
            context.ApiLogManager = new ApiLogManager();
            context.ApiLogManager.ApiLoggerList.Add(new FileLogger("log.txt", true, true, true));
            context.ApiLogManager.EnableLogging = true;

            //set the version
            context.Version = "817";
            context.Site = SiteCodeType.US;

            ReviseFixedPriceItemCall reviseFP = new ReviseFixedPriceItemCall(context);

            ItemType item = new ItemType();
            item.ItemID = itemID;

            //Basic (Title revision)
            item.StartPrice = new AmountType
            {
                Value = price,
                currencyID = CurrencyCodeType.USD
            };

            reviseFP.Item = item;

            reviseFP.Execute();
            Console.WriteLine(reviseFP.ApiResponse.Ack + " Revised ItemID " + reviseFP.ItemID);
        }

        private static void EndFixedPriceItem(string itemID)
        {

            //create the context
            ApiContext context = new ApiContext();

            //set the User token
            string token = AppSettingsHelper.Token;
            context.ApiCredential.eBayToken = token;

            //set the server url
            string endpoint = AppSettingsHelper.Endpoint;
            context.SoapApiServerUrl = endpoint;

            //enable logging
            context.ApiLogManager = new ApiLogManager();
            context.ApiLogManager.ApiLoggerList.Add(new FileLogger("log.txt", true, true, true));
            context.ApiLogManager.EnableLogging = true;

            //set the version
            context.Version = "817";
            context.Site = SiteCodeType.US;

            EndFixedPriceItemCall endFP = new EndFixedPriceItemCall(context);

            endFP.ItemID = itemID;
            endFP.EndingReason = EndReasonCodeType.NotAvailable;

            endFP.Execute();
            Console.WriteLine(endFP.ApiResponse.Ack + " Ended ItemID " + endFP.ItemID);

        }
    }
}
