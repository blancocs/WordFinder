using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinder.Application.DTOs;
using WordFinder.Application.Interfaces;
using WordFinder.Application.Services;

namespace WordFinder.Application.Features.FindWords.Queries
{
    public class FindWordsQuery : IRequest<WordFinderResponseDTO>
    {
        public IEnumerable<string> Matrix { get; set; }
        public IEnumerable<string> WordStream { get; set; }

        public class FindWordsQueryHandler : IRequestHandler<FindWordsQuery, WordFinderResponseDTO>
        {
            private readonly IMapper _mapper;
            
            public FindWordsQueryHandler(IMapper mapper) 
            {
                _mapper = mapper;

            }

            public async Task<WordFinderResponseDTO> Handle(FindWordsQuery request, CancellationToken cancellationToken)
            {
                var _wordsFinder = new WordsFinder(request.Matrix);
                

                var response = new WordFinderResponseDTO();
                var res =  await _wordsFinder.Find(request.WordStream);

                response.MostRepeatedWords = res.ToList();

                return (response);
            }
        }
    }
}
