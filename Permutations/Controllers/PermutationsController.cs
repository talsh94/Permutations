using System;
using Microsoft.AspNetCore.Mvc;
using Permutations.Models;

namespace Permutations.Controllers
{
    [Route("api/values/")]
    [ApiController]

    //Class that controls the Post and Get request of this service
    public class PermutationsController : Controller
    {
        #region constants
        private const string BAD_REQUEST_MSG = "Invalid input. Input needs to be lowercase English characters";
        #endregion

        private readonly KnownWords _knownWords;

        //called by service.AddControllers().
        public PermutationsController(KnownWords knownWords)
        {
            _knownWords = knownWords;
        }

        //GetPermutation returns a hashset of all the known words that are a
        //permutation of the given word
        //return: IActionResult return type- appropriate when multiple ActionResult
        //return. The ActionResult types represent various HTTP status codes.
        // GET: api/values/abc
        [HttpGet("{word}")]
        public IActionResult GetPermutaion(string word)
        {
            if (!IsValid(word))
            {
                return BadRequest(BAD_REQUEST_MSG);
            }
            return Ok(_knownWords.GetPermutation(word));
        }

        //PostPermutation adds content of the request to the list of known words
        //return: IActionResult return type- appropriate when multiple ActionResult
        //return. The ActionResult types represent various HTTP status codes.
        // POST: api/values/
        [HttpPost]
        public IActionResult PostPermutation([FromBody] string word)
        {
            if (!IsValid(word))
            {
                return BadRequest(BAD_REQUEST_MSG);
            }
            var permuationsOfWord = _knownWords.AddWord(word);
            return CreatedAtAction(nameof(GetPermutaion), new { word = word }, word);
        }

        //Checks input is lowercase english
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