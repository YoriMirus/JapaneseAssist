using System;
using System.Text;
using System.Text.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using JapaneseAssistLib.Models;

namespace JapaneseAssistLib.API
{
    
    public static class KanjiAPI
    {
        private static HttpClient Client;

        public static async Task<KanjiAPIEntry> GetKanjiInfoAsync(char kanji)
        {
            using (Client = new HttpClient())
            {
                string json = await Task.Run(() => Client.GetStringAsync("https://kanjiapi.dev/v1/kanji/" + kanji));
                KanjiAPIEntry entry = JsonSerializer.Deserialize<KanjiAPIEntry>(json);
                return entry;
            }
        }
    }
}
