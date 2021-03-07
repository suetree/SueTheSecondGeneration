using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SueTheSecondGeneration.Data
{
    class TSGOptionGroup
    {
        public String IdentifyKey;
        public object TargetInstance { get; set; }
        public String Name { get; set; }
        public String Describe { get; set; }
        public bool Enable { get; set; }
        public List<TSGOptionItem> OptionItems { get; set; }

        public TSGOptionGroup(object instance, string identifyKey, string name, string describe = "")
        {
            TargetInstance = instance;
            IdentifyKey = identifyKey;
            Name = name;
            Describe = describe;
        }
      



        public TSGOptionGroup AddOption(TSGOptionItem property)
        {
            if (null != property)
            {
                if (null == OptionItems)
                {
                    OptionItems = new List<TSGOptionItem>();
                }

                if (!OptionItems.Contains(property))
                {
                    OptionItems.Add(property);
                }
            }
            return this;
        }

        public void SetEnableAndRefreshOriginalValue(bool value)
        {
            if (value != this.Enable)
            {
                this.Enable = value;
                ChangeOrignalValue(value);
            }
        }

        public void ChangeOrignalValue(object value)
        {
            if (null != TargetInstance)
            {
                Type type = TargetInstance.GetType();
                PropertyInfo propertyInfo = type.GetProperty(IdentifyKey);
                if (null != propertyInfo)
                {
                    object v = Convert.ChangeType(value, propertyInfo.PropertyType);
                    propertyInfo.SetValue(TargetInstance, v, null);
                }
            }
        }

    }
}
