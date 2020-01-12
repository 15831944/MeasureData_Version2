using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _0_ElvToCAD
{
    public class OUT_CAD_DATA
    {

        public List<BFFF> DATAs;
        public bool IsLoading = false;
        public List<string> Datas = new List<string>();
        public string OUT_File_Path = "";
        public string FileName;
        public string DirectoryName;

        public OUT_CAD_DATA(string path, string KEYWORD, bool IsInObject = true)
        {
            this.OUT_File_Path = path;
            this.FileName = Path.GetFileNameWithoutExtension(path);
            this.DirectoryName = Path.GetFileNameWithoutExtension(Path.GetDirectoryName(path));
            this.DATAs = new List<BFFF>();

            this.Datas = Function.ReadDatas.LoadingData(path, KEYWORD);

            if (IsInObject)
            {
                foreach (string item in this.Datas)
                {
                    BFFF test = new BFFF(item);
                    if (test.IsLoading) DATAs.Add(test);
                }
                this.IsLoading = true;
            }
        }


        public void TransDataToCSV()
        {
            string RES = "項次,點名,高程(M),標準誤差(CM)\r\nNo,Name,Elevation(m),Std. (cm)\r\n";
            for (int i = 4; i < this.Datas.Count; i++)
            {
                string[] tmp = this.Datas[i].Split(' ');
                foreach (string sp in tmp)
                {
                    if (sp.Trim() == "") continue;
                    RES += sp + ",";
                }
                RES += "\r\n";
            }

            string topPath = Path.Combine(Path.GetDirectoryName(this.OUT_File_Path), "TransToCSV");
            string csvName = Path.GetFileNameWithoutExtension(this.OUT_File_Path);
            if (!Directory.Exists(topPath)) Directory.CreateDirectory(topPath);

            using (StreamWriter sw = new StreamWriter(Path.Combine(topPath, csvName + ".csv"), false, Encoding.Default))
            {
                sw.WriteLine(RES);
            }
        }

        private List<string> LoadingData(string path, string KEYWORD)
        {
            List<string> Datas = new List<string>();
            using (StreamReader sr = new StreamReader(path, Encoding.Default, true))
            {
                bool IsReading = false;
                while (sr.Peek() != -1)
                {
                    string data = sr.ReadLine();
                    if (data.Contains(KEYWORD)) IsReading = true;
                    if (IsReading)
                    {
                        if (data.Trim() == string.Empty) break;
                        Datas.Add(data);
                    }
                }
            }
            return Datas;
        }


    }

}
