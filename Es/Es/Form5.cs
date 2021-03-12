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
    public partial class Form5 : Form
    {
        public ExpertSys Es;

        public bool process;

        struct SmartFact
        {
            public Fact fact;
            public int iparent;
            public List<int> kids;
            public int irule;

            public SmartFact(Fact f, int ip, List<int> k, int ir)
            {
                this.fact = f;
                this.iparent = ip;
                this.kids = k;
                this.irule = ir;
            }
        }

        public void Conclusion()
        {
            string name_target = comboBox1.Text;
            //comboBox1.Enabled = false;
            int len = Es.variables.Count;
            bool notfind_target = true;
            Variable target;
            int curvar = 0;
            while(notfind_target)
            {
                if(name_target==Es.variables[curvar].name)
                {
                    notfind_target = false;
                    target = Es.variables[curvar];
                }
                curvar++;
            }
            List<SmartFact> checked_facts = new List<SmartFact>();
            List<int> used_rules = new List<int>();
            Stack<Fact> unchecked_facts= new Stack<Fact>();
            bool notvalue = true;
            bool notall = true;
            bool notstop = true;
            int currule = 0;
            int rule_target = 0;
            int lastrule = Es.rules.Count - 1;
            while(notvalue && notall && notstop)
            {
                int id_used_fact_target =Es.rules[rule_target].concl.FindIndex(
                    delegate (Fact x)
                    { 
                        return x.variable.name == name_target; 
                    });
                if(id_used_fact_target !=-1)
                {
                    used_rules.Add(rule_target);
                    int count_prem = Es.rules[rule_target].prem.Count;
                    for(int i=count_prem-1;i>=0;i--)
                    {
                        unchecked_facts.Push(Es.rules[rule_target].prem[i]);
                    }
                    bool notcheck_all = true;
                    int del_rule = -1;
                    while (notcheck_all && unchecked_facts.Count!=0 && notstop)
                    {
                        Fact curused_fact = unchecked_facts.Peek();
                        if (curused_fact.variable.type == 1)
                        {
                            Form8 form8 = new Form8();
                            foreach (string a in curused_fact.variable.domain.values)
                            {
                                form8.choices.Add(a);
                            }
                            var res = form8.ShowDialog();
                            if (res == DialogResult.OK)
                            {
                                string choice = form8.choice;
                                if (curused_fact.value != choice)
                                {
                                    del_rule = used_rules[used_rules.Count - 1];
                                    int count_prem_facts = Es.rules[del_rule].prem.Count;
                                    int c = checked_facts.Count;
                                    int count_checked_prem_facts = 0;
                                    for(int i=0;i<c;i++)
                                    {
                                        if(checked_facts[i].iparent==del_rule)
                                        {
                                            count_checked_prem_facts++;
                                        }
                                    }
                                    int del_uncheck_facts = count_prem_facts - count_checked_prem_facts -1;
                                    for(int i=0;i<del_uncheck_facts;i++)
                                    {
                                        unchecked_facts.Pop();
                                    }
                                    used_rules.RemoveAt(used_rules.Count - 1);
                                }
                                List<int> lkids = new List<int>();
                                SmartFact newfact = new SmartFact(curused_fact, used_rules[used_rules.Count - 1], lkids, -1);
                                checked_facts.Add(newfact);
                                unchecked_facts.Pop();
                            }
                            else
                            {
                                notstop = false;
                            }
                            form8.Close();
                        }
                        else
                        {
                            bool notnext = true;
                            currule = del_rule+1;
                            if (currule != 0) del_rule = -1;
                            while (currule<=lastrule&&notnext)
                            {
                                if(!used_rules.Contains(currule)&&Es.rules[currule].concl.Contains(curused_fact))
                                {
                                    used_rules.Add(currule);
                                    int count_prem_facts = Es.rules[currule].prem.Count;
                                    for (int i = count_prem_facts - 1; i >= 0; i--)
                                    {
                                        unchecked_facts.Push(Es.rules[rule_target].prem[i]);
                                    }
                                    notnext = false;
                                }
                                currule++;
                            }
                            if(currule>lastrule)
                            {
                                if(curused_fact.variable.type == 3)
                                {
                                    Form8 form8 = new Form8();
                                    foreach (string a in curused_fact.variable.domain.values)
                                    {
                                        form8.choices.Add(a);
                                    }
                                    var res = form8.ShowDialog();
                                    if (res == DialogResult.OK)
                                    {
                                        string choice = form8.choice;
                                        if (curused_fact.value != choice)
                                        {
                                            del_rule = used_rules[used_rules.Count - 1];
                                            int count_prem_facts = Es.rules[del_rule].prem.Count;
                                            int c = checked_facts.Count;
                                            int count_checked_prem_facts = 0;
                                            for (int i = 0; i < c; i++)
                                            {
                                                if (checked_facts[i].iparent == del_rule)
                                                {
                                                    count_checked_prem_facts++;
                                                }
                                            }
                                            int del_uncheck_facts = count_prem_facts - count_checked_prem_facts - 1;
                                            for (int i = 0; i < del_uncheck_facts; i++)
                                            {
                                                unchecked_facts.Pop();
                                            }
                                            used_rules.RemoveAt(used_rules.Count - 1);
                                        }
                                        List<int> lkids = new List<int>();
                                        SmartFact newfact = new SmartFact(curused_fact, used_rules[used_rules.Count - 1], lkids, -1);
                                        checked_facts.Add(newfact);
                                        unchecked_facts.Pop();
                                    }
                                    else
                                    {
                                        notstop = false;
                                    }
                                    form8.Close();
                                }
                            }
                        }
                    }
                }
                rule_target++;
                if(rule_target>lastrule)
                {
                    notall = false;
                }
            }
        }

        public Form5()
        {
            InitializeComponent();
            Es = null;
            button2.Visible = false;
            button3.Visible = false;
            process = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
            comboBox1.SelectedIndex = 0;
        }

        private void Form5_Shown(object sender, EventArgs e)
        {
            int len = Es.variables.Count;
            for(int i=0;i<len;i++)
            {
                if(Es.variables[i].type == 2)
                {
                    comboBox1.Items.Add(Es.variables[i].name);
                }
            }
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(process)
            {
                button1.DialogResult = DialogResult.Yes;
            }
            else
            {
                process = true;
                Conclusion();
            }
        }
    }
}
