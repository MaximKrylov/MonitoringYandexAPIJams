using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonitoringYandexAPIJams.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public void GetInfoAndWriteToFile(string[] street, double[] length, double[] jamsTime)
        {
            int countOfData = (street.Length + length.Length + jamsTime.Length) / 3;
            double flagCell = 1;

            Microsoft.Office.Interop.Excel.Application excelApp = new Microsoft.Office.Interop.Excel.Application();
            excelApp.Visible = false;
            Microsoft.Office.Interop.Excel.Workbooks excelAppWorkbooks = excelApp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook excelAppWorkbook;

            excelApp.Workbooks.Open("C:/MonitoringYandexAPIJams/MonitoringYandexAPIJams/Results.xlsx");

            Microsoft.Office.Interop.Excel.Sheets excelSheets;
            Microsoft.Office.Interop.Excel.Worksheet excelWorksheet;
            Microsoft.Office.Interop.Excel.Range excelCells;
            

            excelSheets = excelApp.Worksheets;
            excelWorksheet = (Microsoft.Office.Interop.Excel.Worksheet)excelSheets.get_Item(1);

            excelCells = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[countOfData * 3 + 1, 1];
            if (excelCells.Value2 == null)
            {
                excelCells.Value2 = flagCell;
            }
            else
            {
                flagCell = excelCells.Value2;
            }

            int flagRows = 0;
            if (flagCell == 1)
            {
                for (int i = 0; i < countOfData; i++)
                {
                    excelCells = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[i + 1 + flagRows, flagCell];
                    excelCells.Value2 = street[i];
                    excelCells = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[i + 2 + flagRows, flagCell];
                    excelCells.Value2 = length[i];
                    excelCells = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[i + 3 + flagRows, flagCell];
                    excelCells.Value2 = jamsTime[i];
                    excelCells = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[i + 4 + flagRows, flagCell];
                    excelCells.Value2 = DateTime.Now.ToString("HH:mm:ss");
                    flagRows += 3;
                }
            }
            else
            {
                for (int i = 0; i < countOfData; i++)
                {
                    excelCells = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[i + 3 + flagRows, flagCell];
                    excelCells.Value2 = jamsTime[i];
                    excelCells = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[i + 4 + flagRows, flagCell];
                    excelCells.Value2 = DateTime.Now.ToString("HH:mm:ss");
                    flagRows += 3;
                }
            }

            excelCells = (Microsoft.Office.Interop.Excel.Range)excelWorksheet.Cells[countOfData * 3 + 1, 1];
            excelCells.Value2 = flagCell + 1;

            excelAppWorkbooks = excelApp.Workbooks;
            excelAppWorkbook = excelAppWorkbooks[1];
            excelAppWorkbook.Save();
            excelApp.Quit();
        }

    }
}
