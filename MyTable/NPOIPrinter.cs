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



namespace MyTable
{
    class NPOIPrinter
    {
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
        IFont fontBody = workbook.CreateFont();//осовной стиль таблицы
        ICellStyle bodyStyle = workbook.CreateCellStyle();
        IFont fontDynamic = workbook.CreateFont(); //индивидуально для ячейки
        ICellStyle styleDynamic = workbook.CreateCellStyle();

        //IRow row = sheet.GetRow(1);
        public Company company = new Company();

        private int k = 0;
        private int row = 0;
        private int countColumn = 0;

        FileStream stream = new FileStream("outfile.xlsx", FileMode.Create, FileAccess.Write);
        public NPOIPrinter()
    : this(new Company())
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
        private short ColorConvert(System.Drawing.Color color)
        {
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
            font.Color = ColorConvert(System.Drawing.Color.Red);

            headStyle.SetFont(font);//стиль заголовка
            headStyle.Alignment = HorizontalAlignment.Center;
            //headStyle.FillPattern = FillPattern.SolidForeground;

            rowSheet.Cells[0].CellStyle = headStyle;//назначаем стиль заголовка

            fontBody.FontName = "ISOCPEUR";
            fontBody.FontHeightInPoints = 12;
            fontBody.IsBold = true;
            bodyStyle.SetFont(fontBody);//основной стиль таблицы



            var range = new NPOI.SS.Util.CellRangeAddress(row, row, 0, 6);
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

        void AddRow(string s)//добавить строку, значение запишется в первую ячейку
        {
            AddRow(s, bodyStyle);
        }
        void AddRow(string s, ICellStyle Style)//добавить строку с указанием стиля
        {
            row++;
            rowSheet = sheet.CreateRow(row);
            AddCell(s, 0, Style);
        }
        void AddCell(string s, int index)//добавить ячейку в текущей строке, стиль по умолчанию bodyStyle
        {
            AddCell(s, index, bodyStyle);
        }
        void AddCell(string s,int index, ICellStyle Style)//добавить ячейку в текущей строке с указанием стиля
        {
            cell = rowSheet.CreateCell(index);
            cell.SetCellValue(s);
            rowSheet.Cells[0].CellStyle = Style;
        }
        public void HeadArenda(string arendaCB23 = "")
        {//userCB23 - Арендатор, dTP5 - начало периода, dTP6 - конец периода
            row ++;
            AddRow("Потребитель: " + arendaCB23);
            AddRow("Адрес объекта: г.Краснодар, ул. Московская, 5.");
            sheet.GetRow(row).Height = 500;
            row += 2;
        }
        public void NameTable(string name)//задаем имя Таблицы
        {
            AddRow(name);
            sheet.GetRow(row).Height = 500;
        }
        public void HeadTable(List<string> List)//заголовок таблицы
        {
            row++;
            rowSheet = sheet.CreateRow(row);
            double lenght = 0;
            foreach (string elem in List)
            {
                lenght += elem.Length;
            }
            double koeff = 80 / lenght;
            for (int i = 1; i <= List.Count(); i++)
            {
                int mat1 = (int)Math.Round(List[i - 1].Length * koeff, 0, MidpointRounding.AwayFromZero);
                sheet.SetColumnWidth(i,mat1);
                AddCell(List[i - 1], i);
            }
            countColumn = List.Count() - 1;
        }
        /*public void BodyTable(List<Cell> Temp)//заполнение таблицы из List<Cell>
        {
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
        public void Hello()//тест
        {
           /* IRow row = sheet.CreateRow(0);
            ICell cell = row.CreateCell(0);
            cell.SetCellValue("Hello");
            cell = row.CreateCell(1);
            cell.SetCellValue("World");

            var range = new NPOI.SS.Util.CellRangeAddress(1, 6, 2, 5);
            sheet.AddMergedRegion(range);*/
            workbook.Write(stream);

        }
    }



}
