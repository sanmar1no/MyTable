using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using System.Diagnostics;
//using NPOI.HSSF.UserModel; //для xls
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;



namespace MyTable
{
    class NPOIPrinter
    {
        // Создаём экземпляр нашего приложения
        // в NPOI не нужно
        // Создаём экземпляр рабочий книги Excel
        private static IWorkbook workbook = Cell.workbook;
        // IWorkbook workbook = new HSSFWorkbook();//xls
        // Создаём экземпляр листа Excel
        private static ISheet sheet =  workbook.CreateSheet("Лист1");
        // Создаём экземпляр области ячеек Excel
        private static IRow rowSheet = sheet.CreateRow(0);
        private ICell cell = rowSheet.CreateCell(0);
        private IFont fontBody = workbook.CreateFont();//осовной стиль таблицы
        private ICellStyle bodyStyle = workbook.CreateCellStyle();
        private IFont fontDynamic = workbook.CreateFont(); //индивидуально для ячейки
        private ICellStyle styleDynamic =  workbook.CreateCellStyle();

        //IRow row = sheet.GetRow(1);
        public Company company = new Company();

        private int k = 0;
        private int row = 0;
        private int countColumn = 0;

        FileStream stream = new FileStream("outfile.xlsx", FileMode.Create, FileAccess.Write);
        public NPOIPrinter() : this(new Company())
        {
        }

        public NPOIPrinter(Company company)
        {
            this.company = company;
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

            //
            IFont font = workbook.CreateFont();
            font.FontName = "Times New Roman";            
            font.FontHeightInPoints = 24;
            font.IsBold = true;
            ICellStyle headStyle = workbook.CreateCellStyle();

            //font.Color = IndexedColors.Red.Index;
            //font.Color = ColorConvert(System.Drawing.Color.Red);

            headStyle.SetFont(font);//стиль заголовка
            headStyle.Alignment = HorizontalAlignment.Center;


            rowSheet.Cells[0].CellStyle = headStyle;//назначаем стиль заголовка

            fontBody.FontName = "ISOCPEUR";
            fontBody.FontHeightInPoints = 12;
            fontBody.IsBold = true;
            bodyStyle.SetFont(fontBody);//основной стиль таблицы

            var range = new CellRangeAddress(row, row, 0, 6);
            sheet.AddMergedRegion(range);

            switch (company)
            {
               // IRow row = sheet.CreateRow(0);
               // ICell cell = row.CreateCell(0);
                case Company.Impuls:
                    cell.SetCellValue("АО «Компания Импульс»");
                    break;
                case Company.SKB:
                    cell.SetCellValue("ООО «СКБ-Сбытсервис»");
                    break;
            }            
            row ++;
            AddRow("350072, Краснодарский  край, г.Краснодар,");
            AddRow("Ул. Московская, 5.");
            //sheet.GetRow(row).Height = 400;
            rowSheet.Height = 400;
            switch (company)
            {
                case Company.Impuls:
                    AddRow("Тел. 8(861) 252-11-21");
                    break;
                case Company.SKB:
                    AddRow("Тел. 8(861) 252-09-83");
                    break;
            }
            sheet.GetRow(row).Height = 400;
            //rowSheet.Height = 18;
        }

        //добавить строку, значение запишется в первую ячейку
        private void AddRow(string s)
        {
            AddRow(s, bodyStyle);
        }

        //добавить строку с указанием стиля
        private void AddRow(string s, ICellStyle Style)
        {
            row++;
            rowSheet = (XSSFRow)sheet.CreateRow(row);
            AddCell(s, 0, Style);
        }

        //добавить ячейку в текущей строке, стиль по умолчанию bodyStyle
        private void AddCell(string s, int index)
        {
            AddCell(s, index, bodyStyle);
        }

        //добавить ячейку в текущей строке с указанием стиля
        private void AddCell(string s,int index, ICellStyle Style1, CellType type)
        {
            while (rowSheet.Cells.Count <= index)
            {
                int countCell = rowSheet.Cells.Count;
                cell = (XSSFCell)rowSheet.CreateCell(countCell);
                cell.SetCellValue(s);

                //cell.Style.Numberformat.Format = "0.0";

                rowSheet.Cells[countCell].CellStyle = Style1;
                cell.SetCellType(type);
            }
        }
        private void AddCell(string s, int index, ICellStyle Style)
        {
            AddCell(s, index, Style, CellType.String);
        }

        /*
         ReportPrinter() : this(new NPOIPrinter.Company(), DateTime.Now, new DateTime())
         */
        //добавить строку, используя Cell
        private void AddRow(Cell cell1)
        {
            AddRow(cell1.Value, cell1.styleDynamic);
        }

        //добавить ячейку в текущей строке, используя класс Cell
        private void AddCell(Cell cell1,int index)
        {
            AddCell(cell1.Value, index, cell1.styleDynamic);
        }
        //Укажем арендатора в шапке отчета
        public void HeadArenda(string arendaCB23 = "")
        {//userCB23 - Арендатор, dTP5 - начало периода, dTP6 - конец периода
            row ++;
            AddRow("Потребитель: " + arendaCB23);
            AddRow("Адрес объекта: г.Краснодар, ул. Московская, 5.");
            sheet.GetRow(row).Height = 500;
            row += 2;
        }
        
