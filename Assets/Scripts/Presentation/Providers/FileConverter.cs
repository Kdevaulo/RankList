using System.IO;
using System.Threading.Tasks;

using UnityEngine;

namespace Kdevaulo.RankList
{
    public class FileConverter
    {
        public async Task<Texture2D> LoadTextureFromFileAsync(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError($"File not found: {filePath}");
                return null;
            }

            var fileData = await File.ReadAllBytesAsync(filePath);

            var tex = new Texture2D(2, 2, TextureFormat.RGBA32, false);

            if (!tex.LoadImage(fileData))
            {
                Debug.LogError("Failed to load texture from data");
                return null;
            }

            tex.wrapMode = TextureWrapMode.Clamp;
            tex.filterMode = FilterMode.Bilinear;
            tex.Apply(true, false);

            return tex;
        }

        public async Task<T> ConvertFromJsonAsync<T>(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException(filePath);

            var json = await File.ReadAllTextAsync(filePath);
            return JsonUtility.FromJson<T>(json);
        }
    }
}