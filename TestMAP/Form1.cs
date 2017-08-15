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

namespace TestMAP
{
    public partial class Form1 : MetroForm
    {
        // Колонки в таблице bunifuCustomDataGrid1
        private System.Windows.Forms.DataGridViewTextBoxColumn ColNoMarker;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ColInMission;
        private System.Windows.Forms.DataGridViewTextBoxColumn ColLatLon;
        private System.Windows.Forms.DataGridViewButtonColumn ColDelMarker;

        //Переменная отвечающая за состояние нажатия 
        //левой клавиши мыши.
        private bool isLeftButtonDown = false;

        //Таймер для вывода
        private Timer blinkTimer = new Timer();

        //Переменная нового класса,
        //для замены стандартного маркера.
        private TestMAP.GMapMarkerImage currentMarker;

        int currentMarkerInd = 0;
        string StrFormatLatLng = "{0:0.0000000} - {1:0.0000000}";
        Point CurrentMenuPoint;

        //Список маркеров и роутов
        private GMap.NET.WindowsForms.GMapOverlay markersOverlay;
        private GMap.NET.WindowsForms.GMapOverlay routOverlay;
        GMap.NET.WindowsForms.GMapRoute routes;
        GMap.NET.WindowsForms.GMapMarker markers;
        Bitmap bitmapBlackCh;
        Bitmap bitmapBlackNCh;

        
        List<PointLatLng> polygonPoints1 = new List<PointLatLng>();
        Size MetroTabControlSize = new Size(1498, 262);

        public Form1()
        {
            InitializeComponent();
            MyInitializeComponent();
        }

        private void MyInitializeComponent()
        {
            this.ColNoMarker = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColInMission = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ColLatLon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ColDelMarker = new System.Windows.Forms.DataGridViewButtonColumn();
            this.bunifuCustomDataGrid1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ColNoMarker,
            this.ColInMission,
            this.ColLatLon,
            this.ColDelMarker});

            ColNoMarker.HeaderText = "№ Marker";
            ColNoMarker.Name = "ColNoMarker";
            ColNoMarker.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //ColNoMarker.Resizable = System.Windows.Forms.DataGridViewTriState.True;


            ColInMission.HeaderText = "In Mission";
            ColInMission.Name = "ColInMission";
            ColInMission.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ColLatLon.HeaderText = "Lat / Lon";
            ColLatLon.Name = "ColLatLng";
            ColLatLon.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ColDelMarker.HeaderText = "№ Marker";
            ColDelMarker.Name = "ColDelMarker";
            ColDelMarker.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            bitmapBlackCh =
                Bitmap.FromFile(Application.StartupPath + @"\BlackCheck.png") as Bitmap;

            bitmapBlackNCh =
                Bitmap.FromFile(Application.StartupPath + @"\BlackNoCheck.png") as Bitmap;
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
                if (currentMarker != null)
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
                        string.Format("{0},{1}", point.Lat, point.Lng);

                    // Изменение  Lat/Lng в таблице вместе с переносом маркера
                    bunifuCustomDataGrid1.Rows[currentMarkerInd].Cells[ColLatLon.Name].Value = string.Format(StrFormatLatLng, point.Lat, point.Lng);
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
            marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;
            marker.ToolTipText = string.Format(StrFormatLatLng, point.Lat, point.Lng);
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

        void switchChekedMarker(int Index, bool inMission)
        {
            GPoint pnt = gMapControl1.FromLatLngToLocal(markersOverlay.Markers[Index].Position);
            DropMarkerByIndex(Index);
            AddMarkerByIndex(pnt.X, pnt.Y, Index, inMission);
        }

