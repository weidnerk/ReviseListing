using dsmodels;
using reviselisting.com.ebay.developer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace reviselisting
{
    public class ebayAPIs
    {
        //public static async Task<Listing> GetSingleItem(string itemId, string appid)
        //{
        //    StringReader sr;
        //    string output;
        //    try
        //    {
        //        DataModelsDB db = new DataModelsDB();

        //        //CustomShoppingService service = new CustomShoppingService();
        //        //service.Url = "http://open.api.ebay.com/shopping";
        //        //service.appID = profile.AppID;
        //        //var request = new GetSingleItemRequestType();
        //        //request.ItemID = itemId;
        //        //var response = service.GetSingleItem(request);
        //        //return response;

        //        Shopping svc = new Shopping();
        //        // set the URL and it's parameters
        //        // Note: Since this is a demo appid, it is very critical to replace the appid with yours to ensure the proper servicing of your application.
        //        svc.Url = string.Format("http://open.api.ebay.com/shopping?callname=GetSingleItem&IncludeSelector=Details,Description,ItemSpecifics&appid={0}&version=515&ItemID={1}", appid, itemId);
        //        // create a new request type
        //        GetSingleItemRequestType request = new GetSingleItemRequestType();
        //        // put in your own item number
        //        //request.ItemID = itemId;
        //        // we will request Details
        //        // for IncludeSelector reference see
        //        // http://developer.ebay.com/DevZone/shopping/docs/CallRef/GetSingleItem.html#detailControls
        //        //request.IncludeSelector = "Details";
        //        //request.IncludeSelector = "Details,Description,TextDescription";
        //        // create a new response type
        //        GetSingleItemResponseType response = new GetSingleItemResponseType();

        //        string uri = svc.Url;
        //        using (HttpClient httpClient = new HttpClient())
        //        {
        //            string s = await httpClient.GetStringAsync(uri);
        //            s = s.Replace("\"", "'");
        //            output = s.Replace(" xmlns='urn:ebay:apis:eBLBaseComponents'", string.Empty);

        //            XElement root = XElement.Parse(output);
        //            var qryRecords = from record in root.Elements("Item")
        //                             select record;
        //            var r = (from r2 in qryRecords
        //                     select new
        //                     {
        //                         Description = r2.Element("Description"),
        //                         Title = r2.Element("Title"),
        //                         Price = r2.Element("ConvertedCurrentPrice"),
        //                         ListingUrl = r2.Element("ViewItemURLForNaturalSearch"),
        //                         PrimaryCategoryID = r2.Element("PrimaryCategoryID"),
        //                         PrimaryCategoryName = r2.Element("PrimaryCategoryName"),
        //                         Quantity = r2.Element("Quantity"),
        //                         QuantitySold = r2.Element("QuantitySold")
        //                     }).Single();

        //            var list = qryRecords.Elements("PictureURL")
        //                   .Select(element => element.Value)
        //                   .ToArray();

        //            var si = new Listing();
        //            //si.PictureUrl = dsutil.ListToDelimited(list, ';');
        //            si.Title = r.Title.Value;
        //            si.Description = r.Description.Value;
        //            si.SupplierPrice = Convert.ToDecimal(r.Price.Value);
        //            si.EbayUrl = r.ListingUrl.Value;
        //            si.PrimaryCategoryID = r.PrimaryCategoryID.Value;
        //            si.PrimaryCategoryName = r.PrimaryCategoryName.Value;
        //            si.Qty = Convert.ToInt32(r.Quantity.Value) - Convert.ToInt32(r.QuantitySold.Value);
        //            return si;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}

    }
}
