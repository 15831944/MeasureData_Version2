using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _0_ElvToCAD
{
    public class OUT_CSVDATAFormate
    {

        public string Number;
        public string Name;
        public string Elevation;
        public string std;
        public OUT_CSVDATAFormate(string data)
        {
            List<string> dd = dataProcessing(data);
            if (dd.Count == 4)
            {
                this.Number = dd[0];
                this.Name = dd[1];
                this.Elevation = dd[2];
                this.std = dd[3];

            }
        }

        private List<string> dataProcessing(string data)
        {
            List<string> res = new List<string>();
            foreach (string item in data.Split(' '))
            {
                string tmp = item.Trim();
                if (tmp == string.Empty) continue;
                if (tmp.ToArray().GetValue(0).Equals('.')) tmp = "0" + tmp;
                if (tmp.Contains("-.")) tmp = tmp.Replace("-.", "-0.");
                res.Add(tmp);
            }

            return res;
        }

    }
}