        private void Method1(object sender, EventArgs e)
        {
            //MenuItem item = sender as MenuItem;

            ////Если надо установить только один маркер,
            ////то выполняем очистку списка маркеров
            ////markersOverlay.Markers.Clear();
            //PointLatLng point = gMapControl1.FromLocalToLatLng(CurrentMenuPoint.X, CurrentMenuPoint.Y);

            ////Инициализируем новую переменную изображения и
            ////загружаем в нее изображение маркера,
            ////лежащее возле исполняемого файла
            //Bitmap bitmap =
            //    Bitmap.FromFile(Application.StartupPath + @"\marker.png") as Bitmap;

            ////Инициализируем новый маркер с использованием 
            ////созданного нами маркера.
            //GMapMarker marker = new GMapMarkerImage(point, bitmapBlackCh);

            //marker.ToolTipMode = MarkerTooltipMode.OnMouseOver;

            ////В качестве подсказки к маркеру устанавливаем 
            ////координаты где он устанавливается.
            ////Данные о местоположении маркера, вы можете вывести в любой компонент
            ////который вам нужен.
            ////например:
            ////textBo1.Text = point.Lat;
            ////textBo2.Text = point.Lng;
            //marker.ToolTipText = string.Format(StrFormatLatLng, point.Lat, point.Lng);
            ////Добавляем маркер в список маркеров.
            //markersOverlay.Markers.Add(marker);

            //routes.Points.Add(point);
            //gMapControl1.UpdateRouteLocalPosition(routes);
            //bunifuCustomDataGrid1.Rows.Add(markersOverlay.Markers.IndexOf(marker), true, string.Format(StrFormatLatLng, point.Lat, point.Lng));
           
            int index = AddMarkerToEnd(CurrentMenuPoint.X, CurrentMenuPoint.Y, true);

            bunifuCustomDataGrid1.Rows.Add(index, true, string.Format(StrFormatLatLng, markersOverlay.Markers[index].Position.Lat, markersOverlay.Markers[index].Position.Lng));
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

                bunifuCustomDataGrid1.Rows.RemoveAt(currentMarkerInd);
                foreach (DataGridViewRow Row in bunifuCustomDataGrid1.Rows)
                {
                    if (Convert.ToInt32(Row.Cells["ColNoMarker"].Value) > currentMarkerInd)
                    {
                        Row.Cells["ColNoMarker"].Value = Convert.ToInt32(Row.Cells["ColNoMarker"].Value) - 1;
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
            bunifuCustomDataGrid1.Rows.Clear();

        }

        void mapControl_MouseDown(object sender, MouseEventArgs e)
        {
            ContextMenu rightClickMenuStrip = new ContextMenu();
            rightClickMenuStrip.MenuItems.Add("Добавить точку", new EventHandler(Method1));
            rightClickMenuStrip.MenuItems.Add("Удалить точку", new EventHandler(Method2));
            rightClickMenuStrip.MenuItems.Add("Удалить все точки", new EventHandler(Method3));


            switch (e.Button)
            {
                case MouseButtons.Left:
                    isLeftButtonDown = true;
                    if (currentMarker != null)
                    {
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
                    rightClickMenuStrip.Show(this, new Point(e.X, e.Y));
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
            if (item is GMapMarkerImage)
            {
                currentMarker = null;
                GMapMarkerImage m = item as GMapMarkerImage;
                m.Pen.Dispose();
                m.Pen = null;
            }
        }

        //Устанавливаем вокруг маркера красный квадрат
        //если маркер выделен клавишей Enter
        void mapControl_OnMarkerEnter(GMapMarker item)
        {
            if (item is GMapMarkerImage)
            {
                currentMarker = item as GMapMarkerImage;
                currentMarker.Pen = new Pen(Brushes.Red, 2);
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
            metroTabControl1.Location = new Point(Form1.ActiveForm.Size.Width - metroTabControl1.Size.Width - 20, Form1.ActiveForm.Size.Height - metroTabControl1.Size.Height - 20);
            metroTabControl1.Size = new Size(Form1.ActiveForm.Size.Width - 40 < MetroTabControlSize.Width ? Form1.ActiveForm.Size.Width - 40 : MetroTabControlSize.Width, metroTabControl1.Size.Height);

            
        }

        private void bunifuCustomDataGrid1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (bunifuCustomDataGrid1.Columns[e.ColumnIndex] == ColDelMarker)
            {
                bunifuCustomDataGrid1.Rows.RemoveAt(e.RowIndex);

                routes.Points.RemoveAt(e.RowIndex);
                markersOverlay.Markers.RemoveAt(e.RowIndex);
                gMapControl1.UpdateRouteLocalPosition(routes);

                foreach (DataGridViewRow Row in bunifuCustomDataGrid1.Rows)
                {
                    if (Convert.ToInt32(Row.Cells["ColNoMarker"].Value) > e.RowIndex)
                    {
                        Row.Cells["ColNoMarker"].Value = Convert.ToInt32(Row.Cells["ColNoMarker"].Value) - 1;
                    }
                }

            }
        }

        private void bunifuCustomDataGrid1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == ColInMission.Index && e.RowIndex != -1)
            {
                if (bunifuCustomDataGrid1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString() == "True")
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
                bunifuCustomDataGrid1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }
    }
}
