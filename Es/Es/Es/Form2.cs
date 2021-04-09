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
    public partial class Form2 : Form
    {
        public string firstname;
        public List<string> firstvalues;
        public Domain currentDom;
        public Form2()
        {
            InitializeComponent();
            firstname = "New domain";
            firstvalues = new List<string>();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            List<string> values = new List<string>();
            foreach(var el in listBox1.Items)
            {
                values.Add(Convert.ToString(el));
            }
            currentDom = new Domain(textBox1.Text, values);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            listBox1.Items.Add(textBox2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            listBox1.SelectedItem = textBox2.Text;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listBox1.Items.Remove(listBox1.SelectedItem);
        }

        private void Form2_Shown(object sender, EventArgs e)
        {
            textBox1.Text = firstname;
            int len = firstvalues.Count;
            if(len>0)
            {
               for(int i=0;i<len;i++)
               {
                    listBox1.Items.Add(firstvalues[i]);
               }
            }
        }
    }
}
