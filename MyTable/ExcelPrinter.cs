using System;
using System.Collections.Generic;
using System.Linq;
using Excel1 = Microsoft.Office.Interop.Excel;

namespace MyTable
{
    class ExcelPrinter
    {
        //блок переменных
        public Company company = new Company();
 
        int k = 0;//0
        int row = 1;
        int countColumn = 0;
        // Создаём экземпляр нашего приложения
        Excel1.Application excelApp = new Excel1.Application();
        // Создаём экземпляр рабочий книги Excel
        Excel1.Workbook workBook;
        // Создаём экземпляр листа Excel
        Excel1.Worksheet sheet = null;
        // Создаём экземпляр области ячеек Excel
        Excel1.Range range1 = null;

        public ExcelPrinter()
            : this(new Company())
        {
        }
        public ExcelPrinter(Company company)
        {
            this.company = company;
            startWrite();
        }
        private void startWrite()
        {
            workBook = excelApp.Workbooks.Add();
            sheet = (Excel1.Worksheet)workBook.Worksheets.get_Item(1);
            //Заполняем
            //покажем пользователю отчет
            excelApp.Visible = true;
            excelApp.UserControl = false;
            Head();
        }
        public enum Company
        {
            Impuls = 0,
            SKB
        }
        public void Head()
        {
            //заголовок
            sheet.Cells.Font.Name = "ISOCPEUR";
            sheet.Cells.Font.Size = 12;
            sheet.Range[sheet.Cells[row, 1], sheet.Cells[row, 7]].Merge();
            sheet.Cells[row, 1].Font.Size = 24;
            sheet.Cells[row, 1].Font.Name = "Times New Roman";
            sheet.Cells.Font.Bold = true;

            switch (company)
            {
                case Company.Impuls:
                    sheet.Cells[row, 1] = "АО «Компания Импульс»";
                    break;
                case Company.SKB:
                    sheet.Cells[row, 1] = "ООО «СКБ-Сбытсервис»";
                    break;
            }

            sheet.Cells[row, 1].HorizontalAlignment = Excel1.Constants.xlCenter;
            row += 2;
            sheet.Cells[row, 1] = "350072, Краснодарский  край, г.Краснодар,";
            row++;
            sheet.Cells[row, 1] = "Ул. Московская, 5.";
            sheet.Rows[row].RowHeight = 18;
            row++;
            switch (company)
            {
                case Company.Impuls:
                    sheet.Cells[row, 1] = "Тел. 8(861) 252-11-21";
                    break;
                case Company.SKB:
                    sheet.Cells[row, 1] = "Тел. 8(861) 252-09-83";
                    break;
            }
            sheet.Rows[row].RowHeight = 18;
        }

        //headUser - заголовок по Арендатору и выбранному периоду
        public void HeadArenda(string arendaCB23="")
        {//userCB23 - Арендатор, dTP5 - начало периода, dTP6 - конец периода
            row += 2;
            sheet.Cells[row, 1] = "Потребитель: " + arendaCB23;
            row++;
            sheet.Cells[row, 1] = "Адрес объекта: г.Краснодар, ул. Московская, 5.";
            sheet.Rows[row].RowHeight = 22;
            row += 2;
        }
        public void NameTable(string name)
        {
            row++;
            sheet.Cells[row, 1] = name;
            sheet.Rows[row].RowHeight = 22;
        }
        public void HeadTable(List<string> List)
        {
            row++;
            double lenght = 0;
            foreach (string elem in List)
            {
                lenght += elem.Length;
            }
            double koeff = 80/ lenght;
            for (int i = 1; i <= List.Count(); i++)
            {   int mat1 = (int)Math.Round(List[i - 1].Length * koeff, 0, MidpointRounding.AwayFromZero);
                sheet.Columns[i].ColumnWidth = mat1;
                sheet.Cells[row, i]= List[i-1];
            }
            countColumn = List.Count()-1;
        }

        public void BodyTable(List<string> Temp)//заполнение таблицы из List
        {
            //циклом заполним таблицу
            row++;
            if (Temp.Count > 0)
            {
                for (; k < (Temp.Count) / countColumn; k++)
                {
                    sheet.Cells[row + k, 1] = (k + 1).ToString() + ".";
                    for (int i = 0; i < countColumn; i++)
                    {
                        sheet.Cells[row + k, i+2] = Temp[k * countColumn + i].Replace(',', '.');  
                    }
                    // sheet.Cells[12 + k, 7].NumberFormat = "0,0";//формат ячейки числовой
                }
            }
        }
        public void FooterTableSumm(string literColumn)//последняя строка Всего:
        {
            sheet.Cells[row + k, 2] = "Всего";
            Excel1.Range formulaRange = sheet.Range[sheet.Cells[row, countColumn + 1], sheet.Cells[row - 1 + k, countColumn + 1]];
            string ToAdresEx = formulaRange.get_Address(1, 1, Excel1.XlReferenceStyle.xlR1C1, Type.Missing, Type.Missing);
            if (k > 0) sheet.Cells[row + k, countColumn+1].Formula = "=SUM("+literColumn+row.ToString()+":"+literColumn + (row-1 + k).ToString() + ")";//формула (сумма)
            else sheet.Cells[row + k, countColumn + 1] = "0";
        }
        public void FooterTableCount()//последняя строка Всего:
        {
            sheet.Cells[row + k, 2] = "Общее количество";
            sheet.Cells[row + k, countColumn + 1] = k.ToString();
        }
        //for (int i = 0; i < Temp.Count; i++) richTextBox1.Text += Temp[i] + "\r\n";//отладка
        public void BordersTable()//границы и стиль таблицы
        {
            range1 = sheet.Range[sheet.Cells[row - 1, 1], sheet.Cells[row + k, countColumn+1]]; //выделяем всю таблицу
            range1.Cells.Font.Size = 10;
            range1.Cells.Font.Italic = true;
            range1.Cells.Font.Bold = false;
            range1.Cells.WrapText = true;
            range1.Borders.LineStyle = Excel1.XlLineStyle.xlContinuous; //границы выделенной области
            range1.Borders.Weight = Excel1.XlBorderWeight.xlMedium;

            sheet.Cells[row + k, 2].Font.Bold = true;//всего жирное
            sheet.Cells[row + k, countColumn + 1].Font.Bold = true;//сумма жирная
        }
        public void EndSheet()        //подпись
        {
            row += 5;
            //подпись
            sheet.Cells[row + k, 2] = "Главный энергетик";
            sheet.Cells[row + k, 2].Font.Italic = true;
            sheet.Cells[row + k, 5] = "Канавин А.А.";
            sheet.Cells[row + k, 5].Font.Italic = true;
            row += 2;
            sheet.Cells[row + k, 4] = "М.П.";
            sheet.Cells[row + k, 4].HorizontalAlignment = Excel1.Constants.xlRight;
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
