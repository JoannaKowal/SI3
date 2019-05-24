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

		// TODO parametry!
		public void Start()
		{
			m_currentState = GameState.InProgress;
			m_activePlayer = PlayerId.Black;

		}
		public void Stop()
		{
			// Reset Board
		}
		

        public void StartNextRound()
        {
            if (m_currentState != GameState.InProgress)
                return;


			m_activePlayer = GetOpposingPlayer(m_activePlayer);

		}

        public void PlacePawn(BoardFieldId id)
        {
            if ( m_activePlayer != PlayerId.None)
            {
				// TODO Check if move is valid!

				// if valid
                m_boardState.ChangeFieldState(id, GetStateForPlayerPawn(m_activePlayer));

				StartNextRound();
            }

            // Next round
        }

		private PlayerId GetOpposingPlayer(PlayerId player)
		{
			switch (player)
			{
				case PlayerId.White: return PlayerId.Black;
				case PlayerId.Black: return PlayerId.White;
			}

			return PlayerId.None;
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
