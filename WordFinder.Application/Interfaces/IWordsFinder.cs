using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.Application.Interfaces
{
    public interface IWordsFinder
    {
        Task<IEnumerable<string>> Find(IEnumerable<string> wordstream);
    }
}
