using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace _0_ElvToCAD
{
    public class _0_Main_Draw_DXF
    {
        LST_DATAs LST = null;
        OUT_CAD_DATA OUT_DATA = null;
        CSV_CAD_DATAs CSV_DATA = null;
        DXFGenerator DXF;
        string TopPath = "";
        string CASE = "";

        public _0_Main_Draw_DXF(string TopPath, string CASE = "OUT")
        {
            this.CASE = CASE;
            this.TopPath = TopPath;
            if (this.CASE == "OUT")
            {
                this.LoadingOUTandCSV(TopPath, out OUT_CAD_DATA OUT_DATA_, out CSV_CAD_DATAs CSV_DATA_);
                this.OUT_DATA = OUT_DATA_;
                this.CSV_DATA = CSV_DATA_;
            }
            else if (this.CASE == "LST")
            {
                this.LoadingLST(TopPath);
            }
        }

        public string Draw_OUT()
        {
            string ErrorMes = "";
            if (null == this.OUT_DATA && null == this.CSV_DATA) ErrorMes = "CSV與OUT檔案載入失敗\r\n";
            if (ErrorMes != "") return ErrorMes;
            if (null == this.OUT_DATA || !this.OUT_DATA.IsLoading) ErrorMes = "OUT檔案載入失敗\r\n";
            if (ErrorMes != "") return ErrorMes;
            if (null == this.CSV_DATA || !this.CSV_DATA.IsLoading) ErrorMes = "CSV檔案載入失敗\r\n";
            if (ErrorMes != "") return ErrorMes;


            ErrorMes = this.DrawDXF(this.OUT_DATA.DATAs, CSV_DATA.DATAs, OUT_DATA.DirectoryName);
            this.DXF.Save();
            if (ErrorMes == "") ErrorMes = "執行完成";

            return ErrorMes;
        }

        public string Draw_LST()
        {
            string ErrorMes = "";

            if (!this.LST.LoadingDone) return this.LST.ErrorMes;

            ErrorMes = this.LST.ErrorMes;

            /// 剔除重複繪製線段 

            ErrorMes += this.DrawDXF(TakeOffSameLine(this.LST.BFFF_DATAs), this.LST.Coordinates, this.LST.DirectoryName);
            return ErrorMes;
        }


        private List<BFFF> TakeOffSameLine(List<BFFF> Data)
        {
            List<BFFF> RES = new List<BFFF>();
            List<string> Rec = new List<string>();
            foreach (BFFF item in Data)
            {
                string t1 = item.BF;
                string t2 = item.FF;
                string ss = item.Station;
                string c1 = ss + t2;
                string c2 = ss + t1;
                string c3 = t1 + ss;
                string c4 = t2 + ss;
                if (!Rec.Contains(c1) &&
                    !Rec.Contains(c2) &&
                    !Rec.Contains(c3) &&
                    !Rec.Contains(c4))
                {
                    RES.Add(new BFFF(item.BF, item.Station,CASE = "LST"));
                    RES.Add(new BFFF(item.Station, item.FF, CASE = "LST"));
                    Rec.Add(c1);
                    Rec.Add(c2);
                    Rec.Add(c3);
                    Rec.Add(c4);
                     
                }
            }

            return RES;
        }


        private void LoadingOUTandCSV(string TopPath, out OUT_CAD_DATA OUT_DATA, out CSV_CAD_DATAs CSV_DATA)
        {
            OUT_DATA = null;
            CSV_DATA = null;
            string KEYWORD = "Reliability Analysis Information";
            bool IsExist = Directory.Exists(TopPath);
            if (!IsExist) return;

            List<string> out_files = Directory.GetFiles(TopPath).ToList();
            foreach (string item in out_files)
            {
                string fName = Path.GetFileName(item);
                if (fName.Contains(".out") || fName.Contains(".OUT"))
                {
                    OUT_DATA = new OUT_CAD_DATA(item, KEYWORD);
                }
                else if (fName.Contains(".csv") || fName.Contains(".CSV"))
                {
                    CSV_DATA = new CSV_CAD_DATAs(item);
                }
            }
        }



        private void LoadingLST(string TopPath)
        {
            List<string> datas = Directory.GetFiles(TopPath, "*.lst*").ToList();
            string path = datas.Count == 0 ? "" : datas[0];
            this.LST = new LST_DATAs(path, this.CASE, datas.Count != 0);
        }






        private string DrawDXF(List<BFFF> BFFF_Datas, Dictionary<string, Coordinate> COORs, string FileName)
        {
            this.DXF = new DXFGenerator(this.TopPath, FileName);
            string ErrorMes = "";
            int ii = 0;
            List<Coordinate> MarkStation = new List<Coordinate>();
            foreach (BFFF item in BFFF_Datas)
            {
                ii++;
                if (COORs.ContainsKey(item.BF) &&
                    COORs.ContainsKey(item.FF))
                {
                    this.DXF.AddLine(COORs[item.BF].E,
                                     COORs[item.BF].N,
                                     COORs[item.FF].E,
                                     COORs[item.FF].N,
                                    "Cyan");
                    MarkStation.Add(COORs[item.BF]);
                    MarkStation.Add(COORs[item.FF]);
                }
                else
                {
                    string noLocation1 = COORs.ContainsKey(item.BF) ? "" : item.BF;
                    string noLocation2 = COORs.ContainsKey(item.FF) ? "" : item.FF;
                    string noLocation = "無" + noLocation1 + " " + noLocation2;
                    ErrorMes += item.RowNumber + " : " +
                                item.BF + " --> " +
                                item.FF + " 繪製失敗 (" + noLocation + ")\r\n";
                }
            }

            List<string> MarkedStation = new List<string>();
            foreach (Coordinate item in MarkStation)
            {
                if (MarkedStation.Contains(item.Station)) continue;

                this.DXF.AddCircle(item.E, item.N, 1, "ByLayer");
                this.DXF.AddText(item.E + 1, item.N - 0.5, item.Station, "Blue");
                MarkedStation.Add(item.Station);
            }

            this.DXF.Save();

            return ErrorMes;
        }
    }
}
