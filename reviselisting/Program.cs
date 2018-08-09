using dsmodels;
using eBay.Service.Call;
using eBay.Service.Core.Sdk;
using eBay.Service.Core.Soap;
using eBay.Service.Util;
using sclib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reviselisting
{
    class Program
    {
        static DataModelsDB db = new DataModelsDB();
        private static string Log_File = "out_of_stock.txt";

        static void Main(string[] args)
        {
            //ReviseFixedPriceItem("223040988437", 99.95);

            //EndFixedPriceItem("223040988359");
            //EndFixedPriceItem("223040988437");

            Task.Run(async () =>
            {
                await ScanListings();

            }).Wait();
            Console.WriteLine("complete");
           // Console.ReadKey();

        }

        protected static async Task ScanListings()
        {
            string appid = ConfigurationManager.AppSettings["appID"];

            int count = db.PostedListings.Count();
            int i = 0;
            //foreach (PostedListing p in db.PostedListings.Where(r => r.ListedItemID == "223074116525"))
            foreach (PostedListing p in db.PostedListings)
            {
                try
                {
                    string sourcrUrl = p.SourceUrl;
                    var result = await Scrape.GetDetail(sourcrUrl);
                    Console.WriteLine("processing " + (++i) + " of " + count.ToString());
                    if (!string.IsNullOrEmpty(result.availability))
                    {
                        if (result.availability.Contains("Out of stock"))
                        {
                            var myListing = await scrapeAPI.ebayAPIs.GetSingleItem(p.ListedItemID, appid);
                            if (myListing.Qty > 0)
                            {
                                Console.WriteLine("OUT OF STOCK " + p.Title);
                                string reviseResult = scrapeAPI.ebayAPIs.ReviseQty(p.ListedItemID, 0);
                                dsutil.DSUtil.WriteFile(Log_File, p.ListedItemID + " " + p.Title);
                                dsutil.DSUtil.WriteFile(Log_File, p.SourceUrl);
                                dsutil.DSUtil.WriteFile(Log_File, reviseResult);

                                string ret = await dsutil.DSUtil.SendMailProd("kevinw@midfinance.com", "OUT OF STO " + p.Title, "revise listing", "localhost");
                                if (!string.IsNullOrEmpty(ret))
                                {
                                    dsutil.DSUtil.WriteFile(Log_File, "prod email failed: " + ret);
                                    ret = await dsutil.DSUtil.SendMailDev("kevinw@midfinance.com", "OUT OF STO " + p.Title, "revise listing");
                                    if (!string.IsNullOrEmpty(ret))
                                    {
                                        dsutil.DSUtil.WriteFile(Log_File, "dev email failed: " + ret);
                                    }
                                }
                            }
                            else
                            {
                                dsutil.DSUtil.WriteFile(Log_File, p.ListedItemID + " " + p.Title);
                                dsutil.DSUtil.WriteFile(Log_File, p.SourceUrl);
                                dsutil.DSUtil.WriteFile(Log_File, "Listing quantity already set to 0 - no action taken.");
                            }
                            dsutil.DSUtil.WriteFile(Log_File, string.Empty);
                        }
                    }
                }
                catch (Exception exc)
                {
                    dsutil.DSUtil.WriteFile(Log_File, "ERROR: " + p.ListedItemID + " " + exc.Message);
                }
            }
            dsutil.DSUtil.WriteFile(Log_File, string.Format("Processed {0} listings.", count));
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
