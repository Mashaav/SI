using Core;
using System.Collections.Generic;

namespace Sztuczna_Inteligencja
{
    public class Przesuwanka : IProblem<int[,]>
    {
        private int[,] initialState;
        private int[,] finalState;
        private List<int[,]> expanded = new List<int[,]>();

        public Przesuwanka(int[,] initialState, int[,] finalState)
        {
            this.initialState = initialState;
            this.finalState = finalState;
        }

        public int[,] InitialState
        {
            get
            {
                return initialState;
            }
        }

        public IList<int[,]> Expand(int[,] state)
        {
            int up, down, left, right;
            List<int[,]> expandList = new List<int[,]>();

            for (int i = 0; i < state.GetLength(0); i++)
            {
                for (int j = 0; j < state.GetLength(1); j++)
                {
                    if (state[i, j] == 0)
                    {
                        if (i - 1 >= 0)
                        {
                            up = state[i - 1, j];
                            int[,] tmp = state.Clone() as int[,];
                            tmp[i, j] = up;
                            tmp[i - 1, j] = 0;
                            expandList.Add(tmp);
                        }
                        if (i + 1 < state.GetLength(0))
                        {
                            down = state[i + 1, j];
                            int[,] tmp = state.Clone() as int[,];
                            tmp[i, j] = down;
                            tmp[i + 1, j] = 0;
                            expandList.Add(tmp);
                        }
                        if (j - 1 >= 0)
                        {
                            left = state[i, j - 1];
                            int[,] tmp = state.Clone() as int[,];
                            tmp[i, j] = left;
                            tmp[i, j - 1] = 0;
                            expandList.Add(tmp);
                        }
                        if (j + 1 < state.GetLength(1))
                        {
                            right = state[i, j + 1];
                            int[,] tmp = state.Clone() as int[,];
                            tmp[i, j] = right;
                            tmp[i, j + 1] = 0;
                            expandList.Add(tmp);
                        }
                    }
                }
            }
            return expandList;
        }

        public bool IsGoal(int[,] state)
        {
            if (finalState.GetLength(0) != state.GetLength(0) || finalState.GetLength(1) != state.GetLength(1)) return false;
            for (int i = 0; i < finalState.GetLength(0); i++)
            {
                for (int j = 0; j < finalState.GetLength(1); j++)
                {
                    if (finalState[i, j] != state[i, j]) return false;
                }
            }
            return true;
        }

        private static void Main()
        {

        }
    }
}
