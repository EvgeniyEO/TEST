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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle16 = new System.Windows.Forms.DataGridViewCellStyle();
            BunifuAnimatorNS.Animation animation7 = new BunifuAnimatorNS.Animation();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            BunifuAnimatorNS.Animation animation8 = new BunifuAnimatorNS.Animation();
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
            this.animatorPanelGradient = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.bunifuTextbox1 = new Bunifu.Framework.UI.BunifuTextbox();
            this.bunifuThinButton22 = new Bunifu.Framework.UI.BunifuThinButton2();
            this.bunifuCustomLabel2 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.bunifuThinButton21 = new Bunifu.Framework.UI.BunifuThinButton2();
            this.bunifuTileButton1 = new Bunifu.Framework.UI.BunifuTileButton();
            this.bunifuCustomLabel1 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.bunifuDropdown1 = new Bunifu.Framework.UI.BunifuDropdown();
            this.panelMenuGradient = new Bunifu.Framework.UI.BunifuGradientPanel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.FlatButtonManual = new Bunifu.Framework.UI.BunifuFlatButton();
            this.FlatButtonSettings = new Bunifu.Framework.UI.BunifuFlatButton();
            this.FlatButtonMapWay = new Bunifu.Framework.UI.BunifuFlatButton();
            this.MenuButton = new Bunifu.Framework.UI.BunifuImageButton();
            this.animatorPanelMapWay = new BunifuAnimatorNS.BunifuTransition(this.components);
            this.bunifuCheckbox1 = new Bunifu.Framework.UI.BunifuCheckbox();
            this.bunifuCustomLabel3 = new Bunifu.Framework.UI.BunifuCustomLabel();
            this.panelMapWay.SuspendLayout();
            this.panelTab.SuspendLayout();
            this.TabMapWay.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.panelCustomDataGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bunifuGridWayPoint)).BeginInit();
            this.panelMap.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panelMenuGradient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuButton)).BeginInit();
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
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Century Gothic", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.Color.WhiteSmoke;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.Color.DimGray;
            this.bunifuGridWayPoint.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle13;
            this.bunifuGridWayPoint.BackgroundColor = System.Drawing.Color.DimGray;
            this.bunifuGridWayPoint.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.bunifuGridWayPoint.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.bunifuGridWayPoint.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle14.BackColor = System.Drawing.Color.DarkGray;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ButtonFace;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.ButtonShadow;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.Desktop;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bunifuGridWayPoint.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle14;
            this.bunifuGridWayPoint.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.animatorPanelGradient.SetDecoration(this.bunifuGridWayPoint, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelMapWay.SetDecoration(this.bunifuGridWayPoint, BunifuAnimatorNS.DecorationType.None);
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle15.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.bunifuGridWayPoint.DefaultCellStyle = dataGridViewCellStyle15;
            this.bunifuGridWayPoint.Dock = System.Windows.Forms.DockStyle.Left;
            this.bunifuGridWayPoint.DoubleBuffered = true;
            this.bunifuGridWayPoint.EnableHeadersVisualStyles = false;
            this.bunifuGridWayPoint.GridColor = System.Drawing.SystemColors.ButtonShadow;
            this.bunifuGridWayPoint.HeaderBgColor = System.Drawing.Color.DarkGray;
            this.bunifuGridWayPoint.HeaderForeColor = System.Drawing.SystemColors.ButtonFace;
            this.bunifuGridWayPoint.Location = new System.Drawing.Point(0, 0);
            this.bunifuGridWayPoint.Name = "bunifuGridWayPoint";
            this.bunifuGridWayPoint.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle16.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle16.BackColor = System.Drawing.SystemColors.ButtonShadow;
            dataGridViewCellStyle16.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            dataGridViewCellStyle16.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle16.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle16.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle16.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.bunifuGridWayPoint.RowHeadersDefaultCellStyle = dataGridViewCellStyle16;
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
            // animatorPanelGradient
            // 
            this.animatorPanelGradient.AnimationType = BunifuAnimatorNS.AnimationType.Transparent;
            this.animatorPanelGradient.Cursor = null;
            animation7.AnimateOnlyDifferences = true;
            animation7.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation7.BlindCoeff")));
            animation7.LeafCoeff = 0F;
            animation7.MaxTime = 1F;
            animation7.MinTime = 0F;
            animation7.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation7.MosaicCoeff")));
            animation7.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation7.MosaicShift")));
            animation7.MosaicSize = 0;
            animation7.Padding = new System.Windows.Forms.Padding(0);
            animation7.RotateCoeff = 0F;
            animation7.RotateLimit = 0F;
            animation7.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation7.ScaleCoeff")));
            animation7.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation7.SlideCoeff")));
            animation7.TimeCoeff = 0F;
            animation7.TransparencyCoeff = 1F;
            this.animatorPanelGradient.DefaultAnimation = animation7;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.animatorPanelMapWay.SetDecoration(this.panel1, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.panel1, BunifuAnimatorNS.DecorationType.None);
            this.panel1.Location = new System.Drawing.Point(310, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1517, 880);
            this.panel1.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Gray;
            this.panel3.Controls.Add(this.bunifuCustomLabel3);
            this.panel3.Controls.Add(this.bunifuCheckbox1);
            this.panel3.Controls.Add(this.bunifuTextbox1);
            this.panel3.Controls.Add(this.bunifuThinButton22);
            this.panel3.Controls.Add(this.bunifuCustomLabel2);
            this.animatorPanelMapWay.SetDecoration(this.panel3, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.panel3, BunifuAnimatorNS.DecorationType.None);
            this.panel3.Location = new System.Drawing.Point(446, 18);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(363, 422);
            this.panel3.TabIndex = 1;
            // 
            // bunifuTextbox1
            // 
            this.bunifuTextbox1.BackColor = System.Drawing.Color.Silver;
            this.bunifuTextbox1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuTextbox1.BackgroundImage")));
            this.bunifuTextbox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.animatorPanelMapWay.SetDecoration(this.bunifuTextbox1, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.bunifuTextbox1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTextbox1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.bunifuTextbox1.Icon = ((System.Drawing.Image)(resources.GetObject("bunifuTextbox1.Icon")));
            this.bunifuTextbox1.Location = new System.Drawing.Point(29, 136);
            this.bunifuTextbox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bunifuTextbox1.Name = "bunifuTextbox1";
            this.bunifuTextbox1.Size = new System.Drawing.Size(210, 37);
            this.bunifuTextbox1.TabIndex = 6;
            this.bunifuTextbox1.text = "";
            // 
            // bunifuThinButton22
            // 
            this.bunifuThinButton22.ActiveBorderThickness = 1;
            this.bunifuThinButton22.ActiveCornerRadius = 20;
            this.bunifuThinButton22.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton22.ActiveForecolor = System.Drawing.Color.White;
            this.bunifuThinButton22.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton22.BackColor = System.Drawing.Color.Gray;
            this.bunifuThinButton22.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuThinButton22.BackgroundImage")));
            this.bunifuThinButton22.ButtonText = "Подключиться";
            this.bunifuThinButton22.Cursor = System.Windows.Forms.Cursors.Hand;
            this.animatorPanelMapWay.SetDecoration(this.bunifuThinButton22, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.bunifuThinButton22, BunifuAnimatorNS.DecorationType.None);
            this.bunifuThinButton22.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuThinButton22.ForeColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton22.IdleBorderThickness = 1;
            this.bunifuThinButton22.IdleCornerRadius = 20;
            this.bunifuThinButton22.IdleFillColor = System.Drawing.Color.White;
            this.bunifuThinButton22.IdleForecolor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton22.IdleLineColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton22.Location = new System.Drawing.Point(24, 185);
            this.bunifuThinButton22.Margin = new System.Windows.Forms.Padding(5);
            this.bunifuThinButton22.Name = "bunifuThinButton22";
            this.bunifuThinButton22.Size = new System.Drawing.Size(215, 71);
            this.bunifuThinButton22.TabIndex = 4;
            this.bunifuThinButton22.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuThinButton22.Click += new System.EventHandler(this.bunifuThinButton22_Click);
            // 
            // bunifuCustomLabel2
            // 
            this.animatorPanelGradient.SetDecoration(this.bunifuCustomLabel2, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelMapWay.SetDecoration(this.bunifuCustomLabel2, BunifuAnimatorNS.DecorationType.None);
            this.bunifuCustomLabel2.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuCustomLabel2.Location = new System.Drawing.Point(26, 27);
            this.bunifuCustomLabel2.Name = "bunifuCustomLabel2";
            this.bunifuCustomLabel2.Size = new System.Drawing.Size(299, 76);
            this.bunifuCustomLabel2.TabIndex = 1;
            this.bunifuCustomLabel2.Text = "Установка UDP соединения";
            this.bunifuCustomLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Gray;
            this.panel2.Controls.Add(this.bunifuThinButton21);
            this.panel2.Controls.Add(this.bunifuTileButton1);
            this.panel2.Controls.Add(this.bunifuCustomLabel1);
            this.panel2.Controls.Add(this.bunifuDropdown1);
            this.animatorPanelMapWay.SetDecoration(this.panel2, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.panel2, BunifuAnimatorNS.DecorationType.None);
            this.panel2.Location = new System.Drawing.Point(20, 18);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(363, 422);
            this.panel2.TabIndex = 0;
            // 
            // bunifuThinButton21
            // 
            this.bunifuThinButton21.ActiveBorderThickness = 1;
            this.bunifuThinButton21.ActiveCornerRadius = 20;
            this.bunifuThinButton21.ActiveFillColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton21.ActiveForecolor = System.Drawing.Color.White;
            this.bunifuThinButton21.ActiveLineColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton21.BackColor = System.Drawing.Color.Gray;
            this.bunifuThinButton21.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuThinButton21.BackgroundImage")));
            this.bunifuThinButton21.ButtonText = "Подключиться";
            this.bunifuThinButton21.Cursor = System.Windows.Forms.Cursors.Hand;
            this.animatorPanelMapWay.SetDecoration(this.bunifuThinButton21, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.bunifuThinButton21, BunifuAnimatorNS.DecorationType.None);
            this.bunifuThinButton21.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bunifuThinButton21.ForeColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton21.IdleBorderThickness = 1;
            this.bunifuThinButton21.IdleCornerRadius = 20;
            this.bunifuThinButton21.IdleFillColor = System.Drawing.Color.White;
            this.bunifuThinButton21.IdleForecolor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton21.IdleLineColor = System.Drawing.Color.SeaGreen;
            this.bunifuThinButton21.Location = new System.Drawing.Point(14, 224);
            this.bunifuThinButton21.Margin = new System.Windows.Forms.Padding(5);
            this.bunifuThinButton21.Name = "bunifuThinButton21";
            this.bunifuThinButton21.Size = new System.Drawing.Size(215, 71);
            this.bunifuThinButton21.TabIndex = 4;
            this.bunifuThinButton21.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.bunifuThinButton21.Click += new System.EventHandler(this.bunifuThinButton21_Click);
            // 
            // bunifuTileButton1
            // 
            this.bunifuTileButton1.BackColor = System.Drawing.Color.SeaGreen;
            this.bunifuTileButton1.color = System.Drawing.Color.SeaGreen;
            this.bunifuTileButton1.colorActive = System.Drawing.Color.MediumSeaGreen;
            this.bunifuTileButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.animatorPanelMapWay.SetDecoration(this.bunifuTileButton1, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelGradient.SetDecoration(this.bunifuTileButton1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuTileButton1.Font = new System.Drawing.Font("Century Gothic", 15.75F);
            this.bunifuTileButton1.ForeColor = System.Drawing.Color.White;
            this.bunifuTileButton1.Image = global::TestMAP.Properties.Resources.refresh;
            this.bunifuTileButton1.ImagePosition = 0;
            this.bunifuTileButton1.ImageZoom = 100;
            this.bunifuTileButton1.LabelPosition = 0;
            this.bunifuTileButton1.LabelText = "";
            this.bunifuTileButton1.Location = new System.Drawing.Point(251, 136);
            this.bunifuTileButton1.Margin = new System.Windows.Forms.Padding(6);
            this.bunifuTileButton1.Name = "bunifuTileButton1";
            this.bunifuTileButton1.Size = new System.Drawing.Size(60, 62);
            this.bunifuTileButton1.TabIndex = 2;
            this.bunifuTileButton1.Click += new System.EventHandler(this.bunifuTileButton1_Click);
            // 
            // bunifuCustomLabel1
            // 
            this.animatorPanelGradient.SetDecoration(this.bunifuCustomLabel1, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelMapWay.SetDecoration(this.bunifuCustomLabel1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuCustomLabel1.Font = new System.Drawing.Font("Century Gothic", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.bunifuCustomLabel1.Location = new System.Drawing.Point(24, 27);
            this.bunifuCustomLabel1.Name = "bunifuCustomLabel1";
            this.bunifuCustomLabel1.Size = new System.Drawing.Size(299, 76);
            this.bunifuCustomLabel1.TabIndex = 1;
            this.bunifuCustomLabel1.Text = "Выбор манипулятора";
            this.bunifuCustomLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // bunifuDropdown1
            // 
            this.bunifuDropdown1.BackColor = System.Drawing.Color.Transparent;
            this.bunifuDropdown1.BorderRadius = 3;
            this.animatorPanelGradient.SetDecoration(this.bunifuDropdown1, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelMapWay.SetDecoration(this.bunifuDropdown1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuDropdown1.DisabledColor = System.Drawing.Color.Gray;
            this.bunifuDropdown1.ForeColor = System.Drawing.Color.White;
            this.bunifuDropdown1.Items = new string[0];
            this.bunifuDropdown1.Location = new System.Drawing.Point(14, 136);
            this.bunifuDropdown1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bunifuDropdown1.Name = "bunifuDropdown1";
            this.bunifuDropdown1.NomalColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(139)))), ((int)(((byte)(87)))));
            this.bunifuDropdown1.onHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(129)))), ((int)(((byte)(77)))));
            this.bunifuDropdown1.selectedIndex = -1;
            this.bunifuDropdown1.Size = new System.Drawing.Size(215, 62);
            this.bunifuDropdown1.TabIndex = 0;
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
            // pictureBox1
            // 
            this.animatorPanelGradient.SetDecoration(this.pictureBox1, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelMapWay.SetDecoration(this.pictureBox1, BunifuAnimatorNS.DecorationType.None);
            this.pictureBox1.Location = new System.Drawing.Point(3, 389);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(274, 282);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
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
            // animatorPanelMapWay
            // 
            this.animatorPanelMapWay.AnimationType = BunifuAnimatorNS.AnimationType.HorizSlide;
            this.animatorPanelMapWay.Cursor = null;
            animation8.AnimateOnlyDifferences = true;
            animation8.BlindCoeff = ((System.Drawing.PointF)(resources.GetObject("animation8.BlindCoeff")));
            animation8.LeafCoeff = 0F;
            animation8.MaxTime = 1F;
            animation8.MinTime = 0F;
            animation8.MosaicCoeff = ((System.Drawing.PointF)(resources.GetObject("animation8.MosaicCoeff")));
            animation8.MosaicShift = ((System.Drawing.PointF)(resources.GetObject("animation8.MosaicShift")));
            animation8.MosaicSize = 0;
            animation8.Padding = new System.Windows.Forms.Padding(0);
            animation8.RotateCoeff = 0F;
            animation8.RotateLimit = 0F;
            animation8.ScaleCoeff = ((System.Drawing.PointF)(resources.GetObject("animation8.ScaleCoeff")));
            animation8.SlideCoeff = ((System.Drawing.PointF)(resources.GetObject("animation8.SlideCoeff")));
            animation8.TimeCoeff = 0F;
            animation8.TransparencyCoeff = 0F;
            this.animatorPanelMapWay.DefaultAnimation = animation8;
            this.animatorPanelMapWay.TimeStep = 0.05F;
            // 
            // bunifuCheckbox1
            // 
            this.bunifuCheckbox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(205)))), ((int)(((byte)(117)))));
            this.bunifuCheckbox1.ChechedOffColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.bunifuCheckbox1.Checked = true;
            this.bunifuCheckbox1.CheckedOnColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(205)))), ((int)(((byte)(117)))));
            this.animatorPanelGradient.SetDecoration(this.bunifuCheckbox1, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelMapWay.SetDecoration(this.bunifuCheckbox1, BunifuAnimatorNS.DecorationType.None);
            this.bunifuCheckbox1.ForeColor = System.Drawing.Color.White;
            this.bunifuCheckbox1.Location = new System.Drawing.Point(26, 264);
            this.bunifuCheckbox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.bunifuCheckbox1.Name = "bunifuCheckbox1";
            this.bunifuCheckbox1.Size = new System.Drawing.Size(20, 20);
            this.bunifuCheckbox1.TabIndex = 7;
            this.bunifuCheckbox1.OnChange += new System.EventHandler(this.bunifuCheckbox1_OnChange);
            // 
            // bunifuCustomLabel3
            // 
            this.animatorPanelGradient.SetDecoration(this.bunifuCustomLabel3, BunifuAnimatorNS.DecorationType.None);
            this.animatorPanelMapWay.SetDecoration(this.bunifuCustomLabel3, BunifuAnimatorNS.DecorationType.None);
            this.bunifuCustomLabel3.Location = new System.Drawing.Point(53, 264);
            this.bunifuCustomLabel3.Name = "bunifuCustomLabel3";
            this.bunifuCustomLabel3.Size = new System.Drawing.Size(200, 20);
            this.bunifuCustomLabel3.TabIndex = 8;
            this.bunifuCustomLabel3.Text = "Подключаться при запуске";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1842, 930);
            this.Controls.Add(this.panel1);
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
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panelMenuGradient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuButton)).EndInit();
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
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Bunifu.Framework.UI.BunifuTileButton bunifuTileButton1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel1;
        private Bunifu.Framework.UI.BunifuDropdown bunifuDropdown1;
        private Bunifu.Framework.UI.BunifuThinButton2 bunifuThinButton21;
        private System.Windows.Forms.Panel panel3;
        private Bunifu.Framework.UI.BunifuThinButton2 bunifuThinButton22;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel2;
        private Bunifu.Framework.UI.BunifuTextbox bunifuTextbox1;
        private Bunifu.Framework.UI.BunifuCheckbox bunifuCheckbox1;
        private Bunifu.Framework.UI.BunifuCustomLabel bunifuCustomLabel3;
    }
}

