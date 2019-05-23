
using SI3Backend;
using System.Windows;
using static SI3.Options;

namespace SI3
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public SI3Backend.Game m_activeGame;
        public MainWindow()
        {
            InitializeComponent();
   
        }
        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            Options optionsWindow = new Options();
            optionsWindow.Show();
            optionsWindow.OnOKEvent += new Options.OnOK(OnStartGame);
        }
        // Zrobic struct z parametrami i tu przekazac
        private void OnStartGame(GameParameters gameParameters)
        {
            m_activeGame = new SI3Backend.Game();
            m_activeGame.m_currentState = SI3Backend.GameState.InProgress;
            m_activeGame.m_activePlayer = SI3Backend.PlayerId.Black;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
