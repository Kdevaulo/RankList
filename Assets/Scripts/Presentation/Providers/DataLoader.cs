using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

using UnityEngine;

namespace Kdevaulo.RankList
{
    public class DataLoader
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public async Task<string> LoadFileAsync(string url)
        {
            var filePath = Application.persistentDataPath + "/" + Path.GetFileName(new Uri(url).LocalPath);

            try
            {
                await DownloadFileAsync(url, filePath);
                Console.WriteLine($"Файл сохранен в: {filePath}");
                return filePath;
            }
            catch (Exception ex)
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }

                Console.WriteLine($"Ошибка при загрузке: {ex.Message}, файл удален");
                return string.Empty;
            }
        }

        private static async Task DownloadFileAsync(string url, string outputPath)
        {
            using var response = await HttpClient
                .GetAsync(url, HttpCompletionOption.ResponseHeadersRead)
                .ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            await using var sourceStream = await response.Content
                .ReadAsStreamAsync()
                .ConfigureAwait(false);

            await using var fileStream = File.Create(outputPath);

            await sourceStream.CopyToAsync(fileStream)
                .ConfigureAwait(false);
        }
    }
}