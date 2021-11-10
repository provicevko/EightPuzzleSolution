using AStar.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AStar
{
    public class AlgTask<T> : IAlgInitializator<T>
    {
        public AlgTask(T initialState, T finalState)
        {
            InitialState = initialState;
            FinalState = finalState;
        }
        public T InitialState { get; set; }

        public T FinalState { get; set; }
    }
}
