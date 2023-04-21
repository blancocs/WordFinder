using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordFinder.Application.AhoCorasick;
using WordFinder.Application.Interfaces;
using WordFinder.infrastructure.Helpers;

namespace WordFinder.Application.Services
{
    public class WordsFinder : IWordsFinder
    {
        private readonly char[,] _matrix;
        private Trie _trie;
        private readonly int _numThreads = Environment.ProcessorCount;

        
        public WordsFinder(IEnumerable<string> matrix)
        {
            _matrix = MatrixUtilities.ConvertMatrixToCharArray(matrix);
        }

        /// <summary>
        /// this method is basically recieving the matrix as a parameter in the constructor.
        /// then, with the words we need to search for, we create a trie
        /// we use concurrentDictionary because we are using Multi Task to do the search.
        /// passing as the search indexes, trie.root, conccurent dictionary, and search directions (up, down, left to right, etc).
        /// </summary>
        
        public async Task<IEnumerable<string>> Find(IEnumerable<string> wordStream)
        {
            //changing all words to lower case.
            this._trie = new Trie(wordStream.Select(x=>x.ToLower()).ToList());

            var foundWords = new ConcurrentDictionary<string, int>();
            var tasks = new List<Task>();

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    var x = i;
                    var y = j;

                    tasks.Add(Task.Factory.StartNew(() => Search(x, y, _trie.Root, foundWords, new HashSet<(int, int)>(), GetSearchDirections())));
                    if (tasks.Count >= _numThreads * 2)
                    {
                        await Task.WhenAll(tasks);
                        tasks.Clear();
                    }
                }
            }

            await Task.WhenAll(tasks);

            return foundWords.OrderByDescending(x => x.Value).Take(10).Select(x => x.Key).ToList();
        }

        private void Search(int x, int y, TrieNode currentNode, ConcurrentDictionary<string, int> foundWords, HashSet<(int, int)> visited, List<(int dx, int dy)> directions)
        {
            if (visited.Contains((x, y)))
            {
                return;
            }

            visited.Add((x, y));

            var c = _matrix[x, y];
            currentNode = currentNode.GetNode(c);

            if (currentNode == null)
            {
                visited.Remove((x, y));
                return;
            }

            if (currentNode.IsWord)
            {
                var word = currentNode.Word;
                foundWords.AddOrUpdate(word, 1, (_, count) => count + 1);
            }

            foreach (var direction in directions)
            {
                var newX = x + direction.dx;
                var newY = y + direction.dy;

                if (newX >= 0 && newY >= 0 && newX < _matrix.GetLength(0) && newY < _matrix.GetLength(1))
                {
                    Search(newX, newY, currentNode, foundWords, visited, directions);
                }
            }
        }

         private List<(int dx, int dy)> GetSearchDirections()
        {
            List<(int dx, int dy)> directions = new List<(int dx, int dy)>
            {
                (dx: 1, dy: 0),     // horizontal left to right
                (dx: -1, dy: 0),    // horizontal right lo left
                (dx: 0, dy: 1),     // vertical up to down.
                (dx: 0, dy: -1)     // vertical down to up.
            };
                return directions;
        }
    }
}
