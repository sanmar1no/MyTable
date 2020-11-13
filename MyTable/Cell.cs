
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTable
{
    class Cell
    {
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
        public Cell() : this("")
        { 
        }
        public Cell(string Value)
        {
            this.Value = Value;
        }
    }
}
