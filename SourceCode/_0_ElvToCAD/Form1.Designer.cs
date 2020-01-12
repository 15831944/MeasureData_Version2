namespace _0_ElvToCAD
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置 Managed 資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.檔案ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.執行ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oUTToCADToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oUTToCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lSTToDXFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.txt_showMessage = new System.Windows.Forms.TextBox();
            this.oUT比較ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("微軟正黑體", 14F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.檔案ToolStripMenuItem,
            this.執行ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(826, 32);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 檔案ToolStripMenuItem
            // 
            this.檔案ToolStripMenuItem.Name = "檔案ToolStripMenuItem";
            this.檔案ToolStripMenuItem.Size = new System.Drawing.Size(60, 28);
            this.檔案ToolStripMenuItem.Text = "檔案";
            this.檔案ToolStripMenuItem.Click += new System.EventHandler(this.檔案ToolStripMenuItem_Click);
            // 
            // 執行ToolStripMenuItem
            // 
            this.執行ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.oUTToCADToolStripMenuItem,
            this.oUTToCSVToolStripMenuItem,
            this.lSTToDXFToolStripMenuItem,
            this.oUT比較ToolStripMenuItem});
            this.執行ToolStripMenuItem.Name = "執行ToolStripMenuItem";
            this.執行ToolStripMenuItem.Size = new System.Drawing.Size(60, 28);
            this.執行ToolStripMenuItem.Text = "執行";
            // 
            // oUTToCADToolStripMenuItem
            // 
            this.oUTToCADToolStripMenuItem.Name = "oUTToCADToolStripMenuItem";
            this.oUTToCADToolStripMenuItem.Size = new System.Drawing.Size(186, 28);
            this.oUTToCADToolStripMenuItem.Text = "OUT to DXF";
            this.oUTToCADToolStripMenuItem.Click += new System.EventHandler(this.oUTToCADToolStripMenuItem_Click);
            // 
            // oUTToCSVToolStripMenuItem
            // 
            this.oUTToCSVToolStripMenuItem.Name = "oUTToCSVToolStripMenuItem";
            this.oUTToCSVToolStripMenuItem.Size = new System.Drawing.Size(186, 28);
            this.oUTToCSVToolStripMenuItem.Text = "OUT to CSV";
            this.oUTToCSVToolStripMenuItem.Click += new System.EventHandler(this.oUTToCSVToolStripMenuItem_Click);
            // 
            // lSTToDXFToolStripMenuItem
            // 
            this.lSTToDXFToolStripMenuItem.Name = "lSTToDXFToolStripMenuItem";
            this.lSTToDXFToolStripMenuItem.Size = new System.Drawing.Size(186, 28);
            this.lSTToDXFToolStripMenuItem.Text = "LST to DXF";
            this.lSTToDXFToolStripMenuItem.Click += new System.EventHandler(this.lSTToDXFToolStripMenuItem_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(248, 371);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(100, 22);
            this.txtPath.TabIndex = 2;
            this.txtPath.Visible = false;
            // 
            // txt_showMessage
            // 
            this.txt_showMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_showMessage.Font = new System.Drawing.Font("新細明體", 12F);
            this.txt_showMessage.Location = new System.Drawing.Point(12, 46);
            this.txt_showMessage.Multiline = true;
            this.txt_showMessage.Name = "txt_showMessage";
            this.txt_showMessage.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_showMessage.Size = new System.Drawing.Size(802, 347);
            this.txt_showMessage.TabIndex = 3;
            // 
            // oUT比較ToolStripMenuItem
            // 
            this.oUT比較ToolStripMenuItem.Name = "oUT比較ToolStripMenuItem";
            this.oUT比較ToolStripMenuItem.Size = new System.Drawing.Size(186, 28);
            this.oUT比較ToolStripMenuItem.Text = "OUT 比較";
            this.oUT比較ToolStripMenuItem.Click += new System.EventHandler(this.oUT比較ToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(826, 405);
            this.Controls.Add(this.txt_showMessage);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "資料處理";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 檔案ToolStripMenuItem;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.ToolStripMenuItem 執行ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oUTToCADToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oUTToCSVToolStripMenuItem;
        private System.Windows.Forms.TextBox txt_showMessage;
        private System.Windows.Forms.ToolStripMenuItem lSTToDXFToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oUT比較ToolStripMenuItem;
    }
}

