using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormInput
{
    public partial class GetInputString : Form
    {
        public GetInputString()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Globals.ThisWorkbook.WriteStringToCell(this.textBox1.Text);
            this.Dispose();
        }
    }
}