        //задаем имя Таблицы
        public void NameTable(string name)
        {
            AddRow(name);
            sheet.GetRow(row).Height = 500;
        }

        //заголовок(шапка) таблицы
        public void HeadTable(List<Cell> List)
        {
            row++;
            rowSheet = (XSSFRow)sheet.CreateRow(row);
            double lenght = 0;
            foreach (Cell elem in List)
            {
                lenght += elem.Value.Length;
            }
            double koeff = 80 / lenght;
            for (int i = 0; i < List.Count(); i++)
            {
                int mat1 = (int)Math.Round(List[i].Value.Length * koeff, 0, MidpointRounding.AwayFromZero);
                sheet.SetColumnWidth(i,mat1*300);
                //sheet.AutoSizeColumn(i);
                AddCell(List[i].Value, i, List[i].styleDynamic);
            }
            countColumn = List.Count() - 1;
        }

        //заполнение таблицы из List<Cell>
        public void BodyTable(List<Cell> Temp)
        {
            //row++;
            if (Temp.Count > 0)
            {
                for (; k < (Temp.Count) / countColumn; k++)
                {
                    AddRow((k + 1).ToString() + ".", Temp[k * countColumn].styleDynamic);
                    for (int i = 0; i < countColumn; i++)
                    {
                        if (Temp[k * countColumn + i] != null)
                        {
                            AddCell(Temp[k * countColumn + i].Value, i+1, Temp[k * countColumn + i].styleDynamic, Temp[k * countColumn + i].Type);
                        }
                        else AddCell("", i+1);
                    }
                }
            }
        }

        //последняя строка Всего:
        public void FooterTableSumm(string literColumn)
        { //заполнить
            //AddRow("",new Cell("",Cell.Style.bold).styleDynamic);
            AddRow(new Cell("", Cell.Style.bold));
            AddCell(new Cell("Всего:", Cell.Style.bold),1);
            AddCell(new Cell("", Cell.Style.bold), countColumn);
           // var range = new CellRangeAddress(row, row+k-1, countColumn, countColumn);
            if (k > 0)
            {
                cell.CellFormula = "SUM(" + literColumn + (row-k+1).ToString() + ":" + literColumn + row.ToString() + ")";
               // s1.GetRow(1).CreateCell(3).CellFormula = "SUM(A2:C2)";
            }
            else
            {
                cell.SetCellValue("0");
            }

          /*      Excel1.Range formulaRange = sheet.Range[sheet.Cells[row, countColumn + 1], sheet.Cells[row - 1 + k, countColumn + 1]];
            string ToAdresEx = formulaRange.get_Address(1, 1, Excel1.XlReferenceStyle.xlR1C1, Type.Missing, Type.Missing);
            if (k > 0) sheet.Cells[row + k, countColumn + 1].Formula = "=SUM(" + literColumn + row.ToString() + ":" + literColumn + (row - 1 + k).ToString() + ")";//формула (сумма)
            else sheet.Cells[row + k, countColumn + 1] = "0";*/
        }

        //последняя строка Всего:
        public void FooterTableCount()
        { 
        
        }

        //границы и стиль таблицы
        public void BordersTable()
        { 
        
        }

        //подпись
        public void EndSheet()        
        {
            FileStream sw = File.Create("test.xlsx");
            workbook.Write(sw);
            sw.Close();
        }

