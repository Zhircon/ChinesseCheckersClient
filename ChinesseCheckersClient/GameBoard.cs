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
        private const int HEIGHT = 15;
        private const int WIDTH = 21;

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
        public char GetPosition(Point _from)
        {
            return gameBoard[(int)_from.Y, (int)_from.X];
        }
        public void SetPosition(char _char, Point _to)
        {
            gameBoard[(int)_to.Y,(int)_to.X] = _char;
        }
        public void DrawGameboard()
        {
            for (int row = 0; row < 15; row++)
            {
                for (int column = 0; column < 21; column++)
                {
                    if (gameBoard[row, column] == 'X') { Console.Write(" "); }
                    else { Console.Write(gameBoard[row, column]); }
                }
                Console.WriteLine();
            }
        }
        public void Move(Point _from, Point _to)
        {
            gameBoard[(int)_to.Y, (int)_to.X] = gameBoard[(int)_from.Y, (int)_from.X];
            gameBoard[(int)_from.Y, (int)_from.X] = 'O';
        }
        private void ObtainPosibleMovesOccupied(ref List<Point> posiblesMovesList,Point _from)
        {
            if ((int)_from.Y > 0 && (int)_from.Y < 14 && (int)_from.X > 0 && _from.X < 20)
            {
                if (gameBoard[(int)_from.Y - 1, (int)_from.X + 1] == 'O') { posiblesMovesList.Add(new Point(_from.X + 1, _from.Y - 1)); }
                if (gameBoard[(int)_from.Y - 1, (int)_from.X - 1] == 'O') { posiblesMovesList.Add(new Point(_from.X - 1, _from.Y - 1)); }
                if (gameBoard[(int)_from.Y + 1, (int)_from.X + 1] == 'O') { posiblesMovesList.Add(new Point(_from.X + 1, _from.Y + 1)); }
                if (gameBoard[(int)_from.Y + 1, (int)_from.X - 1] == 'O') { posiblesMovesList.Add(new Point(_from.X - 1, _from.Y + 1)); }
                if (gameBoard[(int)_from.Y, (int)_from.X + 1] == 'O') { posiblesMovesList.Add(new Point(_from.X + 1, _from.Y)); }
                if (gameBoard[(int)_from.Y, (int)_from.X - 1] == 'O') { posiblesMovesList.Add(new Point(_from.X - 1, _from.Y)); }
            }
        }
        public List<Point> GetAllPosiblesMoves(Point _from)
        {
            List<Point> posiblesMovesList = new List<Point>();
            GetPosibleMoves(ref posiblesMovesList, _from);
            return posiblesMovesList;
        }
        public List<Point> GetPosibleMoves(ref List<Point> _posiblesMovesList,Point _from)
        {
            if (gameBoard[(int)_from.Y - 1, (int)_from.X + 1] == 'O') { _posiblesMovesList.Add(new Point(_from.X + 1, _from.Y - 1)); }
            else { ObtainPosibleMovesOccupied(ref _posiblesMovesList, new Point(_from.X + 1, _from.Y - 1)); }
            
            if (gameBoard[(int)_from.Y - 1, (int)_from.X - 1] == 'O') { _posiblesMovesList.Add(new Point(_from.X - 1, _from.Y - 1)); }
            else { ObtainPosibleMovesOccupied(ref _posiblesMovesList, new Point(_from.X - 1, _from.Y - 1)); }
            
            if (gameBoard[(int)_from.Y + 1, (int)_from.X + 1] == 'O') { _posiblesMovesList.Add(new Point(_from.X + 1, _from.Y + 1)); }
            else { ObtainPosibleMovesOccupied(ref _posiblesMovesList, new Point(_from.X + 1, _from.Y + 1)); }
            
            if (gameBoard[(int)_from.Y + 1, (int)_from.X - 1] == 'O') { _posiblesMovesList.Add(new Point(_from.X - 1, _from.Y + 1)); }
            else if(gameBoard[(int)_from.Y + 1, (int)_from.X - 1] != 'X')  { ObtainPosibleMovesOccupied(ref _posiblesMovesList, new Point(_from.X - 1, _from.Y + 1)); }
            
            if (gameBoard[(int)_from.Y, (int)_from.X + 1] == 'O') { _posiblesMovesList.Add(new Point(_from.X + 1, _from.Y)); }
            else if (gameBoard[(int)_from.Y, (int)_from.X + 1] != 'X') { ObtainPosibleMovesOccupied(ref _posiblesMovesList, new Point(_from.X + 1, _from.Y)); }
            
            if (gameBoard[(int)_from.Y, (int)_from.X - 1] == 'O') { _posiblesMovesList.Add(new Point(_from.X - 1, _from.Y)); }
            else if (gameBoard[(int)_from.Y, (int)_from.X - 1] == 'O') { ObtainPosibleMovesOccupied(ref _posiblesMovesList, new Point(_from.X - 1, _from.Y)); }
            return _posiblesMovesList;
        }
    }
}
