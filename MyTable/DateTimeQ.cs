﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTable
{
    class DateTimeQ
    {
        public int Year { get; set; }
        private int quarter=0;
        public int Quarter
        {
            get
            {
                return Quarter;
            }
            set
            {
                if (value > 4 && value < 1)
                {
                    quarter = 0;
                }
                else
                {
                    quarter = value;
                }
            }
        }
        private int month = 0;
        public int Month 
        { 
            get 
            {
                return month;
            } 
            set 
            {
                if (value > 12 && value < 0)
                {
                    month = 0;
                }
                else
                {
                    month = value;
                }
            }
        }
        public DateTimeQ()
    : this("")
        {
        }
        public DateTimeQ(string Date)
        {
                ToDateQ(Date);
        }
        public new string ToString()
        {
            string s;
            if ((month != 0)&&(quarter == 0))
            {
                MonthToQuarter();                
            }
            if (Year == 0 || quarter == 0)
            {
                s= "";
            }
            else
            {
                s = QuarterToRome() + " " + Year.ToString();
            }
            return s;
        }

        public int MonthToQuarter()
        {
            if (month > 1 && month <= 3) quarter = 1;
            if (month > 3 && month <= 6) quarter = 2;
            if (month > 6 && month <= 9) quarter = 3;
            if (month > 9 && month <= 12) quarter = 4;
            return quarter;
        }
        public string QuarterToRome()
        {
            switch (quarter)
            {
                case 1:
                    return "I";
                case 2:
                    return "II";
                case 3:
                    return "III";
                case 4:
                    return "IV";
            }
            return null;
        }
        private void ToDateQ(string Date)
        {
            int k = 0;
            string s = "";
            if (Date.IndexOf(" ") > 0) k = Date.IndexOf(" ");
            if (Date.IndexOf("-") > 0) k = Date.IndexOf("-");
            if (k > 0)
            {
                s= Date.Substring(0, k);
                if (s == "I" || s == "1")
                {
                    quarter = 1;
                    month = 1;
                }
                if (s == "II" || s == "2")
                {
                    quarter = 2;
                    month = 4;
                }
                if (s == "III" || s == "3")
                {
                    quarter = 3;
                    month = 7;
                }
                if (s == "IV" || s == "4")
                {
                    quarter = 4;
                    month = 10;
                }
                s=s.Substring(k);
            }
            if (s != "")
            {
                Year = int.Parse(s);
            }
            else
            {
                Year = 0;
            }
        }
    }
}
