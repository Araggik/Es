using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es
{
    [Serializable]
    public class ExpertSys
    {
        public List<Domain> domains;
        public List<Variable> variables;
        public List<Rule> rules;

        public ExpertSys()
        {
            domains = new List<Domain>();
            variables = new List<Variable>();
            rules = new List<Rule>();
        }
    }
}
