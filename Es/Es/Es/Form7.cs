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
    public partial class Form7 : Form
    {
        public List<Variable> variables;
        public Fact fact;
        public Form7()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            string value = comboBox2.Text;
            string name = comboBox1.Text;
            bool notfind = true;
            int len = variables.Count;
            Variable currentvar = null;
            for(int i=0;i<len&&notfind;i++)
            {
                if(variables[i].name == name)
                {
                    notfind = false;
                    currentvar = variables[i];
                }
            }
            fact = new Fact(currentvar, value);
        }

        private void Form7_Shown(object sender, EventArgs e)
        {
            int len = variables.Count;
            for(int i=0;i<len;i++)
            {
                comboBox1.Items.Add(variables[i].name);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            bool notfind = true;
            int len = variables.Count;
            string name = comboBox1.Text;
            Variable selectvar = null;
            for(int i=0;i<len&&notfind;i++)
            {
                if(variables[i].name == name)
                {
                    notfind = false;
                    selectvar = variables[i];
                }
            }
            int len2 = selectvar.domain.values.Count;
            for(int i=0;i<len2;i++)
            {
                comboBox2.Items.Add(selectvar.domain.values[i]);
            }
            comboBox2.SelectedIndex = 0;
        }
    }
}
