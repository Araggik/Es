using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;

namespace Es
{
    public partial class Form1 : Form
    {
        public ExpertSys currentEs;
        public Form1()
        {
            InitializeComponent();
            currentEs = new ExpertSys();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button8_Click(object sender, EventArgs e)
        {           
            var selectitm = listView3.SelectedItems;
            if (selectitm.Count > 0)
            {
                Form2 form2 = new Form2();
                string name = Convert.ToString(selectitm[0].Text);
                int len = currentEs.domains.Count;              
                bool notfind = true;
                int index = -1;
                for (int i = 0; i < len && notfind; i++)
                {
                    if (currentEs.domains[i].name == name)
                    {
                        notfind = false;
                        form2.firstname = name;
                        form2.firstvalues = currentEs.domains[i].values;
                        index = i;
                    }
                }                              
                var result = form2.ShowDialog();
                if (result == DialogResult.OK)
                {
                    currentEs.domains.RemoveAt(index);
                    listView3.Items.Remove(selectitm[0]);
                    currentEs.domains.Add(form2.currentDom);
                    listView3.Items.Add(form2.currentDom.name);
                    form2.Close();
                }
            }            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            var result = form2.ShowDialog();
            if (result == DialogResult.OK)
            {
                currentEs.domains.Add(form2.currentDom);
                listView3.Items.Add(form2.currentDom.name);
                form2.Close();
            }            
        }

        private void listView3_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox3.Items.Clear();
            var selectitm = listView3.SelectedItems;
            if (selectitm.Count > 0)
            {
                string name = Convert.ToString(selectitm[0].Text);
                List<string> values;
                values = new List<string>();
                bool notfind = true;
                int n = currentEs.domains.Count;
                for (int i = 0; i < n && notfind; i++)
                {
                    var el = currentEs.domains[i];
                    if (el.name == name)
                    {
                        values = el.values;
                        notfind = false;
                    }
                }
                int countval = values.Count;
                for (int i = 0; i < countval; i++)
                {
                    listBox3.Items.Add(values[i]);
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.listdom = currentEs.domains;
            var result = form3.ShowDialog();
            if(result == DialogResult.OK)
            {
                currentEs.variables.Add(form3.currentVar);
                //Массив строк
                string namevar = form3.currentVar.name;
                string domvar = form3.currentVar.domain.name;
                string typevar = "";
                if(form3.currentVar.type == 1)
                {
                    typevar = "Запрашиваемая";
                }
                else if(form3.currentVar.type ==2)
                {
                    typevar = "Выводимая";
                }
                else if(form3.currentVar.type == 3)
                {
                    typevar = "Выводимо-запрашиваемая";
                }
                ListViewItem item = new ListViewItem(new string[] {namevar,typevar,domvar});
                listView2.Items.Add(item);
                form3.Close();
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            var selectitm = listView3.SelectedItems;
            if (selectitm.Count > 0)
            {
                string name = Convert.ToString(selectitm[0].Text);
                int len = currentEs.domains.Count;
                bool notfind = true;
                for(int i=0;i<len&&notfind;i++)
                {
                    if(currentEs.domains[i].name ==name)
                    {
                        notfind = false;
                        currentEs.domains.RemoveAt(i);
                    }
                }
                listView3.Items.Remove(selectitm[0]);
            }
        }

        private void listView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectitm = listView2.SelectedItems;
            if(selectitm.Count>0)
            {
                string quest = "";
                string name = selectitm[0].Text;
                int len = currentEs.variables.Count;
                bool notfind = true;
                for(int i=0;i<len&&notfind;i++)
                {
                    if(currentEs.variables[i].name == name)
                    {
                        notfind = false;
                        quest = currentEs.variables[i].quest;
                    }
                }
                label2.Text = quest;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            var selectitm = listView2.SelectedItems;
            if (selectitm.Count > 0)
            {
                string name = selectitm[0].Text;
                int len = currentEs.variables.Count;
                bool notfind = true;
                for (int i = 0; i < len && notfind; i++)
                {
                    if (currentEs.variables[i].name == name)
                    {
                        notfind = false;
                        currentEs.variables.RemoveAt(i);
                    }
                }
                listView2.Items.Remove(selectitm[0]);
            }
        }

        private void listView2_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listView2_DragDrop(object sender, DragEventArgs e)
        {
            if(listView2.SelectedItems.Count>0)
            {
                Point pt = listView2.PointToClient(new Point(e.X, e.Y));
                ListViewItem dragitem = listView2.GetItemAt(pt.X, pt.Y);
                var item = listView2.SelectedItems[0];
                if (dragitem == null) 
                    return;
                int dragindex = dragitem.Index;
                int itemindex = item.Index;
                var dragvar = currentEs.variables[itemindex];
                listView2.Items.Remove(item);
                listView2.Items.Insert(dragindex, item);
                currentEs.variables.Remove(dragvar);
                currentEs.variables.Insert(dragindex, dragvar);
            }
        }

        private void listView2_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView2.DoDragDrop(listView2.SelectedItems, DragDropEffects.Move);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var selectitm = listView2.SelectedItems;
            if (selectitm.Count > 0)
            {
                string name = selectitm[0].Text;
                int len = currentEs.variables.Count;
                bool notfind = true;
                Variable selectvar = null;
                for (int i = 0; i < len && notfind; i++)
                {
                    if (currentEs.variables[i].name == name)
                    {
                        notfind = false;
                        selectvar = currentEs.variables[i];
                    }
                }
                Form3 form3 = new Form3();
                form3.listdom = currentEs.domains;
                var result = form3.ShowDialog();

                if(result == DialogResult.OK)
                {
                    currentEs.variables.Remove(selectvar);
                    listView2.Items.Remove(selectitm[0]);
                    currentEs.variables.Add(form3.currentVar);
                    //Массив строк
                    string namevar = form3.currentVar.name;
                    string domvar = form3.currentVar.domain.name;
                    string typevar = "";
                    if (form3.currentVar.type == 1)
                    {
                        typevar = "Запрашиваемая";
                    }
                    else if (form3.currentVar.type == 2)
                    {
                        typevar = "Выводимая";
                    }
                    else if (form3.currentVar.type == 3)
                    {
                        typevar = "Выводимо-запращиваемая";
                    }
                    ListViewItem item = new ListViewItem(new string[] { namevar, typevar, domvar });
                    listView2.Items.Add(item);                    
                }
                form3.Close();
            }
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                Point pt = listView1.PointToClient(new Point(e.X, e.Y));
                ListViewItem dragitem = listView1.GetItemAt(pt.X, pt.Y);
                var item = listView1.SelectedItems[0];
                if (dragitem == null)
                    return;
                int dragindex = dragitem.Index;
                int itemindex = item.Index;
                var dragrul = currentEs.rules[itemindex];
                listView1.Items.Remove(item);
                listView1.Items.Insert(dragindex, item);
                currentEs.rules.Remove(dragrul);
                currentEs.rules.Insert(dragindex, dragrul);
            }
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            listView1.DoDragDrop(listView1.SelectedItems, DragDropEffects.Move);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.variables = currentEs.variables;
            var result = form4.ShowDialog();
            if(result == DialogResult.OK)
            {
                Rule newrule = form4.rule;
                currentEs.rules.Add(newrule);
                string str = "ЕСЛИ";
                int lenp = newrule.prem.Count;
                for(int i=0;i<lenp-1;i++)
                {
                    string varname = newrule.prem[i].variable.name;
                    string varvalue = newrule.prem[i].value;
                    str = str + " " + varname + "=" + varvalue + " И";
                }
                str = str + " " + newrule.prem[lenp - 1].variable.name + "=" + newrule.prem[lenp - 1].value + " ТО";
                int lenc = newrule.concl.Count;
                for (int i = 0; i < lenc - 1; i++)
                {
                    string varname = newrule.concl[i].variable.name;
                    string varvalue = newrule.concl[i].value;
                    str = str + " " + varname + "=" + varvalue + " И";
                }
                str = str + " " + newrule.concl[lenc - 1].variable.name + "=" + newrule.concl[lenc - 1].value;
                ListViewItem newitem = new ListViewItem(new string[] { newrule.name, str });
                listView1.Items.Add(newitem);
            }
            form4.Close();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var selectitm = listView1.SelectedItems;
            if(selectitm.Count>0)
            {
                string rulename = selectitm[0].Text;
                bool notfind = true;
                int len = currentEs.rules.Count;
                for(int i=0;i<len&&notfind;i++)
                {
                    if(currentEs.rules[i].name == rulename)
                    {
                        notfind = false;
                        currentEs.rules.RemoveAt(i);
                        listView1.Items.Remove(selectitm[i]);
                    }
                }
            }
        }

        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            currentEs.rules.Clear();
            currentEs.variables.Clear();
            currentEs.domains.Clear();
            listView1.Items.Clear();
            listView2.Items.Clear();
            listView3.Items.Clear();
        }

