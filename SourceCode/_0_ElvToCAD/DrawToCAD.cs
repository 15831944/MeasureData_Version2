using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AutoCAD;
using System.Runtime.InteropServices;
using System.Diagnostics;


namespace _0_ElvToCAD
{
    public class DrawToCAD
    {

        AcadApplication acadApp = null;
        AcadModelSpace CAD_DrawModel;
        public bool IsCADOpening = false;
        public DrawToCAD(string version)
        { 
            GetCADexePath cadPath = new GetCADexePath("acad.exe");
            string CAD_PATH = cadPath.FindCADProcess(version);

            bool isStart = false;
            while (null == this.acadApp)
            {
                try
                {
                    this.acadApp = Marshal.GetActiveObject("AutoCAD.Application") as AcadApplication;
                    IsCADOpening = true;
                }
                catch (Exception)
                {
                    if (isStart) continue; 
                    if (CAD_PATH == "") break;

                    Process CAD = new Process();
                    CAD.StartInfo.FileName = CAD_PATH;
                    isStart = CAD.Start();
                }
            }

            if (null == this.acadApp) return;


            try
            {
                this.CAD_DrawModel = acadApp.ActiveDocument.Database.ModelSpace;
            }
            catch (Exception)
            {
                while (null == this.CAD_DrawModel)
                {
                    try
                    {
                        this.acadApp.WindowState = AcWindowState.acMin;
                        this.acadApp.Documents.Add("test");
                        this.CAD_DrawModel = acadApp.ActiveDocument.Database.ModelSpace;
                        Thread.Sleep(3000);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }



        public void ZoomExtents()
        {
            this.acadApp.ZoomExtents();
        }

        public void DrawLine(CSV_Coordinate C1, CSV_Coordinate C2)
        {
            C1.IsDraw = true;
            C2.IsDraw = true;
            double[] p1 = (new List<double> { C1.E, C1.N, 0 }).ToArray();
            double[] p2 = (new List<double> { C2.E, C2.N, 0 }).ToArray();
            AcadLine Line = this.CAD_DrawModel.AddLine(p1, p2);
            Line.color = ACAD_COLOR.acCyan;
        }

        public void DrawCricle(CSV_Coordinate C1)
        {
            if (!C1.IsDraw) return;

            double[] p1 = (new List<double> { C1.E, C1.N, 0 }).ToArray();
            this.CAD_DrawModel.AddCircle(p1, 1);
        }

        public void DrawText(CSV_Coordinate C1)
        {
            if (!C1.IsDraw) return;
            double[] p1 = (new List<double> { C1.E + 1, C1.N - 0.5, 0 }).ToArray();
            AcadText text = this.CAD_DrawModel.AddText(C1.Station, p1, 1);
            text.color = ACAD_COLOR.acBlue;
        }
    }
}
