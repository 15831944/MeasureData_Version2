using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _0_ElvToCAD
{
    public class BFFF
    {
        public string RowNumber;
        public string BF;
        public string FF;
        public string weight;
        public string Dis;
        public string obs;
        public string v;
        public string adjobs;
        public string ri;
        public string std;
        public string mark;
        public bool IsLoading = false;
        public string Station = "";
        public BFFF(string data, string CASE = "OUT")
        {
            if (CASE == "OUT") OUTProcess(data);
            if (CASE == "LST") LSTProcess(data);
        }

        public BFFF(string BF, string FF, string CASE = "OUT")
        {
            this.BF = BF;
            this.FF = FF; 
        }

        private void LSTProcess(string data)
        {
            if (data.Contains("=") || data.Contains("項次")) return;

            List<string> RR = new List<string>();
            foreach (string item in data.Split(' ')) if (item.Trim() != "") RR.Add(item);
            if (RR.Count != 11) return;



            char[] ss = data.ToCharArray();
            this.RowNumber = CharToString(ss, 0, 4, true);
            this.BF = CharToString(ss, 5, 13, true);
            this.FF = CharToString(ss, 23, 31, true);
            this.Station = CharToString(ss, 14, 22, true);

            if ((this.RowNumber != "" && this.BF != "") ||
                (this.RowNumber != "" && this.FF != "") )
            {
                IsLoading = true;
            }

            if (this.Station == "") IsLoading = false; 
        }

        private string CharToString(char[] ss, int st, int en, bool IsTrim)
        {
            string res = "";
            for (int i = st; i <= en; i++) res += ss[i];
            if (IsTrim) res = res.Trim();
            return res;
        }

        private void OUTProcess(string data)
        {
            List<string> dd = new List<string>();
            foreach (string item in data.Split(' '))
            {
                string tmp = item.Trim();
                if (tmp == string.Empty) continue;
                if (tmp.ToArray().GetValue(0).Equals('.')) tmp = "0" + tmp;
                if (tmp.Contains("-.")) tmp = tmp.Replace("-.", "-0.");
                dd.Add(tmp);
            }

            if (dd.Count == 11)
            {
                this.RowNumber = dd[0];
                this.BF = dd[1];
                this.FF = dd[2];
                this.weight = dd[3];
                this.Dis = dd[4];
                this.obs = dd[5];
                this.v = dd[6];
                this.adjobs = dd[7];
                this.ri = dd[8];
                this.std = dd[9];
                this.mark = dd[10];
                this.IsLoading = true;

            }
        }
    }
}
