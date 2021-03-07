using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SueTheSecondGeneration.Data
{
    class TSGOptionBuilder
    {
        public List<TSGOptionGroup> Groups { get; set; }

        public TSGOptionGroup BuildGroup(object instance, string identifyKey, string name, string describe)
        {
            if (null == Groups)
            {
                Groups = new List<TSGOptionGroup>();
            }
            TSGOptionGroup group = new TSGOptionGroup(instance, identifyKey, name);
            group.Describe = describe;
            group.Enable = true;
            this.Groups.Add(group);
            return group;
        }
    }
}
