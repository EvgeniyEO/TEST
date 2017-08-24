using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace TestMAP
{
    public class MyDGVCheckBoxColumn : DataGridViewCheckBoxColumn
    {
        private string label;

        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }
        }

        public override DataGridViewCell CellTemplate
        {
            get
            {
                return new MyDGVCheckBoxCell();
            }
        }
    }

    public class MyDGVCheckBoxCell : DataGridViewCheckBoxCell
    {
        private string label;

        public string Label
        {
            get
            {
                return label;
            }
            set
            {
                label = value;
            }

        }

        protected override void Paint(Graphics graphics, Rectangle clipBounds, Rectangle cellBounds, int rowIndex, DataGridViewElementStates elementState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {

            // the base Paint implementation paints the check box
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, elementState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);

            // Get the check box bounds: they are the content bounds
            Rectangle contentBounds = this.GetContentBounds(rowIndex);

            // Compute the location where we want to paint the string.
            Point stringLocation = new Point();

            // Compute the Y.
            // NOTE: the current logic does not take into account padding.
            stringLocation.Y = cellBounds.Y + 2;


            // Compute the X.
            // Content bounds are computed relative to the cell bounds
            // - not relative to the DataGridView control.
            stringLocation.X = cellBounds.X + contentBounds.Right + 4;


            // Paint the string.
            if (this.Label == null)
            {
                MyDGVCheckBoxColumn col = (MyDGVCheckBoxColumn)this.OwningColumn;
                this.label = col.Label;
            }

            graphics.DrawString(
            this.Label,
            new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204))),
            System.Drawing.Brushes.WhiteSmoke, stringLocation);

        }

    }
    class MaskedEditCell : DataGridViewTextBoxCell
    {
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            MaskedEditColumn mec = OwningColumn as MaskedEditColumn;
            MaskedEditControl mectrl = (MaskedEditControl)DataGridView.EditingControl;
            try
            {
                mectrl.Text = this.Value.ToString();
            }
            catch (Exception)
            {
                mectrl.Text = string.Empty;
            }
            mectrl.Mask = mec.Mask;
            mectrl.PromptChar = mec.PromtChar;
            mectrl.ValidatingType = mec.ValidatingType;
        }
        public override Type EditType
        {
            get
            {
                return typeof(MaskedEditControl);
            }
        }
        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }
        public override object DefaultNewRowValue
        {
            get
            {
                return string.Empty;
            }
        }
        protected override void Paint(System.Drawing.Graphics graphics, System.Drawing.Rectangle clipBounds, System.Drawing.Rectangle cellBounds, int rowIndex, DataGridViewElementStates cellState, object value, object formattedValue, string errorText, DataGridViewCellStyle cellStyle, DataGridViewAdvancedBorderStyle advancedBorderStyle, DataGridViewPaintParts paintParts)
        {
            base.Paint(graphics, clipBounds, cellBounds, rowIndex, cellState, value, formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
        }
    }
    class MaskedEditColumn : DataGridViewColumn
    {
        public MaskedEditColumn()
            : base(new MaskedEditCell())
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
                if ((value != null) && !value.GetType().IsAssignableFrom(typeof(MaskedEditCell)))
                {
                    throw new InvalidCastException("Must be a MaskedEditCell");
                }
                base.CellTemplate = value;
            }
        }
        private string mask;

        public string Mask
        {
            get { return mask; }
            set { mask = value; }
        }
        private Type validatingType;

        public Type ValidatingType
        {
            get { return validatingType; }
            set { validatingType = value; }
        }

        private char promtChar = '_';

        public char PromtChar
        {
            get { return promtChar; }
            set { promtChar = value; }
        }
        private MaskedEditCell MaskedEditCellTemplate
        {
            get { return this.CellTemplate as MaskedEditCell; }
        }
    }

    class MaskedEditControl : MaskedTextBox, IDataGridViewEditingControl
    {
        private DataGridView dataGridViewControl;
        private bool valueIsChanged = false;
        private int rowIndexNum;
        public void ApplyCellStyleToEditingControl(DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.BackColor = dataGridViewCellStyle.BackColor;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
        }

        public DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridViewControl;
            }
            set
            {
                dataGridViewControl = value;
            }
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.Text;
            }
            set
            {
                this.Text = value.ToString();
            }
        }

        public int EditingControlRowIndex
        {
            get
            {
                return rowIndexNum;
            }
            set
            {
                rowIndexNum = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return valueIsChanged;
            }
            set
            {
                valueIsChanged = value;
            }
        }

        public bool EditingControlWantsInputKey(Keys keyData, bool dataGridViewWantsInputKey)
        {
            return true;
        }

        public Cursor EditingPanelCursor
        {
            get { return base.Cursor; }
        }

        public object GetEditingControlFormattedValue(DataGridViewDataErrorContexts context)
        {
            return this.Text;
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
            //throw new NotImplementedException();
        }

        public bool RepositionEditingControlOnValueChange
        {
            get { return false; }
        }
        protected override void OnTextChanged(EventArgs e)
        {
            valueIsChanged = true;
            this.EditingControlDataGridView.NotifyCurrentCellDirty(true);
            base.OnTextChanged(e);
        }
    }
}
