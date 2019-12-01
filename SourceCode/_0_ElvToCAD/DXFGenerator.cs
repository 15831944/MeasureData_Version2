using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using netDxf;
using netDxf.Entities;
using netDxf.Header;

namespace _0_ElvToCAD
{
    public class DXFGenerator
    {
        DxfDocument DXF;
        Dictionary<string, AciColor> Color;
        string SavePath;
        string SaveFileName;
        public DXFGenerator(string SavePath_, string SaveFileName_)
        {
            this.SavePath = SavePath_;
            this.SaveFileName = SaveFileName_ + ".dxf";
            this.SaveFileName = SaveFileName_ + ".dxf";
            this.DXF = new DxfDocument();
            this.Color = new Dictionary<string, AciColor>()
            {
                {"ByLayer",AciColor.ByLayer },
                {"Blue",AciColor.Blue },
                {"Yellow",AciColor.Yellow },
                {"Red",AciColor.Red },
                {"Green",AciColor.Green },
                {"Cyan",AciColor.Cyan },
                {"Magenta",AciColor.Magenta },
            };
        }

        public void AddLine(double sx, double sy, double ex, double ey, string color = "")
        { 
            Line entity = new Line(new Vector3(sx, sy,0), new Vector3(ex, ey, 0));
            if (color != "") entity.Color = this.Color[color];
            this.DXF.AddEntity(entity);
        }

        public void AddCircle(double sx, double sy, double radius, string color = "")
        {
            Circle entity = new Circle(new Vector3(sx, sy, 0), radius);
            if (color != "") entity.Color = this.Color[color];
            this.DXF.AddEntity(entity);
        }

        public void AddText(double sx, double sy, string text, string color = "")
        {
            Text entity = new Text(text, new Vector3(sx, sy, 0),1);
            if (color != "") entity.Color = this.Color[color];
            this.DXF.AddEntity(entity);
        }

        public void Save()
        {

            this.DXF.Save(Path.Combine(this.SavePath, this.SaveFileName),DxfVersion.AutoCad2000);
        }


    }
}
