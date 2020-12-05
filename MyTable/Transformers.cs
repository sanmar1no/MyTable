using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTable
{
    class Transformers
    {
        string ID;                //	Уникальный номер
        string electricCountersID;//    ID  Электросчётчика
        string numCA;             //	Номер тр-ра тока фазы "А"
        string numCB;             //	Номер тр-ра тока фазы "В"
        string numCC;             //	Номер тр-ра тока фазы "С"
        List<string> sealCA;      //	Пломбы тр-ра тока фазы "А"
        List<string> sealCB;      //	Пломбы тр-ра тока фазы "В"
        List<string> sealCC;      //	Пломбы тр-ра тока фазы "С"
        string modelCA;           //	Тип тр-ра тока фазы "А"
        string modelCB;           //	Тип тр-ра тока фазы "В"
        string modelCC;           //	Тип тр-ра тока фазы "С"
        double ratioC;            //	Коэффициент трансформации тока 
        double accuracyClassCA;   //	Класс точности тр-ра тока фазы "А"
        double accuracyClassCB;   //	Класс точности тр-ра тока фазы "В"
        double accuracyClassCC;   //	Класс точности тр-ра тока фазы "С"
        string numVA;             //	Номер тр-ра напр. фазы "А"
        string numVB;             //	Номер тр-ра напр. фазы "В"
        string numVC;             //	Номер тр-ра напр. фазы "С"
        List<string> sealVA;      //	Пломбы тр-ра напр. фазы "А"
        List<string> sealVB;      //	Пломбы тр-ра напр. фазы "В"
        List<string> sealVC;      //	Пломбы тр-ра напр. фазы "С"
        string modelVA;           //	Тип тр-ра напр. фазы "А"
        string modelVB;           //	Тип тр-ра напр. фазы "В"
        string modelVC;           //	Тип тр-ра напр. фазы "С"
        double ratioV;            //	Коэффициент трансформации напр. 
        double accuracyClassVA;   //	Класс точности тр-ра напр. фазы "А"
        double accuracyClassVB;   //	Класс точности тр-ра напр. фазы "В"
        double accuracyClassVC;	  //	Класс точности тр-ра напр. фазы "С"

    }
}
