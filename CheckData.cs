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
        private string[] columns = { "Source Code", "Source Type", "Source Name", "Web Address", "Directory Name", "Found URL", "Notes"};
        private DataTable InDt;
        public DataTable OutDt { get; set; }

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
                returnTable.Rows[i][columns[3]] = nRow[3];
                returnTable.Rows[i][columns[4]] = nRow[4];
                string[] urlStatus = getUrl(returnTable.Rows[i][columns[3]].ToString());
                returnTable.Rows[i][columns[5]] = urlStatus[0];
                returnTable.Rows[i][columns[6]] = urlStatus[1];
            }
            return returnTable;
        }
        private string[] getUrl(string website)
        {
            Console.WriteLine("trying " + website);
            string[] returnUrl = new string[2];
            WebResponse response;
            WebRequest request;
            try
            {
                
                var uri = new Uri(website);
                request = WebRequest.Create(uri);
                request.Timeout = 30000; //will timeout after 30 seconds and throw a webexception
                using (response = request.GetResponse())
                {
                    returnUrl[0] = response.ResponseUri.ToString();

                    if (response.ResponseUri.ToString() == website)
                    {
                        returnUrl[1] = "success";
                    }
                    else
                    {
                        returnUrl[1] = "Website Change";
                    }
                   
                }
                Console.WriteLine("Success");
            }
            catch (WebException wex)
            {
                Console.WriteLine("site was not found :" + wex.Message);
                returnUrl[0] = "Not Found URL";
                returnUrl[1] = wex.Message;
            }
            catch (Exception x)
            {
                Console.WriteLine("Unexpected exception " + x.Message);
                returnUrl[0] = "unknown error";
                returnUrl[1] = x.Message;
            }      
            return returnUrl;
        }
        
    }
}
