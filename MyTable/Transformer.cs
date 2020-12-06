using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTable
{
    class Transformer
    {
        public string ID;                //	Уникальный номер		
        public string electricCountersID;//    ID  Электросчётчика			
        public string numCA;             //	Номер тр-ра тока фазы "А"		
        public string numCB;             //	Номер тр-ра тока фазы "В"		
        public string numCC;             //	Номер тр-ра тока фазы "С"		
        public List<string> sealCA;      //	Пломбы тр-ра тока фазы "А"		
        public List<string> sealCB;      //	Пломбы тр-ра тока фазы "В"		
        public List<string> sealCC;      //	Пломбы тр-ра тока фазы "С"		
        public string modelCA;           //	Тип тр-ра тока фазы "А"		
        public string modelCB;           //	Тип тр-ра тока фазы "В"		
        public string modelCC;           //	Тип тр-ра тока фазы "С"		
        public double ratioC;            //	Коэффициент трансформации тока 		
        public double accuracyClassCA;   //	Класс точности тр-ра тока фазы "А"		
        public double accuracyClassCB;   //	Класс точности тр-ра тока фазы "В"		
        public double accuracyClassCC;   //	Класс точности тр-ра тока фазы "С"		
        public string numVA;             //	Номер тр-ра напр. фазы "А"		
        public string numVB;             //	Номер тр-ра напр. фазы "В"		
        public string numVC;             //	Номер тр-ра напр. фазы "С"		
        public List<string> sealVA;      //	Пломбы тр-ра напр. фазы "А"		
        public List<string> sealVB;      //	Пломбы тр-ра напр. фазы "В"		
        public List<string> sealVC;      //	Пломбы тр-ра напр. фазы "С"		
        public string modelVA;           //	Тип тр-ра напр. фазы "А"		
        public string modelVB;           //	Тип тр-ра напр. фазы "В"		
        public string modelVC;           //	Тип тр-ра напр. фазы "С"		
        public double ratioV;            //	Коэффициент трансформации напр. 		
        public double accuracyClassVA;   //	Класс точности тр-ра напр. фазы "А"		
        public double accuracyClassVB;   //	Класс точности тр-ра напр. фазы "В"		
        public double accuracyClassVC;	 //	Класс точности тр-ра напр. фазы "С"	


    }
}
