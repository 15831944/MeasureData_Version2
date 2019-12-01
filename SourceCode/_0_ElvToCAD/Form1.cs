using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace _0_ElvToCAD
{
    public partial class Form1 : Form
    {
        GeneralAutoSavingFormDatasClass GASF; 
        string[] subTitle = new string[] { "out", "csv" };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.GASF = new GeneralAutoSavingFormDatasClass(this.Controls, "OUT_TO_CAD");
            GASF.Loading();
            this.txt_showMessage.Text = "";
            this.Text = this.txtPath.Text == "0" ?
                                             "Data Process" :
                                             "Data Process (" + this.txtPath.Text + ")";
        }

        private void 檔案ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();
            FBD.SelectedPath = this.txtPath.Text;
            if (FBD.ShowDialog() == DialogResult.OK)
            {
                this.txtPath.Text = FBD.SelectedPath;
                this.Text = "Data Process (" + this.txtPath.Text + ")";
                this.txt_showMessage.Text = "";
                this.GASF.Saving();
            }
        }

        private void oUTToCADToolStripMenuItem_Click(object sender, EventArgs e)
        {

            _0_Main_Draw_DXF DC = new _0_Main_Draw_DXF(this.txtPath.Text );
            this.txt_showMessage.Text = DC.Draw_OUT() + "程式執行結束"; 
        }


        private void lSTToDXFToolStripMenuItem_Click(object sender, EventArgs e)
        {


            _0_Main_Draw_DXF DC = new _0_Main_Draw_DXF(this.txtPath.Text,"LST"); 
            this.txt_showMessage.Text = DC.Draw_LST() + "程式執行結束";

        }


        private void oUTToCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Mes = "";
            List<string> out_files = Directory.GetFiles(this.txtPath.Text, "*.out*").ToList();
            foreach (string item in out_files)
            {
                OUT_CAD_DATA OUT_DATA = new OUT_CAD_DATA(item, "平 差 後 高 程 值", false);
                OUT_DATA.TransDataToCSV();
                Mes += OUT_DATA.FileName + "\r\n";
            }
            Mes += Mes == "" ? "無檔案" : "轉檔完成";
            this.txt_showMessage.Text = Mes;
        }
    }





}
