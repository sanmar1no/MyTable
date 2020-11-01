using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel1 = Microsoft.Office.Interop.Excel;


namespace MyTable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            foreach (string s in System.Environment.GetCommandLineArgs())
            {
                UserKey = s;
            }
        }
        int numRoomTemp = -1;
        int scale = 1;
        int scalekX = 1;
        int scalekY = 1;
        string UserKey = "electro";//"arenda" "electro" "voda" ""

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (g1)
            {
                pictureBox1.Location = new Point(Cursor.Position.X - cur.X + curnew.X, Cursor.Position.Y - cur.Y + curnew.Y);
            }
            else
            {
                if (g3)
                {
                    double X = (Cursor.Position.X - this.Location.X - pictureBox1.Location.X - 9) * 20 / scale;
                    double Y = (Cursor.Position.Y - this.Location.Y - pictureBox1.Location.Y - 37) * 20 / scale;
                    Point[] figura = new Point[1];
                    int numRoomTemp2 = equationSystem(new Point((int)X, (int)Y), out figura);
                    if (numRoomTemp != numRoomTemp2) pictureBox1.Load(@"Этаж" + (floorGlobal + 1).ToString() + ".png");
                    if (numRoomTemp2 >= 0)
                    {
                        label3.Text = "true";
                        bitmap = new Bitmap(pictureBox1.Image);
                        g = Graphics.FromImage(bitmap);
                        //g.DrawLine(new Pen(Color.Green, 5), new Point(int.Parse(textBox2.Text), int.Parse(textBox3.Text)), new Point(int.Parse(textBox2.Text) + 100, int.Parse(textBox3.Text)));
                        g.DrawPolygon(new Pen(Color.Green, 10), figura);
                        pictureBox1.Image.Dispose();
                        pictureBox1.Image = bitmap;
                        g.Dispose();
                    }
                    else
                    {
                        label3.Text = "false";
                        pictureBox1.Load(@"Этаж" + (floorGlobal + 1).ToString() + ".png");
                    }//*/
                }
            }
        }
  
        List<string> File = new List<string>();
        private void pictureBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            double x = Cursor.Position.X - this.Location.X - 9 - pictureBox1.Location.X;
            double y = Cursor.Position.Y - this.Location.Y - 37 - pictureBox1.Location.Y;
            double xpos = pictureBox1.Location.X;
            double ypos = pictureBox1.Location.Y;
            if (scale != 0)
            {
                x = (x * 20 / scale);
                y = (y * 20 / scale);
            }
            else
            {
                x = 0;
                y = 0;
            }

           // double x = ((cur.X - 9 - pictureBox1.Location.X - this.Location.X) * 20 / scale);
           // double y = ((cur.Y - 37 - pictureBox1.Location.Y - this.Location.Y) * 20 / scale);
            if (e.Delta > 0)
            {
                scale += 5;
                pictureBox1.Width = scalekX * scale;
                pictureBox1.Height = scalekY * scale;
                pictureBox1.Location = new Point((int)xpos - (int)x/4, (int)ypos - (int)y/4);//4 -коэффициент увеличения(20/5) 5= scale
            }
            else
            {
                if(scale!=5)scale -= 5;
                pictureBox1.Width = scalekX * scale;
                pictureBox1.Height = scalekY * scale;
                pictureBox1.Location = new Point((int)xpos + (int)x/4, (int)ypos + (int)y/4);
            }

            //new Point(pictureBox1.Location.X - (int)x / 2, pictureBox1.Location.Y - (int)y / 2); 
            curnew = pictureBox1.Location;              
            label39.Text = "Размер картинки x=" + pictureBox1.Size.Width + " y=" + pictureBox1.Size.Height + "skale=" + scale;
        }    ///скролинг мышкой
        Bitmap bitmap;
        Graphics g;
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (GlobalP ==21)
            {
                double X = (Cursor.Position.X - this.Location.X - pictureBox1.Location.X - 9) * 20 / scale;
                double Y = (Cursor.Position.Y - this.Location.Y - pictureBox1.Location.Y - 37) * 20 / scale;
                
                if (g3)
                {
                    int pom1 = equationSystem(new Point((int)X, (int)Y), out figa1);
                    if (pom1 > -1)
                    {
                        if (tabControl1.SelectedIndex != 0) tabControl1.SelectedIndex = 0;
                        comboBox5.Text=data[floorGlobal, 0, pom1];//корпус
                        comboBox6.Text=data[floorGlobal, 1, pom1];//помещение
                        timer1.Enabled = true;
                    }
                    else timer1.Enabled = false;
                }
            }
        }
        public Point cur, curnew;
        bool g1 = false;
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox1.Focus();
            cur = Cursor.Position;
            g1 = true;
            double x = ((cur.X - 9 - pictureBox1.Location.X - this.Location.X) * 20 / scale);
            double y = ((cur.Y - 37 - pictureBox1.Location.Y - this.Location.Y) * 20 / scale);

            if (GlobalP < 20)
            {
                richTextBox1.Text += "scale= \r\n" + scale.ToString() + "\r\n";
                richTextBox1.Text += "Cursor (cur)= \r\n" + cur.X.ToString() + "\r\n" + cur.Y.ToString() + "\r\n";
                richTextBox1.Text += "PictureBox1.Location= \r\n" + pictureBox1.Location.X.ToString() + "\r\n" + pictureBox1.Location.Y.ToString() + "\r\n";
                richTextBox1.Text += "form location= \r\n" + this.Location.X.ToString() + "\r\n" + this.Location.Y.ToString() + "\r\n";
                richTextBox1.Text += "x= " + x.ToString() + "y= " + y.ToString() + "\r\n";
                poligon1[GlobalP] = new Point((int)x, (int)y);

                bitmap = new Bitmap(pictureBox1.Image);
                g = Graphics.FromImage(bitmap);
                g.FillEllipse(Brushes.Green, poligon1[GlobalP].X, poligon1[GlobalP].Y, 4, 4);
                pictureBox1.Image.Dispose();
                pictureBox1.Image = bitmap;
                g.Dispose();

                GlobalP++;
            }

        }
        void LoadCB()//прогрузка данных по этажу:
        {
            //List<string> Arend1 = new List<string>();//арендатор
            List<string> data1 = new List<string>();//корпус
            for (int et = 0; et < 4;et++ )
                for (int i = 0; i < maxRoom; i++)
                {
                   // if (arenda[0, et, 1, i] != null) Arend1.Add(arenda[0, et, 1, i]);
                    if(floorGlobal==et)if (data[floorGlobal, 0, i] != null) data1.Add(data[floorGlobal, 0, i]);
                }
            comboBox1.Items.Clear();
            //Arend1.Sort();
            comboBox1.Items.AddRange(ArendaLong("ToLongName").ToArray());
            comboBox5.Items.Clear();
            data1.Sort();
            comboBox5.Items.AddRange(data1.Distinct().ToArray());
        }
        void LoadCB2() //прогрузка боксов со справочными данными
        {
            List<string> data2 = new List<string>();//марка кабеля
            List<string> data3 = new List<string>();//тип отключающего устройства
            List<string> data4 = new List<string>();//марка электросчетчика
            List<string> data5 = new List<string>();//марка водомера
            for (int et = 0; et < 4; et++)
            {
                for (int i = 0; i < maxRoom; i++)
                {
                    if (data[et, 4, i] != null) data2.Add(data[et, 4, i]);
                    if (data[et, 7, i] != null) data3.Add(data[et, 7, i]);
                    if (data[et, 10, i] != null) data4.Add(data[et, 10, i]);
                    if (data[et, 13, i] != null) data5.Add(data[et, 13, i]);
                }
            }
            comboBox9.Items.Clear();
            data2.Sort();
            comboBox9.Items.AddRange(data2.Distinct().ToArray());
            comboBox12.Items.Clear();
            data3.Sort();
            comboBox12.Items.AddRange(data3.Distinct().ToArray());
            comboBox15.Items.Clear();
            data4.Sort();
            comboBox15.Items.AddRange(data4.Distinct().ToArray());
            comboBox17.Items.Clear();
            data5.Sort();
            comboBox17.Items.AddRange(data5.Distinct().ToArray());
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            g1 = false;
            curnew = pictureBox1.Location;
        }
        bool tim = false;
        private void timer1_Tick(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Image);
            g = Graphics.FromImage(bitmap);
            g.DrawPolygon(new Pen(Color.Green, 5), figa1);
            if (tim == false)
            {
                g.FillPolygon(new SolidBrush(Color.DarkOrange), figa1);
                tim = true;
            }
            else
            {
                g.FillPolygon(new SolidBrush(Color.DimGray), figa1);
                tim = false;
            }
            pictureBox1.Image.Dispose();
            pictureBox1.Image = bitmap;
            g.Dispose();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            double x = Cursor.Position.X - this.Location.X - 9 - pictureBox1.Location.X;
            double y = Cursor.Position.Y - this.Location.Y - 37 - pictureBox1.Location.Y;
            label2.Text = "x=" + x + ";" + " y=" + y + ";";
            x = (x * 20 / scale);
            y = (y * 20 / scale);
            label1.Text = "x=" + x + ";" + " y=" + y + ";";
            timer2.Enabled = true;
        }
        private string DobavitRazdeliteli(string stroka,int Kol_vo)
        {
            int i = 0;
            string temp = stroka;
            for (; i < Kol_vo; i++)
            {
                int findRazdelitel = temp.IndexOf(";");
                if (findRazdelitel > -1)
                {
                    if (temp.Length > 1) temp = temp.Substring(findRazdelitel + 1);
                    else
                    {
                        i++;
                        break;
                    }
                }
                else break;
            }
            Kol_vo -= i;
            for (; Kol_vo > 0; Kol_vo--)
            {
                stroka += ";";
            }
            return stroka;
        }
        private void CorrectDB()
        {
            /*
[etaz_1]206
[0]
1317;310;1316;317;1304;319;1301;337;1301;358;1321;360;1326;363;1330;310
1;151;ТП-887;;;;10;;;014105;СА4У-510;;;;;40;;;;;22.04.2020;;;;;22.04.2020;;;;//29шт
22.04.2020;ООО "РИП-Импульс";;;;;//4+2шт
22.04.2020;ООО "РИП-Импульс";;;;;//4+2шт
[pokazanie]
25.05.2020;41644;;;;//2+3шт
01.05.2020;40910;;;;//2+3шт
[1]
 */
            int schetchik = 0;
            bool flag_pokazanie = false;
            for (int i = 1; i < File.Count; i++)
            {
                int PomOnEt = 0;
                if (File[i].IndexOf("[etaz_") > -1)
                {
                    PomOnEt = int.Parse(File[i].Substring(8, File[i].Length - 8)) - 1;
                    schetchik = 0;
                    i++;
                }                
                if (File[i] == "[" + schetchik + "]")
                {
                    schetchik++;
                    flag_pokazanie = false;
                    i++;
                    i++;
                    //29
                    File[i] = DobavitRazdeliteli(File[i], 29);//data
                    i++;
                }
                if (flag_pokazanie) File[i] = DobavitRazdeliteli(File[i], 5);//counters
                if (File[i] != "[pokazanie]" && flag_pokazanie==false)
                {
                    File[i] = DobavitRazdeliteli(File[i], 6);//arendator's
                }
                else
                {
                    flag_pokazanie = true;
                }
                
            }
        }

        int[] countRoom = new int[4];
        int[, ,] koord = new int[4, 40, 1];
        const int RMD = 40;//размер таблицы data
        string[, ,] data = new string[4, RMD, 1];
        const int RMA = 8;
        string[, , ,] arenda = new string[10, 4, RMA, 1];
        const int RMC = 16;//размер таблицы счетчиков
        string[, , ,] counters = new string[60, 4, RMC, 1];
        int floorGlobal = 0;//текущий этаж
        int roomGlobal = 0;//текущее помещение
        int maxRoom = 300;
        string[] modData = new string[RMD];
        string[] modCounters = new string[RMC];
        string[] modArenda = new string[RMA];
        string[] dataMod = { "", "" };//[1] - ключ electro или voda
        string dataModA = "";//дата измененная для таблицы арендаторов (расширение на перспективу. пока не реализована таблица на форме)
        string ToData(int floor, int j, int room, string s)
        {
            if (floor == floorGlobal && room == roomGlobal)
            {
                if (modData[j] != null)
                {
                    return modData[j];
                }
                else return s;
            }
            else return s;
        }
        void LoadDB() //основная функция загрузки с раздельным внесением информации
        {
            
            int room = 0;
            int floor = 0;
            //int PomeshenieM = int.Parse(File[0]);            
            for (int i = 0; i < File.Count; i++)
            {
                if (File[i].IndexOf("[etaz_") > -1)
                {
                    countRoom[floor] = int.Parse(File[i].Substring(8, File[i].Length - 8)) - 1;//количество помещений на этаже
                    //if (countRoom[floor] > maxRoom) maxRoom = countRoom[floor];
                    floor++;
                }
            }
            floor = 0;
            for (int i = 0; i < File.Count; i++)
            {
                if (File[i].IndexOf("[etaz_") > -1)
                {
                    floor = int.Parse(File[i].Substring(6, 1)) - 1;//номер этажа
                    room = 0;
                }
                if (File[i] == "[" + room + "]")
                {
                    i++;
                    string s = File[i];
                    if (File[i] != "=no koord=")
                    {
                        for (int j = 0; j < 40; j++)
                        {
                            if (s.IndexOf(";") > -1)
                            {
                                koord[floor, j, room] = int.Parse(s.Substring(0, s.IndexOf(";")));
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                koord[floor, j, room] = int.Parse(s);
                                break;
                            }
                        }
                    }
                    i++;
                    if (i >= File.Count()) break;
                    s = File[i];
                    for (int j = 0; j < RMD; j++)
                    {
                        if (s.IndexOf(";") > -1)
                        {
                           // if (s.IndexOf(";") != 0)
                           // {
                                data[floor, j, room] = ToData(floor, j, room, s.Substring(0, s.IndexOf(";")));
                           // }
                            s = s.Substring(s.IndexOf(";") + 1);
                        }
                        else
                        {
                            data[floor, j, room] = ToData(floor, j, room, s);
                            break;
                        }
                    }
                    for (int k = 0; k < 10; k++)
                    {
                        i++;
                        if (i >= File.Count()) break;
                        s = File[i];
                        if (s == "[pokazanie]")
                        {
                            //  i++;
                            break;
                        }
                        for (int j = 0; j < RMA; j++)
                        {
                            if (s.IndexOf(";") > -1)
                            {
                                if (s.IndexOf(";") != 0) arenda[k, floor, j, room] = s.Substring(0, s.IndexOf(";"));
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                arenda[k, floor, j, room] = s;
                                break;
                            }
                        }
                    }
                    for (int k = 0; k < 60; k++)
                    {//прогружаем все счетчики как обычно
                        i++;
                        if (i >= File.Count()) break;
                        s = File[i];
                        if (s.Substring(0, 1) == "[" || s == "=no koord=") break;
                        for (int j = 0; j < RMC; j++)
                        {
                            if (s.IndexOf(";") > -1)
                            {
                                if (s.IndexOf(";") != 0)
                                {
                                    counters[k, floor, j, room] = s.Substring(0, s.IndexOf(";"));
                                }
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                counters[k, floor, j, room] = s;
                                break;
                            }
                        }
                    }

                    room++;
                    i--;
                }
            }
            /*
            for (int floor = 0; floor < 4; floor++)
            {
                for (int numRoom = 0; numRoom < maxRoom; numRoom++)
                { 
                
                }
            }*/
            toCounters(floorGlobal, roomGlobal);//добавить функцию изменения счетчика
            toArenda(floorGlobal, roomGlobal);//добавить функцию изменения арендатора

            modData = new string[RMD];
            modCounters = new string[RMC];
            modArenda = new string[RMA];
            dataMod[0] = null;
        }
        void SaveDB()
        {
            File.Clear();
            File.Add((countRoom[0] + countRoom[1] + countRoom[2] + countRoom[3] + 4).ToString());//записали общее количество помещений в начало
            for (int floor = 0; floor < 4; floor++)
            {
                File.Add("[etaz_" + (floor + 1).ToString() + "]" + (countRoom[floor] + 1).ToString());//запись номера этажа
                for (int numRoom = 0; numRoom <= countRoom[floor]; numRoom++)
                {
                    File.Add("[" + numRoom + "]");//запись номера помещения
                    string s = "";
                    for (int i = 0; i < 40; i++)
                    {
                        if (koord[floor, i, numRoom] == 0) break;
                        else
                        {
                            s += koord[floor, i, numRoom] + ";";
                        }
                    }
                    if (s != "") File.Add(s.Substring(0, s.Length - 1));//записали координаты
                    else File.Add("=no koord=");
                    s = "";
                    for (int i = 0; i < RMD; i++) s += data[floor, i, numRoom] + ";";
                    if (s != "") File.Add(s.Substring(0, s.Length - 1));//записали данные помещения
                    s = "";
                    for (int k = 0; k < 10; k++)
                    {
                        if (arenda[k, floor, 0, numRoom] == null) break;
                        s = "";
                        for (int i = 0; i < RMA; i++) s += arenda[k, floor, i, numRoom] + ";";
                        if (s != "") File.Add(s.Substring(0, s.Length - 1));//записали реквизиты арендатора
                    }
                    File.Add("[pokazanie]");
                    s = "";
                    for (int k = 0; k < 60; k++)
                    {
                        if (counters[k, floor, 0, numRoom] == null) break;
                        s = "";
                        for (int i = 0; i < RMC; i++)
                        {
                            s += counters[k, floor, i, numRoom] + ";";
                        }
                        File.Add(s.Substring(0, s.Length - 1));//записали строку счетчиков
                    }
                    s = "";
                }
            }
            //конец основного кода
            System.IO.File.WriteAllLines(@"Data.txt", File, Encoding.Default);
            System.IO.File.WriteAllLines(@DateTime.Now.ToShortDateString() + ".txt", File, Encoding.Default);//резервная копия (на конец дня)
        }
        void  addRowToMassiv(int floor, int numroom, int row)//записать в массив counters строку modCounters, освободив для нее место в указанной позиции row
        {//floor - номер этажа, numroom - номер помещения
            //1. Освободим строку row
            for (int i = 59; i > row; i--) //row не может быть меньше нуля
            {
                if (counters[i-1, floor, 0, numroom] != null)
                {
                    for (int j = 0; j < RMC; j++)
                    {
                        counters[i, floor, j, numroom] = counters[i - 1, floor, j, numroom];
                    }
                }
            }
            //2. запишем в строку row значения
            writeStrToMass(floor, numroom, row);
        }
        void addRowToMassivA(int floor, int numroom, int row)//записать в массив Arenda строку modArenda, освободив для нее место в указанной позиции row
        {//floor - номер этажа, numroom - номер помещения
            //1. Освободим строку row
            for (int i = 9; i > row; i--) //row не может быть меньше нуля
            {
                if (arenda[i - 1, floor, 0, numroom] != null)
                {
                    for (int j = 0; j < RMA; j++)
                    {
                        arenda[i, floor, j, numroom] = arenda[i - 1, floor, j, numroom];
                    }
                }
            }
            //2. запишем в строку row значения
            writeStrToMassA(floor, numroom, row);
        }
        void writeStrToMass(int floor, int numroom, int row)
        {//запишем в строку row значения массива с измененными значениями.
            for (int j = 0; j < RMC; j++)
            {//заменим элемент массива (только тот, который не изменился)
                if (modCounters[j] != null) counters[row, floor, j, numroom] = modCounters[j];
            }//row - строка, которую перезапишем строкой либо modCounters[j] либо соседней(counters[row+-, floor, j, numroom]), если дата за диапазоном.
            //добавим сюда функцию расчета расхода по воде-электричеству...
            RasxodFull(floor, numroom, DateTime.Parse(counters[row, floor, 0, numroom]));//вопрос, нужно ли проверить заполнение данных по электроэнергии? или это расчет по воде?
        }

        void writeStrToMassA(int floor, int numroom, int row)
        {//запишем в строку row значения массива с измененными значениями.
            for (int j = 0; j < RMA; j++)
            {//заменим элемент массива (только тот, который не изменился)
                if (modArenda[j] != null) arenda[row, floor, j, numroom] = modArenda[j];
            }//row - строка, которую перезапишем строкой либо modArenda[j] либо соседней(arenda[row+-, floor, j, numroom]), если дата за диапазоном.
        }
        bool removeRowInMassiv(int floor, int numroom, int row)//удалить строку row в таблице счетчиков
        {//floor - номер этажа, numroom - номер помещения
            if ((modCounters[1] == null && dataMod[1] == "voda") || (modCounters[8] == null && dataMod[1] == "electro"))
            {
                //1. удалим строку row
                for (; row < 59; row++)
                {
                    if (counters[row, floor, 0, numroom] != null)
                    {
                        for (int j = 0; j < RMC; j++)
                        {
                            counters[row, floor, j, numroom] = counters[row + 1, floor, j, numroom];
                        }
                    }
                    else break;
                }
                clearRowKey(floor, numroom, row);//очистим только часть строки с учетом ключа
                return true;
            }
            else 
            {
                clearRowKey(floor, numroom, row);//очистим только часть строки с учетом ключа
                return false;
            }            
        }

        bool removeRowInMassivA(int floor, int numroom, int row)//удалить строку row в таблице арендаторов
        {//floor - номер этажа, numroom - номер помещения
            if (modArenda[1] == "")
            {
                //1. удалим строку row
                for (; row < 9; row++)
                {
                    if (arenda[row, floor, 0, numroom] != null)
                    {
                        for (int j = 0; j < RMA; j++)
                        {
                            arenda[row, floor, j, numroom] = arenda[row + 1, floor, j, numroom];
                        }
                    }
                    else break;
                }
                clearRowA(floor, numroom, row);//очистим строку
                return true;
            }
            else
            {
                return false;
            }
        }
        void writeRowKey(int etaz, int schetchik, int k)
        {
            if (dataMod[1] == "electro")//запись с учетом ключа
            {                        //э 1,3,4,6,11,12,13,14,15
                if (modCounters[0] != null) counters[k, etaz, 0, schetchik] = modCounters[0];//k-1 ошибка?
                if (modCounters[1] != null) counters[k, etaz, 1, schetchik] = modCounters[1];
                if (modCounters[3] != null) counters[k, etaz, 3, schetchik] = modCounters[3];
                if (modCounters[4] != null) counters[k, etaz, 4, schetchik] = modCounters[4];
                if (modCounters[6] != null) counters[k, etaz, 6, schetchik] = modCounters[6];
                if (modCounters[11] != null) counters[k, etaz, 11, schetchik] = modCounters[11];
                if (modCounters[12] != null) counters[k, etaz, 12, schetchik] = modCounters[12];
                if (modCounters[13] != null) counters[k, etaz, 13, schetchik] = modCounters[13];
                if (modCounters[14] != null) counters[k, etaz, 14, schetchik] = modCounters[14];
                if (modCounters[15] != null) counters[k, etaz, 15, schetchik] = modCounters[15];
            }
            if (dataMod[1] == "voda")
            {                        //в 2,5,7,8,9,10
                if (modCounters[0] != null) counters[k, etaz, 0, schetchik] = modCounters[0];
                if (modCounters[2] != null) counters[k, etaz, 2, schetchik] = modCounters[2];
                if (modCounters[5] != null) counters[k, etaz, 5, schetchik] = modCounters[5];
                if (modCounters[7] != null) counters[k, etaz, 7, schetchik] = modCounters[7];
                if (modCounters[8] != null) counters[k, etaz, 8, schetchik] = modCounters[8];
                if (modCounters[9] != null) counters[k, etaz, 9, schetchik] = modCounters[9];
                if (modCounters[10] != null) counters[k, etaz, 10, schetchik] = modCounters[10];
            }
        }
        void clearRowKey(int floor, int numroom, int row)
        {
            if (modCounters[1] == null)//удаление с учетом ключа
            {                        //э 1,3,4,6,11,12,13,14,15
                if (modCounters[3] != null) counters[row, floor, 3, numroom] = null;
                if (modCounters[4] != null) counters[row, floor, 4, numroom] = null;
                if (modCounters[6] != null) counters[row, floor, 6, numroom] = null;
                if (modCounters[11] != null) counters[row, floor, 11, numroom] = null;
                if (modCounters[12] != null) counters[row, floor, 12, numroom] = null;
                if (modCounters[13] != null) counters[row, floor, 13, numroom] = null;
                if (modCounters[14] != null) counters[row, floor, 14, numroom] = null;
                if (modCounters[15] != null) counters[row, floor, 15, numroom] = null;
            }
            if (modCounters[8] == null)
            {                        //в 2,5,7,8,9,10
                if (modCounters[2] != null) counters[row, floor, 2, numroom] = null;
                if (modCounters[5] != null) counters[row, floor, 5, numroom] = null;
                if (modCounters[7] != null) counters[row, floor, 7, numroom] = null;
                if (modCounters[9] != null) counters[row, floor, 9, numroom] = null;
                if (modCounters[10] != null) counters[row, floor, 10, numroom] = null;
            }
        }
        void clearRowA(int floor, int numroom, int row)
        {
            for (int i = 0; i < RMA; i++)
            {
                arenda[row, floor, i, numroom] = null;
            }
        }
        bool floorNumRoom(int floor, int numroom)
        {
            if (outL2et_pom[0] != 7)
            {
                if (floor == outL2et_pom[0] && numroom == outL2et_pom[1]) return true;
            }
            else
            {
                if (floor == floorGlobal && numroom == roomGlobal) return true;
            }
            return false;
        }
        void toCounters(int floor, int numroom)
        {
            if (floorNumRoom(floor,numroom))
            {//совпал номер помещения
                if (modCounters[0] != null && modCounters[0] != "")//изменение имеет место
                {
                    bool findDate = false;
                    int row = 0;
                    for (; row < 60; row++)//пробежимся по таблице
                    { //у нас в наличии измененная строка {дата-0,показание_Э-1, показание_В-2, номер_Э-3, К_тр_Э-4, номер_В-5, расход_Э-6, кол-во_Сотр_В-7, 
                        //сч-р_В-8, тех-хо_В-9, расход_В-10, корп_Э-11, помещ_Э-12, этаж_Э-13, %_Э-14, С-кВт_Э-15}+dataMod= дата редактируемая в datagrid
                        //если dataMod не пустое, то изменилась дата... измененную дату мы не найдем, но если она пустая, то найдем. как искать?
                        if (counters[row, floor, 0, numroom] == null) break;//пустые строки ниже сбросим
                        if (!(dataMod[0] == "" || dataMod[0] == null))
                        { 
                            if (counters[row, floor, 0, numroom] == DateTime.Parse(dataMod[0]).ToShortDateString())//dataMod[0] - дата в строке, которая была до изменения, [1] - ключ (electro или voda)
                            {//изменилась дата: существующая дата изменила свой индекс row, либо она удалена совсем.
                                findDate = true;
                                writeStrToMass(floor, numroom, row);//перед удалением запишем недостающие данные
                                if (removeRowInMassiv(floor, numroom, row))//если получилось удалить строку
                                {
                                    //modCounters = new string[RMC];//очистим строку изменений 
                                }
                                for (row=0; row < 60; row++)
                                {
                                    if (counters[row, floor, 0, numroom] != null)
                                    {
                                        if (DateTime.Parse(counters[row, floor, 0, numroom]) < DateTime.Parse(modCounters[0]))
                                        {
                                            addRowToMassiv(floor, numroom, row);//добавить строку и записать
                                            break;
                                        }
                                        if (DateTime.Parse(counters[row, floor, 0, numroom]) == DateTime.Parse(modCounters[0]))
                                        {
                                            writeStrToMass(floor, numroom, row);//записать изменения
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        writeStrToMass(floor, numroom, row);//записать изменения
                                        break;
                                    }
                                    //обработчик крайнего значения (если сделали весь цикл, но условие не выполнили)
                                    
                                }
                                break;
                            }
                        }
                    }
                    if (!findDate)
                    {//введена дата, которой раньше не было 
                        for (row = 0; row < 60; row++)
                        {
                            if (counters[row, floor, 0, numroom] != null)
                            {
                                if (DateTime.Parse(counters[row, floor, 0, numroom]) < DateTime.Parse(modCounters[0]))
                                {
                                    addRowToMassiv(floor, numroom, row);//добавить строку и записать
                                    break;
                                }
                                if (DateTime.Parse(counters[row, floor, 0, numroom]) == DateTime.Parse(modCounters[0]))
                                {
                                    writeStrToMass(floor, numroom, row);//записать изменения
                                    break;
                                }
                            }
                            else
                            {
                                writeStrToMass(floor, numroom, row);//записать изменения
                                break;
                            }
                            //обработчик крайнего значения (если сделали весь цикл, но условие не выполнили)

                        }
                    }
                }
                else if (modCounters[0] == "")
                {
                    for (int row = 0; row < 60; row++)
                    {
                        if (counters[row, floor, 0, numroom] == dataMod[0]) removeRowInMassiv(floor, numroom, row);
                    }
                }
            }
        }

        void toArenda(int floor, int numroom)
        {
            if (floorNumRoom(floor, numroom))
            {//совпал номер помещения
                if (modArenda[0] != null&&modArenda[0] != "")//изменение имеет место
                {
                    bool findDate = false;
                    int row = 0;
                    for (; row < 10; row++)//пробежимся по таблице
                    { //
                        if (arenda[row, floor, 0, numroom] == null) break;//пустые строки ниже сбросим
                        if (dataModA != "")
                        {
                            if (arenda[row, floor, 0, numroom] == dataModA)//dataModA - дата в строке, которая была до изменения
                            {//изменилась дата: существующая дата изменила свой индекс row, либо она удалена совсем.
                                findDate = true;
                                writeStrToMassA(floor, numroom, row);//перед удалением запишем недостающие данные
                                removeRowInMassivA(floor, numroom, row);
                                for (row = 0; row < 10; row++)
                                {
                                    if (arenda[row, floor, 0, numroom] != null)
                                    {
                                        if (DateTime.Parse(arenda[row, floor, 0, numroom]) < DateTime.Parse(modArenda[0]))
                                        {
                                            addRowToMassivA(floor, numroom, row);//добавить строку и записать
                                            break;
                                        }
                                        if (DateTime.Parse(arenda[row, floor, 0, numroom]) == DateTime.Parse(modArenda[0]))
                                        {
                                            writeStrToMassA(floor, numroom, row);//записать изменения
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        writeStrToMassA(floor, numroom, row);//записать изменения
                                        break;
                                    }
                                    //обработчик крайнего значения (если сделали весь цикл, но условие не выполнили)

                                }
                                break;
                            }
                        }
                    }
                    if (!findDate)
                    {
                        for (row = 0; row < 10; row++)
                        {
                            if (arenda[row, floor, 0, numroom] != null)
                            {
                                if (DateTime.Parse(arenda[row, floor, 0, numroom]) < DateTime.Parse(modArenda[0]))
                                {
                                    addRowToMassivA(floor, numroom, row);//добавить строку и записать
                                    break;
                                }
                                if (DateTime.Parse(arenda[row, floor, 0, numroom]) == DateTime.Parse(modArenda[0]))
                                {
                                    writeStrToMassA(floor, numroom, row);//записать изменения
                                    break;
                                }
                            }
                            else
                            {
                                writeStrToMassA(floor, numroom, row);//записать изменения
                                break;
                            }
                        }
                    }
                    for (int floor1 = 0; floor1 < 4; floor1++)//найдем и перезапишем данные арендатора по другим помещениям
                    {
                        for (int numroom1 = 0; numroom1 < maxRoom; numroom1++)
                        {
                            if (!(floor1 == floorGlobal && numroom1 == roomGlobal))
                            {

                                if (arenda[0, floor1, 1, numroom1] == arenda[0, floor, 1, numroom])
                                {
                                    for (int j = 2; j < RMA; j++)
                                    {//перезапишем данные в остальных таблицах с учетом изменения по данному арендатору, кроме даты и самого арендатора (j=2)
                                        arenda[0, floor1, j, numroom1] = arenda[0, floor, j, numroom];
                                    }
                                }
                            }
                        }
                    }
                }
                else if (modArenda[0] == "")
                {
                    for (int row = 0; row < 10; row++)
                    {
                        if (arenda[row, floor, 0, numroom] == dataModA) removeRowInMassivA(floor, numroom, row);
                    }
                    
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            tabPage6.Parent = null;
            button2.BackColor = Color.DodgerBlue;
            button45.BackColor = Color.DodgerBlue;
            outL2et_pom[0] = 7;//обнулим в начале
            File = System.IO.File.ReadAllLines(@"Data.txt", Encoding.Default).ToList();
            CorrectDB();//проверка на корректность данных в базе. (не расширенная, только на количество значений)
             //самое максимальное количество помещений на одном из этажей
            int etaz = 0;
            //int PomeshenieM = int.Parse(File[0]);            
            for (int i = 0; i < File.Count; i++)
            {
                if (File[i].IndexOf("[etaz_") > -1)
                {
                    countRoom[etaz] = int.Parse(File[i].Substring(8, File[i].Length - 8)) - 1;//количество помещений на этаже
                    //if (countRoom[floor] > maxRoom) maxRoom = countRoom[floor];
                    etaz++;
                }
            }
            label1.Text = "Загружено";
            koord = new int[4, 40, maxRoom];//координаты помещения
            data = new string[4, RMD, maxRoom];//все данные по помещению
            arenda = new string[10, 4, RMA, maxRoom];//реквизиты арендатора
            counters = new string[60, 4, RMC, maxRoom];//показания счетчиков на последний период.
            int schetchik = 0;
            etaz = 0;
            LoadDB();
            /*
            for (int i = 0; i < File.Count; i++)
            {                
                if (File[i].IndexOf("[etaz_") > -1)
                {
                    floor = int.Parse(File[i].Substring(6, 1)) - 1;//номер этажа
                    room = 0;
                }
                if (File[i] == "[" + room + "]")
                {
                    i++;
                    string s = File[i];
                    if (File[i] != "=no koord=")
                    {
                        for (int j = 0; j < 40; j++)
                        {
                            if (s.IndexOf(";") > -1)
                            {
                                koord[floor, j, room] = int.Parse(s.Substring(0, s.IndexOf(";")));
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                koord[floor, j, room] = int.Parse(s);
                                break;
                            }
                        }
                    }
                    i++;
                    if (i >= File.Count()) break;
                    s = File[i];
                    for (int j = 0; j < RMD; j++)
                    {
                        if (s.IndexOf(";") > -1)
                        {
                            if (s.IndexOf(";") != 0) data[floor, j, room] = s.Substring(0, s.IndexOf(";"));
                            s = s.Substring(s.IndexOf(";") + 1);
                        }
                        else
                        {
                            data[floor, j, room] = s;
                            break;
                        }
                    }
                    for (int k = 0; k < 10; k++)
                    {
                        i++;
                        if (i >= File.Count()) break;
                        s = File[i];
                        if (s == "[pokazanie]")
                        {
                          //  i++;
                            break;
                        }
                        for (int j = 0; j < RMA; j++)
                        {
                            if (s.IndexOf(";") > -1)
                            {
                                if (s.IndexOf(";") != 0) arenda[k, floor, j, room] = s.Substring(0, s.IndexOf(";"));
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                arenda[k, floor, j, room] = s;
                                break;
                            }
                        }
                    }
                    for (int k = 0; k < 60; k++)
                    {
                        i++;
                        if (i >= File.Count()) break;
                        s = File[i];
                        if (s.Substring(0, 1) == "["||s=="=no koord=") break;
                        for (int j = 0; j < RMC; j++)
                        {
                            if (s.IndexOf(";") > -1)
                            {
                                if (s.IndexOf(";") != 0) counters[k, floor, j, room] = s.Substring(0, s.IndexOf(";"));
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                counters[k, floor, j, room] = s;
                                break;
                            }
                        }
                    }
                    room++;
                    i--;
                }
            }
            //*/
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox3.AutoCompleteSource = AutoCompleteSource.ListItems;

            comboBox5.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox5.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox6.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox6.AutoCompleteSource = AutoCompleteSource.ListItems;
           // comboBox7.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
           // comboBox7.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых ТП.

            comboBox8.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox8.AutoCompleteSource = AutoCompleteSource.ListItems;

            comboBox11.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox11.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox12.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox12.AutoCompleteSource = AutoCompleteSource.ListItems;

            scale = 5;
            scalekX = pictureBox1.Image.Size.Width / 20;
            scalekY = pictureBox1.Image.Size.Height / 20;
            pictureBox1.Width = scalekX * scale;
            pictureBox1.Height = scalekY * scale;
            pictureBox1.Focus();
            timer2.Enabled = true;
            pictureBox1.Load(@"Этаж" + (floorGlobal + 1).ToString() + ".png");//, System.Drawing.Imaging.ImageFormat.Png);

            if (UserKey == "voda")
            {
                
                dateTimePicker1.Enabled = false;
                //comboBox1.Enabled = false;
                comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых арендаторов
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                //comboBox4.Enabled = false;//снимем блокировку количества сотрудников
                textBox17.Enabled = false;
                richTextBox3.Enabled = false;
                comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых корпусов
                comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых помещений
                comboBox7.Enabled = false;
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;
                comboBox10.Enabled = false;
                comboBox11.Enabled = false;
                comboBox12.Enabled = false;
                comboBox13.Enabled = false;
                comboBox14.Enabled = false;
                comboBox15.Enabled = false;
                comboBox18.Enabled = false;
                textBox4.Enabled = false;
                checkBox1.Enabled = false;
                textBox10.Enabled = false;
                textBox12.Enabled = false;
                dateTimePicker3.Enabled = false;
                tabPage1.Parent = null;//прячем отладку
                tabControl2.SelectedIndex = 1;//покажем вкладку водоснабжение на первый план                
            }
            if (UserKey == "arenda")
            {
                comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых корпусов
                comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых помещений
                comboBox7.Enabled = false;
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;
                comboBox10.Enabled = false;
                comboBox11.Enabled = false;
                comboBox12.Enabled = false;
                comboBox13.Enabled = false;
                comboBox14.Enabled = false;
                comboBox15.Enabled = false;
                comboBox16.Enabled = false;
                comboBox17.Enabled = false;
                comboBox18.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                checkBox1.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
                tabPage1.Parent = null;//прячем отладку
            }
            if (UserKey == "electro")
            {
                dateTimePicker1.Enabled = false;
                //comboBox1.Enabled = false;
                comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых арендаторов
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                textBox17.Enabled = false;
                richTextBox3.Enabled = false;
                comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых корпусов
                comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых помещений
                comboBox16.Enabled = false;
                comboBox17.Enabled = false;
                textBox5.Enabled = false;
                textBox11.Enabled = false;
                textBox13.Enabled = false;
                dateTimePicker4.Enabled = false;
                tabPage1.Parent = null;//прячем отладку
                textBox19.Enabled = false;
            }
            LoadCB();
            LoadCB2();
            button31.PerformClick();//прогрузим арендаторов в лист бокс внизу
            ///далее прогрузка всех листбоксов
            if (UserKey == "admin" || UserKey == "electro" || UserKey == "arenda" || UserKey == "voda") richTextBox2.Text = UserKey;
            else this.Close();
            OutputSorting("ToLongName");
            
        }

        int panelCentrX = 626;
        int panelCentrY = 389;
        private void button3_Click(object sender, EventArgs e)
        {
            double x = panelCentrX - (Center(textBox1.Text).X) * scale / 20; //626 и 389 - это центр панели с пиктурбоксом
            double y = panelCentrY - (Center(textBox1.Text).Y) * scale / 20;
            richTextBox1.Text += "x=" + x.ToString() + "; y=" + y.ToString() + "\r\n";
            pictureBox1.Location = new Point((int)x, (int)y);
            curnew = pictureBox1.Location;
            pictureBox1.Focus();
        }
        Point Center(string koord)//вида: 0,123,45,79    x=0,y=123,x=45,y=79 и т.д.
        {
            if (koord == "") return new Point(0, 0);
            else
            {
                int x = 0, x1 = -1;
                int y = 0, y1 = -1;
                int[] mass = new int[40]; //потолок - 20 координат
                int i1 = 0;
                for (; i1 < 40; i1++)
                {
                    if (koord.IndexOf(",") > 0)
                    {
                        mass[i1] = int.Parse(koord.Substring(0, koord.IndexOf(",")));
                        koord = koord.Substring(koord.IndexOf(",") + 1);
                    }
                    else
                    {
                        mass[i1] = int.Parse(koord);
                        break;
                    }
                }
                for (int i = 0; i + 1 <= i1; i += 2)//х,y = max; x1,y1 = min
                {
                    if (x1 + y1 < 0)
                    {
                        x1 = mass[i];
                        y1 = mass[i + 1];
                    }
                    if (mass[i] + mass[i + 1] > x + y)
                    {
                        x = mass[i];
                        y = mass[i + 1];
                    }
                    else
                    {
                        if (mass[i] + mass[i + 1] < x1 + y1)
                        {
                            x1 = mass[i];
                            y1 = mass[i + 1];
                        }
                    }
                }
                x = (x + x1) / 2;
                y = (y + y1) / 2;
                return new Point(x, y);
            }
        }
        Point CentrU(double[,] mass)
        {
            double x = 0, x1 = 10000;
            double y = 0, y1 = 10000;
            for (int i = 0; i < 20; i++) //ограничение 20 координат
            {
                if (mass[0, i] != 0)
                {
                    if (mass[0, i] + mass[1, i] < 0)
                    {
                        x1 = mass[0, i];
                        y1 = mass[1, i];
                    }
                    if (mass[0, i] + mass[1, i] > x + y)
                    {
                        x = mass[0, i];
                        y = mass[1, i];
                    }
                    else
                    {
                        if (mass[0, i] + mass[1, i] < x1 + y1)
                        {
                            x1 = mass[0, i];
                            y1 = mass[1, i];
                        }
                    }
                }
                else break;
            }
            x = (x + x1) / 2;
            y = (y + y1) / 2;
            return new Point((int)x, (int)y);
        } //для использования (найти помещение на карте)

        int equationSystem(Point p1, out Point[] P) //y=((x-x1)/(x2-x1))*(y2-y1)+y1 - функция с трассировкой вверх по игреку выводит Помещение
        {
            P = new Point[1];
            int room = -1;

            //потолок - 20 координат
            for (int j = 0; j <= countRoom[floorGlobal]; j++)
            {
                int i1 = 0;
                int i2 = 0;
                double[,] mass = new double[2, 20];
                for (; i1 < 40; i1++, i2++)//пройти по координатам
                {
                    if (koord[floorGlobal, i1, j] != 0)
                    {
                        mass[0, i2] = koord[floorGlobal, i1, j];
                        i1++;
                        mass[1, i2] = koord[floorGlobal, i1, j];
                    }
                    else break;
                }
                mass[0, i2] = mass[0, 0];
                mass[1, i2] = mass[1, 0];
                //i1 += 2;

                bool rezultBool = false;
                for (int i = 1; i <= i2; i++)
                {
                    double y11 = (((double)p1.X - mass[0, i - 1]) / (mass[0, i] - mass[0, i - 1])) * (mass[1, i] - mass[1, i - 1]) + mass[1, i - 1];
                    //добавим ограничение по иксу:
                    double max = 0;
                    double min = 0;

                    if (mass[0, i] - mass[0, i - 1] > 0)//(движение слева на право)
                    {
                        min = mass[0, i - 1];
                        max = mass[0, i];
                    }
                    else//(движение справа на лево)
                    {
                        min = mass[0, i];
                        max = mass[0, i - 1];
                    }
                    if ((double)p1.X < max && (double)p1.X >= min && y11 < p1.Y)//ограничиваем по иксу //трассировка вверх (игрек меньше точки) >= - исправил наконец-то ошибку точки
                    {
                        if (rezultBool == false) rezultBool = true;
                        else rezultBool = false;
                    }
                }
                if (rezultBool == true)
                {
                    room = j;
                    P = new Point[i2];
                    for (int i = 0; i < i2; i++)
                    {
                        P[i].X = (int)mass[0, i];
                        P[i].Y = (int)mass[1, i];
                    }
                    break;
                }
            }
            return room;
        }
        //*/
        private void button4_Click(object sender, EventArgs e)
        {
            label3.Text = Center(textBox1.Text).X + ";" + Center(textBox1.Text).Y;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            pictureBox1.Size = pictureBox1.Image.Size;
            scale = 20;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            pictureBox1.Width = scalekX;
            pictureBox1.Height = scalekY;
            pictureBox1.Location = new Point(0, 0);
            scale = 1;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            comboBox19.Text = "";
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string s = null;
            string s1 = "";
            string s2 = "значение";
            string s3 = s2;
            string s4 = "значение2";

            if (!(s == null && s1 == "") && (s != s1)) richTextBox1.Text += "1\r\n";
            if (!(s == null && s2 == "") && (s != s2)) richTextBox1.Text += "2\r\n";//прокатило
            if (!(s2 == null && s3 == "")&& (s2 != s3)) richTextBox1.Text += "3\r\n";
            if (!(s3 == null && s4 == "") && (s3 != s4)) richTextBox1.Text += "4\r\n";//прокатило
        }
        Point[] poligon1 = new Point[20];
        int GlobalP = 21; //количество точек многогранника, (максимально 20, 21 - за диапазоном)
        private void button9_Click(object sender, EventArgs e)
        {
            g3 = false;
            GlobalP = 0;
            poligon1 = new Point[20];
            textBox1.Text = "";
            button9.Enabled = false;

            roomGlobal = FindPom(comboBox5.Text, comboBox6.Text);
            if (roomGlobal < 0)
            {
                countRoom[floorGlobal]++;
                roomGlobal = countRoom[floorGlobal];
                data[floorGlobal, 0, roomGlobal] = comboBox5.Text;
                data[floorGlobal, 1, roomGlobal] = comboBox6.Text;
            }
            if (koord[floorGlobal, 0, roomGlobal] != 0)
            {
                for (int i = 0; i < 40; i++)
                {
                    if (koord[floorGlobal, i, roomGlobal] != 0) koord[floorGlobal, i, roomGlobal] = 0;
                    else break;
                }
            }
        }
        Point[] figa1 = new Point[1];
        private void button10_Click(object sender, EventArgs e)
        {
            if (GlobalP != 21)
            {
                figa1 = new Point[GlobalP];

                bitmap = new Bitmap(pictureBox1.Image);
                g = Graphics.FromImage(bitmap);

                string s = "";
                int i1 = 0;
                for (int i = 0; i < GlobalP; i++)
                {
                    s += poligon1[i].X.ToString() + ",";
                    koord[floorGlobal, i1, roomGlobal] = poligon1[i].X;
                    s += poligon1[i].Y.ToString() + ",";
                    i1++;
                    koord[floorGlobal, i1, roomGlobal] = poligon1[i].Y;
                    i1++;
                    figa1[i] = poligon1[i];
                }
                s = s.Substring(0, s.Length - 1);

                g.DrawPolygon(new Pen(Color.Green, 5), figa1);
                // g.FillEllipse(Brushes.Red, Center(s).X, Center(s).Y, 6, 6);
                textBox1.Text = s;
                pictureBox1.Image.Dispose();
                pictureBox1.Image = bitmap;
                g.Dispose();
                button19.PerformClick();
            }
            GlobalP = 21;
            g3 = true;
            //  button9.Enabled = true;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Point[] figure = new Point[3];
            figure[0] = new Point(250, 100);
            figure[1] = new Point(1400, 200);
            figure[2] = new Point(500, 650);
            bitmap = new Bitmap(pictureBox1.Image);
            g = Graphics.FromImage(bitmap);
            //g.DrawLine(new Pen(Color.Green, 5), new Point(int.Parse(textBox2.Text), int.Parse(textBox3.Text)), new Point(int.Parse(textBox2.Text) + 100, int.Parse(textBox3.Text)));
            g.DrawPolygon(new Pen(Color.Green, 5), figure);
            string s = "";
            for (int i = 0; i < 3; i++)
            {
                s += figure[i].X.ToString() + ",";
                s += figure[i].Y.ToString() + ",";
            }
            s = s.Substring(0, s.Length - 1);
            textBox1.Text = s;
            g.FillEllipse(Brushes.Black, Center(s).X, Center(s).Y, 6, 6);
            pictureBox1.Image.Dispose();
            pictureBox1.Image = bitmap;
            g.Dispose();
        }
        bool g3 = true; //изменено 29.04.20
        private void button12_Click(object sender, EventArgs e)
        {
            g3 = true;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            pictureBox1.Load(@"точки.png");
        }

        private void button14_Click(object sender, EventArgs e) //save
        {
            if (checkBox2.Checked) pictureBox1.Image.Save(@"Этаж" + (floorGlobal + 1).ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
            System.IO.File.WriteAllLines(@"Data.txt", File, Encoding.Default);
            System.IO.File.WriteAllLines(@DateTime.Now.ToShortDateString() + ".txt", File, Encoding.Default);
        }
        bool panelHide = false;
        private void button15_Click(object sender, EventArgs e)
        {
            if (panelHide == false)
            {
                panel1.Dock = DockStyle.Fill;
                //tabControl1.Dock = DockStyle.None;
                tabControl1.Visible = false;
                int sizeX = panel1.Size.Width - 119;//113+6 60+6
                button9.Location = new Point(sizeX, button9.Location.Y);
                button10.Location = new Point(sizeX, button10.Location.Y);
                button42.Location = new Point(sizeX, button42.Location.Y);
                button43.Location = new Point(sizeX, button43.Location.Y);
                button44.Location = new Point(sizeX, button44.Location.Y);
                button45.Location = new Point(sizeX, button45.Location.Y);
                button46.Location = new Point(sizeX, button46.Location.Y);
                button47.Location = new Point(panel1.Size.Width -66, button47.Location.Y);
                panelHide = true;
            }
            else
            {
                panel1.Dock = DockStyle.Fill;
               // tabControl1.Dock = DockStyle.Right;
                tabControl1.Visible = true;
                int sizeX = panel1.Size.Width - 116-tabControl1.Size.Width;
                button9.Location = new Point(sizeX, button9.Location.Y);
                button10.Location = new Point(sizeX, button10.Location.Y);
                button42.Location = new Point(sizeX, button42.Location.Y);
                button43.Location = new Point(sizeX, button43.Location.Y);
                button44.Location = new Point(sizeX, button44.Location.Y);
                button45.Location = new Point(sizeX, button45.Location.Y);
                button46.Location = new Point(sizeX, button46.Location.Y);
                button47.Location = new Point(panel1.Size.Width - tabControl1.Size.Width - 63, button47.Location.Y);
                panelHide = false;
            }

        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            panelCentrX = panel1.Size.Width / 2;
            panelCentrY = panel1.Size.Height / 2;
        }

        private void button1_Click(object sender, EventArgs e)//saveDB
        {
            File.Clear();
            File.Add((countRoom[0] + countRoom[1] + countRoom[2] + countRoom[3] + 4).ToString());//записали общее количество помещений в начало
            for (int etaz = 0; etaz < 4; etaz++)
            {
                File.Add("[etaz_" + (etaz + 1).ToString() + "]" + (countRoom[etaz] + 1).ToString());//запись номера этажа
                for (int pomeshenie = 0; pomeshenie <= countRoom[etaz]; pomeshenie++)
                {
                    File.Add("[" + pomeshenie + "]");//запись номера помещения
                    string s = "";
                    for (int i = 0; i < 40; i++)
                    {
                        if (koord[etaz, i, pomeshenie] == 0) break;
                        else
                        {
                            s += koord[etaz, i, pomeshenie] + ";";
                        }
                    }
                    if (s != "") File.Add(s.Substring(0, s.Length - 1));//записали координаты
                    s = "";
                    for (int i = 0; i < RMD; i++) s += data[etaz, i, pomeshenie] + ";";
                    if (s != "") File.Add(s.Substring(0, s.Length - 1));//записали данные помещения
                    s = "";
                    for (int k = 0; k < 10; k++)
                    {
                        if (arenda[k, etaz, 0, pomeshenie] == null) break;
                        s = "";
                        for (int i = 0; i < RMA; i++) s += arenda[k, etaz, i, pomeshenie] + ";";//было 5 а не 7... ошибка?
                        if (s != "") File.Add(s.Substring(0, s.Length - 1));//записали реквизиты арендатора
                    }
                    File.Add("[pokazanie]");
                    s = "";
                    for (int k = 0; k < 60; k++)
                    {
                        if (counters[k, etaz, 0, pomeshenie] == null) break;
                        s = "";
                        for (int i = 0; i < RMC; i++)
                        {
                            s += counters[k, etaz, i, pomeshenie] + ";";
                        }
                        File.Add(s.Substring(0, s.Length - 1));//записали строку счетчиков
                    }
                    s = "";
                }
            }
            richTextBox1.Clear();
            for (int i = 0; i < File.Count; i++) richTextBox1.Text += File[i] + "\r\n";
        }

        private void button2_Click(object sender, EventArgs e)//этаж1
        {
            floorGlobal = 0;
            LoadCB();//прогружает арендатора и корпус
            ClearCB();//чистит все данные из боксов
            button2.BackColor = Color.DodgerBlue;
            button16.BackColor = SystemColors.Control;
            button17.BackColor = SystemColors.Control;
            button18.BackColor = SystemColors.Control;
            pictureBox1.Load(@"этаж1.png");
        }

        private void button16_Click(object sender, EventArgs e)//этаж2
        {
            floorGlobal = 1;
            LoadCB();//прогружает арендатора и корпус
            ClearCB();//чистит все данные из боксов
            button16.BackColor = Color.DodgerBlue;
            button2.BackColor = SystemColors.Control;
            button17.BackColor = SystemColors.Control;
            button18.BackColor = SystemColors.Control;
            pictureBox1.Load(@"этаж2.png");
        }

        private void button17_Click(object sender, EventArgs e)//этаж3
        {
            floorGlobal = 2;
            LoadCB();
            ClearCB();
            button17.BackColor = Color.DodgerBlue;
            button2.BackColor = SystemColors.Control;
            button16.BackColor = SystemColors.Control;
            button18.BackColor = SystemColors.Control;
            pictureBox1.Load(@"этаж3.png");
        }

        private void button18_Click(object sender, EventArgs e)//этаж4
        {
            floorGlobal = 3;
            LoadCB();
            ClearCB();
            button18.BackColor = Color.DodgerBlue;
            button2.BackColor = SystemColors.Control;
            button16.BackColor = SystemColors.Control;
            button17.BackColor = SystemColors.Control;
            pictureBox1.Load(@"этаж4.png");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)//трансформаторы тока (показать/скрыть)
        {
            if (checkBox1.Checked)
            {
                label23.Visible = true;
                label24.Visible = true;
                label25.Visible = true;
                label26.Visible = true;
                label27.Visible = true;
                label33.Visible = true;
                label34.Visible = true;
                label35.Visible = true;
                comboBox18.Visible = true;
                textBox6.Visible = true;
                textBox7.Visible = true;
                textBox8.Visible = true;
                textBox9.Visible = true;
                textBox14.Visible = true;
                textBox15.Visible = true;
                textBox16.Visible = true;
            }
            else
            {
                label23.Visible = false;
                label24.Visible = false;
                label25.Visible = false;
                label33.Visible = false;
                label34.Visible = false;
                label35.Visible = false;
                label26.Visible = false;
                label27.Visible = false;
                comboBox18.Visible = false;
                comboBox18.Text = "1";
                textBox6.Visible = false;
                textBox6.Text = "";
                textBox7.Visible = false;
                textBox7.Text = "";
                textBox8.Visible = false;
                textBox8.Text = "";
                textBox9.Visible = false;
                textBox9.Text = "";
                textBox14.Visible = false;
                textBox14.Text = "";
                textBox15.Visible = false;
                textBox15.Text = "";
                textBox16.Visible = false;
                textBox16.Text = "";
            }
        }

        private void button19_Click(object sender, EventArgs e)//записать данные (Сохранить изменения)
        {
            if (comboBox5.Text != "" && comboBox6.Text != "")
            {//найти индекс помещения. Если совпадений нет, то: countRoom[floorGlobal]++; roomGlobal=countRoom[floorGlobal];
                if (countRoom[floorGlobal] == -1)
                {
                    countRoom[floorGlobal]++;
                    roomGlobal = countRoom[floorGlobal];
                }
                else
                {
                    roomGlobal = FindPom(comboBox5.Text, comboBox6.Text);
                    if (roomGlobal < 0)
                    {
                        countRoom[floorGlobal]++;
                        roomGlobal = countRoom[floorGlobal];
                    }
                    else
                    {
                        //вписать остальные данные по этому помещению?
                    }
                }//шпора data шпаргалка                
                if (!(data[floorGlobal, 0, roomGlobal] ==null&& comboBox5.Text=="")&&(data[floorGlobal, 0, roomGlobal] != comboBox5.Text.Replace(";", ","))) 
                {
                    modData[0] = comboBox5.Text.Replace(";", ",");//корпус
                }                
                if (!(data[floorGlobal, 1, roomGlobal] ==null&& comboBox6.Text=="")&&(data[floorGlobal, 1, roomGlobal] != comboBox6.Text.Replace(";", ",")))
                {
                    modData[1] = comboBox6.Text.Replace(";", ",");//помещение
                }
                if (!(data[floorGlobal, 2, roomGlobal] == null&&comboBox7.Text=="")&&(data[floorGlobal, 2, roomGlobal] != comboBox7.Text.Replace(";", ",")))
                {
                    modData[2] = comboBox7.Text.Replace(";", ",");//запитка от тп
                }
                if (!(data[floorGlobal, 3, roomGlobal] ==null&& comboBox8.Text=="")&&(data[floorGlobal, 3, roomGlobal] != comboBox8.Text.Replace(";", ",")))
                {
                    modData[3]= comboBox8.Text.Replace(";", ",");//запитка от сп
                }
                if (!(data[floorGlobal, 4, roomGlobal] ==null&& comboBox9.Text=="")&&(data[floorGlobal, 4, roomGlobal] != comboBox9.Text.Replace(";", ",")))
                {
                    modData[4] = comboBox9.Text.Replace(";", ",");//марка кабеля
                }
                if (!(data[floorGlobal, 5, roomGlobal] ==null&& comboBox10.Text=="")&&(data[floorGlobal, 5, roomGlobal] != comboBox10.Text.Replace(";", ",")))
                {
                    modData[5] = comboBox10.Text.Replace(";", ",");//длина кабеля (м)
                }
                if (!(data[floorGlobal, 6, roomGlobal] ==null&& comboBox11.Text=="")&&(data[floorGlobal, 6, roomGlobal] != comboBox11.Text.Replace(";", ",")))
                {
                    modData[6] = comboBox11.Text.Replace(";", ",");//мощность кВт
                }
                if (!(data[floorGlobal, 7, roomGlobal] ==null&& comboBox12.Text=="")&&(data[floorGlobal, 7, roomGlobal] != comboBox12.Text.Replace(";", ",")))
                {
                    modData[7] = comboBox12.Text.Replace(";", ",");//тип отключающего устройства
                }
                if (!(data[floorGlobal, 8, roomGlobal] ==null&& comboBox13.Text=="")&&(data[floorGlobal, 8, roomGlobal] != comboBox13.Text.Replace(";", ",")))
                {
                    modData[8] = comboBox13.Text.Replace(";", ",");//Уставка (А) In
                }
                if (!(data[floorGlobal, 9, roomGlobal] ==null&& comboBox14.Text=="")&&(data[floorGlobal, 9, roomGlobal] != comboBox14.Text.Replace(";", ",")))
                {
                    modData[9] = comboBox14.Text.Replace(";", ",");//Номер электросчетчика
                }
                if (!(data[floorGlobal, 10, roomGlobal] ==null&& comboBox15.Text=="")&&(data[floorGlobal, 10, roomGlobal] != comboBox15.Text.Replace(";", ",")))
                {
                    modData[10] = comboBox15.Text.Replace(";", ",");//марка электросчетчика
                }
                if (!(data[floorGlobal, 11, roomGlobal] ==null&& textBox4.Text=="")&&(data[floorGlobal, 11, roomGlobal] != textBox4.Text.Replace(";", ",")))
                {
                    modData[11] = textBox4.Text.Replace(";", ",");//год в/поверки эл.счетчика
                }
                if (!(data[floorGlobal, 12, roomGlobal] ==null&& comboBox16.Text=="")&&(data[floorGlobal, 12, roomGlobal] != comboBox16.Text.Replace(";", ",")))
                {
                    modData[12] = comboBox16.Text.Replace(";", ",");//номер водомера
                }
                if (!(data[floorGlobal, 13, roomGlobal] ==null&& comboBox17.Text=="")&&(data[floorGlobal, 13, roomGlobal] != comboBox17.Text.Replace(";", ",")))
                {
                    modData[13] = comboBox17.Text.Replace(";", ",");//марка водомера
                }
                if (!(data[floorGlobal, 14, roomGlobal] ==null&& textBox5.Text=="")&&(data[floorGlobal, 14, roomGlobal] != textBox5.Text.Replace(";", ",")))
                {
                    modData[14] = textBox5.Text.Replace(";", ",");//год в/поверки водомера
                }
                if (!(data[floorGlobal, 15, roomGlobal] ==null&& comboBox18.Text=="")&&(data[floorGlobal, 15, roomGlobal] != comboBox18.Text.Replace(";", ",")))
                {
                    modData[15] = comboBox18.Text.Replace(";", ",");//коэффициент ТТ
                }
                if (!(data[floorGlobal, 16, roomGlobal] ==null&& textBox6.Text=="")&&(data[floorGlobal, 16, roomGlobal] != textBox6.Text.Replace(";", ",")))
                {
                    modData[16] = textBox6.Text.Replace(";", ",");//номер фазы А
                }
                if (!(data[floorGlobal, 17, roomGlobal] ==null&&textBox7.Text=="")&&(data[floorGlobal, 17, roomGlobal] != textBox7.Text.Replace(";", ",")))
                {
                    modData[17] = textBox7.Text.Replace(";", ",");//номер фазы В
                }
                if (!(data[floorGlobal, 18, roomGlobal] ==null&& textBox8.Text=="")&&(data[floorGlobal, 18, roomGlobal] != textBox8.Text.Replace(";", ",")))
                {
                    modData[18] = textBox8.Text.Replace(";", ",");//номер фазы С
                }
                if (!(data[floorGlobal, 19, roomGlobal]==null&& textBox9.Text=="")&&(data[floorGlobal, 19, roomGlobal] != textBox9.Text.Replace(";", ",")))
                {
                    modData[19] = textBox9.Text.Replace(";", ",");//год в/поверки
                }
                if (!(data[floorGlobal, 20, roomGlobal] ==null&& dateTimePicker3.Value.ToShortDateString()=="")&&(data[floorGlobal, 20, roomGlobal] != dateTimePicker3.Value.ToShortDateString().Replace(";", ",")))
                {
                    modData[20] = dateTimePicker3.Value.ToShortDateString().Replace(";", ",");//дата опломбировки эл.счетчика
                }
                if (!(data[floorGlobal, 21, roomGlobal] ==null&& textBox12.Text=="")&&(data[floorGlobal, 21, roomGlobal] != textBox12.Text.Replace(";", ",")))
                {
                    modData[21] = textBox12.Text.Replace(";", ",");//№ пломбы эл.счетчика
                }
                if (!(data[floorGlobal, 22, roomGlobal] ==null&& textBox14.Text=="")&&(data[floorGlobal, 22, roomGlobal] != textBox14.Text.Replace(";", ",")))
                {
                    modData[22] = textBox14.Text.Replace(";", ",");//№ пломбы ТТ "А"
                }
                if (!(data[floorGlobal, 23, roomGlobal] ==null&& textBox15.Text=="")&&(data[floorGlobal, 23, roomGlobal] != textBox15.Text.Replace(";", ",")))
                {
                    modData[23] = textBox15.Text.Replace(";", ",");//№ пломбы ТТ "В"
                }
                if (!(data[floorGlobal, 24, roomGlobal] ==null&& textBox16.Text=="")&&(data[floorGlobal, 24, roomGlobal] != textBox16.Text.Replace(";", ",")))
                {
                    modData[24]= textBox16.Text.Replace(";", ",");//№ пломбы ТТ "С"
                }
                if (!(data[floorGlobal, 25, roomGlobal] ==null&& dateTimePicker4.Value.ToShortDateString()=="")&&(data[floorGlobal, 25, roomGlobal] != dateTimePicker4.Value.ToShortDateString().Replace(";", ",")))
                {
                    modData[25] = dateTimePicker4.Value.ToShortDateString().Replace(";", ",");//дата опломбировки водомера
                }
                if (!(data[floorGlobal, 26, roomGlobal] ==null&& textBox13.Text=="")&&(data[floorGlobal, 26, roomGlobal] != textBox13.Text.Replace(";", ",")))
                {
                    modData[26] = textBox13.Text.Replace(";", ",");//№ пломбы водомера
                }                
                if(!(data[floorGlobal, 27, roomGlobal] ==null&& textBox19.Text=="")&&(data[floorGlobal, 27, roomGlobal] != textBox19.Text.Replace(";", ",")))
                {
                    modData[27] = textBox19.Text.Replace(";", ",");//кв.м.               
                }                
                if( !(data[floorGlobal, 28, roomGlobal] ==null&& textBox22.Text=="")&&(data[floorGlobal, 28, roomGlobal] != textBox22.Text.Replace(";", ",")))
                { 
                    modData[28] = textBox22.Text.Replace(";", ",");//Планировка
                }               
                if(!(data[floorGlobal, 29, roomGlobal] ==null&& textBox23.Text=="")&&(data[floorGlobal, 29, roomGlobal] != textBox23.Text.Replace(";", ",")))
                {
                    modData[29] = textBox23.Text.Replace(";", ",");//Однолинейная схема
                }
                if(!(data[floorGlobal, 30, roomGlobal] ==null&& textBox24.Text=="")&&(data[floorGlobal, 30, roomGlobal] != textBox24.Text.Replace(";", ",")))
                {
                    modData[30] = textBox24.Text.Replace(";", ",");//План электросети
                }
                if (!(data[floorGlobal, 31, roomGlobal] ==null&& textBox25.Text=="")&&(data[floorGlobal, 31, roomGlobal] != textBox25.Text.Replace(";", ",")))
                {
                    modData[31] = textBox25.Text.Replace(";", ",");//План водоснабжения
                }                
                //data[floorGlobal, 32, roomGlobal] = textBox26.Text.Replace(";", ",");//Папка арендатора
                int k = 0;
                //SdvigCHtoOne(arenda, 10, dateTimePicker1.Value.ToShortDateString().Replace(";", ","),5); //в скобочках длина массива, котрый сдвигается на 1.

                //ЕСЛИ арендатор не меняется, не нужно записывать новую строку. в остальных случаях новая запись.
                if (arenda[k, floorGlobal, 0, roomGlobal] != dateTimePicker1.Value.ToShortDateString().Replace(";", ","))
                {
                    modArenda[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");//дата начала аренды
                }
                if (!(arenda[k, floorGlobal, 1, roomGlobal] == null && comboBox1.Text == "") && (arenda[k, floorGlobal, 1, roomGlobal] != comboBox1.Text.Replace(";", ",")))
                {
                    modArenda[1] = comboBox1.Text.Replace(";", ",");//арендатор
                    if (modArenda[0] == null) modArenda[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, floorGlobal, 2, roomGlobal] == null && comboBox2.Text == "") && (arenda[k, floorGlobal, 2, roomGlobal] != comboBox2.Text.Replace(";", ",")))
                {
                    modArenda[2] = comboBox2.Text.Replace(";", ",");//ФИО
                    if (modArenda[0] == null) modArenda[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, floorGlobal, 3, roomGlobal] == null && comboBox3.Text == "") && (arenda[k, floorGlobal, 3, roomGlobal] != comboBox3.Text.Replace(";", ",")))
                {
                    modArenda[3] = comboBox3.Text.Replace(";", ",");//должность
                    if (modArenda[0] == null) modArenda[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, floorGlobal, 4, roomGlobal] == null && comboBox4.Text == "") && (arenda[k, floorGlobal, 4, roomGlobal] != comboBox4.Text.Replace(";", ",")))
                {
                    modArenda[4] = comboBox4.Text.Replace(";", ",");//кол-во сотрудников
                    if (modArenda[0] == null) modArenda[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, floorGlobal, 5, roomGlobal] == null && textBox17.Text == "") && (arenda[k, floorGlobal, 5, roomGlobal] != textBox17.Text.Replace(";", ",")))
                {
                    modArenda[5] = textBox17.Text.Replace(";", ",");//e-mail
                    if (modArenda[0] == null) modArenda[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, floorGlobal, 6, roomGlobal] == null && richTextBox3.Text == "") && (arenda[k, floorGlobal, 6, roomGlobal] != richTextBox3.Text.Replace(";", ",").Replace("\n", "&rn")))
                {
                    modArenda[6] = richTextBox3.Text.Replace(";", ",").Replace("\n", "&rn");//прочее и телефоны
                    if (modArenda[0] == null) modArenda[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, floorGlobal, 7, roomGlobal] == null && textBox26.Text == "") && (arenda[k, floorGlobal, 7, roomGlobal] != textBox26.Text.Replace(";", ",")))
                {
                    modArenda[7] = textBox26.Text.Replace(";", ",");//Папка арендатора
                    if (modArenda[0] == null) modArenda[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (modArenda[0] != null) dataModA = arenda[k, floorGlobal, 0, roomGlobal];//изменение имеет место, запишем в dataModA значение даты до изменения

               // if (!(textBox10.Text == "" && textBox11.Text == ""))
               // {//сюда функцию запишем счетчики
               //     WriteSchet(floorGlobal, roomGlobal, dateTimePicker2.Value, textBox10.Text, textBox11.Text, comboBox14.Text, comboBox18.Text, comboBox16.Text, comboBox4.Text);
              //  }

                if (counters[0, floorGlobal, 0, roomGlobal] != dateTimePicker2.Value.ToShortDateString().Replace(";", ","))
                {
                    modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                if (!(counters[0, floorGlobal, 1, roomGlobal] == null && textBox10.Text == "") && (counters[0, floorGlobal, 1, roomGlobal] != textBox10.Text.Replace(";", ",")))
                {
                    modCounters[1] = textBox10.Text.Replace(";", ",");//показания электроэнергии
                    if (modCounters[0] == null) modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                if (!(counters[0, floorGlobal, 2, roomGlobal] == null && textBox11.Text == "") && (counters[0, floorGlobal, 2, roomGlobal] != textBox11.Text.Replace(";", ",")))
                {
                    modCounters[2] = textBox11.Text.Replace(";", ",");//показания водомера
                    if (modCounters[0] == null) modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                if (!(counters[0, floorGlobal, 3, roomGlobal] == null && comboBox14.Text == "") && (counters[0, floorGlobal, 3, roomGlobal] != comboBox14.Text.Replace(";", ",")))
                {
                    modCounters[3] = comboBox14.Text.Replace(";", ",");//номер электросчетчика
                    if (modCounters[0] == null) modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                if (!(counters[0, floorGlobal, 4, roomGlobal] == null && comboBox18.Text == "") && (counters[0, floorGlobal, 4, roomGlobal] != comboBox18.Text.Replace(";", ",")))
                {
                    modCounters[4] = comboBox18.Text.Replace(";", ",");//коэффициент трансформации
                    if (modCounters[0] == null) modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                if (!(counters[0, floorGlobal, 5, roomGlobal] == null && comboBox16.Text == "") && (counters[0, floorGlobal, 5, roomGlobal] != comboBox16.Text.Replace(";", ",")))
                {
                    modCounters[5] = comboBox16.Text.Replace(";", ",");//номер водомера
                    if (modCounters[0] == null) modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                //расчет
                if (!(counters[0, floorGlobal, 7, roomGlobal] == null && comboBox4.Text == "") && (counters[0, floorGlobal, 7, roomGlobal] != comboBox4.Text.Replace(";", ",")))
                {
                    modCounters[7] = comboBox4.Text.Replace(";", ",");//количество сотрудников (для воды) 
                    if (modCounters[0] == null) modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }

                if (checkBox5.Checked)
                {
                    if (!(counters[k, floorGlobal, 8, roomGlobal] == null && comboBox21.Text == "") && (counters[k, floorGlobal, 8, roomGlobal] != comboBox21.Text.Replace(";", ",")))
                    {
                        modCounters[8] = comboBox21.Text.Replace(";", ",");//корпус
                        if (modCounters[0] == null) modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                    }
                    if (!(counters[k, floorGlobal, 9, roomGlobal] == null && comboBox21.Text == "") && (counters[k, floorGlobal, 9, roomGlobal] != comboBox21.Text.Replace(";", ",")))
                    {
                        modCounters[9] = comboBox21.Text.Replace(";", ",");//помещение
                        if (modCounters[0] == null) modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                    }
                    if (!(counters[k, floorGlobal, 10, roomGlobal] == null && ToEt(counters[k, floorGlobal, 8, roomGlobal], counters[k, floorGlobal, 9, roomGlobal]) == "") && (counters[k, floorGlobal, 10, roomGlobal] != ToEt(counters[k, floorGlobal, 8, roomGlobal], counters[k, floorGlobal, 9, roomGlobal])))
                    {
                        modCounters[10] = ToEt(counters[k, floorGlobal, 8, roomGlobal], counters[k, floorGlobal, 9, roomGlobal]);//этаж
                        if (modCounters[0] == null) modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                    }
                    if (!(counters[k, floorGlobal, 11, roomGlobal] == null && comboBox22.Text == "") && (counters[k, floorGlobal, 11, roomGlobal] != comboBox22.Text.Replace(";", ",")))
                    {
                        modCounters[11] = comboBox22.Text.Replace(";", ",");//% кВт
                        if (modCounters[0] == null) modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                    }
                    if (!(counters[k, floorGlobal, 12, roomGlobal] == null && textBox21.Text == "") && (counters[k, floorGlobal, 12, roomGlobal] != textBox21.Text.Replace(";", ",")))
                    {
                        modCounters[12] = textBox21.Text.Replace(";", ",");//С постоянная величина кВт
                        if (modCounters[0] == null) modCounters[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                    }
                }
            }
            time3 = 0;
            //button19.Enabled = false;
            this.Enabled = false;
            timer3.Interval = 100;
            timer3.Enabled = true;
        }
        string ToEt(string korpus, string pomeshenie)
        {
            for (int et = 0; et < 4; et++)
            {
                for (int pomesh = 0; pomesh < maxRoom; pomesh++)
                {
                    if (data[et, pomesh, 0] != "" && data[et, pomesh, 1] != "")
                    {
                        if (data[et, 0, pomesh] == korpus && data[et, 1, pomesh] == pomeshenie) return et.ToString();
                    }
                    else break;
                }
            }
            return "";
        }
        int time3 = 0;
        private int FindPom(string korp, string pomesh)
        {
            int rezult = -1;
            for (int i = 0; i <= countRoom[floorGlobal]; i++)
            {
                if (data[floorGlobal, 0, i] == korp && data[floorGlobal, 1, i] == pomesh)
                {
                    rezult = i;
                    break;
                }
            }
            return rezult;
        }

        private void button20_Click(object sender, EventArgs e)
        {
           // RasxodFull
        }
        private void comboBox6_TextChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "" && comboBox6.Text != "")
            {
                if (GlobalP == 21 && UserKey == "admin") button9.Enabled = true;
                roomGlobal = FindPom(comboBox5.Text.Replace(";", ","), comboBox6.Text.Replace(";", ","));
                if (roomGlobal != -1)
                {
                    ClearCB2();
                    comboBox7.Text = data[floorGlobal, 2, roomGlobal];
                    comboBox8.Text = data[floorGlobal, 3, roomGlobal];
                    comboBox9.Text = data[floorGlobal, 4, roomGlobal];
                    comboBox10.Text = data[floorGlobal, 5, roomGlobal];
                    comboBox11.Text = data[floorGlobal, 6, roomGlobal];
                    comboBox12.Text = data[floorGlobal, 7, roomGlobal];
                    comboBox13.Text = data[floorGlobal, 8, roomGlobal];
                    comboBox14.Text = data[floorGlobal, 9, roomGlobal];
                    comboBox15.Text = data[floorGlobal, 10, roomGlobal];
                    textBox4.Text = data[floorGlobal, 11, roomGlobal];
                    comboBox16.Text = data[floorGlobal, 12, roomGlobal];
                    comboBox17.Text = data[floorGlobal, 13, roomGlobal];
                    textBox5.Text = data[floorGlobal, 14, roomGlobal];
                    if (data[floorGlobal, 15, roomGlobal] != "1") checkBox1.Checked = true;
                    else checkBox1.Checked = false;
                    comboBox18.Text = data[floorGlobal, 15, roomGlobal];
                    textBox6.Text = data[floorGlobal, 16, roomGlobal];
                    textBox7.Text = data[floorGlobal, 17, roomGlobal];
                    textBox8.Text = data[floorGlobal, 18, roomGlobal];
                    textBox9.Text = data[floorGlobal, 19, roomGlobal];
                    if (data[floorGlobal, 20, roomGlobal] != null) dateTimePicker3.Value = DateTime.Parse(data[floorGlobal, 20, roomGlobal]);
                    textBox12.Text = data[floorGlobal, 21, roomGlobal];
                    textBox14.Text = data[floorGlobal, 22, roomGlobal];
                    textBox15.Text = data[floorGlobal, 23, roomGlobal];
                    textBox16.Text = data[floorGlobal, 24, roomGlobal];
                    if (data[floorGlobal, 25, roomGlobal] != null) dateTimePicker4.Value = DateTime.Parse(data[floorGlobal, 25, roomGlobal]);
                    textBox13.Text = data[floorGlobal, 26, roomGlobal];
                    if (data[floorGlobal, 27, roomGlobal] != null) textBox19.Text = data[floorGlobal, 27, roomGlobal];//кв.м.
                    if (data[floorGlobal, 28, roomGlobal] != null) textBox22.Text = data[floorGlobal, 28, roomGlobal];//Планировка
                    
                    if (data[floorGlobal, 29, roomGlobal] != null) textBox23.Text = data[floorGlobal, 29, roomGlobal];//Однолинейная схема
                    if (data[floorGlobal, 30, roomGlobal] != null) textBox24.Text = data[floorGlobal, 30, roomGlobal];//План электросети
                    if (data[floorGlobal, 31, roomGlobal] != null) textBox25.Text = data[floorGlobal, 31, roomGlobal];//План водоснабжения
                    if (arenda[0, floorGlobal, 0, roomGlobal] != null) dateTimePicker1.Value = DateTime.Parse(arenda[0, floorGlobal, 0, roomGlobal]);
                    if (arenda[0, floorGlobal, 7, roomGlobal] != null) textBox26.Text = arenda[0, floorGlobal, 7, roomGlobal];
                    comboBox1.Text = arenda[0, floorGlobal, 1, roomGlobal];
                    comboBox2.Text = arenda[0, floorGlobal, 2, roomGlobal];
                    comboBox3.Text = arenda[0, floorGlobal, 3, roomGlobal];
                    comboBox4.Text = arenda[0, floorGlobal, 4, roomGlobal];
                    textBox17.Text = arenda[0, floorGlobal, 5, roomGlobal];
                    if (arenda[0, floorGlobal, 6, roomGlobal] != null) richTextBox3.Text = arenda[0, floorGlobal, 6, roomGlobal].Replace("&rn", "\n");
                    if (counters[0, floorGlobal, 0, roomGlobal] != null) dateTimePicker2.Value = DateTime.Parse(counters[0, floorGlobal, 0, roomGlobal]);
                    textBox10.Text = counters[0, floorGlobal, 1, roomGlobal];
                    textBox11.Text = counters[0, floorGlobal, 2, roomGlobal];
                }
                //найти индекс помещения. Если совпадений нет, то: countRoom[floorGlobal]++; roomGlobal=countRoom[floorGlobal];
                Unlock(true);
            }
            else
            {                
                button9.Enabled = false;//обвести контур заблокировано
                Unlock(false);                
            }
        }
        
        void Unlock(bool flag)//true - разблокировать, false - заблокировать кнопку записи
        {            
            if (flag)
            {
                if (comboBox1.Text == "" || (comboBox1.Text.IndexOf(" ") > -1 && comboBox1.Text != " ") || comboBox1.Text == "свободно")
                {
                    button19.Enabled = true;//разблокировать
                    button19.BackColor = Color.DodgerBlue;
                }
                else
                {
                    button19.Enabled = false;//"Cохранить изменения" заблокировано
                    button19.BackColor = Color.LightGray;
                }
            }
            else
            {
                button19.Enabled = false;//"Cохранить изменения" заблокировано
                button19.BackColor = Color.LightGray;
            }
        }
        private void button21_Click(object sender, EventArgs e)
        {
            List<string> Arend1 = new List<string>();//арендатор
            List<string> Arend2 = new List<string>();//должность
            List<string> data1 = new List<string>();//корпус
            List<string> data2 = new List<string>();//помещение
            List<string> data3 = new List<string>();//запитка от ТП
            List<string> data4 = new List<string>();//запитка от СП
            int break1 = 0;
            for (int i = 0; i < maxRoom; i++)
            {
                if (break1 == 6) break;
                else break1 = 0;
                if (arenda[0, floorGlobal, 1, i] != null) Arend1.Add(arenda[0, floorGlobal, 1, i]);
                else break1++;
                if (arenda[0, floorGlobal, 3, i] != null) Arend2.Add(arenda[0, floorGlobal, 3, i]);
                else break1++;
                if (data[floorGlobal, 0, i] != null) data1.Add(data[floorGlobal, 0, i]);
                else break1++;
                if (data[floorGlobal, 1, i] != null) if (data[floorGlobal, 0, i] == comboBox5.Text) data2.Add(data[floorGlobal, 1, i]);
                    else break1++;
                if (data[floorGlobal, 2, i] != null) data3.Add(data[floorGlobal, 2, i]);
                else break1++;
                if (data[floorGlobal, 3, i] != null) data4.Add(data[floorGlobal, 3, i]);
                else break1++;
            }
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(Arend1.Distinct().ToArray());
            comboBox3.Items.Clear();
            comboBox3.Items.AddRange(Arend2.Distinct().ToArray());
            comboBox5.Items.Clear();
            comboBox5.Items.AddRange(data1.Distinct().ToArray());
            comboBox6.Items.Clear();
            comboBox6.Items.AddRange(data2.Distinct().ToArray());
            comboBox7.Items.Clear();
            comboBox7.Items.AddRange(data3.Distinct().ToArray());
            comboBox8.Items.Clear();
            comboBox8.Items.AddRange(data4.Distinct().ToArray());
        }

        void ClearCB()
        {
            comboBox5.Text = "";
            comboBox6.Text = "";
            ClearCB2();
        }
        void ClearCB2()
        {
            comboBox7.Text = "";
            comboBox8.Text = "";
            comboBox9.Text = "";
            comboBox10.Text = "";
            comboBox11.Text = "";
            comboBox12.Text = "";
            comboBox13.Text = "";
            comboBox14.Text = "";
            comboBox15.Text = "";
            textBox4.Text = "";
            richTextBox3.Text = "";
            comboBox16.Text = "";
            comboBox17.Text = "";
            textBox5.Text = "";
            comboBox18.Text = "1";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox12.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox13.Text = "";
            textBox19.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox22.Text = "";
            textBox23.Text = "";
            textBox24.Text = "";
            textBox25.Text = "";
            textBox26.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            checkBox1.Checked = false;
        }
        private void button22_Click(object sender, EventArgs e)
        {
            ClearCB();
        }

        private void button23_Click(object sender, EventArgs e)
        {
            int[] massivA = new int[200];
            int j = 0;
            for (int i = 0; i < 200; i++)
            {
                if (arenda[0, floorGlobal, 1, i] == comboBox1.Text.Replace(";", ","))
                {
                    if (koord[floorGlobal, 0, i] != 0)
                    {
                        massivA[j] = i;
                        j++;
                    }
                }
            }
            Point[,] OutP = new Point[j, 20];

            for (int i = 0; i < j; i++)
            {
                for (int i1 = 0; i1 < 40; i1++)
                {
                    if (koord[floorGlobal, 2 * i1, massivA[i]] != 0)
                    {
                        OutP[i, i1].X = koord[floorGlobal, 2 * i1, massivA[i]];//0.2.4.6.8...38
                        OutP[i, i1].Y = koord[floorGlobal, 2 * i1 + 1, massivA[i]];//1.3.5.7.9...39
                    }
                    else break;
                }
            }

            for (int i = 0; i < j; i++)
            {
                for (int i1 = 0; i1 < 20; i1++)
                {
                    if (OutP[i, i1].X != 0) richTextBox1.Text += OutP[i, i1].X.ToString() + "," + OutP[i, i1].Y.ToString() + "\r\n";
                    else break;
                }
            }



            //вывести OutP, j1, 
            /*
                                 for(int j=0;j<40;j++)
                {
                    if (s.IndexOf(";") > -1)
                    {
                        koord[floor, j, room] = int.Parse(s.Substring(0, s.IndexOf(";")));
                        s = s.Substring(s.IndexOf(";") + 1);
                    }
                    else
                    {
                        koord[floor, j, room] = int.Parse(s);
                        break;
                    }
                }*/
            // richTextBox1.Text += massivA[i].ToString() + "\r\n";
        }

        private void button24_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 200; i++)
            {
                if (koord[floorGlobal, 0, i] != 0)
                {
                    for (int i1 = 0; i1 < 40; i1++)
                    {
                        if (koord[floorGlobal, 2 * i1, i] != 0)
                        {
                            richTextBox1.Text += koord[floorGlobal, 2 * i1, i].ToString() + " " + koord[floorGlobal, 2 * i1 + 1, i] + "\r\n";
                        }
                        else break;
                    }
                    richTextBox1.Text += "\r\n";
                }
            }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            bitmap = new Bitmap(pictureBox1.Image);
            g = Graphics.FromImage(bitmap);
            for (int i = 0; i < 200; i++)
            {
                int j = 0;
                for (; j < 40; j++)
                {
                    if (koord[floorGlobal, j, i] == 0) break;
                }
                j = j / 2;
                if (j > 0)
                {
                    Point[] figura = new Point[j + 1];
                    for (int i1 = 0; i1 < j; i1++)
                    {
                        figura[i1].X = koord[floorGlobal, 2 * i1, i];
                        figura[i1].Y = koord[floorGlobal, 2 * i1 + 1, i];
                    }
                    figura[j].X = koord[floorGlobal, 0, i];
                    figura[j].Y = koord[floorGlobal, 1, i];
                    g.DrawPolygon(new Pen(Color.Green, 4), figura);
                }
            }
            pictureBox1.Image.Dispose();
            pictureBox1.Image = bitmap;
            g.Dispose();
        }

        private void timer3_Tick(object sender, EventArgs e)//таймер записи инфы в файл
        {
            File = System.IO.File.ReadAllLines(@"Data.txt", Encoding.Default).ToList();
            if (time3 == 10)
            {
                //button19.Enabled = true;
                this.Enabled = true;
                button19.Text = "Ошибка! Повторить...";
                timer3.Enabled = false;
            }
            else
            {
                timer3.Interval = 3000;
                time3++;
                button19.Text = "Сохраняется...";
                progressBar1.Value = time3;
            }
            if (File[0] == "=zablokirovano=") timer3.Enabled = true;
            else
            {
                //button19.Enabled = true;
                this.Enabled = true;
                button19.Text = "Сохранить изменения";
                progressBar1.Value = 0;                
                File[0] = "=zablokirovano=";
                System.IO.File.WriteAllLines(@"Data.txt", File, Encoding.Default);
                //основной код
                LoadDB();
                SaveDB();                
                timer3.Enabled = false;
                if(outL2et_pom[0]!=7)SelectL2(listBox2.SelectedItem.ToString()); //обновим значения
            }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            UserKey = "voda";
            if (UserKey == "voda")
            {
                dateTimePicker1.Enabled = false;
                //comboBox1.Enabled = false;
                comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых арендаторов
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых корпусов
                comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых помещений
                comboBox7.Enabled = false;
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;
                comboBox10.Enabled = false;
                comboBox11.Enabled = false;
                comboBox12.Enabled = false;
                comboBox13.Enabled = false;
                comboBox14.Enabled = false;
                comboBox15.Enabled = false;
                comboBox18.Enabled = false;
                textBox4.Enabled = false;
                checkBox1.Enabled = false;
                textBox10.Enabled = false;
                textBox12.Enabled = false;
                dateTimePicker3.Enabled = false;
                tabPage1.Parent = null;
                tabControl2.SelectedIndex = 1;
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            UserKey = "arenda";
            if (UserKey == "arenda")
            {
                comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых корпусов
                comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых помещений
                comboBox7.Enabled = false;
                comboBox8.Enabled = false;
                comboBox9.Enabled = false;
                comboBox10.Enabled = false;
                comboBox11.Enabled = false;
                comboBox12.Enabled = false;
                comboBox13.Enabled = false;
                comboBox14.Enabled = false;
                comboBox15.Enabled = false;
                comboBox16.Enabled = false;
                comboBox17.Enabled = false;
                comboBox18.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                checkBox1.Enabled = false;
                textBox10.Enabled = false;
                textBox11.Enabled = false;
                textBox12.Enabled = false;
                textBox13.Enabled = false;
                dateTimePicker2.Enabled = false;
                dateTimePicker3.Enabled = false;
                dateTimePicker4.Enabled = false;
                tabPage1.Parent = null;
            }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            UserKey = "electro";
            if (UserKey == "electro")
            {
                dateTimePicker1.Enabled = false;
                //comboBox1.Enabled = false;
                comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых арендаторов
                comboBox2.Enabled = false;
                comboBox3.Enabled = false;
                comboBox4.Enabled = false;
                comboBox5.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых корпусов
                comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод новых помещений
                comboBox16.Enabled = false;
                comboBox17.Enabled = false;
                textBox5.Enabled = false;
                textBox11.Enabled = false;
                textBox13.Enabled = false;
                dateTimePicker4.Enabled = false;
                tabPage1.Parent = null;
            }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            int etaz = 0;
            for (int i = 0; i < File.Count; i++)
            {
                if (File[i].IndexOf("[etaz_") > -1)
                {
                    countRoom[etaz] = int.Parse(File[i].Substring(8, File[i].Length - 8)) - 1;//количество помещений на этаже
                    //if (countRoom[floor] > maxRoom) maxRoom = countRoom[floor];
                    etaz++;
                }
            }
            label1.Text = etaz.ToString();
        }
        //bool selectArenda = true;
        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                //if (selectArenda) 
                listBox1.Items.Clear();
                listBox1.Sorted = true;
                for (int et = 0; et < 4; et++)
                {
                    for (int i = 0; i < maxRoom; i++)
                    {
                        if (arenda[0, et, 1, i] != null)
                        {
                            if (arenda[0, et, 1, i] == comboBox1.Text.Replace(";", ","))
                            {
                                comboBox2.Text = arenda[0, et, 2, i];
                                comboBox3.Text = arenda[0, et, 3, i];
                                comboBox4.Text = arenda[0, et, 4, i];
                                textBox17.Text = arenda[0, et, 5, i];
                                if (arenda[0, et, 6, i] != null) richTextBox3.Text = arenda[0, et, 6, i].Replace("&rn", "\n");
                                else richTextBox3.Clear();

                              //  if (selectArenda)
                              //  {
                                    for (int p1 = 0; p1 < maxRoom; p1++)//запишем в listbox все помещения этого арендатора
                                    {
                                        for (int et1 = 0; et1 < 4; et1++)
                                        {
                                            if (arenda[0, et1, 1, p1] == comboBox1.Text.Replace(";", ","))//ошибка?
                                            {//надо записать этаж, корпус, помещение... 
                                                listBox1.Items.Add("{" + et1 + "} к.{" + data[et1, 0, p1] + "} пом.{" + data[et1, 1, p1] + "}");
                                            }
                                        }
                                    }
                                    //if (listBox1.Items.Count > -1) listBox1.SelectedIndex = 0;
                              //  }                                
                                goto Lab;
                            }
                        }
                    }
                }
            }
        Lab: ;//selectArenda=true;
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            //comboBox6.Text = "";//раскомментить по окончании
            List<string> data2 = new List<string>();//помещение
            for (int i = 0; i < maxRoom; i++)
            {
                if (data[floorGlobal, 1, i] != null)
                {
                    if (data[floorGlobal, 0, i] == comboBox5.Text) data2.Add(data[floorGlobal, 1, i]);
                }
                //else break;
            }
            comboBox6.Items.Clear();
            data2.Sort();
            comboBox6.Items.AddRange(data2.Distinct().ToArray());
            if (comboBox5.Text != "" && comboBox6.Text != "")
            {
                Unlock(true);
            }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            UserKey = "admin";
        }

        private void button31_Click(object sender, EventArgs e)
        {
            OutputSorting("ToLongName");
        }
        List<string> Sort()
        {
            List<string> Arendator = new List<string>();
            for (int et1 = 0; et1 < 4; et1++)
            {
                for (int pomesh = 0; pomesh < maxRoom; pomesh++)
                {
                    if(arenda[0,et1, 1, pomesh]!=null) Arendator.Add(arenda[0,et1, 1, pomesh].ToString());
                }
            }
            Arendator = Arendator.Distinct().ToList();
            List<string> SortArendator = new List<string>();
            for (int i = 0; i < Arendator.Count; i++)
            {
                bool Bolshe = true;
                for (int j = 0; j < SortArendator.Count; j++)
                {
                    if (String.Compare(ToShort(Arendator[i]), ToShort(SortArendator[j]), StringComparison.CurrentCultureIgnoreCase) <= 0)
                    {
                        SortArendator.Insert(j,Arendator[i].ToString());
                        Bolshe = false;
                        break;
                    }
                }
                if (Bolshe)
                {
                    SortArendator.Add(Arendator[i]);
                }
            }
            return SortArendator;
        }
        String ToShort(string stroka)
        {
            int find1 = stroka.IndexOf(" \"") + 2;
            int find2 = stroka.IndexOf(" ") + 1;
            if (find1 > 1)
            {
                return stroka.Substring(find1);
            }
            else if (find2 > 0)
            {
                return stroka.Substring(find2);
            }
            else return stroka;
        }
        List<string> ArendaLong(string ParametrSort)
        {
            List<string> ArendatorS = new List<string>();
            List<string> ArendatorLong = new List<string>();
          /*  for (int j = 0; j < 4; j++)//этаж
            {
                for (int i = 0; i < maxRoom; i++)//помещение
                {
                    if (arenda[0, j, 1, i] != null)
                    {
                        int find1 = arenda[0, j, 1, i].IndexOf(" \"") + 2;
                        int find2 = arenda[0, j, 1, i].IndexOf(" ") + 1;
                        if (find1 > 1)
                        {
                            ArendatorS.Add(arenda[0, j, 1, i].Substring(find1));
                        }
                        else if (find2 > 0)
                        {
                            ArendatorS.Add(arenda[0, j, 1, i].Substring(find2));
                        }
                        else ArendatorS.Add(arenda[0, j, 1, i].ToString());//арендатор);
                    }
                }
            }*/
            //ArendatorS.Sort();
            ArendatorS.AddRange(Sort().ToArray());
            //ArendatorS = ArendatorS.Distinct().ToList();//убираем совпадение строк
            for (int i1 = 0; i1 < ArendatorS.Count; i1++)//хрень из отсортированного в новый список со старым названием.
            {
                switch (ParametrSort)
                {
                    case "ToLongName": ArendatorLong.Add(ToLongName(ArendatorS[i1]));
                        break;
                    case "ToLongNamePomes": ArendatorLong.Add(ToLongNamePomes(ArendatorS[i1]));
                        break;
                    case "ToLongNameSchet": ArendatorLong.Add(ToLongNameSchet(ArendatorS[i1]));
                        break;
                }

            }
            return ArendatorLong;
        }
        void OutputSorting(string ParametrSort)
        {
            dataGridView1.Visible = false;
            listBox2.Visible = false;
            richTextBox2.Visible = true;
            List<string> ArendatorLong = ArendaLong(ParametrSort);
            listBox1.Items.Clear();
            listBox1.Sorted = false;
            listBox1.Items.AddRange(ArendatorLong.ToArray());
            richTextBox2.Clear();
            for (int i = 0; i < ArendatorLong.Count; i++)
            {
                richTextBox2.Text += ArendatorLong[i] + "\r\n";
            }
        }
        string ToLongName(string ArendaStringList)
        {
           /* if (ArendaStringList != "свободно")
            {
               for (int j = 0; j < 4; j++)//этаж
                {
                    for (int i = 0; i < maxRoom; i++)//помещение
                    {
                        if (arenda[0, j, 1, i] != null)
                        {
                            if (arenda[0, j, 1, i].IndexOf(" " + ArendaStringList) > -1 || arenda[0, j, 1, i].IndexOf(" \"" + ArendaStringList) > -1) return arenda[0, j, 1, i].ToString();
                        }
                    }
                }
            }
            else return "свободно";
            return "";*/
            return ArendaStringList;
        }
        string ToLongNamePomes(string ArendaStringList)
        {
            string s = "";
            for (int j = 0; j < 4; j++)//этаж
            {
                for (int i = 0; i < maxRoom; i++)//помещение
                {
                    if (arenda[0, j, 1, i] != null)
                    {
                        if (arenda[0, j, 1, i].IndexOf(" " + ArendaStringList) > -1 || arenda[0, j, 1, i].IndexOf(" \"" + ArendaStringList) > -1 || (ArendaStringList == "свободно" && arenda[0, j, 1, i]==ArendaStringList))
                        {
                            if (s == "") s = arenda[0, j, 1, i].ToString();
                            s+= "; корп." + data[j, 0, i].ToString() + ", помещ." + data[j, 1, i].ToString();
                        }
                    }
                }
            }
            return s;
        }
        string ToLongNameSchet(string ArendaStringList)
        {
            string s = "";
            for (int j = 0; j < 4; j++)//этаж
            {
                for (int i = 0; i < maxRoom; i++)//помещение
                {
                    if (arenda[0, j, 1, i] != null)
                    {
                        if (arenda[0, j, 1, i].IndexOf(" " + ArendaStringList) > -1 || arenda[0, j, 1, i].IndexOf(" \"" + ArendaStringList) > -1 || (ArendaStringList == "свободно" && arenda[0, j, 1, i] == ArendaStringList))
                        {
                            if (s == "") s = arenda[0, j, 1, i].ToString();
                            if (data[j, 9, i] != null) s += "; сч." + data[j, 9, i].ToString() + ", коэфф=" + data[j, 15, i].ToString();
                        }
                    }
                }
            }
            return s;
        }
        private void button32_Click(object sender, EventArgs e)
        {
            OutputSorting("ToLongNamePomes");
        }

        private void button33_Click(object sender, EventArgs e)//арендаторы и счетчики
        {
            dataGridView1.Visible = false;
            listBox2.Visible = false;
            richTextBox2.Visible = true;
            List<string> ArendatorS = new List<string>();
            List<string> ArendatorPomesh = new List<string>();
            for (int j = 0; j < 4; j++)//этаж
            {
                for (int i = 0; i < maxRoom; i++)//помещение
                {
                    if (arenda[0, j, 1, i] != null)
                    {
                        ArendatorS.Add(arenda[0, j, 1, i].ToString());//арендатор);
                    }
                }
            }
            listBox1.Items.Clear();
            ArendatorS = ArendatorS.Distinct().ToList();//убираем совпадение строк
            ArendatorS.Sort();
            for (int i1 = 0; i1 < ArendatorS.Count; i1++)
            {
                string s = ArendatorS[i1].ToString();
                for (int j = 0; j < 4; j++)//этаж
                {
                    for (int i = 0; i < maxRoom; i++)//помещение
                    {
                        if (arenda[0, j, 1, i] == ArendatorS[i1])//j-этаж, i-номер помещения
                        {
                            if (data[j, 9, i]!=null) s += "; сч." + data[j, 9, i].ToString() + ", коэфф=" + data[j, 15, i].ToString();
                        }
                    }
                }
                ArendatorPomesh.Add(s);
            }
            listBox1.Items.AddRange(ArendatorPomesh.ToArray());
            richTextBox2.Clear();
            for (int i = 0; i < ArendatorPomesh.Count; i++) richTextBox2.Text += ArendatorPomesh[i] + "\r\n";
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker6.Value = dateTimePicker5.Value;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            dataGridView1.Visible = false;
            listBox2.Visible = false;
            richTextBox2.Visible = true;
            List<string> ArendatorS = new List<string>();
            List<string> ArendatorPomesh = new List<string>();
            for (int j = 0; j < 4; j++)//этаж
            {
                for (int i = 0; i < maxRoom; i++)//помещение
                {
                    if (arenda[0, j, 1, i] != null)
                    {
                        ArendatorS.Add(arenda[0, j, 1, i].ToString());//арендатор);
                    }
                }
            }
            ArendatorS = ArendatorS.Distinct().ToList();//убираем совпадение строк
            ArendatorS.Sort();
            double summArenda = 0;
            double summSobstvennoe = 0;
            string minusR = "";
            for (int i1 = 0; i1 < ArendatorS.Count; i1++)
            {
                ArendatorPomesh.Add(ArendatorS[i1].ToString());
                for (int j = 0; j < 4; j++)//этаж
                {
                    for (int i = 0; i < maxRoom; i++)//помещение
                    {
                        if (arenda[0, j, 1, i] == ArendatorS[i1])//j-этаж, i-номер помещения
                        {
                            if (data[j, 9, i] != null)
                            {
                                ArendatorPomesh.Add("сч." + data[j, 9, i] + ", коэфф=" + data[j, 15, i]+ ", расход=" + Rasxod(j, i, dateTimePicker5.Value).ToString());
                                if (Rasxod(j, i, dateTimePicker5.Value) >= 0)//расход больше нуля
                                {
                                    if (ArendatorS[i1].IndexOf("свободн") > -1 || ArendatorS[i1].IndexOf("ОАО\"Компания Импульс\"") > -1) summSobstvennoe += Rasxod(j, i, dateTimePicker5.Value);
                                    else summArenda += Rasxod(j, i, dateTimePicker5.Value);
                                }
                                else//расход меньше нуля
                                {
                                    minusR += ArendatorS[i1] + ", ";
                                }
                            }                                
                        }
                    }
                }
            }
            richTextBox2.Clear();
            richTextBox2.Text += "Общий расход электроэнергии арендаторов составил: " + summArenda.ToString() + "кВт.\r\n";
            richTextBox2.Text += "Общий расход на собственное потребление электроэнергии составил: " + summSobstvennoe.ToString() + "кВт.\r\n";
            if(minusR!="")richTextBox2.Text += "Ошибки с показаниями у:" + minusR+"\r\n";
            for (int i = 0; i < ArendatorPomesh.Count; i++) richTextBox2.Text += ArendatorPomesh[i] + "\r\n";
        }
        private double Rasxod(int EtazR, int PomesR, DateTime dataMes)
        {
            double rezult = 0;//№ этажа, №помещения, дата предыдущего съема, дата текущая.
            DateTime dataPred = new DateTime(dataMes.Year, dataMes.Month - 1, 15);
            DateTime dataTekus = new DateTime(dataMes.Year, dataMes.Month + 1, 14);
            double sbros = 0;
            for (int k = 0; k < 60; k++)
            {
                if (counters[k, EtazR, 1, PomesR] != null)
                {
                    if (DateTime.Parse(counters[k, EtazR, 0, PomesR]) > dataPred && DateTime.Parse(counters[k, EtazR, 0, PomesR]) < dataTekus)
                    {
                        if (rezult != 0)
                        {
                            if (double.Parse(counters[k, EtazR, 1, PomesR]) != 10000 || double.Parse(counters[k, EtazR, 1, PomesR]) != 100000 || double.Parse(counters[k, EtazR, 1, PomesR]) != 1000000 || double.Parse(counters[k, EtazR, 1, PomesR]) != 10000000) rezult -= double.Parse(counters[k, EtazR, 1, PomesR]) * int.Parse(counters[k, EtazR, 4, PomesR]);
                            else sbros = double.Parse(counters[k, EtazR, 1, PomesR]) * int.Parse(counters[k, EtazR, 4, PomesR]);
                        }
                        else rezult = double.Parse(counters[k, EtazR, 1, PomesR]) * int.Parse(counters[k, EtazR, 4, PomesR]);//умножим на коэффициент
                    }
                }
                else break;
            }
            return Math.Round(rezult + sbros, 1);
        }
        DateTime ToDateRaschet(DateTime dat1)
        {//определяет к какому расчетному периоду относится дата и возвращает 01 число расчетного месяца.
            if (dat1.Day >= 7 && dat1.Day <= 31) return new DateTime(dat1.Year, dat1.Month, 1);
            else
            {
                return new DateTime(dat1.Year, dat1.Month, 1).AddMonths(-1);
            }
        }
        void RasxodFull(int EtazR, int PomesR, DateTime dataMes)
        {//посчитать расход за текущий период и последующие, и пересчитать за последующие с записью в БД
            if(EtazR!=7)
            {
                List<string> DateList = new List<string>();
                string DataS = "";
                for (int k = 59; k > -1; k--)
                {
                    if (counters[k, EtazR, 1, PomesR] != null)//если показания по ЭЭ существуют, то в лист запишем оригинальную дату (01 число расчетного месяца)
                    {
                        if (ToDateRaschet(DateTime.Parse(counters[k, EtazR, 0, PomesR])).ToShortDateString() != DataS)
                        {
                            if (DateTime.Parse(counters[k, EtazR, 0, PomesR]) >= dataMes.Date)//ошибка >= ?
                            {
                                DataS = ToDateRaschet(DateTime.Parse(counters[k, EtazR, 0, PomesR])).ToShortDateString();
                                DateList.Add(DataS);
                            }
                        }
                    }
                }
                for (int i = 0; i < DateList.Count; i++)
                {
                    Rasxod3(EtazR, PomesR, DateTime.Parse(DateList[i]));
                }
            }            
        }
        private double Rasxod3(int EtazR, int PomesR, DateTime dataMes)//основной расход с записью в БД
        {
            double value1 = 0;
            double value2 = 0;
            double summa = 0;
            int koeff = 1;
            double predRasxodMinus = 0;
            double rezult = 0;//результат от начальных показаний текущего счетчика
            string Nschet = "";//номер счетчика
            DateTime dataPred1 = new DateTime(dataMes.Year, dataMes.Month, 24).AddMonths(-1);//с 24 числа предыдущего месяца
            DateTime dataPred2 = new DateTime(dataMes.Year, dataMes.Month, 7);//до 7-го числа текущего месяца.(диапазон)
            DateTime dataTekus1 = new DateTime(dataMes.Year, dataMes.Month, 24);//с 24-го числа текущего месяца
            DateTime dataTekus2 = new DateTime(dataMes.Year, dataMes.Month, 7).AddMonths(1);//до 7-го числа следующего месяца.(диапазон)
            int DataK = -1;//индекс расчетной даты (куда запишется значение отчетного расхода)
            for (int k = 59; k > -1; k--)
            {
                if (counters[k, EtazR, 1, PomesR] != null)
                {
                    if (DateTime.Parse(counters[k, EtazR, 0, PomesR]) > dataPred1 && DateTime.Parse(counters[k, EtazR, 0, PomesR]) <= dataPred2)
                    {
                        value1 = double.Parse(counters[k, EtazR, 1, PomesR]);//начальные показания
                        Nschet = counters[k, EtazR, 3, PomesR];
                        if (counters[k, EtazR, 6, PomesR] == null) counters[k, EtazR, 6, PomesR] = "0";//вручную пропишем нулевой расход на начало периода в БД
                        else
                        {
                            if (counters[k, EtazR, 6, PomesR]!="-") if (double.Parse(counters[k, EtazR, 6, PomesR]) < 0) predRasxodMinus = double.Parse(counters[k, EtazR, 6, PomesR]);
                        }
                    }
                    if (DateTime.Parse(counters[k, EtazR, 0, PomesR]) > dataTekus1 && DateTime.Parse(counters[k, EtazR, 0, PomesR]) <= dataTekus2)// && value2==0)
                    {
                        value2 = double.Parse(counters[k, EtazR, 1, PomesR]);//конечные показания
                        koeff = int.Parse(counters[k, EtazR, 4, PomesR]);//ошибка при k=1,EtazR=1,PomesR=1??
                        DataK = k;
                    }
                    if (DateTime.Parse(counters[k, EtazR, 0, PomesR]) > dataPred2 && DateTime.Parse(counters[k, EtazR, 0, PomesR]) < dataTekus2)
                    {//промежуточные показания (между основными) Проверим на замену счетчика и переход через ноль
                        counters[k, EtazR, 6, PomesR] = "-";//запишем отсутствие расхода в БД
                        if (counters[k, EtazR, 3, PomesR] != Nschet)
                        {//сменился номер счетчика 
                            value1 = double.Parse(counters[k, EtazR, 1, PomesR]);
                            Nschet = counters[k, EtazR, 3, PomesR];
                            summa += rezult;//расход запишем к сумме
                            rezult = 0;
                        }
                        else
                        {//если счетчик не сменился, 
                            //rezult = (double.Parse(counters[k, EtazR, 1, PomesR]) - value1) * int.Parse(counters[k, EtazR, 4, PomesR]);     //посчитаем расход на всякий случай
                            if (double.Parse(counters[k, EtazR, 1, PomesR]) == 10000 || double.Parse(counters[k, EtazR, 1, PomesR]) == 100000 || double.Parse(counters[k, EtazR, 1, PomesR]) == 1000000 || double.Parse(counters[k, EtazR, 1, PomesR]) == 10000000)
                            {//если произошел переход через ноль (показание кратно 10к, а следуюшее (если существует, меньше текущего)
                                if (k + 1 < 60) if (counters[k + 1, EtazR, 1, PomesR] != null) if (double.Parse(counters[k + 1, EtazR, 1, PomesR]) < double.Parse(counters[k, EtazR, 1, PomesR]))
                                {
                                    rezult += double.Parse(counters[k, EtazR, 1, PomesR]) * int.Parse(counters[k, EtazR, 4, PomesR]);
                                    value1 = double.Parse(counters[k+1, EtazR, 1, PomesR]);
                                }  
                            } 
                        }
                        if (rezult != 0)
                        {
                            summa += rezult;
                            rezult = 0;
                        }
                    }
                }
                // else break;может убрать? - что за вопрос? КОНЕЧНО! ведь если будут в другой день покзания по воде, то тут будет пусто, а последующие будут не пустыми.
            }
            if (value2 == 0 && summa == 0)
            {
                //counters[DataK1, EtazR, 6, PomesR] = "0";//начальное нулевое показание в БД
                return 0;//если k=1; или в этом месяце только одно начальное показание
            }
            summa += (value2 - value1)*koeff;
            counters[DataK, EtazR, 6, PomesR] = Math.Round(summa, 1).ToString();//запишет в БД
            return Math.Round(summa, 1);
        }
        private void button35_Click(object sender, EventArgs e)//скорректировать ДБ (добавить электросчетчики)
        {            
            for (int et = 0; et < 4; et++)
            {
                for (int pomesh = 0; pomesh < maxRoom; pomesh++)
                {
                    int MonthTemp = -1;
                    for (int k = 0; k < 60; k++)
                    {
                        if (counters[k, et, 0, pomesh] != null)//если показания записаны (хотябы по воде?)
                        {
                            if (counters[k, et, 3, pomesh] == null)
                            {
                                counters[k, et, 3, pomesh] = data[et, 9, pomesh];//добавить номер счетчика
                                counters[k, et, 4, pomesh] = data[et, 15, pomesh];//добавить расчетный коэффициент
                            }
                            if (counters[k, et, 6, pomesh] == null)//если расход не посчитан
                            {
                                //только в том случае, если дата является расчетной, записывается расход
                                // в противном случае нужно записать что-то, чтобы было понятно, что расход указан в другой строке (м.б. дата?) и желательно не оставлять null
                                //если указан расход, значит он официально используется для отчета. Но может быть и отрицательный расход
                                //например, когда расчетный расход обогнал фактичесие показания, тогда отрицательный расход указывает
                                // на нулевой расход в отчете, и разница должна учитываться при пересчете показаний в следующем расчетном периоде! (добавить в Rasxod3)
                                if (DateTime.Parse(counters[k, et, 0, pomesh]).Month != MonthTemp)
                                {
                                    MonthTemp = DateTime.Parse(counters[k, et, 0, pomesh]).Month;
                                    Rasxod3(et, pomesh, DateTime.Parse(counters[k, et, 0, pomesh]));
                                }
                            }
                        }
                    }
                }
            }
            time3 = 0;
            timer3.Interval = 100;
            timer3.Enabled = true;
        }

        private void button36_Click(object sender, EventArgs e)
        {
            dataMod[1] = "electro";//переключимся на счетчик электроэнергии
            dataGridView1.Visible = true;
            listBox2.Visible = true;
            richTextBox2.Visible = false;
            for (int row = 0; row < counters.GetLength(0); row++) //строки
            {
                for (int column = 0; column < 60; column++)//столбцы (не более 60 дат на счетчик)
                { 
                    //проверить есть ли существующая дата в заголовке таблицы (циклом перебрать),
                    //если нет, добавить столбец с указанной датой (даты должны быть отсортированы по возрастанию)
                    //записать значение в ячейку значение из массива.
                    //отсортировать столбцы по дате (по возрастанию)

                    // вариантов несколько: 1.создать массив (виртуальную таблицу) и переписать из нее значения в реальную талицу.
                    //2. создать листстринг с датами, отсортировать, создать столбцы, записать в таблицу значения
                    //3. либо сделать так как описано выше (внести данные, потом таблицу отсортировать)
                }
            }
        }


        private void button37_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += richTextBox1.Text.Replace("\n", "&rn");
        }

        private void button38_Click(object sender, EventArgs e)
        {
            richTextBox1.Text += richTextBox1.Text.Replace("&rn", "\n");
        }

        private void textBox18_TextChanged(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            for (int et1 = 0; et1 < 4; et1++)
            {
                for (int pomesh = 0; pomesh < maxRoom; pomesh++)
                {
                    if (!(data[et1,9,pomesh]==null||data[et1, 9, pomesh] ==""))
                    {
                        if (data[et1, 9, pomesh] != "расчет")
                        {
                            string sovpadenie = data[et1, 9, pomesh];
                            if (sovpadenie.IndexOf(textBox18.Text) > -1) listBox2.Items.Add(sovpadenie);
                            for (int k = 0; k < 60; k++)
                            {
                                if (counters[k, et1, 0, pomesh] != null)
                                {
                                    if (counters[k, et1, 3, pomesh] != sovpadenie && counters[k, et1, 3, pomesh]!=null)
                                    {
                                        sovpadenie = counters[k, et1, 3, pomesh];
                                        if (sovpadenie.IndexOf(textBox18.Text) > -1) listBox2.Items.Add(sovpadenie);
                                    }
                                }
                                else break;
                            }
                        }
                    }
                    //if(data[et1, 9, pomesh].IndexOf(textBox18.Text)>-1)listBox2.Items.Add(data[et1, 9, pomesh]);
                }
            }
        }

        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                if (listBox1.SelectedItem != null)
                {
                    string selectItem = listBox1.SelectedItem.ToString();
                    if (listBox1.SelectedItem.ToString().IndexOf("{") == 0)
                    {
                        ClearCB();//очистим данные
                        int EtazX = int.Parse(selectItem.Substring(1, 1));
                        if (EtazX == 0) button2.PerformClick();
                        if (EtazX == 1) button16.PerformClick();
                        if (EtazX == 2) button17.PerformClick();
                        if (EtazX == 3) button18.PerformClick();
                        comboBox5.Text = selectItem.ToString().Substring(7, selectItem.ToString().IndexOf("}", 7) - 7);//{0} к.{
                        //} пом.{0xxx}
                        comboBox6.Text = selectItem.ToString().Substring(selectItem.ToString().LastIndexOf("{") + 1, selectItem.ToString().Length - selectItem.ToString().LastIndexOf("{") - 2);
                        timer1.Enabled = false;
                        kontur(roomGlobal);
                    }
                    else
                    {
                        if (selectItem == comboBox1.Text)
                        {
                            comboBox1.Items.Clear();
                            comboBox1.Items.AddRange(Arendators.ToArray());
                        }
                        ClearCB();//очистим данные
                        listBox1.Items.Clear();
                        comboBox1.Text = selectItem;
                    }
                }
                
            }
        }
        void kontur(int pomesh1)//на входе помещение, на выходе мигающее помещение...
        {//сперва заполнить координатами фигуру, затем найти точку внутри этого полигона, третье: отцентрироваться на этом помещении, четвертое изменить масштаб, пятое "моргнуть" 
            if (koord[floorGlobal, 0, pomesh1] != 0)
            {
                int i1 = 0;
                int i2 = 0;
                double[,] mass = new double[2, 20];
                for (; i1 < 40; i1++, i2++)//пройти по координатам
                {
                    if (koord[floorGlobal, i1, pomesh1] != 0)
                    {
                        mass[0, i2] = koord[floorGlobal, i1, pomesh1];
                        i1++;
                        mass[1, i2] = koord[floorGlobal, i1, pomesh1];
                    }
                    else break;
                }
                mass[0, i2] = mass[0, 0];
                mass[1, i2] = mass[1, 0];
                figa1= new Point[i2];
                for (int i = 0; i < i2; i++)
                {
                    figa1[i].X = (int)mass[0, i];
                    figa1[i].Y = (int)mass[1, i];
                }//итак, кооринаты нашли.
                scale = 100;
                pictureBox1.Width = scalekX * scale;
                pictureBox1.Height = scalekY * scale;
                Point centrZ = CentrU(mass);//получили центр полигона
                double x = panel1.Size.Width / 2 - (centrZ.X) * 5; //626 и 389 - это центр панели с пиктурбоксом
                double y = panel1.Size.Height / 2 - (centrZ.Y) * 5;
                pictureBox1.Location = new Point((int)x, (int)y);
                curnew = pictureBox1.Location;
                pictureBox1.Focus();
                timer1.Enabled = true;
            }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            textBox18.Clear();
        }
        int[] outL2et_pom = new int[2];//0= выбранный этаж, 1= порядковый номер помещения
        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > -1)
            {
                outL2et_pom = SelectL2(listBox2.SelectedItem.ToString());
                floorGlobal=outL2et_pom[0];
                roomGlobal=outL2et_pom[1];
                switch (outL2et_pom[0])//выбран этаж, прогрузить этаж
                {
                    case 0: button2.PerformClick();
                        break;
                    case 1: button16.PerformClick();
                        break;
                    case 2: button17.PerformClick();
                        break;
                    case 3: button18.PerformClick();
                        break;
                    default: break;
                }
                comboBox5.Text = data[outL2et_pom[0], 0, outL2et_pom[1]];
                comboBox6.Text = data[outL2et_pom[0], 1, outL2et_pom[1]];
                RasxodFull(outL2et_pom[0], outL2et_pom[1], dateTimePicker5.Value);
            }
            redact = false;//обнуляем флаг редактирования ячеек
        }

        int[] SelectL2(string selItem)
        {
            int[] outInt = new int[2];
            outInt[0] = 7;//"очистим массив"
            dataGridClear = true;
            dataGridView1.Columns.Clear();
            dataGridClear = false;
            for (int et1 = 0; et1 < 4; et1++)
            {
                for (int pomes = 0; pomes < maxRoom; pomes++)
                {
                    for (int k = 0; k < 60; k++)
                    {
                        if (counters[k, et1, 0, pomes] != null) //если дата равна нулю, то дальше можно не искать.
                        {
                            if (counters[k, et1, 3, pomes] == selItem)
                            {
                                dataGridAdd(et1, pomes);//а внутри тоже самое (такой же выход из циклов)
                                outInt[0] = et1;
                                outInt[1] = pomes;
                                return outInt;
                            }
                        }
                        else break;
                    }
                }
            }
            return outInt;
        }
        void dataGridAdd(int et, int pomes)
        {//это все отлично. но нахер заполнять таблицу, если в в ней нет показаний счетчиков??? Надо, Вася)) чтобы запонить ее в дальнейшем.
            var column1 = new DataGridViewCalendarColumn();
            column1.HeaderText = "Дата";
            column1.Width = 85;
            dataGridView1.Columns.Add(column1);
            var column2 = new DataGridViewTextBoxColumn();
            column2.HeaderText = "Показание";
            column2.Width = 85;
            dataGridView1.Columns.Add(column2);
            var column3 = new DataGridViewTextBoxColumn();
            column3.HeaderText = "Расход";
            column3.Width = 60;
            dataGridView1.Columns.Add(column3);
            dataGridView1.Columns[2].ReadOnly = true;//запретим менять расход произвольно
            var column4 = new DataGridViewTextBoxColumn();
            column4.HeaderText = "Коэфф.";
            column4.Width = 60;
            dataGridView1.Columns.Add(column4);
            if (counters[0, et, 1, pomes] != null)
            {
                for (int i = 59; i > -1; i--)
                {
                    if (counters[i, et, 1, pomes] != null) dataGridView1.Rows.Add(DateTime.Parse(counters[i, et, 0, pomes]), counters[i, et, 1, pomes], counters[i, et, 6, pomes], counters[i, et, 4, pomes]);//Rasxod3(et, pomes, DateTime.Parse(counters[i, et, 0, pomes])) - убрали параметр
                }
            }            
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = counters[0, et, 4, pomes];//в конец добавим расчетный коэффициент (который был при предыдущих показаниях)
        }
        bool dgCellEdit = false;
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {


            //рассматриваем следующие 5 параметров:
            //1. изменили дату.
            //2. изменили показание
            //3. изменили коэффициент
            //4. добавили дату.
            //5. редактирование ячейки расход запрещено +
         /*   if (dataGridView1[1, e.RowIndex].Value != null)
            {//изменить можно любую ячейку, но расход будет пересчитываться автоматически и не изменяется вручную.
                label38.Text = dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString();//измененное значение
                label38.Text += " " + dataGridView1[0, e.RowIndex].Value.ToString();//дата измененной ячейки
            }*/
            //дата-показание-расход-коэффициент


        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (outL2et_pom[0] != 7)
                {
                    switch (outL2et_pom[0])//выбран этаж, прогрузить этаж
                    { 
                        case 0: button2.PerformClick();
                            break;
                        case 1: button16.PerformClick();
                            break;
                        case 2: button17.PerformClick();
                            break;
                        case 3: button18.PerformClick();
                            break;
                        default: break;
                    }
                    comboBox5.Text = data[outL2et_pom[0], 0, outL2et_pom[1]];
                    comboBox6.Text = data[outL2et_pom[0], 1, outL2et_pom[1]];
                    outL2et_pom[0] = 7;//обнуление
                    textBox18.Clear();
                    dataGridClear = true;
                    dataGridView1.Columns.Clear();
                    dataGridClear = false;
                }
            }
            if (tabControl1.SelectedIndex == 1)
            {
                comboBox23.Items.AddRange(ArendaLong("ToLongName").ToArray());
                comboBox23.Text = comboBox1.Text;
            }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            OutputSorting("ToLongNameSchet");
        }

        bool redact = false;//какого лешего она зеленая? используется в моих функциях как глобальная переменная
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            if (dataGridView1[0, e.RowIndex].Value != null && dataGridView1[1, e.RowIndex].Value != null && dataGridView1[3, e.RowIndex].Value != null)
            {//заполнены ячейки дата-показание-коэффициент 
                redact = true;
                if (e.ColumnIndex == 0)//выбрана дата
                {
                    modCounters[0] = DateTime.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()).ToShortDateString();
                }
                else 
                {
                    modCounters[0] = DateTime.Parse(dataGridView1[0, e.RowIndex].Value.ToString()).ToShortDateString();//дата
                    modCounters[1] = dataGridView1[1, e.RowIndex].Value.ToString();//показания ээ
                    modCounters[3] = listBox2.SelectedItem.ToString();//номер сч.
                    modCounters[4] = dataGridView1[3, e.RowIndex].Value.ToString();//коэффициент
                    if (dataMod[0] == null || dataMod[0] == "") dataMod[0] = modCounters[0];
                    //dataMod
                   // WriteSchet(outL2et_pom[0], outL2et_pom[1], DateTime.Parse(dataGridView1[0, e.RowIndex].Value.ToString()), dataGridView1[1, e.RowIndex].Value.ToString(), "+", listBox2.SelectedItem.ToString(), dataGridView1[3, e.RowIndex].Value.ToString(), "+", "+");
                }
                timer3.Interval = 100;
                timer3.Enabled = true;                
            }

            if (!dgCellEdit)
            {
                //если изменится дата, то она не должна присутствовать в этой таблице. Если присутствует, то переключиться на нее.
                int selectedRow = e.RowIndex;
             
                if (dataGridView1[0, selectedRow].Value != null&& selectedRow< dataGridView1.RowCount -1)
                {
                    for (int k = 0; k < dataGridView1.RowCount-2; k++)//-2 ошибка???
                    {
                        if (k != selectedRow && DateTime.Parse(dataGridView1[0, k].Value.ToString()).ToShortDateString() == DateTime.Parse(dataGridView1[0, selectedRow].Value.ToString()).ToShortDateString())
                        {
                            //counters[k, outL2et_pom[0], 0, outL2et_pom[1]] = DateTime.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()).ToShortDateString();
                            //dataGridView1.Rows.Remove(dataGridView1.Rows[k]);
                            dataGridView1[0, k].Selected = true;
                            dgCellEdit = true;
                            dataGridView1.Rows.RemoveAt(selectedRow);
                        }
                    }
                    //richTextBox1.Text += dataGridView1[0, e.RowIndex].Value.ToString() + "\r\n";
                }
            }
            else dgCellEdit = false;
 
        }
        
        private void dataGridView1_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)//выбрана дата
            {
                dataMod[0] = DateTime.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()).ToShortDateString();
            }
        }
        bool dataGridClear=false;
        private void dataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            if (outL2et_pom[0] != 7&&!dataGridClear)
            {
                int row1 = -1;
                for (int k = 59; k > -1; k--)
                {
                    if (counters[k, outL2et_pom[0], 0, outL2et_pom[1]] != null)
                    {
                        if (row1 < dataGridView1.RowCount )
                        {
                            row1++;
                            richTextBox1.Text +=
    counters[k, outL2et_pom[0], 0, outL2et_pom[1]] + " " + k.ToString() + " " +
    DateTime.Parse(dataGridView1[0, row1].Value.ToString()).ToShortDateString() + " " + row1.ToString() + "\r\n";
                            if (counters[k, outL2et_pom[0], 0, outL2et_pom[1]] != DateTime.Parse(dataGridView1[0, row1].Value.ToString()).ToShortDateString())
                            {
                                row1--;
                                richTextBox1.Text += counters[k, outL2et_pom[0], 0, outL2et_pom[1]] + "\r\n";
                                //функция удаления строки массива
                                // DelChE(outL2et_pom[0], outL2et_pom[1], k);
                                floorGlobal = outL2et_pom[0];
                                roomGlobal = outL2et_pom[1];
                                dataMod[0] = counters[k, outL2et_pom[0], 0, outL2et_pom[1]];
                                modCounters[0] = "";
                                //LoadDB(); здесь не нужно, таймер сам прогрузит базу и удалит нужную строку
                                timer3.Interval = 100;
                                timer3.Enabled = true;
                                break;
                            }
                        }
                    }
                }
                //SelectL2(listBox2.SelectedItem.ToString());
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            bool nomerE = false;
            bool nomerV = false;
            for(int k=0;k<60;k++)
            {
                if (nomerE)
                {
                    if (counters[k, floorGlobal, 3, roomGlobal] != null) comboBox14.Text = counters[k, floorGlobal, 3, roomGlobal];
                    if (counters[k, floorGlobal, 4, roomGlobal] != null) comboBox14.Text = counters[k, floorGlobal, 4, roomGlobal];
                }
                if (nomerV)
                {
                    if (counters[k, floorGlobal, 5, roomGlobal] != null) comboBox16.Text = counters[k, floorGlobal, 5, roomGlobal];
                }
                if (counters[k, floorGlobal, 0, roomGlobal] == dateTimePicker2.Value.ToShortDateString())
                {
                    
                    if (counters[k, floorGlobal, 1, roomGlobal] != null && counters[k, floorGlobal, 3, roomGlobal] != null && counters[k, floorGlobal, 4, roomGlobal] != null)
                    {
                        textBox10.Text = counters[k, floorGlobal, 1, roomGlobal];//показание электросчетчика 3.
                        comboBox14.Text = counters[k, floorGlobal, 3, roomGlobal];//номер электросчетчика 2.
                        comboBox18.Text = counters[k, floorGlobal, 4, roomGlobal];//коэфф. трансформации 3.
                    }
                    else
                    {
                        textBox10.Text = "";
                        comboBox14.Text = "";//вставить функцию (№счетчика)
                        comboBox18.Text = "";
                        nomerE = true;
                    }
                    if (!(counters[k, floorGlobal, 2, roomGlobal] == null || counters[k, floorGlobal, 5, roomGlobal] == null) || counters[k, floorGlobal, 7, roomGlobal] != null)//добавить остальное, когда займемся водой
                    {
                        /*
                             counters[k, floorGlobal, 8, roomGlobal]//для воды счетчик-расчет-или счетчик/расчет в data? 3.
                             counters[k, floorGlobal, 9, roomGlobal]//для воды на технологич./хозпитнужды в data? 3.
                         */
                        textBox11.Text = counters[k, floorGlobal, 2, roomGlobal];//показание водомера        3.  
                        comboBox16.Text = counters[k, floorGlobal, 5, roomGlobal];//номер водомера 2.
                        comboBox4.Text = counters[k, floorGlobal, 7, roomGlobal];//для воды количество сотрудников 3.
                    }
                    else 
                    {
                        textBox11.Text = "";
                        comboBox16.Text = "";//вставить функцию (№ водомера)
                        comboBox4.Text = "";
                        nomerV = true;
                    }
                    //break;
                }
            }
            
        }

        private void button41_Click(object sender, EventArgs e)//вывести массив счетчиков
        {
            for (int k = 59; k > -1; k--)
            {
                if (counters[k, outL2et_pom[0], 0, outL2et_pom[1]] != null)
                {
                    for (int i = 0; i < RMC; i++) richTextBox1.Text += counters[k, outL2et_pom[0], i, outL2et_pom[1]] + ";";
                    richTextBox1.Text += "\r\n";
                }
            }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            HideAr();//спрятать чертеж
            ButtonBlue(1);//первая кнопка выделена, нужно добавить неактивность формы на время прогрузки чертежа.
        }

        private void button47_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer", System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location)+textBox26.Text);
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox3.Checked && !checkBox4.Checked) checkBox4.Checked=true;
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (!checkBox3.Checked && !checkBox4.Checked) checkBox3.Checked = true;
        }
        List<string> Arendators = new List<string>();
        private void textBox20_TextChanged(object sender, EventArgs e)
        {
            listBox1.Sorted = false;
            if (Arendators.Count < 1)
            {
                Arendators = ArendaLong("ToLongName");
            }
            if (textBox20.Text == "")
            {
                listBox1.Items.Clear();                
                listBox1.Items.AddRange(Arendators.ToArray());
            }
            else
            {
                listBox1.Items.Clear();
                for (int i = 0; i < Arendators.Count; i++)
                {
                    if (Arendators[i].ToLower().IndexOf(textBox20.Text.ToLower()) > -1) listBox1.Items.Add(Arendators[i]);
                }
            }
        }
        int ind1 = -1;
        private void listBox1_MouseMove(object sender, MouseEventArgs e)//покажем подсказку, для значений listBox1 длиной более 20
        {
            Point screenPosition = ListBox.MousePosition;
            Point listBoxClientAreaPosition = listBox1.PointToClient(screenPosition);
            int ind = listBox1.IndexFromPoint(listBoxClientAreaPosition);
            if (ind != -1&&ind!=ind1)
            {
                if(listBox1.Items[ind].ToString().Length>20) Hint.Show(listBox1.Items[ind].ToString(), listBox1);
                ind1 = ind;
            }
        }

        private void comboBox19_SelectedValueChanged(object sender, EventArgs e)
        {
            richTextBox1.Text += comboBox19.Text.ToString() + "\r\n";
        }
        Point positionDTP3 = new Point();//исправляем ошибку на 10-ке... Почему-то статика не работает. хм..
        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox5.Checked)
            {
                tabPage6.Parent = tabControl2;
                tabPage7.Parent = null;//проще исправить порядок не придумал
                tabPage7.Parent = tabControl2;//пока что...
                tabControl2.SelectedIndex = 2;
                //вбиваем проценты
                if(comboBox22.Items.Count<1) for (int i = 0; i < 101; i++) comboBox22.Items.Add(i);
                comboBox22.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;//заблокировать ввод других процентов
                comboBox22.Text = "100";//по умолчанию - 100
                comboBox14.Text = "расчет";
                comboBox14.Visible = false;
                comboBox15.Text = "";
                comboBox15.Visible = false;
                textBox4.Text = "";
                textBox4.Visible = false;
                textBox12.Text = "";
                textBox12.Visible = false;
                checkBox1.Checked = false;
                checkBox1.Visible = false;
                label17.Visible = false;
                label18.Visible = false;
                label19.Visible = false;
                label31.Visible = false;
                dateTimePicker3.Parent = tabPage6;
                positionDTP3 = dateTimePicker3.Location;
                dateTimePicker3.Location= new Point(8,8);
            }
            else
            {
                tabPage6.Parent = null;
                comboBox14.Text = "";
                comboBox14.Visible = true;
                comboBox15.Text = "";
                comboBox15.Visible = true;
                textBox4.Text = "";
                textBox4.Visible = true;
                textBox12.Text = "";
                textBox12.Visible = true;
                checkBox1.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                label31.Visible = true;
                dateTimePicker3.Parent = tabPage4;
                dateTimePicker3.Location = positionDTP3;
            }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            for (int et = 0; et < 4; et++)
            {
                for (int pomesh = 0; pomesh < maxRoom; pomesh++)
                {
                    if (data[et, 0, pomesh] != "" && data[et, 1, pomesh] != "")
                    {
                        string temp = "";
                        for (int i = 0; i < 60; i++)
                        {

                            if (counters[i, et, 0, pomesh] != null)
                            {
                                if (temp == counters[i, et, 0, pomesh]) richTextBox1.Text += counters[i, et, 3, pomesh] + "\r\n";
                                else temp = counters[i, et, 0, pomesh];
                            }
                            else break;
                        }
                    }
                }
            }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            string temp = "";
            for (int i = 1; i < File.Count; i++)
            {
                if (temp == File[i].ToString())
                {
                     File.RemoveAt(i);
                }
                else temp = File[i].ToString();
            }
            System.IO.File.WriteAllLines(@"Data.txt", File, Encoding.Default);
        }

        private void button50_Click_1(object sender, EventArgs e)
        {
            if (comboBox14.Text != "" && comboBox14.Text != "расчет")
            {
                tabControl1.SelectedIndex = 1;
                textBox18.Text = comboBox14.Text;
                button36.PerformClick();
                if (listBox2.Items.Count > 0) listBox2.SelectedIndex=0;
            } 
        }

        private void button51_Click(object sender, EventArgs e)//Отчет за период
        {
            ReportPrinter report = new ReportPrinter(ExcelPrinter.Company.SKB, ExcelPrinter.Report.countersPeriod, dateTimePicker5.Value, dateTimePicker6.Value);
            report.arendaCB23 = comboBox23.Text;
            report.AddList(ToReport(comboBox23.Text, dateTimePicker5.Value, dateTimePicker6.Value));
        }

        List<string> ToReport(string arendator, DateTime DataOtMes, DateTime DataDoMes)// выведет построчно: корпус-помещение, №счетчика, показания на начало, показания на конец, расчетный коэфф., расчет.
        {
            List<string> ToOtchet = new List<string>();
            for (int et1=0; et1 < 4; et1++)
            {
                for (int pomesh = 0; pomesh < maxRoom; pomesh++)
                {
                    if (arenda[0, et1, 1, pomesh] == arendator)
                    {
                        ToOtchet.Add("Корпус №" + data[et1, 0, pomesh] + ", помещ.№" + data[et1, 1, pomesh]);//корпус-помещение
                        ToOtchet.Add(data[et1, 9, pomesh]);//№счетчика

                        DateTime dataPred1 = new DateTime(DataOtMes.Year, DataOtMes.Month, 24).AddMonths(-1);//с 24 числа предыдущего месяца
                        DateTime dataPred2 = new DateTime(DataDoMes.Year, DataOtMes.Month, 7);//до 7-го числа текущего месяца.(диапазон)
                       // DateTime dataPred1 = new DateTime(DataOtMes.Year, DataOtMes.Month, 24);//с 24-го числа текущего месяца
                      //  DateTime dataPred2 = new DateTime(DataOtMes.Year, DataOtMes.Month, 7).AddMonths(1);//до 7-го числа следующего месяца.(диапазон)
                        DateTime dataTekus1 = new DateTime(DataDoMes.Year, DataDoMes.Month, 24);//с 24-го числа текущего месяца
                        DateTime dataTekus2 = new DateTime(DataDoMes.Year, DataDoMes.Month, 7).AddMonths(1);//до 7-го числа следующего месяца.(диапазон)
                        double rasxodZaPeriod = 0;
                        bool flag = false;
                        bool Period=false;
                        for (int k = 0; k < 60; k++)
                        {
                            if (counters[k, et1, 6, pomesh] != null)//расход ЭЭ имеет запись
                            {
                                if (counters[k, et1, 6, pomesh] != "" && counters[k, et1, 6, pomesh] != "-")
                                {
                                    if (!Period)
                                    {
                                        if (DateTime.Parse(counters[k, et1, 0, pomesh]) > dataTekus1 && DateTime.Parse(counters[k, et1, 0, pomesh]) < dataTekus2)
                                        {
                                            Period = true;
                                            rasxodZaPeriod += double.Parse(counters[k, et1, 6, pomesh]);
                                            richTextBox1.Text += "1:"+rasxodZaPeriod.ToString() + "\r\n";//лог
                                            ToOtchet.Add(counters[k, et1, 1, pomesh]);
                                            if (DataOtMes.Month == DataDoMes.Month) flag = false;//если период больше одного месяца и false - если период один месяц.
                                            else flag = true;
                                        }
                                    }
                                    else
                                    {
                                        if (DateTime.Parse(counters[k, et1, 0, pomesh]) > dataPred1 && DateTime.Parse(counters[k, et1, 0, pomesh]) < dataPred2)
                                        {
                                            ToOtchet.Insert(ToOtchet.Count - 1, counters[k, et1, 1, pomesh]);
                                            ToOtchet.Add(counters[k, et1, 4, pomesh]);
                                            break;
                                        }
                                        if (flag)
                                        {
                                            rasxodZaPeriod += double.Parse(counters[k, et1, 6, pomesh]);
                                            richTextBox1.Text += "2:" + rasxodZaPeriod.ToString() + "\r\n";//лог
                                        }
                                        else flag = true;
                                    }
                                }
                            }
                        }
                        if (Period)//значит расход записался как минимум один раз
                        {
                            if (ToOtchet.Count > 3)
                            {
                                if (ToOtchet[ToOtchet.Count - 4] == data[et1, 9, pomesh])//номер счетчика находится на две записи назад (записались оба показания)
                                {
                                    ToOtchet.Add(rasxodZaPeriod.ToString());
                                    richTextBox1.Text += "3:" + rasxodZaPeriod.ToString() + "\r\n";//лог
                                }
                                else//записалось одно показание
                                {
                                    ToOtchet.Insert(ToOtchet.Count - 1, "запись отсутствует");//нет показания на начало
                                    ToOtchet.Add("запись отсутствует");//нет показания на конец
                                    ToOtchet.Add("запись отсутствует");//нет коэфф. счетчика
                                } 
                            }
                            else
                            {
                                ToOtchet.Insert(ToOtchet.Count - 1, "запись отсутствует");//нет показания на начало
                                ToOtchet.Add("запись отсутствует");//нет показания на конец
                                ToOtchet.Add("запись отсутствует");//нет коэфф. счетчика
                            } 
                        }
                        else//расход за этот период отсутствует в базе (не записалось ни одно показание)
                        {
                            ToOtchet.Add("запись отсутствует");//нет показания на начало
                            ToOtchet.Add("запись отсутствует");//нет показания на конец
                            ToOtchet.Add("запись отсутствует");//нет коэфф. счетчика
                            ToOtchet.Add("0");//нет расхода
                        }
                    }
                }
            }
            return ToOtchet;
        }

        string FileNameFD1(string ParentFolder)
        {
            openFileDialog1.InitialDirectory = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + ParentFolder;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string folder = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location.ToLower());
                if (openFileDialog1.FileName.ToLower().IndexOf(folder) > -1)
                {
                    int i1 = folder.Length;
                    return openFileDialog1.FileName.Substring(i1, openFileDialog1.FileName.Length - i1);
                }
            }
            return "";
        }
        private void textBox22_MouseDoubleClick(object sender, MouseEventArgs e)//ПЛАН
        {
            textBox22.Text = FileNameFD1("\\Планировки");
        }
        private void textBox23_MouseDoubleClick(object sender, MouseEventArgs e)//Однолинейная схема
        {
            textBox23.Text = FileNameFD1("\\Однолинейные схемы");
        }
        private void textBox24_MouseDoubleClick(object sender, MouseEventArgs e)//План электроснабжения
        {
            textBox24.Text = FileNameFD1("\\Планы электросетей");
        }
        private void textBox25_MouseDoubleClick(object sender, MouseEventArgs e)//План вдоснабжения
        {
            textBox25.Text = FileNameFD1("\\Планы водоснабжения");
        }
        private void textBox26_MouseDoubleClick(object sender, MouseEventArgs e)//ПАПКА
        {
            folderBrowserDialog1.SelectedPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string folder=System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location.ToLower());
                if (folderBrowserDialog1.SelectedPath.ToLower().IndexOf(folder) > -1)
                {
                    int i1 = folder.Length;
                    folder = folderBrowserDialog1.SelectedPath.Substring(i1, folderBrowserDialog1.SelectedPath.Length - i1);
                    textBox26.Text = folder;
                }
                else textBox26.Text = "";
            }
        }

        private void button52_Click(object sender, EventArgs e)
        {
            for (int et = 0; et < 4; et++)
            {
                for (int pomesh = 0; pomesh < maxRoom; pomesh++)
                {
                    for (int i= 0; i < 10; i++)
                    {
                        if (arenda[i, et, 1, pomesh] == "Свободно") arenda[i, et, 1, pomesh] = "свободно";
                    }
                }
            }
            time3 = 0;
            timer3.Interval = 100;
            timer3.Enabled = true;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "" && comboBox6.Text != "") Unlock(true);
        }

        private void button53_Click(object sender, EventArgs e)
        {
            comboBox19.Items.AddRange(Sort().ToArray());
        }

        private void textBox22_TextChanged(object sender, EventArgs e)
        {
            if(textBox22.Text!="")button42.Enabled = true;
            else button42.Enabled = false;
        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {
            if (textBox23.Text != "") button43.Enabled = true;
            else button43.Enabled = false;
        }

        private void textBox24_TextChanged(object sender, EventArgs e)
        {
            if (textBox24.Text != "") button44.Enabled = true;
            else button44.Enabled = false;
        }

        private void textBox25_TextChanged(object sender, EventArgs e)
        {
            if (textBox25.Text != "") button46.Enabled = true;
            else button46.Enabled = false;
        }

        private void textBox26_TextChanged(object sender, EventArgs e)
        {
            if (textBox26.Text != "") button47.Enabled = true;
            else button47.Enabled = false;
        }

        private void button42_Click(object sender, EventArgs e)
        {
            VisibleAr(textBox22.Text);
            ButtonBlue(2);
        }
        void VisibleAr(string FileName)
        {
            axAcCtrl1.Visible = true;
            pictureBox1.Visible = false;            
            axAcCtrl1.PutSourcePath(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + FileName);
        }
        void ButtonBlue(int c)
        {
            if (c==1)
            {
                button45.BackColor = Color.DodgerBlue;
                button42.BackColor = SystemColors.Control;
                button43.BackColor = SystemColors.Control;
                button44.BackColor = SystemColors.Control;
                button46.BackColor = SystemColors.Control;
            }
            if (c == 2)
            {
                button45.BackColor = SystemColors.Control;
                button42.BackColor = Color.DodgerBlue;
                button43.BackColor = SystemColors.Control;
                button44.BackColor = SystemColors.Control;
                button46.BackColor = SystemColors.Control;
            }
            if (c == 3)
            {
                button45.BackColor = SystemColors.Control;
                button42.BackColor = SystemColors.Control;
                button43.BackColor = Color.DodgerBlue;
                button44.BackColor = SystemColors.Control;
                button46.BackColor = SystemColors.Control;
            }
            if (c == 4)
            {
                button45.BackColor = SystemColors.Control;
                button42.BackColor = SystemColors.Control;
                button43.BackColor = SystemColors.Control;
                button44.BackColor = Color.DodgerBlue;
                button46.BackColor = SystemColors.Control;
            }
            if (c == 5)
            {
                button45.BackColor = SystemColors.Control;
                button42.BackColor = SystemColors.Control;
                button43.BackColor = SystemColors.Control;
                button44.BackColor = SystemColors.Control;
                button46.BackColor = Color.DodgerBlue;
            }
        }
        void HideAr()
        {
            axAcCtrl1.Visible = false;
            pictureBox1.Visible = true; 
        }

        private void button54_Click(object sender, EventArgs e)
        {
            axAcCtrl1.Zoom_In();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            VisibleAr(textBox23.Text);
            ButtonBlue(3);
        }

        private void button44_Click(object sender, EventArgs e)
        {
            VisibleAr(textBox24.Text);
            ButtonBlue(4);
        }

        private void button46_Click(object sender, EventArgs e)
        {
            VisibleAr(textBox25.Text);
            ButtonBlue(5);
        }
        private void button56_Click(object sender, EventArgs e)
        {

        }

        private void button57_Click(object sender, EventArgs e)
        {
            File = System.IO.File.ReadAllLines(@"Data.txt", Encoding.Default).ToList();
            LoadDB();
        }

        private void button58_Click(object sender, EventArgs e)//тестовая, изменить счетчик
        {
            floorGlobal = 0;
            roomGlobal = 0;
            //izmPomes(floorGlobal, roomGlobal);
            modCounters[0] = "";//"29.08.2020";//"29.07.2020";//textBox1.Text;//
            dataMod[0] = "01.05.2020";
            dataMod[1] = "electro";
            modCounters[1] = "6069,1";
            //  modCounters[3] = "014105";
        }

        private void button59_Click(object sender, EventArgs e)//вывести изменения на экран
        {
            File.Clear();
            File.Add((countRoom[0] + countRoom[1] + countRoom[2] + countRoom[3] + 4).ToString());//записали общее количество помещений в начало
            for (int etaz = 0; etaz < 4; etaz++)
            {
                File.Add("[etaz_" + (etaz + 1).ToString() + "]" + (countRoom[etaz] + 1).ToString());//запись номера этажа

                for (int pomeshenie = 0; pomeshenie <= countRoom[etaz]; pomeshenie++)
                {
                    File.Add("[" + pomeshenie + "]");//запись номера помещения
                    string s = "";
                    for (int i = 0; i < 40; i++)
                    {
                        if (koord[etaz, i, pomeshenie] == 0) break;
                        else
                        {
                            s += koord[etaz, i, pomeshenie] + ";";
                        }
                    }
                    if (s != "") File.Add(s.Substring(0, s.Length - 1));//записали координаты
                    else File.Add("=no koord=");
                    s = "";
                    for (int i = 0; i < RMD; i++) s += data[etaz, i, pomeshenie] + ";";
                    if (s != "") File.Add(s.Substring(0, s.Length - 1));//записали данные помещения
                    s = "";
                    for (int k = 0; k < 10; k++)
                    {
                        if (arenda[k, etaz, 0, pomeshenie] == null) break;
                        s = "";
                        for (int i = 0; i < RMA; i++) s += arenda[k, etaz, i, pomeshenie] + ";";
                        if (s != "") File.Add(s.Substring(0, s.Length - 1));//записали реквизиты арендатора
                    }
                    File.Add("[pokazanie]");
                    s = "";
                    for (int k = 0; k < 60; k++)
                    {
                        if (counters[k, etaz, 0, pomeshenie] == null) break;
                        s = "";
                        for (int i = 0; i < RMC; i++)
                        {
                            s += counters[k, etaz, i, pomeshenie] + ";";
                        }
                        File.Add(s.Substring(0, s.Length - 1));//записали строку счетчиков
                    }
                    s = "";
                }
            }
            richTextBox1.Clear();
            string Rich = "";
            for (int i = 0; i < File.Count; i++) Rich += File[i] + "\r\n";
            richTextBox1.Text += Rich;
        }

        private void button60_Click(object sender, EventArgs e)//тестовая, изменить Арендатора
        {
            floorGlobal = 0;
            roomGlobal = 0;
            //izmPomes(floorGlobal, roomGlobal);
            modArenda[0] = "";//"01.05.2020";
            dataModA = "22.04.2020";
            modArenda[1] = null;//"ООО \"АбраКадабра\"";
            //  modCounters[3] = "014105";
        }

        private void button61_Click(object sender, EventArgs e)//инвентаризация электросчетчиков
        {
            ReportPrinter report = new ReportPrinter(ExcelPrinter.Company.Impuls, ExcelPrinter.Report.countersInventoryElectro);
            report.AddList(InvertoryTable(userKeyEnum.electro));
        }

        private void button62_Click(object sender, EventArgs e)//инвентаризация водомеров
        {
            ReportPrinter report = new ReportPrinter(ExcelPrinter.Company.Impuls, ExcelPrinter.Report.countersInventoryAqua);
            report.AddList(InvertoryTable(userKeyEnum.aqua));
        }
        enum userKeyEnum
        {
            electro,
            aqua,
            arenda,
            admin
        }
        List<string> InvertoryTable(userKeyEnum keyEnum)
        {
            List<string> Temp = new List<string>();
            for (int floor = 0; floor < 4; floor++)
            {
                for (int room = 0; room < maxRoom; room++)
                {
                    if (!(data[floor, 0, room] == null || data[floor, 1, room] == null))
                    {
                        if (!(keyEnum != userKeyEnum.electro || counters[0, floor, 3, room] == null || counters[0, floor, 3, room] == "" || counters[0, floor, 3, room] == "расчет")
                            || !(keyEnum != userKeyEnum.aqua || counters[0, floor, 5, room] == null || counters[0, floor, 5, room] == "" || counters[0, floor, 5, room] == "расчет"))
                        {
                            Temp.Add("Корп. " + data[floor, 0, room].ToString() + ", Помещ. " + data[floor, 1, room].ToString());//№ Корпуса и помещения
                            if (keyEnum == userKeyEnum.electro) Temp.Add(counters[0, floor, 3, room]);//№ электросчетчика
                            if (keyEnum == userKeyEnum.aqua) Temp.Add(counters[0, floor, 5, room]);//№ водомера
                            if (keyEnum == userKeyEnum.electro)
                            {
                                if (data[floor, 10, room] != null) Temp.Add(data[floor, 10, room].ToString());//Марка электросчетчика
                                else Temp.Add("");
                            }
                            if (keyEnum == userKeyEnum.aqua)
                            {
                                if (data[floor, 13, room] != null) Temp.Add(data[floor, 13, room].ToString());//Марка водомера
                                else Temp.Add("");
                            }
                            if (keyEnum == userKeyEnum.electro)
                            {
                                if (data[floor, 11, room] != null) Temp.Add(data[floor, 11, room].ToString());//Год выпуска/поверки электросчетчика
                                else Temp.Add("");
                            }
                            if (keyEnum == userKeyEnum.aqua)
                            {
                                if (data[floor, 14, room] != null) Temp.Add(data[floor, 14, room].ToString());//Год выпуска/поверки водомера
                                else Temp.Add("");
                            }
                            if (keyEnum == userKeyEnum.electro)
                            {
                                if (counters[0, floor, 1, room] != null) Temp.Add(counters[0, floor, 1, room]);//Показания (последние), кВт*ч
                                else Temp.Add("");
                            }
                            if (keyEnum == userKeyEnum.aqua)
                            {
                                if (counters[0, floor, 2, room] != null) Temp.Add(counters[0, floor, 2, room]);//Показания (последние), кВт*ч
                                else Temp.Add("");
                            }
                        }
                    }
                }
            }
            return Temp;
        }
        /*
    counters[k, floorGlobal, 0, roomGlobal] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//дата съема показаний 1.
    counters[k, floorGlobal, 1, roomGlobal] = textBox10.Text.Replace(";", ",");//показание электросчетчика 3.
    counters[k, floorGlobal, 2, roomGlobal] = textBox11.Text.Replace(";", ",");//показание водомера        3.  
    counters[k, floorGlobal, 3, roomGlobal] = comboBox14.Text.Replace(";", ",");//номер электросчетчика 2.
    counters[k, floorGlobal, 4, roomGlobal] = comboBox18.Text.Replace(";", ",");//коэфф. трансформации 3.
    counters[k, floorGlobal, 5, roomGlobal] = comboBox16.Text.Replace(";", ",");//номер водомера 2.
* counters[k, floorGlobal, 6, roomGlobal]//расход ЭЭ (текущее минус предыдущее т.е. расход за предыдущий период) 4.
* counters[k, floorGlobal, 7, roomGlobal]//для воды количество сотрудников 3.
* counters[k, floorGlobal, 8, roomGlobal]//для воды счетчик-расчет-или счетчик/расчет в data? 3.
* counters[k, floorGlobal, 9, roomGlobal]//для воды на технологич./хозпитнужды в data? 3.
* counters[k, floorGlobal, 10, roomGlobal]//для воды расход 4.
* counters[k, floorGlobal, 11, roomGlobal]//резерв? приоритет.
* counters[k, floorGlobal, 12, roomGlobal]//резерв? приоритет.
*/
    }
}