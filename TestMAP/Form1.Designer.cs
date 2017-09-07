namespace TestMAP
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            BunifuAnimatorNS.Animation animation2 = new BunifuAnimatorNS.Animation();
            BunifuAnimatorNS.Animation animation1 = new BunifuAnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.panelMapWay = new System.Windows.Forms.Panel();
            this.panelTab = new System.Windows.Forms.Panel();
            this.TabMapWay = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.panelCustomDataGrid = new System.Windows.Forms.Panel();
            this.bunifuFlatButton6 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuFlatButton4 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuFlatButton5 = new Bunifu.Framework.UI.BunifuFlatButton();
            this.bunifuGridWayPoint = new Bunifu.Framework.UI.BunifuCustomDataGrid();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.panelMap = new System.Windows.Forms.Panel();
            this.gMapControl1 = new GMap.NET.WindowsForms.GMapControl();
            this.panelMenuGradient = new Bunifu.Framework.UI.BunifuGradientPanel();
            this.FlatButtonManual = new Bunifu.Framework.UI.BunifuFlatButton();
            this.FlatButtonSettings = new Bunifu.Framework.UI.BunifuFlatButton();
            this.FlatButtonMapWay = new Bunifu.Framework.UI.BunifuFlatButton();
            this.MenuButton = new Bunifu.Framework.UI.BunifuImageButton();
            this.animatorPanelGradient = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.animatorPanelMapWay = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelMapWay.SuspendLayout();
            this.panelTab.SuspendLayout();
            this.TabMapWay.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.panelCustomDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuGridWayPoint)).BeginInit();
            this.panelMap.SuspendLayout();
            this.panelMenuGradient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MenuButton)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelMapWay
            // 
            this.panelMapWay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panelMapWay.Controls.Add(this.panelTab);
            this.panelMapWay.Controls.Add(this.panelMap);
            this.animatorPanelMapWay.SetDecoration(this.panelMapWay, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.panelMapWay, BunifuAnimatorNS.DecorationType.None);
            this.panelMapWay.Location = new System.Drawing.Point(310, 30);
            this.panelMapWay.Name = "panelMapWay";
            this.panelMapWay.Size = new System.Drawing.Size(1517, 880);
            this.panelMapWay.TabIndex = 10;
            // 
            // panelTab
            // 
            this.panelTab.Controls.Add(this.TabMapWay);
            this.animatorPanelMapWay.SetDecoration(this.panelTab, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.panelTab, BunifuAnimatorNS.DecorationType.None);
            this.panelTab.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelTab.Location = new System.Drawing.Point(0, 603);
            this.panelTab.Name = "panelTab";
            this.panelTab.Size = new System.Drawing.Size(1513, 273);
            this.panelTab.TabIndex = 12;
            // 
            // TabMapWay
            // 
            this.TabMapWay.Controls.Add(this.metroTabPage2);
            this.TabMapWay.Controls.Add(this.metroTabPage3);
            this.TabMapWay.Controls.Add(this.metroTabPage1);
            this.animatorPanelGradient.SetDecoration(this.TabMapWay, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelMapWay.SetDecoration(this.TabMapWay, BunifuAnimatorNS.DecorationType.None);
            this.TabMapWay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabMapWay.FontSize = MetroFramework.MetroTabControlSize.Tall;
            this.TabMapWay.FontWeight = MetroFramework.MetroTabControlWeight.Bold;
            this.TabMapWay.HotTrack = true;
            this.TabMapWay.ItemSize = new System.Drawing.Size(110, 34);
            this.TabMapWay.Location = new System.Drawing.Point(0, 0);
            this.TabMapWay.Name = "TabMapWay";
            this.TabMapWay.SelectedIndex = 0;
            this.TabMapWay.ShowToolTips = true;
            this.TabMapWay.Size = new System.Drawing.Size(1513, 273);
            this.TabMapWay.Style = MetroFramework.MetroColorStyle.Blue;
            this.TabMapWay.TabIndex = 11;
            this.TabMapWay.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TabMapWay.UseSelectable = true;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.metroTabPage2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.metroTabPage2.Controls.Add(this.panelCustomDataGrid);
            this.animatorPanelMapWay.SetDecoration(this.metroTabPage2, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.metroTabPage2, BunifuAnimatorNS.DecorationType.None);
            this.metroTabPage2.ForeColor = System.Drawing.SystemColors.Control;
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(1505, 231);
            this.metroTabPage2.Style = MetroFramework.MetroColorStyle.Black;
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Waypoints";
            this.metroTabPage2.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabPage2.VerticalScrollbarBarColor = false;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // panelCustomDataGrid
            // 
            this.panelCustomDataGrid.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.panelCustomDataGrid.Controls.Add(this.bunifuFlatButton6);
            this.panelCustomDataGrid.Controls.Add(this.bunifuFlatButton4);
            this.panelCustomDataGrid.Controls.Add(this.bunifuFlatButton5);
            this.panelCustomDataGrid.Controls.Add(this.bunifuGridWayPoint);
            this.animatorPanelMapWay.SetDecoration(this.panelCustomDataGrid, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.panelCustomDataGrid, BunifuAnimatorNS.DecorationType.None);
            this.panelCustomDataGrid.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCustomDataGrid.Location = new System.Drawing.Point(0, 0);
            this.panelCustomDataGrid.Name = "panelCustomDataGrid";
            this.panelCustomDataGrid.Size = new System.Drawing.Size(1501, 226);
            this.panelCustomDataGrid.TabIndex = 2;
            // 
            // bunifuFlatButton6
            // 
            this.bunifuFlatButton6.Activecolor = System.Drawing.Color.Gray;
            this.bunifuFlatButton6.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bunifuFlatButton6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuFlatButton6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuFlatButton6.BorderRadius = 7;
            this.bunifuFlatButton6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bunifuFlatButton6.ButtonText = "Waypoint includes Z";
            this.bunifuFlatButton6.Cursor = System.Windows.Forms.Cursors.Hand;
            this.animatorPanelMapWay.SetDecoration(this.bunifuFlatButton6, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.bunifuFlatButton6, BunifuAnimatorNS.DecorationType.None);
            this.bunifuFlatButton6.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuFlatButton6.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bunifuFlatButton6.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButton6.Iconimage = null;
            this.bunifuFlatButton6.Iconimage_right = null;
            this.bunifuFlatButton6.Iconimage_right_Selected = null;
            this.bunifuFlatButton6.Iconimage_Selected = null;
            this.bunifuFlatButton6.IconMarginLeft = 0;
            this.bunifuFlatButton6.IconMarginRight = 0;
            this.bunifuFlatButton6.IconRightVisible = false;
            this.bunifuFlatButton6.IconRightZoom = 0D;
            this.bunifuFlatButton6.IconVisible = false;
            this.bunifuFlatButton6.IconZoom = 90D;
            this.bunifuFlatButton6.IsTab = true;
            this.bunifuFlatButton6.Location = new System.Drawing.Point(1355, 154);
            this.bunifuFlatButton6.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bunifuFlatButton6.Name = "bunifuFlatButton6";
            this.bunifuFlatButton6.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuFlatButton6.OnHovercolor = System.Drawing.Color.Gray;
            this.bunifuFlatButton6.OnHoverTextColor = System.Drawing.Color.Black;
            this.bunifuFlatButton6.selected = false;
            this.bunifuFlatButton6.Size = new System.Drawing.Size(142, 66);
            this.bunifuFlatButton6.TabIndex = 14;
            this.bunifuFlatButton6.Text = "Waypoint includes Z";
            this.bunifuFlatButton6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuFlatButton6.Textcolor = System.Drawing.Color.Black;
            this.bunifuFlatButton6.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // bunifuFlatButton4
            // 
            this.bunifuFlatButton4.Activecolor = System.Drawing.Color.Gray;
            this.bunifuFlatButton4.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bunifuFlatButton4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuFlatButton4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuFlatButton4.BorderRadius = 7;
            this.bunifuFlatButton4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bunifuFlatButton4.ButtonText = "Head Toward Waypoints";
            this.bunifuFlatButton4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.animatorPanelMapWay.SetDecoration(this.bunifuFlatButton4, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.bunifuFlatButton4, BunifuAnimatorNS.DecorationType.None);
            this.bunifuFlatButton4.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuFlatButton4.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bunifuFlatButton4.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButton4.Iconimage = null;
            this.bunifuFlatButton4.Iconimage_right = null;
            this.bunifuFlatButton4.Iconimage_right_Selected = null;
            this.bunifuFlatButton4.Iconimage_Selected = null;
            this.bunifuFlatButton4.IconMarginLeft = 0;
            this.bunifuFlatButton4.IconMarginRight = 0;
            this.bunifuFlatButton4.IconRightVisible = false;
            this.bunifuFlatButton4.IconRightZoom = 0D;
            this.bunifuFlatButton4.IconVisible = false;
            this.bunifuFlatButton4.IconZoom = 90D;
            this.bunifuFlatButton4.IsTab = true;
            this.bunifuFlatButton4.Location = new System.Drawing.Point(1355, 80);
            this.bunifuFlatButton4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bunifuFlatButton4.Name = "bunifuFlatButton4";
            this.bunifuFlatButton4.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuFlatButton4.OnHovercolor = System.Drawing.Color.Gray;
            this.bunifuFlatButton4.OnHoverTextColor = System.Drawing.Color.Black;
            this.bunifuFlatButton4.selected = false;
            this.bunifuFlatButton4.Size = new System.Drawing.Size(142, 66);
            this.bunifuFlatButton4.TabIndex = 13;
            this.bunifuFlatButton4.Text = "Head Toward Waypoints";
            this.bunifuFlatButton4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuFlatButton4.Textcolor = System.Drawing.Color.Black;
            this.bunifuFlatButton4.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // bunifuFlatButton5
            // 
            this.bunifuFlatButton5.Activecolor = System.Drawing.Color.Gray;
            this.bunifuFlatButton5.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.bunifuFlatButton5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuFlatButton5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bunifuFlatButton5.BorderRadius = 7;
            this.bunifuFlatButton5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bunifuFlatButton5.ButtonText = "Effort vs. Speed";
            this.bunifuFlatButton5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.animatorPanelMapWay.SetDecoration(this.bunifuFlatButton5, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.bunifuFlatButton5, BunifuAnimatorNS.DecorationType.None);
            this.bunifuFlatButton5.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuFlatButton5.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bunifuFlatButton5.Iconcolor = System.Drawing.Color.Transparent;
            this.bunifuFlatButton5.Iconimage = null;
            this.bunifuFlatButton5.Iconimage_right = null;
            this.bunifuFlatButton5.Iconimage_right_Selected = null;
            this.bunifuFlatButton5.Iconimage_Selected = null;
            this.bunifuFlatButton5.IconMarginLeft = 0;
            this.bunifuFlatButton5.IconMarginRight = 0;
            this.bunifuFlatButton5.IconRightVisible = false;
            this.bunifuFlatButton5.IconRightZoom = 0D;
            this.bunifuFlatButton5.IconVisible = false;
            this.bunifuFlatButton5.IconZoom = 90D;
            this.bunifuFlatButton5.IsTab = true;
            this.bunifuFlatButton5.Location = new System.Drawing.Point(1355, 6);
            this.bunifuFlatButton5.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bunifuFlatButton5.Name = "bunifuFlatButton5";
            this.bunifuFlatButton5.Normalcolor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuFlatButton5.OnHovercolor = System.Drawing.Color.Gray;
            this.bunifuFlatButton5.OnHoverTextColor = System.Drawing.Color.Black;
            this.bunifuFlatButton5.selected = false;
            this.bunifuFlatButton5.Size = new System.Drawing.Size(142, 66);
            this.bunifuFlatButton5.TabIndex = 12;
            this.bunifuFlatButton5.Text = "Effort vs. Speed";
            this.bunifuFlatButton5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuFlatButton5.Textcolor = System.Drawing.Color.Black;
            this.bunifuFlatButton5.TextFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            // 
            // bunifuGridWayPoint
            // 
            this.bunifuGridWayPoint.AllowUserToAddRows = false;
            this.bunifuGridWayPoint.AllowUserToDeleteRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.DimGray;
            this.bunifuGridWayPoint.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.bunifuGridWayPoint.BackgroundColor = System.Drawing.Color.DimGray;
            this.bunifuGridWayPoint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bunifuGridWayPoint.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.bunifuGridWayPoint.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ButtonShadow;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bunifuGridWayPoint.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.bunifuGridWayPoint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.animatorPanelGradient.SetDecoration(this.bunifuGridWayPoint, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelMapWay.SetDecoration(this.bunifuGridWayPoint, BunifuAnimatorNS.DecorationType.None);
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuGridWayPoint.DefaultCellStyle = dataGridViewCellStyle3;
            this.bunifuGridWayPoint.Dock = System.Windows.Forms.DockStyle.Left;
            this.bunifuGridWayPoint.DoubleBuffered = true;
            this.bunifuGridWayPoint.EnableHeadersVisualStyles = false;
            this.bunifuGridWayPoint.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.bunifuGridWayPoint.HeaderBgColor = System.Drawing.Color.DarkGray;
            this.bunifuGridWayPoint.HeaderForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bunifuGridWayPoint.Location = new System.Drawing.Point(0, 0);
            this.bunifuGridWayPoint.Name = "bunifuGridWayPoint";
            this.bunifuGridWayPoint.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.ButtonShadow;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bunifuGridWayPoint.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.bunifuGridWayPoint.RowHeadersVisible = false;
            this.bunifuGridWayPoint.RowTemplate.Height = 24;
            this.bunifuGridWayPoint.Size = new System.Drawing.Size(1341, 226);
            this.bunifuGridWayPoint.TabIndex = 10;
            // 
            // metroTabPage3
            // 
            this.animatorPanelMapWay.SetDecoration(this.metroTabPage3, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.metroTabPage3, BunifuAnimatorNS.DecorationType.None);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(1505, 231);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Map Config";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            // 
            // metroTabPage1
            // 
            this.animatorPanelMapWay.SetDecoration(this.metroTabPage1, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.metroTabPage1, BunifuAnimatorNS.DecorationType.None);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(1505, 231);
            this.metroTabPage1.TabIndex = 3;
            this.metroTabPage1.Text = "Mission";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // panelMap
            // 
            this.panelMap.Controls.Add(this.gMapControl1);
            this.animatorPanelMapWay.SetDecoration(this.panelMap, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.panelMap, BunifuAnimatorNS.DecorationType.None);
            this.panelMap.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelMap.Location = new System.Drawing.Point(0, 0);
            this.panelMap.Name = "panelMap";
            this.panelMap.Size = new System.Drawing.Size(1513, 603);
            this.panelMap.TabIndex = 11;
            // 
            // gMapControl1
            // 
            this.gMapControl1.Bearing = 0F;
            this.gMapControl1.CanDragMap = true;
            this.animatorPanelMapWay.SetDecoration(this.gMapControl1, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.gMapControl1, BunifuAnimatorNS.DecorationType.None);
            this.gMapControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gMapControl1.EmptyTileColor = System.Drawing.Color.Navy;
            this.gMapControl1.GrayScaleMode = false;
            this.gMapControl1.HelperLineOption = GMap.NET.WindowsForms.HelperLineOptions.DontShow;
            this.gMapControl1.LevelsKeepInMemmory = 5;
            this.gMapControl1.Location = new System.Drawing.Point(0, 0);
            this.gMapControl1.MarkersEnabled = true;
            this.gMapControl1.MaxZoom = 2;
            this.gMapControl1.MinZoom = 2;
            this.gMapControl1.MouseWheelZoomType = GMap.NET.MouseWheelZoomType.MousePositionAndCenter;
            this.gMapControl1.Name = "gMapControl1";
            this.gMapControl1.NegativeMode = false;
            this.gMapControl1.PolygonsEnabled = true;
            this.gMapControl1.RetryLoadTile = 0;
            this.gMapControl1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.gMapControl1.RoutesEnabled = true;
            this.gMapControl1.ScaleMode = GMap.NET.WindowsForms.ScaleModes.Integer;
            this.gMapControl1.SelectedAreaFillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(65)))), ((int)(((byte)(105)))), ((int)(((byte)(225)))));
            this.gMapControl1.ShowTileGridLines = false;
            this.gMapControl1.Size = new System.Drawing.Size(1513, 603);
            this.gMapControl1.TabIndex = 3;
            this.gMapControl1.Zoom = 0D;
            // 
            // panelMenuGradient
            // 
            this.panelMenuGradient.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelMenuGradient.BackgroundImage")));
            this.panelMenuGradient.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelMenuGradient.Controls.Add(this.pictureBox1);
            this.panelMenuGradient.Controls.Add(this.FlatButtonManual);
            this.panelMenuGradient.Controls.Add(this.FlatButtonSettings);
            this.panelMenuGradient.Controls.Add(this.FlatButtonMapWay);
            this.panelMenuGradient.Controls.Add(this.MenuButton);
            this.animatorPanelMapWay.SetDecoration(this.panelMenuGradient, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.panelMenuGradient, BunifuAnimatorNS.DecorationType.None);
            this.panelMenuGradient.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenuGradient.GradientBottomLeft = System.Drawing.Color.Black;
            this.panelMenuGradient.GradientBottomRight = System.Drawing.Color.Gray;
            this.panelMenuGradient.GradientTopLeft = System.Drawing.Color.Gray;
            this.panelMenuGradient.GradientTopRight = System.Drawing.Color.Black;
            this.panelMenuGradient.Location = new System.Drawing.Point(20, 30);
            this.panelMenuGradient.Name = "panelMenuGradient";
            this.panelMenuGradient.Quality = 10;
            this.panelMenuGradient.Size = new System.Drawing.Size(280, 880);
            this.panelMenuGradient.TabIndex = 11;
            // 
            // FlatButtonManual
            // 
            this.FlatButtonManual.Activecolor = System.Drawing.Color.Gray;
            this.FlatButtonManual.BackColor = System.Drawing.Color.Transparent;
            this.FlatButtonManual.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FlatButtonManual.BorderRadius = 0;
            this.FlatButtonManual.ButtonText = "              Manual";
            this.FlatButtonManual.Cursor = System.Windows.Forms.Cursors.Hand;
            this.animatorPanelMapWay.SetDecoration(this.FlatButtonManual, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.FlatButtonManual, BunifuAnimatorNS.DecorationType.None);
            this.FlatButtonManual.DisabledColor = System.Drawing.Color.Gray;
            this.FlatButtonManual.Iconcolor = System.Drawing.Color.Transparent;
            this.FlatButtonManual.Iconimage = global::TestMAP.Properties.Resources.ManualPilot;
            this.FlatButtonManual.Iconimage_right = null;
            this.FlatButtonManual.Iconimage_right_Selected = null;
            this.FlatButtonManual.Iconimage_Selected = null;
            this.FlatButtonManual.IconMarginLeft = 0;
            this.FlatButtonManual.IconMarginRight = 0;
            this.FlatButtonManual.IconRightVisible = true;
            this.FlatButtonManual.IconRightZoom = 0D;
            this.FlatButtonManual.IconVisible = true;
            this.FlatButtonManual.IconZoom = 60D;
            this.FlatButtonManual.IsTab = true;
            this.FlatButtonManual.Location = new System.Drawing.Point(0, 284);
            this.FlatButtonManual.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FlatButtonManual.Name = "FlatButtonManual";
            this.FlatButtonManual.Normalcolor = System.Drawing.Color.Transparent;
            this.FlatButtonManual.OnHovercolor = System.Drawing.Color.Gray;
            this.FlatButtonManual.OnHoverTextColor = System.Drawing.Color.WhiteSmoke;
            this.FlatButtonManual.selected = false;
            this.FlatButtonManual.Size = new System.Drawing.Size(279, 56);
            this.FlatButtonManual.TabIndex = 3;
            this.FlatButtonManual.Text = "              Manual";
            this.FlatButtonManual.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FlatButtonManual.Textcolor = System.Drawing.Color.WhiteSmoke;
            this.FlatButtonManual.TextFont = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FlatButtonManual.Click += new System.EventHandler(this.FlatButtonManual_Click);
            // 
            // FlatButtonSettings
            // 
            this.FlatButtonSettings.Activecolor = System.Drawing.Color.Gray;
            this.FlatButtonSettings.BackColor = System.Drawing.Color.Transparent;
            this.FlatButtonSettings.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FlatButtonSettings.BorderRadius = 0;
            this.FlatButtonSettings.ButtonText = "              Settings";
            this.FlatButtonSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.animatorPanelMapWay.SetDecoration(this.FlatButtonSettings, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.FlatButtonSettings, BunifuAnimatorNS.DecorationType.None);
            this.FlatButtonSettings.DisabledColor = System.Drawing.Color.Gray;
            this.FlatButtonSettings.Iconcolor = System.Drawing.Color.Transparent;
            this.FlatButtonSettings.Iconimage = global::TestMAP.Properties.Resources.SettingsIco;
            this.FlatButtonSettings.Iconimage_right = null;
            this.FlatButtonSettings.Iconimage_right_Selected = null;
            this.FlatButtonSettings.Iconimage_Selected = null;
            this.FlatButtonSettings.IconMarginLeft = 0;
            this.FlatButtonSettings.IconMarginRight = 0;
            this.FlatButtonSettings.IconRightVisible = true;
            this.FlatButtonSettings.IconRightZoom = 0D;
            this.FlatButtonSettings.IconVisible = true;
            this.FlatButtonSettings.IconZoom = 60D;
            this.FlatButtonSettings.IsTab = true;
            this.FlatButtonSettings.Location = new System.Drawing.Point(0, 220);
            this.FlatButtonSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FlatButtonSettings.Name = "FlatButtonSettings";
            this.FlatButtonSettings.Normalcolor = System.Drawing.Color.Transparent;
            this.FlatButtonSettings.OnHovercolor = System.Drawing.Color.Gray;
            this.FlatButtonSettings.OnHoverTextColor = System.Drawing.Color.WhiteSmoke;
            this.FlatButtonSettings.selected = false;
            this.FlatButtonSettings.Size = new System.Drawing.Size(279, 56);
            this.FlatButtonSettings.TabIndex = 2;
            this.FlatButtonSettings.Text = "              Settings";
            this.FlatButtonSettings.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FlatButtonSettings.Textcolor = System.Drawing.Color.WhiteSmoke;
            this.FlatButtonSettings.TextFont = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FlatButtonSettings.Click += new System.EventHandler(this.FlatButtonSettings_Click);
            // 
            // FlatButtonMapWay
            // 
            this.FlatButtonMapWay.Activecolor = System.Drawing.Color.Gray;
            this.FlatButtonMapWay.BackColor = System.Drawing.Color.Gray;
            this.FlatButtonMapWay.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FlatButtonMapWay.BorderRadius = 0;
            this.FlatButtonMapWay.ButtonText = "              Map and Waypoint";
            this.FlatButtonMapWay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.animatorPanelMapWay.SetDecoration(this.FlatButtonMapWay, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.FlatButtonMapWay, BunifuAnimatorNS.DecorationType.None);
            this.FlatButtonMapWay.DisabledColor = System.Drawing.Color.Gray;
            this.FlatButtonMapWay.Iconcolor = System.Drawing.Color.Transparent;
            this.FlatButtonMapWay.Iconimage = ((System.Drawing.Image)(resources.GetObject("FlatButtonMapWay.Iconimage")));
            this.FlatButtonMapWay.Iconimage_right = null;
            this.FlatButtonMapWay.Iconimage_right_Selected = null;
            this.FlatButtonMapWay.Iconimage_Selected = null;
            this.FlatButtonMapWay.IconMarginLeft = 0;
            this.FlatButtonMapWay.IconMarginRight = 0;
            this.FlatButtonMapWay.IconRightVisible = true;
            this.FlatButtonMapWay.IconRightZoom = 0D;
            this.FlatButtonMapWay.IconVisible = true;
            this.FlatButtonMapWay.IconZoom = 60D;
            this.FlatButtonMapWay.IsTab = true;
            this.FlatButtonMapWay.Location = new System.Drawing.Point(0, 156);
            this.FlatButtonMapWay.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.FlatButtonMapWay.Name = "FlatButtonMapWay";
            this.FlatButtonMapWay.Normalcolor = System.Drawing.Color.Transparent;
            this.FlatButtonMapWay.OnHovercolor = System.Drawing.Color.Gray;
            this.FlatButtonMapWay.OnHoverTextColor = System.Drawing.Color.WhiteSmoke;
            this.FlatButtonMapWay.selected = true;
            this.FlatButtonMapWay.Size = new System.Drawing.Size(279, 56);
            this.FlatButtonMapWay.TabIndex = 1;
            this.FlatButtonMapWay.Text = "              Map and Waypoint";
            this.FlatButtonMapWay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.FlatButtonMapWay.Textcolor = System.Drawing.Color.WhiteSmoke;
            this.FlatButtonMapWay.TextFont = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FlatButtonMapWay.Click += new System.EventHandler(this.FlatButtonMapWay_Click);
            // 
            // MenuButton
            // 
            this.MenuButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.MenuButton.BackColor = System.Drawing.Color.Transparent;
            this.animatorPanelMapWay.SetDecoration(this.MenuButton, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.MenuButton, BunifuAnimatorNS.DecorationType.None);
            this.MenuButton.Image = global::TestMAP.Properties.Resources.menu_white;
            this.MenuButton.ImageActive = null;
            this.MenuButton.Location = new System.Drawing.Point(222, 16);
            this.MenuButton.Name = "MenuButton";
            this.MenuButton.Size = new System.Drawing.Size(57, 53);
            this.MenuButton.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.MenuButton.TabIndex = 0;
            this.MenuButton.TabStop = false;
            this.MenuButton.Zoom = 20;
            this.MenuButton.Click += new System.EventHandler(this.MenuButton_Click);
            // 
            // animatorPanelGradient
            // 
            this.animatorPanelGradient.AnimationType = BunifuAnimatorNS.AnimationType.Transparent;
            this.animatorPanelGradient.Cursor = null;
            animation2.AnimateOnlyDifferences = true;
            animation2.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.BlindCoeff")));
            animation2.LeafCoeff = 0F;
            animation2.MaxTime = 1F;
            animation2.MinTime = 0F;
            animation2.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicCoeff")));
            animation2.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation2.MosaicShift")));
            animation2.MosaicSize = 0;
            animation2.Padding = new System.Windows.Forms.Padding(0);
            animation2.RotateCoeff = 0F;
            animation2.RotateLimit = 0F;
            animation2.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.ScaleCoeff")));
            animation2.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation2.SlideCoeff")));
            animation2.TimeCoeff = 0F;
            animation2.TransparencyCoeff = 1F;
            this.animatorPanelGradient.DefaultAnimation = animation2;
            // 
            // animatorPanelMapWay
            // 
            this.animatorPanelMapWay.AnimationType = BunifuAnimatorNS.AnimationType.HorizSlide;
            this.animatorPanelMapWay.Cursor = null;
            animation1.AnimateOnlyDifferences = true;
            animation1.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.BlindCoeff")));
            animation1.LeafCoeff = 0F;
            animation1.MaxTime = 1F;
            animation1.MinTime = 0F;
            animation1.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicCoeff")));
            animation1.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation1.MosaicShift")));
            animation1.MosaicSize = 0;
            animation1.Padding = new System.Windows.Forms.Padding(0);
            animation1.RotateCoeff = 0F;
            animation1.RotateLimit = 0F;
            animation1.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.ScaleCoeff")));
            animation1.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation1.SlideCoeff")));
            animation1.TimeCoeff = 0F;
            animation1.TransparencyCoeff = 0F;
            this.animatorPanelMapWay.DefaultAnimation = animation1;
            this.animatorPanelMapWay.TimeStep = 0.05F;
            // 
            // pictureBox1
            // 
            this.animatorPanelGradient.SetDecoration(this.pictureBox1, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelMapWay.SetDecoration(this.pictureBox1, BunifuAnimatorNS.DecorationType.None);
            this.pictureBox1.Location = new System.Drawing.Point(12, 389);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(255, 272);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1842, 930);
            this.Controls.Add(this.panelMapWay);
            this.Controls.Add(this.panelMenuGradient);
            this.animatorPanelMapWay.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this, BunifuAnimatorNS.DecorationType.None);
            this.DisplayHeader = false;
            this.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Name = "Form1";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Black;
            this.Text = "Form1";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TransparencyKey = System.Drawing.Color.LightSteelBlue;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.panelMapWay.ResumeLayout(false);
            this.panelTab.ResumeLayout(false);
            this.TabMapWay.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.panelCustomDataGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bunifuGridWayPoint)).EndInit();
            this.panelMap.ResumeLayout(false);
            this.panelMenuGradient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MenuButton)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMapWay;
        private System.Windows.Forms.Panel panelMap;
        private GMap.NET.WindowsForms.GMapControl gMapControl1;
        private Bunifu.Framework.UI.BunifuGradientPanel panelMenuGradient;
        private Bunifu.Framework.UI.BunifuImageButton MenuButton;
        private Bunifu.Framework.UI.BunifuFlatButton FlatButtonMapWay;
        private Bunifu.Framework.UI.BunifuFlatButton FlatButtonSettings;
        private BunifuAnimatorNS.BunifuTransition animatorPanelGradient;
        private Bunifu.Framework.UI.BunifuFlatButton FlatButtonManual;
        private System.Windows.Forms.Panel panelTab;
        private MetroFramework.Controls.MetroTabControl TabMapWay;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private System.Windows.Forms.Panel panelCustomDataGrid;
        private Bunifu.Framework.UI.BunifuCustomDataGrid bunifuGridWayPoint;
        private Bunifu.Framework.UI.BunifuFlatButton bunifuFlatButton5;
        private Bunifu.Framework.UI.BunifuFlatButton bunifuFlatButton6;
        private Bunifu.Framework.UI.BunifuFlatButton bunifuFlatButton4;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private BunifuAnimatorNS.BunifuTransition animatorPanelMapWay;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}

