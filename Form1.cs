﻿using Cudafy;
using Cudafy.Host;
using Cudafy.Translator;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using unvell.ReoGrid;
using unvell.ReoGrid.CellTypes;
using unvell.ReoGrid.DataFormat;
using unvell.ReoGrid.Events;
using MathNet.Numerics.Statistics;

namespace ParallelSPSS
{
    public partial class Form1 : Form
    {
        public string[,] DataView = new string[10000, 13];
        public string[,] VariableView = new string[100, 100];
        public Form1()
        {
            InitializeComponent();

            //List<ValueCoding> tempValueCoding = new List<ValueCoding>();
            //for (int j = 0; j < 10; j++)
            //    tempValueCoding.Add(new ValueCoding { label = "coba " + j, value = j * j / 2 });

            //for (int i=0;i<10;i++)
            //{
            //    if (i > 2)
            //        tempValueCoding = null;
            //    Data.variableView.Add(new VariableView { nama="coba "+i,type="int",width=3,valueCoding=tempValueCoding ,Decimal=i^2 });

            //}
            for (int i = 0; i < 100; i++)
            {
                int j = i + 1;
                Data.variableView.Add(new VariableView { nama = "VAR00"+j, type = "Numeric", label = "" });
            }
            init();
        }

        private void init()
        {
            var sheet = reoGridControl2.CurrentWorksheet;
            sheet.CellMouseDown += sheet_CellMouseDown;
            sheet.SelectionRangeChanged += sheet_SelectionRangeChanged;
            sheet.FocusPosChanged += sheet_FocusPosChanged;
            var sheet2 = reoGridControl3.CurrentWorksheet;
            sheet2.Rows = 100;
            ButtonCell button = new ButtonCell();
            button.Click += Button_Click;
            for (int i = 0; i < 100; i++)
            {   if(sheet.ColumnHeaders[i]!=null)
                sheet.ColumnHeaders[i].Text = "VAR";
                button = new ButtonCell();
                button.Click += Button_Click;
                sheet2[i, 4] = button;
                sheet2[i, 4] = "...";

                button = new ButtonCell();
                button.Click += Missing_Click;
                sheet2[i, 3] = button;
                sheet2[i, 3] = "...";
                Data.variableView[i].missing = new List<string>();
                Data.variableView[i].missingRange = new List<string>();

            }


   
            sheet.CellMouseDown += columnKeyDown;
            sheet.CellDataChanged += Sheet_CellDataChanged;
            sheet2.CellDataChanged += Sheet2_CellDataChanged1;
   
            sheet2.SetCols(5);
            sheet2.ColumnHeaders[0].Text = "Name";
            sheet2.ColumnHeaders[1].Text = "Type";
            sheet2.ColumnHeaders[2].Text = "Label";
            sheet2.ColumnHeaders[3].Text = "Missing";
            sheet2.ColumnHeaders[4].Text = "Values"; 
            
            for(int i=0;i<Data.variableView.Count()-100;i++)
            {
                sheet2[i, 0] = Data.variableView[i].nama;
                sheet2[i, 1] = Data.variableView[i].type;
                sheet2[i, 2] = Data.variableView[i].label;
           //     sheet2[i, 3] = Data.variableView[i].Decimal;
                sheet.ColumnHeaders[i].Text = Data.variableView[i].nama;

            }
           
                //            sheet.ColumnHeaders[1].DefaultCellBody = typeof(unvell.ReoGrid.CellTypes.RadioButtonGroup);
                sheet2.CellDataChanged += Sheet2_CellDataChanged;
            sheet2.CellMouseDown += Sheet2_CellMouseDown;
            sheet2.SelectionRangeChanged += sheet2_SelectionRangeChanged;

  //          sheet.SetRangeDataFormat(ReoGridRange.EntireRange, CellDataFormatFlag.Number,
  //           new NumberDataFormatter.NumberFormatArgs()
  //{
  //    // decimal digit places 0.1234
  //    DecimalPlaces = 4,

  //    // negative number style: (123) 
  //    NegativeStyle = NumberDataFormatter.NumberNegativeStyle.RedBrackets,

  //    // use separator: 123,456
  //    UseSeparator = true,
  //});
        }

        private void Missing_Click(object sender, EventArgs e)
        {
            Form dlg1 = new MissingForm();
            DialogResult dr = new DialogResult();
            dr = dlg1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                MessageBox.Show("Data Saved");
            }
            else
            {
                dlg1.Close();
            }
        }

        void sheet_FocusPosChanged(object sender, CellPosEventArgs e)
        {
            var sheet = reoGridControl2.CurrentWorksheet;

            if (sheet[e.Position] != null)
                textBox1.Text = sheet[e.Position].ToString();
            else
                textBox1.Text = "";

            string temp = sheet.ColumnHeaders[e.Position.Col].Text;
            if (temp == "VAR")
                temp = "";
            label1.Text = e.Position.Row + 1 + " : " + temp;
            if (sheet[e.Position] != null)
                textBox1.Text = sheet[e.Position].ToString();
            else
                textBox1.Text = "";
        }

        void sheet2_SelectionRangeChanged(object sender, RangeEventArgs e)
        {
            var sheet = reoGridControl2.CurrentWorksheet;
            var sheet2 = reoGridControl3.CurrentWorksheet;

            if (e.Range.Cols == sheet2.ColumnCount)
            {
                // MessageBox.Show("Selection changed: " + args.Range.ToAddress());
                tabControl1.SelectedIndex = 0;
                sheet.FocusPos = new unvell.ReoGrid.ReoGridPos(0, e.Range.Row);
            }
        }

       
        private void Sheet2_CellDataChanged1(object sender, CellEventArgs e)
        {

                var sheet2 = reoGridControl3.CurrentWorksheet;
                sheet2.CellDataChanged -= Sheet2_CellDataChanged1;
                for (int i = 0; i < e.Cell.Position.Row+1 ; i++)
                    for (int j = 0; j < 3; j++)
                        if (sheet2[i, j] == null || sheet2[i, j] == "")
                            if (j == 0)
                                sheet2[i, j] = Data.variableView[i].nama;
                            else if (j == 1)
                                sheet2[i, j] = Data.variableView[i].type;
                            else if (j == 2)
                                sheet2[i, j] = Data.variableView[i].label;
                            //else if (j == 3)
                            //    sheet2[i, j] = Data.variableView[i].Missing;

                Data.variableView[e.Cell.Position.Row].nama = sheet2[e.Cell.Position.Row, 0].ToString();
                Data.variableView[e.Cell.Position.Row].type = sheet2[e.Cell.Position.Row, 1].ToString();
                Data.variableView[e.Cell.Position.Row].label = sheet2[e.Cell.Position.Row, 2].ToString();
                sheet2.CellDataChanged += Sheet2_CellDataChanged1;
                //   Data.variableView[e.Cell.Position.Row].Decimal = sheet2[e.Cell.Position.Row, 3].ToString();
        
        }

