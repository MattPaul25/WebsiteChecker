﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            DataTable dt = new ImportData("C:\\Users\\Matt\\Desktop\\sources.csv").dt;
            var check = new CheckData(dt).OutDt;
            var exp = new ExportData(check, "C:\\Users\\Matt\\Desktop\\results.xlsx");
        }
        private static object GetStatus(string website)
        {
            string ReturnStatus = "";
            using (WebClient client = new WebClient())
            {
                try
                {
                    Console.WriteLine("Reading from " + website);
                    ReturnStatus = client.DownloadString(website);
                }
                catch (WebException ex)
                {

                    Console.WriteLine("Site was not found " + ex.Message);
                }
                catch (Exception x)
                {
                    Console.WriteLine("Unexpected exception " + x.Message);
                }
            }
            return ReturnStatus;

        }
   
    }
}
