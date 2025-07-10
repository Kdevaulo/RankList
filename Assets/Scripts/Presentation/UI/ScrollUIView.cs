using System.Collections.Generic;

using Kdevaulo.RankList.Model.Params;

using UnityEngine.UIElements;

namespace Kdevaulo.RankList.UI
{
    public class ScrollUIView
    {
        private readonly UIDocument _uiDocument;
        private readonly VisualTreeAsset _rankTemplate;

        private ScrollView _ranksScrollView;
        private List<Rank> _itemsCollection;

        public ScrollUIView(UIDocument uiDocument, VisualTreeAsset rankTemplate)
        {
            _uiDocument = uiDocument;
            _rankTemplate = rankTemplate;
        }

        public void Initialize(List<Rank> items)
        {
            _itemsCollection = items;
            _ranksScrollView = _uiDocument.rootVisualElement.Q<ScrollView>("RanksList");

            // _ranksScrollView.Add();
            //     .makeItem = MakeListItem;
            // _ranksScrollView.bindItem = SetupElement;
            // _ranksScrollView.itemsSource = items;
        }

        public void Dispose()
        {
            // _ranksScrollView.makeItem = null;
            // _ranksScrollView.bindItem = null;
        }

        private void SetupElement(VisualElement element, int index)
        {
            var item = _itemsCollection[index];
            element.Q<Label>("Name").text = item.LocalizedName;
            element.Q<Label>("Score").text = item.Score.ToString();
            element.Q<VisualElement>("Image").style.backgroundImage = item.Texture;
        }

        private VisualElement MakeListItem()
        {
            return _rankTemplate.Instantiate();
        }
    }
}