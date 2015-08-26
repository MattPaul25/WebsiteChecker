using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WebsiteChecker
{
    class CheckData
    {
        private string[] columns = { "Source Code", "Source Type", "Source Name", "Web Address", "Directory Name", "Status" };
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
                var nRow = InDt.Rows[i].ItemArray;
                returnTable.Rows.Add();
                returnTable.Rows[i][columns[0]] = nRow[0];
                returnTable.Rows[i][columns[1]] = nRow[1];
                returnTable.Rows[i][columns[2]] = nRow[2];
                returnTable.Rows[i][columns[3]] = nRow[3];
                returnTable.Rows[i][columns[4]] = nRow[4];
                returnTable.Rows[i][columns[5]] = GetStatus(returnTable.Rows[i][columns[3]].ToString());
            }

            return returnTable;
        }

        private object GetStatus(string website)
        {
            string ReturnStatus = "";
            //
            return ReturnStatus;
        }

        
    }
}
