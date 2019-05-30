using SI3Backend;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

using State = SI3Backend.FieldState;

namespace SI3
{
    /// <summary>
    /// Interaction logic for GameField.xaml
    /// </summary>
    public partial class GameField : UserControl
    {
        SI3Backend.Game m_game;
        SI3Backend.BoardFieldId m_fieldId;
        
		public void Initialize(SI3Backend.Game board, SI3Backend.BoardFieldId fieldId)
        {
            m_game = board;
            m_fieldId = fieldId;
            m_game.OnBoardChangedEvent += new Game.OnBoardChanged(RefreshImage);
        }

        State GetCurrentState()
        {
			Debug.Assert(m_game != null);
			return m_game.GetBoard().GetFieldState(m_fieldId);
        }

        void RefreshImage()
        {
            if (GetCurrentState() == State.Empty)
                VisualState.Source = new BitmapImage(new Uri("Resources/pawn_empty.png", UriKind.Relative));
            else if (GetCurrentState() == State.White)
                VisualState.Source = new BitmapImage(new Uri("Resources/pawn_white.png", UriKind.Relative));
            else
                VisualState.Source = new BitmapImage(new Uri("Resources/pawn_Black.png", UriKind.Relative));
            
        }
        public GameField()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
			Debug.Assert(m_game != null);
            if (m_game.numberOfWhitePawns > 0)
            {
                if(m_game.m_activePlayer == PlayerId.White && !m_game.mill)
                {
                    m_game.numberOfWhitePawns--;
                    m_game.whitePawnsOnBord++;
                }
                else if(!m_game.mill)
                {
                    m_game.numberOfBlackPawns--;
                    m_game.blackPawnsOnBord++;
                } 
                m_game.PlacePawn(m_fieldId);
                if(m_game.GetBoard().IsMill(m_fieldId))
                {
                    m_game.mill = true;
                }

            }
            else
            {
                if(m_game.GetBoard().GetFieldState(m_fieldId) != FieldState.Empty)
                {
                    if(m_game.mill)
                    {
                        m_game.PlacePawn(m_fieldId);
                    }
                    else
                    {
                        m_game.fieldToMove = m_fieldId;
                    }  
                }
                else
                {
                    //FieldState newState = m_game.GetBoard().GetFieldState(m_game.fieldToMove);
                    m_game.PlacePawn(m_game.fieldToMove);
                    //m_game.GetBoard().SetFieldState(m_fieldId, newState);
                    m_game.PlacePawn(m_fieldId);
                    if (m_game.GetBoard().IsMill(m_fieldId))
                    {
                        m_game.mill = true;
                    }

                }  
                if(m_game.whitePawnsOnBord < 3)
                {
                    m_game.winner = PlayerId.Black;
                    m_game.m_currentState = GameState.Finished;
                  

                }
                else if(m_game.blackPawnsOnBord < 3)
                {
                    m_game.winner = PlayerId.White;
                    m_game.m_currentState = GameState.Finished;
                    
                }

            }

            // TODO zrobić delegate i event w game (OnBoardChanged) i to RefreshImage podpiąć, zamiast wołać tutaj
			//RefreshImage();
        }
    }
}
