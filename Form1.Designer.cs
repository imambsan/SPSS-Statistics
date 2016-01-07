using System;
using System.Windows.Forms;

namespace ParallelSPSS
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripMenuItem();
            this.analyzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.regresiLinierToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meanPararelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.populationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.populationKurtosisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.populationSkewnessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.populationStandarDeviationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.populationVarianceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maximumSekuensialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimumSekuensialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.othersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.meanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mediumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maxToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.skewnessToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.standarDeviationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.varianceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.rangeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kurtosisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.countToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.covarinceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bantuanToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.reoGridControl2 = new unvell.ReoGrid.ReoGridControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.reoGridControl3 = new unvell.ReoGrid.ReoGridControl();
            this.menuStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(0, 0);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabControlNewButtonVisible = true;
            this.reoGridControl1.SheetTabControlWidth = 400;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.Size = new System.Drawing.Size(0, 0);
            this.reoGridControl1.TabIndex = 5;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.analyzeToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(875, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4,
            this.toolStripMenuItem5,
            this.toolStripMenuItem6,
            this.toolStripMenuItem7,
            this.toolStripMenuItem8,
            this.toolStripMenuItem9,
            this.toolStripMenuItem10});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(37, 20);
            this.toolStripMenuItem1.Text = "File";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem2.Text = "New";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem3.Text = "Open";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem4.Text = "Save";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.toolStripMenuItem4_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem5.Text = "Save As";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.toolStripMenuItem5_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem6.Text = "ImportFromExcel";
            this.toolStripMenuItem6.Click += new System.EventHandler(this.toolStripMenuItem6_Click);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem7.Text = "ExportAsExcel";
            this.toolStripMenuItem7.Click += new System.EventHandler(this.toolStripMenuItem7_Click);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem8.Text = "ImportFromCSV";
            this.toolStripMenuItem8.Click += new System.EventHandler(this.toolStripMenuItem8_Click);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem9.Text = "ExportAsCSV";
            this.toolStripMenuItem9.Click += new System.EventHandler(this.toolStripMenuItem9_Click);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(164, 22);
            this.toolStripMenuItem10.Text = "Exit";
            this.toolStripMenuItem10.Click += new System.EventHandler(this.toolStripMenuItem10_Click);
            // 
            // analyzeToolStripMenuItem
            // 
            this.analyzeToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.modusToolStripMenuItem,
            this.regresiLinierToolStripMenuItem,
            this.meanPararelToolStripMenuItem,
            this.maximumSekuensialToolStripMenuItem,
            this.minimumSekuensialToolStripMenuItem,
            this.populationToolStripMenuItem,
            this.othersToolStripMenuItem});
            this.analyzeToolStripMenuItem.Name = "analyzeToolStripMenuItem";
            this.analyzeToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.analyzeToolStripMenuItem.Text = "Analyze";
            this.analyzeToolStripMenuItem.Click += new System.EventHandler(this.analyzeToolStripMenuItem_Click);
            // 
            // modusToolStripMenuItem
            // 
            this.modusToolStripMenuItem.Name = "modusToolStripMenuItem";
            this.modusToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.modusToolStripMenuItem.Text = "Modus Paralel";
            this.modusToolStripMenuItem.Click += new System.EventHandler(this.modusToolStripMenuItem_Click);
            // 
            // regresiLinierToolStripMenuItem
            // 
            this.regresiLinierToolStripMenuItem.Name = "regresiLinierToolStripMenuItem";
            this.regresiLinierToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.regresiLinierToolStripMenuItem.Text = "Modus Sekuensial";
            this.regresiLinierToolStripMenuItem.Click += new System.EventHandler(this.regresiLinierToolStripMenuItem_Click);
            // 
            // meanPararelToolStripMenuItem
            // 
            this.meanPararelToolStripMenuItem.Name = "meanPararelToolStripMenuItem";
            this.meanPararelToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.meanPararelToolStripMenuItem.Text = "Mean Paralel";
            this.meanPararelToolStripMenuItem.Click += new System.EventHandler(this.meanPararelToolStripMenuItem_Click);
            // 
            // populationToolStripMenuItem
            // 
            this.populationToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.populationKurtosisToolStripMenuItem,
            this.populationSkewnessToolStripMenuItem,
            this.populationStandarDeviationToolStripMenuItem,
            this.populationVarianceToolStripMenuItem});
            this.populationToolStripMenuItem.Name = "populationToolStripMenuItem";
            this.populationToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.populationToolStripMenuItem.Text = "Population";
            // 
            // populationKurtosisToolStripMenuItem
            // 
            this.populationKurtosisToolStripMenuItem.Name = "populationKurtosisToolStripMenuItem";
            this.populationKurtosisToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.populationKurtosisToolStripMenuItem.Text = "Population Kurtosis";
            this.populationKurtosisToolStripMenuItem.Click += new System.EventHandler(this.populationKurtosisToolStripMenuItem_Click);
            // 
            // populationSkewnessToolStripMenuItem
            // 
            this.populationSkewnessToolStripMenuItem.Name = "populationSkewnessToolStripMenuItem";
            this.populationSkewnessToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.populationSkewnessToolStripMenuItem.Text = "Population Skewness";
            this.populationSkewnessToolStripMenuItem.Click += new System.EventHandler(this.populationSkewnessToolStripMenuItem_Click);
            // 
            // populationStandarDeviationToolStripMenuItem
            // 
            this.populationStandarDeviationToolStripMenuItem.Name = "populationStandarDeviationToolStripMenuItem";
            this.populationStandarDeviationToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.populationStandarDeviationToolStripMenuItem.Text = "Population Standar Deviation";
            this.populationStandarDeviationToolStripMenuItem.Click += new System.EventHandler(this.populationStandarDeviationToolStripMenuItem_Click);
            // 
            // populationVarianceToolStripMenuItem
            // 
            this.populationVarianceToolStripMenuItem.Name = "populationVarianceToolStripMenuItem";
            this.populationVarianceToolStripMenuItem.Size = new System.Drawing.Size(228, 22);
            this.populationVarianceToolStripMenuItem.Text = "Population Variance";
            this.populationVarianceToolStripMenuItem.Click += new System.EventHandler(this.populationVarianceToolStripMenuItem_Click);
            // 
            // maximumSekuensialToolStripMenuItem
            // 
            this.maximumSekuensialToolStripMenuItem.Name = "maximumSekuensialToolStripMenuItem";
            this.maximumSekuensialToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.maximumSekuensialToolStripMenuItem.Text = "Maximum Sekuensial";
            this.maximumSekuensialToolStripMenuItem.Click += new System.EventHandler(this.maximumSekuensialToolStripMenuItem_Click);
            // 
            // minimumSekuensialToolStripMenuItem
            // 
            this.minimumSekuensialToolStripMenuItem.Name = "minimumSekuensialToolStripMenuItem";
            this.minimumSekuensialToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.minimumSekuensialToolStripMenuItem.Text = "Minimum Sekuensial";
            this.minimumSekuensialToolStripMenuItem.Click += new System.EventHandler(this.minimumSekuensialToolStripMenuItem_Click);
            // 
            // othersToolStripMenuItem
            // 
            this.othersToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.meanToolStripMenuItem,
            this.mediumToolStripMenuItem,
            this.minToolStripMenuItem,
            this.maxToolStripMenuItem,
            this.skewnessToolStripMenuItem,
            this.standarDeviationToolStripMenuItem,
            this.varianceToolStripMenuItem,
            this.kurtosisToolStripMenuItem,
            this.countToolStripMenuItem,
            this.covarinceToolStripMenuItem,
            this.rangeToolStripMenuItem});
            this.othersToolStripMenuItem.Name = "othersToolStripMenuItem";
            this.othersToolStripMenuItem.Size = new System.Drawing.Size(190, 22);
            this.othersToolStripMenuItem.Text = "Others With Math.Net";
            this.othersToolStripMenuItem.Click += new System.EventHandler(this.othersToolStripMenuItem_Click);
            // 
            // meanToolStripMenuItem
            // 
            this.meanToolStripMenuItem.Name = "meanToolStripMenuItem";
            this.meanToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.meanToolStripMenuItem.Text = "Mean";
            this.meanToolStripMenuItem.Click += new System.EventHandler(this.meanToolStripMenuItem_Click);
            // 
            // mediumToolStripMenuItem
            // 
            this.mediumToolStripMenuItem.Name = "mediumToolStripMenuItem";
            this.mediumToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.mediumToolStripMenuItem.Text = "Median";
            this.mediumToolStripMenuItem.Click += new System.EventHandler(this.mediumToolStripMenuItem_Click_1);
            // 
            // minToolStripMenuItem
            // 
            this.minToolStripMenuItem.Name = "minToolStripMenuItem";
            this.minToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.minToolStripMenuItem.Text = "Min";
            this.minToolStripMenuItem.Click += new System.EventHandler(this.minToolStripMenuItem_Click);
            // 
            // maxToolStripMenuItem
            // 
            this.maxToolStripMenuItem.Name = "maxToolStripMenuItem";
            this.maxToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.maxToolStripMenuItem.Text = "Max";
            this.maxToolStripMenuItem.Click += new System.EventHandler(this.maxToolStripMenuItem_Click);
            // 
            // skewnessToolStripMenuItem
            // 
            this.skewnessToolStripMenuItem.Name = "skewnessToolStripMenuItem";
            this.skewnessToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.skewnessToolStripMenuItem.Text = "Skewness";
            this.skewnessToolStripMenuItem.Click += new System.EventHandler(this.skewnessToolStripMenuItem_Click);
            // 
            // standarDeviationToolStripMenuItem
            // 
            this.standarDeviationToolStripMenuItem.Name = "standarDeviationToolStripMenuItem";
            this.standarDeviationToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.standarDeviationToolStripMenuItem.Text = "Standar Deviation";
            this.standarDeviationToolStripMenuItem.Click += new System.EventHandler(this.standarDeviationToolStripMenuItem_Click);
            // 
            // varianceToolStripMenuItem
            // 
            this.varianceToolStripMenuItem.Name = "varianceToolStripMenuItem";
            this.varianceToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.varianceToolStripMenuItem.Text = "Variance";
            this.varianceToolStripMenuItem.Click += new System.EventHandler(this.varianceToolStripMenuItem_Click);
            // 
            // rangeToolStripMenuItem
            // 
            this.rangeToolStripMenuItem.Name = "rangeToolStripMenuItem";
            this.rangeToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.rangeToolStripMenuItem.Text = "Range";
            this.rangeToolStripMenuItem.Click += new System.EventHandler(this.rangeToolStripMenuItem_Click);
            // 
            // kurtosisToolStripMenuItem
            // 
            this.kurtosisToolStripMenuItem.Name = "kurtosisToolStripMenuItem";
            this.kurtosisToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.kurtosisToolStripMenuItem.Text = "Kurtosis";
            this.kurtosisToolStripMenuItem.Click += new System.EventHandler(this.kurtosisToolStripMenuItem_Click);
            // 
            // countToolStripMenuItem
            // 
            this.countToolStripMenuItem.Name = "countToolStripMenuItem";
            this.countToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.countToolStripMenuItem.Text = "Count";
            this.countToolStripMenuItem.Click += new System.EventHandler(this.countToolStripMenuItem_Click);
            // 
            // covarinceToolStripMenuItem
            // 
            this.covarinceToolStripMenuItem.Name = "covarinceToolStripMenuItem";
            this.covarinceToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.covarinceToolStripMenuItem.Text = "Entrophy";
            this.covarinceToolStripMenuItem.Click += new System.EventHandler(this.covarinceToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bantuanToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem.Text = "About";
            // 
            // bantuanToolStripMenuItem
            // 
            this.bantuanToolStripMenuItem.Name = "bantuanToolStripMenuItem";
            this.bantuanToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.bantuanToolStripMenuItem.Text = "Informasi";
            this.bantuanToolStripMenuItem.Click += new System.EventHandler(this.bantuanToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(0, 26);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(880, 465);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.LightGray;
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.reoGridControl2);
            this.tabPage1.Location = new System.Drawing.Point(4, 4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(872, 439);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Data View";
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(80, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(789, 20);
            this.textBox1.TabIndex = 2;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBox1.Enter += new System.EventHandler(this.textBox1_Enter);
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox1_KeyDown);
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(22, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "1 : ";
            // 
            // reoGridControl2
            // 
            this.reoGridControl2.BackColor = System.Drawing.Color.White;
            this.reoGridControl2.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl2.LeadHeaderContextMenuStrip = null;
            this.reoGridControl2.Location = new System.Drawing.Point(-3, 24);
            this.reoGridControl2.Name = "reoGridControl2";
            this.reoGridControl2.RowHeaderContextMenuStrip = null;
            this.reoGridControl2.Script = null;
            this.reoGridControl2.SheetTabContextMenuStrip = null;
            this.reoGridControl2.SheetTabControlNewButtonVisible = true;
            this.reoGridControl2.SheetTabControlWidth = 60;
            this.reoGridControl2.SheetTabNewButtonVisible = true;
            this.reoGridControl2.Size = new System.Drawing.Size(875, 413);
            this.reoGridControl2.TabIndex = 0;
            this.reoGridControl2.Text = "Data View";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.reoGridControl3);
            this.tabPage2.Location = new System.Drawing.Point(4, 4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(872, 439);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Variable View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // reoGridControl3
            // 
            this.reoGridControl3.BackColor = System.Drawing.Color.White;
            this.reoGridControl3.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl3.LeadHeaderContextMenuStrip = null;
            this.reoGridControl3.Location = new System.Drawing.Point(-3, -3);
            this.reoGridControl3.Name = "reoGridControl3";
            this.reoGridControl3.RowHeaderContextMenuStrip = null;
            this.reoGridControl3.Script = null;
            this.reoGridControl3.SheetTabContextMenuStrip = null;
            this.reoGridControl3.SheetTabControlNewButtonVisible = true;
            this.reoGridControl3.SheetTabControlWidth = 60;
            this.reoGridControl3.SheetTabNewButtonVisible = true;
            this.reoGridControl3.Size = new System.Drawing.Size(875, 440);
            this.reoGridControl3.TabIndex = 0;
            this.reoGridControl3.Text = "reoGridVariableView";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(875, 493);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.reoGridControl1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SPSS - Statistics";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Resize += new System.EventHandler(this.Form1_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }



        #endregion

        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem7;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem8;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem9;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem10;
        private System.Windows.Forms.ToolStripMenuItem analyzeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modusToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private unvell.ReoGrid.ReoGridControl reoGridControl2;
        private unvell.ReoGrid.ReoGridControl reoGridControl3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private ToolStripMenuItem regresiLinierToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem bantuanToolStripMenuItem;
        private ToolStripMenuItem meanPararelToolStripMenuItem;
        private ToolStripMenuItem othersToolStripMenuItem;
        private ToolStripMenuItem meanToolStripMenuItem;
        private ToolStripMenuItem mediumToolStripMenuItem;
        private ToolStripMenuItem minToolStripMenuItem;
        private ToolStripMenuItem maxToolStripMenuItem;
        private ToolStripMenuItem skewnessToolStripMenuItem;
        private ToolStripMenuItem standarDeviationToolStripMenuItem;
        private ToolStripMenuItem varianceToolStripMenuItem;
        private ToolStripMenuItem rangeToolStripMenuItem;
        private ToolStripMenuItem kurtosisToolStripMenuItem;
        private ToolStripMenuItem countToolStripMenuItem;
        private ToolStripMenuItem covarinceToolStripMenuItem;
        private ToolStripMenuItem populationToolStripMenuItem;
        private ToolStripMenuItem populationKurtosisToolStripMenuItem;
        private ToolStripMenuItem populationSkewnessToolStripMenuItem;
        private ToolStripMenuItem populationStandarDeviationToolStripMenuItem;
        private ToolStripMenuItem populationVarianceToolStripMenuItem;
        private ToolStripMenuItem maximumSekuensialToolStripMenuItem;
        private ToolStripMenuItem minimumSekuensialToolStripMenuItem;
    }
}

