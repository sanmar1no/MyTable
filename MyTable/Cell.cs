
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
namespace MyTable
{
    class Cell
    {
        // Создаём экземпляр рабочий книги Excel
        public static IWorkbook workbook = new XSSFWorkbook();
        private IFont fontDynamic = workbook.CreateFont();
        public ICellStyle styleDynamic = workbook.CreateCellStyle();
        public CellType Type = new CellType();
        public enum Style
        {
            normal,
            bold,
            summ,
            clientCame,
            clientOut,
            colorPink
        }
        public ICellStyle ToStyle(Cell.Style style)
        {
            SetStyle(style);
            return styleDynamic;
        }
       private void SetStyle(Cell.Style style)
        {
            switch (style)
            {
                case Cell.Style.normal:
                    fontDynamic.FontName = "ISOCPEUR";
                    fontDynamic.FontHeightInPoints = 12;
                    fontDynamic.IsBold = false;
                    fontDynamic.IsItalic = true;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Thin;
                    styleDynamic.BorderBottom = BorderStyle.Thin;
                    styleDynamic.BorderRight = BorderStyle.Thin;
                    styleDynamic.BorderTop = BorderStyle.Thin;
                    Type = CellType.String;
                    break;
                case Cell.Style.bold:
                    fontDynamic.FontName = "ISOCPEUR";
                    fontDynamic.FontHeightInPoints = 12;
                    fontDynamic.IsBold = true;
                    fontDynamic.IsItalic = true;
                    //font.Color = IndexedColors.Red.Index;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Thick;
                    styleDynamic.BorderBottom = BorderStyle.Thick;
                    styleDynamic.BorderRight = BorderStyle.Thick;
                    styleDynamic.BorderTop = BorderStyle.Thick;
                    Type = CellType.String;
                    break;
                case Cell.Style.summ:
                    fontDynamic.FontName = "ISOCPEUR";
                    fontDynamic.FontHeightInPoints = 12;
                    fontDynamic.IsBold = true;
                    fontDynamic.IsItalic = true;
                    //font.Color = IndexedColors.Red.Index;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Thin;
                    styleDynamic.BorderBottom = BorderStyle.Thin;
                    styleDynamic.BorderRight = BorderStyle.Thin;
                    styleDynamic.BorderTop = BorderStyle.Thin;
                    Type = CellType.Numeric;
                    //  styleDynamic.DataFormat = XSSFFormulaEvaluator.Create(workbook,)
                    break;
                case Cell.Style.clientCame:
                    fontDynamic.FontName = "ISOCPEUR";
                    fontDynamic.FontHeightInPoints = 12;
                    fontDynamic.IsBold = true;
                    fontDynamic.IsItalic = true;
                    //fontDynamic.Color = IndexedColors.Aqua.Index;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Medium;
                    styleDynamic.BorderBottom = BorderStyle.Medium;
                    styleDynamic.BorderRight = BorderStyle.Medium;
                    styleDynamic.BorderTop = BorderStyle.Medium;
                    styleDynamic.FillForegroundColor = IndexedColors.Aqua.Index;
                    styleDynamic.FillPattern = FillPattern.SolidForeground;
                    Type = CellType.String;
                    break;
                case Cell.Style.clientOut:
                    fontDynamic.FontName = "ISOCPEUR";
                    fontDynamic.FontHeightInPoints = 12;
                    fontDynamic.IsBold = true;
                    fontDynamic.IsItalic = true;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Medium;
                    styleDynamic.BorderBottom = BorderStyle.Medium;
                    styleDynamic.BorderRight = BorderStyle.Medium;
                    styleDynamic.BorderTop = BorderStyle.Medium;
                    styleDynamic.FillForegroundColor = IndexedColors.LightGreen.Index;
                    styleDynamic.FillPattern = FillPattern.SolidForeground;
                    Type = CellType.String;
                    break;
                case Cell.Style.colorPink:
                    fontDynamic.FontName = "ISOCPEUR";
                    fontDynamic.FontHeightInPoints = 12;
                    fontDynamic.IsBold = false;
                    fontDynamic.IsItalic = true;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Medium;
                    styleDynamic.BorderBottom = BorderStyle.Medium;
                    styleDynamic.BorderRight = BorderStyle.Medium;
                    styleDynamic.BorderTop = BorderStyle.Medium;
                    styleDynamic.FillForegroundColor = IndexedColors.Pink.Index;
                    styleDynamic.FillPattern = FillPattern.SolidForeground;
                    Type = CellType.String;
                    break;
            }
        }
        private string value1;
        public string Value 
        {
            get { return value1; }
            set 
            {
                if (value != null)
                {
                    value1 = value;
                }
                else
                {
                    value1 = "";
                }
            }
        }
        public Cell() : this("", Style.normal)
        { 
        }
        public Cell(string Value) : this(Value,Style.normal)
        {
        }
      /* public Cell(string Value, Cell.Style style) : this(Value, style)
        {
        }*/
        public Cell(string Value, Cell.Style style)
        {
            this.Value = Value;
            //SetStyle(style);
            styleDynamic = ToStyle(style);
            
        }
    }
}
