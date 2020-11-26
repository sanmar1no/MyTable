using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MyTable
{
    class Variables //класс в котором хранятся переменные проекта
    {
        //пользователи
        public enum UserKeyEnum
        {
            electro,
            aqua,
            arenda,
            admin
        }
        //Книга эксель
        public static IWorkbook workbook;
        public static ISheet sheet1;
        public static string fileNameExcel="test.xlsx";

        // Создаём экземпляр области ячеек Excel
        public static IRow rowSheet;
        public static IFont[] fontM = new IFont[7];
        public static ICellStyle[] styleM = new ICellStyle[7];
       
        //создать в книге новый лист
        public static void newSheet(string nameSheet)
        {
            sheet1 = workbook.CreateSheet(nameSheet);
            rowSheet = sheet1.CreateRow(0);
        }

        //вывести доступные имена листов в книге
        public static List<string> ListSheet()
        {
            List<string> List = new List<string>();
            for (int i = 0; i < workbook.NumberOfSheets; i++)
            {
                List.Add(workbook.GetSheetName(i));
            }
            return List;
        }

        //создать новую книгу Excel, присвоить стилям значения
        public static void newWorkbook()
        {
            newWorkbook("Лист1", "test.xlsx");
        }
        public static void newWorkbook(string nameSheet)
        {
            newWorkbook(nameSheet, "test.xlsx");
        }
        public static void newWorkbook(string nameSheet,string FileName)
        {
            workbook = new XSSFWorkbook();
            fileNameExcel = FileName;
            //workbook.NumberOfSheets
            sheet1 = workbook.CreateSheet(nameSheet);
           // sheet2 = workbook.CreateSheet("Лист2");
           // sheet2 = workbook.CreateSheet("Лист3");
            rowSheet = sheet1.CreateRow(0);

            for (int i = 0; i < 7; i++)
            {
                fontM[i] = workbook.CreateFont();
                styleM[i] = workbook.CreateCellStyle();
            }
            //Cell.Style.normal:
            fontM[0] = workbook.CreateFont();
            fontM[0].FontName = "ISOCPEUR";
            fontM[0].FontHeightInPoints = 12;
            fontM[0].IsBold = false;
            fontM[0].IsItalic = true;

            styleM[0].SetFont(fontM[0]);
            styleM[0].BorderLeft = BorderStyle.Thin;
            styleM[0].BorderBottom = BorderStyle.Thin;
            styleM[0].BorderRight = BorderStyle.Thin;
            styleM[0].BorderTop = BorderStyle.Thin;
            styleM[0].WrapText = true;

            //Cell.Style.bold:
            fontM[1].FontName = "ISOCPEUR";
            fontM[1].FontHeightInPoints = 12;
            fontM[1].IsBold = true;
            fontM[1].IsItalic = true;
            //font.Color = IndexedColors.Red.Index;
            styleM[1].SetFont(fontM[1]);
            styleM[1].BorderLeft = BorderStyle.Thick;
            styleM[1].BorderBottom = BorderStyle.Thick;
            styleM[1].BorderRight = BorderStyle.Thick;
            styleM[1].BorderTop = BorderStyle.Thick;
            styleM[1].WrapText = true;
            //Cell.Style.summ:
            fontM[2].FontName = "ISOCPEUR";
            fontM[2].FontHeightInPoints = 12;
            fontM[2].IsBold = true;
            fontM[2].IsItalic = true;
            //font.Color = IndexedColors.Red.Index;
            styleM[2].SetFont(fontM[2]);
            styleM[2].BorderLeft = BorderStyle.Thin;
            styleM[2].BorderBottom = BorderStyle.Thin;
            styleM[2].BorderRight = BorderStyle.Thin;
            styleM[2].BorderTop = BorderStyle.Thin;
            styleM[2].WrapText = true;

            //Cell.Style.clientCame:
            fontM[3].FontName = "ISOCPEUR";
            fontM[3].FontHeightInPoints = 12;
            fontM[3].IsBold = true;
            fontM[3].IsItalic = true;
            //fontDynamic.Color = IndexedColors.Aqua.Index;
            styleM[3].SetFont(fontM[3]);
            styleM[3].BorderLeft = BorderStyle.Medium;
            styleM[3].BorderBottom = BorderStyle.Medium;
            styleM[3].BorderRight = BorderStyle.Medium;
            styleM[3].BorderTop = BorderStyle.Medium;
            styleM[3].FillForegroundColor = IndexedColors.Aqua.Index;
            styleM[3].FillPattern = FillPattern.SolidForeground;
            styleM[3].WrapText = true;

            //Cell.Style.clientOut:
            fontM[4].FontName = "ISOCPEUR";
            fontM[4].FontHeightInPoints = 12;
            fontM[4].IsBold = true;
            fontM[4].IsItalic = true;
            styleM[4].SetFont(fontM[4]);
            styleM[4].BorderLeft = BorderStyle.Medium;
            styleM[4].BorderBottom = BorderStyle.Medium;
            styleM[4].BorderRight = BorderStyle.Medium;
            styleM[4].BorderTop = BorderStyle.Medium;
            styleM[4].FillForegroundColor = IndexedColors.LightGreen.Index;
            styleM[4].FillPattern = FillPattern.SolidForeground;
            styleM[4].WrapText = true;
            //Cell.Style.colorPink:
            fontM[5].FontName = "ISOCPEUR";
            fontM[5].FontHeightInPoints = 12;
            fontM[5].IsBold = false;
            fontM[5].IsItalic = true;
            styleM[5].SetFont(fontM[5]);
            styleM[5].BorderLeft = BorderStyle.Medium;
            styleM[5].BorderBottom = BorderStyle.Medium;
            styleM[5].BorderRight = BorderStyle.Medium;
            styleM[5].BorderTop = BorderStyle.Medium;
            styleM[5].FillForegroundColor = IndexedColors.Pink.Index;
            styleM[5].FillPattern = FillPattern.SolidForeground;
            styleM[5].WrapText = true;
            //Style.noBorder:
            fontM[6].FontName = "ISOCPEUR";
            fontM[6].FontHeightInPoints = 12;
            fontM[6].IsBold = false;
            fontM[6].IsItalic = false;
            styleM[6].SetFont(fontM[6]);
            styleM[6].BorderLeft = BorderStyle.None;
            styleM[6].BorderBottom = BorderStyle.None;
            styleM[6].BorderRight = BorderStyle.None;
            styleM[6].BorderTop = BorderStyle.None;
            styleM[6].WrapText = false;
        }



    }
}
