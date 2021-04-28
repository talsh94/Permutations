using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Permutations.Models
{
    public class KnownWords
    {
        #region Constants
        const string PATH = @"/Users/talshoham/Desktop/permutationsfile.txt";
        const char SPLIT_BY = '\n';
        #endregion

        private Dictionary<string, HashSet<String>> _knownWords;

        public KnownWords()
        {
            _knownWords = new Dictionary<string, HashSet<string>>();
            string[] wordsFromFile = File.ReadAllText(PATH).Split(SPLIT_BY);
            CreateKnownWordsDic(wordsFromFile);
        }

        #region Get
        public HashSet<String> GetPermutation(string word)
        {
            var sortedWord = sortStringCharachters(word);
            if (!_knownWords.ContainsKey(sortedWord))
            {
                return new HashSet<string> { };
            }
            return _knownWords[sortedWord];
        }
        #endregion

        public HashSet<string> PostPermutation(string word)
        {
            var sortedWord = sortStringCharachters(word);
            if (!_knownWords.ContainsKey(sortedWord))
            {
                _knownWords.Add(sortedWord, new HashSet<string> { word });
            }
            else
            {
                _knownWords[sortedWord].Add(word);
            }

            return _knownWords[sortedWord];
            //Todo: do i need to write to the file?
        }
        //    var wordLetters = word.ToCharArray().OrderBy(c => c).ToString();

        //    if (!_knownWords.ContainsKey(wordLetters))
        //    {
        //        _knownWords.Add(wordLetters, new HashSet<string> { word });
        //    }
        //    else
        //    {
        //        _knownWords[wordLetters].Add(word);
        //    }


        #region Creating the Dictionary
        private void CreateKnownWordsDic(string[] words)
        {
            foreach (var word in words)
            {
                AddToKnownWordsDictionary(word);
            }
        }

        private void AddToKnownWordsDictionary(string word)
        {
            var sortedWord = sortStringCharachters(word);

            if (_knownWords.ContainsKey(sortedWord))
            {
                _knownWords[sortedWord].Add(word);
            }
            else
            {
                _knownWords.Add(sortedWord, new HashSet<string> { word });
            }
        }
        #endregion

        #region Helper Methods
        private string sortStringCharachters(string word)
        {
            char[] sortedLettersArray = word.ToCharArray();
            Array.Sort(sortedLettersArray);
            return new string(sortedLettersArray);
        }
        #endregion
    }

}