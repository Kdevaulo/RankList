using System.Collections.Generic;
using System.Linq;

using Kdevaulo.RankList.Model.Params;
using Kdevaulo.RankList.Presentation.Utilities;

using UnityEngine;
using UnityEngine.UIElements;

namespace Kdevaulo.RankList.UI
{
    public class RanksScroll
    {
        private const string RankGainedIndicatorClass = "rankGainedIndicator";
        private const string LastGainedIndicatorClass = "lastGainedIndicator";
        private const string ActiveProgressBarClass = "rankProgressBarActive";
        private const string GainedRankClass = "rankItemActive";

        private const int ItemSize = 188;

        private readonly UIDocument _uiDocument;
        private readonly VisualTreeAsset _rankTemplate;
        private readonly VisualTreeAsset _rankIndicator;

        public RanksScroll(UIDocument uiDocument, VisualTreeAsset rankTemplate, VisualTreeAsset rankIndicator)
        {
            _uiDocument = uiDocument;
            _rankTemplate = rankTemplate;
            _rankIndicator = rankIndicator;
        }

        public void Initialize(List<Rank> items, int score)
        {
            var ranksList = _uiDocument.rootVisualElement.Q<VisualElement>("RanksList");

            var ranksContainer = ranksList.Q<VisualElement>("RanksContainer");
            var ranksGainContainer = ranksList.Q<VisualElement>("RanksGainContainer");
            var ranksProgressBar = ranksList.Q<VisualElement>("RanksProgressBar");

            var scoreLabel = ranksProgressBar.Q<Label>("CurrentScore");

            if (score > 0)
            {
                ranksProgressBar.AddToClassList(ActiveProgressBarClass);
                var progressBarSize = CalculateProgressBarSize(items, score);
                ranksProgressBar.style.width = progressBarSize;
            }

            if (score >= items.Last().Score)
            {
                ranksGainContainer.Q<VisualElement>("LastIndicator").AddToClassList(LastGainedIndicatorClass);
            }

            SetScore(scoreLabel, score);
            ProcessItems(items, ranksContainer, ranksGainContainer, score);
        }

        private int CalculateProgressBarSize(List<Rank> items, int score)
        {
            var gainedRanksCount = 0;
            var lastRankIndex = 0;
            var ranksCount = items.Count;

            for (var i = 1; i < ranksCount; i++)
            {
                var item = items[i];

                if (score >= item.Score)
                {
                    gainedRanksCount++;
                    lastRankIndex = i;
                }
            }

            if (lastRankIndex == ranksCount - 1)
            {
                return ItemSize * (ranksCount - 1);
            }

            var lastGainedScore = items[lastRankIndex].Score;
            var nextTargetScore = items[lastRankIndex + 1].Score;

            var currentProgressRaw = score - lastGainedScore;
            var currentTarget = nextTargetScore - lastGainedScore;
            var currentProgressPercent = (float) currentProgressRaw / currentTarget;

            var currentPixels = Mathf.RoundToInt(currentProgressPercent * ItemSize);
            var gainedFillPixels = gainedRanksCount * ItemSize;

            return currentPixels + gainedFillPixels;
        }

        private void ProcessItems(List<Rank> items,
            VisualElement ranksContainer, VisualElement ranksGainContainer, int score)
        {
            ProcessRanks(items, ranksContainer, score);
            ProcessIndicators(items, ranksGainContainer, score);
        }

        private void ProcessRanks(List<Rank> items, VisualElement ranksContainer, int score)
        {
            var ranksCount = items.Count;

            for (var i = 0; i < ranksCount; i++)
            {
                var currentRank = items[i];

                var rankRoot = _rankTemplate.Instantiate();

                SetupRank(rankRoot, currentRank, score >= currentRank.Score);

                ranksContainer.Add(rankRoot);

                if (i == 0)
                {
                    RemoveScoreImage(rankRoot);
                }
            }
        }

        private void ProcessIndicators(List<Rank> items, VisualElement indicatorsContainer, int score)
        {
            var ranksCount = items.Count;

            for (var i = 0; i < ranksCount - 1; i++)
            {
                var currentRank = items[i];
                var indicatorRoot = _rankIndicator.Instantiate();

                if (score >= currentRank.Score)
                {
                    indicatorRoot.Q<VisualElement>("RankGainIndicator").AddToClassList(RankGainedIndicatorClass);
                }

                indicatorsContainer.Add(indicatorRoot);
            }
        }

        private void SetScore(Label label, int score)
        {
            label.text = score.ToSpacedString();
        }

        private void SetupRank(TemplateContainer container, Rank rankParams, bool isGained)
        {
            if (isGained)
            {
                container.Q<VisualElement>("Rank").AddToClassList(GainedRankClass);
            }

            container.Q<Label>("Name").text = rankParams.LocalizedName;
            container.Q<Label>("Score").text = rankParams.Score.ToSpacedString();
            container.Q<VisualElement>("Image").style.backgroundImage = rankParams.Texture;
        }

        private void RemoveScoreImage(TemplateContainer rankRoot)
        {
            var scoreImage = rankRoot.Q<VisualElement>("ScoreImage");
            scoreImage.parent.Remove(scoreImage);
        }
    }
}