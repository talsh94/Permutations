# Permutations
Permutations is a Cross Platform Web Service in C#.
On startUp the service loads a list of words to its memory from a given file (check out the "To Run" section of the README). These
are our initial Known Words.
The words are loaded into a dictionary where the Key is a string and the value is a HashSet<string> of all the known words that are a 
permutation of the key. The key is a string that is the original word, but with sorted characters.

The Web service supports 2 requests:
GET: /api/values/{word} – returns all known words that are a permutation of the given word.
  (This is simply implemented with sorting the input word and looking it up in the dictionary. The value of the 
  sorted-word-key is a set of all the known words that are a permutation of the given word)
  
POST:/api/values/ - the content of this request is a string that needs to be added to the list of known words by the service.
  (This is implemented by adding the value (and if the key doesn't yet exist in the knownWords- the key as well to the 
  KnownWords model, and to the file ass well. Duplicated will not be added)

Note on reader writer locks:
  Uses ReaderWriterLockSlim Class - alock that is used to manage access to a resource, allowing multiple threads for reading or
  exclusive access for writing.
  Microsoft documentation recommends using the ReaderWriterLockSlim instead of the ReaderWriterLock. This is due to better perfomance
  and better deadlock prevention.

**********************************************************************************************************************************************
To Run:
- Change the PATH constant in the Permutation/Model/KnownWords.cs file to a txt file. The file should consist of words in lowercase English.
  - If the words are seperated by a character that is not a newline then change the SPLIT_BY constant
