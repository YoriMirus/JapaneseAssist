using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JapaneseAssistLib.Models
{
    public class KanjiAPIEntry
    {
        [JsonPropertyName("kanji")]
        public string Kanji { get; set; }

        [JsonPropertyName("grade")]
        public int Grade { get; set; }

        [JsonPropertyName("stroke_count")]
        public int StrokeCount { get; set; }

        [JsonPropertyName("meanings")]
        public List<string> Meanings { get; set; }

        [JsonPropertyName("kun_readings")]
        public List<string> KunReadings { get; set; }

        [JsonPropertyName("on_readings")]
        public List<string> OnReadings { get; set; }

        [JsonPropertyName("name_readings")]
        public List<string> NameReadings { get; set; }

        [JsonPropertyName("jlpt")]
        public int Jlpt { get; set; }

        [JsonPropertyName("unicode")]
        public string Unicode { get; set; }

        [JsonPropertyName("heisig_en")]
        public string HeisigEn { get; set; }
        
        /// <summary>
        /// Gets kunyomi readings seperated by '、'
        /// </summary>
        /// <returns></returns>
        public string GetKunyomi()
        {
            return string.Join('、', KunReadings);
        }

        /// <summary>
        /// Gets onyomi readings seperated by '、'
        /// </summary>
        /// <returns></returns>
        public string GetOnyomi()
        {
            return string.Join('、', OnReadings);
        }

        /// <summary>
        /// Gets the meanings seperated by ", "
        /// </summary>
        /// <returns></returns>
        public string GetMeanings()
        {
            return string.Join(", ", Meanings);
        }

        /// <summary>
        /// Gets name readings seperated by '、'
        /// </summary>
        /// <returns></returns>
        public string GetNameReadings()
        {
            return string.Join('、', NameReadings);
        }
    }
}
