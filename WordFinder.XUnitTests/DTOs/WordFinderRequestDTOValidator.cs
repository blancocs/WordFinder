using FluentAssertions;
using FluentValidation.TestHelper;
using Shouldly;
using WordFinder.Application.DTOs;

namespace WordFinder.XUnitTests.DTOs
{
    public  class WordFinderRequestDTOValidator
    {
        private readonly FindWordsQueryValidator _validator;

        public WordFinderRequestDTOValidator()
        {
            _validator = new FindWordsQueryValidator();
        }

        [Fact]
        public async Task ShouldHave_validationError_when_matrixEmpty()
        {

            var dto = new WordFinderRequestDTO { Matrix = null, WordStream = new List<string> { "Seba", "test" } };

            TestValidationResult<WordFinderRequestDTO> result = await _validator.TestValidateAsync(dto);

            result.ShouldHaveValidationErrorFor(x => x.Matrix);
        }

        [Fact]
        public async Task ShouldHave_validationError_when_matrixHave_emptyValues()
        {

            var dto = new WordFinderRequestDTO { Matrix = new List<string> {
                    "prueba",
                    "",
                    "test"

                 }
            , 
                WordStream = new List<string> { "Seba", "test" } };

            TestValidationResult<WordFinderRequestDTO> result = await _validator.TestValidateAsync(dto);

            result.ShouldHaveValidationErrorFor(x => x.Matrix);

            result.ToString().Should().Contain("empty values");

        }

        [Fact]
        public async Task ShouldHave_validationError_when_matrixRows_haveDifferentSize()
        {

            var dto = new WordFinderRequestDTO
            {
                Matrix = new List<string> {
                    "prueba",
                    "pruebin",
                    "test"

                 }
            ,
                WordStream = new List<string> { "Seba", "test" }
            };

            TestValidationResult<WordFinderRequestDTO> result = await _validator.TestValidateAsync(dto);

            result.ShouldHaveValidationErrorFor(x => x.Matrix);

            result.ToString().Should().Contain("same number of characters");
        }
    }
}
