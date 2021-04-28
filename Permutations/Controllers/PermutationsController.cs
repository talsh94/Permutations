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
    [Route("api/values/")]
    [ApiController]

    public class PermutationsController : Controller
    {
        //constants
        private const string BAD_REQUEST_MSG = "Invalid input. Input needs to be lowercase English characters";

        private readonly KnownWords _knownWords;

        public PermutationsController(KnownWords knownWords)
        {
            _knownWords = knownWords;
        }
        #endregion

        // GET: api/values/abc
        [HttpGet("{word}")]

        public IActionResult GetPermutaion(string word)
        {
            if (!IsValid(word))
            {
                //todo: deal with invalid input
                return BadRequest(BAD_REQUEST_MSG);
            }
            return Ok(_knownWords.GetPermutation(word));
        }

        // POST: api/values/
        [HttpPost]
        public IActionResult PostPermutation(string word)
        {
            if (!IsValid(word))
            {
                //todo: deal with invalid input
                return BadRequest(BAD_REQUEST_MSG);
            }
            var permuationsOfWord = _knownWords.PostPermutation(word);
            return CreatedAtAction(nameof(GetPermutaion), new { word = word }, permuationsOfWord);
        }

        //This method checks validity of input word-needs to be lowercase english
        private bool IsValid(string word)
        {
            if (word == null)
            {
                return false;
            }
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