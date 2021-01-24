using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace MyTable {
    public class Room {

        [Browsable(false)]
        public int id { get; set; }                     //	Уникальный номер

        [Category("Помещение")]
        [DisplayName("Корпус")]
        [Description("Подсказка: № корпуса/адрес:улица")]
        public string building { get; set; }             //	№ корпуса/адрес:улица

        [Category("Помещение")]
        [DisplayName("Этаж")]
        [Description("Подсказка: № этажа")]
        public int floor { get; set; }                   //	№ этажа

        [Category("Помещение")]
        [DisplayName("Кабинет(офис)")]
        [Description("Подсказка: 	№ помещения/номер дома+помещение")]
        public string room { get; set; }                 //	№ помещения/номер дома+помещение

        [Category("Помещение")]
        [DisplayName("Площадь,кв.м.")]
        [Description("Подсказка: Площадь помещения, кв. м")]
        public double roomArea { get; set; }             //	Площадь помещения, кв. м

        private string filenameAddressPlan;
        [Category("Помещение")]
        [DisplayName("Планировка")]
        [Description("Подсказка: Расположение  *.dwg планировки")]
        [Editor(typeof(OpenDWGFileNameEditorAddressPlan), typeof(UITypeEditor))]
        public string addressPlan           //	Расположение  dwg планировки
        {
            get
            {
                return filenameAddressPlan;
            }
            set
            {
                if (value != null)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(value);
                    filenameAddressPlan = file.Name;
                }
                else
                {
                    filenameAddressPlan = "";
                }
            }
        }

        private string filenameAddressCircuitPlan;
        [Category("Помещение")]
        [DisplayName("План электросети")]
        [Description("Подсказка: Расположение  *.dwg плана электросети")]
        [Editor(typeof(OpenDWGFileNameEditorCircuitPlan), typeof(UITypeEditor))]
        public string addressCircuitPlan    //	Расположение  плана электросети
        {
            get
            {
                return filenameAddressCircuitPlan;
            }
            set
            {
                if (value != null)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(value);
                    filenameAddressCircuitPlan = file.Name;
                }
                else
                {
                    filenameAddressCircuitPlan = "";
                }
            }
        }

        private string filenameAddressCircuitLine;
        [Category("Помещение")]
        [DisplayName("Однолинейная схема")]
        [Description("Подсказка: Расположение  *.dwg однолинейной схемы")]
        [Editor(typeof(OpenDWGFileNameEditorCircuitLine), typeof(UITypeEditor))]
        public string addressCircuitLine    //	Расположение  однолинейной схемы
        {
            get
            {
                return filenameAddressCircuitLine;
            }
            set
            {/*
                string folder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location.ToLower());
                if (value.ToLower().IndexOf(folder) > -1)
                {
                    int i1 = folder.Length;
                    filename = value.Substring(i1, value.Length - i1);
                }
                else filename = value;*/
                if (value != null)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(value);
                    filenameAddressCircuitLine = file.Name;
                }
                else
                {
                    filenameAddressCircuitLine = "";
                }

            }
        }

        private string filenameAddressCircuitWater;
        [Category("Помещение")]
        [DisplayName("План водоснабжения")]
        [Description("Подсказка: Расположение  *.dwg плана водоснабжения")]
        [Editor(typeof(OpenDWGFileNameEditorCircuitWater), typeof(UITypeEditor))]
        public string addressCircuitWater   //	Расположение  плана водоснабжения
        {
            get
            {
                return filenameAddressCircuitWater;
            }
            set
            {
                if (value != null)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(value);
                    filenameAddressCircuitWater = file.Name;
                }
                else
                {
                    filenameAddressCircuitWater = "";
                }

            }
        }

        private string filenameAddressCircuitHeat;
        [Category("Помещение")]
        [DisplayName("План теплоснабжения")]
        [Description("Подсказка: Расположение  *.dwg плана теплоснабжения")]
        [Editor(typeof(OpenDWGFileNameEditorCircuitHeat), typeof(UITypeEditor))]
        public string addressCircuitHeat    //	Расположение  плана теплоснабжения
        {
            get
            {
                return filenameAddressCircuitHeat;
            }
            set
            {
                if (value != null)
                {
                    System.IO.FileInfo file = new System.IO.FileInfo(value);
                    filenameAddressCircuitHeat = file.Name;
                }
                else
                {
                    filenameAddressCircuitHeat = "";
                }
            }
        }

        [Category("Помещение")]
        [DisplayName("Объем")]
        [Description("Подсказка: Объем помещения, куб.м.")]
        public double roomVolume { get; set; }           //	Объем помещения

        [Category("Помещение")]
        [DisplayName("Коэфф.теплоснабж.")]
        [Description("Подсказка: Коэффициент для расчета тепла")]
        public double ratioHeat { get; set; }            //	Коэффициент для расчета тепла

        [Browsable(false)]
        [Category("Помещение")]
        [DisplayName("Координаты помещения")]
        [Description("Подсказка: Список координат помещения (для отображения на карте)")]
        public string coordinatesPoints { get; set; }    //	Координаты расположения помещения

        [Category("Помещение")]
        [DisplayName("Список электросчетчиков")]
        [Description("Подсказка: Список электросчетчиков")]
        public List<CounterE> countersE { get; set; } = new List<CounterE>();    //  Список электросчетчиков	

        [Category("Помещение")]
        [DisplayName("Список водомеров")]
        [Description("Подсказка: Список водомеров")]
        public List<CounterW> countersW { get; set; } = new List<CounterW>();    //  Список водомеров

        [Browsable(false)]
        [Category("Помещение")]
        [DisplayName("Арендаторы")]
        [Description("Подсказка: Список арендаторов/абонентов")]
        public List<Client> clientsList { get; set; } = new List<Client>();       //  Список арендаторов/абонентов

        public string getStr() {
            return $"id: {id}, " +
                $"корп: {building}, " +
                $"этаж: {floor}, " +
                $"пом: {room}, " +
                $"площ.: {roomArea}, " +
                $"объем.: {roomVolume}, " +
                $"коэфф.: {ratioHeat}" +
                $"\r\n";
        }

        //Преобразует строку coordinatesPoints с разделителями ";" в массив координат Point[] (метод - StrToPoint())
        public Point[] GetCoordinatesPoints() {
            List<string> x = new List<string>();
            List<string> y = new List<string>();

            string coordinates = this.coordinatesPoints;

            while (true) {
                int k = coordinates.IndexOf(";");
                
                if (k > 0) {
                    x.Add(coordinates.Substring(0, k));
                    coordinates = coordinates.Substring(k + 1);
                } else break;

                k = coordinates.IndexOf(";");
                
                if (k > 0) {
                    y.Add(coordinates.Substring(0, k));
                    coordinates = coordinates.Substring(k + 1);
                } else {
                    y.Add(coordinates);
                    break;
                }
            }

            Point[] points = new Point[x.Count];
            
            for (int i = 0; i < x.Count; i++) {
                points[i].X = int.Parse(x[i]);
                points[i].Y = int.Parse(y[i]);
            }

            return points;
        }


    }
}
