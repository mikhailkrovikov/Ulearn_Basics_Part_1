// Вставьте сюда финальное содержимое файла SentencesParserTask.cs
using System;
using System.Collections.Generic;
using System.Linq;

namespace TextAnalysis
{
    static class SentencesParserTask
    {
        public static List<List<string>> ParseSentences(string text)
        {
            var sentencesList = new List<List<string>>();
            string[] signsSents = { ".", "!", "?", ";", "(", ")", ":" };
            char[] signsWords = {
                                  '/', '	','=',' ', ',', '\"','\t','\n', '\r','^','#',
                                  '$','%','&','*','-','+','_','@',
                                  '0','1','2','3','4','5','6','7','8','9','“','”','—','…','‘',' '};
            string[] sentences = text.Split(signsSents, StringSplitOptions.RemoveEmptyEntries);
            foreach (string sentence in sentences)
            {
                var wordsList = new List<string>();
                string[] words = sentence.Split(signsWords, StringSplitOptions.RemoveEmptyEntries);
                foreach (string word in words)
                    wordsList.Add(word.ToLower());
                if (wordsList.Count > 0)
                    sentencesList.Add(wordsList);
            }
            return sentencesList;
        }
    }
}