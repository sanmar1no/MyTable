
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
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
                    styleDynamic.BorderLeft = BorderStyle.Hair;
                    styleDynamic.BorderBottom = BorderStyle.Hair;
                    styleDynamic.BorderRight = BorderStyle.Hair;
                    styleDynamic.BorderTop = BorderStyle.Hair;
                    break;
                case Cell.Style.bold:
                    fontDynamic.FontName = "ISOCPEUR";
                    fontDynamic.FontHeightInPoints = 12;
                    fontDynamic.IsBold = true;
                    fontDynamic.IsItalic = true;
                    //font.Color = IndexedColors.Red.Index;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Medium;
                    styleDynamic.BorderBottom = BorderStyle.Medium;
                    styleDynamic.BorderRight = BorderStyle.Medium;
                    styleDynamic.BorderTop = BorderStyle.Medium;
                    break;
                case Cell.Style.clientCame:
                    fontDynamic.FontName = "ISOCPEUR";
                    fontDynamic.FontHeightInPoints = 12;
                    fontDynamic.IsBold = true;
                    fontDynamic.IsItalic = true;
                    font.Color = IndexedColors.Aqua.Index;
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
                    font.Color = IndexedColors.LightGreen.Index;
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
                    font.Color = IndexedColors.Pink.Index;
                    styleDynamic.SetFont(fontDynamic);
                    styleDynamic.BorderLeft = BorderStyle.Medium;
                    styleDynamic.BorderBottom = BorderStyle.Medium;
                    styleDynamic.BorderRight = BorderStyle.Medium;
                    styleDynamic.BorderTop = BorderStyle.Medium;
                    break;
            }
        }
        public Color ForeColor = Color.Black;
        public Color ColorInterior = Color.White;
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
        public dynamic font = new System.Drawing.Font("ISOCPEUR", 10, FontStyle.Italic & ~FontStyle.Bold);
        public dynamic LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
        public dynamic Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin;//.xlMedium;
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
