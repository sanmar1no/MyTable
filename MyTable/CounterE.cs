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
        public string ID;                    //	Уникальный номер		
        public string roomsID;               //	ID Помещения		
        public string number;                //	Номер счётчика		
        public string model;                 //	Модель (марка) счётчика		
        public DateTimeQ verificationYear;         //	Год поверки		
        public DateTimeQ madeYear;                 //	Год изготовления		
        public DateTime sealDate;            //	Дата опломбирования		
        public double ratio;                 //	Коэффициент учета		
        public List<string> sealList;        //	Список пломб на счётчике		
        public double accuracyClass;         //	Класс точности		
        public Point[] coordinatesPointsC;   //	Координаты расположения эл. счетчика		
        public List<string> addressListC;    //	Расположение фотографий счетчика		
        public string coordinatesRoomsID;    //	ID Помещения в котором уст. счетчик		
        public string substantionNo;         //	Номер ТП откуда подключен		
        public string substantionCabNo;      //	Номер СП откуда подключен		
        public string cableModel;            //	Марка кабеля ввода		
        public double cableLenght;           //	Длина кабеля до счетчика, м		
        public double power;                 //	Разрешенная мощность, кВт		
        public string switchType;            //	Тип отключающего устройства		
        public int switchValue;              //	Уставка In(А) вводного устройства	
        public List<Transformer> transformers; // Список трансформаторов	
        public List<RecordE> recordsList;      // Показания электросчетчиков
    }
}
