using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace MyTable
{
    class DataGridViewRadiobuttonColumn : DataGridViewColumn
    {
        public DataGridViewRadiobuttonColumn()
            : base(new RadiobuttonCell())
        {
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                if (value != null && !value.GetType().IsAssignableFrom(typeof(RadiobuttonCell)))
                {
                    throw new InvalidCastException("Must be a RadiobuttonCell");
                }
            }
        }
    }
    public class RadiobuttonCell : DataGridViewCell
    {
        public RadiobuttonCell()
            : base()
        {
        }

        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            RadiobuttonEditingControl rdb = DataGridView.EditingControl as RadiobuttonEditingControl;
            if (this.Value == null)
            {
                rdb.Checked = false;
            }
            else
            {
                rdb.Checked = (Boolean)this.Value;
            }
        }

        public override Type EditType
        {
            get
            {
                return typeof(RadiobuttonEditingControl);
            }
        }

        public override Type ValueType
        {
            get
            {
                return typeof(Boolean);
            }
        }

        public override object DefaultNewRowValue
        {
            get
            {
                return false;
            }
        }

        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);


            Rectangle rectRadioButton = default(Rectangle);

            rectRadioButton.Width = 14;
            rectRadioButton.Height = 14;
            rectRadioButton.X = cellBounds.X + (cellBounds.Width - rectRadioButton.Width) / 2;
            rectRadioButton.Y = cellBounds.Y + (cellBounds.Height - rectRadioButton.Height) / 2;

            ControlPaint.DrawRadioButton(graphics, rectRadioButton, ButtonState.Normal);
        }
        class RadiobuttonEditingControl : RadioButton, IDataGridViewEditingControl
        {
            DataGridView dataGridView;
            private bool valueChanged = false;
            int rowIndex;

            public RadiobuttonEditingControl()
            {
                this.Checked = false;
            }

            public object EditingControlFormattedValue
            {
                get
                {
                    return this.Checked = true;
                }
                set
                {
                    this.Checked = false;
                }
            }

            public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
            {
                return EditingControlFormattedValue;
            }

            public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
            {
            }

            public int EditingControlRowIndex
            {
                get
                {
                    return rowIndex;
                }
                set
                {
                    rowIndex = value;
                }
            }

            public bool EditingControlWantsInputKey(Keys key, bool dataGridViewWantsInputKey)
            {
                switch (key & Keys.KeyCode)
                {
                    case Keys.Space:
                        return true;
                    default:
                        return !dataGridViewWantsInputKey;
                }
            }

            public void PrepareEditingControlForEdit(bool selectAll)
            {
            }

            public bool RepositionEditingControlOnValueChange
            {
                get
                {
                    return false;
                }
            }

            public DataGridView EditingControlDataGridView
            {
                get
                {
                    return dataGridView;
                }
                set
                {
                    dataGridView = value;
                }
            }

            public bool EditingControlValueChanged
            {
                get
                {
                    return valueChanged;
                }
                set
                {
                    valueChanged = value;
                }
            }

            public Cursor EditingPanelCursor
            {
                get
                {
                    return base.Cursor;
                }
            }

            protected override void OnCheckedChanged(EventArgs eventArgs)
            {
                valueChanged = true;
                this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
                base.Checked = false;
            }
        }
    }
}
