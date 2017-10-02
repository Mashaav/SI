using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Map
{

    public class BestFS : IProblem<Node>
    {
        private Node _goal, _initialState;
        private List<Node> _map;
        private List<string> costList = new List<string>();
        private List<Node> expanded = new List<Node>();
        private double cost;

        public BestFS(Node goal, Node initialState)
        {
            _goal = goal;
            InitialState = initialState;
            _map = new MapServices().NodeMap;
            cost = 0;
        }

        private Node BestAvailable(Dictionary<Node, double> list)
        {
            Dictionary<Node, double> List = new Dictionary<Node, double>();
            Dictionary<Node, double> toDrop = new Dictionary<Node, double>();

            //kopia listy sąsiadów
            foreach (var item in list)
            {
                List.Add(item.Key, item.Value);
            }

            //dodanie już odwiedzonych sąsiadów do listy wyrzucanych
            foreach (var item in List)
            {
                if(item.Key.Seen)
                    toDrop.Add(item.Key, item.Value);
            }
            
            //usuwanie odwiedzonych sąsiadów
            foreach (var item in toDrop)
            {
                var a = List.First(m=>m.Key.Name == item.Key.Name);
                List.Remove(a.Key);
            }

            if (List.Count > 0)
            {
                var a = List.First(m => m.Value == List.Values.Min());
                return a.Key;
            }
            else return null;
        }

        public Node InitialState
        {
            get
            {
                return _initialState;
            }
            set
            {
                _initialState = value;
            }
        }

        public IList<Node> Expand(Node state)
        {

            if (!IsGoal(state)) // jeśli state nie jest celem
            {
                Dictionary<Node, double> StateNeighbours = state.Neighbours; //stwórz listę sąsiadów
                Node n = BestAvailable(StateNeighbours); //wybierz najbliższego sąsiada

                if (n != null && !n.Seen) //jeśli najlepszy sąsiad nie był odwiedzony
                {
                    if (state.Parent == null)
                    {
                        if (expanded.Count > 0) state.Parent = expanded.Last(); //ustaw poprzednika na rodzica aktualnego miasta
                        else state.Parent = null;
                    }
                    state.Seen = true; // ustaw aktualne miasto jako odwiedzone
                    expanded.Add(state); // i dodaj do listy expanded
                    costList.Add("+ " + StateNeighbours.First(m => m.Key.Name == n.Name).Value); 
                    cost += StateNeighbours.First(m => m.Key.Name == n.Name).Value; // oblicz aktualny koszt
                    return Expand(n); // odwiedź najlepszego sąsiada
                }
                else //jeśli najlepszy sąsiad był odwiedzony
                {
                    if (!state.Seen)
                    {
                        state.Seen = true; // ustaw jako odwiedzone aktualne miasto
                        state.Parent = expanded.Last(); //ustaw poprzednika jako rodzica aktualnego miasta
                    }
                    costList.Add("- " + StateNeighbours.First(m => m.Key.Name == state.Parent.Name).Value);
                    cost -= StateNeighbours.First(m => m.Key.Name == state.Parent.Name).Value;
                    expanded.Add(state); // dodaj aktualne miasto do odwiedzonych
                    return Expand(state.Parent); //wróć do rodzica
                }
            }
            else
            {
                expanded.Add(_goal);
            }
            return expanded;
        }

        public bool IsGoal(Node state)
        {
            if (state == _goal) return true;
            else return false;
        }

        public void Print()
        {
            Console.Write("\n");
            for(int i = 0; i < expanded.Count; i++)
            {
                if (i != expanded.Count - 1) Console.Write(expanded[i].Name + " -> (" + costList[i] + ")" + " -> ");
                else
                {
                    Console.Write(expanded[i].Name);
                    Console.WriteLine("\n\nTotal Cost: {0}km", cost);
                }

            }
            Console.Write("\n");
        }
    }
}
