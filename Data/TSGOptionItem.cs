using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SueTheSecondGeneration.Data
{
    class TSGOptionItem
    {
        public object TargetInstance { get; set; }
        public Type TargetInstanceType { get; set; }
        public String DisplayName { get; set; }
        private object _propertyValue;
        public TSGOptionType OptionType { get; set; }
        public float MinValue { get; set; }
        public float MaxValue { get; set; }
        public List<TSGValueNamePair> SelectItems { get; set; }
        public String IdentifyKey;
        public List<TSGOptionItem> ItemTemplate = new List<TSGOptionItem>();

      

        public TSGOptionItem(object instance, string identifyKey,  string name, object value, TSGOptionType type, float minValue = 0,float maxValue = 0)
        {
            TargetInstance = instance;
            IdentifyKey = identifyKey;
            DisplayName = name;
            this. _propertyValue = value;
            OptionType = type;
            MinValue = minValue;
            MaxValue = maxValue;
           
        }

        public TSGOptionItem(object instance, string identifyKey, string name, object value, TSGOptionType type,  List<TSGOptionItem> items ) : this(instance,identifyKey, name, value, type, 0, 100)
        {
            if (null != items)
            {
                ItemTemplate = items;
            }
        }


        public TSGOptionItem(object instance, string identifyKey, string name, object value, TSGOptionType type) : this(instance, identifyKey, name, value, type, 0, 100)
        {
           
        }

        public TSGOptionItem Copy()
        {
            TSGOptionItem copy = new TSGOptionItem(TargetInstance, IdentifyKey, DisplayName, this._propertyValue, OptionType, MinValue, MaxValue);
            copy.FillSelectItems(SelectItems);
            copy.SetTargetInstanceType(TargetInstanceType);
            copy.ItemTemplate = ItemTemplate;
            return copy;
        }

        public TSGOptionItem FillSelectItems(List<TSGValueNamePair> list)
        {
            SelectItems = list;
            return this;
        }

        public TSGOptionItem SetTargetInstanceType(Type type)
        {
            TargetInstanceType = type;
            return this;
        }

        public object PropertyValue
        {
            get
            {
                return this._propertyValue;
            }
         
        }

        public void SetAndRefreshOriginalValue(object value)
        {
            if (value != this._propertyValue)
            {
                this._propertyValue = value;
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
