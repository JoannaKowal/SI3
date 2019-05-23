using SI3;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SI3Backend
{
    public enum PlayerId
    {
        None,
        White,
        Black
    }
    public enum GameState
    {
        Initial,
        InProgress,
        Finished
    }
    public class Game
    {
        public GameState m_currentState = GameState.Initial;
        public PlayerId m_activePlayer = PlayerId.None;
        Board m_boardState = new Board();
      //  GameField gameField = new GameField();

        public void StartNextRound()
        {
            if (m_currentState != GameState.InProgress)
                return;
            //...
        }

        public void PlacePawn(BoardFieldId id)
        {
            if ( m_activePlayer != PlayerId.None)
            {
                m_boardState.ChangeFieldState(id, GetStateForPlayerPawn(m_activePlayer));
            }

            // Next round
        }

        private FieldState GetStateForPlayerPawn(PlayerId player)
        {
            Debug.Assert(player != PlayerId.None);

            return player == PlayerId.White ? FieldState.White : FieldState.Black;
        }

        public Board GetBoard()
        {
            return m_boardState;
        }
    }
}
