using System;
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
        SI3Backend.BoardFieldId m_boardId;
        SI3Backend.Game m_game;
        SI3Backend.BoardFieldId m_fieldId;
        State currentState;
       public void Initialize(SI3Backend.Game board, SI3Backend.BoardFieldId fieldId)
        {
            m_game = board;
            m_fieldId = fieldId;
        }


        //void NextState()
        //{
        //    if (GetCurrentState() == State.Empty)
        //        currentState = State.White;
        //    else if (currentState == State.White)
        //        currentState = State.Black;
        //    else
        //        currentState = State.Empty;
        //}
        State GetCurrentState()
        {
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
           // Initialize()

        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            GetCurrentState();
            m_game.PlacePawn(m_fieldId);
            RefreshImage();
        }
    }
}