        private void открытьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filedg = new OpenFileDialog();
            filedg.DefaultExt = ".json";
            var result = filedg.ShowDialog();
            if(result == DialogResult.OK)
            {
                var path = filedg.FileName;
                currentEs = JsonConvert.DeserializeObject<ExpertSys>(File.ReadAllText(path));

                listView1.Items.Clear();
                listView2.Items.Clear();
                listView3.Items.Clear();


                foreach(var newrule in currentEs.rules)
                {
                    string str = "ЕСЛИ";
                    int lenp = newrule.prem.Count;
                    for (int i = 0; i < lenp - 1; i++)
                    {
                        string varname = newrule.prem[i].variable.name;
                        string varvalue = newrule.prem[i].value;
                        str = str + " " + varname + "=" + varvalue + " И";
                    }
                    str = str + " " + newrule.prem[lenp - 1].variable.name + "=" + newrule.prem[lenp - 1].value + " ТО";
                    int lenc = newrule.concl.Count;
                    for (int i = 0; i < lenc - 1; i++)
                    {
                        string varname = newrule.concl[i].variable.name;
                        string varvalue = newrule.concl[i].value;
                        str = str + " " + varname + "=" + varvalue + " И";
                    }
                    str = str + " " + newrule.concl[lenc - 1].variable.name + "=" + newrule.concl[lenc - 1].value;
                    ListViewItem newitem = new ListViewItem(new string[] { newrule.name, str });
                    listView1.Items.Add(newitem);
                }

                foreach (var el in currentEs.variables)
                {
                    string namevar = el.name;
                    string domvar = el.domain.name;
                    string typevar = "";
                    if (el.type == 1)
                    {
                        typevar = "Запрашиваемая";
                    }
                    else if (el.type == 2)
                    {
                        typevar = "Выводимая";
                    }
                    else if (el.type == 3)
                    {
                        typevar = "Выводимо-запрашиваемая";
                    }
                    ListViewItem item = new ListViewItem(new string[] { namevar, typevar, domvar });
                    listView2.Items.Add(item);
                }

                foreach (var el in currentEs.domains)
                {
                    listView3.Items.Add(el.name);
                }
            }
        }

        private void сохранитьКакToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var filedg = new SaveFileDialog();
            filedg.DefaultExt = ".json";
            filedg.FileName = "NewExpertSystem";
            var result = filedg.ShowDialog();
            if(result == DialogResult.OK)
            {
                var path = filedg.FileName;
                File.WriteAllText(path, JsonConvert.SerializeObject(currentEs));
            }
        }

        private void консультацияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Es = currentEs;
            var result = form5.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
