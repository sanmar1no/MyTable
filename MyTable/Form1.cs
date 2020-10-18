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


namespace PictureBox
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
        int pomeshTemp = -1;
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
                    int pomeshTemp2 = sistemaU2(new Point((int)X, (int)Y), out figura);
                    if (pomeshTemp != pomeshTemp2) pictureBox1.Load(@"Этаж" + (EtazT + 1).ToString() + ".png");
                    if (pomeshTemp2 >= 0)
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
                        pictureBox1.Load(@"Этаж" + (EtazT + 1).ToString() + ".png");
                    }//*/
                }
            }
        }
        public Bitmap ResizeBitmap(Bitmap b, int nWidth, int nHeight)
        {
            Bitmap result = new Bitmap(nWidth, nHeight);
            using (Graphics g = Graphics.FromImage((Image)result))
                g.DrawImage(b, 0, 0, nWidth, nHeight);
            return result;
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
                    int pom1 = sistemaU2(new Point((int)X, (int)Y), out figa1);
                    if (pom1 > -1)
                    {
                        comboBox5.Text=data[EtazT, 0, pom1];//корпус
                        comboBox6.Text=data[EtazT, 1, pom1];//помещение
                        timer1.Enabled = true;
                    }
                    else timer1.Enabled = false;
                }
            }
        }
        void BuildFigura(int et1, int pomesh1)
        {
            int i1=0;
            for(int i=0;i<39;i+=2)
            {
                if (koord[et1, i, pomesh1] == 0)
                {
                    break;
                }
                i1++;
            }
            figa1 = new Point[i1];
            i1=0;
            for (int i = 0; i < 40; i++)
            {
                if (koord[et1, i, pomesh1] != 0)
                {
                    figa1[i1].X = koord[et1, i, pomesh1];
                    i++;
                    figa1[i1].Y = koord[et1, i, pomesh1];
                    i1++;
                }
                else break;
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
                for (int i = 0; i < max1; i++)
                {
                   // if (arenda[0, et, 1, i] != null) Arend1.Add(arenda[0, et, 1, i]);
                    if(EtazT==et)if (data[EtazT, 0, i] != null) data1.Add(data[EtazT, 0, i]);
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
                for (int i = 0; i < max1; i++)
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
                if (flag_pokazanie) File[i] = DobavitRazdeliteli(File[i], 5);//chetchiki
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

        int[] kolvo = new int[4];
        int[, ,] koord = new int[4, 40, 1];
        const int RMD = 40;//размер таблицы data
        string[, ,] data = new string[4, RMD, 1];
        const int RMA = 8;
        string[, , ,] arenda = new string[10, 4, RMA, 1];
        const int RMS = 16;//размер таблицы счетчиков
        string[, , ,] chetchiki = new string[60, 4, RMS, 1];
        int EtazT = 0;//текущий этаж
        int PomeshenieT = 0;//текущее помещение
        int max1 = 300;
        string[] izmMass = new string[RMD];
        string[] izmMassSCH = new string[RMS];
        string[] izmMassA = new string[RMA];
        string[] dataRedact = { "", "" };//[1] - ключ electro или voda
        string dataModA = "";//дата измененная для таблицы арендаторов (расширение на перспективу. пока не реализована таблица на форме)
        string ToData(int etaz, int j, int schetchik, string s)
        {
            if (etaz == EtazT && schetchik == PomeshenieT)
            {
                if (izmMass[j] != null)
                {
                    return izmMass[j];
                }
                else return s;
            }
            else return s;
        }
        void LoadDB() //основная функция загрузки с раздельным внесением информации
        {
            
            int schetchik = 0;
            int etaz = 0;
            //int PomeshenieM = int.Parse(File[0]);            
            for (int i = 0; i < File.Count; i++)
            {
                if (File[i].IndexOf("[etaz_") > -1)
                {
                    kolvo[etaz] = int.Parse(File[i].Substring(8, File[i].Length - 8)) - 1;//количество помещений на этаже
                    //if (kolvo[etaz] > max1) max1 = kolvo[etaz];
                    etaz++;
                }
            }
            etaz = 0;
            for (int i = 0; i < File.Count; i++)
            {
                if (File[i].IndexOf("[etaz_") > -1)
                {
                    etaz = int.Parse(File[i].Substring(6, 1)) - 1;//номер этажа
                    schetchik = 0;
                }
                if (File[i] == "[" + schetchik + "]")
                {
                    i++;
                    string s = File[i];
                    if (File[i] != "=no koord=")
                    {
                        for (int j = 0; j < 40; j++)
                        {
                            if (s.IndexOf(";") > -1)
                            {
                                koord[etaz, j, schetchik] = int.Parse(s.Substring(0, s.IndexOf(";")));
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                koord[etaz, j, schetchik] = int.Parse(s);
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
                                data[etaz, j, schetchik] = ToData(etaz, j, schetchik, s.Substring(0, s.IndexOf(";")));
                           // }
                            s = s.Substring(s.IndexOf(";") + 1);
                        }
                        else
                        {
                            data[etaz, j, schetchik] = ToData(etaz, j, schetchik, s);
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
                                if (s.IndexOf(";") != 0) arenda[k, etaz, j, schetchik] = s.Substring(0, s.IndexOf(";"));
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                arenda[k, etaz, j, schetchik] = s;
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
                        for (int j = 0; j < RMS; j++)
                        {
                            if (s.IndexOf(";") > -1)
                            {
                                if (s.IndexOf(";") != 0)
                                {
                                    chetchiki[k, etaz, j, schetchik] = s.Substring(0, s.IndexOf(";"));
                                }
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                chetchiki[k, etaz, j, schetchik] = s;
                                break;
                            }
                        }
                    }

                    schetchik++;
                    i--;
                }
            }
            /*
            for (int floor = 0; floor < 4; floor++)
            {
                for (int numRoom = 0; numRoom < max1; numRoom++)
                { 
                
                }
            }*/
            toSchet(EtazT, PomeshenieT);//добавить функцию изменения счетчика
            ToArenda(EtazT, PomeshenieT);//добавить функцию изменения арендатора

            izmMass = new string[RMD];
            izmMassSCH = new string[RMS];
            izmMassA = new string[RMA];
            dataRedact = new string[2];
        }
        void SaveDB()
        {
            File.Clear();
            File.Add((kolvo[0] + kolvo[1] + kolvo[2] + kolvo[3] + 4).ToString());//записали общее количество помещений в начало
            for (int floor = 0; floor < 4; floor++)
            {
                File.Add("[etaz_" + (floor + 1).ToString() + "]" + (kolvo[floor] + 1).ToString());//запись номера этажа
                for (int numRoom = 0; numRoom <= kolvo[floor]; numRoom++)
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
                        if (chetchiki[k, floor, 0, numRoom] == null) break;
                        s = "";
                        for (int i = 0; i < RMS; i++)
                        {
                            s += chetchiki[k, floor, i, numRoom] + ";";
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
        void  addRowToMassiv(int floor, int numroom, int row)//записать в массив chetchiki строку izmMassSCH, освободив для нее место в указанной позиции row
        {//floor - номер этажа, numroom - номер помещения
            //1. Освободим строку row
            for (int i = 59; i > row; i--) //row не может быть меньше нуля
            {
                if (chetchiki[i-1, floor, 0, numroom] != null)
                {
                    for (int j = 0; j < RMS; j++)
                    {
                        chetchiki[i, floor, j, numroom] = chetchiki[i - 1, floor, j, numroom];
                    }
                }
            }
            //2. запишем в строку row значения
            writeStrToMass(floor, numroom, row);
        }
        void addRowToMassivA(int floor, int numroom, int row)//записать в массив Arenda строку izmMassA, освободив для нее место в указанной позиции row
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
            for (int j = 0; j < RMS; j++)
            {//заменим элемент массива (только тот, который не изменился)
                if (izmMassSCH[j] != null) chetchiki[row, floor, j, numroom] = izmMassSCH[j];
            }//row - строка, которую перезапишем строкой либо izmMassSCH[j] либо соседней(chetchiki[row+-, floor, j, numroom]), если дата за диапазоном.
            //добавим сюда функцию расчета расхода по воде-электричеству...
            RasxodFull(floor, numroom, DateTime.Parse(chetchiki[row, floor, 0, numroom]));//вопрос, нужно ли проверить заполнение данных по электроэнергии? или это расчет по воде?
        }

        void writeStrToMassA(int floor, int numroom, int row)
        {//запишем в строку row значения массива с измененными значениями.
            for (int j = 0; j < RMA; j++)
            {//заменим элемент массива (только тот, который не изменился)
                if (izmMassA[j] != null) arenda[row, floor, j, numroom] = izmMassA[j];
            }//row - строка, которую перезапишем строкой либо izmMassA[j] либо соседней(arenda[row+-, floor, j, numroom]), если дата за диапазоном.
        }
        bool removeRowInMassiv(int floor, int numroom, int row)//удалить строку row в таблице счетчиков
        {//floor - номер этажа, numroom - номер помещения
            if ((izmMassSCH[1] == null && dataRedact[1] == "voda") || (izmMassSCH[8] == null && dataRedact[1] == "electro"))
            {
                //1. удалим строку row
                for (; row < 59; row++)
                {
                    if (chetchiki[row, floor, 0, numroom] != null)
                    {
                        for (int j = 0; j < RMS; j++)
                        {
                            chetchiki[row, floor, j, numroom] = chetchiki[row + 1, floor, j, numroom];
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
            if (izmMassA[1] == "")
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
            if (dataRedact[1] == "electro")//запись с учетом ключа
            {                        //э 1,3,4,6,11,12,13,14,15
                if (izmMassSCH[0] != null) chetchiki[k, etaz, 0, schetchik] = izmMassSCH[0];//k-1 ошибка?
                if (izmMassSCH[1] != null) chetchiki[k, etaz, 1, schetchik] = izmMassSCH[1];
                if (izmMassSCH[3] != null) chetchiki[k, etaz, 3, schetchik] = izmMassSCH[3];
                if (izmMassSCH[4] != null) chetchiki[k, etaz, 4, schetchik] = izmMassSCH[4];
                if (izmMassSCH[6] != null) chetchiki[k, etaz, 6, schetchik] = izmMassSCH[6];
                if (izmMassSCH[11] != null) chetchiki[k, etaz, 11, schetchik] = izmMassSCH[11];
                if (izmMassSCH[12] != null) chetchiki[k, etaz, 12, schetchik] = izmMassSCH[12];
                if (izmMassSCH[13] != null) chetchiki[k, etaz, 13, schetchik] = izmMassSCH[13];
                if (izmMassSCH[14] != null) chetchiki[k, etaz, 14, schetchik] = izmMassSCH[14];
                if (izmMassSCH[15] != null) chetchiki[k, etaz, 15, schetchik] = izmMassSCH[15];
            }
            if (dataRedact[1] == "voda")
            {                        //в 2,5,7,8,9,10
                if (izmMassSCH[0] != null) chetchiki[k, etaz, 0, schetchik] = izmMassSCH[0];
                if (izmMassSCH[2] != null) chetchiki[k, etaz, 2, schetchik] = izmMassSCH[2];
                if (izmMassSCH[5] != null) chetchiki[k, etaz, 5, schetchik] = izmMassSCH[5];
                if (izmMassSCH[7] != null) chetchiki[k, etaz, 7, schetchik] = izmMassSCH[7];
                if (izmMassSCH[8] != null) chetchiki[k, etaz, 8, schetchik] = izmMassSCH[8];
                if (izmMassSCH[9] != null) chetchiki[k, etaz, 9, schetchik] = izmMassSCH[9];
                if (izmMassSCH[10] != null) chetchiki[k, etaz, 10, schetchik] = izmMassSCH[10];
            }
        }
        void clearRowKey(int floor, int numroom, int row)
        {
            if (izmMassSCH[1] == null)//удаление с учетом ключа
            {                        //э 1,3,4,6,11,12,13,14,15
                if (izmMassSCH[3] != null) chetchiki[row, floor, 3, numroom] = null;
                if (izmMassSCH[4] != null) chetchiki[row, floor, 4, numroom] = null;
                if (izmMassSCH[6] != null) chetchiki[row, floor, 6, numroom] = null;
                if (izmMassSCH[11] != null) chetchiki[row, floor, 11, numroom] = null;
                if (izmMassSCH[12] != null) chetchiki[row, floor, 12, numroom] = null;
                if (izmMassSCH[13] != null) chetchiki[row, floor, 13, numroom] = null;
                if (izmMassSCH[14] != null) chetchiki[row, floor, 14, numroom] = null;
                if (izmMassSCH[15] != null) chetchiki[row, floor, 15, numroom] = null;
            }
            if (izmMassSCH[8] == null)
            {                        //в 2,5,7,8,9,10
                if (izmMassSCH[2] != null) chetchiki[row, floor, 2, numroom] = null;
                if (izmMassSCH[5] != null) chetchiki[row, floor, 5, numroom] = null;
                if (izmMassSCH[7] != null) chetchiki[row, floor, 7, numroom] = null;
                if (izmMassSCH[9] != null) chetchiki[row, floor, 9, numroom] = null;
                if (izmMassSCH[10] != null) chetchiki[row, floor, 10, numroom] = null;
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
                if (floor == EtazT && numroom == PomeshenieT) return true;
            }
            return false;
        }
        void toSchet(int floor, int numroom)
        {
            if (floorNumRoom(floor,numroom))
            {//совпал номер помещения
                if (izmMassSCH[0] != null && izmMassSCH[0] != "")//изменение имеет место
                {
                    for (int row = 0; row < 60; row++)//пробежимся по таблице
                    { //у нас в наличии измененная строка {дата-0,показание_Э-1, показание_В-2, номер_Э-3, К_тр_Э-4, номер_В-5, расход_Э-6, кол-во_Сотр_В-7, 
                        //сч-р_В-8, тех-хо_В-9, расход_В-10, корп_Э-11, помещ_Э-12, этаж_Э-13, %_Э-14, С-кВт_Э-15}+dataRedact= дата редактируемая в datagrid
                        //если dataRedact не пустое, то изменилась дата... измененную дату мы не найдем, но если она пустая, то найдем. как искать?
                        if (chetchiki[row, floor, 0, numroom] == null) break;//пустые строки ниже сбросим
                        if (!(dataRedact[0] == "" || dataRedact[0] == null))
                        {
                            string ss = DateTime.Parse(dataRedact[0]).ToShortDateString();
                            if (chetchiki[row, floor, 0, numroom] == DateTime.Parse(dataRedact[0]).ToShortDateString())//dataRedact[0] - дата в строке, которая была до изменения, [1] - ключ (electro или voda)
                            {//изменилась дата: существующая дата изменила свой индекс row, либо она удалена совсем.
                                writeStrToMass(floor, numroom, row);//перед удалением запишем недостающие данные
                                if (removeRowInMassiv(floor, numroom, row))//если получилось удалить строку
                                {
                                    //izmMassSCH = new string[RMS];//очистим строку изменений 
                                }
                                for (row=0; row < 60; row++)
                                {
                                    if (chetchiki[row, floor, 0, numroom] != null)
                                    {
                                        if (DateTime.Parse(chetchiki[row, floor, 0, numroom]) < DateTime.Parse(izmMassSCH[0]))
                                        {
                                            addRowToMassiv(floor, numroom, row);//добавить строку и записать
                                            break;
                                        }
                                        if (DateTime.Parse(chetchiki[row, floor, 0, numroom]) == DateTime.Parse(izmMassSCH[0]))
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
                }
                else if (izmMassSCH[0] == "")
                {
                    for (int row = 0; row < 60; row++)
                    {
                        if (chetchiki[row, floor, 0, numroom] == dataRedact[0]) removeRowInMassiv(floor, numroom, row);
                    }
                }
            }
        }

        void ToArenda(int floor, int numroom)
        {
            if (floorNumRoom(floor, numroom))
            {//совпал номер помещения
                if (izmMassA[0] != null&&izmMassA[0] != "")//изменение имеет место
                {
                    for (int row = 0; row < 10; row++)//пробежимся по таблице
                    { //
                        if (arenda[row, floor, 0, numroom] == null) break;//пустые строки ниже сбросим
                        if (dataModA != "")
                        {
                            if (arenda[row, floor, 0, numroom] == dataModA)//dataModA - дата в строке, которая была до изменения
                            {//изменилась дата: существующая дата изменила свой индекс row, либо она удалена совсем.
                                writeStrToMassA(floor, numroom, row);//перед удалением запишем недостающие данные
                                removeRowInMassivA(floor, numroom, row);
                                for (row = 0; row < 10; row++)
                                {
                                    if (arenda[row, floor, 0, numroom] != null)
                                    {
                                        if (DateTime.Parse(arenda[row, floor, 0, numroom]) < DateTime.Parse(izmMassA[0]))
                                        {
                                            addRowToMassivA(floor, numroom, row);//добавить строку и записать
                                            break;
                                        }
                                        if (DateTime.Parse(arenda[row, floor, 0, numroom]) == DateTime.Parse(izmMassA[0]))
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
                    string s = arenda[0, floor, 1, numroom];
                    string s1 = arenda[0, 0, 1, 4];
                    for (int floor1 = 0; floor1 < 4; floor1++)//найдем и перезапишем данные арендатора по другим помещениям
                    {
                        for (int numroom1 = 0; numroom1 < max1; numroom1++)
                        {
                            if (floor1 == 0 && numroom1 == 4)
                            {
                                s1 = arenda[0, 0, 1, 4];
                                //
                            }
                            if (!(floor1 == EtazT && numroom1 == PomeshenieT))
                            {

                                if (arenda[0, floor1, 1, numroom1] == arenda[0, floor, 1, numroom])
                                {
                                    for (int j = 2; j < RMA; j++)
                                    {//перезапишем данные в остальных таблицах с учетом изменения по данному арендатору, кроме даты и самого арендатора (j=2)
                                        arenda[0, floor1, j, numroom1] = arenda[0, floor, j, numroom];
                                        richTextBox1.Text += arenda[0, floor1, j, numroom1]+"\r\n";
                                    }
                                }
                            }
                        }
                    }
                }
                else if (izmMassA[0] == "")
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
                    kolvo[etaz] = int.Parse(File[i].Substring(8, File[i].Length - 8)) - 1;//количество помещений на этаже
                    //if (kolvo[etaz] > max1) max1 = kolvo[etaz];
                    etaz++;
                }
            }
            label1.Text = "Загружено";
            koord = new int[4, 40, max1];//координаты помещения
            data = new string[4, RMD, max1];//все данные по помещению
            arenda = new string[10, 4, RMA, max1];//реквизиты арендатора
            chetchiki = new string[60, 4, RMS, max1];//показания счетчиков на последний период.
            int schetchik = 0;
            etaz = 0;
            LoadDB();
            /*
            for (int i = 0; i < File.Count; i++)
            {                
                if (File[i].IndexOf("[etaz_") > -1)
                {
                    etaz = int.Parse(File[i].Substring(6, 1)) - 1;//номер этажа
                    schetchik = 0;
                }
                if (File[i] == "[" + schetchik + "]")
                {
                    i++;
                    string s = File[i];
                    if (File[i] != "=no koord=")
                    {
                        for (int j = 0; j < 40; j++)
                        {
                            if (s.IndexOf(";") > -1)
                            {
                                koord[etaz, j, schetchik] = int.Parse(s.Substring(0, s.IndexOf(";")));
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                koord[etaz, j, schetchik] = int.Parse(s);
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
                            if (s.IndexOf(";") != 0) data[etaz, j, schetchik] = s.Substring(0, s.IndexOf(";"));
                            s = s.Substring(s.IndexOf(";") + 1);
                        }
                        else
                        {
                            data[etaz, j, schetchik] = s;
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
                                if (s.IndexOf(";") != 0) arenda[k, etaz, j, schetchik] = s.Substring(0, s.IndexOf(";"));
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                arenda[k, etaz, j, schetchik] = s;
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
                        for (int j = 0; j < RMS; j++)
                        {
                            if (s.IndexOf(";") > -1)
                            {
                                if (s.IndexOf(";") != 0) chetchiki[k, etaz, j, schetchik] = s.Substring(0, s.IndexOf(";"));
                                s = s.Substring(s.IndexOf(";") + 1);
                            }
                            else
                            {
                                chetchiki[k, etaz, j, schetchik] = s;
                                break;
                            }
                        }
                    }
                    schetchik++;
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
            pictureBox1.Load(@"Этаж" + (EtazT + 1).ToString() + ".png");//, System.Drawing.Imaging.ImageFormat.Png);

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
            double x = panelCentrX - (Centr(textBox1.Text).X) * scale / 20; //626 и 389 - это центр панели с пиктурбоксом
            double y = panelCentrY - (Centr(textBox1.Text).Y) * scale / 20;
            richTextBox1.Text += "x=" + x.ToString() + "; y=" + y.ToString() + "\r\n";
            pictureBox1.Location = new Point((int)x, (int)y);
            curnew = pictureBox1.Location;
            pictureBox1.Focus();
        }
        Point Centr(string koord)//вида: 0,123,45,79    x=0,y=123,x=45,y=79 и т.д.
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
        bool sistemaU(string koord, Point p1) //y=((x-x1)/(x2-x1))*(y2-y1)+y1 - функция с трассировкой вверх по игреку
        {
            bool otvet = false;
            if (koord == "") return false;
            else
            {
                double[,] mass = new double[2, 20]; //потолок - 20 координат
                int i1 = 0;
                for (; i1 < 40; i1++)
                {
                    if (koord.IndexOf(",") > 0)
                    {
                        mass[0, i1] = int.Parse(koord.Substring(0, koord.IndexOf(",")));
                        koord = koord.Substring(koord.IndexOf(",") + 1);
                        if (koord.IndexOf(",") > 0)
                        {
                            mass[1, i1] = int.Parse(koord.Substring(0, koord.IndexOf(",")));
                            koord = koord.Substring(koord.IndexOf(",") + 1);
                        }
                        else
                        {
                            mass[1, i1] = int.Parse(koord);
                            break;
                        }
                    }
                    else
                    {
                        break;
                    }
                }
                mass[0, i1 + 1] = mass[0, 0];
                mass[1, i1 + 1] = mass[1, 0];
                i1 += 2;
                //тут

                //int i = 1;
                for (int i = 1; i < i1; i++)
                {
                    double y11 = (((double)p1.X - mass[0, i - 1]) / (mass[0, i] - mass[0, i - 1])) * (mass[1, i] - mass[1, i - 1]) + mass[1, i - 1];
                    //   richTextBox1.Text += y11 + "\r\n";
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
                    if ((double)p1.X < max && (double)p1.X > min && y11 < p1.Y)//ограничиваем по иксу //трассировка вверх (игрек меньше точки)
                    {
                        if (otvet == false) otvet = true;
                        else otvet = false;
                    }
                }
            }
            return otvet;
        }
        int sistemaU2(Point p1, out Point[] P) //y=((x-x1)/(x2-x1))*(y2-y1)+y1 - функция с трассировкой вверх по игреку выводит Помещение
        {
            P = new Point[1];
            int otvet = -1;

            //потолок - 20 координат
            for (int j = 0; j <= kolvo[EtazT]; j++)
            {
                int i1 = 0;
                int i2 = 0;
                double[,] mass = new double[2, 20];
                for (; i1 < 40; i1++, i2++)//пройти по координатам
                {
                    if (koord[EtazT, i1, j] != 0)
                    {
                        mass[0, i2] = koord[EtazT, i1, j];
                        i1++;
                        mass[1, i2] = koord[EtazT, i1, j];
                    }
                    else break;
                }
                mass[0, i2] = mass[0, 0];
                mass[1, i2] = mass[1, 0];
                //i1 += 2;

                bool otvetB = false;
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
                        if (otvetB == false) otvetB = true;
                        else otvetB = false;
                    }
                }
                if (otvetB == true)
                {
                    otvet = j;
                    P = new Point[i2];
                    for (int i = 0; i < i2; i++)
                    {
                        P[i].X = (int)mass[0, i];
                        P[i].Y = (int)mass[1, i];
                    }
                    break;
                }
            }
            return otvet;
        }
        //*/
        private void button4_Click(object sender, EventArgs e)
        {
            label3.Text = Centr(textBox1.Text).X + ";" + Centr(textBox1.Text).Y;
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
        int GlobalP = 21;
        private void button9_Click(object sender, EventArgs e)
        {
            g3 = false;
            GlobalP = 0;
            poligon1 = new Point[20];
            textBox1.Text = "";
            button9.Enabled = false;

            PomeshenieT = FindPom(comboBox5.Text, comboBox6.Text);
            if (PomeshenieT < 0)
            {
                kolvo[EtazT]++;
                PomeshenieT = kolvo[EtazT];
                data[EtazT, 0, PomeshenieT] = comboBox5.Text;
                data[EtazT, 1, PomeshenieT] = comboBox6.Text;
            }
            if (koord[EtazT, 0, PomeshenieT] != 0)
            {
                for (int i = 0; i < 40; i++)
                {
                    if (koord[EtazT, i, PomeshenieT] != 0) koord[EtazT, i, PomeshenieT] = 0;
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
                    koord[EtazT, i1, PomeshenieT] = poligon1[i].X;
                    s += poligon1[i].Y.ToString() + ",";
                    i1++;
                    koord[EtazT, i1, PomeshenieT] = poligon1[i].Y;
                    i1++;
                    figa1[i] = poligon1[i];
                }
                s = s.Substring(0, s.Length - 1);

                g.DrawPolygon(new Pen(Color.Green, 5), figa1);
                // g.FillEllipse(Brushes.Red, Centr(s).X, Centr(s).Y, 6, 6);
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
            Point[] figura = new Point[3];
            figura[0] = new Point(250, 100);
            figura[1] = new Point(1400, 200);
            figura[2] = new Point(500, 650);
            bitmap = new Bitmap(pictureBox1.Image);
            g = Graphics.FromImage(bitmap);
            //g.DrawLine(new Pen(Color.Green, 5), new Point(int.Parse(textBox2.Text), int.Parse(textBox3.Text)), new Point(int.Parse(textBox2.Text) + 100, int.Parse(textBox3.Text)));
            g.DrawPolygon(new Pen(Color.Green, 5), figura);
            string s = "";
            for (int i = 0; i < 3; i++)
            {
                s += figura[i].X.ToString() + ",";
                s += figura[i].Y.ToString() + ",";
            }
            s = s.Substring(0, s.Length - 1);
            textBox1.Text = s;
            g.FillEllipse(Brushes.Black, Centr(s).X, Centr(s).Y, 6, 6);
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
            if (checkBox2.Checked) pictureBox1.Image.Save(@"Этаж" + (EtazT + 1).ToString() + ".png", System.Drawing.Imaging.ImageFormat.Png);
            System.IO.File.WriteAllLines(@"Data.txt", File, Encoding.Default);
            System.IO.File.WriteAllLines(@DateTime.Now.ToShortDateString() + ".txt", File, Encoding.Default);
        }
        bool panelHide = false;
        private void button15_Click(object sender, EventArgs e)
        {
            if (panelHide == false)
            {
                panel1.Dock = DockStyle.Fill;
                tabControl1.Dock = DockStyle.None;
                panelHide = true;
            }
            else
            {
                panel1.Dock = DockStyle.Fill;
                tabControl1.Dock = DockStyle.Right;
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
            File.Add((kolvo[0] + kolvo[1] + kolvo[2] + kolvo[3] + 4).ToString());//записали общее количество помещений в начало
            for (int etaz = 0; etaz < 4; etaz++)
            {
                File.Add("[etaz_" + (etaz + 1).ToString() + "]" + (kolvo[etaz] + 1).ToString());//запись номера этажа
                for (int pomeshenie = 0; pomeshenie <= kolvo[etaz]; pomeshenie++)
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
                        if (chetchiki[k, etaz, 0, pomeshenie] == null) break;
                        s = "";
                        for (int i = 0; i < RMS; i++)
                        {
                            s += chetchiki[k, etaz, i, pomeshenie] + ";";
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
            EtazT = 0;
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
            EtazT = 1;
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
            EtazT = 2;
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
            EtazT = 3;
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
            {//найти индекс помещения. Если совпадений нет, то: kolvo[EtazT]++; PomeshenieT=kolvo[EtazT];
                if (kolvo[EtazT] == -1)
                {
                    kolvo[EtazT]++;
                    PomeshenieT = kolvo[EtazT];
                }
                else
                {
                    PomeshenieT = FindPom(comboBox5.Text, comboBox6.Text);
                    if (PomeshenieT < 0)
                    {
                        kolvo[EtazT]++;
                        PomeshenieT = kolvo[EtazT];
                    }
                    else
                    {
                        //вписать остальные данные по этому помещению?
                    }
                }//шпора data шпаргалка                
                if (!(data[EtazT, 0, PomeshenieT] ==null&& comboBox5.Text=="")&&(data[EtazT, 0, PomeshenieT] != comboBox5.Text.Replace(";", ","))) 
                {
                    izmMass[0] = comboBox5.Text.Replace(";", ",");//корпус
                }                
                if (!(data[EtazT, 1, PomeshenieT] ==null&& comboBox6.Text=="")&&(data[EtazT, 1, PomeshenieT] != comboBox6.Text.Replace(";", ",")))
                {
                    izmMass[1] = comboBox6.Text.Replace(";", ",");//помещение
                }
                if (!(data[EtazT, 2, PomeshenieT] == null&&comboBox7.Text=="")&&(data[EtazT, 2, PomeshenieT] != comboBox7.Text.Replace(";", ",")))
                {
                    izmMass[2] = comboBox7.Text.Replace(";", ",");//запитка от тп
                }
                if (!(data[EtazT, 3, PomeshenieT] ==null&& comboBox8.Text=="")&&(data[EtazT, 3, PomeshenieT] != comboBox8.Text.Replace(";", ",")))
                {
                    izmMass[3]= comboBox8.Text.Replace(";", ",");//запитка от сп
                }
                if (!(data[EtazT, 4, PomeshenieT] ==null&& comboBox9.Text=="")&&(data[EtazT, 4, PomeshenieT] != comboBox9.Text.Replace(";", ",")))
                {
                    izmMass[4] = comboBox9.Text.Replace(";", ",");//марка кабеля
                }
                if (!(data[EtazT, 5, PomeshenieT] ==null&& comboBox10.Text=="")&&(data[EtazT, 5, PomeshenieT] != comboBox10.Text.Replace(";", ",")))
                {
                    izmMass[5] = comboBox10.Text.Replace(";", ",");//длина кабеля (м)
                }
                if (!(data[EtazT, 6, PomeshenieT] ==null&& comboBox11.Text=="")&&(data[EtazT, 6, PomeshenieT] != comboBox11.Text.Replace(";", ",")))
                {
                    izmMass[6] = comboBox11.Text.Replace(";", ",");//мощность кВт
                }
                if (!(data[EtazT, 7, PomeshenieT] ==null&& comboBox12.Text=="")&&(data[EtazT, 7, PomeshenieT] != comboBox12.Text.Replace(";", ",")))
                {
                    izmMass[7] = comboBox12.Text.Replace(";", ",");//тип отключающего устройства
                }
                if (!(data[EtazT, 8, PomeshenieT] ==null&& comboBox13.Text=="")&&(data[EtazT, 8, PomeshenieT] != comboBox13.Text.Replace(";", ",")))
                {
                    izmMass[8] = comboBox13.Text.Replace(";", ",");//Уставка (А) In
                }
                if (!(data[EtazT, 9, PomeshenieT] ==null&& comboBox14.Text=="")&&(data[EtazT, 9, PomeshenieT] != comboBox14.Text.Replace(";", ",")))
                {
                    izmMass[9] = comboBox14.Text.Replace(";", ",");//Номер электросчетчика
                }
                if (!(data[EtazT, 10, PomeshenieT] ==null&& comboBox15.Text=="")&&(data[EtazT, 10, PomeshenieT] != comboBox15.Text.Replace(";", ",")))
                {
                    izmMass[10] = comboBox15.Text.Replace(";", ",");//марка электросчетчика
                }
                if (!(data[EtazT, 11, PomeshenieT] ==null&& textBox4.Text=="")&&(data[EtazT, 11, PomeshenieT] != textBox4.Text.Replace(";", ",")))
                {
                    izmMass[11] = textBox4.Text.Replace(";", ",");//год в/поверки эл.счетчика
                }
                if (!(data[EtazT, 12, PomeshenieT] ==null&& comboBox16.Text=="")&&(data[EtazT, 12, PomeshenieT] != comboBox16.Text.Replace(";", ",")))
                {
                    izmMass[12] = comboBox16.Text.Replace(";", ",");//номер водомера
                }
                if (!(data[EtazT, 13, PomeshenieT] ==null&& comboBox17.Text=="")&&(data[EtazT, 13, PomeshenieT] != comboBox17.Text.Replace(";", ",")))
                {
                    izmMass[13] = comboBox17.Text.Replace(";", ",");//марка водомера
                }
                if (!(data[EtazT, 14, PomeshenieT] ==null&& textBox5.Text=="")&&(data[EtazT, 14, PomeshenieT] != textBox5.Text.Replace(";", ",")))
                {
                    izmMass[14] = textBox5.Text.Replace(";", ",");//год в/поверки водомера
                }
                if (!(data[EtazT, 15, PomeshenieT] ==null&& comboBox18.Text=="")&&(data[EtazT, 15, PomeshenieT] != comboBox18.Text.Replace(";", ",")))
                {
                    izmMass[15] = comboBox18.Text.Replace(";", ",");//коэффициент ТТ
                }
                if (!(data[EtazT, 16, PomeshenieT] ==null&& textBox6.Text=="")&&(data[EtazT, 16, PomeshenieT] != textBox6.Text.Replace(";", ",")))
                {
                    izmMass[16] = textBox6.Text.Replace(";", ",");//номер фазы А
                }
                if (!(data[EtazT, 17, PomeshenieT] ==null&&textBox7.Text=="")&&(data[EtazT, 17, PomeshenieT] != textBox7.Text.Replace(";", ",")))
                {
                    izmMass[17] = textBox7.Text.Replace(";", ",");//номер фазы В
                }
                if (!(data[EtazT, 18, PomeshenieT] ==null&& textBox8.Text=="")&&(data[EtazT, 18, PomeshenieT] != textBox8.Text.Replace(";", ",")))
                {
                    izmMass[18] = textBox8.Text.Replace(";", ",");//номер фазы С
                }
                if (!(data[EtazT, 19, PomeshenieT]==null&& textBox9.Text=="")&&(data[EtazT, 19, PomeshenieT] != textBox9.Text.Replace(";", ",")))
                {
                    izmMass[19] = textBox9.Text.Replace(";", ",");//год в/поверки
                }
                if (!(data[EtazT, 20, PomeshenieT] ==null&& dateTimePicker3.Value.ToShortDateString()=="")&&(data[EtazT, 20, PomeshenieT] != dateTimePicker3.Value.ToShortDateString().Replace(";", ",")))
                {
                    izmMass[20] = dateTimePicker3.Value.ToShortDateString().Replace(";", ",");//дата опломбировки эл.счетчика
                }
                if (!(data[EtazT, 21, PomeshenieT] ==null&& textBox12.Text=="")&&(data[EtazT, 21, PomeshenieT] != textBox12.Text.Replace(";", ",")))
                {
                    izmMass[21] = textBox12.Text.Replace(";", ",");//№ пломбы эл.счетчика
                }
                if (!(data[EtazT, 22, PomeshenieT] ==null&& textBox14.Text=="")&&(data[EtazT, 22, PomeshenieT] != textBox14.Text.Replace(";", ",")))
                {
                    izmMass[22] = textBox14.Text.Replace(";", ",");//№ пломбы ТТ "А"
                }
                if (!(data[EtazT, 23, PomeshenieT] ==null&& textBox15.Text=="")&&(data[EtazT, 23, PomeshenieT] != textBox15.Text.Replace(";", ",")))
                {
                    izmMass[23] = textBox15.Text.Replace(";", ",");//№ пломбы ТТ "В"
                }
                if (!(data[EtazT, 24, PomeshenieT] ==null&& textBox16.Text=="")&&(data[EtazT, 24, PomeshenieT] != textBox16.Text.Replace(";", ",")))
                {
                    izmMass[24]= textBox16.Text.Replace(";", ",");//№ пломбы ТТ "С"
                }
                if (!(data[EtazT, 25, PomeshenieT] ==null&& dateTimePicker4.Value.ToShortDateString()=="")&&(data[EtazT, 25, PomeshenieT] != dateTimePicker4.Value.ToShortDateString().Replace(";", ",")))
                {
                    izmMass[25] = dateTimePicker4.Value.ToShortDateString().Replace(";", ",");//дата опломбировки водомера
                }
                if (!(data[EtazT, 26, PomeshenieT] ==null&& textBox13.Text=="")&&(data[EtazT, 26, PomeshenieT] != textBox13.Text.Replace(";", ",")))
                {
                    izmMass[26] = textBox13.Text.Replace(";", ",");//№ пломбы водомера
                }                
                if(!(data[EtazT, 27, PomeshenieT] ==null&& textBox19.Text=="")&&(data[EtazT, 27, PomeshenieT] != textBox19.Text.Replace(";", ",")))
                {
                    izmMass[27] = textBox19.Text.Replace(";", ",");//кв.м.               
                }                
                if( !(data[EtazT, 28, PomeshenieT] ==null&& textBox22.Text=="")&&(data[EtazT, 28, PomeshenieT] != textBox22.Text.Replace(";", ",")))
                { 
                    izmMass[28] = textBox22.Text.Replace(";", ",");//Планировка
                }               
                if(!(data[EtazT, 29, PomeshenieT] ==null&& textBox23.Text=="")&&(data[EtazT, 29, PomeshenieT] != textBox23.Text.Replace(";", ",")))
                {
                    izmMass[29] = textBox23.Text.Replace(";", ",");//Однолинейная схема
                }
                if(!(data[EtazT, 30, PomeshenieT] ==null&& textBox24.Text=="")&&(data[EtazT, 30, PomeshenieT] != textBox24.Text.Replace(";", ",")))
                {
                    izmMass[30] = textBox24.Text.Replace(";", ",");//План электросети
                }
                if (!(data[EtazT, 31, PomeshenieT] ==null&& textBox25.Text=="")&&(data[EtazT, 31, PomeshenieT] != textBox25.Text.Replace(";", ",")))
                {
                    izmMass[31] = textBox25.Text.Replace(";", ",");//План водоснабжения
                }                
                //data[EtazT, 32, PomeshenieT] = textBox26.Text.Replace(";", ",");//Папка арендатора
                int k = 0;
                //SdvigCHtoOne(arenda, 10, dateTimePicker1.Value.ToShortDateString().Replace(";", ","),5); //в скобочках длина массива, котрый сдвигается на 1.

                //ЕСЛИ арендатор не меняется, не нужно записывать новую строку. в остальных случаях новая запись.
                if (arenda[k, EtazT, 0, PomeshenieT] != dateTimePicker1.Value.ToShortDateString().Replace(";", ","))
                {
                    izmMassA[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");//дата начала аренды
                }
                if (!(arenda[k, EtazT, 1, PomeshenieT] == null && comboBox1.Text == "") && (arenda[k, EtazT, 1, PomeshenieT] != comboBox1.Text.Replace(";", ",")))
                {
                    izmMassA[1] = comboBox1.Text.Replace(";", ",");//арендатор
                    if (izmMassA[0] == null) izmMassA[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, EtazT, 2, PomeshenieT] == null && comboBox2.Text == "") && (arenda[k, EtazT, 2, PomeshenieT] != comboBox2.Text.Replace(";", ",")))
                {
                    izmMassA[2] = comboBox2.Text.Replace(";", ",");//ФИО
                    if (izmMassA[0] == null) izmMassA[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, EtazT, 3, PomeshenieT] == null && comboBox3.Text == "") && (arenda[k, EtazT, 3, PomeshenieT] != comboBox3.Text.Replace(";", ",")))
                {
                    izmMassA[3] = comboBox3.Text.Replace(";", ",");//должность
                    if (izmMassA[0] == null) izmMassA[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, EtazT, 4, PomeshenieT] == null && comboBox4.Text == "") && (arenda[k, EtazT, 4, PomeshenieT] != comboBox4.Text.Replace(";", ",")))
                {
                    izmMassA[4] = comboBox4.Text.Replace(";", ",");//кол-во сотрудников
                    if (izmMassA[0] == null) izmMassA[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, EtazT, 5, PomeshenieT] == null && textBox17.Text == "") && (arenda[k, EtazT, 5, PomeshenieT] != textBox17.Text.Replace(";", ",")))
                {
                    izmMassA[5] = textBox17.Text.Replace(";", ",");//e-mail
                    if (izmMassA[0] == null) izmMassA[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, EtazT, 6, PomeshenieT] == null && richTextBox3.Text == "") && (arenda[k, EtazT, 6, PomeshenieT] != richTextBox3.Text.Replace(";", ",").Replace("\n", "&rn")))
                {
                    izmMassA[6] = richTextBox3.Text.Replace(";", ",").Replace("\n", "&rn");//прочее и телефоны
                    if (izmMassA[0] == null) izmMassA[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (!(arenda[k, EtazT, 7, PomeshenieT] == null && textBox26.Text == "") && (arenda[k, EtazT, 7, PomeshenieT] != textBox26.Text.Replace(";", ",")))
                {
                    izmMassA[7] = textBox26.Text.Replace(";", ",");//Папка арендатора
                    if (izmMassA[0] == null) izmMassA[0] = dateTimePicker1.Value.ToShortDateString().Replace(";", ",");
                }
                if (izmMassA[0] != null) dataModA = arenda[k, EtazT, 0, PomeshenieT];//изменение имеет место, запишем в dataModA значение даты до изменения

               // if (!(textBox10.Text == "" && textBox11.Text == ""))
               // {//сюда функцию запишем счетчики
               //     WriteSchet(EtazT, PomeshenieT, dateTimePicker2.Value, textBox10.Text, textBox11.Text, comboBox14.Text, comboBox18.Text, comboBox16.Text, comboBox4.Text);
              //  }

                if (chetchiki[0, EtazT, 0, PomeshenieT] != dateTimePicker2.Value.ToShortDateString().Replace(";", ","))
                {
                    izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                if (!(chetchiki[0, EtazT, 1, PomeshenieT] == null && textBox10.Text == "") && (chetchiki[0, EtazT, 1, PomeshenieT] != textBox10.Text.Replace(";", ",")))
                {
                    izmMassSCH[1] = textBox10.Text.Replace(";", ",");//показания электроэнергии
                    if (izmMassSCH[0] == null) izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                if (!(chetchiki[0, EtazT, 2, PomeshenieT] == null && textBox11.Text == "") && (chetchiki[0, EtazT, 2, PomeshenieT] != textBox11.Text.Replace(";", ",")))
                {
                    izmMassSCH[2] = textBox11.Text.Replace(";", ",");//показания водомера
                    if (izmMassSCH[0] == null) izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                if (!(chetchiki[0, EtazT, 3, PomeshenieT] == null && comboBox14.Text == "") && (chetchiki[0, EtazT, 3, PomeshenieT] != comboBox14.Text.Replace(";", ",")))
                {
                    izmMassSCH[3] = comboBox14.Text.Replace(";", ",");//номер электросчетчика
                    if (izmMassSCH[0] == null) izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                if (!(chetchiki[0, EtazT, 4, PomeshenieT] == null && comboBox18.Text == "") && (chetchiki[0, EtazT, 4, PomeshenieT] != comboBox18.Text.Replace(";", ",")))
                {
                    izmMassSCH[4] = comboBox18.Text.Replace(";", ",");//коэффициент трансформации
                    if (izmMassSCH[0] == null) izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                if (!(chetchiki[0, EtazT, 5, PomeshenieT] == null && comboBox16.Text == "") && (chetchiki[0, EtazT, 5, PomeshenieT] != comboBox16.Text.Replace(";", ",")))
                {
                    izmMassSCH[5] = comboBox16.Text.Replace(";", ",");//номер водомера
                    if (izmMassSCH[0] == null) izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }
                //расчет
                if (!(chetchiki[0, EtazT, 7, PomeshenieT] == null && comboBox4.Text == "") && (chetchiki[0, EtazT, 7, PomeshenieT] != comboBox4.Text.Replace(";", ",")))
                {
                    izmMassSCH[7] = comboBox4.Text.Replace(";", ",");//количество сотрудников (для воды) 
                    if (izmMassSCH[0] == null) izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                }

                if (checkBox5.Checked)
                {
                    if (!(chetchiki[k, EtazT, 8, PomeshenieT] == null && comboBox21.Text == "") && (chetchiki[k, EtazT, 8, PomeshenieT] != comboBox21.Text.Replace(";", ",")))
                    {
                        izmMassSCH[8] = comboBox21.Text.Replace(";", ",");//корпус
                        if (izmMassSCH[0] == null) izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                    }
                    if (!(chetchiki[k, EtazT, 9, PomeshenieT] == null && comboBox21.Text == "") && (chetchiki[k, EtazT, 9, PomeshenieT] != comboBox21.Text.Replace(";", ",")))
                    {
                        izmMassSCH[9] = comboBox21.Text.Replace(";", ",");//помещение
                        if (izmMassSCH[0] == null) izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                    }
                    if (!(chetchiki[k, EtazT, 10, PomeshenieT] == null && ToEt(chetchiki[k, EtazT, 8, PomeshenieT], chetchiki[k, EtazT, 9, PomeshenieT]) == "") && (chetchiki[k, EtazT, 10, PomeshenieT] != ToEt(chetchiki[k, EtazT, 8, PomeshenieT], chetchiki[k, EtazT, 9, PomeshenieT])))
                    {
                        izmMassSCH[10] = ToEt(chetchiki[k, EtazT, 8, PomeshenieT], chetchiki[k, EtazT, 9, PomeshenieT]);//этаж
                        if (izmMassSCH[0] == null) izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                    }
                    if (!(chetchiki[k, EtazT, 11, PomeshenieT] == null && comboBox22.Text == "") && (chetchiki[k, EtazT, 11, PomeshenieT] != comboBox22.Text.Replace(";", ",")))
                    {
                        izmMassSCH[11] = comboBox22.Text.Replace(";", ",");//% кВт
                        if (izmMassSCH[0] == null) izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                    }
                    if (!(chetchiki[k, EtazT, 12, PomeshenieT] == null && textBox21.Text == "") && (chetchiki[k, EtazT, 12, PomeshenieT] != textBox21.Text.Replace(";", ",")))
                    {
                        izmMassSCH[12] = textBox21.Text.Replace(";", ",");//С постоянная величина кВт
                        if (izmMassSCH[0] == null) izmMassSCH[0] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//Дата показаний воды/электроэнергии
                    }
                }
            }
            time3 = 0;
            //button19.Enabled = false;
            this.Enabled = false;
            timer3.Interval = 100;
            timer3.Enabled = true;
        }
        void WriteSchet(int etT, int pomesT, DateTime DTP2v, string pokE,string pokV,string NchE,string Ktr,string NchV,string KolSotr)
        {
            int k = 0;
            if (chetchiki[0, etT, 0, pomesT] != DTP2v.ToShortDateString().Replace(";", ",") && chetchiki[0, etT, 0, pomesT] != null)
            {
                for (int i = 0; i < 60; i++)
                {
                    if (chetchiki[i, etT, 0, pomesT] == null) break;
                    if (chetchiki[i, etT, 0, pomesT] == DTP2v.ToShortDateString().Replace(";", ","))//в записях по счетчику встретили существующие показания на указанную дату
                    {
                        k = i;
                        break;
                    }
                }
                if (chetchiki[k, etT, 0, pomesT] != DTP2v.ToShortDateString().Replace(";", ","))//7-207-15 17-00
                {
                    for (int i = 59; i > 0; i--)
                    {
                        if (DateTime.Parse(chetchiki[0, etT, 0, pomesT]) < DTP2v)
                        {
                            if (chetchiki[i - 1, etT, 0, pomesT] != null)
                            {
                                for (int Rmass = RMS - 1; Rmass > -1; Rmass--)
                                {                                    
                                    if(!(chetchiki[i, etT, Rmass, pomesT] ==null&& chetchiki[i - 1, etT, Rmass, pomesT]==null)&&(chetchiki[i, etT, Rmass, pomesT] != chetchiki[i - 1, etT, Rmass, pomesT]))
                                    {
                                        chetchiki[i, etT, Rmass, pomesT] = chetchiki[i - 1, etT, Rmass, pomesT];
                                    }
                                } 
                            }
                        }
                        else
                        {
                            if (chetchiki[i - 1, etT, 0, pomesT] != null)
                            {
                                if (DateTime.Parse(chetchiki[i - 1, etT, 0, pomesT]) > DTP2v)
                                {
                                    k = i;
                                    break;
                                }
                                else for (int Rmass = RMS - 1; Rmass > -1; Rmass--)
                                    {
                                        if (!(chetchiki[i, etT, Rmass, pomesT] ==null&& chetchiki[i - 1, etT, Rmass, pomesT]==null)&&(chetchiki[i, etT, Rmass, pomesT] != chetchiki[i - 1, etT, Rmass, pomesT]))
                                        {
                                            chetchiki[i, etT, Rmass, pomesT] = chetchiki[i - 1, etT, Rmass, pomesT];
                                        }                                        
                                    } 
                            }
                        }
                    }
                }
            }
            if (!(chetchiki[k, etT, 0, pomesT] ==null&& DTP2v.ToShortDateString()=="")&&(chetchiki[k, etT, 0, pomesT] != DTP2v.ToShortDateString().Replace(";", ",")))
            {
                chetchiki[k, etT, 0, pomesT] = DTP2v.ToShortDateString().Replace(";", ",");//дата съема показаний
            }            
            //"+" означает параметр, который не будет перезаписан. Использовать именованные параметры здесь не стану (неудобно, считаю)
            if (pokE != "+")
            {
                if (!(chetchiki[k, etT, 1, pomesT] == null && pokE == "") && (chetchiki[k, etT, 1, pomesT] != pokE.Replace(";", ",").Replace(".", ",")))
                {
                    chetchiki[k, etT, 1, pomesT] = pokE.Replace(";", ",").Replace(".", ",");//показание электросчетчика+
                }
            }
            if (pokV != "+")
            {
                if (!(chetchiki[k, etT, 2, pomesT] == null && pokV == "") && (chetchiki[k, etT, 2, pomesT] != pokV.Replace(";", ",")))
                {
                    chetchiki[k, etT, 2, pomesT] = pokV.Replace(";", ",");//показание водомера 
                }                
            }
            if (NchE != "+")
            {
                if (!(chetchiki[k, etT, 3, pomesT] ==null&& NchE=="")&&(chetchiki[k, etT, 3, pomesT] != NchE.Replace(";", ",")))
                {
                    chetchiki[k, etT, 3, pomesT] = NchE.Replace(";", ",");//номер электросчетчика+
                }                
            }
            if (Ktr != "+")
            {
                if (!(chetchiki[k, etT, 4, pomesT] ==null&& Ktr=="")&&(chetchiki[k, etT, 4, pomesT] != Ktr.Replace(";", ",")))
                {
                    chetchiki[k, etT, 4, pomesT] = Ktr.Replace(";", ",");//коэфф. трансформации+
                }                
            }
            if (NchV != "+")
            {
                if (!(chetchiki[k, etT, 5, pomesT] ==null&& NchV=="")&&(chetchiki[k, etT, 5, pomesT] != NchV.Replace(";", ",")))
                {
                    chetchiki[k, etT, 5, pomesT] = NchV.Replace(";", ",");//номер водомера
                }                
            }
            //расход, если в начале месяца (до 7-го числа), то за предыдущий период, иначе за текущий.
            //Rasxod3(etT, pomesT, dateTimePicker2.Value.Day<7?new DateTime(dateTimePicker2.Value.Year,dateTimePicker2.Value.Month-1,dateTimePicker2.Value.Day):dateTimePicker2.Value);
            if (Ktr != "+" && pokE != "+") RasxodFull(etT, pomesT, DTP2v);
            if (KolSotr != "+")
            {
                if (!(chetchiki[k, etT, 7, pomesT] ==null&& KolSotr=="")&&(chetchiki[k, etT, 7, pomesT] != KolSotr.Replace(";", ",")))
                {
                    chetchiki[k, etT, 7, pomesT] = KolSotr.Replace(";", ",");//количество сотрудников (для воды) 
                }                
            }
            if (checkBox5.Checked)
            {
                if (!(chetchiki[k, etT, 8, pomesT] == null && comboBox21.Text == "") && (chetchiki[k, etT, 8, pomesT] != comboBox21.Text.Replace(";", ",")))
                {
                    chetchiki[k, etT, 8, pomesT] = comboBox21.Text.Replace(";", ",");//корпус
                }
                if (!(chetchiki[k, etT, 9, pomesT] == null && comboBox21.Text == "") && (chetchiki[k, etT, 9, pomesT] != comboBox21.Text.Replace(";", ",")))
                {
                    chetchiki[k, etT, 9, pomesT] = comboBox21.Text.Replace(";", ",");//помещение
                }
                if (!(chetchiki[k, etT, 10, pomesT] == null && ToEt(chetchiki[k, etT, 8, pomesT], chetchiki[k, etT, 9, pomesT]) == "") && (chetchiki[k, etT, 10, pomesT] != ToEt(chetchiki[k, etT, 8, pomesT], chetchiki[k, etT, 9, pomesT])))
                {
                    chetchiki[k, etT, 10, pomesT] = ToEt(chetchiki[k, etT, 8, pomesT], chetchiki[k, etT, 9, pomesT]);//этаж
                }
                if (!(chetchiki[k, etT, 11, pomesT] == null && comboBox22.Text == "") && (chetchiki[k, etT, 11, pomesT] != comboBox22.Text.Replace(";", ",")))
                {
                    chetchiki[k, etT, 11, pomesT] = comboBox22.Text.Replace(";", ",");//% кВт
                }
                if (!(chetchiki[k, etT, 12, pomesT] == null && textBox21.Text == "") && (chetchiki[k, etT, 12, pomesT] != textBox21.Text.Replace(";", ",")))
                {
                    chetchiki[k, etT, 12, pomesT] = textBox21.Text.Replace(";", ",");//С постоянная величина кВт
                }
            }
        }

        string ToEt(string korpus, string pomeshenie)
        {
            for (int et = 0; et < 4; et++)
            {
                for (int pomesh = 0; pomesh < max1; pomesh++)
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
            for (int i = 0; i <= kolvo[EtazT]; i++)
            {
                if (data[EtazT, 0, i] == korp && data[EtazT, 1, i] == pomesh)
                {
                    rezult = i;
                    break;
                }
            }
            return rezult;
        }
        void SdvigCHtoOne(string[, , ,] chetch1, int i, string data1, int Rmass)
        {//массив, который нужно сдвинуть, чтобы записать в первую ячейку нужные данные
            //i=количество элементов массива "Х" string[Х, , ,] chetch1 (первый столбик)
            //data1 - проверка текущей даты. Если дата текущая в первой строке элементов, то двигать ничего не нужно
            //количество элементов массива "Х" string[, ,Х ,] chetch1 (третий столбик)
            if (chetch1[0, EtazT, 0, PomeshenieT] != data1 && chetch1[0, EtazT, 0, PomeshenieT] != null)
            {
                i--;
                Rmass--;
                for (; i > 0; i--)
                {
                    if (DateTime.Parse(chetch1[0, EtazT, 0, PomeshenieT]) < DateTime.Parse(data1))
                    {
                        if (chetch1[i - 1, EtazT, 0, PomeshenieT] != null)
                        {
                            for (; Rmass > -1; Rmass--) chetch1[i, EtazT, Rmass, PomeshenieT] = chetch1[i - 1, EtazT, Rmass, PomeshenieT];
                        }
                    }
                    else
                    {

                        if (chetch1[i - 1, EtazT, 0, PomeshenieT] != null)
                        {
                            if (DateTime.Parse(chetch1[i - 1, EtazT, 0, PomeshenieT]) > DateTime.Parse(data1))
                            {
                                for (; Rmass > -1; Rmass--) chetch1[i, EtazT, Rmass, PomeshenieT] = chetch1[i - 1, EtazT, Rmass, PomeshenieT];
                            }
                            else for (; Rmass > -1; Rmass--) chetch1[i, EtazT, Rmass, PomeshenieT] = chetch1[i - 1, EtazT, Rmass, PomeshenieT];

                        }
                    }
                }
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            kontur(PomeshenieT);
        }
        private void comboBox6_TextChanged(object sender, EventArgs e)
        {
            if (comboBox5.Text != "" && comboBox6.Text != "")
            {
                if (GlobalP == 21 && UserKey == "admin") button9.Enabled = true;
                PomeshenieT = FindPom(comboBox5.Text.Replace(";", ","), comboBox6.Text.Replace(";", ","));
                if (PomeshenieT != -1)
                {
                    ClearCB2();
                    comboBox7.Text = data[EtazT, 2, PomeshenieT];
                    comboBox8.Text = data[EtazT, 3, PomeshenieT];
                    comboBox9.Text = data[EtazT, 4, PomeshenieT];
                    comboBox10.Text = data[EtazT, 5, PomeshenieT];
                    comboBox11.Text = data[EtazT, 6, PomeshenieT];
                    comboBox12.Text = data[EtazT, 7, PomeshenieT];
                    comboBox13.Text = data[EtazT, 8, PomeshenieT];
                    comboBox14.Text = data[EtazT, 9, PomeshenieT];
                    comboBox15.Text = data[EtazT, 10, PomeshenieT];
                    textBox4.Text = data[EtazT, 11, PomeshenieT];
                    comboBox16.Text = data[EtazT, 12, PomeshenieT];
                    comboBox17.Text = data[EtazT, 13, PomeshenieT];
                    textBox5.Text = data[EtazT, 14, PomeshenieT];
                    if (data[EtazT, 15, PomeshenieT] != "1") checkBox1.Checked = true;
                    else checkBox1.Checked = false;
                    comboBox18.Text = data[EtazT, 15, PomeshenieT];
                    textBox6.Text = data[EtazT, 16, PomeshenieT];
                    textBox7.Text = data[EtazT, 17, PomeshenieT];
                    textBox8.Text = data[EtazT, 18, PomeshenieT];
                    textBox9.Text = data[EtazT, 19, PomeshenieT];
                    if (data[EtazT, 20, PomeshenieT] != null) dateTimePicker3.Value = DateTime.Parse(data[EtazT, 20, PomeshenieT]);
                    textBox12.Text = data[EtazT, 21, PomeshenieT];
                    textBox14.Text = data[EtazT, 22, PomeshenieT];
                    textBox15.Text = data[EtazT, 23, PomeshenieT];
                    textBox16.Text = data[EtazT, 24, PomeshenieT];
                    if (data[EtazT, 25, PomeshenieT] != null) dateTimePicker4.Value = DateTime.Parse(data[EtazT, 25, PomeshenieT]);
                    textBox13.Text = data[EtazT, 26, PomeshenieT];
                    if (data[EtazT, 27, PomeshenieT] != null) textBox19.Text = data[EtazT, 27, PomeshenieT];//кв.м.
                    if (data[EtazT, 28, PomeshenieT] != null) textBox22.Text = data[EtazT, 28, PomeshenieT];//Планировка
                    
                    if (data[EtazT, 29, PomeshenieT] != null) textBox23.Text = data[EtazT, 29, PomeshenieT];//Однолинейная схема
                    if (data[EtazT, 30, PomeshenieT] != null) textBox24.Text = data[EtazT, 30, PomeshenieT];//План электросети
                    if (data[EtazT, 31, PomeshenieT] != null) textBox25.Text = data[EtazT, 31, PomeshenieT];//План водоснабжения
                    if (arenda[0, EtazT, 0, PomeshenieT] != null) dateTimePicker1.Value = DateTime.Parse(arenda[0, EtazT, 0, PomeshenieT]);
                    if (arenda[0, EtazT, 7, PomeshenieT] != null) textBox26.Text = arenda[0, EtazT, 7, PomeshenieT];
                    comboBox1.Text = arenda[0, EtazT, 1, PomeshenieT];
                    comboBox2.Text = arenda[0, EtazT, 2, PomeshenieT];
                    comboBox3.Text = arenda[0, EtazT, 3, PomeshenieT];
                    comboBox4.Text = arenda[0, EtazT, 4, PomeshenieT];
                    textBox17.Text = arenda[0, EtazT, 5, PomeshenieT];
                    if (arenda[0, EtazT, 6, PomeshenieT] != null) richTextBox3.Text = arenda[0, EtazT, 6, PomeshenieT].Replace("&rn", "\n");
                    if (chetchiki[0, EtazT, 0, PomeshenieT] != null) dateTimePicker2.Value = DateTime.Parse(chetchiki[0, EtazT, 0, PomeshenieT]);
                    textBox10.Text = chetchiki[0, EtazT, 1, PomeshenieT];
                    textBox11.Text = chetchiki[0, EtazT, 2, PomeshenieT];
                }
                //найти индекс помещения. Если совпадений нет, то: kolvo[EtazT]++; PomeshenieT=kolvo[EtazT];
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
            for (int i = 0; i < max1; i++)
            {
                if (break1 == 6) break;
                else break1 = 0;
                if (arenda[0, EtazT, 1, i] != null) Arend1.Add(arenda[0, EtazT, 1, i]);
                else break1++;
                if (arenda[0, EtazT, 3, i] != null) Arend2.Add(arenda[0, EtazT, 3, i]);
                else break1++;
                if (data[EtazT, 0, i] != null) data1.Add(data[EtazT, 0, i]);
                else break1++;
                if (data[EtazT, 1, i] != null) if (data[EtazT, 0, i] == comboBox5.Text) data2.Add(data[EtazT, 1, i]);
                    else break1++;
                if (data[EtazT, 2, i] != null) data3.Add(data[EtazT, 2, i]);
                else break1++;
                if (data[EtazT, 3, i] != null) data4.Add(data[EtazT, 3, i]);
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

        private void comboBox5_TextChanged(object sender, EventArgs e)
        {

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
                if (arenda[0, EtazT, 1, i] == comboBox1.Text.Replace(";", ","))
                {
                    if (koord[EtazT, 0, i] != 0)
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
                    if (koord[EtazT, 2 * i1, massivA[i]] != 0)
                    {
                        OutP[i, i1].X = koord[EtazT, 2 * i1, massivA[i]];//0.2.4.6.8...38
                        OutP[i, i1].Y = koord[EtazT, 2 * i1 + 1, massivA[i]];//1.3.5.7.9...39
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
                        koord[etaz, j, schetchik] = int.Parse(s.Substring(0, s.IndexOf(";")));
                        s = s.Substring(s.IndexOf(";") + 1);
                    }
                    else
                    {
                        koord[etaz, j, schetchik] = int.Parse(s);
                        break;
                    }
                }*/
            // richTextBox1.Text += massivA[i].ToString() + "\r\n";
        }

        private void button24_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 200; i++)
            {
                if (koord[EtazT, 0, i] != 0)
                {
                    for (int i1 = 0; i1 < 40; i1++)
                    {
                        if (koord[EtazT, 2 * i1, i] != 0)
                        {
                            richTextBox1.Text += koord[EtazT, 2 * i1, i].ToString() + " " + koord[EtazT, 2 * i1 + 1, i] + "\r\n";
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
                    if (koord[EtazT, j, i] == 0) break;
                }
                j = j / 2;
                if (j > 0)
                {
                    Point[] figura = new Point[j + 1];
                    for (int i1 = 0; i1 < j; i1++)
                    {
                        figura[i1].X = koord[EtazT, 2 * i1, i];
                        figura[i1].Y = koord[EtazT, 2 * i1 + 1, i];
                    }
                    figura[j].X = koord[EtazT, 0, i];
                    figura[j].Y = koord[EtazT, 1, i];
                    g.DrawPolygon(new Pen(Color.Green, 4), figura);
                }
            }
            pictureBox1.Image.Dispose();
            pictureBox1.Image = bitmap;
            g.Dispose();
        }
        void Block()
        { 
        
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
                    kolvo[etaz] = int.Parse(File[i].Substring(8, File[i].Length - 8)) - 1;//количество помещений на этаже
                    //if (kolvo[etaz] > max1) max1 = kolvo[etaz];
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
                    for (int i = 0; i < max1; i++)
                    {
                        if (arenda[0, et, 1, i] != null)
                        {
                            if (arenda[0, et, 1, i] == comboBox1.Text.Replace(";", ","))
                            {
                                comboBox2.Text = arenda[0, et, 2, i];
                                comboBox3.Text = arenda[0, et, 3, i];
                                comboBox4.Text = arenda[0, et, 4, i];
                                textBox17.Text = arenda[0, et, 5, i];
                                if (arenda[0, et, 6, i]!=null) richTextBox3.Text = arenda[0, et, 6, i].Replace("&rn", "\n");

                              //  if (selectArenda)
                              //  {
                                    for (int p1 = 0; p1 < max1; p1++)//запишем в listbox все помещения этого арендатора
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
            for (int i = 0; i < max1; i++)
            {
                if (data[EtazT, 1, i] != null)
                {
                    if (data[EtazT, 0, i] == comboBox5.Text) data2.Add(data[EtazT, 1, i]);
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
                for (int pomesh = 0; pomesh < max1; pomesh++)
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
                for (int i = 0; i < max1; i++)//помещение
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
                    for (int i = 0; i < max1; i++)//помещение
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
                for (int i = 0; i < max1; i++)//помещение
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
                for (int i = 0; i < max1; i++)//помещение
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
                for (int i = 0; i < max1; i++)//помещение
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
                    for (int i = 0; i < max1; i++)//помещение
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
                for (int i = 0; i < max1; i++)//помещение
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
                    for (int i = 0; i < max1; i++)//помещение
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
                if (chetchiki[k, EtazR, 1, PomesR] != null)
                {
                    if (DateTime.Parse(chetchiki[k, EtazR, 0, PomesR]) > dataPred && DateTime.Parse(chetchiki[k, EtazR, 0, PomesR]) < dataTekus)
                    {
                        if (rezult != 0)
                        {
                            if (double.Parse(chetchiki[k, EtazR, 1, PomesR]) != 10000 || double.Parse(chetchiki[k, EtazR, 1, PomesR]) != 100000 || double.Parse(chetchiki[k, EtazR, 1, PomesR]) != 1000000 || double.Parse(chetchiki[k, EtazR, 1, PomesR]) != 10000000) rezult -= double.Parse(chetchiki[k, EtazR, 1, PomesR]) * int.Parse(chetchiki[k, EtazR, 4, PomesR]);
                            else sbros = double.Parse(chetchiki[k, EtazR, 1, PomesR]) * int.Parse(chetchiki[k, EtazR, 4, PomesR]);
                        }
                        else rezult = double.Parse(chetchiki[k, EtazR, 1, PomesR]) * int.Parse(chetchiki[k, EtazR, 4, PomesR]);//умножим на коэффициент
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
                    if (chetchiki[k, EtazR, 1, PomesR] != null)//если показания по ЭЭ существуют, то в лист запишем оригинальную дату (01 число расчетного месяца)
                    {
                        if (ToDateRaschet(DateTime.Parse(chetchiki[k, EtazR, 0, PomesR])).ToShortDateString() != DataS)
                        {
                            if (DateTime.Parse(chetchiki[k, EtazR, 0, PomesR]) >= dataMes.Date)//ошибка >= ?
                            {
                                DataS = ToDateRaschet(DateTime.Parse(chetchiki[k, EtazR, 0, PomesR])).ToShortDateString();
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
            double znachenie1 = 0;
            double znachenie2 = 0;
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
                if (chetchiki[k, EtazR, 1, PomesR] != null)
                {
                    if (DateTime.Parse(chetchiki[k, EtazR, 0, PomesR]) > dataPred1 && DateTime.Parse(chetchiki[k, EtazR, 0, PomesR]) <= dataPred2)
                    {
                        znachenie1 = double.Parse(chetchiki[k, EtazR, 1, PomesR]);//начальные показания
                        Nschet = chetchiki[k, EtazR, 3, PomesR];
                        if (chetchiki[k, EtazR, 6, PomesR] == null) chetchiki[k, EtazR, 6, PomesR] = "0";//вручную пропишем нулевой расход на начало периода в БД
                        else
                        {
                            if (chetchiki[k, EtazR, 6, PomesR]!="-") if (double.Parse(chetchiki[k, EtazR, 6, PomesR]) < 0) predRasxodMinus = double.Parse(chetchiki[k, EtazR, 6, PomesR]);
                        }
                    }
                    if (DateTime.Parse(chetchiki[k, EtazR, 0, PomesR]) > dataTekus1 && DateTime.Parse(chetchiki[k, EtazR, 0, PomesR]) <= dataTekus2)// && znachenie2==0)
                    {
                        znachenie2 = double.Parse(chetchiki[k, EtazR, 1, PomesR]);//конечные показания
                        koeff = int.Parse(chetchiki[k, EtazR, 4, PomesR]);
                        DataK = k;
                    }
                    if (DateTime.Parse(chetchiki[k, EtazR, 0, PomesR]) > dataPred2 && DateTime.Parse(chetchiki[k, EtazR, 0, PomesR]) < dataTekus2)
                    {//промежуточные показания (между основными) Проверим на замену счетчика и переход через ноль
                        chetchiki[k, EtazR, 6, PomesR] = "-";//запишем отсутствие расхода в БД
                        if (chetchiki[k, EtazR, 3, PomesR] != Nschet)
                        {//сменился номер счетчика 
                            znachenie1 = double.Parse(chetchiki[k, EtazR, 1, PomesR]);
                            Nschet = chetchiki[k, EtazR, 3, PomesR];
                            summa += rezult;//расход запишем к сумме
                            rezult = 0;
                        }
                        else
                        {//если счетчик не сменился, 
                            //rezult = (double.Parse(chetchiki[k, EtazR, 1, PomesR]) - znachenie1) * int.Parse(chetchiki[k, EtazR, 4, PomesR]);     //посчитаем расход на всякий случай
                            if (double.Parse(chetchiki[k, EtazR, 1, PomesR]) == 10000 || double.Parse(chetchiki[k, EtazR, 1, PomesR]) == 100000 || double.Parse(chetchiki[k, EtazR, 1, PomesR]) == 1000000 || double.Parse(chetchiki[k, EtazR, 1, PomesR]) == 10000000)
                            {//если произошел переход через ноль (показание кратно 10к, а следуюшее (если существует, меньше текущего)
                                if (k + 1 < 60) if (chetchiki[k + 1, EtazR, 1, PomesR] != null) if (double.Parse(chetchiki[k + 1, EtazR, 1, PomesR]) < double.Parse(chetchiki[k, EtazR, 1, PomesR]))
                                {
                                    rezult += double.Parse(chetchiki[k, EtazR, 1, PomesR]) * int.Parse(chetchiki[k, EtazR, 4, PomesR]);
                                    znachenie1 = double.Parse(chetchiki[k+1, EtazR, 1, PomesR]);
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
            if (znachenie2 == 0 && summa == 0)
            {
                //chetchiki[DataK1, EtazR, 6, PomesR] = "0";//начальное нулевое показание в БД
                return 0;//если k=1; или в этом месяце только одно начальное показание
            }
            summa += (znachenie2 - znachenie1)*koeff;
            chetchiki[DataK, EtazR, 6, PomesR] = Math.Round(summa, 1).ToString();//запишет в БД
            return Math.Round(summa, 1);
        }
        private void button35_Click(object sender, EventArgs e)//скорректировать ДБ (добавить электросчетчики)
        {            
            for (int et = 0; et < 4; et++)
            {
                for (int pomesh = 0; pomesh < max1; pomesh++)
                {
                    int MonthTemp = -1;
                    for (int k = 0; k < 60; k++)
                    {
                        if (chetchiki[k, et, 0, pomesh] != null)//если показания записаны (хотябы по воде?)
                        {
                            if (chetchiki[k, et, 3, pomesh] == null)
                            {
                                chetchiki[k, et, 3, pomesh] = data[et, 9, pomesh];//добавить номер счетчика
                                chetchiki[k, et, 4, pomesh] = data[et, 15, pomesh];//добавить расчетный коэффициент
                            }
                            if (chetchiki[k, et, 6, pomesh] == null)//если расход не посчитан
                            {
                                //только в том случае, если дата является расчетной, записывается расход
                                // в противном случае нужно записать что-то, чтобы было понятно, что расход указан в другой строке (м.б. дата?) и желательно не оставлять null
                                //если указан расход, значит он официально используется для отчета. Но может быть и отрицательный расход
                                //например, когда расчетный расход обогнал фактичесие показания, тогда отрицательный расход указывает
                                // на нулевой расход в отчете, и разница должна учитываться при пересчете показаний в следующем расчетном периоде! (добавить в Rasxod3)
                                if (DateTime.Parse(chetchiki[k, et, 0, pomesh]).Month != MonthTemp)
                                {
                                    MonthTemp = DateTime.Parse(chetchiki[k, et, 0, pomesh]).Month;
                                    Rasxod3(et, pomesh, DateTime.Parse(chetchiki[k, et, 0, pomesh]));
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
            dataRedact[1] = "electro";//переключимся на счетчик электроэнергии
            dataGridView1.Visible = true;
            listBox2.Visible = true;
            richTextBox2.Visible = false;
            for (int row = 0; row < chetchiki.GetLength(0); row++) //строки
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
                for (int pomesh = 0; pomesh < max1; pomesh++)
                {
                    if (data[et1,9,pomesh]!=null)
                    {
                        if (data[et1, 9, pomesh] != "расчет")
                        {
                            string sovpadenie = data[et1, 9, pomesh];
                            if (sovpadenie.IndexOf(textBox18.Text) > -1) listBox2.Items.Add(sovpadenie);
                            for (int k = 0; k < 60; k++)
                            {
                                if (chetchiki[k, et1, 0, pomesh] != null)
                                {
                                    if (chetchiki[k, et1, 3, pomesh] != sovpadenie)
                                    {
                                        sovpadenie = chetchiki[k, et1, 3, pomesh];
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
                        kontur(PomeshenieT);
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
            if (koord[EtazT, 0, pomesh1] != 0)
            {
                int i1 = 0;
                int i2 = 0;
                double[,] mass = new double[2, 20];
                for (; i1 < 40; i1++, i2++)//пройти по координатам
                {
                    if (koord[EtazT, i1, pomesh1] != 0)
                    {
                        mass[0, i2] = koord[EtazT, i1, pomesh1];
                        i1++;
                        mass[1, i2] = koord[EtazT, i1, pomesh1];
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

        Point InPolygon(Point centrZ,double[,] mass,int i2)//центр полигона, mass-координаты вершин, i2-длина массива
        {
            //находим центр, переходим на 0 по игреку, с нуля пускаем луч, он должен порезать фигуру. после последнего пересечения
            //делим отрезок между двумя пересеченными гранями пополам, это и есть точка внутри полигона.
            centrZ.Y = 0;
            Point PrePoint = centrZ;
            Point PostPoint = centrZ;
            bool otvetB = false;
            for (int i = 1; i <= i2; i++)
            {
                double y11 = (((double)centrZ.X - mass[0, i - 1]) / (mass[0, i] - mass[0, i - 1])) * (mass[1, i] - mass[1, i - 1]) + mass[1, i - 1];
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
                if ((double)centrZ.X < max && (double)centrZ.X >= min && y11 > centrZ.Y)//ограничиваем по иксу //трассировка вниз (игрек больше точки) >= - исправил наконец-то ошибку точки
                {
                    if (otvetB == false) otvetB = true;
                    else otvetB = false;
                    PrePoint = PostPoint;//предыдущая точка становится точкой бывшей текущей, а текущая точка определена существующим пересечением
                    PostPoint.Y = (int)y11;
                }
            }
            if (!otvetB)
            {
                //однако все еще остается вариант, когда центр помещения находится вне полигона и луч ни разу не пересек фигуру. 
                /* if (PrePoint != PostPoint)
                 {                 
                 }*/
                centrZ.Y = (int)((PostPoint.Y - PrePoint.Y) / 2);                
            }
            return centrZ;
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
                for (int pomes = 0; pomes < max1; pomes++)
                {
                    for (int k = 0; k < 60; k++)
                    {
                        if (chetchiki[k, et1, 0, pomes] != null) //если дата равна нулю, то дальше можно не искать.
                        {
                            if (chetchiki[k, et1, 3, pomes] == selItem)
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
            if (chetchiki[0, et, 1, pomes] != null)
            {
                for (int i = 59; i > -1; i--)
                {
                    if (chetchiki[i, et, 1, pomes] != null) dataGridView1.Rows.Add(DateTime.Parse(chetchiki[i, et, 0, pomes]), chetchiki[i, et, 1, pomes], chetchiki[i, et, 6, pomes], chetchiki[i, et, 4, pomes]);//Rasxod3(et, pomes, DateTime.Parse(chetchiki[i, et, 0, pomes])) - убрали параметр
                }
            }            
            dataGridView1.Rows[dataGridView1.Rows.Count - 1].Cells[3].Value = chetchiki[0, et, 4, pomes];//в конец добавим расчетный коэффициент (который был при предыдущих показаниях)
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
                    switch (outL2et_pom[0])
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
                    izmMassSCH[0] = DateTime.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()).ToShortDateString();
                }
                else 
                {
                    izmMassSCH[0] = DateTime.Parse(dataGridView1[0, e.RowIndex].Value.ToString()).ToShortDateString();//дата
                    izmMassSCH[1] = dataGridView1[1, e.RowIndex].Value.ToString();//показания ээ
                    izmMassSCH[3] = listBox2.SelectedItem.ToString();//номер сч.
                    izmMassSCH[4] = dataGridView1[3, e.RowIndex].Value.ToString();//коэффициент
                    if (dataRedact[0] == null || dataRedact[0] == "") dataRedact[0] = izmMassSCH[0];
                    //dataRedact
                   // WriteSchet(outL2et_pom[0], outL2et_pom[1], DateTime.Parse(dataGridView1[0, e.RowIndex].Value.ToString()), dataGridView1[1, e.RowIndex].Value.ToString(), "+", listBox2.SelectedItem.ToString(), dataGridView1[3, e.RowIndex].Value.ToString(), "+", "+");
                }
                timer3.Interval = 100;
                timer3.Enabled = true;
                
            }


            if (!dgCellEdit)
            {
                //если изменится дата, то она не должна присутствовать в этой таблице. Если присутствует, то переключиться на нее.
                if (dataGridView1[0, e.RowIndex].Value != null)
                {
                    for (int k = 0; k < dataGridView1.RowCount-2; k++)//-2 ошибка???
                    {
                        if (k != e.RowIndex && DateTime.Parse(dataGridView1[0, k].Value.ToString()).ToShortDateString() == DateTime.Parse(dataGridView1[0, e.RowIndex].Value.ToString()).ToShortDateString())
                        {
                            //chetchiki[k, outL2et_pom[0], 0, outL2et_pom[1]] = DateTime.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()).ToShortDateString();
                            //dataGridView1.Rows.Remove(dataGridView1.Rows[k]);
                            int i = e.RowIndex;

                            dataGridView1[0, k].Selected = true;
                            dgCellEdit = true;
                            dataGridView1.Rows.RemoveAt(i);

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
                dataRedact[0] = DateTime.Parse(dataGridView1[e.ColumnIndex, e.RowIndex].Value.ToString()).ToShortDateString();
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
                    if (chetchiki[k, outL2et_pom[0], 0, outL2et_pom[1]] != null)
                    {
                        if (row1 < dataGridView1.RowCount - 1)
                        {
                            row1++;
                            richTextBox1.Text +=
    chetchiki[k, outL2et_pom[0], 0, outL2et_pom[1]] + " " + k.ToString() + " " +
    DateTime.Parse(dataGridView1[0, row1].Value.ToString()).ToShortDateString() + " " + row1.ToString() + "\r\n";
                            if (chetchiki[k, outL2et_pom[0], 0, outL2et_pom[1]] != DateTime.Parse(dataGridView1[0, row1].Value.ToString()).ToShortDateString())
                            {
                                row1--;
                                richTextBox1.Text += chetchiki[k, outL2et_pom[0], 0, outL2et_pom[1]] + "\r\n";
                                //функция удаления строки массива
                                DelChE(outL2et_pom[0], outL2et_pom[1], k);
                                timer3.Interval = 100;
                                timer3.Enabled = true;
                                break;
                            }
                        }
                    }
                }
                SelectL2(listBox2.SelectedItem.ToString());
            }
        }
        void DelChE(int et1, int pomes1, int k)//удаление данных по ээ
        {
            if (chetchiki[k, et1, 0, pomes1] != null)
            {
                if (chetchiki[k, et1, 2, pomes1] != null)//показание водомера не пустое
                {//очистим данные по электросчетчику только, оставим дату и данные по водомерам
                    chetchiki[k, et1, 1, pomes1] = null;
                    chetchiki[k, et1, 3, pomes1] = null;
                    chetchiki[k, et1, 4, pomes1] = null;
                    chetchiki[k, et1, 6, pomes1] = null;
                }
                else //удаляем строку целиком
                {
                    for (; k < 59; k++)
                    {
                        if (chetchiki[k, et1, 0, pomes1] != null)
                        {
                            for (int i = 0; i < RMS; i++)
                            {
                                chetchiki[k, et1, i, pomes1] = chetchiki[k+1, et1, i, pomes1];
                            }
                        }
                        else break;//наверное нужно
                    }
                }
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
                    if (chetchiki[k, EtazT, 3, PomeshenieT] != null) comboBox14.Text = chetchiki[k, EtazT, 3, PomeshenieT];
                    if (chetchiki[k, EtazT, 4, PomeshenieT] != null) comboBox14.Text = chetchiki[k, EtazT, 4, PomeshenieT];
                }
                if (nomerV)
                {
                    if (chetchiki[k, EtazT, 5, PomeshenieT] != null) comboBox16.Text = chetchiki[k, EtazT, 5, PomeshenieT];
                }
                if (chetchiki[k, EtazT, 0, PomeshenieT] == dateTimePicker2.Value.ToShortDateString())
                {
                    
                    if (chetchiki[k, EtazT, 1, PomeshenieT] != null && chetchiki[k, EtazT, 3, PomeshenieT] != null && chetchiki[k, EtazT, 4, PomeshenieT] != null)
                    {
                        textBox10.Text = chetchiki[k, EtazT, 1, PomeshenieT];//показание электросчетчика 3.
                        comboBox14.Text = chetchiki[k, EtazT, 3, PomeshenieT];//номер электросчетчика 2.
                        comboBox18.Text = chetchiki[k, EtazT, 4, PomeshenieT];//коэфф. трансформации 3.
                    }
                    else
                    {
                        textBox10.Text = "";
                        comboBox14.Text = "";//вставить функцию (№счетчика)
                        comboBox18.Text = "";
                        nomerE = true;
                    }
                    if (chetchiki[k, EtazT, 2, PomeshenieT] != null && chetchiki[k, EtazT, 5, PomeshenieT] != null && chetchiki[k, EtazT, 7, PomeshenieT] != null)//добавить остальное, когда займемся водой
                    {
                        /*
                             chetchiki[k, EtazT, 8, PomeshenieT]//для воды счетчик-расчет-или счетчик/расчет в data? 3.
                             chetchiki[k, EtazT, 9, PomeshenieT]//для воды на технологич./хозпитнужды в data? 3.
                         */
                        textBox11.Text = chetchiki[k, EtazT, 2, PomeshenieT];//показание водомера        3.  
                        comboBox16.Text = chetchiki[k, EtazT, 5, PomeshenieT];//номер водомера 2.
                        comboBox4.Text = chetchiki[k, EtazT, 7, PomeshenieT];//для воды количество сотрудников 3.
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
                if (chetchiki[k, outL2et_pom[0], 0, outL2et_pom[1]] != null)
                {
                    for (int i = 0; i < RMS; i++) richTextBox1.Text += chetchiki[k, outL2et_pom[0], i, outL2et_pom[1]] + ";";
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
                for (int pomesh = 0; pomesh < max1; pomesh++)
                {
                    if (data[et, 0, pomesh] != "" && data[et, 1, pomesh] != "")
                    {
                        string temp = "";
                        for (int i = 0; i < 60; i++)
                        {

                            if (chetchiki[i, et, 0, pomesh] != null)
                            {
                                if (temp == chetchiki[i, et, 0, pomesh]) richTextBox1.Text += chetchiki[i, et, 3, pomesh] + "\r\n";
                                else temp = chetchiki[i, et, 0, pomesh];
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

        private void button50_Click(object sender, EventArgs e)
        {

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

        private void button51_Click(object sender, EventArgs e)
        {
            // Создаём экземпляр нашего приложения
            Excel1.Application excelApp = new Excel1.Application();
            // Создаём экземпляр рабочий книги Excel
            Excel1.Workbook workBook;
            // Создаём экземпляр листа Excel
            Excel1.Worksheet sheet=null;
            // Создаём экземпляр области ячеек Excel
            Excel1.Range range1 = null;
            workBook = excelApp.Workbooks.Add();
            sheet = (Excel1.Worksheet)workBook.Worksheets.get_Item(1);

            //Заполняем
            //покажем поьзователю отчет
            excelApp.Visible = true;
            excelApp.UserControl = false;

            //заголовок
            sheet.Cells.Font.Name = "ISOCPEUR";
            sheet.Cells.Font.Size = 12;
            sheet.Range[sheet.Cells[1, 1], sheet.Cells[1, 7]].Merge();
            sheet.Cells[1, 1].Font.Size = 24;
            sheet.Cells[1, 1].Font.Name = "Times New Roman";
            sheet.Cells.Font.Bold = true;
            sheet.Cells[1, 1] = "ООО «СКБ-Сбытсервис»";
            sheet.Cells[1, 1].HorizontalAlignment = Excel1.Constants.xlCenter;

            sheet.Cells[3, 1] = "350072, Краснодарский  край, г.Краснодар,";
            sheet.Cells[4, 1] = "Ул. Московская, 5.";
            sheet.Rows[4].RowHeight = 18;
            sheet.Cells[5, 1] = "Тел. 8(861)252-09-83";
            sheet.Rows[5].RowHeight = 18;
            sheet.Cells[7, 1] = "Потребитель: " + comboBox23.Text;//АО «ЭР-Телеком Холдинг»";//parse
            sheet.Cells[8, 1] = "Адрес объекта: г.Краснодар, ул. Московская, 5.";//parse?
            sheet.Rows[8].RowHeight = 22;
            sheet.Cells[10, 1] = "Расчет количества потребленной электроэнергии за " + periodMY(dateTimePicker5.Value, dateTimePicker6.Value);//parse
            sheet.Rows[10].RowHeight = 22;

            //таблица
            sheet.Columns[2].ColumnWidth = 22;
            sheet.Columns[3].ColumnWidth = 10;
            sheet.Columns[4].ColumnWidth = 12;
            sheet.Columns[5].ColumnWidth = 12;
            sheet.Columns[7].ColumnWidth = 9;
            sheet.Cells[11, 1] = "№";
            sheet.Cells[11, 2] = "№ точки учета по договору";
            sheet.Cells[11, 3] = "№ счетчика";
            sheet.Cells[11, 4] = "Показания на  01." + dateTimePicker5.Value.Month + "." + dateTimePicker5.Value.Year;//parse
            DateTime nextDate = dateTimePicker6.Value;
            nextDate= nextDate.AddMonths(1);
            sheet.Cells[11, 5] = "Показания на 01." + nextDate.Month + "." + nextDate.Year;//parse
            sheet.Cells[11, 6] = "Расч. Коэфф.";
            sheet.Cells[11, 7] = "Расход, кВт.ч";
            int k = 0;//0
            //циклом заполним таблицу
            List<string> Temp = new List<string>();
            Temp.AddRange(ToReport(comboBox23.Text, dateTimePicker5.Value, dateTimePicker6.Value).ToArray());
            if (Temp.Count > 0)
            {
                for (; k < (Temp.Count) / 6; k++)
                {
                    sheet.Cells[12 + k, 1] = (k + 1).ToString() + ".";
                    sheet.Cells[12 + k, 2] = Temp[k * 6];       //помещение   из data, остальное из chetchiki
                    sheet.Cells[12 + k, 3] = Temp[k * 6 + 1];   //№счетчика
                    sheet.Cells[12 + k, 4] = Temp[k * 6 + 2].Replace(',', '.');   //показания начало
                    sheet.Cells[12 + k, 5] = Temp[k * 6 + 3].Replace(',', '.');   //показания конец
                    sheet.Cells[12 + k, 6] = Temp[k * 6 + 4];   //расч. коэфф.                    
                    sheet.Cells[12 + k, 7] = Temp[k * 6 + 5].Replace(',', '.');   //расход
                    // sheet.Cells[12 + k, 7].NumberFormat = "0,0";//формат ячейки числовой
                }
            }
            //for (int i = 0; i < Temp.Count; i++) richTextBox1.Text += Temp[i] + "\r\n";//отладка
                sheet.Cells[12 + k, 2] = "Всего";

            Excel1.Range formulaRange = sheet.Range[sheet.Cells[12, 7], sheet.Cells[11 + k, 7]];
            string ToAdresEx = formulaRange.get_Address(1, 1, Excel1.XlReferenceStyle.xlR1C1, Type.Missing, Type.Missing);

            sheet.Cells[12 + k, 7].Formula = "=SUM(G12:G" + (11 + k).ToString() + ")";//формула (сумма)
            range1 = sheet.Range[sheet.Cells[11, 1], sheet.Cells[12 + k, 7]]; //выделяем всю таблицу
            range1.Cells.Font.Size = 10;
            range1.Cells.Font.Italic = true;
            range1.Cells.Font.Bold = false;
            range1.Cells.WrapText = true;
            range1.Borders.LineStyle = Excel1.XlLineStyle.xlContinuous; //границы выделенной области
            range1.Borders.Weight = Excel1.XlBorderWeight.xlMedium;

            sheet.Cells[12 + k, 2].Font.Bold = true;//всего жирное
            sheet.Cells[12 + k, 7].Font.Bold = true;//сумма жирная
            //подпись
            sheet.Cells[17 + k, 2] = "Главный энергетик";
            sheet.Cells[17 + k, 2].Font.Italic = true;
            sheet.Cells[17 + k, 5] = "Канавин А.А.";
            sheet.Cells[17 + k, 5].Font.Italic = true;
            sheet.Cells[19 + k, 4] = "М.П.";
            sheet.Cells[19 + k, 4].HorizontalAlignment = Excel1.Constants.xlRight;
            // Открываем созданный excel-файл
            //workBook.Application.DisplayAlerts = false;
            // workBook.SaveAs( "d:\\Parse.xlsx"); 
            //excelApp.Visible = true;
            //excelApp.UserControl = true;

            //выгружаем
            /*
            System.Runtime.InteropServices.Marshal.ReleaseComObject(range1);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
            workBook.Close(false, null, null);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(workBook);
            excelApp.Quit();
            System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
            GC.Collect();
            GC.WaitForPendingFinalizers();*/
        }

        List<string> ToReport(string arendator, DateTime DataOtMes, DateTime DataDoMes)// выведет построчно: корпус-помещение, №счетчика, показания на начало, показания на конец, расчетный коэфф., расчет.
        {
            List<string> ToOtchet = new List<string>();
            for (int et1=0; et1 < 4; et1++)
            {
                for (int pomesh = 0; pomesh < max1; pomesh++)
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
                            if (chetchiki[k, et1, 6, pomesh] != null)//расход ЭЭ имеет запись
                            {
                                if (chetchiki[k, et1, 6, pomesh] != "" && chetchiki[k, et1, 6, pomesh] != "-")
                                {
                                    if (!Period)
                                    {
                                        if (DateTime.Parse(chetchiki[k, et1, 0, pomesh]) > dataTekus1 && DateTime.Parse(chetchiki[k, et1, 0, pomesh]) < dataTekus2)
                                        {
                                            Period = true;
                                            rasxodZaPeriod += double.Parse(chetchiki[k, et1, 6, pomesh]);
                                            richTextBox1.Text += "1:"+rasxodZaPeriod.ToString() + "\r\n";//лог
                                            ToOtchet.Add(chetchiki[k, et1, 1, pomesh]);
                                            if (DataOtMes.Month == DataDoMes.Month) flag = false;//если период больше одного месяца и false - если период один месяц.
                                            else flag = true;
                                        }
                                    }
                                    else
                                    {
                                        if (DateTime.Parse(chetchiki[k, et1, 0, pomesh]) > dataPred1 && DateTime.Parse(chetchiki[k, et1, 0, pomesh]) < dataPred2)
                                        {
                                            ToOtchet.Insert(ToOtchet.Count - 1, chetchiki[k, et1, 1, pomesh]);
                                            ToOtchet.Add(chetchiki[k, et1, 4, pomesh]);
                                            break;
                                        }
                                        if (flag)
                                        {
                                            rasxodZaPeriod += double.Parse(chetchiki[k, et1, 6, pomesh]);
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

        string periodMY(DateTime dat1, DateTime dat2)
        {
            if (dat1 == dat2)
            {
                return MonthToStr(dat1.Month) + " " + dat1.Year.ToString()+"г.";
            }
            else
            {
                return MonthToStr(dat1.Month) + " " + dat1.Year.ToString()+"г. - "+MonthToStr(dat2.Month) + " " + dat2.Year.ToString()+"г.";
            }
        }

        string MonthToStr(int month1)
        { 
            string retMonth="";
            switch (month1)
            {
                case 1: retMonth = "январь";
                    break;
                case 2: retMonth = "февраль";
                    break;
                case 3: retMonth = "март";
                    break;
                case 4: retMonth = "апрель";
                    break;
                case 5: retMonth = "май";
                    break;
                case 6: retMonth = "июнь";
                    break;
                case 7: retMonth = "июль";
                    break;
                case 8: retMonth = "август";
                    break;
                case 9: retMonth = "сентябрь";
                    break;
                case 10: retMonth = "октябрь";
                    break;
                case 11: retMonth = "ноябрь";
                    break;
                case 12: retMonth = "декабрь";
                    break;
            }
            return retMonth;            
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
                for (int pomesh = 0; pomesh < max1; pomesh++)
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
            EtazT = 0;
            PomeshenieT = 0;
            //izmPomes(EtazT, PomeshenieT);
            izmMassSCH[0] = "";//"29.08.2020";//"29.07.2020";//textBox1.Text;//
            dataRedact[0] = "01.05.2020";
            dataRedact[1] = "electro";
            izmMassSCH[1] = "6069,1";
            //  izmMassSCH[3] = "014105";
        }

        private void button59_Click(object sender, EventArgs e)//вывести изменения на экран
        {
            File.Clear();
            File.Add((kolvo[0] + kolvo[1] + kolvo[2] + kolvo[3] + 4).ToString());//записали общее количество помещений в начало
            for (int etaz = 0; etaz < 4; etaz++)
            {
                File.Add("[etaz_" + (etaz + 1).ToString() + "]" + (kolvo[etaz] + 1).ToString());//запись номера этажа

                for (int pomeshenie = 0; pomeshenie <= kolvo[etaz]; pomeshenie++)
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
                        if (chetchiki[k, etaz, 0, pomeshenie] == null) break;
                        s = "";
                        for (int i = 0; i < RMS; i++)
                        {
                            s += chetchiki[k, etaz, i, pomeshenie] + ";";
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
            EtazT = 0;
            PomeshenieT = 0;
            //izmPomes(EtazT, PomeshenieT);
            izmMassA[0] = "";//"01.05.2020";
            dataModA = "22.04.2020";
            izmMassA[1] = null;//"ООО \"АбраКадабра\"";
            //  izmMassSCH[3] = "014105";
        }
        /*
    chetchiki[k, EtazT, 0, PomeshenieT] = dateTimePicker2.Value.ToShortDateString().Replace(";", ",");//дата съема показаний 1.
    chetchiki[k, EtazT, 1, PomeshenieT] = textBox10.Text.Replace(";", ",");//показание электросчетчика 3.
    chetchiki[k, EtazT, 2, PomeshenieT] = textBox11.Text.Replace(";", ",");//показание водомера        3.  
    chetchiki[k, EtazT, 3, PomeshenieT] = comboBox14.Text.Replace(";", ",");//номер электросчетчика 2.
    chetchiki[k, EtazT, 4, PomeshenieT] = comboBox18.Text.Replace(";", ",");//коэфф. трансформации 3.
    chetchiki[k, EtazT, 5, PomeshenieT] = comboBox16.Text.Replace(";", ",");//номер водомера 2.
* chetchiki[k, EtazT, 6, PomeshenieT]//расход ЭЭ (текущее минус предыдущее т.е. расход за предыдущий период) 4.
* chetchiki[k, EtazT, 7, PomeshenieT]//для воды количество сотрудников 3.
* chetchiki[k, EtazT, 8, PomeshenieT]//для воды счетчик-расчет-или счетчик/расчет в data? 3.
* chetchiki[k, EtazT, 9, PomeshenieT]//для воды на технологич./хозпитнужды в data? 3.
* chetchiki[k, EtazT, 10, PomeshenieT]//для воды расход 4.
* chetchiki[k, EtazT, 11, PomeshenieT]//резерв? приоритет.
* chetchiki[k, EtazT, 12, PomeshenieT]//резерв? приоритет.
*/
    }
}