using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyTable
{
    class Room
    {
        public string building;             //	№ корпуса/адрес:улица
        public int floor;                   //	№ этажа
        public string room;                 //	№ помещения/номер дома+помещение
        public double roomArea;             //	Площадь помещения, кв. м
        public string addressPlan;          //	Расположение  dwg планировки
        public string addressCircuitPlan;   //	Расположение  плана электросети
        public string addressCircuitLine;   //	Расположение  однолинейной схемы
        public string addressCircuitWater;  //	Расположение  плана водоснабжения
        public string addressCircuitHeat;   //	Расположение  плана теплоснабжения
        public double roomVolume;           //	Объем помещения
        public double ratioHeat;            //	Коэффициент для расчета тепла
        public Point[] coordinatesPoints;   //	Координаты расположения помещения
        public List<CounterE> countersE;    //  Список электросчетчиков	
        public List<CounterW> countersW;    //  Список водомеров
        public List<Client> clientsList;        //  Список арендаторов/абонентов
    }
}
