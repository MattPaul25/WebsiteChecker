using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebsiteChecker
{
    class ImportData
    {
        public DataTable dt { get; set; }
        public ImportData(string Location)
        {
            dt = csvToDataTable(Location, '|');
        }
        private DataTable csvToDataTable(string fileName, char splitCharacter)
        {
            StreamReader sr = new StreamReader(fileName);
            string myStringRow = sr.ReadLine();
            var rows = myStringRow.Split(splitCharacter);
            DataTable CsvData = new DataTable();
            foreach (string column in rows)
            {
                //creates the columns of new datatable based on first row of csv
                CsvData.Columns.Add(column);
            }
            myStringRow = sr.ReadLine();
            while (myStringRow != null)
            {
                //runs until string reader returns null and adds rows to dt 
                rows = myStringRow.Split(splitCharacter);
                CsvData.Rows.Add(rows);
                myStringRow = sr.ReadLine();
            }
            sr.Close();
            sr.Dispose();
            return CsvData;
        }
    }
}
