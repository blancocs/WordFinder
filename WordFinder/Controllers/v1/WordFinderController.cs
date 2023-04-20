using Microsoft.AspNetCore.Mvc;
using WordFinder.Application.DTOs;
using WordFinder.Application.Features.FindWords.Queries;

namespace WordFinder.Controllers.v1
{
    public class WordFinderController : BaseController
    {
        [HttpPost()]
        public async Task<IActionResult> Get([FromBody] WordFinderRequestDTO filters)
        {
            return Ok(await Mediator.Send(new FindWordsQuery { Matrix = filters.Matrix, WordStream = filters.WordStream}));
            
        }
    }
}
