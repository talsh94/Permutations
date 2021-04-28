using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Permutations.Models
{
    public class KnownWordsDictionary
    {
        const string PATH = @"/Users/talshoham/Desktop/permutationsfile.txt";
        const char SPLIT_BY = '\n';

        private Dictionary<string, HashSet<String>> _knownWords;

        public KnownWordsDictionary()
        {
            _knownWords = new Dictionary<string, HashSet<string>>();
            string[] wordsFromFile = File.ReadAllText(PATH).Split(SPLIT_BY);
            CreateKnownWordsDic(wordsFromFile);
        }
        public List<String> GetPermutations(string word)
        {
            var wordLetters = word.ToCharArray().OrderBy(c => c).ToString();
            if (!_knownWords.ContainsKey(wordLetters))
            {
                return new List<string> { };
            }
            return _knownWords[wordLetters].ToList();
        }

        public void CreateKnownWordsDic(string[] words)
        {
            foreach (var word in words)
            {
                AddToKnownWordsDictionary(word);
            }
        }

        private void AddToKnownWordsDictionary(string word)
        {
            var wordLetters = word.ToCharArray().OrderBy(c => c).ToString();

            if (_knownWords.ContainsKey(wordLetters))
            {
                _knownWords[wordLetters].Add(word);
            }
            else
            {
                _knownWords.Add(wordLetters, new HashSet<string> { word });
            }
        }
        //method for adding and finding
    }

}