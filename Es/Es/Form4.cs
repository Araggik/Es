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
    public partial class Form4 : Form
    {
        public List<Variable> variables;
        public Rule rule;
        public Form4()
        {
            InitializeComponent();
            textBox1.Text = "NewRule";
            rule = new Rule();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            rule.name = textBox1.Text;
            rule.description = textBox2.Text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.variables = variables;
            var result = form7.ShowDialog();
            if(result == DialogResult.OK)
            {
                rule.prem.Add(form7.fact);
                string str = "";
                str = form7.fact.variable.name;
                str = str + " = ";
                str = str + form7.fact.value;
                listBox1.Items.Add(str);
            }
            form7.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var selectitm = listBox1.SelectedItem;
            if(selectitm!=null)
            {
                string name = selectitm.ToString().Split(' ')[0];
                int len = rule.prem.Count;
                bool notfind = true;
                for(int i=0;i<len&&notfind;i++)
                {
                    if(rule.prem[i].variable.name ==name)
                    {
                        notfind = false;
                        rule.prem.RemoveAt(i);
                        listBox1.Items.Remove(selectitm);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var selectitm = listBox1.SelectedItem;
            if (selectitm!=null)
            {               
                Form7 form7 = new Form7();
                form7.variables = variables;
                var result = form7.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //Добавление
                    rule.prem.Add(form7.fact);
                    string str = "";
                    str = form7.fact.variable.name;
                    str = str + " = ";
                    str = str + form7.fact.value;
                    listBox1.Items.Add(str);
                    //Удаление
                    string name = selectitm.ToString().Split(' ')[0];
                    int len = rule.prem.Count;
                    bool notfind = true;
                    for (int i = 0; i < len && notfind; i++)
                    {
                        if (rule.prem[i].variable.name == name)
                        {
                            notfind = false;
                            rule.prem.RemoveAt(i);
                            listBox1.Items.Remove(selectitm);
                        }
                    }
                }
                form7.Close();                           
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Form7 form7 = new Form7();
            form7.variables = variables.FindAll(
                delegate (Variable v)
                {
                    return v.type == 2 || v.type == 3;
                }
            ); ;
            var result = form7.ShowDialog();
            if (result == DialogResult.OK)
            {
                rule.concl.Add(form7.fact);
                string str = "";
                str = form7.fact.variable.name;
                str = str + " = ";
                str = str + form7.fact.value;
                listBox2.Items.Add(str);
            }
            form7.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var selectitm = listBox2.SelectedItem;
            if (selectitm != null)
            {
                string name = selectitm.ToString().Split(' ')[0];
                int len = rule.concl.Count;
                bool notfind = true;
                for (int i = 0; i < len && notfind; i++)
                {
                    if (rule.concl[i].variable.name == name)
                    {
                        notfind = false;
                        rule.concl.RemoveAt(i);
                        listBox2.Items.Remove(selectitm);
                    }
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var selectitm = listBox2.SelectedItem;
            if (selectitm != null)
            {
                Form7 form7 = new Form7();
                form7.variables = variables.FindAll(
                    delegate (Variable v)
                    {
                      return v.type == 2 || v.type == 3;
                    }
                ); 
                var result = form7.ShowDialog();
                if (result == DialogResult.OK)
                {
                    //Добавление
                    rule.concl.Add(form7.fact);
                    string str = "";
                    str = form7.fact.variable.name;
                    str = str + " = ";
                    str = str + form7.fact.value;
                    listBox2.Items.Add(str);
                    //Удаление
                    string name = selectitm.ToString().Split(' ')[0];
                    int len = rule.concl.Count;
                    bool notfind = true;
                    for (int i = 0; i < len && notfind; i++)
                    {
                        if (rule.concl[i].variable.name == name)
                        {
                            notfind = false;
                            rule.concl.RemoveAt(i);
                            listBox2.Items.Remove(selectitm);
                        }
                    }
                }
                form7.Close();
            }
        }
    }
}
