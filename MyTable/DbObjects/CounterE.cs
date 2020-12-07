using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyTable
{
    class CounterE
    {
        public int ID{ get; set; }                       //	Уникальный номер		
        public string roomsID{ get; set; }               //	ID Помещения		
        public string number{ get; set; }                //	Номер счётчика		
        public string model{ get; set; }                 //	Модель (марка) счётчика		
        public DateTimeQ verificationYear{ get; set; }   //	Год поверки		
        public DateTimeQ madeYear{ get; set; }           //	Год изготовления		
        public DateTime sealDate{ get; set; }            //	Дата опломбирования		
        public double ratio{ get; set; }                 //	Коэффициент учета		
        public List<string> sealList{ get; set; }        //	Список пломб на счётчике		
        public double accuracyClass{ get; set; }         //	Класс точности		
        public Point[] coordinatesPointsC{ get; set; }   //	Координаты расположения эл. счетчика		
        public List<string> addressListC{ get; set; }    //	Расположение фотографий счетчика		
        public string coordinatesRoomsID{ get; set; }    //	ID Помещения в котором уст. счетчик		
        public string substantionNo{ get; set; }         //	Номер ТП откуда подключен		
        public string substantionCabNo{ get; set; }      //	Номер СП откуда подключен		
        public string cableModel{ get; set; }            //	Марка кабеля ввода		
        public double cableLenght{ get; set; }           //	Длина кабеля до счетчика, м		
        public double power{ get; set; }                 //	Разрешенная мощность, кВт		
        public string switchType{ get; set; }            //	Тип отключающего устройства		
        public int switchValue{ get; set; }              //	Уставка In(А) вводного устройства	
        public List<Transformer> transformers{ get; set; } // Список трансформаторов	
        public List<RecordE> recordsList{ get; set; }      // Показания электросчетчиков
    }
}
