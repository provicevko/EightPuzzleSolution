using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EightPuzzle.Enums;

namespace EightPuzzle.Services
{
    public interface IPuzzleMovement
    {
        public IEnumerable<byte[]> ExpandMove(byte[] currentState);
        public byte[] Move(byte[] state, int index, MoveDirection direction);
    }
}
