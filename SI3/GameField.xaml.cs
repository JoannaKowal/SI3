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

			m_game.PlacePawn(m_fieldId);

			// TODO zrobić delegate i event w game (OnBoardChanged) i to RefreshImage podpiąć, zamiast wołać tutaj
			RefreshImage();
        }
    }
}
