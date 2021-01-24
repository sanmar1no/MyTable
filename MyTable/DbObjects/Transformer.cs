using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Drawing.Design;

namespace MyTable
{
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class Transformer
    {
        [Browsable(false)]
        public int ID{ get; set; }                   //	Уникальный номер	

        [Browsable(false)]
        public string electricCountersID{ get; set; }//    ID  Электросчётчика		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ ТТ фазы \"А\"")]
        [Description("Подсказка: Номер тр-ра тока фазы \"А\"")]
        public string numCA{ get; set; }             //	Номер тр-ра тока фазы "А"	

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ ТТ фазы \"В\"")]
        [Description("Подсказка: Номер тр-ра тока фазы \"В\"")]
        public string numCB{ get; set; }             //	Номер тр-ра тока фазы "В"	

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ ТТ фазы \"С\"")]
        [Description("Подсказка: Номер тр-ра тока фазы \"С\"")]
        public string numCC{ get; set; }             //	Номер тр-ра тока фазы "С"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Пломбы TT фазы \"A\"")]
        [Description("Подсказка: Пломбы тр-ра тока фазы \"A\"")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, " +
            "Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, " +
            "Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public List<string> sealCA { get; set; } = new List<string>();     //	Пломбы тр-ра тока фазы "А"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Пломбы TT фазы \"B\"")]
        [Description("Подсказка: Пломбы тр-ра тока фазы \"B\"")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, " +
            "Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, " +
            "Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public List<string> sealCB{ get; set; } = new List<string>();    //	Пломбы тр-ра тока фазы "В"	

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Пломбы TT фазы \"С\"")]
        [Description("Подсказка: Пломбы тр-ра тока фазы \"С\"")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, " +
            "Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, " +
            "Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public List<string> sealCC{ get; set; } = new List<string>();    //	Пломбы тр-ра тока фазы "С"	

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Тип тр-ра тока фазы \"А\"")]
        [Description("Подсказка: Тип трансформатора тока фазы \"А\"")]
        public string modelCA{ get; set; }           //	Тип тр-ра тока фазы "А"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Тип тр-ра тока фазы \"В\"")]
        [Description("Подсказка: Тип трансформатора тока фазы \"В\"")]
        public string modelCB{ get; set; }           //	Тип тр-ра тока фазы "В"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Тип тр-ра тока фазы \"С\"")]
        [Description("Подсказка: Тип трансформатора тока фазы \"С\"")]
        public string modelCC{ get; set; }           //	Тип тр-ра тока фазы "С"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Коэфф. трансформации ТТ")]
        [Description("Подсказка: Коэффициент трансформации трансформаторов тока")]
        public double ratioC{ get; set; }            //	Коэффициент трансформации тока 

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Класс точности ТТ ф. \"А\"")]
        [Description("Подсказка: Класс точности трансформатора тока фазы \"А\"")]
        public double accuracyClassCA{ get; set; }   //	Класс точности тр-ра тока фазы "А"	

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Класс точности ТТ ф. \"В\"")]
        [Description("Подсказка: Класс точности трансформатора тока фазы \"В\"")]
        public double accuracyClassCB{ get; set; }   //	Класс точности тр-ра тока фазы "В"	

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Класс точности ТТ ф. \"С\"")]
        [Description("Подсказка: Класс точности трансформатора тока фазы \"С\"")]
        public double accuracyClassCC{ get; set; }   //	Класс точности тр-ра тока фазы "С"

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Год в/поверки ТТ ф. \"А\"")]
        [Description("Подсказка: Год выпуска или поверки трансформатора тока фазы \"А\"")]
        [Editor(typeof(DataQEditor), typeof(UITypeEditor))]
        public DateTimeQ verificationYearCA{ get; set; } = new DateTimeQ();// год в/поверки ТТ ф.А

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Год в/поверки ТТ ф. \"В\"")]
        [Description("Подсказка: Год выпуска или поверки трансформатора тока фазы \"В\"")]
        [Editor(typeof(DataQEditor), typeof(UITypeEditor))]
        public DateTimeQ verificationYearCB{ get; set; } = new DateTimeQ();// год в/поверки ТТ ф.B

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Год в/поверки ТТ ф. \"С\"")]
        [Description("Подсказка: Год выпуска или поверки трансформатора тока фазы \"С\"")]
        [Editor(typeof(DataQEditor), typeof(UITypeEditor))]
        public DateTimeQ verificationYearCC { get; set; } = new DateTimeQ();// год в/поверки ТТ ф.C

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ ТН фазы \"А\"")]
        [Description("Подсказка: Номер трансформатора напряжения фазы \"А\"")]
        public string numVA{ get; set; }             //	Номер тр-ра напр. фазы "А"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ ТН фазы \"В\"")]
        [Description("Подсказка: Номер трансформатора напряжения фазы \"В\"")]
        public string numVB{ get; set; }             //	Номер тр-ра напр. фазы "В"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ ТН фазы \"С\"")]
        [Description("Подсказка: Номер трансформатора напряжения фазы \"С\"")]
        public string numVC{ get; set; }             //	Номер тр-ра напр. фазы "С"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Пломбы TН фазы \"А\"")]
        [Description("Подсказка: Пломбы трасформатора напряжения фазы \"А\"")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, " +
        "Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, " +
        "Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public List<string> sealVA{ get; set; } = new List<string>();   //  Пломбы тр-ра напр. фазы "А"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Пломбы TН фазы \"В\"")]
        [Description("Подсказка: Пломбы трасформатора напряжения фазы \"В\"")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, " +
        "Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, " +
        "Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public List<string> sealVB{ get; set; } = new List<string>();   //	Пломбы тр-ра напр. фазы "В"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Пломбы TН фазы \"С\"")]
        [Description("Подсказка: Пломбы трасформатора напряжения фазы \"С\"")]
        [Editor("System.Windows.Forms.Design.StringCollectionEditor, System.Design, " +
        "Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a",
        "System.Drawing.Design.UITypeEditor,System.Drawing, Version=2.0.0.0, " +
        "Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
        public List<string> sealVC{ get; set; } = new List<string>();   //	Пломбы тр-ра напр. фазы "С"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Тип ТН фазы \"А\"")]
        [Description("Подсказка: Тип трансформатора напряжения фазы \"А\"")]
        public string modelVA{ get; set; }           //	Тип тр-ра напр. фазы "А"	

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Тип ТН фазы \"В\"")]
        [Description("Подсказка: Тип трансформатора напряжения фазы \"В\"")]
        public string modelVB{ get; set; }           //	Тип тр-ра напр. фазы "В"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("№ Тип ТН фазы \"С\"")]
        [Description("Подсказка: Тип трансформатора напряжения фазы \"С\"")]
        public string modelVC{ get; set; }           //	Тип тр-ра напр. фазы "С"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Коэфф. трансформации ТН")]
        [Description("Подсказка: Коэффициент трансформации трансформаторов напряжения")]
        public double ratioV{ get; set; }            //	Коэффициент трансформации напр. 	

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Класс точности ТН ф. \"А\"")]
        [Description("Подсказка: Класс точности трансформатора напряжения фазы \"А\"")]
        public double accuracyClassVA{ get; set; }   //	Класс точности тр-ра напр. фазы "А"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Класс точности ТН ф. \"В\"")]
        [Description("Подсказка: Класс точности трансформатора напряжения фазы \"В\"")]
        public double accuracyClassVB{ get; set; }   //	Класс точности тр-ра напр. фазы "В"		

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Класс точности ТН ф. \"С\"")]
        [Description("Подсказка: Класс точности трансформатора напряжения фазы \"С\"")]
        public double accuracyClassVC{ get; set; }   //	Класс точности тр-ра напр. фазы "С"	

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Год в/поверки ТН ф. \"А\"")]
        [Description("Подсказка: Год выпуска или поверки трансформатора напряжения фазы \"А\"")]
        [Editor(typeof(DataQEditor), typeof(UITypeEditor))]
        public DateTimeQ verificationYearVA{ get; set; } = new DateTimeQ();// год в/поверки ТH ф.А

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Год в/поверки ТН ф. \"В\"")]
        [Description("Подсказка: Год выпуска или поверки трансформатора напряжения фазы \"В\"")]
        [Editor(typeof(DataQEditor), typeof(UITypeEditor))]
        public DateTimeQ verificationYearVB{ get; set; } = new DateTimeQ();// год в/поверки ТH ф.B

        [Category("Трансформаторы тока и напряжения")]
        [DisplayName("Год в/поверки ТН ф. \"В\"")]
        [Description("Подсказка: Год выпуска или поверки трансформатора напряжения фазы \"В\"")]
        [Editor(typeof(DataQEditor), typeof(UITypeEditor))]
        public DateTimeQ verificationYearVC{ get; set; } = new DateTimeQ();// год в/поверки ТH ф.C

        public override string ToString()
        {
            return ratioC.ToString()+" ; "+ratioV.ToString();
        }
    }
}
