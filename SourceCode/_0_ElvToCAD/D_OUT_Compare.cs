using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _0_ElvToCAD
{
    public class D_OUT_Compare
    {
        string SaveFileName = "";
        Dictionary<string, List<BFFF>> DATAs;
        public D_OUT_Compare(string TopFolderPath, List<string> FilePaths)
        {
            this.DATAs = new Dictionary<string, List<BFFF>>();
            string KEY_WORD = "Reliability Analysis Information";
            foreach (string path in FilePaths)
            {
                List<BFFF> Datas = Function.ReadDatas.GetBFFF(path,
                                                              KEY_WORD,
                                                              "-");
                string fName = Path.GetFileNameWithoutExtension(path);
                DATAs[fName] = Datas;
            }

            string sName = Path.GetFileName(TopFolderPath) + ".csv";
            this.SaveFileName = Path.Combine(TopFolderPath, sName);
        }

        private Dictionary<string, string[]> Compare()
        {
            int LEN = this.DATAs.Keys.Count;
            List<string> KEYs = this.DATAs.Keys.ToList();
            Dictionary<string, string[]> RES = new Dictionary<string, string[]>();
            for (int i = 0; i < LEN; i++)
            {
                List<BFFF> tmpData = this.DATAs[KEYs[i]];
                foreach (BFFF item in tmpData)
                {
                    string Name = item.Station;                   
                    string[] tmp = RES.ContainsKey(Name) ? RES[Name] : 
                                                           new string[LEN];
                    tmp[i] = item.obs; //// Will Change Target
                    RES[Name] = tmp;
                }
            } 
            return RES;
        }





        private void CompareAndSave()
        {
            using (StreamWriter sw = new StreamWriter(this.SaveFileName))
            {
                sw.AutoFlush = true;
                List<string> saveRes = new List<string>();
                Dictionary<string, string[]> RES = this.Compare();
                foreach (string item in RES.Keys.OrderBy(t => t))
                {
                    string[] tmp = RES[item];
                    string saveStr = item + ",";
                    foreach (string tt in tmp) saveStr += tt + ",";
                    sw.WriteLine(saveStr);
                }
            }

        }






    }
}
