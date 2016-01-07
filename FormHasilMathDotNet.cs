using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ParallelSPSS
{
    public partial class FormHasilMathDotNet : Form
    {
        public FormHasilMathDotNet()
        {
            InitializeComponent();
        }

        private void FormHasilMathDotNet_Load(object sender, EventArgs e)
        {
            resultTable.Controls.Clear();
            for (int i = 0; i < Form1.results.Count; i++)
            {
                //Debug.Write(Form1.columnChoosen[i]);
                resultTable.RowCount = resultTable.RowCount + 1;
                resultTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 50F));
                resultTable.Controls.Add(new Label() { Text = Form1.columnChoosen[i], Anchor = AnchorStyles.Left, AutoSize = true }, 0, resultTable.RowCount - 1);
                resultTable.Controls.Add(new Label() { Text = Form1.results[i].ToString(), Anchor = AnchorStyles.Left, AutoSize = true }, 1, resultTable.RowCount - 1);
            }
        }
    }
}
