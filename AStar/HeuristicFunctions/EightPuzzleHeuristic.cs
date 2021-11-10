using AStar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar.HeuristicFunctions
{
    public class EightPuzzleHeuristic : IHeuristicCalculator<byte[]>
    {
        private byte _width;
        private readonly byte[] _goalBoard;
        public EightPuzzleHeuristic(byte[] goalBoard)
        {
            if (goalBoard == null || goalBoard.Length == 0)
                throw new ArgumentException(nameof(goalBoard));

            _width = (byte)Math.Sqrt(goalBoard.Length);
            _goalBoard = goalBoard;
        }
        public double Calculate(byte[] state)
        {
            int result = 0;
            for (int row = 0; row < _width; row++)
                for (int col = 0; col < _width; col++)
                {
                    int value = state[row * _width + col];
                    int goal = _goalBoard[row * _width + col];

                    if (value != goal)
                        result++;
                }
            return result;
        }
    }
}
