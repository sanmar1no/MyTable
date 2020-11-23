using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MyTable
{
    class Variables //класс в котором хранятся переменные проекта
    {
        //пользователи
        public enum UserKeyEnum
        {
            electro,
            aqua,
            arenda,
            admin
        }
        //Книга эксель
        public static IWorkbook workbook;
        public static ISheet sheet;
        // Создаём экземпляр области ячеек Excel
        public static IRow rowSheet;
        public static void newWorkbook()
        {
            workbook = new XSSFWorkbook();
            sheet = workbook.CreateSheet("Лист1");
            rowSheet = sheet.CreateRow(0);
            // return workbook;
        }
    }
}
