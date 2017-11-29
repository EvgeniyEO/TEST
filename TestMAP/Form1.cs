using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GMap;
using GMap.NET;
using GMap.NET.WindowsForms;
using MetroFramework.Forms;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms.Markers;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Drawing.Drawing2D;
using TestMAP.Properties;
using System.Text.RegularExpressions;
using System.Net;

namespace TestMAP
{
    public partial class Form1 : MetroForm
    {
        // Колонки в таблице bunifuCustomDataGrid1
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNoMarker;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColInMission;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLatLon;
        private System.Windows.Forms.DataGridViewImageColumn ColDelMarker;
        private TestMAP.MaskedEditColumn ColDown;
        private TestMAP.MyDGVCheckBoxColumn ColDownMode;
        private TestMAP.MaskedEditColumn ColSpeed;
        //private System.Windows.Forms.Panel panelSettings;
        //private System.Windows.Forms.Panel panelManual;
        //Переменная нового класса,
        //для замены стандартного маркера.
        private TestMAP.GMapMarkerImage currentMarker;
        //Список маркеров и роутов
        private GMap.NET.WindowsForms.GMapOverlay markersOverlay;
        private GMap.NET.WindowsForms.GMapOverlay routOverlay;
        private GMap.NET.WindowsForms.GMapRoute routes;
        private Bitmap bitmapBlackCh;
        private Bitmap bitmapBlackNCh;

        //private Bunifu.Framework.UI.BunifuMetroTextbox ManualTextboxX;
        private TestMAP.DirectInputController DirectInputCntrl;

        List<JoystickDescriptor> ListJoyDescriptor = null;
        private TestMAP.UDPClientClass UDPClient = null;
        private TestMAP.ReceiveBytesData ReceiveData = new ReceiveBytesData();

        //Переменная отвечающая за состояние нажатия 
        //левой клавиши мыши.
        private bool isLeftButtonDown = false;
        //Таймер для вывода
        private Timer blinkTimer = new Timer();

        int currentMarkerInd = 0;
        string StrFormatLatLng = "{0:0.0000000} - {1:0.0000000}";
        Point CurrentMenuPoint;
        List<PointLatLng> polygonPoints1 = new List<PointLatLng>();

        public Form1()
        {
            try
            {
                InitializeComponent();
                MyInitializeComponent();
                initDefaultSettings();
            }
            catch (Exception ex)
            {
                LogError.MessageError(ex, null, "Form1()", true);
            }
            
        }
        public void initDefaultSettings()
        {
            // Joustic -----------------------------------------------------
            bunifuCheckbox2.Checked = Settings.Default.SettingJoyAutoConnect;

            if (Settings.Default.SettingJoyAutoConnect && !string.IsNullOrEmpty(Settings.Default.SettingJoyStringConnect))
            {
                try
                {
                    if ( DirectInputCntrl != null && !(DirectInputCntrl.IsConnect()) )
                    {
                        int index = -1;
                        bunifuDropdown1.Clear();
                        ListJoyDescriptor = DirectInputCntrl.DetectDevices();
                        if ( ListJoyDescriptor != null )
                        {
                            foreach (var Descriptor in ListJoyDescriptor)
                            {
                                bunifuDropdown1.AddItem(Descriptor.DescriptorName);
                                if (string.Equals(Descriptor.DescriptorName, Settings.Default.SettingJoyStringConnect))
                                    index = ListJoyDescriptor.IndexOf(Descriptor);
                                if (index >= 0)
                                {
                                    DirectInputCntrl.StartCapture(ListJoyDescriptor[index].DescriptorGuid);
                                    bunifuDropdown1.selectedIndex = index;
                                }
                            }
                            if ( bunifuDropdown1.selectedIndex >= 0 )
                            {
                                bunifuThinButton21.ButtonText = "Подключиться";
                            }
                            else
                            {
                                bunifuThinButton21.ButtonText = "Отключить";
                            }                         
                        }
                    }
                    else
                    {
                        bunifuTileButton1_Click(this, new EventArgs());
                        bunifuThinButton21.ButtonText = "Подключиться";
                    }
                }
                catch (Exception initDefaultSettingsJoyExc)
                {
                    LogError.MessageError(initDefaultSettingsJoyExc, null, "Joustic initialization section", true);
                }
            }
            else
            {
                bunifuTileButton1_Click(this, new EventArgs());
                bunifuThinButton21.ButtonText = "Подключиться";
            }

            try
            {
                // UDP Connect -------------------------------------------------
                bunifuCheckbox1.Checked = Settings.Default.SettingUdpAutoConnect;
                if (!string.IsNullOrEmpty(Settings.Default.SettingUdpPort))
                    maskedTextBox3.Text = Settings.Default.SettingUdpPort;
                if (!string.IsNullOrEmpty(Settings.Default.SettingAddressMud))
                {
                    maskedTextBox1.Text = Settings.Default.SettingAddressMud;
                    UDPClientClass.ipEndPoint_MUD = new System.Net.IPEndPoint(
                        IPAddress.Parse(Settings.Default.SettingAddressMud.Substring(0, 15)), 
                        Convert.ToInt32(Settings.Default.SettingAddressMud.Substring(16, 5))
                        );
                }
                
                if (!string.IsNullOrEmpty(Settings.Default.SettingAddressNav))
                {
                    maskedTextBox2.Text = Settings.Default.SettingAddressNav;
                    UDPClientClass.ipEndPoint_NAV = new System.Net.IPEndPoint(
                        IPAddress.Parse(Settings.Default.SettingAddressNav.Substring(0, 15)),
                        Convert.ToInt32(Settings.Default.SettingAddressNav.Substring(16, 5))
                        );
                }

                //PWM--------
                if (!string.IsNullOrEmpty(Settings.Default.SettingAddressPwm1))
                {
                    maskedTextPwm1.Text = Settings.Default.SettingAddressPwm1;
                    UDPClientClass.ipEndPoint_Pwm1 = new System.Net.IPEndPoint(
                        IPAddress.Parse(Settings.Default.SettingAddressPwm1.Substring(0, 15)),
                        Convert.ToInt32(Settings.Default.SettingAddressPwm1.Substring(16, 5))
                        );
                }

                if (!string.IsNullOrEmpty(Settings.Default.SettingAddressPwm2))
                {
                    maskedTextPwm2.Text = Settings.Default.SettingAddressPwm2;
                    UDPClientClass.ipEndPoint_Pwm2 = new System.Net.IPEndPoint(
                        IPAddress.Parse(Settings.Default.SettingAddressPwm2.Substring(0, 15)),
                        Convert.ToInt32(Settings.Default.SettingAddressPwm2.Substring(16, 5))
                        );
                }

                if (!string.IsNullOrEmpty(Settings.Default.SettingAddressPwm3))
                {
                    maskedTextPwm3.Text = Settings.Default.SettingAddressPwm3;
                    UDPClientClass.ipEndPoint_Pwm3 = new System.Net.IPEndPoint(
                        IPAddress.Parse(Settings.Default.SettingAddressPwm3.Substring(0, 15)),
                        Convert.ToInt32(Settings.Default.SettingAddressPwm3.Substring(16, 5))
                        );
                }

                if (!string.IsNullOrEmpty(Settings.Default.SettingAddressEngine))
                {
                    maskedTextEngine.Text = Settings.Default.SettingAddressEngine;
                    UDPClientClass.ipEndPoint_Engine = new System.Net.IPEndPoint(
                        IPAddress.Parse(Settings.Default.SettingAddressEngine.Substring(0, 15)),
                        Convert.ToInt32(Settings.Default.SettingAddressEngine.Substring(16, 5))
                        );
                }
                //--------
                
                if (UDPClient!=null && Settings.Default.SettingUdpAutoConnect && !string.IsNullOrEmpty(Settings.Default.SettingUdpPort))
                {
                    UDPClient.StartReceiving(Convert.ToInt32(Settings.Default.SettingUdpPort));
                    if (UDPClient.IsConnect())
                    {
                        bunifuThinButton22.ButtonText = "Отключить";
                    }
                }
            }
            catch (Exception ex)
            {
                LogError.MessageError(ex, null, "Udp initialization section", true);
            }
        }
        private void MyInitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ColNoMarker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColInMission = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColLatLon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDelMarker = new System.Windows.Forms.DataGridViewImageColumn();
            this.ColDown = new TestMAP.MaskedEditColumn();
            this.ColDownMode = new TestMAP.MyDGVCheckBoxColumn();
            this.ColSpeed = new TestMAP.MaskedEditColumn();
            //this.panelSettings = new System.Windows.Forms.Panel();
            //this.panelManual = new System.Windows.Forms.Panel();
            this.DirectInputCntrl = new DirectInputController();

            this.DirectInputCntrl.JoystickYchange += JoystickY_change;
            this.DirectInputCntrl.JoystickRotationXchange += JoystickRotationX_change;
            this.DirectInputCntrl.JoystickRotationYchange += JoystickRotationY_change;
            this.DirectInputCntrl.Packet_CA_change += JoystickPacket_CA_change;
            //List<JoystickDescriptor> ListJoy = DirectInputCntrl.DetectDevices();
            //DirectInputCntrl.StartCapture(ListJoy[0].DescriptorGuid);

            this.UDPClient = new UDPClientClass(50000);
            UDPClient.ReceiveData += UDPClient_ReceiveData;
            //UDPClient.StartReceiving();

            //this.ManualTextboxX = new Bunifu.Framework.UI.BunifuMetroTextbox();
            //this.panelManual.Controls.Add(this.ManualTextboxX);


            //// 
            //// ManualTextboxX
            //// 
            ////this.ManualTextboxX.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuTextbox1.BackgroundImage")));
            ////this.ManualTextboxX.Icon = null;
            //this.ManualTextboxX.BackColor = System.Drawing.Color.Silver;
            //this.ManualTextboxX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            //this.ManualTextboxX.ForeColor = System.Drawing.Color.SeaGreen;
            //this.ManualTextboxX.Location = new System.Drawing.Point(39, 449);
            //this.ManualTextboxX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            //this.ManualTextboxX.Name = "ManualTextboxX";
            //this.ManualTextboxX.Size = new System.Drawing.Size(195, 114);
            //this.ManualTextboxX.TabIndex = 4;
            //this.ManualTextboxX.Text = "ManualTextboxX";
            //this.ManualTextboxX.Visible = true;
            
            // 
            // panelSettings
            // 
            //this.panelSettings.SuspendLayout();
            //this.Controls.Add(this.panelSettings);
            //this.panelSettings.ResumeLayout(false);

            //this.panelSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            ////this.panelMapWay.Controls.Add(this.panelTab);
            ////this.panelMapWay.Controls.Add(this.panelMap);
            ////this.animatorPanelGradient.SetDecoration(this.panelSettings, BunifuAnimatorNS.DecorationType.None);
            //this.panelSettings.Location = new System.Drawing.Point(310, 30);
            //this.panelSettings.Name = "panelSettings";
            //this.panelSettings.Size = new System.Drawing.Size(1517, 880);
            //this.panelSettings.TabIndex = 10;

            // 
            // panelManual
            // 
            //this.panelManual.SuspendLayout();
            //this.Controls.Add(this.panelManual);
            //this.panelManual.ResumeLayout(false);

            //this.panelManual.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            ////this.panelMapWay.Controls.Add(this.panelTab);
            ////this.panelMapWay.Controls.Add(this.panelMap);
            ////this.animatorPanelGradient.SetDecoration(this.panelSettings, BunifuAnimatorNS.DecorationType.None);
            //this.panelManual.Location = new System.Drawing.Point(310, 30);
            //this.panelManual.Name = "panelManual";
            //this.panelManual.Size = new System.Drawing.Size(1517, 880);
            //this.panelManual.TabIndex = 10;

