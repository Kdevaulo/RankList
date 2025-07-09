using System.Collections.Generic;

using Kdevaulo.RankList.Model.Params;

using UnityEngine.UIElements;

namespace Kdevaulo.RankList.UI
{
    public class ScrollView
    {
        private readonly UIDocument _uiDocument;
        private readonly VisualTreeAsset _rankTemplate;

        private ListView _ranksListView;
        private List<Rank> _itemsCollection;

        public ScrollView(UIDocument uiDocument, VisualTreeAsset rankTemplate)
        {
            _uiDocument = uiDocument;
            _rankTemplate = rankTemplate;
        }

        public void Initialize(List<Rank> items)
        {
            _itemsCollection = items;
            _ranksListView = _uiDocument.rootVisualElement.Q<ListView>("RanksList");

            _ranksListView.makeItem = MakeListItem;
            _ranksListView.bindItem = SetupElement;
            _ranksListView.itemsSource = items;
        }

        public void Dispose()
        {
            _ranksListView.makeItem = null;
            _ranksListView.bindItem = null;
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