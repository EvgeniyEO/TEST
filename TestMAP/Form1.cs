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
using MetroFramework.Forms;

namespace TestMAP
{
    public partial class Form1 : MetroForm
    {
        public Form1()
        {
            InitializeComponent();
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
            //18ти кратное приближение.
            gMapControl1.Zoom = 5;

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
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            gMapControl1.MaximumSize = new Size((Form1.ActiveForm.Size.Width / 100 * 70), Form1.ActiveForm.Size.Height);
   
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GroupBox groupBoxTST = new GroupBox();
            MetroFramework.Controls.MetroButton But1 = new MetroFramework.Controls.MetroButton();
            RadioButton RBut1 = new RadioButton();
            RBut1.Text = "Some text";
            groupBoxTST.Size = new Size(flowLayoutPanel1.Size.Width, flowLayoutPanel1.Size.Height/5);
            But1.Size = new Size(groupBoxTST.Size.Width / 2, groupBoxTST.Size.Height);
            RBut1.Size = new Size(groupBoxTST.Size.Width / 2, groupBoxTST.Size.Height);

            groupBoxTST.Controls.Add(But1);
            But1.Location = new Point(groupBoxTST.Location.X, groupBoxTST.Location.Y);
            groupBoxTST.Controls.Add(RBut1);
            RBut1.Location = new Point(groupBoxTST.Location.X + groupBoxTST.Size.Width / 2, groupBoxTST.Location.Y);



            this.flowLayoutPanel1.Controls.Add(groupBoxTST);

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            this.flowLayoutPanel1.Controls.RemoveAt(flowLayoutPanel1.Controls.Count-1);
        }

    }
}
