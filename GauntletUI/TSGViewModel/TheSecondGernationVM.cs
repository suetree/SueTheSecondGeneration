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
    class TheSecondGernationVM: TaleWorlds.Library.ViewModel
    {
        private TheSecondGenerationScreen _screen;
        List<TSGOptionGroup> _optionGroups;
        MBBindingList<TSGOptionGroupVM> optionGroupVMs;
        TheSecondGenerationDataSetting _theSecondGenerationDataSetting;

        public TheSecondGernationVM(TheSecondGenerationScreen screen)
        {
            this._screen = screen;
            this._theSecondGenerationDataSetting = new TheSecondGenerationDataSetting();
            this._optionGroups = this._theSecondGenerationDataSetting.GenerateSettingData();
            optionGroupVMs = new MBBindingList<TSGOptionGroupVM>();
            foreach (TSGOptionGroup group in this._optionGroups)
            {
                optionGroupVMs.Add(new TSGOptionGroupVM(group));
            }
        }


        [DataSourceProperty]
        public MBBindingList<TSGOptionGroupVM> Groups
        {
            get
            {
                return this.optionGroupVMs;
            }
        }


        [DataSourceProperty]
        public string BtnName {
            get
            {
                return new TextObject("{=tsg_complete}Complete").ToString();
            }
        }

        public void ExecuteClose()
        {
            this._screen.CloseScreen();
        }

        public void ExcuteTheSecondBusiness()
        {
            this._screen.CloseScreen();
            TheSecondBusiness.PreOnGameStart();
        }
    }
}
