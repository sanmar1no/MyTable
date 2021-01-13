using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTable
{
    class RecordE
    {
        public int ID { get; set; }          //	Уникальный номер
        public DateTime date { get; set; }   // Дата показания
        public double value { get; set; }    // Значение фактическое
        public double ratio { get; set; }    //  Коэффициент трансформации
    }
}
