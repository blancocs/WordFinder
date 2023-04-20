using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public ValidationException() : base("The following validation errors have occurred")
        {
            Errors = new List<string>();

        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            foreach (var failure in failures)
            {
                Errors.Add(failure.ErrorMessage);
            }
        }

        public List<string> Errors { get; }

    }
}
