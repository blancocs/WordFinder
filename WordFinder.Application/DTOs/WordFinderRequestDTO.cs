using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WordFinder.Application.DTOs
{
    public class WordFinderRequestDTO
    {
        public IEnumerable<String> Matrix { get; set; }
        public IEnumerable<String> WordStream { get; set; }
    }

    public class FindWordsQueryValidator : AbstractValidator<WordFinderRequestDTO>
    {
        private const int MaxSize = 64;
        public FindWordsQueryValidator()
        {

            //validate empty values, null, values.
            RuleFor(wf => wf.Matrix).Cascade(CascadeMode.StopOnFirstFailure)
                .NotNull().WithMessage("{propertyName cannot be empty")
                .Must(m => m.Any()).WithMessage("{propertyName cannot be empty}")
                .Must(m => m.All(row => !string.IsNullOrWhiteSpace(row)))
                .WithMessage("The matrix cannot contain empty values")
                
                //validate max size and max length.
                .Must(m => m.Count() <= 64 && m.All(s => s.Length <= 64))
                .WithMessage($"The matrix size cannot exceed {MaxSize} x {MaxSize}")


                //validate all rows have same length.
                .Must(matrix => matrix.Select(row => row.Length).Distinct().Count() == 1)
                .WithMessage("All rows must have the same number of characters.");

            //validate wordstream not empty
            RuleFor(wf => wf.WordStream).NotNull().WithMessage("{propertyName cannot be null")
                .NotEmpty().WithMessage("{propertyName cannot be empty");
        }
    }

}