            this.bunifuGridWayPoint.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNoMarker,
            this.ColInMission,
            this.ColLatLon,
            this.ColDown,
            this.ColDownMode,
            this.ColSpeed,
            this.ColDelMarker
            });

            ColNoMarker.HeaderText = "№ Marker";
            ColNoMarker.Name = "ColNoMarker";
            ColNoMarker.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            //ColNoMarker.Resizable = System.Windows.Forms.DataGridViewTriState.True;


            ColInMission.HeaderText = "In Mission";
            ColInMission.Name = "ColInMission";
            ColInMission.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;

            ColLatLon.HeaderText = "Lat / Lon";
            ColLatLon.Name = "ColLatLng";
            ColLatLon.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            ColDelMarker.HeaderText = "Delete";
            ColDelMarker.Name = "ColDelMarker";
            ColDelMarker.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            ColDelMarker.Image = global::TestMAP.Properties.Resources.delete_1;

            ColDown.HeaderText = "Down";
            ColDown.Name = "ColDown";
            ColDown.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ColDown.Mask = @"0000\.0\m";

            ColDownMode.HeaderText = "Down Mode";
            ColDownMode.Name = "ColDownMode";
            ColDownMode.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ColDownMode.Label = "Depth";
            ColDownMode.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            ColSpeed.HeaderText = "Speed";
            ColSpeed.Name = "ColSpeed";
            ColSpeed.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ColSpeed.Mask = @"000\%";

            bitmapBlackCh =
                Bitmap.FromFile(Application.StartupPath + @"\BlackCheck.png") as Bitmap;

            bitmapBlackNCh =
                Bitmap.FromFile(Application.StartupPath + @"\BlackNoCheck.png") as Bitmap;

            this.bunifuGridWayPoint.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.bunifuCustomDataGrid1_CellValueChanged);
            this.bunifuGridWayPoint.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.bunifuCustomDataGrid1_CellContentClick);
            this.bunifuGridWayPoint.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.bunifuCustomDataGrid1_CellClick);


            panelMenuGradient.Width = 280;
            FlatButtonMapWay.Width = 280;
            FlatButtonControl.Width = 280;
            FlatButtonSettings.Width = 280;

            pictureBox1.BackColor = Color.White;
            pictureBox1.Visible = true;

            // Connect the Paint event of the PictureBox to the event handler method.
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            pictureBox2.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            pictureBox3.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox3_Paint);
            pictureBox4.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox4_Paint);
        }

        private void MyDisposeComponent()
        {
            if (DirectInputCntrl != null)
            {
                DirectInputCntrl.StopCapture();
                DirectInputCntrl.Dispose();
            }
            
        }
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            MyDisposeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //Настройки для компонента GMap.
            gMapControl1.Bearing = 0;

            //CanDragMap - Если параметр установлен в True,
            //пользователь может перетаскивать карту 
            ///с помощью правой кнопки мыши. 
            gMapControl1.CanDragMap = true;

            //Указываем, что перетаскивание карты осуществляется 
            //с использованием левой клавишей мыши.
            //По умолчанию - правая.
            gMapControl1.DragButton = MouseButtons.Left;

            gMapControl1.GrayScaleMode = true;

            //MarkersEnabled - Если параметр установлен в True,
            //любые маркеры, заданные вручную будет показаны.
            //Если нет, они не появятся.
            gMapControl1.MarkersEnabled = true;

            //Указываем значение максимального приближения.
            gMapControl1.MaxZoom = 18;

            //Указываем значение минимального приближения.
            gMapControl1.MinZoom = 2;

            //Устанавливаем центр приближения/удаления
            //курсор мыши.
            gMapControl1.MouseWheelZoomType =
                GMap.NET.MouseWheelZoomType.MousePositionAndCenter;

            //Отказываемся от негативного режима.
            gMapControl1.NegativeMode = false;

            //Разрешаем полигоны.
            gMapControl1.PolygonsEnabled = true;

            //Разрешаем маршруты
            gMapControl1.RoutesEnabled = true;

            //Скрываем внешнюю сетку карты
            //с заголовками.
            gMapControl1.ShowTileGridLines = false;

            //Указываем, что при загрузке карты будет использоваться 
            //18ти кратной приближение.
            gMapControl1.Zoom = 15;

            //Указываем что все края элемента управления
            //закрепляются у краев содержащего его элемента
            //управления(главной формы), а их размеры изменяются 
            //соответствующим образом.
            gMapControl1.Dock = DockStyle.Fill;

            //Указываем что будем использовать карты Google.
            gMapControl1.MapProvider = 
                GMap.NET.MapProviders.GMapProviders.GoogleMap;
            GMap.NET.GMaps.Instance.Mode =
                GMap.NET.AccessMode.ServerOnly;

            //Если вы используете интернет через прокси сервер,
            //указываем свои учетные данные.
            GMap.NET.MapProviders.GMapProvider.WebProxy =
                System.Net.WebRequest.GetSystemWebProxy();
            GMap.NET.MapProviders.GMapProvider.WebProxy.Credentials =
                System.Net.CredentialCache.DefaultCredentials;

            //Устанавливаем координаты центра карты для загрузки.
            gMapControl1.Position = new GMap.NET.PointLatLng(55.75393, 37.620795);

            //Создаем новый список маркеров, с указанием компонента 
            //в котором они будут использоваться и названием списка.
            markersOverlay = new GMap.NET.WindowsForms.GMapOverlay("marker");
            gMapControl1.Overlays.Add(markersOverlay);
            routOverlay = new GMap.NET.WindowsForms.GMapOverlay("rout");
            gMapControl1.Overlays.Add(routOverlay);

            //Устанавливаем свои методы на события.
            gMapControl1.OnMapZoomChanged +=
                new MapZoomChanged(mapControl_OnMapZoomChanged);
            gMapControl1.MouseClick +=
                new MouseEventHandler(mapControl_MouseClick);
            gMapControl1.MouseDown +=
                new MouseEventHandler(mapControl_MouseDown);
            gMapControl1.MouseUp +=
                new MouseEventHandler(mapControl_MouseUp);
            gMapControl1.MouseMove +=
                new MouseEventHandler(mapControl_MouseMove);
            gMapControl1.OnMarkerClick +=
                new MarkerClick(mapControl_OnMarkerClick);
            gMapControl1.OnMarkerEnter +=
                new MarkerEnter(mapControl_OnMarkerEnter);
            gMapControl1.OnMarkerLeave +=
                new MarkerLeave(mapControl_OnMarkerLeave);

            routes = new GMap.NET.WindowsForms.GMapRoute(polygonPoints1, "single_line");
            routes.Stroke.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            routOverlay.Routes.Add(routes);     


            //Добавляем в элемент управления карты
            //список маркеров.
            gMapControl1.Overlays.Add(markersOverlay);
            gMapControl1.Overlays.Add(routOverlay);
            //System.Windows.Forms.datagridviewim
        }

        //Метод, отвечающий за перемещение
        //маркера левой клавишей мыши
        //по карте и отображения подсказки с 
        //текущими координатами маркера.
        void mapControl_MouseMove(object sender, MouseEventArgs e)
        {
            //Проверка, что нажата левая клавиша мыши.
            if (e.Button == System.Windows.Forms.MouseButtons.Left && isLeftButtonDown)
            {
                if (currentMarker != null && currentMarkerInd >= 0)
                {
                    PointLatLng point =
                        gMapControl1.FromLocalToLatLng(e.X, e.Y);
                    //Получение координат маркера.
                    currentMarker.Position = point;

                    // Удаляем точку из с предыдущей позицией трека маркеров
                    routes.Points.RemoveAt(currentMarkerInd);

                    // Добавляем точку в трек для слежения линии за перетаскиванием маркера
                    routes.Points.Insert(currentMarkerInd, point);
                    
                    gMapControl1.UpdateRouteLocalPosition(routes);
                    //Вывод координат маркера в подсказке.
                    currentMarker.ToolTipText =
                        string.Format(StrFormatLatLng, point.Lat, point.Lng);

                    // Изменение  Lat/Lng в таблице вместе с переносом маркера
                    bunifuGridWayPoint.Rows[currentMarkerInd].Cells[ColLatLon.Name].Value = string.Format(StrFormatLatLng, point.Lat, point.Lng);
                }
            }
        }
        void mapControl_MouseUp(object sender, MouseEventArgs e)
        {
            //Выполняем проверку, какая клавиша мыши была отпущена,
            //если левая, то устанавливаем переменной значение false.
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                isLeftButtonDown = false;
                //routes.Points.Insert(currentMarkerInd,currentMarker.Position);
                //gMapControl1.UpdateRouteLocalPosition(routes); 
            }
        }
        int AddMarkerToEnd(int x, int y, bool inMission)
        {
            // Индекс добавленного маркера
            int retIndex = 0;

            PointLatLng point = gMapControl1.FromLocalToLatLng(x, y);
            GMapMarker marker = new GMapMarkerImage(point, inMission ? bitmapBlackCh : bitmapBlackNCh);
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.ToolTipText = string.Format(StrFormatLatLng, point.Lat, point.Lng);
            // Добавляем маркер в список маркеров.
            markersOverlay.Markers.Add(marker);
            // Добавляем точку маркера в маршрут
            routes.Points.Add(point);
            // Обновляем карту для перерисовки
            gMapControl1.UpdateRouteLocalPosition(routes);

            retIndex = markersOverlay.Markers.IndexOf(marker);

            return retIndex;
        }
        void AddMarkerByIndex(long x, long y, int Index, bool inMission)
        {
            PointLatLng point = gMapControl1.FromLocalToLatLng((int)x, (int)y);
            GMapMarker marker = new GMapMarkerImage(point, inMission ? bitmapBlackCh : bitmapBlackNCh);
            marker.ToolTipMode = inMission ? MarkerTooltipMode.Always : MarkerTooltipMode.OnMouseOver;
            marker.ToolTipText = Index.ToString();
            // Добавляем маркер в список маркеров.
            markersOverlay.Markers.Insert(Index, marker);
            // Добавляем точку маркера в маршрут
            routes.Points.Insert(Index, point);
            // Обновляем карту для перерисовки
            gMapControl1.UpdateRouteLocalPosition(routes);
        }
        void DropMarkerByIndex( int Index )
        {
            routes.Points.RemoveAt(Index);
            markersOverlay.Markers.RemoveAt(Index);
        }
        void updateToolTipText()
        {

            for (int i = 0; i < routes.Points.Count; i++)
            {
                markersOverlay.Markers[i].ToolTipText = i.ToString();
            }
        }
        void switchChekedMarker(int Index, bool inMission)
        {
            GPoint pnt = gMapControl1.FromLatLngToLocal(markersOverlay.Markers[Index].Position);
            DropMarkerByIndex(Index);
            AddMarkerByIndex(pnt.X, pnt.Y, Index, inMission);
        }
        private void Method1(object sender, EventArgs e)
        {
            int index = AddMarkerToEnd(CurrentMenuPoint.X, CurrentMenuPoint.Y, true);
            markersOverlay.Markers[index].ToolTipText = index.ToString();
            markersOverlay.Markers[index].ToolTipMode = MarkerTooltipMode.Always;
            bunifuGridWayPoint.Rows.Add(index, true, string.Format(StrFormatLatLng, markersOverlay.Markers[index].Position.Lat, markersOverlay.Markers[index].Position.Lng));
            bunifuGridWayPoint.Rows[index].Cells["ColDown"].Value = "0003.0m";
            bunifuGridWayPoint.Rows[index].Cells["ColSpeed"].Value = "070%";
            
        }
        private void Method2(object sender, EventArgs e)
        {
            if (currentMarker != null)
            {
                PointLatLng point = currentMarker.Position;
                currentMarkerInd = routes.Points.IndexOf(point);

                routes.Points.Remove(point);
                markersOverlay.Markers.Remove(currentMarker);
                gMapControl1.UpdateRouteLocalPosition(routes);

                bunifuGridWayPoint.Rows.RemoveAt(currentMarkerInd);
                foreach (DataGridViewRow Row in bunifuGridWayPoint.Rows)
                {
                    if (Convert.ToInt32(Row.Cells["ColNoMarker"].Value) > currentMarkerInd)
                    {
                        Row.Cells["ColNoMarker"].Value = Row.Index.ToString();
                        markersOverlay.Markers[Row.Index].ToolTipText = Row.Index.ToString();
                    }                                     
                }
            }
        }
        private void Method3(object sender, EventArgs e)
        {
            //routes.Points.RemoveAll(x => !x.IsZero);

            // Очистка точек маршрута
            routes.Points.Clear();
            // Очистка Маркеров
            markersOverlay.Markers.Clear();
            // Перерисовка
            gMapControl1.UpdateRouteLocalPosition(routes);
            // Очистка таблицы от точек
            bunifuGridWayPoint.Rows.Clear();

        }
        void mapControl_MouseDown(object sender, MouseEventArgs e)
        {
            ContextMenu rightClickMenuStrip = new ContextMenu();
            rightClickMenuStrip.MenuItems.Add("Добавить точку", new EventHandler(Method1));
            rightClickMenuStrip.MenuItems.Add("Удалить точку", new EventHandler(Method2));
            rightClickMenuStrip.MenuItems.Add("Удалить все точки", new EventHandler(Method3));

            GMapControl GMapCtrl;
            if (sender is GMap.NET.WindowsForms.GMapControl)
            {
                GMapCtrl = sender as GMap.NET.WindowsForms.GMapControl;
            }
            else
            {
                GMapCtrl = this.gMapControl1;
            }


            switch (e.Button)
            {
                case MouseButtons.Left:
                    
                    if (currentMarker != null && isLeftButtonDown == false)
                    {
                        isLeftButtonDown = true;
                        PointLatLng point = currentMarker.Position;
                        currentMarkerInd = routes.Points.IndexOf(point);
                    }
                    break;
                case MouseButtons.Middle:
                    CurrentMenuPoint = new Point(e.X, e.Y);
                    Method1(sender, e as EventArgs);
                    break;
                case MouseButtons.None:
                    break;
                case MouseButtons.Right:
                    CurrentMenuPoint = new Point(e.X, e.Y);
                    rightClickMenuStrip.Show(GMapCtrl, CurrentMenuPoint);
                    break;
                case MouseButtons.XButton1:
                    break;
                case MouseButtons.XButton2:
                    break;
                default:
                    break;
            }
        }
        //Убираем квадрат выделения маркера
        //если нет действий с маркером.
        void mapControl_OnMarkerLeave(GMapMarker item)
        {
            if (item is GMapMarkerImage && isLeftButtonDown == false)
            {
                currentMarker = null;
                GMapMarkerImage m = item as GMapMarkerImage;
                if ( m.Pen != null )
                {
                    m.Pen.Dispose();
                    m.Pen = null;
                }
                //Возвращаем значение подписи маркера равное его порядковому номеру
                m.ToolTipText = routes.Points.IndexOf(m.Position).ToString();
            }
        }

        //Устанавливаем вокруг маркера красный квадрат
        //если маркер выделен клавишей Enter
        void mapControl_OnMarkerEnter(GMapMarker item)
        {
            if (item is GMapMarkerImage && isLeftButtonDown == false)
            {
                currentMarker = item as GMapMarkerImage;
                currentMarker.Pen = new Pen(Brushes.Red, 2);
                currentMarker.ToolTipText = string.Format(StrFormatLatLng, currentMarker.Position.Lat, currentMarker.Position.Lng);
            }
        }
        void mapControl_OnMarkerClick(GMapMarker item, MouseEventArgs e)
        {
        }
        void mapControl_MouseClick(object sender, MouseEventArgs e)
        {
            ////Выполняем проверку, какая клавиша мыши была нажата,
            ////если правая, то выполняем установку маркера.
            //if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    //Если надо установить только один маркер,
            //    //то выполняем очистку списка маркеров
            //    //markersOverlay.Markers.Clear();
            //    PointLatLng point = gMapControl1.FromLocalToLatLng(e.X, e.Y);

            //    //Инициализируем новую переменную изображения и
            //    //загружаем в нее изображение маркера,
            //    //лежащее возле исполняемого файла
            //    Bitmap bitmap =
            //        Bitmap.FromFile(Application.StartupPath + @"\marker.png") as Bitmap;

            //    //Инициализируем новый маркер с использованием 
            //    //созданного нами маркера.
            //    GMapMarker marker = new GMapMarkerImage(point, bitmap);
            //    marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;

            //    //В качестве подсказки к маркеру устанавливаем 
            //    //координаты где он устанавливается.
            //    //Данные о местоположении маркера, вы можете вывести в любой компонент
            //    //который вам нужен.
            //    //например:
            //    //textBo1.Text = point.Lat;
            //    //textBo2.Text = point.Lng;
            //    marker.ToolTipText = string.Format("{0},{1}", point.Lat, point.Lng);

            //    //polygonPoints1.Add(point);
            //    routes.Points.Add(point);
            //    gMapControl1.UpdateRouteLocalPosition(routes);
            //    //routOverlay.Routes.Add(routes);


            //    //Добавляем маркер в список маркеров.
            //    markersOverlay.Markers.Add(marker);
            //}
        }
        //Событие изменения масштаба
        void mapControl_OnMapZoomChanged()
        {
        }
        //Запускаем таймер при наведении на маркер
        //private void buttonBeginBlink_Click(object sender, EventArgs e)
        //{
        //    //Устанавливаем интервал срабатывания
        //    //таймера, равным одной секунде.
        //    blinkTimer.Interval = 1000;

        //    //Добавляем свое событие на каждое
        //    //срабатывание таймера.
        //    blinkTimer.Tick += new EventHandler(blinkTimer_Tick);

        //    //Запускаем таймер.
        //    blinkTimer.Start();
        //}

        ////Отрисовка красного квадрата при наведении на маркер
        //void blinkTimer_Tick(object sender, EventArgs e)
        //{
        //    foreach (GMapMarker m in markersOverlay.Markers)
        //    {
        //        if (m is GMapMarkerImage)
        //        {
        //            GMapMarkerImage marker = m as GMapMarkerImage;
        //            if (marker.OutPen == null)
        //                //Задаем цвет и ширину линии квадрата,
        //                //отображаемого вокруг маркера
        //                //на который наведен курсор.
        //                marker.OutPen = new Pen(Brushes.Red, 2);
        //            else
        //            {
        //                //Убираем красный квадрат.
        //                marker.OutPen.Dispose();
        //                marker.OutPen = null;
        //            }
        //        }
        //    }
        //    //Перерисовываем карту.
        //    gMapControl1.Refresh();
        //}

        //private void buttonStopBlink_Click(object sender, EventArgs e)
        //{
        //    //Останавливаем таймер отображения квадрата.
        //    blinkTimer.Stop();
        //    foreach (GMapMarker m in markersOverlay.Markers)
        //    {
        //        if (m is GMapMarkerImage)
        //        {
        //            GMapMarkerImage marker = m as GMapMarkerImage;
        //            marker.OutPen.Dispose();
        //            marker.OutPen = null;
        //        }
        //    }
        //    //Перерисовываем карту.
        //    gMapControl1.Refresh();
        //}
        private void Form1_Resize(object sender, EventArgs e)
        {
            try
            {
                int DeltaX = 10;
                int DeltaY = 10;
                if (sender is MetroForm)
                {
                    MetroForm FormTmp = sender as MetroForm;
                    var MainPanelWidth = FormTmp.Size.Width - panelMenuGradient.Bounds.Right - panelMenuGradient.Location.X - DeltaX;
                    var MainPanelHeight = panelMenuGradient.Height;
                    Point MainPanelLocation = new Point(panelMenuGradient.Location.X + panelMenuGradient.Width + DeltaX, panelMenuGradient.Location.Y);
                    panelMapWay.Width = MainPanelWidth;
                    panelMapWay.Height = MainPanelHeight;
                    panelMapWay.Location = MainPanelLocation;
                    panelMap.Height = panelMapWay.Height - panelTab.Height - DeltaY;
                    bunifuGridWayPoint.Width = bunifuFlatButton5.Bounds.Left - 5;

                    if (panelSettings != null)
                    {
                        panelSettings.Width = MainPanelWidth;
                        panelSettings.Height = MainPanelHeight;
                        panelSettings.Location = MainPanelLocation;
                    }

                    if (panelManual != null)
                    {
                        panelManual.Width = MainPanelWidth;
                        panelManual.Height = MainPanelHeight;
                        panelManual.Location = MainPanelLocation;
                    }
                }

            }
            catch (Exception Form1_ResizeExc)
            {
                LogError.MessageError(Form1_ResizeExc, null, "События отображения формы", true);
            }
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bunifuGridWayPoint.Columns[e.ColumnIndex] == this.ColDelMarker)
            {
                bunifuGridWayPoint.Rows.RemoveAt(e.RowIndex);

                routes.Points.RemoveAt(e.RowIndex);
                markersOverlay.Markers.RemoveAt(e.RowIndex);
                gMapControl1.UpdateRouteLocalPosition(routes);

                foreach (DataGridViewRow Row in bunifuGridWayPoint.Rows)
                {
                    if (Convert.ToInt32(Row.Cells["ColNoMarker"].Value) > e.RowIndex)
                    {
                        Row.Cells["ColNoMarker"].Value = Row.Index.ToString();
                        markersOverlay.Markers[Row.Index].ToolTipText = Row.Index.ToString();
                    }
                }

            }
        }
        private void bunifuCustomDataGrid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColInMission.Index && e.RowIndex != -1)
            {
                if (bunifuGridWayPoint.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "True")
                {
                    switchChekedMarker(e.RowIndex, true);
                }
                else
                {
                    switchChekedMarker(e.RowIndex, false);
                }
            }
        }
        private void bunifuCustomDataGrid1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColInMission.Index && e.RowIndex != -1)
            {
                bunifuGridWayPoint.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
        private void MenuButton_Click(object sender, EventArgs e)
        {
            if (panelMenuGradient.Size.Width == 75)
            {
                panelMenuGradient.Width = 280;
                Form1_Resize(this, e);
                FlatButtonMapWay.Text = "              Map and Waypoint";
                FlatButtonControl.Text = "              Control";
                FlatButtonSettings.Text = "              Settings";
                // Для анимации боковой панели
                /*
                panelMenuGradient.Visible = false;
                animatorPanelGradient.ShowSync(panelMenuGradient);
                */

            }
            else
            {
                panelMenuGradient.Width = 75;
                Form1_Resize(this, e);
                FlatButtonMapWay.Text = "";
                FlatButtonControl.Text = "";
                FlatButtonSettings.Text = "";
                // Для анимации боковой панели
                /*
                panelMenuGradient.Visible = false;
                animatorPanelGradient.ShowSync(panelMenuGradient);
                 */
                // Сворачиваем меню управления
                FlatButtonManual.Visible = false;
                
            }  
        }
        private void FlatButtonMapWay_Click(object sender, EventArgs e)
        {
            //panelMapWay.Visible = true;
            panelSettings.Visible = false;
            panelManual.Visible = false;

            animatorPanelMapWay.ShowSync(panelMapWay);
        }
        private void FlatButtonSettings_Click(object sender, EventArgs e)
        {
            panelSettings.Visible = true;
            panelMapWay.Visible = false;
            panelManual.Visible = false;
        }
        private void FlatButtonControl_Click(object sender, EventArgs e)
        {
            FlatButtonManual.Visible = !FlatButtonManual.Visible;
            if (FlatButtonManual.Visible && panelMenuGradient.Size.Width == 75)
            {
                MenuButton_Click(sender, e);
            }
        }
        delegate void SetTextBoxCallback(string text, Control TextBox);
        private void SetTextBox(string text, Control TextBox)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (TextBox.InvokeRequired)
            {
                SetTextBoxCallback d = new SetTextBoxCallback(SetTextBox);
                this.Invoke(d, new object[] { text, TextBox });
            }
            else
            {
                TextBox.Text = text; //TextBox.Text + text;
            }
        }
        delegate void RefreshPicBoxCallback(Control TextBox);
        private void RefreshPicBox(Control PicBox)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (PicBox.InvokeRequired)
            {
                RefreshPicBoxCallback d = new RefreshPicBoxCallback(RefreshPicBox);
                this.Invoke(d, new object[] { PicBox });
            }
            else
            {
                PicBox.Refresh();
            }
        }
        private void JoystickRotationX_change(object sender, JoystickButtonPressedEventArgs e)  
        {
            //SetTextBox(e.Value.ToString(), ManualTextboxX);
            JoyRotationX = e.Value;
            //pictureBox1.Refresh();
            RefreshPicBox(pictureBox1);
        }
        private void JoystickRotationY_change(object sender, JoystickButtonPressedEventArgs e)
        {
            //SetTextBox(e.Value.ToString(), ManualTextboxX);
            JoyRotationY = e.Value;
            //pictureBox1.Refresh();
            RefreshPicBox(pictureBox1);
        }
        private void JoystickY_change(object sender, JoystickButtonPressedEventArgs e)
        {
            //SetTextBox(e.Value.ToString(), ManualTextboxX);
            JoyY = e.Value;
            //pictureBox1.Refresh();
            RefreshPicBox(pictureBox2);
        }
        private void JoystickPacket_CA_change(object sender, JoystickPacket_CAEventArgs e)
        {
            Packet_CA packet_CA = e.packet_CA;
            if (UDPClient != null)
            {
                UDPClient.Send(ReceiveData.getBytesToSendPacketCA(packet_CA), ReceiveData.getLenToSendPacketCA(), UDPClientClass.ipEndPoint_Pwm1);
                UDPClient.Send(ReceiveData.getBytesToSendPacketCA(packet_CA), ReceiveData.getLenToSendPacketCA(), UDPClientClass.ipEndPoint_Pwm2);
                UDPClient.Send(ReceiveData.getBytesToSendPacketCA(packet_CA), ReceiveData.getLenToSendPacketCA(), UDPClientClass.ipEndPoint_Pwm3);
                UDPClient.Send(ReceiveData.getBytesToSendPacketCA(packet_CA), ReceiveData.getLenToSendPacketCA(), UDPClientClass.ipEndPoint_Engine);
            }
        }

        string TMP = null;

        void UDPClient_ReceiveData(object sender, UDPClientClass.UdpClientEventArgs e)
        {
            ReceiveData = new ReceiveBytesData(e.Data);
            //SetTextBox(ReceiveData.packet_AB.rms_N3.ToString(), textBox1);
            TMP = Encoding.UTF8.GetString(e.Data);//BitConverter.ToString(e.Data);
            SetTextBox(TMP, textBox1);
            //if (sender is UDPClientClass)
            //{
            //    UDPClientClass client = sender as UDPClientClass;
            //    client.Send(ReceiveData.getBytesToSendPacketCA(), ReceiveData.getLenToSendPacketCA(), e.endPoint);
            //}
        }

        int JoyRotationX = 32767;
        int JoyRotationY = 32767;
        int JoyY = 32767;
        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            int joyX = JoyRotationX;
            int joyY = JoyRotationY;
            PictureBox curentPicBox = sender as PictureBox;
            Graphics graphics = e.Graphics;
            graphics.Clear(Color.Transparent);
            var PicWidth = curentPicBox.Width;
            var PicHeight = curentPicBox.Height;
            var koefScale = 3;
            var ellipseWidth = PicWidth/koefScale;
            var ellipseHeight = PicHeight/koefScale;
            Point pointEllipse = new Point((PicWidth-ellipseWidth)/2,(PicHeight-ellipseHeight)/2);
            Size sizeEllipse = new System.Drawing.Size(ellipseWidth,ellipseHeight);
            Rectangle rectEllipse = new Rectangle(pointEllipse, sizeEllipse);


            // Create a path that consists of a single ellipse.
            GraphicsPath ellipsePath = new GraphicsPath();
            ellipsePath.AddEllipse(rectEllipse);

            // Use the path to construct a brush.
            PathGradientBrush pthGrBrush = new PathGradientBrush(ellipsePath);

            // Set the center point to a location that is not
            // the centroid of the path.
            float centerX = joyX * ((float)ellipseWidth / (float)UInt16.MaxValue);

            float centerY = joyY * ((float)ellipseHeight / (float)UInt16.MaxValue);
            pthGrBrush.CenterPoint = new PointF(pointEllipse.X + centerX, pointEllipse.Y + centerY);

            // Set the color at the center of the path to blue.
            pthGrBrush.CenterColor = Color.FromArgb(255, 0, 0, 255);
            pthGrBrush.FocusScales = new PointF(0.2f, 0.2f);
            // Set the color along the entire boundary 
            // of the path to aqua.
            Color[] colors = { Color.FromArgb(255, 0, 255, 255) };
            pthGrBrush.SurroundColors = colors;


            var topArrX = pointEllipse.X + sizeEllipse.Width / 2;
            var buttomArrY = pointEllipse.Y - pointEllipse.Y / 2;
            var buttomArrXleft = pointEllipse.X;
            var buttomArrXright = pointEllipse.X + sizeEllipse.Width;

            GraphicsPath arrowBottomPath = new GraphicsPath();
            arrowBottomPath.AddPolygon(new Point[] { new Point(topArrX, 0),
                                                     new Point(buttomArrXleft, buttomArrY), 
                                                     new Point(buttomArrXright, buttomArrY) });

            Rectangle rectArr = new Rectangle( pointEllipse.X + (sizeEllipse.Width - (sizeEllipse.Width/2))/2,
                                               pointEllipse.Y - pointEllipse.Y/2,
                                               sizeEllipse.Width/2,
                                               (int)(pointEllipse.Y - pointEllipse.Y / 2 + (sizeEllipse.Height/2*(1-0.866))));

            arrowBottomPath.AddRectangle(rectArr);


            Pen pen = new Pen(Color.FromArgb(255, 0, 255, 255));


            graphics.FillPath(pen.Brush, arrowBottomPath);


            //---------------------
            var topArrBX = pointEllipse.X + sizeEllipse.Width / 2;
            var buttomArrBY = pointEllipse.Y + sizeEllipse.Height + (PicHeight - (pointEllipse.Y + sizeEllipse.Height))/2;
            var buttomArrBXleft = pointEllipse.X;
            var buttomArrBXright = pointEllipse.X + sizeEllipse.Width;
            var heightArrTrgl = PicHeight - buttomArrBY;

            GraphicsPath arrowBBottomPath = new GraphicsPath();
            arrowBBottomPath.AddPolygon(new Point[] { new Point(topArrBX, PicHeight),
                                                     new Point(buttomArrBXleft, buttomArrBY), 
                                                     new Point(buttomArrBXright, buttomArrBY) });

            Rectangle rectArrB = new Rectangle(pointEllipse.X + (sizeEllipse.Width - (sizeEllipse.Width / 2)) / 2,
                                               pointEllipse.Y + sizeEllipse.Height/2+ (int)(sizeEllipse.Height/2 * 0.866),
                                               sizeEllipse.Width / 2,
                                               buttomArrBY - (pointEllipse.Y + sizeEllipse.Height/2+ (int)(sizeEllipse.Height/2 * 0.866)));


            arrowBBottomPath.AddRectangle(rectArrB);
            
            graphics.FillPath(pen.Brush, arrowBBottomPath);
            //---------------------

            //---------------------
            var topArrRX = PicWidth;
            var topArrRY = pointEllipse.Y + sizeEllipse.Height/2;
            var buttomArrRYleft = pointEllipse.Y;
            var buttomArrRYright = pointEllipse.Y + sizeEllipse.Height;
            var buttomArrRX = topArrRX - sizeEllipse.Height/2;
            var heightArrRTrgl = PicHeight - buttomArrBY;

            GraphicsPath arrowRBottomPath = new GraphicsPath();
            arrowRBottomPath.AddPolygon(new Point[] { new Point(topArrRX, topArrRY),
                                                     new Point(buttomArrRX, buttomArrRYleft), 
                                                     new Point(buttomArrRX, buttomArrRYright) });

            Rectangle rectArrR = new Rectangle(pointEllipse.X + sizeEllipse.Width / 2 + (int)(sizeEllipse.Width / 2 * 0.866),
                                               pointEllipse.Y + (sizeEllipse.Height - (sizeEllipse.Height / 2 ))/2,
                                               buttomArrRX - (pointEllipse.X + sizeEllipse.Width / 2 + (int)(sizeEllipse.Width / 2 * 0.866)),
                                               sizeEllipse.Height/2);
                                               


            arrowRBottomPath.AddRectangle(rectArrR);
            
            graphics.FillPath(pen.Brush, arrowRBottomPath);
            //---------------------

            var topArrLX = 0;
            var topArrLY = pointEllipse.Y + sizeEllipse.Height / 2;
            var buttomArrLYleft = pointEllipse.Y + sizeEllipse.Height;
            var buttomArrLYright = pointEllipse.Y;
            var buttomArrLX = sizeEllipse.Height / 2;

            GraphicsPath arrowLBottomPath = new GraphicsPath();
            arrowLBottomPath.AddPolygon(new Point[] { new Point(topArrLX, topArrLY),
                                                     new Point(buttomArrLX, buttomArrLYleft), 
                                                     new Point(buttomArrLX, buttomArrLYright) });

            Rectangle rectArrL = new Rectangle(buttomArrLX,
                                               pointEllipse.Y + (sizeEllipse.Height - (sizeEllipse.Height / 2)) / 2,
                                               (int)(pointEllipse.X - pointEllipse.X / 2 + (sizeEllipse.Width / 2 * (1 - 0.866))),
                                               sizeEllipse.Height / 2);

            arrowLBottomPath.AddRectangle(rectArrL);
            graphics.FillPath(pen.Brush, arrowLBottomPath);

           
            if (joyY < UInt16.MaxValue / 2)
            {
                try
                {
                    GraphicsPath arrayMarkPath = new GraphicsPath();
                    Rectangle rectMarkArray;
                    Point[] poligonMarkArray;

                    var heightArray = buttomArrY + rectArrB.Height;
                    float Height = ((UInt16.MaxValue / 2) - joyY) * ((float)heightArray / (float)(UInt16.MaxValue / 2));
                    if (Height > rectArr.Height)
                    {
                        rectMarkArray = new Rectangle(rectArr.X,
                                                      rectArr.Y,
                                                      rectArr.Width,
                                                      rectArr.Height);

                        float poliHeight = buttomArrY - (Height - rectArr.Height);
                        float k = (sizeEllipse.Width / 2) * (float)poliHeight / (ellipseHeight / 2);

                        poligonMarkArray = new Point[] { new Point(topArrX + (int)k, (int)poliHeight),
                                                         new Point(topArrX - (int)k, (int)poliHeight),
                                                         new Point(buttomArrXleft, buttomArrY), 
                                                         new Point(buttomArrXright, buttomArrY) };
                        arrayMarkPath.AddRectangle(rectMarkArray);
                        arrayMarkPath.AddPolygon(poligonMarkArray);
                    }
                    else
                    {
                        rectMarkArray = new Rectangle(rectArr.X,
                                                      rectArr.Y + (rectArr.Height - (int)Height),
                                                      rectArr.Width,
                                                      (int)Height);
                        arrayMarkPath.AddRectangle(rectMarkArray);
                    }
                    if (Height>1)
                    {
                        var ttt = (heightArray - (int)Height) < 1 ? 1 : heightArray - (int)Height;

                        LinearGradientBrush linGrBrush = new LinearGradientBrush(
                                    new Point(rectArr.X, ttt),
                                    new Point(rectArr.X, rectArr.Y + rectArr.Height+1),
                                    Color.Blue,
                                    Color.FromArgb(255, 0, 255, 255));

                        graphics.FillPath(linGrBrush, arrayMarkPath);
                    }   
                }
                catch (Exception e6)
                {
                    string tmp2 = e6.Message;
                    throw;
                }
                
            }
            else
            {
                try
                {
                    GraphicsPath arrayMarkPath = new GraphicsPath();
                    Rectangle rectMarkArray;
                    Point[] poligonMarkArray;

                    var heightArray = heightArrTrgl + rectArrB.Height;
                    float Height = (joyY - (UInt16.MaxValue / 2)) * ((float)heightArray / (float)(UInt16.MaxValue / 2));
                    if (Height > rectArrB.Height)
                    {
                        rectMarkArray = new Rectangle(rectArrB.X,
                                                      rectArrB.Y,
                                                      rectArrB.Width,
                                                      rectArrB.Height);

                        float poliHeight = heightArrTrgl - (Height - rectArrB.Height);
                        float k = (sizeEllipse.Width / 2) * (float)poliHeight / (ellipseHeight / 2);

                        poligonMarkArray = new Point[] { new Point(topArrBX + (int)k, PicHeight - (int)poliHeight),
                                                         new Point(topArrBX - (int)k, PicHeight - (int)poliHeight),
                                                         new Point(buttomArrBXleft, buttomArrBY), 
                                                         new Point(buttomArrBXright, buttomArrBY) };
                        arrayMarkPath.AddRectangle(rectMarkArray);
                        arrayMarkPath.AddPolygon(poligonMarkArray);
                    }
                    else
                    {
                        rectMarkArray = new Rectangle(rectArrB.X,
                                                      rectArrB.Y,
                                                      rectArrB.Width,
                                                      (int)Height);
                        arrayMarkPath.AddRectangle(rectMarkArray);
                    }

                    var ttt = (heightArray - (int)Height) < 0 ? 0 : heightArray - (int)Height;
                    if (Height>1)
                    {
                        LinearGradientBrush linGrBrush = new LinearGradientBrush(
                                new Point(rectArrB.X, rectArrB.Y + (int)Height),
                                new Point(rectArrB.X, rectArrB.Y),
                                Color.Blue,
                                Color.FromArgb(255, 0, 255, 255));

                        graphics.FillPath(linGrBrush, arrayMarkPath);
                    }
                    
                }
                catch (Exception e6)
                {
                    string tmp2 = e6.Message;
                    throw;
                }
            }

            if (joyX < UInt16.MaxValue / 2)
            {
                try
                {
                    GraphicsPath arrayMarkPath = new GraphicsPath();
                    Rectangle rectMarkArray;
                    Point[] poligonMarkArray;

                    var heightArray = buttomArrLX + rectArrL.Width;
                    float Height = ((UInt16.MaxValue / 2) - joyX) * ((float)heightArray / (float)(UInt16.MaxValue / 2));
                    if (Height > rectArrL.Width)
                    {
                        rectMarkArray = new Rectangle(rectArrL.X,
                                                      rectArrL.Y,
                                                      rectArrL.Width,
                                                      rectArrL.Height);

                        float poliHeight = buttomArrLX - (Height - rectArr.Width);
                        float k = (sizeEllipse.Height / 2) * (float)poliHeight / (ellipseWidth / 2);

                        poligonMarkArray = new Point[] { new Point((int)poliHeight, topArrLY - (int)k),
                                                         new Point((int)poliHeight, topArrLY + (int)k),
                                                         new Point(buttomArrLX, buttomArrLYleft), 
                                                         new Point(buttomArrLX, buttomArrLYright) };
                        arrayMarkPath.AddRectangle(rectMarkArray);
                        arrayMarkPath.AddPolygon(poligonMarkArray);
                    }
                    else
                    {
                        rectMarkArray = new Rectangle(rectArrL.X + (rectArrL.Width - (int)Height),
                                                      rectArrL.Y ,
                                                      (int)Height,
                                                      rectArrL.Height);
                        arrayMarkPath.AddRectangle(rectMarkArray);
                    }
                    if (Height > 1)
                    {
                        var ttt = (heightArray - (int)Height) < 1 ? 1 : heightArray - (int)Height;

                        LinearGradientBrush linGrBrush = new LinearGradientBrush(
                                    new Point(ttt, rectArrL.Y),
                                    new Point(rectArrL.X + rectArrL.Width + 1, rectArrL.Y),
                                    Color.Blue,
                                    Color.FromArgb(255, 0, 255, 255));

                        graphics.FillPath(linGrBrush, arrayMarkPath);
                    }
                }
                catch (Exception e6)
                {
                    string tmp2 = e6.Message;
                    throw;
                }

            }
            else
            {
                try
                {
                    GraphicsPath arrayMarkPath = new GraphicsPath();
                    Rectangle rectMarkArray;
                    Point[] poligonMarkArray;

                    var heightArray = heightArrRTrgl + rectArrR.Width;
                    float Height = (joyX - (UInt16.MaxValue / 2)) * ((float)heightArray / (float)(UInt16.MaxValue / 2));
                    if (Height > rectArrR.Width)
                    {
                        rectMarkArray = new Rectangle(rectArrR.X,
                                                      rectArrR.Y,
                                                      rectArrR.Width,
                                                      rectArrR.Height);

                        float poliHeight = heightArrRTrgl - (Height - rectArrR.Width);
                        float k = (sizeEllipse.Height / 2) * (float)poliHeight / (ellipseWidth / 2);

                        poligonMarkArray = new Point[] { new Point(PicWidth - (int)poliHeight, topArrRY + (int)k),
                                                         new Point(PicWidth - (int)poliHeight, topArrRY - (int)k),
                                                         new Point(buttomArrRX, buttomArrRYleft), 
                                                         new Point(buttomArrRX, buttomArrRYright) };
                        arrayMarkPath.AddRectangle(rectMarkArray);
                        arrayMarkPath.AddPolygon(poligonMarkArray);
                    }
                    else
                    {
                        rectMarkArray = new Rectangle(rectArrR.X,
                                                      rectArrR.Y,
                                                      (int)Height,
                                                      rectArrR.Height);
                        arrayMarkPath.AddRectangle(rectMarkArray);
                    }

                    var ttt = (heightArray - (int)Height) < 0 ? 0 : heightArray - (int)Height;
                    if (Height > 1)
                    {
                        LinearGradientBrush linGrBrush = new LinearGradientBrush(
                                new Point(rectArrR.X + (int)Height, rectArrR.Y),
                                new Point(rectArrR.X, rectArrR.Y),
                                Color.Blue,
                                Color.FromArgb(255, 0, 255, 255));

                        graphics.FillPath(linGrBrush, arrayMarkPath);
                    }

                }
                catch (Exception e6)
                {
                    string tmp2 = e6.Message;
                    throw;
                }
            }

            graphics.FillEllipse(pthGrBrush, rectEllipse);
        }

        private void pictureBox2_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            int joyX = 32767;
            int joyY = JoyY;
            PictureBox curentPicBox = sender as PictureBox;
            Graphics graphics = e.Graphics;
            graphics.Clear(Color.Transparent);
            var PicWidth = curentPicBox.Width;
            var PicHeight = curentPicBox.Height;
            var koefScale = 3;
            var ellipseWidth = PicWidth / koefScale;
            var ellipseHeight = PicHeight / koefScale;
            Point pointEllipse = new Point((PicWidth - ellipseWidth) / 2, (PicHeight - ellipseHeight) / 2);
            Size sizeEllipse = new System.Drawing.Size(ellipseWidth, ellipseHeight);
            Rectangle rectEllipse = new Rectangle(pointEllipse, sizeEllipse);


            // Create a path that consists of a single ellipse.
            GraphicsPath ellipsePath = new GraphicsPath();
            ellipsePath.AddEllipse(rectEllipse);

            // Use the path to construct a brush.
            PathGradientBrush pthGrBrush = new PathGradientBrush(ellipsePath);

            // Set the center point to a location that is not
            // the centroid of the path.
            float centerX = joyX * ((float)ellipseWidth / (float)UInt16.MaxValue);

            float centerY = joyY * ((float)ellipseHeight / (float)UInt16.MaxValue);
            pthGrBrush.CenterPoint = new PointF(pointEllipse.X + centerX, pointEllipse.Y + centerY);

            // Set the color at the center of the path to blue.
            pthGrBrush.CenterColor = Color.FromArgb(255, 0, 0, 255);
            pthGrBrush.FocusScales = new PointF(0.2f, 0.2f);
            // Set the color along the entire boundary 
            // of the path to aqua.
            Color[] colors = { Color.FromArgb(255, 0, 255, 255) };
            pthGrBrush.SurroundColors = colors;


            var topArrX = pointEllipse.X + sizeEllipse.Width / 2;
            var buttomArrY = pointEllipse.Y - pointEllipse.Y / 2;
            var buttomArrXleft = pointEllipse.X;
            var buttomArrXright = pointEllipse.X + sizeEllipse.Width;

            GraphicsPath arrowBottomPath = new GraphicsPath();
            arrowBottomPath.AddPolygon(new Point[] { new Point(topArrX, 0),
                                                     new Point(buttomArrXleft, buttomArrY), 
                                                     new Point(buttomArrXright, buttomArrY) });

            Rectangle rectArr = new Rectangle(pointEllipse.X + (sizeEllipse.Width - (sizeEllipse.Width / 2)) / 2,
                                               pointEllipse.Y - pointEllipse.Y / 2,
                                               sizeEllipse.Width / 2,
                                               (int)(pointEllipse.Y - pointEllipse.Y / 2 + (sizeEllipse.Height / 2 * (1 - 0.866))));

            arrowBottomPath.AddRectangle(rectArr);


            Pen pen = new Pen(Color.FromArgb(255, 0, 255, 255));


            graphics.FillPath(pen.Brush, arrowBottomPath);


            //---------------------
            var topArrBX = pointEllipse.X + sizeEllipse.Width / 2;
            var buttomArrBY = pointEllipse.Y + sizeEllipse.Height + (PicHeight - (pointEllipse.Y + sizeEllipse.Height)) / 2;
            var buttomArrBXleft = pointEllipse.X;
            var buttomArrBXright = pointEllipse.X + sizeEllipse.Width;
            var heightArrTrgl = PicHeight - buttomArrBY;

            GraphicsPath arrowBBottomPath = new GraphicsPath();
            arrowBBottomPath.AddPolygon(new Point[] { new Point(topArrBX, PicHeight),
                                                     new Point(buttomArrBXleft, buttomArrBY), 
                                                     new Point(buttomArrBXright, buttomArrBY) });

            Rectangle rectArrB = new Rectangle(pointEllipse.X + (sizeEllipse.Width - (sizeEllipse.Width / 2)) / 2,
                                               pointEllipse.Y + sizeEllipse.Height / 2 + (int)(sizeEllipse.Height / 2 * 0.866),
                                               sizeEllipse.Width / 2,
                                               buttomArrBY - (pointEllipse.Y + sizeEllipse.Height / 2 + (int)(sizeEllipse.Height / 2 * 0.866)));


            arrowBBottomPath.AddRectangle(rectArrB);

            graphics.FillPath(pen.Brush, arrowBBottomPath);
            //---------------------

            //---------------------
            var topArrRX = PicWidth;
            var topArrRY = pointEllipse.Y + sizeEllipse.Height / 2;
            var buttomArrRYleft = pointEllipse.Y;
            var buttomArrRYright = pointEllipse.Y + sizeEllipse.Height;
            var buttomArrRX = topArrRX - sizeEllipse.Height / 2;
            var heightArrRTrgl = PicHeight - buttomArrBY;

            GraphicsPath arrowRBottomPath = new GraphicsPath();
            arrowRBottomPath.AddPolygon(new Point[] { new Point(topArrRX, topArrRY),
                                                     new Point(buttomArrRX, buttomArrRYleft), 
                                                     new Point(buttomArrRX, buttomArrRYright) });

            Rectangle rectArrR = new Rectangle(pointEllipse.X + sizeEllipse.Width / 2 + (int)(sizeEllipse.Width / 2 * 0.866),
                                               pointEllipse.Y + (sizeEllipse.Height - (sizeEllipse.Height / 2)) / 2,
                                               buttomArrRX - (pointEllipse.X + sizeEllipse.Width / 2 + (int)(sizeEllipse.Width / 2 * 0.866)),
                                               sizeEllipse.Height / 2);



            arrowRBottomPath.AddRectangle(rectArrR);

            graphics.FillPath(pen.Brush, arrowRBottomPath);
            //---------------------

            var topArrLX = 0;
            var topArrLY = pointEllipse.Y + sizeEllipse.Height / 2;
            var buttomArrLYleft = pointEllipse.Y + sizeEllipse.Height;
            var buttomArrLYright = pointEllipse.Y;
            var buttomArrLX = sizeEllipse.Height / 2;

            GraphicsPath arrowLBottomPath = new GraphicsPath();
            arrowLBottomPath.AddPolygon(new Point[] { new Point(topArrLX, topArrLY),
                                                     new Point(buttomArrLX, buttomArrLYleft), 
                                                     new Point(buttomArrLX, buttomArrLYright) });

            Rectangle rectArrL = new Rectangle(buttomArrLX,
                                               pointEllipse.Y + (sizeEllipse.Height - (sizeEllipse.Height / 2)) / 2,
                                               (int)(pointEllipse.X - pointEllipse.X / 2 + (sizeEllipse.Width / 2 * (1 - 0.866))),
                                               sizeEllipse.Height / 2);

            arrowLBottomPath.AddRectangle(rectArrL);
            graphics.FillPath(pen.Brush, arrowLBottomPath);


            if (joyY < UInt16.MaxValue / 2)
            {
                try
                {
                    GraphicsPath arrayMarkPath = new GraphicsPath();
                    Rectangle rectMarkArray;
                    Point[] poligonMarkArray;

                    var heightArray = buttomArrY + rectArrB.Height;
                    float Height = ((UInt16.MaxValue / 2) - joyY) * ((float)heightArray / (float)(UInt16.MaxValue / 2));
                    if (Height > rectArr.Height)
                    {
                        rectMarkArray = new Rectangle(rectArr.X,
                                                      rectArr.Y,
                                                      rectArr.Width,
                                                      rectArr.Height);

                        float poliHeight = buttomArrY - (Height - rectArr.Height);
                        float k = (sizeEllipse.Width / 2) * (float)poliHeight / (ellipseHeight / 2);

                        poligonMarkArray = new Point[] { new Point(topArrX + (int)k, (int)poliHeight),
                                                         new Point(topArrX - (int)k, (int)poliHeight),
                                                         new Point(buttomArrXleft, buttomArrY), 
                                                         new Point(buttomArrXright, buttomArrY) };
                        arrayMarkPath.AddRectangle(rectMarkArray);
                        arrayMarkPath.AddPolygon(poligonMarkArray);
                    }
                    else
                    {
                        rectMarkArray = new Rectangle(rectArr.X,
                                                      rectArr.Y + (rectArr.Height - (int)Height),
                                                      rectArr.Width,
                                                      (int)Height);
                        arrayMarkPath.AddRectangle(rectMarkArray);
                    }
                    if (Height > 1)
                    {
                        var ttt = (heightArray - (int)Height) < 1 ? 1 : heightArray - (int)Height;

                        LinearGradientBrush linGrBrush = new LinearGradientBrush(
                                    new Point(rectArr.X, ttt),
                                    new Point(rectArr.X, rectArr.Y + rectArr.Height + 1),
                                    Color.Blue,
                                    Color.FromArgb(255, 0, 255, 255));

                        graphics.FillPath(linGrBrush, arrayMarkPath);
                    }
                }
                catch (Exception e6)
                {
                    string tmp2 = e6.Message;
                    throw;
                }

            }
            else
            {
                try
                {
                    GraphicsPath arrayMarkPath = new GraphicsPath();
                    Rectangle rectMarkArray;
                    Point[] poligonMarkArray;

                    var heightArray = heightArrTrgl + rectArrB.Height;
                    float Height = (joyY - (UInt16.MaxValue / 2)) * ((float)heightArray / (float)(UInt16.MaxValue / 2));
                    if (Height > rectArrB.Height)
                    {
                        rectMarkArray = new Rectangle(rectArrB.X,
                                                      rectArrB.Y,
                                                      rectArrB.Width,
                                                      rectArrB.Height);

                        float poliHeight = heightArrTrgl - (Height - rectArrB.Height);
                        float k = (sizeEllipse.Width / 2) * (float)poliHeight / (ellipseHeight / 2);

                        poligonMarkArray = new Point[] { new Point(topArrBX + (int)k, PicHeight - (int)poliHeight),
                                                         new Point(topArrBX - (int)k, PicHeight - (int)poliHeight),
                                                         new Point(buttomArrBXleft, buttomArrBY), 
                                                         new Point(buttomArrBXright, buttomArrBY) };
                        arrayMarkPath.AddRectangle(rectMarkArray);
                        arrayMarkPath.AddPolygon(poligonMarkArray);
                    }
                    else
                    {
                        rectMarkArray = new Rectangle(rectArrB.X,
                                                      rectArrB.Y,
                                                      rectArrB.Width,
                                                      (int)Height);
                        arrayMarkPath.AddRectangle(rectMarkArray);
                    }

                    var ttt = (heightArray - (int)Height) < 0 ? 0 : heightArray - (int)Height;
                    if (Height > 1)
                    {
                        LinearGradientBrush linGrBrush = new LinearGradientBrush(
                                new Point(rectArrB.X, rectArrB.Y + (int)Height),
                                new Point(rectArrB.X, rectArrB.Y),
                                Color.Blue,
                                Color.FromArgb(255, 0, 255, 255));

                        graphics.FillPath(linGrBrush, arrayMarkPath);
                    }

                }
                catch (Exception e6)
                {
                    string tmp2 = e6.Message;
                    throw;
                }
            }

            if (joyX < UInt16.MaxValue / 2)
            {
                try
                {
                    GraphicsPath arrayMarkPath = new GraphicsPath();
                    Rectangle rectMarkArray;
                    Point[] poligonMarkArray;

                    var heightArray = buttomArrLX + rectArrL.Width;
                    float Height = ((UInt16.MaxValue / 2) - joyX) * ((float)heightArray / (float)(UInt16.MaxValue / 2));
                    if (Height > rectArrL.Width)
                    {
                        rectMarkArray = new Rectangle(rectArrL.X,
                                                      rectArrL.Y,
                                                      rectArrL.Width,
                                                      rectArrL.Height);

                        float poliHeight = buttomArrLX - (Height - rectArr.Width);
                        float k = (sizeEllipse.Height / 2) * (float)poliHeight / (ellipseWidth / 2);

                        poligonMarkArray = new Point[] { new Point((int)poliHeight, topArrLY - (int)k),
                                                         new Point((int)poliHeight, topArrLY + (int)k),
                                                         new Point(buttomArrLX, buttomArrLYleft), 
                                                         new Point(buttomArrLX, buttomArrLYright) };
                        arrayMarkPath.AddRectangle(rectMarkArray);
                        arrayMarkPath.AddPolygon(poligonMarkArray);
                    }
                    else
                    {
                        rectMarkArray = new Rectangle(rectArrL.X + (rectArrL.Width - (int)Height),
                                                      rectArrL.Y,
                                                      (int)Height,
                                                      rectArrL.Height);
                        arrayMarkPath.AddRectangle(rectMarkArray);
                    }
                    if (Height > 1)
                    {
                        var ttt = (heightArray - (int)Height) < 1 ? 1 : heightArray - (int)Height;

                        LinearGradientBrush linGrBrush = new LinearGradientBrush(
                                    new Point(ttt, rectArrL.Y),
                                    new Point(rectArrL.X + rectArrL.Width + 1, rectArrL.Y),
                                    Color.Blue,
                                    Color.FromArgb(255, 0, 255, 255));

                        graphics.FillPath(linGrBrush, arrayMarkPath);
                    }
                }
                catch (Exception e6)
                {
                    string tmp2 = e6.Message;
                    throw;
                }

            }
            else
            {
                try
                {
                    GraphicsPath arrayMarkPath = new GraphicsPath();
                    Rectangle rectMarkArray;
                    Point[] poligonMarkArray;

                    var heightArray = heightArrRTrgl + rectArrR.Width;
                    float Height = (joyX - (UInt16.MaxValue / 2)) * ((float)heightArray / (float)(UInt16.MaxValue / 2));
                    if (Height > rectArrR.Width)
                    {
                        rectMarkArray = new Rectangle(rectArrR.X,
                                                      rectArrR.Y,
                                                      rectArrR.Width,
                                                      rectArrR.Height);

                        float poliHeight = heightArrRTrgl - (Height - rectArrR.Width);
                        float k = (sizeEllipse.Height / 2) * (float)poliHeight / (ellipseWidth / 2);

                        poligonMarkArray = new Point[] { new Point(PicWidth - (int)poliHeight, topArrRY + (int)k),
                                                         new Point(PicWidth - (int)poliHeight, topArrRY - (int)k),
                                                         new Point(buttomArrRX, buttomArrRYleft), 
                                                         new Point(buttomArrRX, buttomArrRYright) };
                        arrayMarkPath.AddRectangle(rectMarkArray);
                        arrayMarkPath.AddPolygon(poligonMarkArray);
                    }
                    else
                    {
                        rectMarkArray = new Rectangle(rectArrR.X,
                                                      rectArrR.Y,
                                                      (int)Height,
                                                      rectArrR.Height);
                        arrayMarkPath.AddRectangle(rectMarkArray);
                    }

                    var ttt = (heightArray - (int)Height) < 0 ? 0 : heightArray - (int)Height;
                    if (Height > 1)
                    {
                        LinearGradientBrush linGrBrush = new LinearGradientBrush(
                                new Point(rectArrR.X + (int)Height, rectArrR.Y),
                                new Point(rectArrR.X, rectArrR.Y),
                                Color.Blue,
                                Color.FromArgb(255, 0, 255, 255));

                        graphics.FillPath(linGrBrush, arrayMarkPath);
                    }

                }
                catch (Exception e6)
                {
                    string tmp2 = e6.Message;
                    throw;
                }
            }

            graphics.FillEllipse(pthGrBrush, rectEllipse);
        }

        private void pictureBox3_Paint(object sender, System.Windows.Forms.PaintEventArgs ev)
        {
            double Kur = 0.1;

            PictureBox curentPicBox = sender as PictureBox;
            Graphics graphics = ev.Graphics;
            graphics.Clear(Color.White);

            int Width = curentPicBox.Width;
            int Height = curentPicBox.Height;

            Pen pen = new Pen(Color.Green);
            pen.Width = 3;
            graphics.DrawRectangle(pen, 0, 0, 200, 200);
            pen.Color = Color.Black;
            graphics.DrawEllipse(pen, 3, 3, 194, 194);
            pen.Color = Color.SkyBlue;
            graphics.DrawEllipse(pen, 8, 8, 184, 184);

            pen.Color = Color.Black;
            pen.Width = 8;
            
            int s, d;
            int e = 0;
            
            double angle = Kur - 1.5707963267971534928089599443094;
            while (e < 80)
            {
                s = (int)((40 - e) * Math.Cos(angle) + Width / 2);
                d = (int)((40 - e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 4, d + 4);
                e = e + 1;
            }

            pen.Width = 6;
            e = 0;
            while (e < 20)
            {
                s = (int)((e) * Math.Cos(angle + 2.5) + (40) * Math.Cos(angle) + Width / 2);
                d = (int)((e) * Math.Sin(angle + 2.5) + (40) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 4, d + 4);

                s = (int)((e) * Math.Cos(angle - 2.5) + (40) * Math.Cos(angle) + Width / 2);
                d = (int)((e) * Math.Sin(angle - 2.5) + (40) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 4, d + 4);

                e = e + 1;
            }

            pen.Color = Color.Black;
            pen.Width = 2;
            angle = 0.0;
            while (angle < 6.2831853070)
            {
                e = 0;
                while (e < 10)
                {

                    s = (int)((60 + e) * Math.Cos(angle) + Width / 2);
                    d = (int)((60 + e) * Math.Sin(angle) + Height / 2);
                    graphics.DrawLine(pen, s, d, s + 2, d + 2);
                    e = e + 1;
                }
                angle = angle + 0.2617993877995255821348266573849;
            }

            int k = 0;
            angle = 0.0;
            while (angle < 6.2831853070)
            {
                if (k % 3 != 0)
                {
                    e = 0;
                    while (e < 5)
                    {

                        s = (int)((60 + e) * Math.Cos(angle) + Width / 2);
                        d = (int)((60 + e) * Math.Sin(angle) + Height / 2);
                        graphics.DrawLine(pen, s, d, s + 2, d + 2);
                        e = e + 1;
                    }
                }
                k = k + 1;
                angle = angle + 0.08726646259984186071160888579497;
            }

            //graphics.RotateTransform(30);
            Font font = new Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            graphics.DrawString("0", font, pen.Brush, Width / 2 - 5, Height / 2 - 90);

            graphics.DrawString("180", font, pen.Brush, Width / 2 - 15, Height / 2 + 70);

            graphics.TranslateTransform(Width / 2 + 90, Height / 2 - 10);
            graphics.RotateTransform(90);
            graphics.TranslateTransform(-(Width / 2 + 90), -(Height / 2 - 10));
            graphics.DrawString("90", font, pen.Brush, Width / 2 + 90, Height / 2 - 10);

            graphics.ResetTransform();
            graphics.TranslateTransform(Width / 2 - 90, Height / 2 + 15);
            graphics.RotateTransform(-90);
            graphics.TranslateTransform(-(Width / 2 - 90), -(Height / 2 + 15));
            graphics.DrawString("270", font, pen.Brush, Width / 2 - 90, Height / 2 + 15);

            graphics.ResetTransform();
            graphics.TranslateTransform(Width / 2 + 38, Height / 2 + 63);
            graphics.RotateTransform(-45);
            graphics.TranslateTransform(-(Width / 2 + 38), -(Height / 2 + 63));
            graphics.DrawString("135", font, pen.Brush, Width / 2 + 38, Height / 2 + 63);

            graphics.ResetTransform();
            graphics.TranslateTransform(Width / 2 - 59, Height / 2 + 40);
            graphics.RotateTransform(45);
            graphics.TranslateTransform(-(Width / 2 - 59), -(Height / 2 + 40));
            graphics.DrawString("225", font, pen.Brush, Width / 2 - 59, Height / 2 + 40);

            graphics.ResetTransform();
            graphics.TranslateTransform(Width / 2 - 73, Height / 2 - 50);
            graphics.RotateTransform(-45);
            graphics.TranslateTransform(-(Width / 2 - 73), -(Height / 2 - 50));
            graphics.DrawString("315", font, pen.Brush, Width / 2 - 73, Height / 2 - 50);

            graphics.ResetTransform();
            graphics.TranslateTransform(Width / 2 + 55, Height / 2 - 70);
            graphics.RotateTransform(45);
            graphics.TranslateTransform(-(Width / 2 + 55), -(Height / 2 - 70));
            graphics.DrawString("45", font, pen.Brush, Width / 2 + 55, Height / 2 - 70);
        }

        private void pictureBox4_Paint(object sender, System.Windows.Forms.PaintEventArgs ev)
        {
            int Tang = 0; 
            double Diff = 0.5;
            int Kur = 10;

            PictureBox curentPicBox = sender as PictureBox;
            Graphics graphics = ev.Graphics;
            graphics.Clear(Color.White);

            int Width = curentPicBox.Width;
            int Height = curentPicBox.Height;
            Pen pen = new Pen(Color.Transparent);

            //background
            int x = 0;
            int y = Tang * 4;

            pen.Color = Color.SkyBlue;
            while (x < ((Height) / 2 - y))
            {
                graphics.DrawLine(pen, 0, x, Width, x);
                x = x + 1;
            }

            pen.Color = Color.White;
            graphics.DrawLine(pen, 0, x, Width, x);
            x = x + 1;
            pen.Color = Color.Olive;
            while (x < Height)
            {
                graphics.DrawLine(pen, 0, x, Width, x);
                x = x + 1;
            }

            //---------------------------------------------------------------------------
            //scale of pitch
            int tmp1 = Height / 2 - y;
            int tmp2 = Width / 2;

            pen.Color = Color.White;
            graphics.DrawLine(pen, tmp2 - 20, tmp1 - 20, tmp2 + 20, tmp1 - 20);
            graphics.DrawLine(pen, tmp2 - 20, tmp1 + 20, tmp2 + 20, tmp1 + 20);
            graphics.DrawLine(pen, tmp2 - 20, tmp1 - 60, tmp2 + 20, tmp1 - 60);
            graphics.DrawLine(pen, tmp2 - 20, tmp1 + 60, tmp2 + 20, tmp1 + 60);
            graphics.DrawLine(pen, tmp2 - 40, tmp1 - 40, tmp2 + 40, tmp1 - 40);
            graphics.DrawLine(pen, tmp2 - 40, tmp1 - 80, tmp2 + 40, tmp1 - 80);
            graphics.DrawLine(pen, tmp2 - 40, tmp1 + 40, tmp2 + 40, tmp1 + 40);
            graphics.DrawLine(pen, tmp2 - 40, tmp1 + 80, tmp2 + 40, tmp1 + 80);

            Font font = new Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            pen.Color = Color.White;

            tmp1 -= 2;
            tmp2 -= 4;

            graphics.DrawString("10", font, pen.Brush, tmp2 + 47, tmp1 - 50);
            graphics.DrawString("10", font, pen.Brush, tmp2 - 65, tmp1 - 50);
            graphics.DrawString("20", font, pen.Brush, tmp2 + 47, tmp1 - 90);
            graphics.DrawString("20", font, pen.Brush, tmp2 - 65, tmp1 - 90);     
            graphics.DrawString("10", font, pen.Brush, tmp2 + 47, tmp1 + 30);
            graphics.DrawString("10", font, pen.Brush, tmp2 - 65, tmp1 + 30);
            graphics.DrawString("20", font, pen.Brush, tmp2 + 47, tmp1 + 70);
            graphics.DrawString("20", font, pen.Brush, tmp2 - 65, tmp1 + 70);
            //---------------------------------------------------------------------------
            // rotating frame
            pen.Color = Color.Yellow;
            pen.Width = 3;

            int s, d;
            int e = 0;
            double angle = -Diff;
            while (e < 50)
            {

                s = (int)((30 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((30 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                s = (int)((30 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((30 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                e = e + 1;
            }

            //graphics.DrawArc(pen, Width / 2 - 30, Height / 2 - 30, Width / 2 + 30, Height / 2 + 30, (int)((30) * Math.Cos(angle - 3.1415926535) + Width / 2), (int)((30) * Math.Sin(angle - 3.1415926535) + Height / 2));//, (30) * Math.Cos(angle) + Width / 2, (30) * Math.Sin(angle) + Height / 2);


            pen.Width = 6;
            graphics.DrawLine(pen, Width / 2, Height / 2, Width / 2, Height / 2);
            //---------------------------------------------------------------------------
            //Dial rotating frame

            e = 0;
            pen.Color = Color.White;
            pen.Width = 1;

            while (e < 10)
            {
                angle = 0.0;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                s = (int)((100 + e) * Math.Cos(angle + 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle + 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = 0.255;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -0.255;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = 0.51;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -0.51;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = 0.765;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -0.765;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = 1.02;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -1.02;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                e = e + 1;
            }
            e = 0;
            while (e < 5)
            {
                angle = 0.085;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -0.085;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = 0.17;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -0.17;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = 0.34;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -0.34;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = 0.425;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -0.425;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = 0.595;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -0.595;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = 0.68;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -0.68;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = 0.85;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -0.85;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = 0.935;
                s = (int)((100 + e) * Math.Cos(angle) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                angle = -0.935;
                s = (int)((100 + e) * Math.Cos(angle - 3.1415926535) + Width / 2);
                d = (int)((100 + e) * Math.Sin(angle - 3.1415926535) + Height / 2);
                graphics.DrawLine(pen, s, d, s + 1, d + 1);
                e = e + 1;
            }
            //---------------------------------------------------------------------------
            //String of Course

            int b = 0;
            if (Kur < 0)
            {
                Kur = Kur + 360;

            }

            pen.Color = Color.Black;
            while (b < 50)
            {
                graphics.DrawLine(pen, 0, b, Width, b);
                b = b + 1;
            }

            pen.Color = Color.White;
            font = new Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));

            graphics.DrawLine(pen, (int)(-(720 - Width / 2) + 720 - Kur * 4), 10, (int)(Width + (720 - Width / 2) + 720 - Kur * 4), 10);
            graphics.DrawLine(pen, (int)(-(720 - Width / 2) + 530 - Kur * 4), 10, (int)(-(720 - Width / 2) + 720 - Kur * 4), 10);
            graphics.DrawLine(pen, (int)(Width + (720 - Width / 2) + 720 - Kur * 4), 10, (int)(Width + (720 - Width / 2) + 910 - Kur * 4), 10);

            int f = 0;


            //while (f < 1444)
            //{
            //    bitmap->Canvas->MoveTo(-(720 - bitmap->Width / 2) + 720 - Kur * 4 + f, 10);
            //    bitmap->Canvas->LineTo(-(720 - bitmap->Width / 2) + 720 - Kur * 4 + f, 22);

            //    bitmap->Canvas->MoveTo(-(720 - bitmap->Width / 2) + 720 - Kur * 4 + f + 20, 10);
            //    bitmap->Canvas->LineTo(-(720 - bitmap->Width / 2) + 720 - Kur * 4 + f + 20, 16);

            //    bitmap->Canvas->Brush->Color = clBlack;
            //    bitmap->Canvas->Font->Color = clWhite;
            //    if (f == 0 || f == 1440)
            //    {
            //        bitmap->Canvas->TextOutW(-(720 - bitmap->Width / 2) + 720 - Kur * 4 + f, 22, AnsiString(0));
            //    }
            //    if (f < 40)
            //    {
            //        bitmap->Canvas->TextOutW(-(720 - bitmap->Width / 2) + 720 - Kur * 4 + f - 2, 22, f / 4);
            //    }
            //    if (f < 400)
            //    {
            //        bitmap->Canvas->TextOutW(-(720 - bitmap->Width / 2) + 720 - Kur * 4 + f - 6, 22, f / 4);
            //    }
            //    else
            //    {
            //        bitmap->Canvas->TextOutW(-(720 - bitmap->Width / 2) + 720 - Kur * 4 + f - 9, 22, f / 4);
            //    }
            //    f = f + 40;
            //}
            //f = 0;
            ////Out of range
            //while (f < 130)
            //{
            //    bitmap->Canvas->MoveTo(-(720 - bitmap->Width / 2) + 560 - Kur * 4 + f, 10);
            //    bitmap->Canvas->LineTo(-(720 - bitmap->Width / 2) + 560 - Kur * 4 + f, 22);

            //    bitmap->Canvas->MoveTo(-(720 - bitmap->Width / 2) + 560 - Kur * 4 + f + 20, 10);
            //    bitmap->Canvas->LineTo(-(720 - bitmap->Width / 2) + 560 - Kur * 4 + f + 20, 16);

            //    bitmap->Canvas->TextOutW(-(720 - bitmap->Width / 2) + 560 - Kur * 4 + f - 9, 22, 320 + f / 4);

            //    f = f + 40;
            //}

            //f = 0;
            //while (f < 130)
            //{
            //    bitmap->Canvas->MoveTo(bitmap->Width + (720 - bitmap->Width / 2) + 760 - Kur * 4 + f, 10);
            //    bitmap->Canvas->LineTo(bitmap->Width + (720 - bitmap->Width / 2) + 760 - Kur * 4 + f, 22);

            //    bitmap->Canvas->MoveTo(bitmap->Width + (720 - bitmap->Width / 2) + 760 - Kur * 4 + f + 20, 10);
            //    bitmap->Canvas->LineTo(bitmap->Width + (720 - bitmap->Width / 2) + 760 - Kur * 4 + f + 20, 16);

            //    bitmap->Canvas->TextOutW(bitmap->Width + (720 - bitmap->Width / 2) + 760 - Kur * 4 + f - 6, 22, 10 + f / 4);
            //    //int g=bitmap->Width+(720-bitmap->Width/2)+760-Kur*4-2;
            //    f = f + 40;
            //}




            ////Window of Course
            //bitmap->Canvas->Rectangle(bitmap->Width / 2 - 18, 17, bitmap->Width / 2 + 19, 43);
            //bitmap->Canvas->Font->Size = 15;
            //bitmap->Canvas->TextOutW(bitmap->Width / 2 - 16, 18, Kur);

            //--------------------------------------
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pictureBox3.Refresh();
            pictureBox4.Refresh();
        }

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            bunifuDropdown1.Clear();
            ListJoyDescriptor = DirectInputCntrl.DetectDevices();
            if (ListJoyDescriptor.Count > 0)
            {
                foreach (var Descriptor in ListJoyDescriptor)
                {
                    bunifuDropdown1.AddItem(Descriptor.DescriptorName);
                }
                bunifuDropdown1.selectedIndex = 0;
            }      
        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {
            if ( DirectInputCntrl.IsConnect() )
            {
                DirectInputCntrl.StopCapture();
                bunifuThinButton21.ButtonText = "Подключиться";
            }
            else
            {
                if ( (ListJoyDescriptor != null) && (ListJoyDescriptor.Count > 0) && (bunifuDropdown1.selectedIndex >= 0) )
                {
                    DirectInputCntrl.StartCapture(ListJoyDescriptor[bunifuDropdown1.selectedIndex].DescriptorGuid);
                }
                if (DirectInputCntrl.IsConnect())
                {
                    // Если отмечено автоподключение, то сохраняем название устройства
                    if (bunifuCheckbox2.Checked)
                    {
                        Settings.Default.SettingJoyStringConnect = ListJoyDescriptor[bunifuDropdown1.selectedIndex].DescriptorName;
                        Settings.Default.Save();
                    }
                    
                    bunifuThinButton21.ButtonText = "Отключить";
                }
            }
        }

        private void bunifuThinButton22_Click(object sender, EventArgs e)
        {
            if (UDPClient.IsConnect())
            {
                UDPClient.StopReceiving();
                bunifuThinButton22.ButtonText = "Подключиться";
            }
            else
            {
                UDPClient.StartReceiving();
                if (UDPClient.IsConnect())
                {
                    // Если отмечено автоподключение, то сохраняем название устройства
                    if (bunifuCheckbox1.Checked)
                    {
                        Settings.Default.SettingUdpPort = maskedTextBox3.Lines[0];
                        Settings.Default.Save();
                    }
                    bunifuThinButton22.ButtonText = "Отключить";
                }
            }
        }

        private void bunifuCheckbox1_OnChange(object sender, EventArgs e)
        {
            Settings.Default.SettingUdpAutoConnect = bunifuCheckbox1.Checked;
            Settings.Default.Save();
        }

        private void bunifuCheckbox2_OnChange(object sender, EventArgs e)
        {
            try
            {
                Settings.Default.SettingJoyAutoConnect = bunifuCheckbox2.Checked;
                // Сохраняем название выбранного устройства для автоподключения при запуске приложения
                if (DirectInputCntrl.IsConnect() && bunifuCheckbox2.Checked)
                {
                    Settings.Default.SettingJoyStringConnect = bunifuDropdown1.selectedValue;
                }
                Settings.Default.Save();
            }
            catch (Exception Ex)
            {
                LogError.MessageError(Ex, null, "События отображения формы", true);
            }
        }

        private char isCharToIP(object sender,KeyPressEventArgs e)
        {
            char ret = e.KeyChar;

            MaskedTextBox maskText = null;
            if (sender is MaskedTextBox)
                maskText = sender as MaskedTextBox;
            if (maskText!=null)
            {
                var indexer = maskText.SelectionStart;
                if (maskText.SelectionStart < 15)
                {
                    if (indexer == 3 || indexer == 7 || indexer == 11)
                    {
                        indexer++;
                    }
                    Regex regIP = new Regex(@"\b(([01]?\d?\d|2[0-4]\d|25[0-5])\.){3}([01]?\d?\d|2[0-4]\d|25[0-5])\b");
                    char[] chars = maskText.Lines[0].Substring(0, 15).ToCharArray();
                    chars[indexer] = e.KeyChar;
                    string tmp = new string(chars);
                    if (!regIP.IsMatch(tmp))
                    {
                        ret = '0';
                    }
                }
            }
            
            return ret;
        }

        private void maskedTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = isCharToIP(sender, e);
        }

        private void maskedTextBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = isCharToIP(sender, e);
        }

        private void TileButtonSaveMud_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(maskedTextBox1.Text))
            {
                Settings.Default.SettingAddressMud = maskedTextBox1.Lines[0];
                Settings.Default.Save();
                
                UDPClientClass.ipEndPoint_MUD = new System.Net.IPEndPoint(
                    IPAddress.Parse(Settings.Default.SettingAddressMud.Substring(0, 15)),
                    Convert.ToInt32(Settings.Default.SettingAddressMud.Substring(16, 5))
                    );
            }  
        }

        private void TileButtonSaveNav_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(maskedTextBox2.Text))
            {
                Settings.Default.SettingAddressNav = maskedTextBox2.Lines[0];
                Settings.Default.Save();

                UDPClientClass.ipEndPoint_NAV = new System.Net.IPEndPoint(
                    IPAddress.Parse(Settings.Default.SettingAddressNav.Substring(0, 15)),
                    Convert.ToInt32(Settings.Default.SettingAddressNav.Substring(16, 5))
                    );
            }  
        }

        private void bunifuThinButton23_Click(object sender, EventArgs e)
        {
            FontDialog fontDialog1 = new FontDialog();

            //if (fontDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    FlatButtonSettings.TextFont = fontDialog1.Font;
            //}

            if (fontDialog1.ShowDialog() == DialogResult.OK)
            {
                bunifuTextbox1.Font = fontDialog1.Font;
                bunifuTextbox1.ForeColor = fontDialog1.Color;

                ChangeFont(this.panel3, fontDialog1.Font);
            }
        }
        void ChangeFont(Control ctrl, Font font )
        {
            ctrl.Font = font;

            foreach (Control inner in ctrl.Controls)
            {
                if (inner is Panel)
                    ChangeFont(inner, font);
            }
                
        }

        private void FlatButtonManual_Click(object sender, EventArgs e)
        {
            panelManual.Visible = true;
            panelSettings.Visible = false;
            panelMapWay.Visible = false;
            pictureBox1.Refresh();
        }

        private void maskedTextPwm1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = isCharToIP(sender, e);
        }

        private void maskedTextPwm2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = isCharToIP(sender, e);
        }

        private void maskedTextPwm3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = isCharToIP(sender, e);
        }

        private void maskedTextEngine_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = isCharToIP(sender, e);
        }

        private void TileButtonSavePwm1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(maskedTextPwm1.Text))
            {
                Settings.Default.SettingAddressPwm1 = maskedTextPwm1.Lines[0];
                Settings.Default.Save();

                UDPClientClass.ipEndPoint_Pwm1 = new System.Net.IPEndPoint(
                    IPAddress.Parse(Settings.Default.SettingAddressPwm1.Substring(0, 15)),
                    Convert.ToInt32(Settings.Default.SettingAddressPwm1.Substring(16, 5))
                    );
            }  
        }

        private void TileButtonSavePwm2_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(maskedTextPwm2.Text))
            {
                Settings.Default.SettingAddressPwm2 = maskedTextPwm2.Lines[0];
                Settings.Default.Save();

                UDPClientClass.ipEndPoint_Pwm2 = new System.Net.IPEndPoint(
                    IPAddress.Parse(Settings.Default.SettingAddressPwm2.Substring(0, 15)),
                    Convert.ToInt32(Settings.Default.SettingAddressPwm2.Substring(16, 5))
                    );
            }  
        }

        private void TileButtonSavePwm3_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(maskedTextPwm3.Text))
            {
                Settings.Default.SettingAddressPwm3 = maskedTextPwm3.Lines[0];
                Settings.Default.Save();

                UDPClientClass.ipEndPoint_Pwm3 = new System.Net.IPEndPoint(
                    IPAddress.Parse(Settings.Default.SettingAddressPwm3.Substring(0, 15)),
                    Convert.ToInt32(Settings.Default.SettingAddressPwm3.Substring(16, 5))
                    );
            }  
        }

        private void TileButtonSaveEngine_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(maskedTextEngine.Text))
            {
                Settings.Default.SettingAddressEngine = maskedTextEngine.Lines[0];
                Settings.Default.Save();

                UDPClientClass.ipEndPoint_Engine = new System.Net.IPEndPoint(
                    IPAddress.Parse(Settings.Default.SettingAddressEngine.Substring(0, 15)),
                    Convert.ToInt32(Settings.Default.SettingAddressEngine.Substring(16, 5))
                    );
            }  
        }

    }
   
}
