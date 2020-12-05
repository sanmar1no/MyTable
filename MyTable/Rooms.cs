using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyTable
{
    class Rooms
    {
        string building;	        //	№ корпуса/адрес:улица
        string floor;	            //	№ этажа
        string room;	            //	№ помещения/номер дома+помещение
        double roomArea;	        //	Площадь помещения, кв. м
        string addressPlan;	        //	Расположение  dwg планировки
        string addressCircuitPlan;	//	Расположение  плана электросети
        string addressCircuitLine;	//	Расположение  однолинейной схемы
        string addressCircuitWater;	//	Расположение  плана водоснабжения
        string addressCircuitHeat;	//	Расположение  плана теплоснабжения
        double roomVolume;	        //	Объем помещения
        double ratioHeat;	        //	Коэффициент для расчета тепла
        Point[] coordinatesPoints;	//	Координаты расположения помещения
    }
}
