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


namespace MyTable {
    class NPOIPrinter {
        // Создаём экземпляр нашего приложения
        // в NPOI не нужно
        // Создаём экземпляр рабочий книги Excel
        private static IWorkbook workbook = new XSSFWorkbook();

        // IWorkbook workbook = new HSSFWorkbook();//xls
        // Создаём экземпляр листа Excel
        private static ISheet sheet = workbook.CreateSheet("Лист1");

        // Создаём экземпляр области ячеек Excel
        private static IRow rowSheet = sheet.CreateRow(0);
        ICell cell = rowSheet.CreateCell(0);
        
        IFont fontBody = workbook.CreateFont();                 //основной стиль таблицы
        ICellStyle bodyStyle = workbook.CreateCellStyle();
        
        IFont fontDynamic = workbook.CreateFont();              //индивидуально для ячейки
        ICellStyle styleDynamic = workbook.CreateCellStyle();

        //IRow row = sheet.GetRow(1);
        public Company company = new Company();

        private int k = 0;
        private int row = 0;
        private int countColumn = 0;

        //FileStream stream = new FileStream("outfile.xlsx", FileMode.Create, FileAccess.Write);

        public NPOIPrinter() : this(new Company()) {
        }

        public NPOIPrinter(Company company) {
            //this.company = company;
            //Head();
        }

        public enum Company {
            Impuls = 0,
            SKB
        }

        private short ColorConvert(System.Drawing.Color color) {
            /*string s= System.Drawing.KnownColor.White.ToString();           
            if (color.IsKnownColor)
            {
                s = NPOI.SS.UserModel.IndexedColors.ValueOf(color.Name).HexString;
            }
            short colorI = NPOI.SS.UserModel.IndexedColors.ValueOf(s).Index;
            return colorI;*/

            byte[] rgb = new byte[3] { color.R, color.G, color.B };
            XSSFColor colorX= new XSSFColor(rgb);
            
            return colorX.Indexed;
            
            /*
             byte[] rgb = new byte[3] { 192, 0, 0 };
             XSSFCellStyle HeaderCellStyle1 = (XSSFCellStyle)xssfworkbook.CreateCellStyle();
             HeaderCellStyle1.SetFillForegroundColor(new XSSFColor(rgb));
             */
        }

