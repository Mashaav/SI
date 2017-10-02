using Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Map
{
    public class AStarNode
    {
        private string _name;
        private bool _closed, _opened; // added to closed/opened list
        private AStarNode _parent;
        private Vector2D _position;
        private List<AStarNode> _neighbours;
        private double _g, _h, _f;

        public AStarNode(string name, Vector2D position, bool closed = false, bool opened = false, AStarNode parent = null)
        {
            _name = name;
            _position = position;
            _closed = closed;
            _opened = opened;
            _parent = parent;
        }

        public string Name { get { return _name; } }

        public bool Closed { get { return _closed; } set { _closed = value; } }

        public bool Opened { get { return _opened; } set { _opened = value; } }

        public AStarNode Parent { get { return _parent; } set { _parent = value; } }

        public List<AStarNode> Neighbours { get { return _neighbours; } set { _neighbours = value; } }

        public double F// suma 
        {
            get { return G + H; }
            set { _f = value; }
        }

        //odległość danego węzła od początku
        private double G { get { return _g; } set { _g = value; } }

        //odległość danego węzła do końca
        private double H { get { return _h; } set { _h = value; } }

        public Vector2D Position { get { return _position; } private set { _position = value; } }

        private void SetG(Vector2D start)
        {
            G = Astar.Heuristic(start, Position);
        }

        private void SetH(Vector2D end)
        {
            H = Astar.Heuristic(end, Position);
        }

        public void ComputeF(Vector2D start, Vector2D end)
        {
            SetG(start);
            SetH(end);
            F = G + H;
        }
    }

    public class Astar : IProblem<AStarNode>
    {
        private List<AStarNode> _openList, _closedList, _stackTrace; // closed -> expanded, odwiedzone, open -> możliwe do odwiedzenia
        private AStarNode _goal, _initialState;
        private static int _chosenHeuristic = 0;
        private enum _heuristic { ManhattanDistance , other};

        public Astar(AStarNode goal)
        {
            OpenList = new List<AStarNode>();
            ClosedList = new List<AStarNode>();
            StackTrace = new List<AStarNode>();
            Goal = goal;
        }

        public static int ChosenHeuristic
        {
            get { return _chosenHeuristic; }
            set { _chosenHeuristic = value; }
        }

        public static double Heuristic(Vector2D destination, Vector2D position)
        {
            switch(ChosenHeuristic)
            {
                case (int)_heuristic.ManhattanDistance:
                    return Math.Abs(position.X - destination.X) + Math.Abs(position.Y - destination.Y);

                case (int)_heuristic.other:
                    return 0;

                default: return 0;
            }
        }

        public List<AStarNode> OpenList //lista węzłów dodawanych przy zamykaniu
        {
            get { return _openList; }
            private set { _openList = value; }
        }

        public List<AStarNode> ClosedList
        {
            get { return _closedList; }
            private set { _closedList = value; }
        }

        public List<AStarNode> StackTrace // przebyta ścieżka
        {
            get { return _stackTrace; }
            private set { _stackTrace = value; }
        }

        private void AddInitial(AStarNode initialState)
        {
            InitialState = initialState;
            InitialState.Closed = true; 
            foreach (var item in InitialState.Neighbours) //dodaj wszystkich sąsiadów do listy otwartej
            {
                item.Opened = true;
                item.Parent = InitialState;
                item.ComputeF(InitialState.Position, Goal.Position);
                AddToOpen(item);
            }
            ClosedList.Add(InitialState); //dodaj do listy zamkniętej
        }

        public AStarNode InitialState
        {
            get
            {
                return _initialState;
            }
            private set
            {
                _initialState = value;
            }
        }

        public AStarNode Goal
        {
            get
            {
                return _goal;
            }
            private set
            {
                _goal = value;
            }
        }

        public void ManageNeighbours(AStarNode state)
        {
            foreach (var item in state.Neighbours)
            {
                if (!item.Closed)
                {
                    if (!item.Opened)//element, spoza listy zamkniętych i otwartych
                    {
                        item.Parent = state; // zapisz rodzica
                        item.Opened = true;
                        item.ComputeF(InitialState.Position, Goal.Position); //oblicz współczynnik F
                        AddToOpen(item);//dodaj do otwartych
                    }
                    else//element już znajduje się na liście otwartych
                    {
                        AStarNode tmp = OpenList.Find(p => p.Name == item.Name); //znajdź na liście ten element
                        if (tmp != null && item.F < tmp.F) // odśwież współczynnik F
                        {
                            item.Parent = tmp.Parent = state;
                            item.ComputeF(InitialState.Position, Goal.Position);
                            tmp.F = item.F;
                        }
                    }
                }
            }
        }

        public bool IsGoal(AStarNode state)
        {
            if (state == Goal)
                return true;
            else
                return false;
        }

        private void AddToClosed(AStarNode s)
        {
            ClosedList.Add(s);
        }

        private void AddToOpen(AStarNode s)
        {
            OpenList.Add(s);
        }

        private void RemoveFirst(List<AStarNode> list)
        {
            list.RemoveAt(0);
        }

        private void RemoveLast(List<AStarNode> list)
        {
            list.Remove(list.Last());
        }

        public IList<AStarNode> Expand(AStarNode state)
        {
            AddInitial(state); // dodaj startowy element do listy zamkniętych(odwiedzonych)
            AStarNode tmp;
            while (!IsGoal(ClosedList.Last()))
            {
                OpenList.OrderBy(p => p.F); //lista otwartych posortowana wg najmniejszej wagi współczynnika F
                tmp = OpenList.First(); //weź pierwszy element z otwartej listy(najlepiej prosperujący)
                RemoveFirst(OpenList);//przenieś do zamkniętej
                tmp.Opened = false;
                tmp.Closed = true;
                AddToClosed(tmp);
                ManageNeighbours(ClosedList.Last()); //dodaj sąsiadów do otwartej listy lub zaktualizuj wsp F i rodzica
            }

            AStarNode parent = Goal; //uzupełnij listę ze ścieżką poprzez wybranie elementu docelowego i rekursywnie dodaj wszystkie elementy połączone
            while(parent != null)
            {
                StackTrace.Insert(0,parent);
                parent = parent.Parent;
            }
            return StackTrace;
        }

        private static void Main()
        {

        }
    }

}