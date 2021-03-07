using SueTheSecondGeneration.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SueTheSecondGeneration.GauntletUI.TSGViewModel
{
    class TSGOptionGroupVM: TaleWorlds.Library.ViewModel
    {
        TSGOptionGroup _optionGroup;
        MBBindingList<TSGOptionGroupItemVM> _optionItemVMs;

        public TSGOptionGroupVM(TSGOptionGroup optionGroup)
        {
            this._optionGroup = optionGroup;
            this._optionItemVMs = new MBBindingList<TSGOptionGroupItemVM>();
            foreach (TSGOptionItem item in this._optionGroup.OptionItems)
            {
                this._optionItemVMs.Add(new TSGOptionGroupItemVM(item));
            }
        }
        [DataSourceProperty]
        public string Name 
        {

            get {
                return  new TextObject(this._optionGroup.Name, null).ToString(); 
            }
        }

        [DataSourceProperty]
        public string Describe
        {

            get
            {
                return new TextObject(this._optionGroup.Describe, null).ToString();
            }
        }

        [DataSourceProperty]
        public bool BoolValue
        {
            set {
                if (!this._optionGroup.Enable.Equals(value))
                {
                   
                    this._optionGroup.SetEnableAndRefreshOriginalValue(value);
                    base.OnPropertyChanged("BoolValue");
                   
                }
            }

            get {
                return this._optionGroup.Enable;
            }
        }

        [DataSourceProperty]
        public MBBindingList<TSGOptionGroupItemVM> OptionItems
        {
            get
            {
                return this._optionItemVMs;
            }
        }
    }
}
