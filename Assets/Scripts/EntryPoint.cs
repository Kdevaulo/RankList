using System.Collections.Generic;

using Kdevaulo.RankList.Model.Params;
using Kdevaulo.RankList.UI;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Kdevaulo.RankList
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private VisualTreeAsset _rankTemplate;
        [SerializeField] private VisualTreeAsset _rankIndicatorTemplate;

        private RanksScroll _ranksScrollView;

        private void Awake()
        {
            Assert.IsNotNull(_uiDocument);
            Assert.IsNotNull(_rankTemplate);

            _ranksScrollView = new RanksScroll(_uiDocument, _rankTemplate, _rankIndicatorTemplate);
        }

        public void Start()
        {
            var t = new List<Rank>()
            {
                new Rank() { Id = 0, Texture = null, LocalizedName = "Test1", Score = 0 },
                new Rank() { Id = 1, Texture = null, LocalizedName = "Test2", Score = 10000 },
                new Rank() { Id = 2, Texture = null, LocalizedName = "Test3", Score = 200000 },
            };

            _ranksScrollView.Initialize(t, 25000);
        }
    }
}