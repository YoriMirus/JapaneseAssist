using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace JapaneseAssistLib.Models
{
    public class Meta
    {
        [JsonPropertyName("status")]
        public int Status { get; set; }
    }

    public class Japanese
    {
        [JsonPropertyName("word")]
        public string Word { get; set; }

        [JsonPropertyName("reading")]
        public string Reading { get; set; }
    }

    public class Link
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Source
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }

        [JsonPropertyName("word")]
        public string Word { get; set; }
    }

    public class Senses
    {
        [JsonPropertyName("english_definitions")]
        public List<string> EnglishDefinitions { get; set; }

        /// <summary>
        /// Returns english definitions seperated by ", "
        /// </summary>
        /// <returns></returns>
        public string GetEnglishDefinitions()
        {
            return String.Join(", ", EnglishDefinitions);
        }

        [JsonPropertyName("parts_of_speech")]
        public List<string> PartsOfSpeech { get; set; }

        public string GetPartsOfSpeech()
        {
            return String.Join(", ", PartsOfSpeech);
        }

        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }

        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        [JsonPropertyName("restrictions")]
        public List<string> Restrictions { get; set; }

        [JsonPropertyName("see_also")]
        public List<string> SeeAlso { get; set; }

        [JsonPropertyName("antonyms")]
        public List<string> Antonyms { get; set; }

        [JsonPropertyName("source")]
        public List<Source> Source { get; set; }

        [JsonPropertyName("info")]
        public List<string> Info { get; set; }
    }

    public class Attribution
    {
        /// <summary>
        /// Usually a bool or a string, contains a link to a Jmdict entry.
        /// </summary>
        [JsonPropertyName("jmdict")]
        public object Jmdict { get; set; }

        /// <summary>
        /// Usually a bool or a string, contains a link to a Jmnedict entry.
        /// </summary>
        [JsonPropertyName("jmnedict")]
        public object Jmnedict { get; set; }

        /// <summary>
        /// Usually a bool or a string, contains a link to a Dpedia entry.
        /// </summary>
        [JsonPropertyName("dbpedia")]
        public object Dbpedia { get; set; }
    }

    public class JishoEntry
    {
        /// <summary>
        /// A string that can be used to search for this specific entry in the Jisho API
        /// </summary>
        [JsonPropertyName("slug")]
        public string Slug { get; set; }

        [JsonPropertyName("is_common")]
        public bool IsCommon { get; set; }

        /// <summary>
        /// Contains tags like the wani kani level.
        /// </summary>
        [JsonPropertyName("tags")]
        public List<string> Tags { get; set; }

        /// <summary>
        /// JLPT level of this word
        /// </summary>
        [JsonPropertyName("jlpt")]
        public List<string> Jlpt { get; set; }

        /// <summary>
        /// Contains how this word is written and read.
        /// </summary>
        [JsonPropertyName("japanese")]
        public List<Japanese> Japanese { get; set; }

        /// <summary>
        /// Contains the english information about this entry (english definitions, parts of speech, antonyms, additional information, etc.)
        /// </summary>
        [JsonPropertyName("senses")]
        public List<Senses> Senses { get; set; }

        /// <summary>
        /// Contains links for this entry in a different dictionary.
        /// </summary>
        [JsonPropertyName("attribution")]
        public Attribution Attribution { get; set; }
    }

    public class JishoAPIResponse
    {
        [JsonPropertyName("meta")]
        public Meta Meta { get; set; }

        [JsonPropertyName("data")]
        public List<JishoEntry> Data { get; set; }
    }

}
