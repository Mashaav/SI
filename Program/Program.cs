using Map;
using Core;
using System;
using System.Collections.Generic;
using System.Linq;
using Sztuczna_Inteligencja;
using Hetmani;
using System.Diagnostics;

namespace Program
{
    class Program
    {
        private static void SzukanieMiastaDFS()
        {
            var mapa = new MapServices();

            Console.WriteLine("Lista miast: ");
            for (int i = 0; i < mapa.Map.Count; i++)
            {
                Console.WriteLine(i + ". " + mapa.Map.ElementAt(i).Key.ToString());
            }

            Console.Write("\nWybierz miasto początkowe(numer): ");
            int poczatek = int.Parse(Console.ReadLine());

            Console.Write("Wybierz miasto końcowe(numer): ");
            int koniec = Int32.Parse(Console.ReadLine());

            DFS a = new DFS(mapa.Map, mapa.Map.ElementAt(poczatek).Key, mapa.Map.ElementAt(koniec).Key);
            a.Search();
            a.Print();
        }

        private static void SzukanieMiastaBestFS()
        {   
            int i = 1, pocz, kon;
            MapServices mapa = new MapServices();

            Console.WriteLine("Lista miast: ");
            foreach (var item in mapa.NodeMap)
            {
                Console.WriteLine( i.ToString() + " " + item.Name);
                i++;
            }

            Console.WriteLine("Wybierz miasto początkowe: ");
            pocz = int.Parse(Console.ReadLine());
            Node poczatek = mapa.NodeMap.ElementAt(pocz - 1);

            Console.Write("\nWybierz miasto końcowe: ");
            kon = int.Parse(Console.ReadLine());
            Node koniec = mapa.NodeMap.ElementAt(kon - 1);

            BestFS a = new BestFS(mapa.NodeMap.Find(m => m.Name == koniec.Name), mapa.NodeMap.Find(m => m.Name == poczatek.Name));
            a.Expand(poczatek);
            a.Print();
        }

        public static Node<State> TreeSearch<State>(IProblem<State> problem, IFringe<Node<State>> queue)
        {
            queue.Add(new Node<State>(problem.InitialState, null));
            while (!queue.Empty())
            {
                Node<State> node = queue.Remove();
                if (problem.IsGoal(node.State))
                {
                    return node;
                }
                IList<State> successors = problem.Expand(node.State);
                foreach (State st in successors)
                {
                    queue.Add(new Node<State>(st, node));
                }
            }
            return null;
        }

        public static void Przesuwanka_Start()
        {
            int[,] finalState = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 0 } };

            int[,] initialState = { { 0, 1, 3 }, { 4, 2, 6 }, { 7, 5, 8 } };

            Przesuwanka przesuwanka = new Przesuwanka(initialState, finalState);

            var result = TreeSearch<int[,]>(przesuwanka, new FIFOFringe<Node<int[,]>>());

            Stack<Node<int[,]>> path = new Stack<Node<int[,]>>();

            while (true)
            {
                path.Push(result);
                if (result.Parent != null) result = result.Parent as Node<int[,]>; else break;
            }

            foreach (var item in path)
            {
                for (int i = 0; i < item.State.GetLength(0); i++)
                {
                    for (int j = 0; j < item.State.GetLength(1); j++)
                    {
                        Console.Write(item.State[i, j] + " ");
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();
            }
            Console.ReadKey();
        }

        public static void Astar()
        {
            var mapa = new MapServices();
            Console.WriteLine("Lista miast: ");
            for (int i = 0; i < mapa.AStarNodeMap.Count; i++)
            {
                Console.WriteLine(i + ". " + mapa.AStarNodeMap.ElementAt(i).Name);
            }

            int startNum, endNum;

            Console.Write("\nWybierz miasto początkowe(numer): ");
            int.TryParse(Console.ReadLine(), out startNum);

            Console.Write("Wybierz miasto końcowe(numer): ");
            int.TryParse(Console.ReadLine(), out endNum);

            AStarNode start = mapa.AStarNodeMap.ElementAt(startNum);
            AStarNode end = mapa.AStarNodeMap.ElementAt(endNum);

            Astar search = new Astar(end);
            IList<AStarNode> route = search.Expand(start);
            foreach (var item in route)
            {
                Console.WriteLine(item.Name);
            }

        }

        public static void Hetmani()
        {
            Hetman h = new Hetman();
            Console.WriteLine("Podaj stan początkowy(x,y) hetmana. Wpisz wartość spoza zakresu 0-7, aby zrezygnować ze stanu początkowego.");
            int x, y;
            int.TryParse(Console.ReadLine(), out x);
            int.TryParse(Console.ReadLine(), out y);
            h.Search(new KeyValuePair<int, int>(x, y));
        }

        private static void Main(string[] args)
        {
            int i = int.MaxValue;
            int j = int.MaxValue;
            do
            {
                Console.WriteLine("1. Przesuwanka" + "\n2. Mapy \n3. Hetmani\n0. Wyjście");
                int.TryParse(Console.ReadLine().ToString(), out i);

                switch (i)
                {
                    case 1: //przesuwanka
                        Przesuwanka_Start();
                        break;

                    case 2: //mapy
                        Console.Clear();
                        Console.WriteLine("Mapa znajduje się w folderze solucji.");
                        do
                        {
                            Console.WriteLine("1. DFS\n2. Best First Search\n3. A*\n0. Wyjście");
                            int.TryParse(Console.ReadLine().ToString(), out j);

                            switch (j)
                            {
                                case 1:
                                    SzukanieMiastaDFS();
                                    break;

                                case 2:
                                    SzukanieMiastaBestFS();
                                    break;

                                case 3:
                                    Astar();
                                    break;
                            }
                        } while (j != 0);
                        break;

                    case 3: //hetmani
                        Hetmani();
                        break;

                    default:
                        break;
                }
            } while (i != 0);
        }
    }
}
