using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaleWorlds.CampaignSystem;

namespace SueTheSecondGeneration.Utils
{
    class DistinctSpouse<TModel> : IEqualityComparer<TModel>
    {
        public bool Equals(TModel x, TModel y)
        {
            Hero t = x as Hero;
            Hero tt = y as Hero;
            if (t != null && tt != null) return t.StringId == tt.StringId;
            return false;
        }

        public int GetHashCode(TModel obj)
        {
            return obj.ToString().GetHashCode();
        }
    }
}
