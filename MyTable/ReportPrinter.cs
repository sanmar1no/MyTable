using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace MyTable
{
    class ReportPrinter
    {
        public ExcelPrinter.Company company = new ExcelPrinter.Company();
        public ExcelPrinter.Report report = new ExcelPrinter.Report();
        public DateTime dTP5 = new DateTime();
        public DateTime dTP6 = new DateTime();
        public string arendaCB23 = "";
        private List<string> TableList = new List<string>();
        public ReportPrinter()
            : this(new ExcelPrinter.Company(), new ExcelPrinter.Report(), DateTime.Now, new DateTime())
        {
        }
        public ReportPrinter(ExcelPrinter.Company company)
            : this(company, new ExcelPrinter.Report(), DateTime.Now, new DateTime())
        {
        }
        public ReportPrinter(ExcelPrinter.Company company, ExcelPrinter.Report report)
            : this(company, report, DateTime.Now, new DateTime())
        {
        }
        public ReportPrinter(ExcelPrinter.Company company, ExcelPrinter.Report report, DateTime dTP5)
            : this(company, report, dTP5, new DateTime())
        {
        }
        public ReportPrinter(ExcelPrinter.Company company, ExcelPrinter.Report report, DateTime dTP5, DateTime dTP6)
        {
            this.company = company;
            this.report = report;
            this.dTP5 = dTP5;
            this.dTP6 = dTP6;
        }
        public void AddList(List<string> List)
        {
            TableList.Clear();
            TableList.AddRange(List);
            startReport();
        }
        private void startReport()
        {
            ExcelPrinter report1 = new ExcelPrinter();
            switch (report)
            {
                case ExcelPrinter.Report.countersPeriod:                    //отчет по расходу электросчетчиков за период 
                    report1.company = ExcelPrinter.Company.SKB;
                    report1.report = ExcelPrinter.Report.countersPeriod;
                    report1.dTP5 = dTP5;
                    report1.dTP6 = dTP6;
                    report1.arendaCB23 = arendaCB23;
                    report1.headName();
                    report1.bodyTable(TableList);
                    report1.footerTableSumm("G");
                    report1.bordersTable();
                    report1.endSheet();
                    break;
                case ExcelPrinter.Report.countersInventoryElectro:          //инвентаризация электросчетчиков
                    report1.company = ExcelPrinter.Company.Impuls;
                    report1.report = ExcelPrinter.Report.countersInventoryElectro;
                    report1.headName();
                    report1.bodyTable(TableList);
                    report1.footerTableCount();
                    report1.bordersTable();
                    report1.endSheet();
                    break;
                case ExcelPrinter.Report.countersInventoryAqua:             //инвентаризация водомеров
                    report1.company = ExcelPrinter.Company.Impuls;
                    report1.report = ExcelPrinter.Report.countersInventoryAqua;
                    report1.headName();
                    report1.bodyTable(TableList);
                    report1.footerTableCount();
                    report1.bordersTable();
                    report1.endSheet();
                    break;
                case ExcelPrinter.Report.countersPeriodAll:                 //Основной отчет по электроэнергии (период - месяц)
                    report1.company = ExcelPrinter.Company.SKB;
                    report1.report = ExcelPrinter.Report.countersPeriodAll;
                    report1.dTP5 = dTP5;
                    report1.headName();
                    report1.bodyTable(TableList);
                    report1.footerTableSumm("D");
                    report1.bordersTable();
                    report1.endSheet();
                    break;
            }
        }
    }
}
