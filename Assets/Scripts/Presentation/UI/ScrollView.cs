using System;
using System.Collections.Generic;

using Kdevaulo.RankList.Model.Params;

using UnityEngine;
using UnityEngine.UIElements;

namespace Kdevaulo.RankList.UI
{
    public class ScrollView
    {
        private readonly UIDocument _uiDocument;

        public ScrollView(UIDocument uiDocument)
        {
            _uiDocument = uiDocument;
        }

        public void Initialize(List<Rank> ranks)
        {
            Debug.Log("Ranks initialized");
        }

        public void Dispose()
        {
        }
    }
}