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
        public delegate void OnBoardChanged();
        public event OnBoardChanged OnBoardChangedEvent;
        public GameState m_currentState = GameState.Initial;
        public PlayerId m_activePlayer = PlayerId.None;
        Board m_boardState = new Board();
        public int numberOfWhitePawns { get; set; }
        public int numberOfBlackPawns { get; set; }
        public int whitePawnsOnBord;
        public int blackPawnsOnBord;

        public bool mill;
        public PlayerId winner;
        public BoardFieldId fieldToMove;


        // TODO parametry!
        public void Start()
		{
			m_currentState = GameState.InProgress;
			m_activePlayer = PlayerId.Black;
            numberOfBlackPawns = 9;
            numberOfWhitePawns = 9;
            whitePawnsOnBord = 0;
            blackPawnsOnBord = 0;
            mill = false;
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
                if(this.numberOfWhitePawns > 0)
                {
                    // TODO Check if move is valid!
                    if (mill)
                    {
                        if (m_boardState.GetFieldState(id) == GetStateForPlayerPawn(m_activePlayer))
                        {
                            if (!m_boardState.IsMill(id))
                            {
                                if(m_boardState.GetFieldState(id) == FieldState.Black)
                                {
                                    blackPawnsOnBord--;
                                }
                                else
                                {
                                    whitePawnsOnBord--;
                                }
                                this.GetBoard().SetFieldState(id, FieldState.Empty);
                                mill = false;
                                OnBoardChangedEvent();
                            }
                        }
                    }
                    else if (m_boardState.GetFieldState(id) == FieldState.Empty)
                    {
                        m_boardState.ChangeFieldState(id, GetStateForPlayerPawn(m_activePlayer));
                        OnBoardChangedEvent();
                        StartNextRound();
                    }

                }
                else
                {
                    if (mill)
                    {
                        if (m_boardState.GetFieldState(id) == GetStateForPlayerPawn(m_activePlayer))
                        {
                            if (!m_boardState.IsMill(id))
                            {
                                if (m_boardState.GetFieldState(id) == FieldState.Black)
                                {
                                    blackPawnsOnBord--;
                                }
                                else
                                {
                                    whitePawnsOnBord--;
                                }
                                this.GetBoard().SetFieldState(id, FieldState.Empty);
                                mill = false;
                                OnBoardChangedEvent();
                            }
                        }
                    }
                    else if (this.GetBoard().GetFieldState(id) != FieldState.Empty)
                    {
                        m_boardState.ChangeFieldState(id, FieldState.Empty);
                        OnBoardChangedEvent();
                    }
                    else
                    {
                        m_boardState.ChangeFieldState(id, GetStateForPlayerPawn(m_activePlayer));
                        OnBoardChangedEvent();
                        StartNextRound();
                    }
                   
                }

                // if valid

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
