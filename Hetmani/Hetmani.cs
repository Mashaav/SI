using Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Hetmani
{

    public class ChessNode : Node<int>
    {
        private int _initialState;
        public ChessNode _parent;
        public ChessNode LeftUpper, LeftMiddle, LeftLower, MiddleUpper, MiddleLower, RightUpper, RightMiddle, RightLower; //sąsiedzi
        public bool isChecked = false; //czy pole jest szachowane
        public bool Hetman = false; // czy jest na polu ustawiony Hetman
        public KeyValuePair<int, int> coords = new KeyValuePair<int, int>(); // koordynaty na szachownicy

        public ChessNode(int initialState, object parent) : base(initialState, parent)
        {
            _initialState = initialState;
            _parent = null;
        }

        public int InitialState
        {
            get { return _initialState; }
            set { _initialState = value; }
        }

        public new ChessNode Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        #region Checkers
        public void CheckAllSides()
        {
            CheckLU();
            CheckMU();
            CheckRU();
            CheckLM();
            CheckRM();
            CheckLL();
            CheckML();
            CheckRL();
        }

        public void CheckLU()
        {
            isChecked = true;
            if(LeftUpper != null)
            {
                LeftUpper.CheckLU();
            }
        }

        public void CheckMU()
        {
            isChecked = true;
            if (MiddleUpper != null)
            {
                MiddleUpper.CheckMU(); 
            }
        }

        public void CheckRU()
        {
            isChecked = true;
            if(RightUpper != null)
            {
                RightUpper.CheckRU();
            }
        }

        public void CheckLM()
        {
            isChecked = true;
            if (LeftMiddle != null)
            {
                LeftMiddle.CheckLM();
            }
        }

        public void CheckRM()
        {
            isChecked = true;
            if (RightMiddle != null)
            {
                RightMiddle.CheckRM();
            }
        }

        public void CheckLL()
        {
            isChecked = true;
            if(LeftLower != null)
            {
                LeftLower.CheckLL();
            }
        }

        public void CheckML()
        {
            isChecked = true;
            if(MiddleLower != null)
            {
                MiddleLower.CheckML();
            }
        }

        public void CheckRL()
        {
            isChecked = true;
            if (RightLower != null)
            {
                RightLower.CheckRL();
            }
        }
        #endregion

    }

    public class Hetman : IProblem<KeyValuePair<int,int>>
    {
        private ChessNode[,] chessBoard; // szachownica
        private List<KeyValuePair<int, int>> Hetmani; // lista kordynatów ustawionych hetmanów
        private KeyValuePair<int, int> _initialState;

        public Hetman()
        {
            //1. stworzenie obiektu
            chessBoard = new ChessNode[8, 8]; //szachownica
            Hetmani = new List<KeyValuePair<int, int>>(); //lista koordynatów pola z hetmanem


            //2. wypełnienie tablicy "polami" typu ChessNode
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    //3. Nadanie koordynatów dla każdego pola
                    chessBoard[i, j] = new ChessNode(0, null) { coords = new KeyValuePair<int, int>(i,j) };
                }
            }


            //4. Ustawienie sąsiadów każdego pola. W przypadku, gdy próbuję sprawdzić element poza tablicą -> exception
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    try
                    {
                        if (chessBoard[i - 1, j - 1] != null)
                            chessBoard[i, j].LeftUpper = chessBoard[i - 1, j - 1];
                    }
                    catch
                    {
                        Debug.WriteLine("Exception: Bad Array Index" + i + " / " + j + "No. LU");
                    }
                    try
                    {
                        if (chessBoard[i - 1, j] != null)
                            chessBoard[i, j].MiddleUpper = chessBoard[i - 1, j];
                    }
                    catch
                    {
                        Debug.WriteLine("Exception: Bad Array Index" + i + " / " + j + "No. MU");
                    }
                    try
                    {
                        if (chessBoard[i - 1, j + 1] != null)
                            chessBoard[i, j].RightUpper = chessBoard[i - 1, j + 1];
                    }
                    catch
                    {
                        Debug.WriteLine("Exception: Bad Array Index" + i + " / " + j + "No. RU");
                    }
                    try
                    {
                        if (chessBoard[i, j - 1] != null)
                            chessBoard[i, j].LeftMiddle = chessBoard[i, j - 1];
                    }
                    catch
                    {
                        Debug.WriteLine("Exception: Bad Array Index" + i + " / " + j + "No. LM");
                    }
                    try
                    {
                        if (chessBoard[i, j + 1] != null)
                            chessBoard[i, j].RightMiddle = chessBoard[i, j + 1];
                    }
                    catch
                    {
                        Debug.WriteLine("Exception: Bad Array Index" + i + " / " + j + "No. RM");
                    }
                    try
                    {
                        if (chessBoard[i + 1, j - 1] != null)
                            chessBoard[i, j].LeftLower = chessBoard[i + 1, j - 1];
                    }
                    catch
                    {
                        Debug.WriteLine("Exception: Bad Array Index" + i + " / " + j + "No. LL");
                    }
                    try
                    {
                        if (chessBoard[i + 1, j] != null)
                            chessBoard[i, j].MiddleLower = chessBoard[i + 1, j];
                    }
                    catch
                    {
                        Debug.WriteLine("Exception: Bad Array Index" + i + " / " + j + "No. ML");
                    }
                    try
                    {
                        if (chessBoard[i + 1, j + 1] != null)
                                chessBoard[i, j].RightLower = chessBoard[i + 1, j + 1];
                    }
                    catch
                    {
                        Debug.WriteLine("Exception: Bad Array Index" + i + " / " + j + "No. RL");
                    }
                }
            }
        }

        public KeyValuePair<int, int> InitialState
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

        public IList<KeyValuePair<int, int>> Expand(KeyValuePair<int, int> state)
        {
            RefreshChessBoard();
            InsertIfPossible(InitialState);
            InsertIfPossible(state);
            
            //od początku szachownicy ustaw hetmanów na polach nieszachowanych i nieposiadających hetmana
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    InsertIfPossible(new KeyValuePair<int, int>(i, j));
                }
            }
            //wynik wstawiania hetmanów zwróć w postaci listy koordynatów
            return Hetmani;
        }

        public bool IsGoal(KeyValuePair<int, int> state)
        {
            if(Hetmani.Count == 8)
            {
                return true;
            }
            return false;
        }

        public void Search(KeyValuePair<int,int> initialState)//wyszukiwanie z opcjonalnym dodaniem obowiązkowego hetmana
        {
            InitialState = initialState; // ustawienie stanu początkowego do szukania
            IList<KeyValuePair<int, int>> list = new List<KeyValuePair<int, int>>(); // Lista wynikowa
            int i = 0, j = 0, k = 1; //zmienne pomocnicze

            do
            {
                Debug.WriteLine("Podejscie: " + k); //wypisuje numer podejścia

                list = Expand(new KeyValuePair<int, int>(i,j));//wykonanie expand dla stanu początkowego oraz ustawienie hetmana drugiego we wskazanym(i,j) miejscu
                
                j++;//zmiana ustawienia kolumny dla hetmana dla następnej iteracji
                if (j >= 8)//dotarłem do ostatniej kolumny
                {
                    i++; // przejdź do kolejnego wiersza
                    j -= 8; // wróć do pierwszej kolumny
                    if (i >= 8) //jeśli jestem poza szachownicą(chęć sprawdzania 9 rządu) to zakończ działanie
                    {
                        Debug.WriteLine("Exception: " + "Koniec szachownicy!");
                        break;
                    }
                }

                if (IsGoal(initialState)) //jeśli jest wynik to wypisz
                {
                    Console.WriteLine("Ilość Hetmanów: " + list.Count);
                    Console.WriteLine("Koordynaty: ");

                    foreach (var item in list)
                    {
                        Console.WriteLine(item.Key + " / " + item.Value);
                    }
                }
                k++;

            } while (true);
        }

        private void RefreshChessBoard() //funckja czyszcząca szachownicę i listę hetmanów
        {
            //czyszczenie listy hetmanów
            Hetmani.Clear();

            //czyszczenie szachownicy
            foreach (var node in chessBoard)
            {
                node.isChecked = false;
                node.Hetman = false;
            }
        }

        private void InsertIfPossible(KeyValuePair<int,int> state)// jeśli się da to funkcja doda hetmana na wskazane pole
        {
            if (state.Key >= 0 && state.Key < 8 && state.Value >= 0 && state.Value < 8)
            {
                ChessNode node = chessBoard[state.Key, state.Value];
                if (!node.isChecked && !node.Hetman)
                {
                    node.Hetman = true; // ustaw w danym miejscu hetmana
                    Hetmani.Add(state); // dodaj koordynaty do listy
                    node.CheckAllSides(); // szachowanie pól po diagonalnych i wertykalnie/horyzontalnie
                }
            }
        }

        public static void Main()
        {

        }
    }
}
