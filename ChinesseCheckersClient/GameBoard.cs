using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ChinesseCheckersClient
{
    public class GameBoard
    {
        private readonly List<Point> baseOrange;
        private readonly List<Point> baseYellow;
        private readonly List<Point> baseWhite;
        private readonly List<Point> baseRed;

        private const char RED = 'R';
        private const char YELLOW ='M';
        private const char ORANGE = 'N';
        private const char WHITE = 'B';
        private const char NOTHING = 'X';
        private const char FREE = 'O';
        
        private bool isTriangular;

        private char[,] board;
        public char[,] Board
        {
            get { return board; }
            set { board = value; }
        }
        public void ConfigureForTwoPlayers()
        {
            board= new char[,]{
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
            board = new char[,]{
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
            if (isTriangular)
            {
                if (IsOccupiedBaseOrange()) { return ORANGE; }
                if (IsOccupiedBaseYellow()) { return YELLOW; }
                if (IsOccupiedBaseWhite()) { return  WHITE; }
            }
            else
            {
                if (IsOccupiedBaseRed()) { return  RED; }
                if (IsOccupiedBaseOrange()) { return ORANGE; }
            }
            return NOTHING;
        }
        public bool IsOccupiedBaseOrange()
        {
            bool isOcupped = true;
            foreach(Point element in baseOrange)
            {
                if (board[element.Y, element.X] != ORANGE) { isOcupped = false; } 
            }
            return isOcupped;
        }
        public bool IsOccupiedBaseYellow()
        {
            bool isOcupped = true;
            foreach (Point element in baseYellow)
            {
                if (board[element.Y, element.X] != YELLOW) { isOcupped = false; }
            }
            return isOcupped;
        }
        public bool IsOccupiedBaseRed()
        {
            bool isOcupped = true;
            foreach (Point element in baseRed)
            {
                if (board[element.Y, element.X] != RED) { isOcupped = false; }
            }
            return isOcupped;
        }
        public bool IsOccupiedBaseWhite()
        {
            bool isOcupped = true;
            foreach (Point element in baseWhite)
            {
                if (board[element.Y, element.X] != WHITE) { isOcupped = false; }
            }
            return isOcupped;
        }
        public char GetPosition(Point _from)
        {
            return board[(int)_from.Y, (int)_from.X];
        }
        public void SetPosition(char _char, Point _to)
        {
            board[(int)_to.Y,(int)_to.X] = _char;
        }

        public void Move(Point _from, Point _to)
        {
            board[(int)_to.Y, (int)_to.X] = board[(int)_from.Y, (int)_from.X];
            board[(int)_from.Y, (int)_from.X] = FREE;
        }

        public List<Point> GetAllPosiblesMoves(Point _from)
        {
            List<Point> posiblesMovesList = new List<Point>();
            GetPosibleMovesSimple(ref posiblesMovesList,_from);
            return posiblesMovesList;
        }

        public void GetPosibleMovesSimple(ref List<Point> _posiblesMovesList, Point _from )
        {
            if (board[(int)_from.Y - 1, (int)_from.X + 1] == FREE) { _posiblesMovesList.Add(new Point(_from.X + 1, _from.Y - 1)); } 
            if (board[(int)_from.Y - 1, (int)_from.X - 1] == FREE) { _posiblesMovesList.Add(new Point(_from.X - 1, _from.Y - 1)); }
            if (board[(int)_from.Y + 1, (int)_from.X + 1] == FREE) { _posiblesMovesList.Add(new Point(_from.X + 1, _from.Y + 1)); }
            if (board[(int)_from.Y + 1, (int)_from.X - 1] == FREE) { _posiblesMovesList.Add(new Point(_from.X - 1, _from.Y + 1)); }
        }
    }
}
