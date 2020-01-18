using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _0_ElvToCAD
{
    public class D_OUT_Compare
    {
        string res_Msg = "";
        string SaveFileName = "";
        Dictionary<string, List<BFFF>> DATAs;
        public D_OUT_Compare(string TopFolderPath, List<string> FilePaths)
        {
            this.res_Msg = "檔案比較順序 : ";
            this.DATAs = new Dictionary<string, List<BFFF>>();
            string KEY_WORD = "Reliability Analysis Information";
            foreach (string path in FilePaths)
            {
                List<BFFF> Datas = Function.ReadDatas.GetBFFF(path,
                                                              KEY_WORD,
                                                              "-");
                string fName = Path.GetFileName(Path.GetDirectoryName(path));
                DATAs[fName] = Datas;
                this.res_Msg = this.res_Msg + fName + " -> ";
            }
            this.res_Msg = this.res_Msg.Remove(this.res_Msg.Length - 3, 3);
            string sName = Path.GetFileName(TopFolderPath) + ".csv";
            this.SaveFileName = Path.Combine(TopFolderPath, sName);
        }

        private Dictionary<BFFF, List<BFFF>> Compare()
        {
            int LEN = this.DATAs.Keys.Count;
            List<string> KEYs = this.DATAs.Keys.ToList();
            Dictionary<BFFF, List<BFFF>> RESULTs = new Dictionary<BFFF, List<BFFF>>();
            for (int i = 0; i < LEN; i++)
            {
                List<BFFF> tmpData = this.DATAs[KEYs[i]];
                List<BFFF> KEYS = RESULTs.Keys.ToList();
                foreach (BFFF item in tmpData)
                {

                    item.ITER = i;
                    BFFF key = KEYS.Find(t => t.IsSameGroup(item, out int Reverse));
                    if (null == key)
                    {
                        RESULTs[item] = new List<BFFF>();
                    }
                    else
                    { 
                        List<BFFF> tmp2 = RESULTs[key];
                        tmp2.Add(item);
                        RESULTs[key] = tmp2;
                    }
                }
            }
            return RESULTs;
        }


        private Dictionary<string, string[]> Compare_ori()
        {
            int LEN = this.DATAs.Keys.Count;
            List<string> KEYs = this.DATAs.Keys.ToList();
            Dictionary<string, string[]> RES = new Dictionary<string, string[]>();
            for (int i = 0; i < LEN; i++)
            {
                List<BFFF> tmpData = this.DATAs[KEYs[i]];
                List<string> TmpCmp = new List<string>();
                foreach (BFFF item in tmpData)
                {
                    string Name = item.BF;
                    string[] tmp = RES.ContainsKey(Name) ? RES[Name] :
                                                           new string[LEN];
                    if (TmpCmp.Contains(item.BF))
                    {

                    }

                    tmp[i] = item.obs;
                    RES[Name] = tmp;
                    TmpCmp.Add(item.BF);

                }
            }
            return RES;
        }



        public string CompareAndSave()
        {
            using (StreamWriter sw = new StreamWriter(this.SaveFileName))
            {
                sw.AutoFlush = true;
                List<string> saveRes = new List<string>();
                Dictionary<BFFF, List<BFFF>> RES_ = this.Compare(); 
                List<string> RES = this.TransToCSVForm(RES_);
                foreach (string item in RES.OrderBy(t => t))
                {
                    string tmp = item;
                    sw.WriteLine(tmp);
                }
            }
            this.res_Msg = this.res_Msg + " \r\n處理完成";
            return this.res_Msg;

        }


        private List<string> TransToCSVForm(Dictionary<BFFF, List<BFFF>> RES)
        {
            Dictionary<string, string> Results = new Dictionary<string, string>();
            List<string> RESULTs = new List<string>();
            foreach (BFFF key in RES.Keys)
            {
                List<BFFF> valuse = RES[key];
                valuse.Add(key);
                int len = valuse.Max(t => t.ITER);
                string[] res = new string[len + 1];



                string NAME = "";
                if (valuse.Count == 1)
                {
                    BFFF item = valuse[0];
                    NAME = NAME == "" ? item.BF + " -> " + item.FF : NAME;
                    string rr = NAME + "," + item.obs + ",";
                    res[item.ITER] = rr;
                     
                }
                else
                {
                    BFFF BASE_BFFF = null;
                    for (int i = 0; i < valuse.Count; i++)
                    {
                        BFFF item = valuse[i];
                        BASE_BFFF = BASE_BFFF == null ? item : BASE_BFFF;
                        BASE_BFFF.IsSameGroup(item, out int Reverse);

                        NAME = NAME == "" ? item.BF + " -> " + item.FF : NAME;
                        string rr = NAME + "," + 
                                    (Convert.ToDouble(item.obs) * Reverse).ToString() + ",";
                         
                        res[item.ITER] = rr;
                        
                    }
                }
                 

                string output = "";
                foreach (string item in res)
                {
                    if (item != null && item.Contains("AA01"))
                    {

                    }


                    if (item == null)
                    {
                        output += " , ,";
                    }
                    else
                    {
                        output += item;
                    }

                }
                 
                Results[NAME] = output;
                RESULTs.Add(output);

            }

            return RESULTs;
        }





    }
}
