using System;

namespace Kdevaulo.RankList.Model.Params
{
    [Serializable]
    public class SerializableRank
    {
        public long id;
        public int score;
        public LocalizationNames name;
    }
}