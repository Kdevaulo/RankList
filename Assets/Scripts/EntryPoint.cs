using System.Collections.Generic;

using Kdevaulo.RankList.Model.Params;

using UnityEngine;
using UnityEngine.UIElements;

using ScrollView = Kdevaulo.RankList.UI.ScrollView;

namespace Kdevaulo.RankList
{
    public class EntryPoint : MonoBehaviour
    {
        [SerializeField] private UIDocument _uiDocument;

        private ScrollView _scrollView;

        private void Awake()
        {
            _scrollView = new ScrollView(_uiDocument);
        }

        public void Start()
        {
            var t = new List<Rank>()
            {
                new Rank() { Id = 0, Image = null, LocalizedName = "Test1", Score = 0 },
                new Rank() { Id = 1, Image = null, LocalizedName = "Test2", Score = 100 },
                new Rank() { Id = 2, Image = null, LocalizedName = "Test3", Score = 200 },
            };

            _scrollView.Initialize(t);
        }
    }
}