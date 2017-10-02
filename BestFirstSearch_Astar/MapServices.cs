using Map;
using System.Collections.Generic;

namespace Core
{
    public class MapServices
    {
        private Dictionary<string, List<string>> map = new Dictionary<string, List<string>>();
        private Dictionary<string, Dictionary<string, double>> distanceMap = new Dictionary<string, Dictionary<string, double>>();
        private List<Node> _nodeMap = new List<Node>();
        private List<AStarNode> _AStarNodeMap = new List<AStarNode>();

        public List<Node> NodeMap
        {
            get
            {
                return _nodeMap;
            }
            set
            {
                _nodeMap = value;
            }
        }

        public List<AStarNode> AStarNodeMap
        {
            get
            {
                return _AStarNodeMap;
            }
            set
            {
                _AStarNodeMap = value;
            }
        }

        public Dictionary<string, List<string>> Map
        {
            get
            {
                return map;
            }
            set
            {
                map.Clear();
                map = value;
            }
        }

        public Dictionary<string, Dictionary<string, double>> DistanceMap
        {
            get
            {
                return distanceMap;
            }
            set
            {
                distanceMap = value;
            }
        }

        public MapServices()
        {
            #region MapDefinition
            map.Add("Oradea", new List<string>() { "Zerind", "Sibiu" });
            map.Add("Zerind", new List<string>() { "Oradea", "Arad" });
            map.Add("Arad", new List<string>() { "Timisoara", "Zerind", "Sibiu" });
            map.Add("Timisoara", new List<string>() { "Arad", "Lugoj" });
            map.Add("Lugoj", new List<string>() { "Timisoara", "Mehadia" });
            map.Add("Mehadia", new List<string>() { "Lugoj", "Drobeta" });
            map.Add("Drobeta", new List<string>() { "Mehadia", "Craiova" });
            map.Add("Craiova", new List<string>() { "Drobeta", "Rimnicu Vilcea", "Pitesti" });
            map.Add("Rimnicu Vilcea", new List<string>() { "Sibiu", "Pitesti", "Craiova" });
            map.Add("Sibiu", new List<string>() { "Arad", "Oradea", "Rimnicu Vilcea", "Fagaras" });
            map.Add("Pitesti", new List<string>() { "Rimnicu Vilcea", "Craiova", "Bucharest" });
            map.Add("Fagaras", new List<string>() { "Sibiu", "Bucharest" });
            map.Add("Bucharest", new List<string>() { "Pitesti", "Giurgiu", "Urziceni", "Fagaras" });
            map.Add("Giurgiu", new List<string>() { "Bucharest" });
            map.Add("Urziceni", new List<string>() { "Bucharest", "Hirsova", "Vaslui" });
            map.Add("Hirsova", new List<string>() { "Eforie", "Urziceni" });
            map.Add("Eforie", new List<string>() { "Hirsova" });
            map.Add("Vaslui", new List<string>() { "Urziceni", "Iasi" });
            map.Add("Iasi", new List<string>() { "Vaslui", "Neamt" });
            map.Add("Neamt", new List<string>() { "Iasi" });
            #endregion
            
            #region DistanceMapDefinition
            distanceMap.Add
                ("Oradea", new Dictionary<string, double>()
                    {
                        { "Zerind", 71 },
                        { "Sibiu", 151 }
                    }
                );
            distanceMap.Add
                ("Zerind", new Dictionary<string, double>()
                    {
                        { "Oradea", 71 },
                        { "Arad", 75 }
                    }
                );
            distanceMap.Add
                ("Arad", new Dictionary<string, double>()
                    {
                        { "Timisoara", 118 },
                        { "Zerind", 75 },
                        { "Sibiu", 140 }
                    }
                );
            distanceMap.Add
                ("Timisoara", new Dictionary<string, double>()
                    {
                        { "Arad", 118 },
                        { "Lugoj", 111 }
                    }
                );
            distanceMap.Add
                ("Lugoj", new Dictionary<string, double>()
                    {
                        { "Timisoara", 111 },
                        { "Mehadia", 70 }
                    }
                );
            distanceMap.Add
                ("Mehadia", new Dictionary<string, double>()
                    {
                        { "Lugoj", 70 },
                        { "Drobeta", 75 }
                    }
                );
            distanceMap.Add
                ("Drobeta", new Dictionary<string, double>()
                    {
                        { "Mehadia", 75 },
                        { "Craiova", 120 }
                    }
                );
            distanceMap.Add
                ("Craiova", new Dictionary<string, double>()
                    {
                        { "Drobeta", 120 },
                        { "Rimnicu Vilcea", 146 },
                        { "Pitesti", 138 }
                    }
                );
            distanceMap.Add
                ("Rimnicu Vilcea", new Dictionary<string, double>()
                    {
                        { "Craiova", 146 },
                        { "Sibiu", 80 },
                        { "Pitesti", 97 }
                    }
                );
            distanceMap.Add
                ("Sibiu", new Dictionary<string, double>()
                    {
                        { "Oradea", 151 },
                        { "Rimnicu Vilcea", 80 },
                        { "Fagaras", 99 },
                        { "Arad", 140 }
                    }
                );
            distanceMap.Add
                ("Pitesti", new Dictionary<string, double>()
                    {
                        { "Rimnicu Vilcea", 97 },
                        { "Craiova", 138 },
                        { "Bucharest", 101 }
                    }
                );
            distanceMap.Add
                ("Fagaras", new Dictionary<string, double>()
                    {
                        { "Sibiu", 99 },
                        { "Bucharest", 211 }
                    }
                );
            distanceMap.Add
                ("Bucharest", new Dictionary<string, double>()
                    {
                        { "Pitesti", 101 },
                        { "Fagaras", 211 },
                        { "Giurgiu", 90 },
                        { "Urziceni", 85 }
                    }
                );
            distanceMap.Add
                ("Giurgiu", new Dictionary<string, double>()
                    {
                        { "Bucharest", 90 }
                    }
                );
            distanceMap.Add
                ("Urziceni", new Dictionary<string, double>()
                    {
                        { "Bucharest", 90 },
                        { "Hirsova", 98 },
                        { "Vaslui", 142 }
                    }
                );
            distanceMap.Add
                ("Hirsova", new Dictionary<string, double>()
                    {
                        { "Urziceni", 98 },
                        { "Eforie", 86 }
                    }
                );
            distanceMap.Add
                ("Eforie", new Dictionary<string, double>()
                    {
                        { "Hirsova", 86 }
                    }
                );
            distanceMap.Add
                ("Vaslui", new Dictionary<string, double>()
                    {
                        { "Urziceni", 142 },
                        { "Iasi", 92 }
                    }
                );
            distanceMap.Add
                ("Iasi", new Dictionary<string, double>()
                    {
                        { "Vaslui", 92 },
                        { "Neamt", 87 }
                    }
                );
            distanceMap.Add
                ("Neamt", new Dictionary<string, double>()
                    {
                        { "Iasi", 87 }
                    }
                );
            #endregion

            #region NodeMapDefinition

            Node Oradea = new Node("Oradea", null, null);
            Node Zerind = new Node("Zerind", null, null);
            Node Arad = new Node("Arad", null, null);
            Node Timisoara = new Node("Timisoara", null, null);
            Node Lugoj = new Node("Lugoj", null, null);
            Node Mehadia = new Node("Mehadia", null, null);
            Node Drobeta = new Node("Drobeta", null, null);
            Node Craiova = new Node("Craiova", null, null);
            Node Rimnicu_Vilcea = new Node("Rimnicu Vilcea", null, null);
            Node Sibiu = new Node("Sibiu", null, null);
            Node Pitesti = new Node("Pitesti", null, null);
            Node Fagaras = new Node("Fagaras", null, null);
            Node Bucharest = new Node("Bucharest", null, null);
            Node Giurgiu = new Node("Giurgiu", null, null);
            Node Urziceni = new Node("Urziceni", null, null);
            Node Hirsova = new Node("Hirsova", null, null);
            Node Eforie = new Node("Eforie", null, null);
            Node Vaslui = new Node("Vaslui", null, null);
            Node Iasi = new Node("Iasi", null, null);
            Node Neamt = new Node("Neamt", null, null);

            Oradea.Neighbours = new Dictionary<Node, double>() { { Zerind, 71 }, { Sibiu, 151 } };
            Zerind.Neighbours = new Dictionary<Node, double>() { { Oradea, 71 }, { Arad, 75 } };
            Arad.Neighbours = new Dictionary<Node, double>() { { Zerind, 75 }, { Sibiu, 140 }, { Timisoara, 118 } };
            Timisoara.Neighbours = new Dictionary<Node, double>() { { Arad, 118 }, { Lugoj, 111 } };
            Lugoj.Neighbours = new Dictionary<Node, double>() { { Timisoara, 111 }, { Mehadia, 70} };
            Mehadia.Neighbours = new Dictionary<Node, double>() { { Lugoj, 70 }, { Drobeta, 75 } };
            Drobeta.Neighbours = new Dictionary<Node, double>() { { Mehadia, 75 }, { Craiova, 120 } };
            Craiova.Neighbours = new Dictionary<Node, double>() { { Drobeta, 120 }, { Pitesti, 138 }, { Rimnicu_Vilcea, 146 } };
            Rimnicu_Vilcea.Neighbours = new Dictionary<Node, double>() { { Craiova, 146 }, { Sibiu, 80 }, { Pitesti, 97 } };
            Sibiu.Neighbours = new Dictionary<Node, double>() { { Arad, 140 }, { Oradea, 151 }, { Rimnicu_Vilcea, 80 }, { Fagaras, 99 } };
            Pitesti.Neighbours = new Dictionary<Node, double>() { { Rimnicu_Vilcea, 97 }, { Craiova, 138 }, { Bucharest, 101 } };
            Fagaras.Neighbours = new Dictionary<Node, double>() { { Sibiu, 99 }, { Bucharest, 211 } };
            Bucharest.Neighbours = new Dictionary<Node, double>() { { Pitesti, 101 }, { Fagaras, 211 }, { Giurgiu, 90 }, { Urziceni, 85 } };
            Giurgiu.Neighbours = new Dictionary<Node, double>() { { Bucharest, 90 } };
            Urziceni.Neighbours = new Dictionary<Node, double>() { { Bucharest, 85 }, { Hirsova, 98 }, { Vaslui, 142 } };
            Hirsova.Neighbours = new Dictionary<Node, double>() { { Urziceni, 98 }, { Eforie, 86 } };
            Eforie.Neighbours = new Dictionary<Node, double>() { { Hirsova, 86 } };
            Vaslui.Neighbours = new Dictionary<Node, double>() { { Urziceni, 142 }, { Iasi, 92 } };
            Iasi.Neighbours = new Dictionary<Node, double>() { { Vaslui, 92 }, { Neamt, 87 } };
            Neamt.Neighbours = new Dictionary<Node, double>() { { Iasi, 87 } };

            NodeMap.Add(Oradea);
            NodeMap.Add(Zerind);
            NodeMap.Add(Arad);
            NodeMap.Add(Timisoara);
            NodeMap.Add(Lugoj);
            NodeMap.Add(Mehadia);
            NodeMap.Add(Drobeta);
            NodeMap.Add(Craiova);
            NodeMap.Add(Rimnicu_Vilcea);
            NodeMap.Add(Sibiu);
            NodeMap.Add(Pitesti);
            NodeMap.Add(Fagaras);
            NodeMap.Add(Bucharest);
            NodeMap.Add(Giurgiu);
            NodeMap.Add(Urziceni);
            NodeMap.Add(Hirsova);
            NodeMap.Add(Eforie);
            NodeMap.Add(Vaslui);
            NodeMap.Add(Iasi);
            NodeMap.Add(Neamt);
            #endregion

            #region AStarNodeMapDefinition
            AddAStarNodes();
            
            #endregion
        }

