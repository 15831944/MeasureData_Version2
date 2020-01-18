using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _0_ElvToCAD
{
    public static class Function
    {
        public static class ReadDatas
        {

            public static List<BFFF> GetBFFF(string fPath,
                                                     string KEYWORD,
                                                     string TargetStr = "",
                                                     string CASE = "OUT")
            {
                List<BFFF> DATAs = new List<BFFF>();
                List<string> Datas = Function.ReadDatas.LoadingData(fPath, KEYWORD);
                foreach (string item in Datas)
                {
                    BFFF test = new BFFF(item, CASE);
                    if (!test.IsLoading) continue;

                    test.BF = test.BF.Replace(TargetStr, "");
                    test.FF = test.FF.Replace(TargetStr, "");
                    DATAs.Add(test);
                }

                return DATAs;
            }


            public static List<string> LoadingData(string path, string KEYWORD)
            {
                List<string> Datas = new List<string>();
                using (StreamReader sr =
                    new StreamReader(path, Encoding.Default, true))
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

            private static void ReadOUTData_Out_comp_Out(string file)
            {
                string KEY_WORDs = "Reliability Analysis Information";
                List<string> res = new List<string>();
                bool Open_1 = false;
                bool IsRead = false;
                using (StreamReader sr = new StreamReader(file))
                {
                    while (sr.Peek() != -1)
                    {
                        string Data = sr.ReadLine();
                        if (IsRead) res.Add(Data);
                        if (Data.Contains(KEY_WORDs)) Open_1 = true;
                        if (Open_1 && Data.Contains("==")) IsRead = true;
                        if (Data.Trim() == "") IsRead = false;
                    }

                }
            }
        }




    }
}
