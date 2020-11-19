using System;
using System.Collections.Generic;
using System.Drawing;

namespace MyTable
{
    class ReportPrinter
    {
        public NPOIPrinter.Company company = new NPOIPrinter.Company();
        public Variables.UserKeyEnum reportKey = new Variables.UserKeyEnum();
        public DateTime dTP5 = new DateTime();
        public DateTime dTP6 = new DateTime();
        private List<Cell> TableList = new List<Cell>();

        public ReportPrinter()
            : this(new NPOIPrinter.Company(), DateTime.Now, new DateTime())
        {
        }
        public ReportPrinter(NPOIPrinter.Company company)
            : this(company, DateTime.Now, new DateTime())
        {
        }
        /* public ReportPrinter(NPOIPrinter.Company company, Variables.userKeyEnum report)
             : this(company, DateTime.Now, new DateTime())
         {
                    this.reportKey = reportKey;
         }*/
        public ReportPrinter(NPOIPrinter.Company company, DateTime dTP5)
            : this(company, dTP5, new DateTime())
        {
        }
        public ReportPrinter(NPOIPrinter.Company company, DateTime dTP5, DateTime dTP6)
        {
            this.company = company;
            this.dTP5 = dTP5;
            this.dTP6 = dTP6;
        }
        public void AddList(List<Cell> List)
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
            List<Cell> Header = new List<Cell>();
            NPOIPrinter report1 = new NPOIPrinter(company);
            DateTime data = dTP6.AddMonths(1);
            //List<string> Header = new List<string>(){ "№", "№ точки учета по договору", "№ счетчика", "Показания на  01."+dTP5.Month + "." + dTP5.Year, "Показания на 01." + data.Month + "." + data.Year, "Расч.Коэфф.", "Расход, кВт.ч."};
            Header.Add(new Cell("№ п/п",Cell.Style.bold));
            Header.Add(new Cell("№ точки учета по договору", Cell.Style.bold));
            Header.Add(new Cell("№ счетчика    ", Cell.Style.bold));
            Header.Add(new Cell("Показания на  01." + dTP5.Month + "." + dTP5.Year, Cell.Style.bold));
            Header.Add(new Cell("Показания на 01." + data.Month + "." + data.Year, Cell.Style.bold));
            Header.Add(new Cell("Расч.Коэфф.", Cell.Style.bold));
            Header.Add(new Cell("Расход, кВт.ч.", Cell.Style.bold));
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
            List<Cell> Header = new List<Cell>();
            NPOIPrinter report1 = new NPOIPrinter(company);
            //report1.company = NPOIPrinter.Company.Impuls;//исправить
            Header.Add(new Cell("№ п/п", Cell.Style.bold));
            Header.Add(new Cell("№ Корпуса и помещения", Cell.Style.bold));
            Header.Add(new Cell("№ счетчика    ", Cell.Style.bold));
            Header.Add(new Cell("Марка счетчика", Cell.Style.bold));
            Header.Add(new Cell("Год выпуска/поверки", Cell.Style.bold));
            Header.Add(new Cell("Показания (последние), " + (reportKey == Variables.UserKeyEnum.electro ? "кВт*ч" : "куб.м."), Cell.Style.bold));
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
            List<Cell> Header = new List<Cell>();
            NPOIPrinter report1 = new NPOIPrinter(company);
            //report1.company = NPOIPrinter.Company.SKB;//исправить
            Header.Add(new Cell("№ п/п", Cell.Style.bold));
            Header.Add(new Cell("Арендатор  ", Cell.Style.bold));
            Header.Add(new Cell("№ счетчика  ", Cell.Style.bold));
            Header.Add(new Cell("Расход, кВт*ч", Cell.Style.bold));
            string dataStr = "";
            if (dTP5 != new DateTime())
            {
                DateTime toName = dTP5;// dTP5.AddMonths(-1);
                dataStr = MonthToStr(toName.Month) + " " + toName.Year + "г.";
            }
            report1.NameTable("Расчет количества потребленной электроэнергии за " + dataStr);
           // report1.HeadTable(Header);
            report1.BodyTable(TableList);
            report1.FooterTableSumm("D");
            report1.BordersTable();
            report1.EndSheet();
        }
        public void ReportArendaPhoneBook()
        {
            List<Cell> Header = new List<Cell>();
            NPOIPrinter report1 = new NPOIPrinter();
            Header.Add(new Cell("№ п/п", Cell.Style.bold));
            Header.Add(new Cell("Наименование организации ", Cell.Style.bold));
            Header.Add(new Cell("ФИО руководителя организации", Cell.Style.bold));
            Header.Add(new Cell("Место расположения             ", Cell.Style.bold));
            Header.Add(new Cell("Телефоны                                                          ", Cell.Style.bold));
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
            List<Cell> Header = new List<Cell>();
            List<Cell> Table = new List<Cell>();
            Header.Add(new Cell("№ п/п", Cell.Style.bold));
            Header.Add(new Cell("Наименование организации ", Cell.Style.bold));
            Cell cell = new Cell();
            cell.Value = "123123";
            NPOIPrinter report1 = new NPOIPrinter();
            report1.NameTable("Перечень арендаторов АО \"Компания Импульс\",  ");
            report1.HeadTable(Header);
            Table.Add(cell);
            Table.Add(cell);
            report1.BodyTable(Table);
        }
    }
}
