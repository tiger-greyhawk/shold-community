using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Shold_Community
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Form1.addPatternObj = null;
            //Form1.addPatternObj.typeCastle = comboBox1.Text;
            comboBox1.Visible = false;
            button1.Visible = true;
            textBox1.Visible = true;
            textBox1.Focus();
            textBox1.SelectAll();
            //this.DialogResult = DialogResult.OK;
            //this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Form1.addPatternObj.name = textBox1.Text;
        }

        
    }
}
