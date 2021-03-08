using System;
using System.Text.Json;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Text;

using JapaneseAssistLib.Models;

namespace JapaneseAssistLib.API
{
    public static class JishoAPI
    {
        private static HttpClient client;

        public static async Task<List<JishoEntry>> GetJishoEntry(string word)
        {
            List<JishoEntry> entries = new List<JishoEntry>();
            using (client = new HttpClient())
            {
                StringBuilder json = new StringBuilder(await Task.Run(() => client.GetStringAsync("https://jisho.org/api/v1/search/words?keyword=" + word)));
                entries = await Task.Run(() => JsonSerializer.Deserialize<JishoAPIResponse>(json.ToString()).Data);
            }
            return entries;
        }
    }
}
