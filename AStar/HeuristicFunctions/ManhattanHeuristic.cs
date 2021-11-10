using AStar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar.HeuristicFunctions
{
    public class ManhattanHeuristic : IHeuristicCalculator<byte[]>
    {
        private byte _width = 3;

        private readonly Dictionary<int, (int, int)> _tileExpectedPosDict = new();

        public ManhattanHeuristic(byte[] goalBoard)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    int val = goalBoard[row * _width + col];

                    _tileExpectedPosDict[val] = (row, col);
                }
            }
        }
        public double Calculate(byte[] state)
        {
            int result = 0;

            int expected = 0;

            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    int val = state[row * _width + col];
                    expected++;

                    var expectedPos = _tileExpectedPosDict[val];

                    if (val != 0 && val != expected)
                    {
                        result += Math.Abs(row - expectedPos.Item1) +
                            Math.Abs(col - expectedPos.Item2);
                    }
                }
            }

            return result;
        }
    }
}
