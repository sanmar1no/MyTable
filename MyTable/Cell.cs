
using NPOI.SS.UserModel;
namespace MyTable
{
    class Cell
    {
        private IFont fontDynamic;
        public ICellStyle styleDynamic;
        public enum Style
        {
            normal,
            bold,
            clientCame,
            clientOut,
            colorPink
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
                    //font.Color = IndexedColors.Red.Index;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Thin;
                    styleDynamic.BorderBottom = BorderStyle.Thin;
                    styleDynamic.BorderRight = BorderStyle.Thin;
                    styleDynamic.BorderTop = BorderStyle.Thin;
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
                    break;
                case Cell.Style.clientCame:
                    fontDynamic.FontName = "ISOCPEUR";
                    fontDynamic.FontHeightInPoints = 12;
                    fontDynamic.IsBold = true;
                    fontDynamic.IsItalic = true;
                    fontDynamic.Color = IndexedColors.Aqua.Index;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Medium;
                    styleDynamic.BorderBottom = BorderStyle.Medium;
                    styleDynamic.BorderRight = BorderStyle.Medium;
                    styleDynamic.BorderTop = BorderStyle.Medium;
                    break;
                case Cell.Style.clientOut:
                    fontDynamic.FontName = "ISOCPEUR";
                    fontDynamic.FontHeightInPoints = 12;
                    fontDynamic.IsBold = true;
                    fontDynamic.IsItalic = true;
                    fontDynamic.Color = IndexedColors.LightGreen.Index;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Medium;
                    styleDynamic.BorderBottom = BorderStyle.Medium;
                    styleDynamic.BorderRight = BorderStyle.Medium;
                    styleDynamic.BorderTop = BorderStyle.Medium;
                    break;
                case Cell.Style.colorPink:
                    fontDynamic.FontName = "ISOCPEUR";
                    fontDynamic.FontHeightInPoints = 12;
                    fontDynamic.IsBold = false;
                    fontDynamic.IsItalic = true;
                    fontDynamic.Color = IndexedColors.Pink.Index;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Medium;
                    styleDynamic.BorderBottom = BorderStyle.Medium;
                    styleDynamic.BorderRight = BorderStyle.Medium;
                    styleDynamic.BorderTop = BorderStyle.Medium;
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
        public Cell(string Value, Cell.Style style)
        {
            this.Value = Value;
            SetStyle(style);
        }
    }
}
