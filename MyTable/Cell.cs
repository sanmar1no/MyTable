
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Diagnostics;
namespace MyTable
{
    class Cell
    {
        // Создаём экземпляр рабочий книги Excel
        //public static IWorkbook workbook = new XSSFWorkbook();
        //private static IWorkbook workbook = Variables.workbook;
        private IFont fontDynamic = Variables.workbook.CreateFont();
        public ICellStyle styleDynamic = Variables.workbook.CreateCellStyle();
        public CellType Type = CellType.String;

        private IFont[] fontM = Variables.fontM;
        private ICellStyle[] styleM = Variables.styleM;

        public enum Style
        {
            normal,
            bold,
            summ,
            clientCame,
            clientOut,
            colorPink,
            noBorder
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
                    fontDynamic = fontM[0];
                    styleDynamic = styleM[0];
                    Type = CellType.String;
                    break;
                case Cell.Style.bold:
                    fontDynamic = fontM[1];
                    styleDynamic = styleM[1];
                    Type = CellType.String;
                    break;
                case Cell.Style.summ:
                    fontDynamic = fontM[2];
                    styleDynamic = styleM[2];
                    Type = CellType.Numeric;
                    break;
                case Cell.Style.clientCame:
                    fontDynamic = fontM[3];
                    styleDynamic = styleM[3];
                    Type = CellType.String;
                    break;
                case Cell.Style.clientOut:
                    fontDynamic = fontM[4];
                    styleDynamic = styleM[4];
                    Type = CellType.String;
                    break;
                case Cell.Style.colorPink:
                    fontDynamic = fontM[5];
                    styleDynamic = styleM[5];
                    Type = CellType.String;
                    break;
                case Cell.Style.noBorder:
                    fontDynamic = fontM[6];
                    styleDynamic = styleM[6];
                    Type = CellType.String;
                    break;
            }
        }

        private string valueS;
        private double valueD;
        public dynamic Value 
        {
            get
            {
                if (Type == CellType.Numeric)
                {
                    return valueD;
                }
                else return valueS;
            }
            set 
            {
                if (value != null)
                {
                    if (Type == CellType.Numeric)
                    {
                        valueD = double.Parse(value);
                    }
                    else valueS = value;
                }
                else
                {
                    valueS = "";
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
            //SetStyle(style);
            fontDynamic = Variables.workbook.CreateFont();
            styleDynamic = Variables.workbook.CreateCellStyle();
            styleDynamic = ToStyle(style);            
            this.Value = Value;
        }
    }
}
