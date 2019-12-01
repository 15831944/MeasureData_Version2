using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using static System.Windows.Forms.Control;

namespace _0_ElvToCAD
{
    public class GeneralAutoSavingFormDatasClass
    {
        private string SaveName = "";
        private ControlCollection Control;
        private List<Control> Controls = new List<Control>();
        private List<Panel> Panels = new List<Panel>();
        private List<GroupBox> GroupBoxes = new List<GroupBox>();
        private List<FormGeneralSavingAndLoading> FGSAL = new List<FormGeneralSavingAndLoading>();

        public GeneralAutoSavingFormDatasClass(ControlCollection controls, string SaveName_)
        {
            this.Control = controls;
            for (int i = controls.Count - 1; i >= 0; i--) this.Controls.Add(controls[i]);

            this.SaveName = SaveName_;
            foreach (Control item in this.Controls)
            {
                if (item is Panel)
                {
                    Panels.Add(item as Panel);
                }
                else if (item is GroupBox)
                {
                    GroupBoxes.Add(item as GroupBox);
                }
            }

            FGSAL.Add(new FormGeneralSavingAndLoading(this.Control, SaveName_));
            foreach (Panel item in Panels)
            {
                string saveName2 = this.SaveName.Split(',')[0] + item.Name + "_Panel.txt";
                FGSAL.Add(new FormGeneralSavingAndLoading(item.Controls, saveName2));
            }

            foreach (GroupBox item in GroupBoxes)
            {
                string saveName2 = this.SaveName.Split(',')[0] + item.Name + "GroupBox.txt";
                FGSAL.Add(new FormGeneralSavingAndLoading(item.Controls, saveName2));
            }
        }


        public void Loading()
        {
            foreach (FormGeneralSavingAndLoading item in FGSAL)
            {
                item.Loading();
            }
        }

        public void Saving()
        {
            foreach (FormGeneralSavingAndLoading item in FGSAL)
            {
                item.Saving();
            }
        }


    }





    public class FormGeneralSavingAndLoading
    {
        private ControlCollection Control;
        private List<TextBox> TextBoxs = new List<TextBox>();
        private List<RadioButton> RadioButtons = new List<System.Windows.Forms.RadioButton>();
        private List<CheckBox> CheckBoxs = new List<System.Windows.Forms.CheckBox>();
        private List<ListBox> ListBoxs = new List<System.Windows.Forms.ListBox>();
        private List<ComboBox> ComboBoxs = new List<System.Windows.Forms.ComboBox>();
        private List<Control> Controls = new List<Control>();
        private string SaveName = "";
        private string AppPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        public FormGeneralSavingAndLoading(ControlCollection controls, string SaveName_)
        {
            this.Control = controls;
            for (int i = controls.Count - 1; i >= 0; i--) this.Controls.Add(controls[i]);

            this.SaveName = SaveName_;
            foreach (Control item in this.Controls)
            {
                if (item is TextBox)
                {
                    TextBox item2 = item as TextBox;
                    item2.Text = "0";
                    this.TextBoxs.Add(item2);
                }
                else if (item is RadioButton)
                {
                    RadioButton item2 = item as RadioButton;
                    this.RadioButtons.Add(item2);
                }
                else if (item is CheckBox)
                {
                    CheckBox item2 = item as CheckBox;
                    this.CheckBoxs.Add(item2); 
                }
                else if (item is ListBox)
                { 
                    ListBox item2 = item as ListBox;
                    this.ListBoxs.Add(item2);

                }
                else if (item is ComboBox)
                {
                    ComboBox item2 = item as ComboBox;
                    this.ComboBoxs.Add(item2);
                }
            }
        }


        public void Saving()
        {
            using (StreamWriter sw = new StreamWriter(Path.Combine(this.AppPath, this.SaveName + "_TextBox_.txt")))
            {
                foreach (TextBox item in this.TextBoxs)
                {
                    sw.WriteLine(item.Text);
                    sw.Flush();
                }
            }

            using (StreamWriter sw = new StreamWriter(Path.Combine(this.AppPath, this.SaveName + "_RadioButton_.txt")))
            {
                foreach (RadioButton item in this.RadioButtons)
                {
                    sw.WriteLine(item.Checked);
                    sw.Flush();
                }
            }

            using (StreamWriter sw = new StreamWriter(Path.Combine(this.AppPath, this.SaveName + "_CheckBox_.txt")))
            {
                foreach (CheckBox item in this.CheckBoxs)
                {
                    sw.WriteLine(item.Checked);
                    sw.Flush();
                }
            }


            using (StreamWriter sw = new StreamWriter(Path.Combine(this.AppPath, this.SaveName + "_ComboBox_.txt")))
            {
                foreach (ComboBox item in this.ComboBoxs)
                {
                    sw.WriteLine(item.SelectedIndex);
                    sw.Flush();
                }
            }


            for (int i = 0; i < this.ListBoxs.Count; i++)
            {
                using (StreamWriter sw = new StreamWriter(Path.Combine(this.AppPath, this.SaveName + "_ListBox_" + i.ToString() + ".txt")))
                {
                    foreach (var item in this.ListBoxs[i].Items)
                    {
                        sw.WriteLine(item.ToString());
                        sw.Flush();
                    }
                }
            }

        }

        public void Loading()
        {
            try
            {

                using (StreamReader sr = new StreamReader(Path.Combine(this.AppPath, this.SaveName + "_TextBox_.txt")))
                {
                    int kk = 0;
                    while (sr.Peek() != -1)
                    {
                        this.TextBoxs[kk].Text = sr.ReadLine();
                        kk++;
                    }
                }
            }
            catch (Exception)
            {
            }

            try
            {

                using (StreamReader sr = new StreamReader(Path.Combine(this.AppPath, this.SaveName + "_RadioButton_.txt")))
                {
                    int kk = 0;
                    while (sr.Peek() != -1)
                    {
                        string tmp = sr.ReadLine();
                        if (tmp == "True")
                        {
                            this.RadioButtons[kk].Checked = true;
                        }
                        kk++;
                    }
                }
            }
            catch (Exception)
            {

            }

            try
            {

                using (StreamReader sr = new StreamReader(Path.Combine(this.AppPath, this.SaveName + "_CheckBox_.txt")))
                {
                    int kk = 0;
                    while (sr.Peek() != -1)
                    {
                        string tmp = sr.ReadLine();
                        if (tmp == "True")
                        {
                            this.CheckBoxs[kk].Checked = true;
                        }
                        kk++;
                    }
                }
            }
            catch (Exception)
            {
            }


            try
            {
                using (StreamReader sr = new StreamReader(Path.Combine(this.AppPath, this.SaveName + "_ComboBox_.txt")))
                {

                    int kk = 0;
                    while (sr.Peek() != -1)
                    {
                        string tmp = sr.ReadLine();
                        this.ComboBoxs[kk].SelectedIndex = Convert.ToInt32(tmp);
                        kk++;
                    }
                }
            }
            catch (Exception)
            {

            }

        }



    }
}
