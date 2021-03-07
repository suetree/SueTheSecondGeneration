using SueTheSecondGeneration.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SueTheSecondGeneration.GauntletUI.TSGViewModel
{
    class TSGOptionGroupItemVM: ViewModel
    {
        private TSGOptionItem _optionItem;
        TSGSelectorVM<TSGSelectorItemVM> _selectorVM;
        MBBindingList<TSGOptionGroupItemGroupVM> _optionItemGroupVMList = new MBBindingList<TSGOptionGroupItemGroupVM>();
        public TSGOptionGroupItemVM(TSGOptionItem optionItem)
        {
            this._optionItem = optionItem;
            InitDropdownPropertyData();
           // InitDefaultValue();
            ExcuteNewListItem();
            this.RefreshValues();
        }


 

        private void InitDropdownPropertyData()
        {
            if (this.IsDropdownProperty)
            {
                List<TextObject> texts = new List<TextObject>();

                this._optionItem.SelectItems.ForEach((obj) => {
                    texts.Add(new TextObject(obj.Name, null));
                });
                int k = 0;
                List<int> selectIndexs = new List<int>();
                if (this._optionItem.OptionType == TSGOptionType.MultipleSelectProperty)
                {
                    List<TSGValueNamePair> list = (List<TSGValueNamePair>)this._optionItem.PropertyValue;
                    this._optionItem.SelectItems.ForEach((obj) => {
                        list.ForEach((obj2) =>
                        {
                            if (obj2.Equals(obj)) { selectIndexs.Add(k); }
                        });
                        k++;
                    });
                }
                else
                {
                    TSGValueNamePair pair = (TSGValueNamePair)this._optionItem.PropertyValue;
                    this._optionItem.SelectItems.ForEach((obj) => {
                        if (pair.Equals(obj) ) {
                            if (selectIndexs.Count == 0) {
                                selectIndexs.Add(k);
                                }; 
                        }
                        k++;
                    });
                }


                if (this._optionItem.OptionType == TSGOptionType.MultipleSelectProperty)
                {
                    this._selectorVM = new TSGSelectorVM<TSGSelectorItemVM>(texts, selectIndexs, OnSelectItem, true);
                }
                else
                {
                    this._selectorVM = new TSGSelectorVM<TSGSelectorItemVM>(texts, selectIndexs, OnSelectItem, false);
                }


            }
        }
        [DataSourceProperty]
        public bool IsDropdownProperty
        {
            get => (this._optionItem.OptionType == TSGOptionType.SingleSelectProperty || this._optionItem.OptionType == TSGOptionType.MultipleSelectProperty);
        }

        [DataSourceProperty]
        public bool IsGroupProperty
        {
            get => (this._optionItem.OptionType == TSGOptionType.ListProerty );
        }

        [DataSourceProperty]
        public bool IsNotGroupProperty
        {
            get => (this._optionItem.OptionType != TSGOptionType.ListProerty);
        }


        public void OnSelectItem(TSGSelectorVM<TSGSelectorItemVM> selectorVM)
        {
            List<int> indexs = selectorVM.SelectedIndexs;
            if (selectorVM.IsMutipleSelectMode)
            {
                List<TSGValueNamePair> list = new List<TSGValueNamePair>();
                foreach (int index in indexs)
                {
                    if (index < this._optionItem.SelectItems.Count)
                    {
                        list.Add(this._optionItem.SelectItems[index]);
                    }
                   
                }
                this._optionItem.SetAndRefreshOriginalValue(list);
            }
            else
            {
                this._optionItem.SetAndRefreshOriginalValue(this._optionItem.SelectItems[indexs[0]]) ;
            }
           // base.OnPropertyChangedWithValue(this._selectorVM, "DropdownValue");
        }

        [DataSourceProperty]
        public string DisplayName
        {
            get => new TextObject(this._optionItem.DisplayName, null).ToString();
        }

        [DataSourceProperty]
        public string ListAddBtnName
        {
            get => new TextObject("New").ToString();
        }

        [DataSourceProperty]
        public MBBindingList<TSGOptionGroupItemGroupVM> OptionItemGroupList
        {
            get
            {
                return this._optionItemGroupVMList;
            }
        }

        [DataSourceProperty]
        public bool IsBoolProperty
        {
            get => this._optionItem.OptionType == TSGOptionType.BoolProperty;
        }

        [DataSourceProperty]
        public bool IsTextInputProperty
        {
            get => this._optionItem.OptionType == TSGOptionType.InputTextProperty;
        }

        [DataSourceProperty]
        public bool IsFloatProperty
        {
            get => (this._optionItem.OptionType == TSGOptionType.FloatProperty || this._optionItem.OptionType == TSGOptionType.IntegerProperty);
        }


        [DataSourceProperty]
        public TSGSelectorVM<TSGSelectorItemVM> DropdownValue
        {
            get
            {
                if (this.IsDropdownProperty)
                {
                    return this._selectorVM;
                }
                else
                {
                    return new TSGSelectorVM<TSGSelectorItemVM>(null);
                }
            }
        }

        [DataSourceProperty]
        public String TextValue
        {
            get
            {
                if (IsTextInputProperty && null != this._optionItem.PropertyValue)
                {
                    return this._optionItem.PropertyValue.ToString();
                }
                return "";
            }
        }

        public void OnTextValueClick()
        {
            InformationManager.ShowTextInquiry(new TextInquiryData(DisplayName, string.Empty, true, true,
                GameTexts.FindText("str_done", null).ToString(), GameTexts.FindText("str_cancel", null).ToString(),
                new Action<string>(this.OnChangeNameDone), null, false, new Func<string, bool>(this.IsNewNameApplicable), ""), false);
        }
        private bool IsNewNameApplicable(string input)
        {
            return input.Length <= 10 && input.Length >= 0;
        }

        private void OnChangeNameDone(string value)
        {
            if (null != this._optionItem.PropertyValue && !this._optionItem.PropertyValue.Equals(value))
            {
                this._optionItem.SetAndRefreshOriginalValue(value);

            }
            else
            {
                this._optionItem.SetAndRefreshOriginalValue(value) ;
            }

            base.OnPropertyChanged("TextValue");
        }

        [DataSourceProperty]
        public string BackgroundBrushName
        {
            get {
                if (!IsGroupProperty)
                {
                    return "Clan.Item.Tuple";
                }
                else
                {
                    return "Frame1Brush";
                }
               
            }
        }


        [DataSourceProperty]
        public bool BoolValue
        {
            get
            {
                bool val = false;
                if (this._optionItem.OptionType == TSGOptionType.BoolProperty)
                {
                    if (null != this._optionItem.PropertyValue)
                    {
                        val = (bool)this._optionItem.PropertyValue;
                    }
                }
                return val;
            }
            set
            {
                if (!this._optionItem.PropertyValue.Equals(value))
                {
                    this._optionItem.SetAndRefreshOriginalValue(value);
                    base.OnPropertyChangedWithValue(value, "BoolValue");
                   
                }
            }
        }

        [DataSourceProperty]
        public float NumberValue
        {
            get
            {
                float val = 0f;
                if (this._optionItem.OptionType == TSGOptionType.FloatProperty || this._optionItem.OptionType == TSGOptionType.IntegerProperty)
                {
                    if (null != this._optionItem.PropertyValue)
                    {
                        if (this._optionItem.OptionType == TSGOptionType.FloatProperty)
                        {
                            val = (float)this._optionItem.PropertyValue;
                        }
                        else
                        {
                            val = Convert.ToInt32(this._optionItem.PropertyValue);
                        }
                    }
                }
                return val;
            }
            set
            {
                if (!this._optionItem.PropertyValue.Equals(value))
                {
                    if (this._optionItem.OptionType == TSGOptionType.IntegerProperty)
                    {
                        this._optionItem.SetAndRefreshOriginalValue(Convert.ToInt32(value)) ;
                    }
                    else
                    {
                        this._optionItem.SetAndRefreshOriginalValue(value) ;
                    }

                    base.OnPropertyChanged("NumberValue");
                    base.OnPropertyChanged("ShowValueText");
                    //InformationManager.DisplayMessage(new InformationMessage("newValue " + this._optionItem.PropertyValue));
                }
            }
        }

        [DataSourceProperty]
        public float MaxValue
        {
            get => (float)this._optionItem.MaxValue;
        }


        [DataSourceProperty]
        public float MinValue
        {
            get => (float)this._optionItem.MinValue;
        }


        [DataSourceProperty]
        public string ShowValueText
        {
            get
            {
                String text = "";
                if (null != this._optionItem.PropertyValue)
                {
                    if (this._optionItem.OptionType == TSGOptionType.FloatProperty)
                    {
                        float val = (float)this._optionItem.PropertyValue;
                        text = val.ToString("0.00");
                    }
                    else if (this._optionItem.OptionType == TSGOptionType.IntegerProperty)
                    {
                        int val = Convert.ToInt32(this._optionItem.PropertyValue);
                        text = val + "";
                    }
                }
                return text;
            }
        }

        public void ExcuteNewListItem()
        {
            if (!IsGroupProperty)
            {
                return;
            }
            TSGOptionGroup group = new TSGOptionGroup(null, null, "" + this._optionItemGroupVMList.Count);
            Type type = this._optionItem.TargetInstanceType;
            ArrayList list = (ArrayList)this._optionItem.PropertyValue ;
            object obj =((object)Activator.CreateInstance(type, new object[]{}));
            list.Add(obj);
            foreach (TSGOptionItem item in this._optionItem.ItemTemplate)
            {
                TSGOptionItem newItem = item.Copy();
                newItem.TargetInstance = obj;
                group.AddOption(newItem);
            }
            this._optionItem.ChangeOrignalValue(list);
            TSGOptionGroupItemGroupVM groupItemGroupVM = new TSGOptionGroupItemGroupVM(group, OnDeleteItemFromGroup);
            this._optionItemGroupVMList.Add(groupItemGroupVM);
            base.OnPropertyChanged( "OptionItemGroupList");
        }

        public void OnDeleteItemFromGroup(TSGOptionGroupItemGroupVM vm)
        {
            int index = this._optionItemGroupVMList.IndexOf(vm);
            ArrayList list = (ArrayList)this._optionItem.PropertyValue;
            list.RemoveAt(index);
            this._optionItem.ChangeOrignalValue(list);
            this._optionItemGroupVMList.Remove(vm);
            base.OnPropertyChanged("OptionItemGroupList");
        }
    }
}
