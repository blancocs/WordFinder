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
        private readonly ConcurrentDictionary<string, int> wordCounts = new ConcurrentDictionary<string, int>();
        
        public WordsFinder(IEnumerable<string> matrix)
        {
            _matrix = MatrixUtilities.ConvertMatrixToCharArray(matrix);
        }

        public async Task<IEnumerable<string>> Find(IEnumerable<string> wordStream)
        {
            //changing all words to lower case.
            this._trie = new Trie(wordStream.Select(x=>x.ToLower()).ToList());

            var foundWords = new Dictionary<string, int>();

            for (int i = 0; i < _matrix.GetLength(0); i++)
            {
                for (int j = 0; j < _matrix.GetLength(1); j++)
                {
                    Search(i, j, _trie.Root, foundWords, new HashSet<(int, int)>(), GetSearchDirections());
                }
            }

            return foundWords.OrderByDescending(x => x.Value).Take(10).Select(x => x.Key).ToList();
        }

        private void Search(int x, int y, TrieNode currentNode, Dictionary<string, int> foundWords, HashSet<(int, int)> visited, List<(int dx, int dy)> directions)
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
                if (!foundWords.ContainsKey(word))
                {
                    foundWords[word] = 0;
                }
                foundWords[word]++;
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
                (dx: 1, dy: 0),     // horizontal hacia la derecha
                (dx: -1, dy: 0),    // horizontal hacia la izquierda
                (dx: 0, dy: 1),     // vertical hacia abajo
                (dx: 0, dy: -1)     // vertical hacia arriba
            };
                return directions;
        }
    }
}
