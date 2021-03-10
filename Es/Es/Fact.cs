using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es
{
    [Serializable]
    public class Fact
    {
        public Variable variable;
        public string value;

        public Fact(Variable variable, string value)
        {
            this.variable = variable;
            this.value = value;
        }
    }
}
