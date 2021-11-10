using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BFS;
using EightPuzzle.Enums;

namespace EightPuzzle.Services
{
    public class PuzzleMovementService : IPuzzleMovement
    {
        private int _width;
        public PuzzleMovementService(int width)
        {
            _width = width <= 0 ? throw new ArgumentOutOfRangeException(nameof(width)) : width;
        }

        public IEnumerable<byte[]> ExpandMove(byte[] currentState)
        {
            int zeroIndex = Array.IndexOf(currentState,(byte)0);

            return MoveToAllSides(currentState, zeroIndex);
        }

        public byte[] Move(byte[] state, int index, MoveDirection direction) => direction switch
        {
            MoveDirection.Right => MoveRight(state, index),
            MoveDirection.Left => MoveLeft(state, index),
            MoveDirection.Up => MoveUp(state, index),
            MoveDirection.Down => MoveDown(state, index),
            _ => null
        };

        private IEnumerable<byte[]> MoveToAllSides(byte[] state, int index)
        {
            List<byte[]> newStates = new List<byte[]>();

            foreach (var direction in Enum.GetValues<MoveDirection>())
            {
                var newState = Move(state, index, direction);

                if(newState != null)
                    newStates.Add(newState);
            }

            return newStates;
        }

        private byte[] MoveRight(byte[] state, int index)
        {
            byte[] newState = null;
            if (index % _width < _width - 1)
            {
                newState = CopyState(state);
                (newState[index + 1], newState[index]) = (newState[index], newState[index + 1]);
            }

            return newState;
        }

        private byte[] MoveLeft(byte[] state, int index)
        {
            byte[] newState = null;
            if (index % _width > 0)
            {
                newState = CopyState(state);
                (newState[index - 1], newState[index]) = (newState[index], newState[index - 1]);
            }

            return newState;
        }

        private byte[] MoveUp(byte[] state, int index)
        {
            byte[] newState = null;
            if (index - _width >= 0)
            {
                newState = CopyState(state);
                (newState[index - _width], newState[index]) = (newState[index], newState[index - _width]);
            }

            return newState;
        }

        private byte[] MoveDown(byte[] state, int index)
        {
            byte[] newState = null;
            if (index + _width < state.Length)
            {
                newState = CopyState(state);
                (newState[index + _width], newState[index]) = (newState[index], newState[index + _width]);
            }

            return newState;
        }

        private byte[] CopyState(byte[] state) => state.Clone() as byte[];

    }
}