        //тест
        public void Hello()
        {
            /* IRow row = sheet.CreateRow(0);
             ICell cell = row.CreateCell(0);
             cell.SetCellValue("Hello");
             cell = row.CreateCell(1);
             cell.SetCellValue("World");

             var range = new NPOI.SS.Util.CellRangeAddress(1, 6, 2, 5);
             sheet.AddMergedRegion(range);*/

            ISheet sheet1 = workbook.CreateSheet("Sheet1");

            //fill background
            ICellStyle style1 = workbook.CreateCellStyle();
            style1.FillForegroundColor = IndexedColors.Blue.Index;
            style1.FillPattern = FillPattern.SolidForeground;
           // style1.FillBackgroundColor = IndexedColors.Pink.Index;
            sheet1.CreateRow(0).CreateCell(0).CellStyle = style1;

            //fill background
            ICellStyle style2 = workbook.CreateCellStyle();
           // style2.FillForegroundColor = IndexedColors.Yellow.Index;
            style2.FillPattern = FillPattern.SolidForeground;
            style2.FillBackgroundColor = IndexedColors.Rose.Index;
            sheet1.CreateRow(1).CreateCell(0).CellStyle = style2;

            //fill background
            ICellStyle style3 = workbook.CreateCellStyle();
            style3.FillForegroundColor = IndexedColors.Lime.Index;
            style3.FillPattern = FillPattern.SolidForeground;
            style3.FillBackgroundColor = IndexedColors.LightGreen.Index;
            sheet1.CreateRow(2).CreateCell(0).CellStyle = style3;

            //fill background
            ICellStyle style4 = workbook.CreateCellStyle();
            style4.FillForegroundColor = IndexedColors.Blue.Index;
            style4.FillPattern = FillPattern.SolidForeground;
            style4.FillBackgroundColor = IndexedColors.Blue.Index;
            sheet1.CreateRow(3).CreateCell(0).CellStyle = style4;

            //fill background
            ICellStyle style5 = workbook.CreateCellStyle();
            style5.FillForegroundColor = IndexedColors.LightBlue.Index;
            style5.FillPattern = FillPattern.Bricks;
            style5.FillBackgroundColor = IndexedColors.Plum.Index;
            sheet1.CreateRow(4).CreateCell(0).CellStyle = style5;

            //fill background
            ICellStyle style6 = workbook.CreateCellStyle();
            style6.FillForegroundColor = IndexedColors.SeaGreen.Index;
            style6.FillPattern = FillPattern.FineDots;
            style6.FillBackgroundColor = IndexedColors.White.Index;
            sheet1.CreateRow(5).CreateCell(0).CellStyle = style6;

            //fill background
            ICellStyle style7 = workbook.CreateCellStyle();
            style7.FillForegroundColor = IndexedColors.Orange.Index;
            style7.FillPattern = FillPattern.Diamonds;
            style7.FillBackgroundColor = IndexedColors.Orchid.Index;
            sheet1.CreateRow(6).CreateCell(0).CellStyle = style7;

            //fill background
            ICellStyle style8 = workbook.CreateCellStyle();
            style8.FillForegroundColor = IndexedColors.White.Index;
            style8.FillPattern = FillPattern.Squares;
            style8.FillBackgroundColor = IndexedColors.Red.Index;
            sheet1.CreateRow(7).CreateCell(0).CellStyle = style8;

            //fill background
            ICellStyle style9 = workbook.CreateCellStyle();
            style9.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            style9.FillPattern = FillPattern.SparseDots;
            style9.FillBackgroundColor = IndexedColors.Yellow.Index;
            sheet1.CreateRow(8).CreateCell(0).CellStyle = style9;

            //fill background
            ICellStyle style10 = workbook.CreateCellStyle();
            style10.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            style10.FillPattern = FillPattern.ThinBackwardDiagonals;
            style10.FillBackgroundColor = IndexedColors.Yellow.Index;
            sheet1.CreateRow(9).CreateCell(0).CellStyle = style10;

            //fill background
            ICellStyle style11 = workbook.CreateCellStyle();
            style11.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            style11.FillPattern = FillPattern.ThickForwardDiagonals;
            style11.FillBackgroundColor = IndexedColors.Yellow.Index;
            sheet1.CreateRow(10).CreateCell(0).CellStyle = style11;

            //fill background
            ICellStyle style12 = workbook.CreateCellStyle();
            style12.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            style12.FillPattern = FillPattern.ThickHorizontalBands;
            style12.FillBackgroundColor = IndexedColors.Yellow.Index;
            sheet1.CreateRow(11).CreateCell(0).CellStyle = style12;


            //fill background
            ICellStyle style13 = workbook.CreateCellStyle();
            style13.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            style13.FillPattern = FillPattern.ThickVerticalBands;
            style13.FillBackgroundColor = IndexedColors.Yellow.Index;
            sheet1.CreateRow(12).CreateCell(0).CellStyle = style13;

            //fill background
            ICellStyle style14 = workbook.CreateCellStyle();
            style14.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            style14.FillPattern = FillPattern.ThickBackwardDiagonals;
            style14.FillBackgroundColor = IndexedColors.Yellow.Index;
            sheet1.CreateRow(13).CreateCell(0).CellStyle = style14;

            //fill background
            ICellStyle style15 = workbook.CreateCellStyle();
            style15.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            style15.FillPattern = FillPattern.ThinForwardDiagonals;
            style15.FillBackgroundColor = IndexedColors.Yellow.Index;
            sheet1.CreateRow(14).CreateCell(0).CellStyle = style15;

            //fill background
            ICellStyle style16 = workbook.CreateCellStyle();
            style16.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            style16.FillPattern = FillPattern.ThinHorizontalBands;
            style16.FillBackgroundColor = IndexedColors.Yellow.Index;
            sheet1.CreateRow(15).CreateCell(0).CellStyle = style16;

            //fill background
            ICellStyle style17 = workbook.CreateCellStyle();
            style17.FillForegroundColor = IndexedColors.RoyalBlue.Index;
            style17.FillPattern = FillPattern.ThinVerticalBands;
            style17.FillBackgroundColor = IndexedColors.Yellow.Index;
            sheet1.CreateRow(16).CreateCell(0).CellStyle = style17;

            FileStream sw = File.Create("test.xlsx");
            workbook.Write(sw);
            sw.Close();
            //workbook.Write(stream);

        }
    }
}
