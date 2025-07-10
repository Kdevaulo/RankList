using System.Collections.Generic;
using System.Threading.Tasks;

using Kdevaulo.RankList.Model.Params;
using Kdevaulo.RankList.Presentation.Params;

using UnityEngine;

namespace Kdevaulo.RankList
{
    public class RankDataProvider
    {
        private const string BaseUrl = "https://tanki-test.maxray.pro/Items/";
        private const string RanksUrl = "ranks.json";

        private readonly DataLoader _dataLoader = new DataLoader();
        private readonly FileConverter _fileConverter = new FileConverter();

        public async Task<List<Rank>> GetDataAsync()
        {
            var path = await _dataLoader.LoadFileAsync(BaseUrl + RanksUrl);
            var ranks = await _fileConverter.ConvertFromJsonAsync<SerializableRanks>(path);

            var ranksCollection = ranks.ranks;
            var ranksCount = ranksCollection.Length;

            var loadedRanks = new List<Rank>();

            for (var i = 0; i < ranksCount; i++)
            {
                var currentRank = ranksCollection[i];
                var texture = await GetTextureAsync(currentRank.id);

                loadedRanks.Add(new Rank()
                {
                    Score = currentRank.score,
                    Names = currentRank.name,
                    Texture = texture
                });
            }

            return loadedRanks;
        }

        private async Task<Texture2D> GetTextureAsync(long id)
        {
            var path = await _dataLoader.LoadFileAsync(BaseUrl + id + ".png");
            return await _fileConverter.LoadTextureFromFileAsync(path);
        }
    }
}