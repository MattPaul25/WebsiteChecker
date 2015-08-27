using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Data;
using System.Runtime.InteropServices;


namespace WebsiteChecker
{
    class ExportData
    {
        public ExportData(System.Data.DataTable dt, string location)
        {          
      
            //instantiate excel objects (application, workbook, worksheets)
            excel.Application XlObj = new excel.Application();
            XlObj.Visible = false;
            excel._Workbook WbObj = (excel.Workbook)(XlObj.Workbooks.Add(""));
            excel._Worksheet WsObj = (excel.Worksheet)WbObj.ActiveSheet;

            //run through datatable and assign cells to values of datatable
            
                int row = 1; int col = 1;
                foreach (DataColumn column in dt.Columns)
                {
                    //adding columns
                    WsObj.Cells[row, col] = column.ColumnName;
                    col++;
                }
                //reset column and row variables
                col = 1;
                row++;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //adding data
                    foreach (var cell in dt.Rows[i].ItemArray)
                    {
                        WsObj.Cells[row, col] = cell;
                        col++;
                    }
                    col = 1;
                    row++;
                }
                WbObj.SaveAs(location);          
         
        }
    }
}
