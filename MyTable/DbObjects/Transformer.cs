using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTable
{
    class Transformer
    {
        public int ID{ get; set; }                   //	Уникальный номер		
        public string electricCountersID{ get; set; }//    ID  Электросчётчика			
        public string numCA{ get; set; }             //	Номер тр-ра тока фазы "А"		
        public string numCB{ get; set; }             //	Номер тр-ра тока фазы "В"		
        public string numCC{ get; set; }             //	Номер тр-ра тока фазы "С"		
        public List<string> sealCA{ get; set; }      //	Пломбы тр-ра тока фазы "А"		
        public List<string> sealCB{ get; set; }      //	Пломбы тр-ра тока фазы "В"		
        public List<string> sealCC{ get; set; }      //	Пломбы тр-ра тока фазы "С"		
        public string modelCA{ get; set; }           //	Тип тр-ра тока фазы "А"		
        public string modelCB{ get; set; }           //	Тип тр-ра тока фазы "В"		
        public string modelCC{ get; set; }           //	Тип тр-ра тока фазы "С"		
        public double ratioC{ get; set; }            //	Коэффициент трансформации тока 		
        public double accuracyClassCA{ get; set; }   //	Класс точности тр-ра тока фазы "А"		
        public double accuracyClassCB{ get; set; }   //	Класс точности тр-ра тока фазы "В"		
        public double accuracyClassCC{ get; set; }   //	Класс точности тр-ра тока фазы "С"
        public DateTimeQ verificationYearCA{ get; set; }// год в/поверки ТТ ф.А
        public DateTimeQ verificationYearCB{ get; set; }// год в/поверки ТТ ф.B
        public DateTimeQ verificationYearCC{ get; set; }// год в/поверки ТТ ф.C
        public string numVA{ get; set; }             //	Номер тр-ра напр. фазы "А"		
        public string numVB{ get; set; }             //	Номер тр-ра напр. фазы "В"		
        public string numVC{ get; set; }             //	Номер тр-ра напр. фазы "С"		
        public List<string> sealVA{ get; set; }      //	Пломбы тр-ра напр. фазы "А"		
        public List<string> sealVB{ get; set; }      //	Пломбы тр-ра напр. фазы "В"		
        public List<string> sealVC{ get; set; }      //	Пломбы тр-ра напр. фазы "С"		
        public string modelVA{ get; set; }           //	Тип тр-ра напр. фазы "А"		
        public string modelVB{ get; set; }           //	Тип тр-ра напр. фазы "В"		
        public string modelVC{ get; set; }           //	Тип тр-ра напр. фазы "С"		
        public double ratioV{ get; set; }            //	Коэффициент трансформации напр. 		
        public double accuracyClassVA{ get; set; }   //	Класс точности тр-ра напр. фазы "А"		
        public double accuracyClassVB{ get; set; }   //	Класс точности тр-ра напр. фазы "В"		
        public double accuracyClassVC{ get; set; }   //	Класс точности тр-ра напр. фазы "С"	
        public DateTimeQ verificationYearVA{ get; set; }// год в/поверки ТH ф.А
        public DateTimeQ verificationYearVB{ get; set; }// год в/поверки ТH ф.B
        public DateTimeQ verificationYearVC{ get; set; }// год в/поверки ТH ф.C
    }
}
