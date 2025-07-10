using System.Collections.Generic;

using Kdevaulo.RankList.Presentation.Params;

namespace Kdevaulo.RankList.Presentation.Localization
{
    public class RanksLocalizer
    {
        public void Localize(List<Rank> ranks)
        {
            foreach (var rank in ranks)
            {
                rank.LocalizedName = rank.Names.en;
            }
        }
    }
}