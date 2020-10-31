using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel1 = Microsoft.Office.Interop.Excel;

namespace MyTable
{
    class ExcelPrinter
    {
        // Создаём экземпляр нашего приложения
        Excel1.Application excelApp = new Excel1.Application();
        // Создаём экземпляр рабочий книги Excel
        Excel1.Workbook workBook;
        // Создаём экземпляр листа Excel
        Excel1.Worksheet sheet = null;
        // Создаём экземпляр области ячеек Excel
        Excel1.Range range1 = null;
        public ExcelPrinter()
        {
            workBook = excelApp.Workbooks.Add();
            sheet = (Excel1.Worksheet)workBook.Worksheets.get_Item(1);
            write();
        }
        private void write()
        {
            //Заполняем
            //покажем пользователю отчет
            excelApp.Visible = true;
            excelApp.UserControl = false;
        }
        public enum Company
        {
            Impuls = 0,
            SKB
        }
        public void head(Company company)
        {
            //заголовок
            sheet.Cells.Font.Name = "ISOCPEUR";
            sheet.Cells.Font.Size = 12;
            sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 7]].Merge();
            sheet.Cells[1, 1].Font.Size = 24;
            sheet.Cells[1, 1].Font.Name = "Times New Roman";
            sheet.Cells.Font.Bold = true;

            switch (company)
            {
                case Company.Impuls:
                    sheet.Cells[1, 1] = "АО «Компания Импульс»";
                    break;
                case Company.SKB:
                    sheet.Cells[1, 1] = "ООО «СКБ-Сбытсервис»";
                    break;
            }

            sheet.Cells[1, 1].HorizontalAlignment = Excel1.Constants.xlCenter;

