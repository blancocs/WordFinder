using FluentAssertions;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinder.Application.Features.FindWords.Queries;
using static WordFinder.Application.Features.FindWords.Queries.FindWordsQuery;

namespace WordFinder.XUnitTests.Features.FindWords.Queries
{
    public class FindWordsQueryTests
    {
        private FindWordsQueryHandler _queryHandler;
        public  FindWordsQueryTests()
        {
            _queryHandler = new FindWordsQueryHandler(null);
        }


        //validate that returns in order Test and Seba. Test appears twice -vertical in 4 column, horizontal last row- seba once in first line.
        [Fact]
        public async Task  FindWord_should_return_TestAndSeba()
        {
            //arrange
            var matrix = new List<String>()
            {
                "Sebapasdasd", 
                "eovteasdasd",
                "ftuersadagf",
                "oeasodfgfdg",
                "orztnfgfdgg",
                "Pruxbafgfdf",
                "qwehnfgdfgf",
                "bltqafgdfgf",
                "isfvmfgdfgg",
                "Testttryrty"
            };

            var wordstream = new List<String>() { "test", "seba" };

            //act 
            var FindWordsQuery = new FindWordsQuery { Matrix = matrix, WordStream = wordstream };

            var result = await _queryHandler.Handle(FindWordsQuery, default);

            result.MostRepeatedWords.Should().ContainInOrder(wordstream);


            }

        [Fact]
        public async Task FindWord_should_return_EmptyList()
        {
            //arrange
            var matrix = new List<String>()
            {
                "Sebapasdasd",
                "eovteasdasd",
                "ftuersadagf",
                "oeasodfgfdg",
                "orztnfgfdgg",
                "Pruxbafgfdf",
                "qwehnfgdfgf",
                "bltqafgdfgf",
                "isfvmfgdfgg",
                "Testttryrty"
            };

            var wordstream = new List<String>() { "estanolaencontras", "nichance" };

            //act 
            var FindWordsQuery = new FindWordsQuery { Matrix = matrix, WordStream = wordstream };

            var result = await _queryHandler.Handle(FindWordsQuery, default);

            result.MostRepeatedWords.ShouldBeEmpty();


        }
    }
    }
