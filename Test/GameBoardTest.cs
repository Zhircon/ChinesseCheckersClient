using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Test
{
    [TestClass]
    public class GameBoardTest
    {        
        private const char RED = 'R';
        private const char YELLOW = 'M';
        private const char ORANGE = 'N';
        private const char WHITE = 'B';
        private const char NOTHING = 'X';
        private const char FREE = 'O';

        private readonly char[,] boardRedVictory = new char[,]
                {
                { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'},
                { 'X','X','X','X','X','X','X','X','X','X','R','X','X','X','X','X','X','X','X','X','X' },
                { 'X','X','X','X','X','X','X','X','X','R','X','R','X','X','X','X','X','X','X','X','X' },
                { 'X','X','X','X','X','X','X','X','R','X','R','X','R','X','X','X','X','X','X','X','X' },
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
        private readonly char[,] boardOrangeVictory= new char[,]
        {
                { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'},
                { 'X','X','X','X','X','X','X','X','X','X','O','X','X','X','X','X','X','X','X','X','X' },
                { 'X','X','X','X','X','X','X','X','X','O','X','O','X','X','X','X','X','X','X','X','X' },
                { 'X','X','X','X','X','X','X','X','O','X','O','X','O','X','X','X','X','X','X','X','X' },
                { 'X','X','X','X','X','X','X','O','X','O','X','O','X','O','X','X','X','X','X','X','X' },
                { 'X','X','X','X','X','X','O','X','O','X','O','X','O','X','O','X','X','X','X','X','X' },
                { 'X','X','X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X','X','X' },
                { 'X','X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X','X' },
                { 'X','X','X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X','X','X' },
                { 'X','X','X','X','X','X','O','X','O','X','O','X','O','X','O','X','X','X','X','X','X' },
                { 'X','X','X','X','X','X','X','O','X','O','X','O','X','O','X','X','X','X','X','X','X' },
                { 'X','X','X','X','X','X','X','X','N','X','N','X','N','X','X','X','X','X','X','X','X' },
                { 'X','X','X','X','X','X','X','X','X','N','X','N','X','X','X','X','X','X','X','X','X' },
                { 'X','X','X','X','X','X','X','X','X','X','N','X','X','X','X','X','X','X','X','X','X' },
                { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' }
        };

        private readonly char[,] boardWhiteVictory = new char[,]
        {
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'},
            { 'X','X','X','X','X','X','X','X','X','X','N','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','N','X','N','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','N','X','N','X','N','X','X','X','X','X','X','X','X' },
            { 'X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','B','X','B','X','B','X' },
            { 'X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','B','X','B','X','X' },
            { 'X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','B','X','X','X' },
            { 'X','X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X','X' },
            { 'X','X','X','B','X','O','X','O','X','O','X','O','X','O','X','O','X','M','X','X','X' },
            { 'X','X','B','X','B','X','O','X','O','X','O','X','O','X','O','X','M','X','M','X','X' },
            { 'X','B','X','B','X','B','X','O','X','O','X','O','X','O','X','M','X','M','X','M','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' }
        };
        private readonly char[,] boardYellowVictory = new char[,]
        {
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'},
            { 'X','X','X','X','X','X','X','X','X','X','N','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','N','X','N','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','N','X','N','X','N','X','X','X','X','X','X','X','X' },
            { 'X','M','X','M','X','M','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X' },
            { 'X','X','M','X','M','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X' },
            { 'X','X','X','M','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X' },
            { 'X','X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X','X' },
            { 'X','X','X','B','X','O','X','O','X','O','X','O','X','O','X','O','X','M','X','X','X' },
            { 'X','X','B','X','B','X','O','X','O','X','O','X','O','X','O','X','M','X','M','X','X' },
            { 'X','B','X','B','X','B','X','O','X','O','X','O','X','O','X','M','X','M','X','M','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' }
        };
        private readonly char[,] boardNothingVictory = new char[,]
        {
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X'},
            { 'X','X','X','X','X','X','X','X','X','X','N','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','N','X','N','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','N','X','N','X','N','X','X','X','X','X','X','X','X' },
            { 'X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X' },
            { 'X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X' },
            { 'X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X' },
            { 'X','X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','X','X','X' },
            { 'X','X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','M','X','X','X' },
            { 'X','X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','M','X','M','X','X' },
            { 'X','O','X','O','X','O','X','O','X','O','X','O','X','O','X','M','X','M','X','M','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' },
            { 'X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X','X' }
};
        [TestMethod]
        public void RedVictory()
        {
            var gameboard = new ChinesseCheckersClient.GameBoard();
            gameboard.Board = boardRedVictory;
            char colorWinner = gameboard.CheckWinColor();
            Assert.AreEqual(colorWinner, RED);
        }
        [TestMethod]
        public void OrangeVictory()
        {
            var gameboard = new ChinesseCheckersClient.GameBoard();
            gameboard.Board = boardOrangeVictory;
            char colorWinner = gameboard.CheckWinColor();
            Assert.AreEqual(colorWinner, ORANGE);
        }
        [TestMethod]
        public void WhiteVictory()
        {
            var gameboard = new ChinesseCheckersClient.GameBoard();
            gameboard.ConfigureForThrePlayers();
            gameboard.Board = boardWhiteVictory;
            char colorWinner = gameboard.CheckWinColor();
            Assert.AreEqual(colorWinner, WHITE);
        }
        [TestMethod]
        public void YellowVictory()
        {
            var gameboard = new ChinesseCheckersClient.GameBoard();
            gameboard.ConfigureForThrePlayers();
            gameboard.Board = boardYellowVictory;
            char colorWinner = gameboard.CheckWinColor();
            Assert.AreEqual(colorWinner, YELLOW);
        }
        [TestMethod]
        public void NothingVictory()
        {
            var gameboard = new ChinesseCheckersClient.GameBoard();
            gameboard.ConfigureForThrePlayers();
            gameboard.Board = boardNothingVictory;
            char colorWinner = gameboard.CheckWinColor();
            Assert.AreEqual(colorWinner, NOTHING);
        }
    }
}