        private void Sheet2_CellMouseDown(object sender, CellMouseEventArgs e)
        {
            Data.indexRow = e.CellPosition.Row;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Control control = (Control)sender;
            Size newSize = new Size(control.Size.Width -20, control.Size.Height - 70);
            reoGridControl2.Size = newSize;
            reoGridControl3.Size = newSize;
            tabControl1.Size = new Size(newSize.Width+5,newSize.Height+5);
        }


        
        private void Button_Click(object sender, EventArgs e)
        {
            var sheet2 = reoGridControl3.CurrentWorksheet;
        //    Data.indexRow = sheet2.GetRowHeader
            Form dlg1 = new Form();
            DialogResult dr = new DialogResult();
            dlg1 = new FormValue();
            dr = dlg1.ShowDialog();
            if (dr == DialogResult.OK)
            {
                MessageBox.Show("Data Saved");
 //               Data.variableView[Data.indexRow].valueCoding = FormValue.tempValueCoding;
            }
            else
            {
         //       FormValue.tempValueCoding.Clear();
                dlg1.Close();
            }
          //  else if (dr == DialogResult.Cancel)
          //      MessageBox.Show("User clicked Cancel button");



        }

        private void Sheet2_CellDataChanged(object sender, CellEventArgs e)
        {
            if(e.Cell.Position.Col==0 && e.Cell.Data!=null)
            {
                var sheet = reoGridControl2.CurrentWorksheet;
                sheet.ColumnHeaders[e.Cell.Position.Row].Text = e.Cell.Data.ToString();
            }
            if(e.Cell.Position.Col==1 && e.Cell.Data!=null)
            {
                //var sheet = reoGridControl2.CurrentWorksheet;
             //   sheet.ColumnHeaders[e.Cell.Position.Row].DefaultCellBody = typeof(unvell.ReoGrid.CellTypes.);
            }
            if (e.Cell.Position.Col == 2 && e.Cell.Data != null)
            {
                //var sheet = reoGridControl2.CurrentWorksheet;
                //sheet.SetColumnsWidth(Data.indexRow, 1, (ushort)Data.variableView[2].label);
            }

        }

        private void Sheet_CellDataChanged(object sender, CellEventArgs e)
        {
            if (e.Cell.Data != null)
            {
                var sheet2 = reoGridControl3.CurrentWorksheet;
                if ( sheet2[e.Cell.Column,1]== "Numeric" && !IsDigitsOnly(e.Cell.Data.ToString())
                    || sheet2[e.Cell.Column, 1] == "String" && IsDigitsOnly(e.Cell.Data.ToString()))
                {
                    var sheet = reoGridControl2.CurrentWorksheet;
                    sheet[e.Cell.Position] = null;
                    textBox1.Text = "";
                }

                if (e.Cell.Data != null)
                textBox1.Text = e.Cell.Data.ToString();
                
            }

        }
        bool onTextboxFocus = false;
        void sheet_CellMouseDown(object sender, CellMouseEventArgs e)
        {
            var sheet = reoGridControl2.CurrentWorksheet;
            string temp = sheet.ColumnHeaders[e.CellPosition.Col].Text;
            if (temp == "VAR")
                temp = "";
            label1.Text = e.CellPosition.Row+1 + " : " + temp ;
            if (e.Cell != null && e.Cell.Data!=null)
                textBox1.Text = e.Cell.Data.ToString();
            else
                textBox1.Text = "";

            if (e.CellPosition.Row == sheet.RowCount-1)
                sheet.InsertRows(sheet.RowCount-1, 100);

            onTextboxFocus = false;
        }

        void sheet_SelectionRangeChanged(object sender, RangeEventArgs args)
        {
            var sheet = reoGridControl2.CurrentWorksheet;
            var sheet2 = reoGridControl3.CurrentWorksheet;

            if (args.Range.Rows == sheet.RowCount)
            {
                // MessageBox.Show("Selection changed: " + args.Range.ToAddress());
                tabControl1.SelectedIndex = 1;
                sheet2.FocusPos = new unvell.ReoGrid.ReoGridPos(args.Range.Col, 0);
            }
        }
        void columnKeyDown(object sender, CellMouseEventArgs e)
        {
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".sp";
            dlg.Filter = "SPSS Parallel format(*.sp)|*.sp";

            // Process open file dialog box results 
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                // Open document 
                if (File.Exists(dlg.FileName)) {
                    try
                    {
                        //    reoGridControl2.Load(dlg.FileName);
                        string json = File.ReadAllText(dlg.FileName);
                        JToken obj = JToken.Parse(json);
                        JToken temp = obj["Data"];
                        string temp2 = temp[0]["DataView"].ToString();
                        string temp3 = temp[1]["VariableView"].ToString();
                        var sheet1 = reoGridControl2.CurrentWorksheet;
                        var sheet2 = reoGridControl3.CurrentWorksheet;
                        //        DataView = =temp2.Select(jv => (string)jv).ToArray();
                        DataView = JsonConvert.DeserializeObject<string[,]>(temp2);
                        Data.variableView = JsonConvert.DeserializeObject<List<VariableView>>(temp3);
                        sheet1.AppendRows(DataView.GetLength(0) - 200);
                        //     Debug.Write(DataView.GetLength(0)+ " , "+ DataView.GetLength(1));
                        for (int i = 0; i < DataView.GetLength(0); i++)
                            for (int j = 0; j < DataView.GetLength(1); j++)
                                sheet1[i,j] = DataView[i,j];

                        //for (int x = 0; x < Data.variableView.GetLength(0); x++)
                        //    for (int y = 0; y < VariableView.GetLength(1); y++)
                        //        sheet2[x, y] = VariableView[x, y];
                        int y = 0;
                        for(int x=0;x<Data.variableView.Count;x++)
                        {
                            y = x + 1;
                            if (Data.variableView[x].nama != "VAR00" + y)
                            {
                                sheet2[x, 0] = Data.variableView[x].nama;
                                sheet2[x, 1] = Data.variableView[x].type;
                                sheet2[x, 2] = Data.variableView[x].label;
                                //      sheet2[x, 3] = Data.variableView[x].missing;
                            }
                            
                        }
                        filePath = dlg.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(this, "Loading error: " + ex.Message, "Error");
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel 2007 Document(*.xlsx)|*.xlsx";
            int jumlahKolom=0;
            // Process open file dialog box results 
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                // Open document 
                try
                {
                    reoGridControl2.Load(dlg.FileName);
                    var sheet1 = reoGridControl2.CurrentWorksheet;
                    for (int i = 0; i < sheet1.Columns; i++)
                        if (sheet1[0, i] != null && sheet1[0, i] != "")
                        {
                            sheet1.ColumnHeaders[i].Text = sheet1[0, i].ToString();
                            Data.variableView[i].nama = sheet1[0, i].ToString();
                            Data.variableView[i].label = sheet1[0, i].ToString();

                            jumlahKolom++;
                        }
           //         Debug.Write(jumlahKolom);
                    sheet1.DeleteRows(0,1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Loading error: " + ex.Message, "Error");
                }

                var sheet = reoGridControl2.CurrentWorksheet;
                var sheet2 = reoGridControl3.CurrentWorksheet;
                for (int j = 0; j < jumlahKolom; j++)
                {
                    // int jumlahBaris = 0;
                    bool onlyNumber = true;
                    for (int i = 0; i < 10; i++)
                        if (sheet[i, j] != null && !IsDigitsOnly(sheet[i, j].ToString()))
                        {
                            onlyNumber = false;
                            //         jumlahBaris++;
                        }

                    if (onlyNumber)
                    {
                        Data.variableView[j].type = "Numeric";
                    }
                    else
                    {
                        Data.variableView[j].type = "String";
                    }

                    sheet2.CellDataChanged -= Sheet2_CellDataChanged1;
                    sheet2.CellDataChanged -= Sheet2_CellDataChanged;
                    sheet2[j, 0] = Data.variableView[j].nama;
                    sheet2[j, 1] = Data.variableView[j].type;
                    sheet2[j, 2] = Data.variableView[j].label;
                    //sheet2[jumlahBaris, 3] = Data.variableView[j].type;
                    sheet2.CellDataChanged += Sheet2_CellDataChanged;
                    sheet2.CellDataChanged += Sheet2_CellDataChanged1;
                    sheet.CellMouseDown += columnKeyDown;
                    sheet.CellMouseDown += sheet_CellMouseDown;
                    sheet.SelectionRangeChanged += sheet_SelectionRangeChanged;
                    sheet2.SelectionRangeChanged += sheet2_SelectionRangeChanged;
                    sheet.FocusPosChanged += sheet_FocusPosChanged;
                    if (sheet.RowCount % 2 != 0)
                        sheet.AppendRows(1);
                }


            }
        }

        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c.Equals('.'))
                    return true;
                if (c < '0' || c > '9' )
                    return false;
            }

