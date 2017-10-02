using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Map
{
    public class DFS : IProblem<string>
    {
        private Dictionary<string, List<string>> map;
        private List<string> expanded = new List<string>();
        private string goal = "";
        private int backIndex = 0;
        private List<string> printStack = new List<string>();

        public string Goal
        {
            get
            {
                return goal;

            }
            set
            {
                goal = value;
            }
        }

        public DFS(Dictionary<string, List<string>> _map, string _initialState, string _goal)
        {
            map = _map;
            InitialState = _initialState;
            Goal = _goal;
        }

        public string InitialState  
        {
            get
            {
                return expanded.First();
            }
            set
            {
                expanded = new List<string>();
                expanded.Add(value);
                printStack.Add(expanded.First());
            }
        }

        public IList<string> Expand(string state)
        {
            List<string> neighbours = map[state]; //lista sąsiadów danego miasta
            for (int i = 0; i < neighbours.Count; i++) //sprawdzanie czy wszystkie sąsiedzkie miasta zostały odwiedzone
            {
                string neighbour = expanded.Find(m => m == neighbours[i]); //sprawdź czy na liście expanded już jest takie miasto odwiedzone
                if (neighbours[i] != neighbour) //jeśli nie odwiedzono to dodaj do expanded i zakończ
                {
                    expanded.Add(neighbours[i]);
                    backIndex++;
                    printStack.Add(expanded.Last());
                    return expanded;
                }
            }// w przeciwnym wypadku cofnij się do poprzedniego miasta
            printStack.Add("*" + expanded.ElementAt(backIndex - 1));
            Expand(expanded.ElementAt(--backIndex));
            return expanded;
        }

        public bool IsGoal(string state)
        {
            if (state == goal)
                return true;
            else
                return false;
        }

        private bool SetsEqual(IList<string> set1, IList<string> set2)
        {
            HashSet<string> a = new HashSet<string>(set1);
            HashSet<string> b = new HashSet<string>(set2);
            return a.SetEquals(b);
        }

        public void Search()
        {
            IList<string> list = new List<string>();

            while (!IsGoal(expanded.Last()))
            {
                //lista dzięki której jestem w stanie sprawdzić czy zaszły zmiany
                #region FillList 
                if (list != null) list.Clear();
                for (int i = 0; i < expanded.Count; i++)
                {
                    list.Add(expanded[i]);
                }
                #endregion 

                Expand(expanded.Last());

                if (!SetsEqual(expanded, list))//jeśli nie zszedłem głębiej to cofam się do rodzica
                {
                    backIndex = expanded.Count - 1;
                }
            }
        }

        public void Print()
        {
            //Console.WriteLine("\n***SHORTSTACK***");
            //for (int i = 0; i < expanded.Count; i++)
            //{
            //    if (expanded.Count - 1 != i)
            //        Console.Write(expanded[i] + " -> ");
            //    else
            //        Console.Write(expanded[i]);
            //}

            Console.WriteLine("\n\n***STACK***");
            foreach (var item in printStack)
            {
                Console.Write(item);
                if (printStack.IndexOf(item) != printStack.Count - 1)
                    Console.Write(" -> ");
            }
            Console.WriteLine("\n");
        }
    }
}
