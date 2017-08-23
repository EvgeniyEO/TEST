using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing;
namespace TestMAP
{
    class GMapMarkerImage : GMapMarker
    {
        private Image image;
        public Image Image
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                if (image != null)
                {
                    this.Size = 
                        new Size(image.Width,
                            image.Height);
                }
            }
        }

        public Pen Pen
        {
            get;
            set;
        }

        public Pen OutPen
        {
            get;
            set;
        }

        public GMapMarkerImage(
            GMap.NET.PointLatLng p,
            Image image)
            : base(p)
        {
            Size = 
                new System.Drawing.Size(
                    image.Width,
                    image.Height);
            Offset =
                new System.Drawing.Point(
                    -Size.Width / 2,
                    -Size.Height / 2);
            this.image = image;
            Pen = null;
            OutPen = null;
        }

        public override void OnRender(Graphics g)
        {
            if (image == null)
                return;

            Rectangle rect =
                new Rectangle(LocalPosition.X, 
                              LocalPosition.Y, 
                              Size.Width, 
                              Size.Height);
            g.DrawImage(image, rect);

            if (Pen != null)
            {
                g.DrawRectangle(Pen, rect);
            }

            if (OutPen != null)
            {
                g.DrawEllipse(OutPen, rect);
            }
        }
    }

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
}
