using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace _0_ElvToCAD
{
    public class CSV_CAD_DATAs
    {
        public Dictionary<string, Coordinate> DATAs;
        public bool IsLoading = false;
        public CSV_CAD_DATAs(string path)
        {
            try
            {
                this.DATAs = new Dictionary<string, Coordinate>();
                using (StreamReader sr = new StreamReader(path))
                {
                    while (sr.Peek() != -1)
                    {
                        string data = sr.ReadLine();
                        Coordinate CC = new Coordinate(data);
                        this.DATAs[CC.Station] = CC;
                    }
                }
                this.IsLoading = true;
            }
            catch (Exception)
            {
            }
        }


    }
}
