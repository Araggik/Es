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
    public partial class Form8 : Form
    {
        public List<string> choices;

        public string choice;

        public string q;

        public Form8()
        {
            InitializeComponent();
            choices = new List<string>();
            choice = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            choice = comboBox1.SelectedItem.ToString();
            this.DialogResult = DialogResult.OK;
            
        }

        private void Form8_Shown(object sender, EventArgs e)
        {
            foreach(string a in choices)
            {
                comboBox1.Items.Add(a);
            }
            comboBox1.SelectedIndex = 0;
            label1.Text = q;
        }
    }
}
