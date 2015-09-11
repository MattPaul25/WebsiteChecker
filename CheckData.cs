using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Net;

namespace WebsiteChecker
{
    class CheckData
    {
        private string[] columns = { "EntityID", "EntityName", "WebURL", "Found URL", "Notes" };
        private DataTable InDt;
        public DataTable OutDt { get; set; }
        public bool tried { get; set; }

        public CheckData(DataTable dt)
        {
            // TODO: Complete member initialization
            InDt = dt;
            OutDt = MoveThroughTable();
        }

        private DataTable MoveThroughTable()
        {
            DataTable returnTable = new DataTable();

            foreach (var column in columns)
            {
                returnTable.Columns.Add(column);
            }
            for (int i = 0; i < InDt.Rows.Count; i++)
            {
                Console.WriteLine(i);
                var nRow = InDt.Rows[i].ItemArray;
                returnTable.Rows.Add();
                returnTable.Rows[i][columns[0]] = nRow[0];
                returnTable.Rows[i][columns[1]] = nRow[1];
                returnTable.Rows[i][columns[2]] = nRow[2];
                string[] urlStatus = getUrl(returnTable.Rows[i][columns[2]].ToString());
                returnTable.Rows[i][columns[3]] = urlStatus[0];
                returnTable.Rows[i][columns[4]] = urlStatus[1];
                tried = false;
            }
            return returnTable;
        }
        private string[] getUrl(string website)
        {
            string[] returnUrl = new string[2];
            WebResponse response;
            WebRequest request;
            string httpSite = tried ? "https://" + website : "http://" + website;
            Console.WriteLine("trying " + httpSite);
            try
            {
                
                var uri = new Uri(httpSite);
                request = WebRequest.Create(uri);
                request.Timeout = 30000; //will timeout after 30 seconds and throw a webexception
                using (response = request.GetResponse())
                {
                    returnUrl[0] = response.ResponseUri.ToString();

                    if (response.ResponseUri.ToString() == httpSite)
                    {
                        returnUrl[1] = "success";
                    }
                    else
                    {
                        returnUrl[1] = "Website Change";
                    }
                   
                }
                Console.WriteLine("Success");
                tried  = false;
            }
            catch (WebException wex)
            {
                if (tried)
                {
                    Console.WriteLine("site was not found :" + wex.Message);
                    returnUrl[0] = "Not Found URL";
                    returnUrl[1] = wex.Message;
                }
                else
                {
                    tried = true;
                    getUrl(website);                   
                }
            }
            catch (Exception x)
            {
                if (tried)
                {
                    Console.WriteLine("Unexpected exception " + x.Message);
                    returnUrl[0] = "unknown error";
                    returnUrl[1] = x.Message;
                }
                else
                {
                    tried = true;
                    getUrl(website);                   
                }
                
            }      
            return returnUrl;
        }
        
    }
}
