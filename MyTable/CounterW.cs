using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyTable
{
    class CounterW
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
        public Point[] coordinatesPointsC;   //	Координаты расположения водомера	
        public List<string> addressListC;    //	Расположение фотографий счетчика	
        public string coordinatesRoomsID;    //	ID Помещения в котором уст. счетчик	
    }
}