        private void AddAStarNodes()
        {
            AStarNode Oradea = new AStarNode("Oradea", new Vector2D() { X = 7, Y = 2 });
            AStarNode Zerind = new AStarNode("Zerind", new Vector2D() { X = 4, Y = 4 });
            AStarNode Arad = new AStarNode("Arad", new Vector2D() { X = 3, Y = 7 });
            AStarNode Timisoara = new AStarNode("Timisoara", new Vector2D() { X = 3, Y = 12 });
            AStarNode Lugoj = new AStarNode("Lugoj", new Vector2D() { X = 8, Y = 13 });
            AStarNode Mehadia = new AStarNode("Mehadia", new Vector2D() { X = 8, Y = 16 });
            AStarNode Drobeta = new AStarNode("Drobeta", new Vector2D() { X = 7, Y = 20 });
            AStarNode Craiova = new AStarNode("Craiova", new Vector2D() { X = 13, Y = 21 });
            AStarNode Rimnicu_Vilcea = new AStarNode("Rimnicu Vilcea", new Vector2D() { X = 12, Y = 12 });
            AStarNode Sibiu = new AStarNode("Sibiu", new Vector2D() { X = 11, Y = 8 });
            AStarNode Pitesti = new AStarNode("Pitesti", new Vector2D() { X = 18, Y = 14 });
            AStarNode Fagaras = new AStarNode("Fagaras", new Vector2D() { X = 17, Y = 8 });
            AStarNode Bucharest = new AStarNode("Bucharest", new Vector2D() { X = 22, Y = 15 });
            AStarNode Giurgiu = new AStarNode("Giurgiu", new Vector2D() { X = 22, Y = 21 });
            AStarNode Urziceni = new AStarNode("Urziceni", new Vector2D() { X = 25, Y = 14 });
            AStarNode Hirsova = new AStarNode("Hirsova", new Vector2D() { X = 28, Y = 14 });
            AStarNode Eforie = new AStarNode("Eforie", new Vector2D() { X = 30, Y = 20 });
            AStarNode Vaslui = new AStarNode("Vaslui", new Vector2D() { X = 27, Y = 11 });
            AStarNode Iasi = new AStarNode("Iasi", new Vector2D() { X = 25, Y = 5 });
            AStarNode Neamt = new AStarNode("Neamt", new Vector2D() { X = 21, Y = 3 });

            Oradea.Neighbours = new List<AStarNode>() { Zerind, Sibiu };
            Zerind.Neighbours = new List<AStarNode>() { Oradea, Arad };
            Arad.Neighbours = new List<AStarNode>() { Zerind, Sibiu, Timisoara };
            Timisoara.Neighbours = new List<AStarNode>() { Arad, Lugoj };
            Lugoj.Neighbours = new List<AStarNode>() { Timisoara, Mehadia };
            Mehadia.Neighbours = new List<AStarNode>() { Lugoj, Drobeta };
            Drobeta.Neighbours = new List<AStarNode>() { Mehadia, Craiova };
            Craiova.Neighbours = new List<AStarNode>() { Drobeta, Pitesti, Rimnicu_Vilcea };
            Rimnicu_Vilcea.Neighbours = new List<AStarNode>() { Craiova, Sibiu, Pitesti };
            Sibiu.Neighbours = new List<AStarNode>() { Arad, Oradea, Rimnicu_Vilcea, Fagaras };
            Pitesti.Neighbours = new List<AStarNode>() { Rimnicu_Vilcea, Craiova, Bucharest };
            Fagaras.Neighbours = new List<AStarNode>() { Sibiu, Bucharest };
            Bucharest.Neighbours = new List<AStarNode>() { Pitesti, Fagaras, Giurgiu, Urziceni };
            Giurgiu.Neighbours = new List<AStarNode>() { Bucharest };
            Urziceni.Neighbours = new List<AStarNode>() { Bucharest, Hirsova, Vaslui };
            Hirsova.Neighbours = new List<AStarNode>() { Urziceni, Eforie };
            Eforie.Neighbours = new List<AStarNode>() { Hirsova };
            Vaslui.Neighbours = new List<AStarNode>() { Urziceni, Iasi };
            Iasi.Neighbours = new List<AStarNode>() { Vaslui, Neamt };
            Neamt.Neighbours = new List<AStarNode>() { Iasi };

            AStarNodeMap.Add(Oradea);
            AStarNodeMap.Add(Zerind);
            AStarNodeMap.Add(Arad);
            AStarNodeMap.Add(Timisoara);
            AStarNodeMap.Add(Lugoj);
            AStarNodeMap.Add(Mehadia);
            AStarNodeMap.Add(Drobeta);
            AStarNodeMap.Add(Craiova);
            AStarNodeMap.Add(Rimnicu_Vilcea);
            AStarNodeMap.Add(Sibiu);
            AStarNodeMap.Add(Pitesti);
            AStarNodeMap.Add(Fagaras);
            AStarNodeMap.Add(Bucharest);
            AStarNodeMap.Add(Giurgiu);
            AStarNodeMap.Add(Urziceni);
            AStarNodeMap.Add(Hirsova);
            AStarNodeMap.Add(Eforie);
            AStarNodeMap.Add(Vaslui);
            AStarNodeMap.Add(Iasi);
            AStarNodeMap.Add(Neamt);
        }
    }
}