            sheet.Cells[3, 1] = "350072, Краснодарский  край, г.Краснодар,";
            sheet.Cells[4, 1] = "Ул. Московская, 5.";
            sheet.Rows[4].RowHeight = 18;
            switch (company)
            {
                case Company.Impuls:
                    sheet.Cells[5, 1] = "Тел. 8(861) 252-11-21";
                    break;
                case Company.SKB:
                    sheet.Cells[5, 1] = "Тел. 8(861) 252-09-83";
                    break;
            }
            sheet.Rows[5].RowHeight = 18;
        }
        private string periodMY(DateTime dat1, DateTime dat2)
        {
            if (dat1 == dat2)
            {
                return MonthToStr(dat1.Month) + " " + dat1.Year.ToString() + "г.";
            }
            else
            {
                return MonthToStr(dat1.Month) + " " + dat1.Year.ToString() + "г. - " + MonthToStr(dat2.Month) + " " + dat2.Year.ToString() + "г.";
            }
        }
        private string MonthToStr(int month1)
        {
            string retMonth = "";
            switch (month1)
            {
                case 1: retMonth = "январь";
                    break;
                case 2: retMonth = "февраль";
                    break;
                case 3: retMonth = "март";
                    break;
                case 4: retMonth = "апрель";
                    break;
                case 5: retMonth = "май";
                    break;
                case 6: retMonth = "июнь";
                    break;
                case 7: retMonth = "июль";
                    break;
                case 8: retMonth = "август";
                    break;
                case 9: retMonth = "сентябрь";
                    break;
                case 10: retMonth = "октябрь";
                    break;
                case 11: retMonth = "ноябрь";
                    break;
                case 12: retMonth = "декабрь";
                    break;
            }
            return retMonth;
        }
        //headUser - заголовок по Арендатору и выбранному периоду
        public void headUser(string userCB23, DateTime dTP5, DateTime dTP6)
        {//userCB23 - Арендатор, dTP5 - начало периода, dTP6 - конец периода
            sheet.Cells[7, 1] = "Потребитель: " + userCB23;
            sheet.Cells[8, 1] = "Адрес объекта: г.Краснодар, ул. Московская, 5.";
            sheet.Rows[8].RowHeight = 22;
            sheet.Cells[10, 1] = "Расчет количества потребленной электроэнергии за " + periodMY(dTP5, dTP6);
            sheet.Rows[10].RowHeight = 22;
        }

        public void headTable(DateTime dTP5, DateTime nextDate)//nextDate = dTP6;
        {
            //заголовок таблицы
            sheet.Columns[2].ColumnWidth = 22;
            sheet.Columns[3].ColumnWidth = 10;
            sheet.Columns[4].ColumnWidth = 12;
            sheet.Columns[5].ColumnWidth = 12;
            sheet.Columns[7].ColumnWidth = 9;
            sheet.Cells[11, 1] = "№";
            sheet.Cells[11, 2] = "№ точки учета по договору";
            sheet.Cells[11, 3] = "№ счетчика";
            sheet.Cells[11, 4] = "Показания на  01." + dTP5.Month + "." + dTP5.Year;
            nextDate = nextDate.AddMonths(1);
            sheet.Cells[11, 5] = "Показания на 01." + nextDate.Month + "." + nextDate.Year;
            sheet.Cells[11, 6] = "Расч. Коэфф.";
            sheet.Cells[11, 7] = "Расход, кВт.ч";
        }
        //Temp.AddRange(ToReport(comboBox23.Text, dateTimePicker5.Value, dateTimePicker6.Value).ToArray());
        int k = 0;//0
        public void bodyTable(List<string> Temp)
        {
            //циклом заполним таблицу

            if (Temp.Count > 0)
            {
                for (; k < (Temp.Count) / 6; k++)
                {
                    sheet.Cells[12 + k, 1] = (k + 1).ToString() + ".";
                    sheet.Cells[12 + k, 2] = Temp[k * 6];       //помещение   из data, остальное из counters
                    sheet.Cells[12 + k, 3] = Temp[k * 6 + 1];   //№счетчика
                    sheet.Cells[12 + k, 4] = Temp[k * 6 + 2].Replace(',', '.');   //показания начало
                    sheet.Cells[12 + k, 5] = Temp[k * 6 + 3].Replace(',', '.');   //показания конец
                    sheet.Cells[12 + k, 6] = Temp[k * 6 + 4];   //расч. коэфф.                    
                    sheet.Cells[12 + k, 7] = Temp[k * 6 + 5].Replace(',', '.');   //расход
                    // sheet.Cells[12 + k, 7].NumberFormat = "0,0";//формат ячейки числовой
                }
            }
        }

        public void footerTable()
        {
            sheet.Cells[12 + k, 2] = "Всего";
            Excel1.Range formulaRange = sheet.Range[sheet.Cells[12, 7], sheet.Cells[11 + k, 7]];
            string ToAdresEx = formulaRange.get_Address(1, 1, Excel1.XlReferenceStyle.xlR1C1, Type.Missing, Type.Missing);
            if (k > 0) sheet.Cells[12 + k, 7].Formula = "=SUM(G12:G" + (11 + k).ToString() + ")";//формула (сумма)
            else sheet.Cells[12 + k, 7] = "0";
        }
        //for (int i = 0; i < Temp.Count; i++) richTextBox1.Text += Temp[i] + "\r\n";//отладка
        public void bordersTable()
        {
            range1 = sheet.Range[sheet.Cells[11, 1], sheet.Cells[12 + k, 7]]; //выделяем всю таблицу
            range1.Cells.Font.Size = 10;
            range1.Cells.Font.Italic = true;
            range1.Cells.Font.Bold = false;
            range1.Cells.WrapText = true;
            range1.Borders.LineStyle = Excel1.XlLineStyle.xlContinuous; //границы выделенной области
            range1.Borders.Weight = Excel1.XlBorderWeight.xlMedium;

            sheet.Cells[12 + k, 2].Font.Bold = true;//всего жирное
            sheet.Cells[12 + k, 7].Font.Bold = true;//сумма жирная
        }
        public void endSheet()
        {
            //подпись
            sheet.Cells[17 + k, 2] = "Главный энергетик";
            sheet.Cells[17 + k, 2].Font.Italic = true;
            sheet.Cells[17 + k, 5] = "Канавин А.А.";
            sheet.Cells[17 + k, 5].Font.Italic = true;
            sheet.Cells[19 + k, 4] = "М.П.";
            sheet.Cells[19 + k, 4].HorizontalAlignment = Excel1.Constants.xlRight;
        }

        // Открываем созданный excel-файл
        //workBook.Application.DisplayAlerts = false;
        // workBook.SaveAs( "d:\\Parse.xlsx"); 
        //excelApp.Visible = true;
        //excelApp.UserControl = true;

        //выгружаем
        /*
        System.Runtime.InteropServices.Marshal.ReleaseComObject(range1);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
        workBook.Close(false, null, null);
        System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
        excelApp.Quit();
        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
        GC.Collect();
        GC.WaitForPendingFinalizers();*/
    }
}
