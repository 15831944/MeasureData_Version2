using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace _0_ElvToCAD
{
    public class GetCADexePath
    {
        List<string> Paths = new List<string>();
        string TartetName;
        public GetCADexePath(string TartetName)
        {
            this.TartetName = TartetName;

            List<string> paths = new List<string>();

            foreach (DriveInfo dd in DriveInfo.GetDrives()) this.SearchFile(dd.Name, ref paths);

            this.Paths = paths;

        }


        public string FindCADProcess(string version)
        {
            List<string> res = new List<string>();
            foreach (string item in this.Paths)
            {
                if (item.Contains("x64")) continue;
                if (!item.Contains("AutoCAD")) continue;
                if (!item.Contains(version)) continue;

                res.Add(item);
            }

            return res.Count == 0 ? "" : res[0];
        }

        private void SearchFile(string sPath, ref List<string> paths)
        {
            string[] files = null;
            try
            {
                string[] Paths = Directory.GetDirectories(sPath);
                foreach (string item in Paths)
                {
                    try
                    {
                        files = Directory.GetFiles(item, this.TartetName, SearchOption.AllDirectories);
                        if (files.Count() == 0) files = null;

                    }
                    catch (Exception)
                    {
                    }

                    if (null != files) break;
                }
            }
            catch (Exception)
            {

            }

            if (null == files) return;

            foreach (string item in files) paths.Add(item);
        }


    }
}
