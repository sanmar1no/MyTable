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

namespace MyTable
{
    class ShellGrid //no using
    {
        public Client ShClient { get; set; }
        public RecordE ShRecordE { get; set; }
        public RecordW ShRecordW { get; set; }
        public Room ShRoom { get; set; } = new Room();
        public Transformer ShTransformer { get; set; }
        [Category("Помещение")]       
        [DisplayName("Корпус")]
        [Description("Подсказка: № корпуса/адрес:улица")]
        public string Building { get; set; }             //	№ корпуса/адрес:улица
        [Category("Помещение")]
        [DisplayName("Этаж")]
        [Description("Подсказка: № этажа")]
        public int Floor { get; set; }                   //	№ этажа
        [Category("Помещение")]
        [DisplayName("Кабинет(офис)")]
        [Description("Подсказка: 	№ помещения/номер дома+помещение")]
        public string Room { get; set; }                 //	№ помещения/номер дома+помещение
        [Category("Помещение")]
        [DisplayName("Площадь,кв.м.")]
        [Description("Подсказка: Площадь помещения, кв. м")]
        public double RoomArea { get; set; }             //	Площадь помещения, кв. м

        private string filenameAddressPlan;
        [Category("Помещение")]
        [DisplayName("Планировка")]
        [Description("Подсказка: Расположение  *.dwg планировки")]
        [Editor(typeof(OpenDWGFileNameEditorAddressPlan), typeof(UITypeEditor))]
        public string AddressPlan           //	Расположение  dwg планировки
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
        public string AddressCircuitPlan    //	Расположение  плана электросети
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
        public string AddressCircuitLine   //	Расположение  однолинейной схемы
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
        public string AddressCircuitWater //	Расположение  плана водоснабжения
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
        public string AddressCircuitHeat   //	Расположение  плана теплоснабжения
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
        public double RoomVolume { get; set; }           //	Объем помещения
        [Category("Помещение")]
        [DisplayName("Коэфф.теплоснабж.")]
        [Description("Подсказка: Коэффициент для расчета тепла")]
        public double RatioHeat { get; set; }            //	Коэффициент для расчета тепла
        [Browsable(false)]
        [Category("Помещение")]
        [DisplayName("Координаты помещения")]
        [Description("Подсказка: Список координат помещения (для отображения на карте)")]
        public string CoordinatesPoints { get; set; }    //	Координаты расположения помещения
        [Category("Помещение")]
        [DisplayName("Список электросчетчиков")]
        [Description("Подсказка: Список электросчетчиков")]
        public List<CounterE> ShCountersE { get; set; } = new List<CounterE>();    //  Список электросчетчиков	
        [Category("Помещение")]
        [DisplayName("Список водомеров")]
        [Description("Подсказка: Список водомеров")]
        public List<CounterW> ShCountersW { get; set; } = new List<CounterW>();   //  Список водомеров
        [Browsable(false)]
        [Category("Помещение")]
        [DisplayName("Арендаторы")]
        [Description("Подсказка: Список арендаторов/абонентов")]
        public List<Client> clientsList { get; set; } = new List<Client>();   //  Список арендаторов/абонентов

        public void GetRoom()
        {            
            Building = ShRoom.building;
            Floor = ShRoom.floor;
            Room = ShRoom.room;
            RoomArea = ShRoom.roomArea;
            AddressPlan = ShRoom.addressPlan;
            AddressCircuitPlan = ShRoom.addressCircuitPlan;
            AddressCircuitLine = ShRoom.addressCircuitLine;
            AddressCircuitWater = ShRoom.addressCircuitWater;
            AddressCircuitHeat = ShRoom.addressCircuitHeat;
            RoomVolume = ShRoom.roomVolume;
            RatioHeat = ShRoom.ratioHeat;
            CoordinatesPoints = ShRoom.coordinatesPoints;
            ShCountersE = ShRoom.countersE;
            ShCountersW = ShRoom.countersW;
            clientsList = ShRoom.clientsList;
        }
    }
    public class OpenDWGFileNameEditorAddressPlan : FileNameEditor
    {
        private string _parentFolder = "\\Планировки";
        protected override void InitializeDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + _parentFolder;
            base.InitializeDialog(openFileDialog);        
            
            openFileDialog.Filter = "Autocad files (*.dwg)|*.dwg";
            /*
            base.InitializeDialog(openFileDialog);
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + _parentFolder;
            openFileDialog.Filter = "Autocad files (*.dwg)|*.dwg";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string folder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location.ToLower());
                if (openFileDialog.FileName.ToLower().IndexOf(folder) > -1)
                {
                    int i1 = folder.Length;
                    openFileDialog.FileName = openFileDialog.FileName.Substring(i1, openFileDialog.FileName.Length - i1);
                }
            }
             */
        }
    }
    public class OpenDWGFileNameEditorCircuitPlan : FileNameEditor
    {
        private string _parentFolder = "\\Планы электросетей";
        protected override void InitializeDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + _parentFolder;
            base.InitializeDialog(openFileDialog);
            openFileDialog.Filter = "Autocad files (*.dwg)|*.dwg";
        }
    }
    public class OpenDWGFileNameEditorCircuitLine : FileNameEditor
    {
        private string _parentFolder = "\\Однолинейные схемы";
        protected override void InitializeDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + _parentFolder;
            base.InitializeDialog(openFileDialog);
            openFileDialog.Filter = "Autocad files (*.dwg)|*.dwg";
        }
    }
    public class OpenDWGFileNameEditorCircuitWater : FileNameEditor
    {
        private string _parentFolder = "\\Планы водоснабжения";
        protected override void InitializeDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + _parentFolder;
            base.InitializeDialog(openFileDialog);
            openFileDialog.Filter = "Autocad files (*.dwg)|*.dwg";
        }
    }
    public class OpenDWGFileNameEditorCircuitHeat : FileNameEditor
    {
        private string _parentFolder = "\\Планы теплоснабжения";
        protected override void InitializeDialog(OpenFileDialog openFileDialog)
        {
            openFileDialog.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + _parentFolder;
            base.InitializeDialog(openFileDialog);
            openFileDialog.Filter = "Autocad files (*.dwg)|*.dwg";
        }
    }
}
