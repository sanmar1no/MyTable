using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyTable
{
    class ReportPrinter
    {
        public ExcelPrinter.Company company = new ExcelPrinter.Company();
        public Variables.UserKeyEnum reportKey = new Variables.UserKeyEnum();
        public DateTime dTP5 = new DateTime();
        public DateTime dTP6 = new DateTime();
        private List<string> TableList = new List<string>();

        public ReportPrinter()
            : this(new ExcelPrinter.Company(), DateTime.Now, new DateTime())
        {
        }
        public ReportPrinter(ExcelPrinter.Company company)
            : this(company, DateTime.Now, new DateTime())
        {
        }
        /* public ReportPrinter(ExcelPrinter.Company company, Variables.userKeyEnum report)
             : this(company, DateTime.Now, new DateTime())
         {
                    this.reportKey = reportKey;
         }*/
        public ReportPrinter(ExcelPrinter.Company company, DateTime dTP5)
            : this(company, dTP5, new DateTime())
        {
        }
        public ReportPrinter(ExcelPrinter.Company company, DateTime dTP5, DateTime dTP6)
        {
            this.company = company;
            this.dTP5 = dTP5;
            this.dTP6 = dTP6;
        }
        public void AddList(List<string> List)
        {
            TableList.Clear();
            TableList.AddRange(List);
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
                case 1:
                    retMonth = "январь";
                    break;
                case 2:
                    retMonth = "февраль";
                    break;
                case 3:
                    retMonth = "март";
                    break;
                case 4:
                    retMonth = "апрель";
                    break;
                case 5:
                    retMonth = "май";
                    break;
                case 6:
                    retMonth = "июнь";
                    break;
                case 7:
                    retMonth = "июль";
                    break;
                case 8:
                    retMonth = "август";
                    break;
                case 9:
                    retMonth = "сентябрь";
                    break;
                case 10:
                    retMonth = "октябрь";
                    break;
                case 11:
                    retMonth = "ноябрь";
                    break;
                case 12:
                    retMonth = "декабрь";
                    break;
            }
            return retMonth;
        }
        public void ReportCountersPeriod(string arendaCB23) //отчет по расходу электросчетчиков за период 
        {
            List<string> Header = new List<string>();
            ExcelPrinter report1 = new ExcelPrinter();
            DateTime data = dTP6.AddMonths(1);
            //List<string> Header = new List<string>(){ "№", "№ точки учета по договору", "№ счетчика", "Показания на  01."+dTP5.Month + "." + dTP5.Year, "Показания на 01." + data.Month + "." + data.Year, "Расч.Коэфф.", "Расход, кВт.ч."};
            Header.Add("№ п/п");
            Header.Add("№ точки учета по договору");
            Header.Add("№ счетчика    ");
            Header.Add("Показания на  01." + dTP5.Month + "." + dTP5.Year);
            Header.Add("Показания на 01." + data.Month + "." + data.Year);
            Header.Add("Расч.Коэфф.");
            Header.Add("Расход, кВт.ч.");
            report1.company = ExcelPrinter.Company.SKB;
            report1.HeadArenda(arendaCB23);
            report1.NameTable("Расчет количества потребленной электроэнергии за " + periodMY(dTP5, dTP6));
            report1.HeadTable(Header);
            report1.BodyTable(TableList);
            report1.FooterTableSumm("G");
            report1.BordersTable();
            report1.EndSheet();
        }
        public void ReportCountersInventory(Variables.UserKeyEnum reportKey) //инвентаризация электросчетчиков/водомеров
        {
            List<string> Header = new List<string>();
            ExcelPrinter report1 = new ExcelPrinter();
            report1.company = ExcelPrinter.Company.Impuls;//исправить
            Header.Add("№ п/п");
            Header.Add("№ Корпуса и помещения");
            Header.Add("№ счетчика    ");
            Header.Add("Марка счетчика");
            Header.Add("Год выпуска/поверки");
            Header.Add("Показания (последние), " + (reportKey == Variables.UserKeyEnum.electro ? "кВт*ч" : "куб.м."));
            if(reportKey == Variables.UserKeyEnum.electro) report1.NameTable("Инвентаризация электросчетчиков");
            if(reportKey == Variables.UserKeyEnum.aqua) report1.NameTable("Инвентаризация водомеров");
            report1.HeadTable(Header);
            report1.BodyTable(TableList);
            report1.FooterTableCount();
            report1.BordersTable();
            report1.EndSheet();
        }
        public void ReportCountersPeriodAll() //Основной отчет по электроэнергии (период - месяц)
        {
            List<string> Header = new List<string>();
            ExcelPrinter report1 = new ExcelPrinter();
            report1.company = ExcelPrinter.Company.SKB;//исправить
            Header.Add("№ п/п");
            Header.Add("Арендатор  ");
            Header.Add("№ счетчика  ");
            Header.Add("Расход, кВт*ч");
            string dataStr = "";
            if (dTP5 != new DateTime())
            {
                DateTime toName = dTP5;// dTP5.AddMonths(-1);
                dataStr = MonthToStr(toName.Month) + " " + toName.Year + "г.";
            }
            report1.NameTable("Расчет количества потребленной электроэнергии за " + dataStr);
            report1.HeadTable(Header);
            report1.BodyTable(TableList);
            report1.FooterTableSumm("D");
            report1.BordersTable();
            report1.EndSheet();
        }
        public void ReportArendaPhoneBook()
        {
            List<string> Header = new List<string>();
            ExcelPrinter report1 = new ExcelPrinter();
            Header.Add("№ п/п");
            Header.Add("Наименование организации ");
            Header.Add("ФИО руководителя организации");
            Header.Add("Место расположения             ");
            Header.Add("Телефоны                                                          ");
            string dataStr = "";
            if (dTP5 != new DateTime())
            {
                DateTime toName = dTP5;// dTP5.AddMonths(-1);
                dataStr = MonthToStr(toName.Month) + " " + toName.Year + "г.";
            }
            report1.NameTable("Перечень арендаторов АО \"Компания Импульс\",  " + dataStr);
            report1.HeadTable(Header);
            report1.BodyTable(TableList);
            report1.BordersTable();
            report1.EndSheet();
        }
        public void test()
        {
            List<string> Header = new List<string>();
            List<Cell> Table = new List<Cell>();
            Header.Add("№ п/п");
            Header.Add("Наименование организации ");
            Cell cell = new Cell();
            cell.ForeColor = Color.Red;
            cell.ColorInterior = Color.Green;
            string s = cell.font.Italic.ToString();
            cell.value = "123123";
           // cell.border.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;-		cell	font = {Name = "ISOCPEUR" Size=10}	MyTable.Cell

            ExcelPrinter report1 = new ExcelPrinter();
            report1.NameTable("Перечень арендаторов АО \"Компания Импульс\",  ");
            report1.HeadTable(Header);
            Table.Add(cell);
            Table.Add(cell);
            report1.BodyTable1(Table);
        }
    }
}
