using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.IO;
using AutoCAD;

/// Total File : .out .csv
/// File CSV : Coordinate data
/// File OUT : The Releation Between BF and FF
/// Target : Draw the elavation net in CAD

namespace _0_ElvToCAD
{
    public class _0_Main_Draw
    {
        OUT_CAD_DATAs OUT_DATA = null;
        CSV_CAD_DATAs CSV_DATA = null;
        DrawToCAD DrawCAD = null;
        string version;
        public _0_Main_Draw(string TopPath, string version_)
        {
            this.Loading(TopPath, out OUT_CAD_DATAs OUT_DATA_, out CSV_CAD_DATAs CSV_DATA_);
            this.OUT_DATA = OUT_DATA_;
            this.CSV_DATA = CSV_DATA_;
            this.version = version_;
        }


        public void Draw(out string ErrorMes)
        {
            ErrorMes = "";

            if (null == this.OUT_DATA || !this.OUT_DATA.IsLoading)
            {
                ErrorMes = "OUT檔案載入失敗\r\n";
                return;
            }

            if (null == this.CSV_DATA || !this.CSV_DATA.IsLoading)
            {
                ErrorMes = "CSV檔案載入失敗\r\n";
                return;
            }

            this.DrawCAD = new DrawToCAD(this.version);

            if (!this.DrawCAD.IsCADOpening)
            {
                ErrorMes = "找不到AutoCAD" + this.version + "\r\n";
                return;
            }

            foreach (OUT_DATAFormat item in this.OUT_DATA.DATAs)
            {
                if (CSV_DATA.DATAs.ContainsKey(item.BF) &&
                    CSV_DATA.DATAs.ContainsKey(item.FF))
                {
                    this.DrawCAD.DrawLine(
                        CSV_DATA.DATAs[item.BF],
                        CSV_DATA.DATAs[item.FF]);
                }
                else
                {
                    string noLocation1 = CSV_DATA.DATAs.ContainsKey(item.BF) ? "" : item.BF;
                    string noLocation2 = CSV_DATA.DATAs.ContainsKey(item.FF) ? "" : item.FF;
                    string noLocation = "無" + noLocation1 + " " + noLocation2;
                    ErrorMes += item.RowNumber + " : " +
                                item.BF + " --> " +
                                item.FF + " 繪製失敗 (" + noLocation + ")\r\n";
                }
            }

            foreach (CSV_Coordinate item in this.CSV_DATA.DATAs.Values)
            {
                this.DrawCAD.DrawCricle(item);
                this.DrawCAD.DrawText(item);
            }

            if (ErrorMes == "") ErrorMes = "執行完成";
            this.DrawCAD.ZoomExtents();
        }


        private void Loading(string TopPath, out OUT_CAD_DATAs OUT_DATA, out CSV_CAD_DATAs CSV_DATA)
        {
            OUT_DATA = null;
            CSV_DATA = null;
            string KEYWORD = "Reliability Analysis Information";
            List<string> out_files = Directory.GetFiles(TopPath).ToList();
            foreach (string item in out_files)
            {
                string fName = Path.GetFileName(item);
                if (fName.Contains(".out") || fName.Contains(".OUT"))
                {
                    OUT_DATA = new OUT_CAD_DATAs(item, KEYWORD);
                }
                else if (fName.Contains(".csv") || fName.Contains(".CSV"))
                {
                    CSV_DATA = new CSV_CAD_DATAs(item);
                }
            }
        }


    }
}
