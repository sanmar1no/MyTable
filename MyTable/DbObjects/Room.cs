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
        public int ID { get; set; }                     //	Уникальный номер
        public string building{ get; set; }             //	№ корпуса/адрес:улица
        public int floor{ get; set; }                   //	№ этажа
        public string room{ get; set; }                 //	№ помещения/номер дома+помещение
        public double roomArea{ get; set; }             //	Площадь помещения, кв. м
        public string addressPlan{ get; set; }          //	Расположение  dwg планировки
        public string addressCircuitPlan{ get; set; }   //	Расположение  плана электросети
        public string addressCircuitLine{ get; set; }   //	Расположение  однолинейной схемы
        public string addressCircuitWater{ get; set; }  //	Расположение  плана водоснабжения
        public string addressCircuitHeat{ get; set; }   //	Расположение  плана теплоснабжения
        public double roomVolume{ get; set; }           //	Объем помещения
        public double ratioHeat{ get; set; }            //	Коэффициент для расчета тепла
        public Point[] coordinatesPoints{ get; set; }   //	Координаты расположения помещения
        public List<CounterE> countersE{ get; set; }    //  Список электросчетчиков	
        public List<CounterW> countersW{ get; set; }    //  Список водомеров
        public List<Client> clientsList{ get; set; }        //  Список арендаторов/абонентов

        public string getStr() {
            return $"id: {ID}, " +
                $"корп: {building}, " +
                $"этаж: {floor}, " +
                $"пом: {room}, " +
                $"площ.: {roomArea}, " +
                $"объем.: {roomVolume}, " +
                $"коэфф.: {ratioHeat}" +
                $"\r\n";
        }


    }
}
