using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MyTable
{
    public class RecordW
    {
        [Browsable(false)]
        public int ID { get; set; }               //	Уникальный номер

        [Category("Расход водоснабжения")]
        [DisplayName("Дата показания")]
        [Description("Подсказка: Дата показания")]
        public DateTime date { get; set; }        // Дата показания

        [Category("Расход водоснабжения")]
        [DisplayName("Значение фактическое")]
        [Description("Подсказка: Значение фактическое, куб.м.")]
        public double value { get; set; }         // Значение фактическое

        [Category("Расход водоснабжения")]
        [DisplayName("Коэфф-т трансформации")]
        [Description("Подсказка: Коэффициент трансформации")]
        public double ratio { get; set; }         //  Коэффициент трансформации

        [Category("Расход водоснабжения")]
        [DisplayName("Кол-во сотрудников")]
        [Description("Подсказка: Количество сотрудников")]
        public int workersAmount { get; set; }    //  Кол-во сотрудников

    }
}
