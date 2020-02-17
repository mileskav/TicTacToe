using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using Demo_Wpf_TheSimpleGame.Presentation;

//using TicTacToe.ConsoleApp.Model;

namespace Demo_Wpf_TheSimpleGame.Models
{
    public class Gameboard : ObservableObject
    {
        #region ENUMS

        public const string PLAYER_PIECE_X = "X";
        public const string PLAYER_PIECE_O = "O";
        public const string PLAYER_PIECE_NONE = "";

        public enum GameboardState
        {
            NewRound,
            PlayerXTurn,
            PlayerOTurn,
            PlayerXWin,
            PlayerOWin,
            CatsGame
        }

        #endregion

        #region FIELDS

        private const int MAX_NUM_OF_ROWS_COLUMNS = 4;

        private string[][] _currentBoard;

        #endregion

        #region PROPERTIES

        public int MaxNumOfRowsColumns
        {
            get { return MAX_NUM_OF_ROWS_COLUMNS; }
        }

        public string[][] CurrentBoard
        {
            get { return _currentBoard; }
            set
            {
                _currentBoard = value;
                OnPropertyChanged(nameof(CurrentBoard));
            }
        }

        public GameboardState CurrentRoundState { get; set; }
        #endregion

        #region CONSTRUCTORS

        public Gameboard()
        {
            CurrentBoard = new string[4][];
            CurrentBoard[0] = new string[4];
            CurrentBoard[1] = new string[4];
            CurrentBoard[2] = new string[4];
            CurrentBoard[3] = new string[4];

            InitializeGameboard();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// fill the game board array with "None" enum values
        /// </summary>
        public void InitializeGameboard()
        {
            CurrentRoundState = GameboardState.NewRound;

            //
            // Set all PlayerPiece array values to "None"
            //
            for (int row = 0; row < MAX_NUM_OF_ROWS_COLUMNS; row++)
            {
                for (int column = 0; column < MAX_NUM_OF_ROWS_COLUMNS; column++)
                {
                    CurrentBoard[row][column] = PLAYER_PIECE_NONE;
                }
            }
        }


        /// <summary>
        /// Determine if the game board position is taken
        /// </summary>
        /// <param name="gameboardPosition"></param>
        /// <returns>true if position is open</returns>
        public bool GameboardPositionAvailable(GameboardPosition gameboardPosition)
        {
            //
            // Confirm that the board position is empty
            // Note: gameboardPosition converted to array index by subtracting 1
            //

            if (CurrentBoard[gameboardPosition.Row][gameboardPosition.Column] == PLAYER_PIECE_NONE)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Update the game board state if a player wins or a cat's game happens.
        /// </summary>
        public void UpdateGameboardState()
        {
            if (FourInARow(PLAYER_PIECE_X))
            {
                CurrentRoundState = GameboardState.PlayerXWin;

            }
            // player O has won
            else if (FourInARow(PLAYER_PIECE_O))
            {
                CurrentRoundState = GameboardState.PlayerOWin;
            }
            // all positions filled
            else if (IsCatsGame())
            {
                CurrentRoundState = GameboardState.CatsGame;
            }
        }

        public bool IsCatsGame()
        {
            //
            // All positions on board are filled and no winner
            //
            for (int row = 0; row < 4; row++)
            {
                for (int column = 0; column < 4; column++)
                {
                    if (CurrentBoard[row][column] == PLAYER_PIECE_NONE)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// Check for any three in a row.
        /// </summary>
        /// <param name="playerPieceToCheck">Player's game piece to check</param>
        /// <returns>true if a player has won</returns>
        private bool FourInARow(string playerPieceToCheck)
        {
            //
            // Check rows for player win
            //
            for (int row = 0; row < 4; row++)
            {
                if (CurrentBoard[row][0] == playerPieceToCheck &&
                    CurrentBoard[row][1] == playerPieceToCheck &&
                    CurrentBoard[row][2] == playerPieceToCheck &&
                    CurrentBoard[row][3] == playerPieceToCheck)
                {
                    return true;
                }
            }

            //
            // Check columns for player win
            //
            for (int column = 0; column < 4; column++)
            {
                if (CurrentBoard[0][column] == playerPieceToCheck &&
                    CurrentBoard[1][column] == playerPieceToCheck &&
                    CurrentBoard[2][column] == playerPieceToCheck &&
                    CurrentBoard[3][column] == playerPieceToCheck)
                {
                    return true;
                }
            }

            //
            // Check diagonals for player win
            //
            if (
                (CurrentBoard[0][0] == playerPieceToCheck &&
                 CurrentBoard[1][1] == playerPieceToCheck &&
                 CurrentBoard[2][2] == playerPieceToCheck &&
                 CurrentBoard[3][3] == playerPieceToCheck)
                ||
                (CurrentBoard[0][3] == playerPieceToCheck &&
                 CurrentBoard[1][2] == playerPieceToCheck &&
                 CurrentBoard[2][1] == playerPieceToCheck &&
                 CurrentBoard[3][0] == playerPieceToCheck)
                )
            {
                return true;
            }

            //
            // No Player Has Won
            //

            return false;
        }

        /// <summary>
        /// Add player's move to the game board.
        /// </summary>
        /// <param name="gameboardPosition"></param>
        /// <param name="PlayerPiece"></param>
        public void SetPlayerPiece(GameboardPosition gameboardPosition, string PlayerPiece)
        {
            //
            // Row and column value adjusted to match array structure
            // Note: gameboardPosition converted to array index by subtracting 1
            //
            CurrentBoard[gameboardPosition.Row][gameboardPosition.Column] = PlayerPiece;

            //
            // Change game board state to next player
            //
            SetNextPlayer();
        }

        /// <summary>
        /// Switch the game board state to the next player.
        /// </summary>
        private void SetNextPlayer()
        {
            if (CurrentRoundState == GameboardState.PlayerXTurn)
            {
                CurrentRoundState = GameboardState.PlayerOTurn;
            }
            else
            {
                CurrentRoundState = GameboardState.PlayerXTurn;
            }
        }

        #endregion
    }
}

