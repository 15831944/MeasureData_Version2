using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _0_ElvToCAD
{
    public class Coordinate
    {
        public string Station = "";
        public double N;
        public double E;
        public bool IsDraw = false;
        public bool IsLoading = false;
        public Coordinate(string data, string CASE = "OUT")
        {
            try
            { 
                if (CASE == "OUT")
                {
                    string[] dd = data.Split(',');
                    Station = dd[0];
                    N = Convert.ToDouble(dd[1]);
                    E = Convert.ToDouble(dd[2]);
                    this.IsLoading = true;
                }
                else if (CASE == "LST")
                {
                    string[] dd = data.Split(' ');
                    List<string> dd2 = new List<string>();
                    foreach (string item in dd) if (item.Trim() != "") dd2.Add(item);
                    this.Station = dd2[2];
                    this.N = Convert.ToDouble(dd2[3]);
                    this.E = Convert.ToDouble(dd2[4]);
                    this.IsLoading = true; 
                }
            }
            catch (Exception)
            { 
            }
        }

    }
}
