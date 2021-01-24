using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyTable
{
    public partial class SelectDTQ : Form
    {
        public SelectDTQ(DateTimeQ dataQ)
        {
            InitializeComponent();
            if(dataQ.Quarter>0&& dataQ.Quarter<5) numericUpDown1.Value = dataQ.Quarter;
            if (dataQ.Year > 1899 && dataQ.Year < 2201) numericUpDown2.Value = dataQ.Year;
        }

        public DateTimeQ DataQ
        {
            get
            {
                return new DateTimeQ(numericUpDown1.Value + " " + numericUpDown2.Value);
            }
            set
            {
                numericUpDown1.Value = DataQ.Quarter;
                numericUpDown2.Value = DataQ.Year;
            }
        }
    }
}
