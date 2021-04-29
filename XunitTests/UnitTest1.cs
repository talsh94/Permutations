using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Permutations.Models;
using Permutations.Controllers;
using Moq;
using Xunit;


namespace XunitTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Get_ReturnsHashSet_WithAllPermutations()
        {
            //setup
            var mockKnownWords = new Mock<KnownWords>();
            mockKnownWords.Setup(repo => repo).ReturnsAsync(GetTestSessions());
        }

        private Dictionary<string, HashSet<string>> GetTestSessions()
        {
            var words = new Dictionary<string, HashSet<string>>();
            words.Add("abc", new HashSet<string> { "abc", "bac" });
            words.Add("add", new HashSet<string> { "dad", "add" });
            return words;
        }
    }
}
