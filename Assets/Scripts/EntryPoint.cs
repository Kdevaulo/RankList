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

        private ScrollUIView _scrollView;

        private void Awake()
        {
            Assert.IsNotNull(_uiDocument);
            Assert.IsNotNull(_rankTemplate);

            _scrollView = new ScrollUIView(_uiDocument, _rankTemplate);
        }

        public void Start()
        {
            var t = new List<Rank>()
            {
                new Rank() { Id = 0, Texture = null, LocalizedName = "Test1", Score = 0 },
                new Rank() { Id = 1, Texture = null, LocalizedName = "Test2", Score = 100 },
                new Rank() { Id = 2, Texture = null, LocalizedName = "Test3", Score = 200 },
            };

            _scrollView.Initialize(t);
        }
    }
}