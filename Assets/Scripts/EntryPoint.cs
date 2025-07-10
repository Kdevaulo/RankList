using System.Collections.Generic;

using Kdevaulo.RankList.Presentation.Localization;
using Kdevaulo.RankList.Presentation.Params;
using Kdevaulo.RankList.UI;

using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UIElements;

namespace Kdevaulo.RankList
{
    public class EntryPoint : MonoBehaviour
    {
        [Min(0)]
        [SerializeField] private int _currentScore;

        [SerializeField] private UIDocument _uiDocument;
        [SerializeField] private VisualTreeAsset _rankTemplate;
        [SerializeField] private VisualTreeAsset _rankIndicatorTemplate;

        private RanksScroll _ranksScrollView;
        private RanksLocalizer _ranksLocalizer;
        private RankDataProvider _rankDataProvider;
        private ScrollDragHandler _scrollDragHandler;

        private void Awake()
        {
            Assert.IsNotNull(_uiDocument);
            Assert.IsNotNull(_rankTemplate);

            _ranksScrollView = new RanksScroll(_uiDocument, _rankTemplate, _rankIndicatorTemplate);
            _rankDataProvider = new RankDataProvider();
            _ranksLocalizer = new RanksLocalizer();
            _scrollDragHandler = new ScrollDragHandler(_uiDocument);
        }

        public async void Start()
        {
            _scrollDragHandler.Initialize();

            // todo: loading screen enable
            var ranks = await _rankDataProvider.GetDataAsync();
            _ranksLocalizer.Localize(ranks);
            InitializeScroll(ranks);
        }

        private void InitializeScroll(List<Rank> ranks)
        {
            _ranksScrollView.Initialize(ranks, _currentScore);
            // todo: loading screen disable
        }
    }
}