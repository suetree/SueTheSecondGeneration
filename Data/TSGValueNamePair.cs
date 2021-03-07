using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SueTheSecondGeneration.Data
{
    class TSGValueNamePair
    {
        public object Value { set; get; }
        public String Name { set; get; }

        public TSGValueNamePair(object value, String name)
        {
            this.Value = value;
            this.Name = name;
        }
    }
}
