using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _0_ElvToCAD
{
    public class LST_DATAs
    {
        public Dictionary<string, Coordinate> Coordinates;
        public List<BFFF> BFFF_DATAs = new List<BFFF>();
        public string DirectoryName = "";
        public string ErrorMes = "";
        public bool LoadingDone = false;
        public string CASE = "";
        public LST_DATAs(string path, string CASE, bool isLoading = true)
        {
            if (!isLoading)
            {
                ErrorMes = "無LST檔案 \r\n";
                return;
            }
            this.CASE = CASE;

            this.DirectoryName = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(path));
            LoadData(path);

            this.LoadingDone = true;
            this.ErrorMes = this.ErrorMes == "" ? "" : "前後視資料\r\n" + this.ErrorMes + "  列資料有誤\r\n\r\n";
        }



        private void LoadData(string path)
        {
            string th1 = "項次";
            string th2 = "編號";
            bool Open1 = false;
            bool Open2 = false;
            this.Coordinates = new Dictionary<string, Coordinate>();
            this.BFFF_DATAs = new List<BFFF>();
            using (StreamReader sr = new StreamReader(path, Encoding.Default, true))
            {
                while (sr.Peek() != -1)
                {
                    string data = sr.ReadLine();
                    if (data.Contains(th1)) Open1 = true;
                    if (data.Contains(th2)) Open2 = true;
                    if (Open1)
                    {
                        BFFF bf = new BFFF(data, this.CASE);
                        if (bf.IsLoading)
                        {
                            this.BFFF_DATAs.Add(bf);
                        }
                        else
                        {
                            this.ErrorMes += bf.RowNumber + "\r\n";
                        }
                    }


                    if (Open2)
                    {
                        Coordinate coor = new Coordinate(data, this.CASE);
                        if (coor.IsLoading) this.Coordinates[coor.Station] = coor;
                    }
                    if (data.Trim() == "")
                    {
                        Open1 = false;
                        Open2 = false;
                    }
                }
            }

            this.ErrorMes = this.ErrorMes.Trim();
        }





    }
}
