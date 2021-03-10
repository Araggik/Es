using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es
{
    [Serializable]
    public class Domain
    {
        public string name;
        public List<string> values;

        public Domain()
        {
            
        }

        public Domain(string name,List<string> val)
        {
            this.name = name;        
            values = val;
        }
    }
}
