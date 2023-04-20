using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordFinder.infrastructure.Helpers
{
    public static class MatrixUtilities
    {
        public static char[,] ConvertMatrixToCharArray(IEnumerable<string> matrix)
        {
            var numRows = matrix.Count();
            var numCols = matrix.First().Length;
            var result = new char[numRows, numCols];

            for (var i = 0; i < numRows; i++)
            {
                for (var j = 0; j < numCols; j++)
                {
                    result[i, j] = matrix.ElementAt(i)[j];
                }
            }

            return result;
        }


        public static IEnumerable<char[,]> DivideMatrix(char[,] matrix, int sectionSize)
        {
            if (sectionSize <= 0 || sectionSize > matrix.GetLength(0) || sectionSize > matrix.GetLength(1))
            {
                throw new ArgumentException("Invalid section size.");
            }

            for (int i = 0; i < matrix.GetLength(0); i += sectionSize)
            {
                for (int j = 0; j < matrix.GetLength(1); j += sectionSize)
                {
                    var section = new char[Math.Min(sectionSize, matrix.GetLength(0) - i), Math.Min(sectionSize, matrix.GetLength(1) - j)];

                    for (int x = 0; x < section.GetLength(0); x++)
                    {
                        for (int y = 0; y < section.GetLength(1); y++)
                        {
                            section[x, y] = matrix[i + x, j + y];
                        }
                    }

                    yield return section;
                }
            }
        }
    }
}
