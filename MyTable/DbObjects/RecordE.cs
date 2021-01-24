using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MyTable
{
    public class RecordE
    {
        [Browsable(false)]
        public int ID { get; set; }          //	Уникальный номер

        [Category("Расход электроснабжения")]
        [DisplayName("Дата показания")]
        [Description("Подсказка: Дата показания")]
        public DateTime date { get; set; }   // Дата показания

        [Category("Расход электроснабжения")]
        [DisplayName("Значение фактическое")]
        [Description("Подсказка: Значение фактическое, кВт*ч")]
        public double value { get; set; }    // Значение фактическое

        [Category("Расход электроснабжения")]
        [DisplayName("Коэфф-т трансформации")]
        [Description("Подсказка: Коэффициент трансформации")]
        public double ratio { get; set; }    //  Коэффициент трансформации
    }
}
