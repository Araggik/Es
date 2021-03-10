using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Es
{
    public partial class Form3 : Form
    {
        public Variable currentVar;
        public List<Domain> listdom;
        public Form3()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            int type = 1;
            if(radioButton2.Checked)
            {
                type = 2;
            }
            else if(radioButton3.Checked)
            {
                type = 3;
            }
            currentVar = new Variable(textBox1.Text, listdom[comboBox1.SelectedIndex], type, textBox2.Text);
        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            int len = listdom.Count;
            for (int i = 0; i < len; i++)
            {
                comboBox1.Items.Add(listdom[i].name);
            }
            comboBox1.SelectedIndex = 0;
            radioButton1.Checked = true;
            textBox1.Text = "NewVariable";
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
