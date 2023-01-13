using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChinesseCheckersClient
{
    class GameBoard
    {
        private readonly List<Point> baseOrange;
        private readonly List<Point> baseYellow;
        private readonly List<Point> baseWhite;
        private readonly List<Point> baseRed;

        private const char RED = 'R';
        private const char YELLOW ='M';
        private const char ORANGE = 'N';
        private const char WHITE = 'W';
        private const char NOTHING = 'X';
        private const char FREE = 'O';
        
        private bool isTriangular;

        private char[,] gameBoard = new char[,]{
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'},
            { 'X','X','X','X','X','X','X','X','X','X','N','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','N','X','N','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','N','X','N','X','N','X','X','X','X','X','X','X','X' },
            { 'X','V','X','V','X','V','X','O','X','O','X','O','X','O','X','A','X','A','X','A','X' },
            { 'X','X','V','X','V','X','O','X','O','X','O','X','O','X','O','X','A','X','A','X','X' },
            { 'X','X','X','V','X','O','X','O','X','O','X','O','X','O','X','O','X','A','X','X','X' },
            { 'X','X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X','X' },
            { 'X','X','X','B','X','O','X','O','X','O','X','O','X','O','X','O','X','M','X','X','X' },
            { 'X','X','B','X','B','X','O','X','O','X','O','X','O','X','O','X','M','X','M','X','X' },
            { 'X','B','X','B','X','B','X','O','X','O','X','O','X','O','X','M','X','M','X','M','X' },
            { 'X','X','X','X','X','X','X','X','R','X','R','X','R','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','R','X','R','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','R','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' }
        };
        public void ConfigureForTwoPlayers()
        {
            gameBoard= new char[,]{
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'},
            { 'X','X','X','X','X','X','X','X','X','X','N','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','N','X','N','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','N','X','N','X','N','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','O','X','O','X','O','X','O','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','O','X','O','X','O','X','O','X','O','X','X','X','X','X','X' },
            { 'X','X','X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X','X','X' },
            { 'X','X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X','X' },
            { 'X','X','X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X','X','X' },
            { 'X','X','X','X','X','X','O','X','O','X','O','X','O','X','O','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','O','X','O','X','O','X','O','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','R','X','R','X','R','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','R','X','R','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','R','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' }
            };
        }

        public void ConfigureForThrePlayers()
        {
            isTriangular = true;
            gameBoard = new char[,]{
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'},
            { 'X','X','X','X','X','X','X','X','X','X','N','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','N','X','N','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','N','X','N','X','N','X','X','X','X','X','X','X','X' },
            { 'X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X' },
            { 'X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X' },
            { 'X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X' },
            { 'X','X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X','X' },
            { 'X','X','X','B','X','O','X','O','X','O','X','O','X','O','X','O','X','M','X','X','X' },
            { 'X','X','B','X','B','X','O','X','O','X','O','X','O','X','O','X','M','X','M','X','X' },
            { 'X','B','X','B','X','B','X','O','X','O','X','O','X','O','X','M','X','M','X','M','X' },
            { 'X','X','X','X','X','X','X','X','O','X','O','X','O','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','O','X','O','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','O','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' }
            };
        }
        public GameBoard()
        {
            baseRed = new List<Point>();
            baseOrange = new List<Point>();
            baseWhite = new List<Point>();
            baseYellow = new List<Point>();

            baseRed.Add(new Point(10, 1));
            baseRed.Add(new Point(9, 2));
            baseRed.Add(new Point(11, 12));
            baseRed.Add(new Point(8, 3));
            baseRed.Add(new Point(10, 3));
            baseRed.Add(new Point(12, 3));

            baseWhite.Add(new Point(15, 4));
            baseWhite.Add(new Point(17, 4));
            baseWhite.Add(new Point(19, 4));
            baseWhite.Add(new Point(16, 5));
            baseWhite.Add(new Point(18, 5));
            baseWhite.Add(new Point(17, 6));

            baseYellow.Add(new Point(1, 4));
            baseYellow.Add(new Point(3, 4));
            baseYellow.Add(new Point(5, 4));
            baseYellow.Add(new Point(2, 5));
            baseYellow.Add(new Point(4, 5));
            baseYellow.Add(new Point(3, 6));

            baseOrange.Add(new Point(8, 11));
            baseOrange.Add(new Point(10, 11));
            baseOrange.Add(new Point(12, 11));
            baseOrange.Add(new Point(9, 12));
            baseOrange.Add(new Point(11, 12));
            baseOrange.Add(new Point(10, 13));

        }
        public char CheckWinColor()
        {
            char _colorWinner = NOTHING;
            if (isTriangular)
            {
                if (IsOccupiedBaseOrange()) { _colorWinner = ORANGE; }
                if (IsOccupiedBaseYellow()) { _colorWinner = YELLOW; }
                if (IsOccupiedBaseWhite()) { _colorWinner = WHITE; }
            }
            else
            {
                if (IsOccupiedBaseOrange()) { _colorWinner = ORANGE; }
                if (IsOccupiedBaseRed()) { _colorWinner = RED; }
            }
            return _colorWinner;
        }
        public bool IsOccupiedBaseOrange()
        {
            bool isOcupped = true;
            foreach(Point element in baseOrange)
            {
                if (gameBoard[element.Y, element.X] != FREE) { isOcupped = false; } 
            }
            return isOcupped;
        }
        public bool IsOccupiedBaseYellow()
        {
            bool isOcupped = true;
            foreach (Point element in baseRed)
            {
                if (gameBoard[element.Y, element.X] != YELLOW) { isOcupped = false; }
            }
            return isOcupped;
        }
        public bool IsOccupiedBaseRed()
        {
            bool isOcupped = true;
            foreach (Point element in baseRed)
            {
                if (gameBoard[element.Y, element.X] != RED) { isOcupped = false; }
            }
            return isOcupped;
        }
        public bool IsOccupiedBaseWhite()
        {
            bool isOcupped = true;
            foreach (Point element in baseWhite)
            {
                if (gameBoard[element.Y, element.X] != WHITE) { isOcupped = false; }
            }
            return isOcupped;
        }
        public char GetPosition(Point _from)
        {
            return gameBoard[(int)_from.Y, (int)_from.X];
        }
        public void SetPosition(char _char, Point _to)
        {
            gameBoard[(int)_to.Y,(int)_to.X] = _char;
        }

        public void Move(Point _from, Point _to)
        {
            gameBoard[(int)_to.Y, (int)_to.X] = gameBoard[(int)_from.Y, (int)_from.X];
            gameBoard[(int)_from.Y, (int)_from.X] = FREE;
        }

        public List<Point> GetAllPosiblesMoves(Point _from)
        {
            List<Point> posiblesMovesList = new List<Point>();
            GetPosibleMovesSimple(ref posiblesMovesList,_from);
            return posiblesMovesList;
        }

        public void GetPosibleMovesSimple(ref List<Point> _posiblesMovesList, Point _from )
        {
            if (gameBoard[(int)_from.Y - 1, (int)_from.X + 1] == FREE) { _posiblesMovesList.Add(new Point(_from.X + 1, _from.Y - 1)); } 
            if (gameBoard[(int)_from.Y - 1, (int)_from.X - 1] == FREE) { _posiblesMovesList.Add(new Point(_from.X - 1, _from.Y - 1)); }
            if (gameBoard[(int)_from.Y + 1, (int)_from.X + 1] == FREE) { _posiblesMovesList.Add(new Point(_from.X + 1, _from.Y + 1)); }
            if (gameBoard[(int)_from.Y + 1, (int)_from.X - 1] == FREE) { _posiblesMovesList.Add(new Point(_from.X - 1, _from.Y + 1)); }
        }
    }
}
