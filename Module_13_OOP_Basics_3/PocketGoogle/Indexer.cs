using System;
using System.Collections.Generic;
using System.Linq;

namespace PocketGoogle
{
    public class Indexer : IIndexer
    {
        private readonly Dictionary<string, Dictionary<int, List<int>>> _dict = new();
        private readonly HashSet<char> _signes = new() { ' ', '.', ',', '!', '?', ':', '-', '–', '\r', '\n' };

        public void Add(int id, string text)
        {
            int length = text.Length;
            int index = 0;
            while (index < length)
            {
                while (index < length && _signes.Contains(text[index]))
                    index++;
                if (index >= length) break;
                int start = index;
                while (index < length && !_signes.Contains(text[index]))
                    index++;
                string word = text[start..index];
                if (!_dict.TryGetValue(word, out var wordMap))
                {
                    wordMap = new Dictionary<int, List<int>>();
                    _dict[word] = wordMap;
                }

                if (!wordMap.TryGetValue(id, out var positions))
                {
                    positions = new List<int>();
                    wordMap[id] = positions;
                }
                positions.Add(start);
            }
        }

        public List<int> GetIds(string word)
        {
            if (_dict.TryGetValue(word, out var wordMap))
            {
                return wordMap.Keys.ToList();
            }
            else
            {
                return new List<int>();
            }
        }

        public List<int> GetPositions(int id, string word)
        {
            if ((_dict.TryGetValue(word, out var wordMap) && wordMap.TryGetValue(id, out var positions)))
            {
                return new List<int>(positions);
            }
            else
            {
                return new List<int>();
            }
        }

        public void Remove(int id)
        {
            foreach (var word in _dict.Keys.ToList())
            {
                _dict[word].Remove(id);
                if (_dict[word].Count == 0)
                {
                    _dict.Remove(word);
                }
            }
        }
    }
}