            return true;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".xlsx";
            dlg.Filter = "Excel 2007 Document|*.xlsx";
            var sheet = reoGridControl2.CurrentWorksheet;

            // Process open file dialog box results 
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Open document 

                sheet.InsertRows(0, 1);
                for (int i = 0; i < sheet.ColumnCount; i++)
                        sheet[0, i] = sheet.ColumnHeaders[i].Text;

                    reoGridControl2.Save(dlg.FileName);
            //    reoGridControl3.Save(dlg.FileName);
                System.Diagnostics.Process.Start(dlg.FileName);
                sheet.DeleteRows(0, 1);

            }
            
        }

        private void analyzeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            dlg.DefaultExt = ".CSV";
            dlg.Filter = "Comma Separated Value(*.CSV)|*.CSV";
            int jumlahKolom = 0;

            // Process open file dialog box results 
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Open document 
                try
                {
                    reoGridControl2.Load(dlg.FileName);
                    var sheet1 = reoGridControl2.CurrentWorksheet;
                    for (int i = 0; i < sheet1.Columns; i++)
                        if (sheet1[0, i] != null && sheet1[0, i] != "")
                        {
                            sheet1.ColumnHeaders[i].Text = sheet1[0, i].ToString();
                            Data.variableView[i].nama = sheet1[0, i].ToString();
                            Data.variableView[i].label = sheet1[0, i].ToString();

                            jumlahKolom++;
                        }
              //      Debug.Write(jumlahKolom);
                    sheet1.DeleteRows(0, 1);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(this, "Loading error: " + ex.Message, "Error");
                }

                var sheet = reoGridControl2.CurrentWorksheet;
                var sheet2 = reoGridControl3.CurrentWorksheet;
                for (int j = 0; j < jumlahKolom; j++)
                {
                    // int jumlahBaris = 0;
                    bool onlyNumber = true;
                    for (int i = 0; i < 10; i++)
                        if (sheet[i, j] != null && !IsDigitsOnly(sheet[i, j].ToString()))
                        {
                            onlyNumber = false;
                            //         jumlahBaris++;
                        }

                    if (onlyNumber)
                    {
                        Data.variableView[j].type = "Numeric";
                    }
                    else
                    {
                        Data.variableView[j].type = "String";
                    }

                    sheet2.CellDataChanged -= Sheet2_CellDataChanged1;
                    sheet2.CellDataChanged -= Sheet2_CellDataChanged;
                    sheet2[j, 0] = Data.variableView[j].nama;
                    sheet2[j, 1] = Data.variableView[j].type;
                    sheet2[j, 2] = Data.variableView[j].label;
                    //sheet2[jumlahBaris, 3] = Data.variableView[j].type;
                    sheet2.CellDataChanged += Sheet2_CellDataChanged;
                    sheet2.CellDataChanged += Sheet2_CellDataChanged1;
                    sheet.CellMouseDown += columnKeyDown;
                    sheet.CellMouseDown += sheet_CellMouseDown;
                    sheet.SelectionRangeChanged += sheet_SelectionRangeChanged;
                    sheet2.SelectionRangeChanged += sheet2_SelectionRangeChanged;
                    sheet.FocusPosChanged += sheet_FocusPosChanged;
                    if (sheet.RowCount % 2 != 0)
                        sheet.AppendRows(1);
                }

            }
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.DefaultExt = ".CSV";
            dlg.Filter = "Comma Separated Value|*.CSV";
            var sheet = reoGridControl2.CurrentWorksheet;

            // Process open file dialog box results 
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                // Open document 
                sheet.InsertRows(0, 1);
                for (int i = 0; i < sheet.ColumnCount; i++)
                    sheet[0, i] = sheet.ColumnHeaders[i].Text;
                //     reoGridControl2.Save(dlg.FileName);
                //    reoGridControl3.Save(dlg.FileName);
                //    System.Diagnostics.Process.Start(dlg.FileName);
                var worksheet = this.reoGridControl2.CurrentWorksheet;
                worksheet.ExportAsCSV(dlg.FileName, 0, Encoding.Unicode);
                sheet.DeleteRows(0, 1);

            }
        }

        string filePath;
        private async void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            //SaveFileDialog dlg = new SaveFileDialog();
            //dlg.DefaultExt = ".rgf";
            //dlg.Filter = "Reo Grid F|*.rgf";

            //// Process open file dialog box results 
            //if (dlg.ShowDialog() == DialogResult.OK)
            //{
            //    // Open document 
            //    //     reoGridControl2.Save(dlg.FileName);
            //    //    reoGridControl3.Save(dlg.FileName);
            //    //    System.Diagnostics.Process.Start(dlg.FileName);
            //    var worksheet = this.reoGridControl2.CurrentWorksheet;
            //    worksheet.Save(dlg.FileName);

            //}

            string json;
            json = "{\"Data\":[{\"DataView\": ";
            var sheet1 = reoGridControl2.CurrentWorksheet;
            var sheet2 = reoGridControl3.CurrentWorksheet;
            for (int i = 0; i < sheet1.RowCount; i++)
                for (int j = 0; j < sheet1.ColumnCount; j++)
                    if(sheet1[i, j]!=null)
                         DataView[i, j] = sheet1[i, j].ToString();

            json += await JsonConvert.SerializeObjectAsync(DataView);
            json += " }, {\"VariableView\": ";

            for (int i = 0; i < sheet2.RowCount; i++)
                for (int j = 0; j < sheet2.ColumnCount; j++)
                    if (sheet2[i, j] != null)
                        VariableView[i, j] = sheet2[i, j].ToString();
            json += await JsonConvert.SerializeObjectAsync(Data.variableView);
            json += " }]}";


            //Debug.WriteLine(json);
            if (filePath == null || filePath == "")
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".sp";
                dlg.Filter = "SPSS Sistem Paralel|*.sp";

                // Process open file dialog box results 
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filePath=dlg.FileName;
                    System.IO.File.WriteAllText(filePath, json);
                }
            }
            else
                System.IO.File.WriteAllText(filePath, json);


        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private static GPGPU _gpu;
        public static List<double> results = new List<double>();
        public static List<string> columnChoosen = new List<string>();
        public static string judul;

        private void modusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IDictionary<string, double> hasilmodus = new Dictionary<string, double>();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length;ix++ )
                if(Data.columnChoosen[ix]!=-1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length;index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        string labelkolom = Data.variableView[Data.columnChoosen[index]].nama;
                        Console.WriteLine(column);
                        
                        try
                        {
                            var sheet=reoGridControl2.CurrentWorksheet;
                            int N = sheet.RowCount;
                            Console.WriteLine(N);

                            double[] arrayIsi = new double[N];
                            for (int i = 0; i < N; i++) {
                                var isi = sheet.GetCell(i, column);


                                if (isi != null && isi.Data != null)
                                {
                                    string s = isi.Data.ToString();
                                    double bil;
                                    try
                                    {
                                        bil = double.Parse(s);
                                    }
                                    catch (Exception c) 
                                    {
                                        bil = 0;
                                    }
                                    arrayIsi[i] = bil;

                                    /*
                                    for (int ix = 0; ix < Data.variableView[column].missing.Count; ix++)
                                    {
                                        if(bil.ToString() != Data.variableView[column].missing[ix])
                                      

                                    }*/
                                }
                            }
                            double temp, temp2;
                            IDictionary<double, int> zz = new Dictionary<double, int>();
                            for(int i = 0; i<arrayIsi.Length; i++) {
                                foreach (var terserah in Data.variableView[column].missing) // Missing Diskritisasi
                                {
                                    if (terserah != arrayIsi[i].ToString())
                                        if (zz.ContainsKey(arrayIsi[i]))
                                        {
                                            zz[arrayIsi[i]] = zz[arrayIsi[i]] + 1;
                                        }
                                        else
                                        {
                                            zz.Add(arrayIsi[i], 1);
                                        }
                                    //untuk mengeluarkan perhitungan frekuensi dari sebuah bilangan
                                }

                                if (Data.variableView[column].missingRange.Count > 0)
                                {

                                    double.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    double.TryParse(Data.variableView[column].missingRange[1], out temp2);

                                    if ( arrayIsi[i] < temp || arrayIsi[i] > temp2)
                                        if (zz.ContainsKey(arrayIsi[i]))
                                        {
                                            zz[arrayIsi[i]] = zz[arrayIsi[i]] + 1;
                                        }
                                        else
                                        {
                                            zz.Add(arrayIsi[i], 1);
                                        }
                                }

                                
                            }

                            if (zz.Count == 0)
                            {
                                for (int i = 0; i < arrayIsi.Length; i++)

                                    if (zz.ContainsKey(arrayIsi[i]))
                                    {
                                        zz[arrayIsi[i]] = zz[arrayIsi[i]] + 1;
                                    }
                                    else
                                    {
                                        zz.Add(arrayIsi[i], 1);
                                    }
                            }
                            zz.Remove(0);

                            //Parallel Disini aja kali ya 
                            //Menghitung Jumlah data dan dibagi 2 di masukan ke dalam thread                                                   

                            double mode = 0;
                            
                            int ma = 0;
                            var sw = Stopwatch.StartNew();
                            foreach (KeyValuePair<double, int> pair in zz)
                            {
                                if (ma < pair.Value)
                                {
                                    ma = pair.Value;
                                    mode = pair.Key;
                                }
                                //Console.WriteLine("{0}, {1}", pair.Key, pair.Value);
                                Console.WriteLine("Sekuensial thread {0} nilainya {1}", Thread.CurrentThread.ManagedThreadId, pair);
                            }
                            Console.WriteLine("Nilai Modusnya adalah : {0}", mode);
                            Console.WriteLine("waktu seq: {0}", sw.Elapsed.TotalMilliseconds);
                            Console.WriteLine();
                            hasilmodus.Add(labelkolom, mode);

                            double labelparalel = 0;
                            float modusparalel = 0;
                            sw = Stopwatch.StartNew();
                            Parallel.ForEach(zz, isi =>
                            {
                                if (modusparalel < isi.Value)
                                {
                                    modusparalel = isi.Value;
                                    labelparalel = isi.Key;
                                }
                                Console.WriteLine("Paralel thread {0} nilainya {1}", Thread.CurrentThread.ManagedThreadId, isi);

                            });
                            results.Add(labelparalel);
                            Console.WriteLine("hasil modus paralel: {0}", labelparalel);
                            Console.WriteLine("Wktu parael: {0}", sw.Elapsed.TotalMilliseconds);
                           
                            /*int jumlahData = arrayIsi.Length / 2;
                            double[] bagian_1 = new double[jumlahData];
                            double[] bagian_2 = new double[jumlahData];
                            double[] allocate = new double[jumlahData];

                            int counter = 0;
                            foreach (double data in arrayIsi)
                            {
                                if (counter <= jumlahData)
                                {
                                    bagian_1[counter] = data;
                                }
                                else
                                {
                                    bagian_2[counter] = data;
                                }

                                counter++;
                            }



                            double[] dev_a = _gpu.CopyToDevice(bagian_1);
                            double[] dev_b = _gpu.CopyToDevice(bagian_2);
                            double[] dev_c = _gpu.Allocate<double>(allocate);

                            while (jumlahData > 1) {

                                if (jumlahData % 2 == 0)
                                {
                                    jumlahData = jumlahData / 2;
                                }
                                else {
                                    jumlahData = (jumlahData + 1) / 2;
                                }
                            }
                             */

                            //mode = zz.GroupBy(value => value.Value).OrderByDescending(group => group.Count()).First().First().Key;
                            //Console.WriteLine(mode);
                            //hasilmodus.Add(labelkolom, mode);
                            //Console.WriteLine("di atas ini");

                            /* 
                             * hapus aja
                            int[] data = new int[10000000];
                            int[] K=new int[2500];
                            IDictionary<string, int> kamus = new Dictionary<string, int>();
                            kamus.Add("1", 13);
                            kamus.Add("2", 99999668);
                            int maxValue = 0;
                            for (int i = 0; i < sheet.RowCount;i++ )
                            {
                                data[i] = Convert.ToInt32(sheet[i,0]);
                            }

                            for (int x = 0; x < data.Length; x++)
                            {
                                //K[data[x]] = K[data[x]] + 1;
                                if(kamus.ContainsKey(data[x].ToString())) {
                                    kamus[data[x].ToString()] = kamus[data[x].ToString()] + 1;
                                    //kamus.Add(data[x].ToString(), kamus[data[x].ToString()] + 1);
                                } else {
                                    //kamus[data[x].ToString()] = 1;
                                    kamus.Add(data[x].ToString(), 1);
                                }
                            }
                            //Console.WriteLine(kamus.ToString());
                            foreach (int z in kamus.Values) {
                                //Console.WriteLine(z);
                            }
                            maxValue = kamus.Values.Max();
                            
                            for (int y = 0; y < data.Length; y++)
                            {
                                if(data[y]>kamus[data[y].ToString()])
                                //(data[y]>K[data[y]])
                                {
                                    maxValue=data[y];
                                    //diubah
                                    //maxValue = kamus[data[y]];  
                                }
                            }
                             
                            
                            Debug.WriteLine(maxValue);
                            */
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new ResultForm();

                /*
                foreach (KeyValuePair<string, double> pair in hasilmodus)
                {
                    Console.WriteLine("Kolom {0} nilai modus {1}",
                    pair.Key,
                    pair.Value);

                }
                 */

                dialog = dialogResult.ShowDialog();

              //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        [Cudafy]
        public static void addVector(GThread thread, float[] a, float[] b, float[] c, int N)
        {
            int tid = thread.threadIdx.x + thread.blockIdx.x * thread.blockDim.x;
            while (tid < N)
            {
                c[tid] = a[tid] + b[tid];
                tid += thread.gridDim.x;
            }

        }

        private async void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            string json;
            json = "{\"Data\":[{\"DataView\": ";
            var sheet1 = reoGridControl2.CurrentWorksheet;
            var sheet2 = reoGridControl3.CurrentWorksheet;
            for (int i = 0; i < sheet1.RowCount; i++)
                for (int j = 0; j < sheet1.ColumnCount; j++)
                    if (sheet1[i, j] != null)
                        DataView[i, j] = sheet1[i, j].ToString();

            json += await JsonConvert.SerializeObjectAsync(DataView);
            json += " }, {\"VariableView\": ";

            for (int i = 0; i < sheet2.RowCount; i++)
                for (int j = 0; j < sheet2.ColumnCount; j++)
                    if (sheet2[i, j] != null)
                        VariableView[i, j] = sheet2[i, j].ToString();
            json += await JsonConvert.SerializeObjectAsync(Data.variableView);
            json += " }]}";


            //Debug.WriteLine(json);
           
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.DefaultExt = ".sp";
                dlg.Filter = "SPSS Sistem Paralel|*.sp";

                // Process open file dialog box results 
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filePath = dlg.FileName;
                    System.IO.File.WriteAllText(filePath, json);
                }
         
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bantuanToolStripMenuItem_Click(object sender, EventArgs e)
        {
           Form2 frm = new Form2();
           frm.Show();
        }

        private void regresiLinierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            IDictionary<string, double> hasilmodus = new Dictionary<string, double>();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        string labelkolom = Data.variableView[Data.columnChoosen[index]].nama;
                        Console.WriteLine(column);
                        IDictionary<float, int> myDictionary = new Dictionary<float, int>();

                        try
                        {
                            {
                                var sheet = reoGridControl2.CurrentWorksheet;
                                int N = sheet.RowCount; // Variable N integer dalam menghitung banyak baris
                                Console.WriteLine(N); 

                                double[] arrayIsi = new double[N];
                                for (int i = 0; i < N; i++)
                                {
                                    var isi = sheet.GetCell(i, column);
                                    if (isi != null && isi.Data != null)
                                    {
                                        string s = isi.Data.ToString();
                                        double bil;
                                        try
                                        {
                                            bil = double.Parse(s);
                                        }
                                        catch (Exception c)
                                        {
                                            bil = 0;
                                        }
                                        arrayIsi[i] = bil;

                                        /*
                                        for (int ix = 0; ix < Data.variableView[column].missing.Count; ix++)
                                        {
                                            if(bil.ToString() != Data.variableView[column].missing[ix])
                                      

                                        }*/
                                    }
                                }
                                double temp, temp2;
                                IDictionary<double, int> zz = new Dictionary<double, int>();
                                for (int i = 0; i < arrayIsi.Length; i++)
                                {
                                    foreach (var terserah in Data.variableView[column].missing) // Missing Diskritisasi
                                    {

                                        if (terserah != arrayIsi[i].ToString())
                                            if (zz.ContainsKey(arrayIsi[i]))
                                            {
                                                zz[arrayIsi[i]] = zz[arrayIsi[i]] + 1;
                                            }
                                            else
                                            {
                                                zz.Add(arrayIsi[i], 1);
                                            }
                                        //untuk mengeluarkan perhitungan frekuensi dari sebuah bilangan
                                    }

                                    if (Data.variableView[column].missingRange.Count > 0) // Missing dengan Batas Bawah dan Batas Atas
                                    {

                                        double.TryParse(Data.variableView[column].missingRange[0], out temp);
                                        double.TryParse(Data.variableView[column].missingRange[1], out temp2);

                                        if (arrayIsi[i] < temp || arrayIsi[i] > temp2)
                                            if (zz.ContainsKey(arrayIsi[i]))
                                            {
                                                zz[arrayIsi[i]] = zz[arrayIsi[i]] + 1;
                                            }
                                            else
                                            {
                                                zz.Add(arrayIsi[i], 1);
                                            }
                                    }


                                }

                                if (zz.Count == 0)
                                {
                                    for (int i = 0; i < arrayIsi.Length; i++)

                                        if (zz.ContainsKey(arrayIsi[i]))
                                        {
                                            zz[arrayIsi[i]] = zz[arrayIsi[i]] + 1;
                                        }
                                        else
                                        {
                                            zz.Add(arrayIsi[i], 1);
                                        }
                                }
                                zz.Remove(0);

                                //Parallel Disini aja kali ya 
                                //Menghitung Jumlah data dan dibagi 2 di masukan ke dalam thread                                                   

                                double mode = 0;

                                int ma = 0;
                                var sw = Stopwatch.StartNew();
                                foreach (KeyValuePair<double, int> pair in zz)
                                {
                                    if (ma < pair.Value)
                                    {
                                        ma = pair.Value;
                                        mode = pair.Key;
                                    }
                                    //Console.WriteLine("{0}, {1}", pair.Key, pair.Value);

                                    Console.WriteLine("Sekuensial thread {0} nilainya {1}", Thread.CurrentThread.ManagedThreadId, pair);

                                }

                                Console.WriteLine("Nilai Modusnya adalah : {0}", mode);
                                Console.WriteLine("waktu seq: {0}", sw.Elapsed.TotalMilliseconds);
                                Console.WriteLine();

                                hasilmodus.Add(labelkolom, mode);
                                //results.Clear();
                                results.Add(mode);
                              
                                double labelparalel = 0;
                                float modusparalel = 0;
                                sw = Stopwatch.StartNew();
                                Parallel.ForEach(zz, isi =>
                                {
                                    if (modusparalel < isi.Value)
                                    {
                                        modusparalel = isi.Value;
                                        labelparalel = isi.Key;
                                    }
                                    Console.WriteLine("Paralel thread {0} nilainya {1}", Thread.CurrentThread.ManagedThreadId, isi);

                                });
                                Console.WriteLine("hasil modus paralel: {0}", labelparalel);
                                Console.WriteLine("Wktu parael: {0}", sw.Elapsed.TotalMilliseconds);

                            }
                            /*    var sheet = reoGridControl2.CurrentWorksheet;
                                int N = sheet.RowCount;
                                Console.WriteLine(N);
                                float[] arrayIsi = new float[N];
                                for (int i = 0; i < N; i++)
                                {
                                    var isi = sheet.GetCell(i, column);
                                    if (isi != null)
                                    {
                                        string s = isi.Data.ToString();
                                        float bil;
                                        try
                                        {
                                            bil = float.Parse(s);
                                        }
                                        catch (Exception c)
                                        {
                                            bil = 0;
                                        }
                                        arrayIsi[i] = bil;
                                    }
                                }
                                */
                            //IDictionary<double, int> zz = new Dictionary<double, int>();
                            //for(int i = 0; i<arrayIsi.Length; i++) {
                            //    if (zz.ContainsKey(arrayIsi[i]))
                            //    {
                            //        zz[arrayIsi[i]] = zz[arrayIsi[i]] + 1;
                            //    }
                            //    else
                            //    {
                            //        zz.Add(arrayIsi[i], 1);
                            //    }
                            //}
                            //zz.Remove(0);
                            //Parallel Disini aja kali ya 
                            //double mode = 0;
                            //int ma = 0;
                            //foreach (KeyValuePair<double, int> pair in zz)
                            //{
                            //    if (ma < pair.Value)
                            //    {
                            //        ma = pair.Value;
                            //        mode = pair.Key;
                            //    }
                            //    Console.WriteLine("{0}, {1}",
                            //    pair.Key,
                            //    pair.Value);
                            //}
                            //Console.WriteLine(mode);
                            //Console.WriteLine("di atas ini sequential");
                            //mode = zz.GroupBy(value => value.Value).OrderByDescending(group => group.Count()).First().First().Key;
                            //Console.WriteLine(mode);
                            //hasilmodus.Add(labelkolom, mode);
                            //Console.WriteLine("di atas ini");
                            /* 
                             * hapus aja
                            int[] data = new int[10000000];
                            int[] K=new int[2500];
                            IDictionary<string, int> kamus = new Dictionary<string, int>();
                            kamus.Add("1", 13);
                            kamus.Add("2", 99999668);
                            int maxValue = 0;
                            for (int i = 0; i < sheet.RowCount;i++ )
                            {
                                data[i] = Convert.ToInt32(sheet[i,0]);
                            }

                            for (int x = 0; x < data.Length; x++)
                            {
                                //K[data[x]] = K[data[x]] + 1;
                                if(kamus.ContainsKey(data[x].ToString())) {
                                    kamus[data[x].ToString()] = kamus[data[x].ToString()] + 1;
                                    //kamus.Add(data[x].ToString(), kamus[data[x].ToString()] + 1);
                                } else {
                                    //kamus[data[x].ToString()] = 1;
                                    kamus.Add(data[x].ToString(), 1);
                                }
                            }
                            //Console.WriteLine(kamus.ToString());
                            foreach (int z in kamus.Values) {
                                //Console.WriteLine(z);
                            }
                            maxValue = kamus.Values.Max();
                            
                            for (int y = 0; y < data.Length; y++)
                            {
                                if(data[y]>kamus[data[y].ToString()])
                                //(data[y]>K[data[y]])
                                {
                                    maxValue=data[y];
                                    //diubah
                                    //maxValue = kamus[data[y]];  
                                }
                            }
                             
                            
                            Debug.WriteLine(maxValue);
                            */

                            /*  for (int i = 0; i < arrayIsi.Length; i++)
                              {
                                  if (!myDictionary.Keys.Contains(arrayIsi[i]))
                                  {
                                      myDictionary.Add(arrayIsi[i], 1);
                                  }
                                  else
                                      myDictionary[arrayIsi[i]] = myDictionary[arrayIsi[i]] + 1;

                                  Debug.WriteLine(myDictionary[arrayIsi[i]]);

                              }
                              */
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new ResultForm();

                foreach (KeyValuePair<string, double> pair in hasilmodus)
                {
                    Console.WriteLine("Kolom {0} nilai modus {1}",
                    pair.Key,
                    pair.Value);

                }

                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void meanPararelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);

            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            // This 'smart' method will Cudafy all members with the Cudafy attribute in the calling type (i.e. Program)
                            CudafyModule km = CudafyTranslator.Cudafy(eArchitecture.sm_20);
                            // If cudafying will not work for you (CUDA SDK + VS not set up right) then comment out above and
                            // uncomment below. Remember to also comment out the Structs and 3D arrays region below.
                            // CUDA 5.5 SDK must be installed and cl.exe (VC++ compiler) must be in path.
                            //CudafyModule km = CudafyModule.Deserialize(typeof(Program).Name);
                            //var options = NvccCompilerOptions.Createx64(eArchitecture.sm_12);
                            //km.CompilerOptionsList.Add(options);
                            _gpu = CudafyHost.GetDevice(eGPUType.Cuda);
                            _gpu.LoadModule(km);
                            GPGPUProperties gpprop = _gpu.GetDeviceProperties(false);
                            var sheet = reoGridControl2.CurrentWorksheet;
                            // Get the first CUDA device and load our module
                            int N = sheet.RowCount / 2;
                            float[] a = new float[N];
                            float[] b = new float[N];
                            float[] c = new float[N];
                            // fill the arrays 'a' and 'b' on the CPU
                            int jumlahData = 0;
                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                    jumlahData++;
                                }
                                if (sheet[i + N, column] != null && sheet[i + N, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i + N, column].ToString(), out b[i]);
                                    jumlahData++;

                                }
                            }
                            float temp, temp2;
                            int missingCount = 0;

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (a[ax] == temp)
                                    {
                                        a[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (a[ax] >= temp && a[ax] <= temp2)
                                    {
                                        a[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }

                            Debug.WriteLine(missingCount);
                            float meanSequential = 0;
                            for (int i = 0; i < N; i++)
                                meanSequential += a[i] + b[i];
                            meanSequential = meanSequential / (jumlahData - missingCount); ;
                            float[] dev_a = _gpu.CopyToDevice(a);
                            float[] dev_b = _gpu.CopyToDevice(b);
                            float[] dev_c = _gpu.Allocate<float>(c);


                            bool first = true;
                            int N_awal = N;
                            while (N > 1)
                            {
                                if (!first)
                                {
                                    a = new float[N];
                                    b = new float[N];
                                    // c = new int[N];
                                    float[] baru = new float[N];
                                    for (int i = 0; i < (c.Count() - N); i++)
                                        baru[i] = c[N + i];

                                    dev_a = _gpu.CopyToDevice(c.Take(N).ToArray());
                                    dev_b = _gpu.CopyToDevice(baru);
                                    c = new float[N];
                                    dev_c = _gpu.Allocate<float>(c);
                                }

                                float[] d = new float[N];
                                _gpu.CopyFromDevice(dev_a, d);
                                //      _gpu.Launch(N, 1).addVector(dev_a, dev_b, dev_c, N);
                                _gpu.Launch((N + 127) / 128, 128).addVector(dev_a, dev_b, dev_c, N);

                                _gpu.CopyFromDevice(dev_c, c);


                                _gpu.Free(dev_a);
                                _gpu.Free(dev_b);
                                _gpu.Free(dev_c);

                                if (N % 2 == 0)
                                    N = N / 2;
                                else
                                    N = (N + 1) / 2;

                                first = false;
                            }

                            Debug.WriteLine("mean-nya adalah " + (c[0] + c[1]) / (jumlahData - missingCount) + " mean dari sequensial adalah " + meanSequential);
                            results.Add((c[0] + c[1]) / (jumlahData - missingCount));
                            //for (int i = 0; i < N; i++)
                            //    Debug.Assert(a[i] + b[i] == c[i]);
                            _gpu.FreeAll();

                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormResultsMean();
                dialog = dialogResult.ShowDialog();


                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        void sheet_CellEditTextChanging(object sender, CellEditTextChangingEventArgs e)
        {
            var sheet = reoGridControl2.CurrentWorksheet;
            textBox1.Text = sheet[sheet.FocusPos].ToString();
        }

        //MANGATSS MAAAAMMMMM 


        int[] pos = new int[2];
        bool onFocusChanged = true;
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var sheet = reoGridControl2.CurrentWorksheet;



            //     sheet_FocusPosChanged(this, null);

            //pos[0] = sheet.FocusPos.Row;
            //pos[1] = sheet.FocusPos.Col;

            //   if(true)
            if (onTextboxFocus)
                sheet[sheet.FocusPos] = textBox1.Text;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            //          textBox1.TextChanged -= textBox1_TextChanged;
        }

        private void textBox1_Enter(object sender, EventArgs e)
        {
            onTextboxFocus = true;
            textBox1.TextChanged += textBox1_TextChanged;
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            onTextboxFocus = false;
            textBox1.TextChanged -= textBox1_TextChanged;
        }

        private void othersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        public int jumlahdata(int column, int N)
        {
            var sheet = reoGridControl2.CurrentWorksheet;
            int jumlahData = 0;
            for (int i = 0; i < N; i++)
            {
                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                {
                    jumlahData++;
                }
                if (sheet[i + N, column] != null && sheet[i + N, column].ToString() != "")
                {
                    jumlahData++;
                }
            }
            return jumlahData;
        }

        public int jumlahdata2(int column)
        {
            var sheet = reoGridControl2.CurrentWorksheet;
            int N = new int();
            if (sheet.RowCount > 200)
            {
                N = sheet.RowCount;
            }
            else
            {
                N = 0;
                for (int i = 0; i < sheet.RowCount; i++)
                {
                    if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                    {
                        N++;
                    }
                }
            }
            return N;
        }

        public int jumlahthread(int column)
        {
            var sheet = reoGridControl2.CurrentWorksheet;
            int N = new int();
            if (sheet.RowCount > 200)
            {
                N = sheet.RowCount;
            }
            else
            {
                N = 0;
                for (int i = 0; i < sheet.RowCount; i++)
                {
                    if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                    {
                        N++;
                    }
                }
            }
            if (N % 2 == 0)
            {
                N /= 2;
            }
            else
            {
                N = N / 2 + 1;
            }
            return N;
        }

        public float[] InitData(int choice, int column, int N, float[] a, float[] b)
        {
            for (int i = 0; i < N; i++)
            {
                var sheet = reoGridControl2.CurrentWorksheet;

                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                {
                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                }
                if (sheet[i + N, column] != null && sheet[i + N, column].ToString() != "")
                {
                    float.TryParse(sheet[i + N, column].ToString(), out b[i]);

                }
            }
            if (choice == 1)
            {
                return a;
            }
            else
            {
                return b;
            }
        }

        public float[] InitData2(int column, int N, float[] a)
        {
            for (int i = 0; i < N; i++)
            {
                var sheet = reoGridControl2.CurrentWorksheet;

                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                {
                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                }
            }
            return a;
        }

        public void merge(float[] a, int low, int high, int mid)
        {
            int i, j, k;
            float[] c = new float[a.Count()];
            i = low;
            k = low;
            j = mid + 1;
            while (i <= mid && j <= high)
            {
                if (a[i] < a[j])
                {
                    c[k] = a[i];
                    k++; i++;
                }
                else
                {
                    c[k] = a[j];
                    k++;
                    j++;
                }
            }
            while (i <= mid)
            {
                c[k] = a[i];
                k++; i++;
            }
            while (j <= high)
            {
                c[k] = a[j];
                k++; j++;
            }
            for (i = low; i < k; i++)
            {
                a[i] = c[i];
            }
        }

        public void mergesort(float[] a, int low, int high)
        {
            int mid;
            if (low < high)
            {
                mid = (low + high) / 2;
                mergesort(a, low, mid);
                mergesort(a, mid + 1, high);
                merge(a, low, high, mid);
            }
        }


        public float Median(int n, float[] arrayData)
        {
            mergesort(arrayData, 0, n - 1);
            float median;
            if (n % 2 != 0)
            {
                median = arrayData[(n) / 2];

            }
            else
            {
                median = (arrayData[(n - 1) / 2] + arrayData[((n - 1) / 2) + 1]) / 2;
            }
            return median;
        }

        public float modus(int n, float[] arrayData)
        {
            mergesort(arrayData, 0, n - 1);
            int counter = 1;
            int max = 0;
            float mode = arrayData[0];
            for (int i = 0; i < n - 1; i++)
            {
                if (arrayData[i] == arrayData[i + 1])
                {
                    counter++;
                    if (counter > max)
                    {
                        max = counter;
                        mode = arrayData[i];
                    }
                }
                else
                {
                    counter = 1;
                }
            }
            return mode;
        }

        public float Range(int n, float[] arrayData)
        {
            mergesort(arrayData, 0, n - 1);
            float range = arrayData[n - 1] - arrayData[0];
            return range;
        }

        public float MaximumValue(int n, float[] arrayData)
        {
            mergesort(arrayData, 0, n - 1);
            float maximumValue = arrayData[n-1];
            return maximumValue;
        }

        public float MinimumValue(int n, float[] arrayData)
        {
            mergesort(arrayData, 0, n - 1);
            float minimumValue = arrayData[0];
            return minimumValue;
        }

        private void rangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Range";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];

                            a = InitData2(column, N, a);

                            float temp, temp2;
                            int missingCount = 0;

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (a[ax] == temp)
                                    {
                                        a[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (a[ax] >= temp && a[ax] <= temp2)
                                    {
                                        a[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }

                            Debug.WriteLine(missingCount);
                            float max = a[0];
                            float min = a[0];
                            for (int i = 1; i < N; i++)
                            {
                                if (a[i] > max)
                                {
                                    max = a[i];
                                }
                                if (a[i] < min)
                                {
                                    min = a[i];
                                }
                            }
                            float range = Range(N, a);
                            results.Add(range);
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void standarDeviationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Stdev";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }

                            var statistic = new DescriptiveStatistics(b);
                            var maximum1 = (float)statistic.StandardDeviation;
                            results.Add(maximum1);
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Range";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }

                            var statistic = new DescriptiveStatistics(b);
                            var minimum1 = (float)statistic.Minimum;
                            results.Add(minimum1);
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        
        }

        private void maxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Maximum";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }

                            var statistic = new DescriptiveStatistics(b);
                            var maximum1 = (float)statistic.Maximum;
                           
                            results.Add(maximum1);
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        public void meanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Mean";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }
                         
                            var statistic = new DescriptiveStatistics(b);
                            var mean1 = (float)statistic.Mean;
                            results.Add(mean1);
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void skewnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Skewness";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }

                            var statistic = new DescriptiveStatistics(b);
                            var skwn = (float)statistic.Skewness;
                            results.Add(skwn);
                            
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void varianceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Variansi";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            var statistic = new DescriptiveStatistics(b);
                            var variansi = (float)statistic.Variance;
                            results.Add(variansi);
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void kurtosisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Kurtosis";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }

                            var statistic = new DescriptiveStatistics(b);
                            var kurtosis = (float)statistic.Kurtosis;
                            results.Add(kurtosis);
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void countToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Count";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }

                            var statistic = new DescriptiveStatistics(b);
                            var Counting = (float)statistic.Count;
                            results.Add(Counting);
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void covarinceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Entropy";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }
                            var entropi = (double)Statistics.Entropy(b);
                            results.Add(entropi);
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void mediumToolStripMenuItem_Click(object sender, EventArgs e)
        {
          
        }

        private void mediumToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Median";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }
                            var minimum1 = (double)Statistics.Median(b);
                            results.Add(minimum1);

                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void populationKurtosisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "PopulasiKurtosis";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }
                            var minimum1 = (double)Statistics.PopulationKurtosis(b);
                            results.Add(minimum1);

                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void populationSkewnessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "PopulasiSkewness";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }
                            var minimum1 = (double)Statistics.PopulationSkewness(b);
                            results.Add(minimum1);

                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void populationStandarDeviationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "PopulasiStdev";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }
                            var minimum1 = (double)Statistics.PopulationStandardDeviation(b);
                            results.Add(minimum1);

                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void populationVarianceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "PopulasiVariansi";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];
                            double[] b = new double[N];

                            float temp, temp2;
                            int missingCount = 0;

                            for (int i = 0; i < N; i++)
                            {
                                if (sheet[i, column] != null && sheet[i, column].ToString() != "")
                                {
                                    float.TryParse(sheet[i, column].ToString(), out a[i]);
                                }
                            }

                            for (int i = 0; i < N; i++)
                            {
                                b[i] = System.Convert.ToDouble(a[i]);
                            }

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (b[ax] == temp)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (b[ax] >= temp && b[ax] <= temp2)
                                    {
                                        b[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }
                            var minimum1 = (double)Statistics.PopulationVariance(b);
                            results.Add(minimum1);

                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void populationSkewnessKurtosisToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void maximumSekuensialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Range";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];

                            a = InitData2(column, N, a);

                            float temp, temp2;
                            int missingCount = 0;

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (a[ax] == temp)
                                    {
                                        a[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (a[ax] >= temp && a[ax] <= temp2)
                                    {
                                        a[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }

                            Debug.WriteLine(missingCount);
                            float max = a[0];
                            float min = a[0];
                            for (int i = 1; i < N; i++)
                            {
                                if (a[i] > max)
                                {
                                    max = a[i];
                                }
                            }
                            float max1 = MaximumValue(N, a);
                            results.Add(max1);
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }

        private void minimumSekuensialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            results.Clear();
            DialogResult dr = new DialogResult();
            Form dlg1 = new AnalyzeForm();
            dr = dlg1.ShowDialog();
            for (int ix = 0; ix < Data.columnChoosen.Length; ix++)
                if (Data.columnChoosen[ix] != -1)
                    columnChoosen.Add(Data.variableView[Data.columnChoosen[ix]].nama);
            judul = "Range";
            if (dr == DialogResult.OK)
            {
                for (int index = 0; index < Data.columnChoosen.Length; index++)
                    if (Data.columnChoosen[index] != -1)
                    {
                        int column = Data.columnChoosen[index];
                        try
                        {
                            var sheet = reoGridControl2.CurrentWorksheet;
                            int N = jumlahdata2(column);

                            float[] a = new float[N];

                            a = InitData2(column, N, a);

                            float temp, temp2;
                            int missingCount = 0;

                            for (int bx = 0; bx < Data.variableView[column].missing.Count; bx++)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missing[bx], out temp);
                                    if (a[ax] == temp)
                                    {
                                        a[ax] = 0;
                                        missingCount++;
                                    }
                                }
                            }

                            if (Data.variableView[column].missingRange.Count > 1)
                            {
                                for (int ax = 0; ax < N; ax++)
                                {
                                    float.TryParse(Data.variableView[column].missingRange[0], out temp);
                                    float.TryParse(Data.variableView[column].missingRange[1], out temp2);
                                    if (a[ax] >= temp && a[ax] <= temp2)
                                    {
                                        a[ax] = 0;
                                        missingCount++;
                                    }

                                }
                            }

                            Debug.WriteLine(missingCount);
                            float max = a[0];
                            float min = a[0];
                            for (int i = 1; i < N; i++)
                            {
                                if (a[i] < min)
                                {
                                    min = a[i];
                                }
                            }
                            float min1 = MinimumValue(N, a);
                            results.Add(min1);
                        }
                        catch (CudafyLanguageException cle)
                        {
                        }
                        catch (CudafyCompileException cce)
                        {
                        }
                        catch (CudafyHostException che)
                        {
                            Console.Write(che.Message);
                        }
                    }

                DialogResult dialog = new DialogResult();
                Form dialogResult = new FormHasilMathDotNet();
                dialog = dialogResult.ShowDialog();

                //  Console.ReadLine();
            }
            else
                dlg1.Close();
        }
    }
}
