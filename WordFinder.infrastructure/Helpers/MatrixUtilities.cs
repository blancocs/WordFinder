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
                    result[i, j] = matrix.ElementAt(i).ToLower()[j];
                }
            }

            return result;
        }
    }
}
