using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Permutations.Models
{
    //KnownWords is where the webservice manages its data-a data structure of known words
    public class KnownWords
    {
        #region Constants
        //path to the file of words (TODO: may need to change this)
        const string PATH = @"/Users/talshoham/Desktop/permutationsfile.txt";
        const char SPLIT_BY = '\n';
        #endregion

        //lock that's used to manage resource access- allows multiple
        //threads for reading or exclusive access for writing.
        private ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();

        //dictionary that hold all the data of the web service.
        //Key: a string that was sorted by charachters, Value: all the known
        //permutation of the key-letters
        private Dictionary<string, HashSet<String>> _knownWords;

        //constructor reads the file and create a dictionary of known words
        public KnownWords()
        {
            _knownWords = new Dictionary<string, HashSet<string>>();
            string[] wordsFromFile = File.ReadAllText(PATH).Split(SPLIT_BY);
            CreateKnownWordsDic(wordsFromFile);
        }

        //Method for getting a set of all known permutaions of the input word.
        //the method creates a sorted string from the input, then while in a
        //Readlock, searches for the sorted word in the knownWords dicitonary.
        public HashSet<String> GetPermutation(string word)
        {
            var sortedWord = sortStringCharachters(word);
            HashSet<string> returnVal = new HashSet<string>();

            rwl.EnterReadLock();
            try
            {
                if (!_knownWords.ContainsKey(sortedWord))
                {
                    returnVal = new HashSet<string> { };
                }
                else
                {
                    returnVal = _knownWords[sortedWord];
                }
            }
            finally
            {
                rwl.ExitReadLock();
            }

            return returnVal;
        }

        //Method for adding the input word to the knownWords dictionary
        //The method creates a sorted string from the input, then enters a
        //ReadLock to check if the sorted word is in knownWords. It either adds
        //a new Key, Value pair, or add the word to the values according to
        //if knownWords already contained the key.
        //The word is added to the file of words if it is not already there.
        public HashSet<string> AddWord(string word)
        {
            var sortedWord = sortStringCharachters(word);
            HashSet<string> returnVal = new HashSet<string>();

            rwl.EnterUpgradeableReadLock(); //may need to upgrade to writelock
            try
            {
                HashSet<string> values;

                if (!_knownWords.TryGetValue(sortedWord, out values))
                {
                    rwl.EnterWriteLock();
                    try
                    {
                        _knownWords.Add(sortedWord, new HashSet<string> { word });
                        addToFile(word);
                    }
                    finally
                    {
                        rwl.ExitWriteLock();
                    }
                }
                //If values contain input words, then there's no need to add it
                else if (!values.Contains(word))
                {
                    rwl.EnterWriteLock();
                    try
                    {
                        _knownWords[sortedWord].Add(word);
                        addToFile(word);
                    }
                    finally
                    {
                        rwl.ExitWriteLock();
                    }
                }
            }
            finally
            {
                rwl.ExitUpgradeableReadLock();
            }
            return _knownWords[sortedWord];
        }

    #region Creating the Dictionary
    //Creates the knownWord dictionary from array of strings
    private void CreateKnownWordsDic(string[] words)
    {
        foreach (var word in words)
        {
            AddToKnownWordsDictionary(word);
        }
    }

    // Adds word to the knownWords with key being the sorted string
    private void AddToKnownWordsDictionary(string word)
    {
        if (word == string.Empty)
        {
            return;
            
        }
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
    //Sorts the iput word by converting to CharArray, Sorting, and back to string
    private string sortStringCharachters(string word)
    {
        char[] sortedLettersArray = word.ToCharArray();
        Array.Sort(sortedLettersArray);
        return new string(sortedLettersArray);
    }

    //Write input word to the file of words
    private void addToFile(string word)
    {
        using (StreamWriter sw = File.AppendText(PATH))
        {
            sw.WriteLine(word);
        }
    }
    #endregion
}

}