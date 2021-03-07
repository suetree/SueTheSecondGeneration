using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.Core.ViewModelCollection;
using TaleWorlds.Library;
using TaleWorlds.Localization;

namespace SueTheSecondGeneration.GauntletUI.TSGViewModel
{
    class TSGSelectorItemVM: SelectorItemVM
    {
        Action<TSGSelectorItemVM> _onSelectorItemClick;
        private bool _isMutipleSelectMode;

		[DataSourceProperty]
		public bool IsMutipleSelectMode
        {
			get
			{
				return this._isMutipleSelectMode;
			}
			
		}


        public TSGSelectorItemVM(TextObject s, Action<TSGSelectorItemVM> OnSelectorItemClick, bool isMutipleSelector = false) : base(s)
        {
            this._isMutipleSelectMode  = isMutipleSelector;
            this._onSelectorItemClick = OnSelectorItemClick;
        }

        public void ExcuteSelectorItemClick()
        {
            this._onSelectorItemClick(this);
        }
    }
}
