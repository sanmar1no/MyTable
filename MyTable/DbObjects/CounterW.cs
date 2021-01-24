using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;

namespace MyTable
{
    public class CounterW
    {
        [Browsable(false)]
        public int ID{ get; set; }                       //	Уникальный номер	

        [Browsable(false)]
        public string roomsID{ get; set; }               //	ID Помещения	

        [Category("Водомер")]
        [DisplayName("Номер счетчика")]
        [Description("Подсказка: № водомера")]
        public string number{ get; set; } = "0000";      //	Номер счётчика	

        [Category("Водомер")]
        [DisplayName("Модель счетчика")]
        [Description("Подсказка: Модель (марка) водомера")]
        public string model{ get; set; }                 //	Модель (марка) счётчика	

        [Category("Водомер")]
        [DisplayName("Год поверки")]
        [Description("Подсказка: Квартал и год поверки")]
        [Editor(typeof(DataQEditor), typeof(UITypeEditor))]
        public DateTimeQ verificationYear { get; set; } = new DateTimeQ();   //	Год поверки	

        [Category("Водомер")]
        [DisplayName("Год изготовления")]
        [Description("Подсказка: Квартал и год изготовления")]
        [Editor(typeof(DataQEditor), typeof(UITypeEditor))]
        public DateTimeQ madeYear{ get; set; } = new DateTimeQ();        //	Год изготовления	

        [Category("Водомер")]
        [DisplayName("Дата опломбирования")]
        [Description("Подсказка: Дата опломбирования")]
        public DateTime sealDate{ get; set; }            //	Дата опломбирования	

        [Category("Водомер")]
        [DisplayName("Коэффициент учета")]
        [Description("Подсказка: Коэффициент учета (по умолчанию: 1)")]
        public double ratio { get; set; } = 1;           //	Коэффициент учета	

        [Category("Водомер")]
        [DisplayName("Список пломб на счётчике")]
        [Description("Подсказка: Список пломб на счётчике")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, " +
            "Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, " +
            "Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public List<string> sealList { get; set; } = new List<string>();        //	Список пломб на счётчике	

        [Category("Водомер")]
        [DisplayName("Класс точности")]
        [Description("Подсказка: Класс точности")]
        public double accuracyClass{ get; set; }         //	Класс точности	

        [Category("Водомер")]
        [DisplayName("Место установки водомера")]
        [Description("Подсказка: Координаты расположения водомера на схеме")]
        //нужен отдельный класс
        public Point coordinatesPointsC{ get; set; }     //	Координаты расположения водомера	

        [Category("Водомер")]
        [DisplayName("Фото счетчика")]
        [Description("Подсказка: Список адресов фотографий водомера")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, " +
            "Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, " +
            "Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        //нужен отдельный класс
        public List<string> addressListC { get; set; } = new List<string>();  //	Расположение фотографий счетчика	

        [Browsable(false)]
        [Category("Водомер")]
        [DisplayName("ID помещения")]
        [Description("Подсказка: ID Помещения в котором установлен водомер")]
        public string coordinatesRoomsID{ get; set; }    //	ID Помещения в котором уст. счетчик	

        [Category("Водомер")]
        [DisplayName("Показания водомера")]
        [Description("Подсказка: Таблица показаний водомера")]
        public List<RecordW> recordsList { get; set; } = new List<RecordW>();    // Показания водомеров

        public override string ToString()
        {
            return number.ToString();
        }
    }
}
