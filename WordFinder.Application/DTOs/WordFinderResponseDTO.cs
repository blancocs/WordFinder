﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.Application.DTOs
{
    [ExcludeFromCodeCoverage]
    public class WordFinderResponseDTO
    {
        public List<string> MostRepeatedWords { get; set; }
    }
}
