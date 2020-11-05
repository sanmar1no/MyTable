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
        //блок переменных
        public Company company = new Company();
        public Report report = new Report();
        public DateTime dTP5 = new DateTime();
        public DateTime dTP6 = new DateTime();
        public string arendaCB23 = "";

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
            : this(new Company(), new Report(), DateTime.Now, new DateTime())
        {
        }
        public ExcelPrinter(Company company)
            : this(company, new Report(), DateTime.Now, new DateTime())
        {
        }
        public ExcelPrinter(Company company, Report report)
            : this(company, report, new DateTime(), new DateTime())
        {
        }
        public ExcelPrinter(Company company, Report report, DateTime dTP5, DateTime dTP6)
        {
            this.company = company;
            this.report = report;
            this.dTP5 = dTP5;
            this.dTP6 = dTP6;
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
            head();
        }
        public enum Company
        {
            Impuls = 0,
            SKB
        }
        public enum Report
        {
            countersPeriod = 0,
            countersPeriodAll,
            countersInventoryElectro,
            countersInventoryAqua            
        }
        public void head()
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
        private void headArenda()
        {//userCB23 - Арендатор, dTP5 - начало периода, dTP6 - конец периода
            row += 2;
            sheet.Cells[row, 1] = "Потребитель: " + arendaCB23;
            row++;
            sheet.Cells[row, 1] = "Адрес объекта: г.Краснодар, ул. Московская, 5.";
            sheet.Rows[row].RowHeight = 22;
            row += 2;
        }
        public void headName()
        {
            switch (report)//добавить название таблицы
            {
                case Report.countersPeriod:
                    headArenda();
                    nameTable("Расчет количества потребленной электроэнергии за " + periodMY(dTP5, dTP6));
                    break;
                case Report.countersInventoryElectro:
                    nameTable("Инвентаризация электросчетчиков");
                    break;
                case Report.countersInventoryAqua:
                    nameTable("Инвентаризация водомеров");
                    break;
                case Report.countersPeriodAll:
                    string dataStr = "";
                    if (dTP5 != new DateTime())
                    {
                        DateTime toName = dTP5;// dTP5.AddMonths(-1);
                        dataStr = MonthToStr(toName.Month) + " " + toName.Year + "г.";
                    }                    
                    nameTable("Расчет количества потребленной электроэнергии за "+ dataStr);
                    break;
            }
            headTable();
        }
        private void nameTable(string name)
        {
            row++;
            sheet.Cells[row, 1] = name;
            sheet.Rows[row].RowHeight = 22;
        }
        private void headTable()//разные отчеты, разные заголовки
        {
            switch (report)
            {
                case Report.countersPeriod:
                    headTableCP();
                    break;
                case Report.countersInventoryElectro:
                    headTableCI();
                    break;
                case Report.countersInventoryAqua:
                    headTableCI();
                    break;
                case Report.countersPeriodAll:
                    headTableREPORT();
                    break;
            }
        }
        private void headTableCP()//nextDate = dTP6; CP-countersPeriod
        {
            row++;
            //заголовок таблицы
            sheet.Columns[2].ColumnWidth = 22;
            sheet.Columns[3].ColumnWidth = 10;
            sheet.Columns[4].ColumnWidth = 12;
            sheet.Columns[5].ColumnWidth = 12;
            sheet.Columns[7].ColumnWidth = 9;
            sheet.Cells[row, 1] = "№";
            sheet.Cells[row, 2] = "№ точки учета по договору";
            sheet.Cells[row, 3] = "№ счетчика";
            sheet.Cells[row, 4] = "Показания на  01." + dTP5.Month + "." + dTP5.Year;
            DateTime data = dTP6.AddMonths(1);
            sheet.Cells[row, 5] = "Показания на 01." + data.Month + "." + data.Year;
            sheet.Cells[row, 6] = "Расч. Коэфф.";
            sheet.Cells[row, 7] = "Расход, кВт.ч";
            countColumn = 6;
        }
        private void headTableCI()
        {
            row++;
            sheet.Columns[1].ColumnWidth = 5;
            sheet.Columns[2].ColumnWidth = 22;
            sheet.Columns[3].ColumnWidth = 10;
            sheet.Columns[4].ColumnWidth = 12;
            sheet.Columns[5].ColumnWidth = 9;
            sheet.Columns[7].ColumnWidth = 12;
            sheet.Cells[row, 1] = "№ п/п";
            sheet.Cells[row, 2] = "№ Корпуса и помещения";
            sheet.Cells[row, 3] = "№ счетчика";
            sheet.Cells[row, 4] = "Марка счетчика";
            sheet.Cells[row, 5] = "Год выпуска/поверки";
            sheet.Cells[row, 6] = "Показания (последние), " + (report==Report.countersInventoryElectro ? "кВт*ч" : "куб.м.");
            countColumn = 5;
        }
        private void headTableREPORT()
        {
            row++;
            sheet.Columns[1].ColumnWidth = 5;
            sheet.Columns[2].ColumnWidth = 40;
            sheet.Columns[3].ColumnWidth = 10;
            sheet.Columns[4].ColumnWidth = 10;
            sheet.Cells[row, 1] = "№ п/п";
            sheet.Cells[row, 2] = "Арендатор";
            sheet.Cells[row, 3] = "№ счетчика";
            sheet.Cells[row, 4] = "Расход, кВт*ч";
            countColumn = 3;
        }
        //Temp.AddRange(ToReport(comboBox23.Text, dateTimePicker5.Value, dateTimePicker6.Value).ToArray());

        public void bodyTable(List<string> Temp)//заполнение таблицы из List
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

        public void footerTableSumm(string literColumn)//последняя строка Всего:
        {
            sheet.Cells[row + k, 2] = "Всего";
            Excel1.Range formulaRange = sheet.Range[sheet.Cells[row, countColumn + 1], sheet.Cells[row - 1 + k, countColumn + 1]];
            string ToAdresEx = formulaRange.get_Address(1, 1, Excel1.XlReferenceStyle.xlR1C1, Type.Missing, Type.Missing);
            if (k > 0) sheet.Cells[row + k, countColumn+1].Formula = "=SUM("+literColumn+row.ToString()+":"+literColumn + (row-1 + k).ToString() + ")";//формула (сумма)
            else sheet.Cells[row + k, countColumn + 1] = "0";
        }
        public void footerTableCount()//последняя строка Всего:
        {
            sheet.Cells[row + k, 2] = "Общее количество";
            sheet.Cells[row + k, countColumn + 1] = k.ToString();
        }
        //for (int i = 0; i < Temp.Count; i++) richTextBox1.Text += Temp[i] + "\r\n";//отладка
        public void bordersTable()//границы и стиль таблицы
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
        public void endSheet()        //подпись
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
