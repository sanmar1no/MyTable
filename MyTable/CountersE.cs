using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace MyTable
{
    class CountersE
    {
        string ID;                    //	Уникальный номер
        string roomsID;               //	ID Помещения
        string number;                //	Номер счётчика
        string model;                 //	Модель (марка) счётчика
        DateTime verificationYear;    //	Год поверки
        DateTime madeYear;            //	Год изготовления
        DateTime sealDate;            //	Дата опломбирования
        double ratio;                 //	Коэффициент учета
        List<string> sealList;        //	Список пломб на счётчике
        double accuracyClass;         //	Класс точности
        Point[] coordinatesPointsC;   //	Координаты расположения эл. счетчика
        List<string> addressListC;    //	Расположение фотографий счетчика
        string coordinatesRoomsID;    //	ID Помещения в котором уст. счетчик
        string substantionNo;         //	Номер ТП откуда подключен
        string substantionCabNo;      //	Номер СП откуда подключен
        string cableModel;            //	Марка кабеля ввода
        double cableLenght;           //	Длина кабеля до счетчика, м
        double power;                 //	Разрешенная мощность, кВт
        string switchType;            //	Тип отключающего устройства
        int switchValue;	          //	Уставка In(А) вводного устройства

    }
}
