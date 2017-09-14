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
        private System.Windows.Forms.Panel panelSettings;
        private System.Windows.Forms.Panel panelManual;
        //Переменная нового класса,
        //для замены стандартного маркера.
        private TestMAP.GMapMarkerImage currentMarker;
        //Список маркеров и роутов
        private GMap.NET.WindowsForms.GMapOverlay markersOverlay;
        private GMap.NET.WindowsForms.GMapOverlay routOverlay;
        private GMap.NET.WindowsForms.GMapRoute routes;
        private Bitmap bitmapBlackCh;
        private Bitmap bitmapBlackNCh;

        private Bunifu.Framework.UI.BunifuMetroTextbox ManualTextboxX;
        private TestMAP.DirectInputController DirectInputCntrl;

        List<JoystickDescriptor> ListJoyDescriptor;
        private TestMAP.UDPClientClass UDPClient;
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
            InitializeComponent();
            MyInitializeComponent();
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
            this.panelSettings = new System.Windows.Forms.Panel();
            this.panelManual = new System.Windows.Forms.Panel();
            this.DirectInputCntrl = new DirectInputController();

            this.DirectInputCntrl.JoystickXchange += JoystickX_change;
            this.DirectInputCntrl.JoystickYchange += JoystickY_change;
            this.DirectInputCntrl.Packet_CA_change += JoystickPacket_CA_change;
            //List<JoystickDescriptor> ListJoy = DirectInputCntrl.DetectDevices();
            //DirectInputCntrl.StartCapture(ListJoy[0].DescriptorGuid);

            this.UDPClient = new UDPClientClass(50000);
            UDPClient.ReceiveData += UDPClient_ReceiveData;
            UDPClient.StartReceiving();


            this.ManualTextboxX = new Bunifu.Framework.UI.BunifuMetroTextbox();
            this.panelManual.Controls.Add(this.ManualTextboxX);


            // 
            // ManualTextboxX
            // 
            //this.ManualTextboxX.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("bunifuTextbox1.BackgroundImage")));
            //this.ManualTextboxX.Icon = null;
            this.ManualTextboxX.BackColor = System.Drawing.Color.Silver;
            this.ManualTextboxX.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ManualTextboxX.ForeColor = System.Drawing.Color.SeaGreen;
            this.ManualTextboxX.Location = new System.Drawing.Point(39, 449);
            this.ManualTextboxX.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ManualTextboxX.Name = "ManualTextboxX";
            this.ManualTextboxX.Size = new System.Drawing.Size(195, 114);
            this.ManualTextboxX.TabIndex = 4;
            this.ManualTextboxX.Text = "ManualTextboxX";
            this.ManualTextboxX.Visible = true;
            
            // 
            // panelSettings
            // 
            this.panelSettings.SuspendLayout();
            this.Controls.Add(this.panelSettings);
            this.panelSettings.ResumeLayout(false);

            this.panelSettings.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            //this.panelMapWay.Controls.Add(this.panelTab);
            //this.panelMapWay.Controls.Add(this.panelMap);
            //this.animatorPanelGradient.SetDecoration(this.panelSettings, BunifuAnimatorNS.DecorationType.None);
            this.panelSettings.Location = new System.Drawing.Point(310, 30);
            this.panelSettings.Name = "panelSettings";
            this.panelSettings.Size = new System.Drawing.Size(1517, 880);
            this.panelSettings.TabIndex = 10;

            // 
            // panelManual
            // 
            this.panelManual.SuspendLayout();
            this.Controls.Add(this.panelManual);
            this.panelManual.ResumeLayout(false);

            this.panelManual.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            //this.panelMapWay.Controls.Add(this.panelTab);
            //this.panelMapWay.Controls.Add(this.panelMap);
            //this.animatorPanelGradient.SetDecoration(this.panelSettings, BunifuAnimatorNS.DecorationType.None);
            this.panelManual.Location = new System.Drawing.Point(310, 30);
            this.panelManual.Name = "panelManual";
            this.panelManual.Size = new System.Drawing.Size(1517, 880);
            this.panelManual.TabIndex = 10;

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
            FlatButtonManual.Width = 280;
            FlatButtonSettings.Width = 280;

            pictureBox1.BackColor = Color.White;
            pictureBox1.Visible = true;

            // Connect the Paint event of the PictureBox to the event handler method.
            pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);

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
                //MessageBox.Show(e1.Message + " / " + e1.HelpLink + " / " + e1.Source + " / " + e1.TargetSite + " / " + e1.Data);
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
            if (panelMenuGradient.Size.Width == 60)
            {
                panelMenuGradient.Width = 280;
                Form1_Resize(this, e);
                panelMenuGradient.Visible = false;
                FlatButtonMapWay.Text = "              Map and Waypoint";
                FlatButtonManual.Text = "              Manual";
                FlatButtonSettings.Text = "              Settings";
                animatorPanelGradient.ShowSync(panelMenuGradient);
                

            }
            else
            {
                panelMenuGradient.Width = 60;
                Form1_Resize(this, e);
                panelMenuGradient.Visible = false;
                FlatButtonMapWay.Text = "";
                FlatButtonManual.Text = "";
                FlatButtonSettings.Text = "";
                animatorPanelGradient.ShowSync(panelMenuGradient);
                
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
        private void FlatButtonManual_Click(object sender, EventArgs e)
        {
            panelManual.Visible = true;
            panelSettings.Visible = false;
            panelMapWay.Visible = false;
            pictureBox1.Refresh();
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
                TextBox.Text = text;
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
        private void JoystickX_change(object sender, JoystickButtonPressedEventArgs e)  
        {
            SetTextBox(e.Value.ToString(), ManualTextboxX);
            joyX = e.Value;
            //pictureBox1.Refresh();
            RefreshPicBox(pictureBox1);
        }
        private void JoystickY_change(object sender, JoystickButtonPressedEventArgs e)
        {
            //SetTextBox(e.Value.ToString(), ManualTextboxX);
            joyY = e.Value;
            //pictureBox1.Refresh();
            RefreshPicBox(pictureBox1);
        }
        private void JoystickPacket_CA_change(object sender, JoystickPacket_CAEventArgs e)
        {
            Packet_CA packet_CA = e.packet_CA;
            if (UDPClient != null)
            {
                UDPClient.Send(ReceiveData.getBytesToSendPacketCA(packet_CA), ReceiveData.getLenToSendPacketCA(), UDPClient.ipEndPoint_MUD);
            }
        }

        void UDPClient_ReceiveData(object sender, UDPClientClass.UdpClientEventArgs e)
        {
            //var data = Data1.FromBytes(e.Data);
            //var data = Data.FromBytes(e.Data);
            //SetTextBox(Encoding.ASCII.GetString(e.Data), bunifuMetroTextbox1);

            ReceiveData = new ReceiveBytesData(e.Data);
            SetTextBox(ReceiveData.packet_AB.rms_N3.ToString(), ManualTextboxX);
            if (sender is UDPClientClass)
            {
                UDPClientClass client = sender as UDPClientClass;
                client.Send(ReceiveData.getBytesToSendPacketCA(), ReceiveData.getLenToSendPacketCA(), e.endPoint);
            }
  
        }

        int joyX = 32767;
        int joyY = 32767;
        private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {
            // Создаем локальную версию графического объекта для PictureBox
            //PictureBox picbox = sender as PictureBox;
            //Graphics g = e.Graphics;
            //g.Clear(Color.Transparent);

            //int num = joyX / 4096;

            //Rectangle[] rects = { new Rectangle(0, 0, 0, 0)};
            //if (num > 8)
            //{
            //    for (int i = 0; i < (num - 8); i++)
            //    {
            //        Array.Resize(ref rects, rects.Length + 1);
            //        rects[rects.Length - 1] = new Rectangle(0, 150 + i * 15, 100, 10);
            //    }
            //}
            //else
            //{
            //    for (int i = 0; i < (7 - num); i++)
            //    {
            //        Array.Resize(ref rects, rects.Length + 1);
            //        rects[rects.Length - 1] = new Rectangle(0, 150 - i * 15, 100, 10);
            //    }
            //}

            //if (rects != null)
            //    g.FillRectangles(Brushes.Black, rects);


            //LinearGradientBrush linGrBrush = new LinearGradientBrush(
            //   new Point(0, picbox.Height / 2),
            //   new Point(picbox.Width, picbox.Height / 2),
            //   Color.Red,
            //   Color.Blue);

            //g.FillRectangle(linGrBrush, 0, 0, 200, 50);
            //linGrBrush.GammaCorrection = true;
            //g.FillRectangle(linGrBrush, 0, 60, 200, 50);
            //g.FillRectangle(linGrBrush, 60, 120, 200, 50);
            //g.FillRectangle(linGrBrush, 0, 200, picbox.Width, 50);


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

        private void bunifuTileButton1_Click(object sender, EventArgs e)
        {
            bunifuDropdown1.Clear();
            ListJoyDescriptor = DirectInputCntrl.DetectDevices();
            foreach (var Descriptor in ListJoyDescriptor)
	        {
                bunifuDropdown1.AddItem(Descriptor.DescriptorName);
	        }
            bunifuDropdown1.selectedIndex = 0;
             
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
                if (DirectInputCntrl.IsConnect())
                {
                    bunifuThinButton21.ButtonText = "Отключить";
                }
            }
        }



    }
   
}
