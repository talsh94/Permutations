using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Permutations.Models;

namespace Permutations.Controllers
{
 

    #region PermutationController

    //TODO: need to change this probably
    [Route("api/values/")]
    [ApiController]

    public class PermutationsController : Controller
    {
        //constants
        private const string BAD_REQUEST_MSG = "Invalid input. Input needs to be lowercase English characters";

        private readonly KnownWordsDictionary _knownWords;

        public PermutationsController(KnownWordsDictionary knownWords)
        {
            _knownWords = knownWords;
        }
        #endregion

        // GET: api/values/"abc"
        [HttpGet("{word}")]

        public IActionResult GetPermutaions(string word)
        {
            if (!IsValid(word))
            {
                //todo: deal with invalid input
                return BadRequest(BAD_REQUEST_MSG);
            }
            return Ok(_knownWords.GetPermutations(word));
        }

        //// POST: api/values/
        //[HttpPost]
        //public async Task<ActionResult<string>> PostPermutation(string word)
        //{
        //    if (!IsValid(word))
        //    {
        //        //todo: deal with invalid input
        //        return BadRequest(BAD_REQUEST_MSG);
        //    }

        //    var wordLetters = word.ToCharArray().OrderBy(c => c).ToString();

        //    if (!_knownWords.ContainsKey(wordLetters))
        //    {
        //        _knownWords.Add(wordLetters, new HashSet<string> { word });
        //    }
        //    else
        //    {
        //        _knownWords[wordLetters].Add(word);
        //    }

        //    //TODO: if i need to write to th file- add this here with File.AppendText or WriteLines...

        //    //TODO: is this correct???
        //    //todo: do i need to return this when the word already exists...? i dont even checkthis

        //    return CreatedAtAction(nameof(GetPermutaions), new { id = wordLetters }, word);

        //}

        private bool IsValid(string word)
        {
            foreach (Char c in word)
            {
                if (!Char.IsLower(c))
                {
                    return false;
                }
            }
            return true;
        }
    }
}