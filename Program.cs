using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Starting");
            DataTable dt = new ImportData("C:\\Users\\Matt\\Desktop\\Sources.csv").dt;
            var check = new CheckData(dt);
        }
    }
}
