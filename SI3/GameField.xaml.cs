using SI3Backend;
using System;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace SI3
{
    /// <summary>
    /// Interaction logic for GameField.xaml
    /// </summary>
    public partial class GameField : UserControl
    {
        int fieldId;
        MainWindow controller;
        
		public void Initialize(MainWindow uiControllerWindow, int fieldId)
        {
            this.controller = uiControllerWindow;
            this.fieldId = fieldId;
        }

        public void RefreshImage(PlayerNumber playerNumber)
        {
            if (playerNumber == PlayerNumber.None)
            {
                VisualState.Source = new BitmapImage(new Uri("Resources/pawn_empty.png", UriKind.Relative));
            }
            else if (playerNumber == PlayerNumber.FirstPlayer)
            {
                VisualState.Source = new BitmapImage(new Uri("Resources/pawn_white.png", UriKind.Relative));
            }
            else
            {
                VisualState.Source = new BitmapImage(new Uri("Resources/pawn_Black.png", UriKind.Relative));
            }

        }
        public GameField()
        {
            InitializeComponent();
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.controller.HandleButtonClick(this.fieldId);
        }
    }
}
