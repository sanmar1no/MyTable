using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTable
{
    class Client
    {
        public int roomsID{ get; set; }                //	ID Помещения
        public DateTime startDate{ get; set; }         //	Дата начала аренды
        public DateTime endDate{ get; set; }           //	Дата окончания аренды
        public string name{ get; set; }                //	Арендатор
        public string FIO{ get; set; }                 //	ФИО
        public string post{ get; set; }                //	Должность
        public string phoneNumber{ get; set; }         //	Телефон
        public string email{ get; set; }               //	Email
        public string info{ get; set; }                //	Доп. Информация
        public int workersAmount{ get; set; }          //	Кол-во сотрудников
        public string addressFolder{ get; set; }	   //   Папка арендатора

    }
}