        public void Head() {
            //заголовок

            IFont font = workbook.CreateFont();
            font.FontName = "Times New Roman";            
            font.FontHeightInPoints = 24;
            font.IsBold = true;

            ICellStyle headStyle = workbook.CreateCellStyle();

            //font.Color = IndexedColors.Red.Index;
            font.Color = ColorConvert(System.Drawing.Color.Red);
            
            headStyle.SetFont(font);//стиль заголовка
            headStyle.Alignment = HorizontalAlignment.Center;
            //headStyle.FillPattern = FillPattern.SolidForeground;

            rowSheet.Cells[0].CellStyle = headStyle;    //назначаем стиль заголовка

            fontBody.FontName = "ISOCPEUR";
            fontBody.FontHeightInPoints = 12;
            fontBody.IsBold = true;
            bodyStyle.SetFont(fontBody);                //основной стиль таблицы

            var range = new NPOI.SS.Util.CellRangeAddress(row, row, 0, 6);
            sheet.AddMergedRegion(range);

            switch (company) {
               // IRow row = sheet.CreateRow(0);
               // ICell cell = row.CreateCell(0);
                case Company.Impuls:
                    cell.SetCellValue("АО «Компания Импульс»");
                    break;
                case Company.SKB:
                    cell.SetCellValue("ООО «СКБ-Сбытсервис»");
                    break;
            }            

            row++;
            AddRow("350072, Краснодарский  край, г.Краснодар,");
            AddRow("Ул. Московская, 5.");

            //sheet.GetRow(row).Height = 400;

            rowSheet.Height = 400;

            switch (company) {
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
        void AddRow(string s) {
            AddRow(s, bodyStyle);
        }

        //добавить строку с указанием стиля
        void AddRow(string s, ICellStyle Style) {
            row++;
            rowSheet = sheet.CreateRow(row);
            AddCell(s, 0, Style);
        }

        //добавить ячейку в текущей строке, стиль по умолчанию bodyStyle
        void AddCell(string s, int index) {
            AddCell(s, index, bodyStyle);
        }

        //добавить ячейку в текущей строке с указанием стиля
        void AddCell(string s,int index, ICellStyle Style) {
            cell = rowSheet.CreateCell(index);
            cell.SetCellValue(s);
            rowSheet.Cells[0].CellStyle = Style;
        }

        //userCB23 - Арендатор, dTP5 - начало периода, dTP6 - конец периода
        public void HeadArenda(string arendaCB23 = "") {
            row ++;
            AddRow("Потребитель: " + arendaCB23);
            AddRow("Адрес объекта: г.Краснодар, ул. Московская, 5.");
            sheet.GetRow(row).Height = 500;
            row += 2;
        }

        //задаем имя Таблицы
        public void NameTable(string name) {
            AddRow(name);
            sheet.GetRow(row).Height = 500;
        }

        //заголовок таблицы
        public void HeadTable(List<string> List) {
            row++;
            rowSheet = sheet.CreateRow(row);
            double lenght = 0;
            foreach (string elem in List) {
                lenght += elem.Length;
            }

            double koeff = 80 / lenght;

            for (int i = 1; i <= List.Count(); i++) {
                int mat1 = (int)Math.Round(List[i - 1].Length * koeff, 0, MidpointRounding.AwayFromZero);
                sheet.SetColumnWidth(i,mat1);
                AddCell(List[i - 1], i);
            }

            countColumn = List.Count() - 1;
        }

        //заполнение таблицы из List<Cell>
        /*
        public void BodyTable(List<Cell> Temp) {
            row++;
            if (Temp.Count > 0)
            {
                for (; k < (Temp.Count) / countColumn; k++)
                {
                    
                    fontDynamic.Color= ColorConvert(Temp[k * countColumn].ForeColor);
                    fontDynamic.FontHeight= Temp[k * countColumn].font.Size;
                    fontDynamic.IsItalic= Temp[k * countColumn].font.Italic;
                    fontDynamic.IsBold = Temp[k * countColumn].font.Bold;
                    styleDynamic.FillBackgroundColor = ColorConvert(Temp[k * countColumn].ColorInterior);
                    styleDynamic.BorderBottom = Temp[k * countColumn].LineStyle;
                    fontDynamic.Boldweight =  Temp[k * countColumn].Weight;

                    sheet.Cells[row + k, 1].Font.Color = Temp[k * countColumn].ForeColor;
                    sheet.Cells[row + k, 1].Font.Size = Temp[k * countColumn].font.Size;
                    sheet.Cells[row + k, 1].Font.Italic = Temp[k * countColumn].font.Italic;
                    sheet.Cells[row + k, 1].Font.Bold = Temp[k * countColumn].font.Bold;
                    sheet.Cells[row + k, 1].Interior.Color = Temp[k * countColumn].ColorInterior;
                    sheet.Cells[row + k, 1].Borders.LineStyle = Temp[k * countColumn].LineStyle;
                    sheet.Cells[row + k, 1].Borders.Weight = Temp[k * countColumn].Weight;

                    sheet.Cells[row + k, 1] = (k + 1).ToString() + ".";
                    for (int i = 0; i < countColumn; i++)
                    {
                        if (Temp[k * countColumn + i] != null)
                        {
                            sheet.Cells[row + k, i + 2].Font.Color = Temp[k * countColumn + i].ForeColor;
                            sheet.Cells[row + k, i + 2].Font.Size = Temp[k * countColumn + i].font.Size;
                            sheet.Cells[row + k, i + 2].Font.Italic = Temp[k * countColumn + i].font.Italic;
                            sheet.Cells[row + k, i + 2].Font.Bold = Temp[k * countColumn + i].font.Bold;
                            sheet.Cells[row + k, i + 2].Interior.Color = Temp[k * countColumn + i].ColorInterior;
                            sheet.Cells[row + k, i + 2].Borders.LineStyle = Temp[k * countColumn + i].LineStyle;
                            sheet.Cells[row + k, i + 2].Borders.Weight = Temp[k * countColumn + i].Weight;
                            sheet.Cells[row + k, i + 2] = Temp[k * countColumn + i].Value;
                        }
                        else sheet.Cells[row + k, i + 2] = "";
                    }
                }
            }
        }//*/

        //тест
        public void Hello() {
            /* 
             IRow row = sheet.CreateRow(0);
             ICell cell = row.CreateCell(0);
             cell.SetCellValue("Hello");
             cell = row.CreateCell(1);
             cell.SetCellValue("World");

             var range = new NPOI.SS.Util.CellRangeAddress(1, 6, 2, 5);
             sheet.AddMergedRegion(range);
             */

            //workbook.Write(stream);
            


            //Код SCH
            //Создаём книгу
            IWorkbook book = new XSSFWorkbook();
            
            //Создаём лист в книге
            ISheet sheet = book.CreateSheet("test");

            //1
            IRow row = sheet.CreateRow(1);
            ICell cell = row.CreateCell(1);
            cell.SetCellValue("Hello World");

            //2
            row = sheet.CreateRow(2);
            cell = row.CreateCell(2);
            cell.SetCellValue("Hello World");
            sheet.AutoSizeColumn(2);

            //3
            row = sheet.CreateRow(3);
            cell = row.CreateCell(3, CellType.String);

            ICellStyle style = book.CreateCellStyle();
            //BorderStyle border = style.BorderBottom();

            IFont font = book.CreateFont();
            font.FontName = "Times New Roman";
            font.IsBold = true;
            font.FontHeightInPoints = 11.0;
            font.Color = IndexedColors.Red.Index;
            //font.Color = new XSSFColor(System.Drawing.Color.DarkBlue).Indexed; - Не заработало

            style.SetFont(font);

            cell.CellStyle = style;
            cell.SetCellValue("Hello World");
            sheet.AutoSizeColumn(3);

            //4
            row = sheet.CreateRow(4);
            cell = row.CreateCell(4);

            IDataFormat format = book.CreateDataFormat();
            XSSFCellStyle dateStyle = (XSSFCellStyle) book.CreateCellStyle();
            dateStyle.SetDataFormat(format.GetFormat("dd.mm.yyyy"));

            cell.CellStyle = dateStyle;

            cell.SetCellValue(new DateTime(2020, 11, 15));
            sheet.AutoSizeColumn(4);

            //5
            row = sheet.CreateRow(5);
            cell = row.CreateCell(5);

            XSSFCellStyle styleB = (XSSFCellStyle) book.CreateCellStyle();
            styleB.BorderTop = BorderStyle.Medium;
            styleB.BorderLeft = BorderStyle.Medium;
            styleB.BorderRight = BorderStyle.Medium;
            styleB.BorderBottom = BorderStyle.Medium;

            cell.CellStyle = styleB;
            cell.SetCellValue(150);


            //Сохраняем готовую книгу в файл
            using (FileStream s = new FileStream("test.xlsx", FileMode.Create, FileAccess.Write)) {
                book.Write(s);
            }

        }
    }



}
