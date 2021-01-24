using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;

namespace MyTable
{
    public class CounterE
    {
        [Browsable(false)]
        public int ID{ get; set; }                       //	Уникальный номер

        [Browsable(false)]
        public string roomsID{ get; set; }               //	ID Помещения	

        [Category("Электросчетчик")]
        [DisplayName("Номер счетчика")]
        [Description("Подсказка: № электросчетчика")]
        public string number { get; set; } = "0000";     //	Номер счётчика	

        [Category("Электросчетчик")]
        [DisplayName("Марка счетчика")]
        [Description("Подсказка: Модель (марка) электросчетчика")]
        public string model{ get; set; }                 //	Модель (марка) счётчика		

        [Category("Электросчетчик")]
        [DisplayName("Год поверки")]
        [Description("Подсказка: Квартал и год поверки электросчетчика")]
        [Editor(typeof(DataQEditor), typeof(UITypeEditor))]
        public DateTimeQ verificationYear { get; set; } = new DateTimeQ();   //	Год поверки		

        [Category("Электросчетчик")]
        [DisplayName("Год изготовления")]
        [Description("Подсказка: Квартал (если указан) и год изготовления электросчетчика")]
        [Editor(typeof(DataQEditor), typeof(UITypeEditor))]
        public DateTimeQ madeYear{ get; set; } = new DateTimeQ();          //	Год изготовления	

        [Category("Электросчетчик")]
        [DisplayName("Дата опломбирования")]
        [Description("Подсказка: Дата опломбирования электросчетчика")]
        public DateTime sealDate{ get; set; }            //	Дата опломбирования		

        [Category("Электросчетчик")]
        [DisplayName("Коэффициент учета")]
        [Description("Подсказка: Коэффициент учета (по умолчанию, без ТТ и ТН:'1')")]
        public double ratio { get; set; } = 1;           //	Коэффициент учета		

        [Category("Электросчетчик")]
        [DisplayName("Список пломб на счётчике")]
        [Description("Подсказка: Список пломб на счётчике (по одной на строку)")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, " +
            "Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, " +
            "Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public List<string> sealList { get; set; } = new List<string>();        //	Список пломб на счётчике		

        [Category("Электросчетчик")]
        [DisplayName("Класс точности")]
        [Description("Подсказка: Класс точности электросчетчика (по умолчанию:'0.5')")]
        public double accuracyClass{ get; set; }=0.5;         //	Класс точности		

        [Category("Электросчетчик")]
        [DisplayName("Координаты")]
        [Description("Подсказка: Координаты расположения электросчетчика на общей схеме")]
        public Point coordinatesPointsC{ get; set; }   //	Координаты расположения эл. счетчика	
        
        [Category("Электросчетчик")]
        [DisplayName("Фото счетчика")]
        [Description("Подсказка: Список адресов фотографий электросчетчика")]
        //нужен отдеьный класс
        public List<string> addressListC{ get; set; }    //	Расположение фотографий счетчика	

        [Browsable(false)]
        public string coordinatesRoomsID{ get; set; }    //	ID Помещения в котором уст. счетчик		

        [Category("Электросчетчик")]
        [DisplayName("Номер ТП")]
        [Description("Подсказка: Номер ТП, от которой подключен электросчетчик")]
        public string substantionNo { get; set; }         //	Номер ТП откуда подключен

        [Category("Электросчетчик")]
        [DisplayName("Номер СП")]
        [Description("Подсказка: Номер СП, от которой подключен электросчетчик")]
        public string substantionCabNo{ get; set; }      //	Номер СП откуда подключен		

        [Category("Электросчетчик")]
        [DisplayName("Марка вводного кабеля")]
        [Description("Подсказка: Марка вводного кабеля (перед счетчиком)")]
        public string cableModel{ get; set; }            //	Марка кабеля ввода	

        [Category("Электросчетчик")]
        [DisplayName("Длина вводного кабеля")]
        [Description("Подсказка: Длина кабеля до счетчика, м")]
        public double cableLenght{ get; set; }           //	Длина кабеля до счетчика, м	

        [Category("Электросчетчик")]
        [DisplayName("Разрешенная мощность")]
        [Description("Подсказка: Разрешенная мощность, кВт")]
        public double power{ get; set; }                 //	Разрешенная мощность, кВт	

        [Category("Электросчетчик")]
        [DisplayName("Отключающее устройство")]
        [Description("Подсказка: Тип (марка) вводного отключающего устройства (автомата или рубильника)")]
        public string switchType{ get; set; }            //	Тип отключающего устройства	


        [Category("Электросчетчик")]
        [DisplayName("Уставка In(А)")]
        [Description("Подсказка: Уставка In(А) вводного отключающего устройства")]
        public int switchValue{ get; set; }              //	Уставка In(А) вводного устройства	

        [Category("Электросчетчик")]
        [DisplayName("ТТ и ТН")]
        [Description("Подсказка: Список трансформаторов тока и напряжения (при их наличии)")]
        public Transformer transformers { get; set; } = new Transformer(); // Список трансформаторов	

        [Category("Электросчетчик")]
        [DisplayName("Показания")]
        [Description("Подсказка: Показания электросчетчика")]
        public List<RecordE> recordsList { get; set; } = new List<RecordE>();      // Показания электросчетчиков


        public override string ToString()
        {
            return number.ToString();
        }

    }


}


