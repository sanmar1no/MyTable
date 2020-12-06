using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTable
{
    class Client
    {
        public string roomsID;             //	ID Помещения
        public DateTime startDate;         //	Дата начала аренды
        public DateTime endDate;           //	Дата окончания аренды
        public string name;                //	Арендатор
        public string FIO;                 //	ФИО
        public string post;                //	Должность
        public string phoneNumber;         //	Телефон
        public string email;               //	Email
        public string info;                //	Доп. Информация
        public int workersAmount;          //	Кол-во сотрудников
        public string client_folder;	   //   Папка арендатора

    }
}
