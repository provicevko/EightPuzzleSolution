using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BFS;
using EightPuzzle.Enums;

namespace EightPuzzle.Services
{
    public interface IPuzzleSolver
    {
        IEnumerable<byte[]> FindSolution(byte[] startState, byte[] endState, EightPuzzle.Enums.Algorithm algorithm);
    }
}
