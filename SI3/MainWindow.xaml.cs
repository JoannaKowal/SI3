
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
        public SI3Backend.Game m_activeGame = new SI3Backend.Game();
        public MainWindow()
        {
            InitializeComponent();

			SetupBoard();
		}

		private void SetupBoard()
		{
			// Ring 0
			slot_0_0.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 0));
			slot_0_1.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 1));
			slot_0_2.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 2));
			slot_0_3.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 3));
			slot_0_4.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 4));
			slot_0_5.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 5));
			slot_0_6.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 6));
			slot_0_7.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 7));

			// Ring 1
			slot_1_0.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 0));
			slot_1_1.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 1));
			slot_1_2.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 2));
			slot_1_3.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 3));
			slot_1_4.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 4));
			slot_1_5.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 5));
			slot_1_6.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 6));
			slot_1_7.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 7));

			// Ring 2
			slot_2_0.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 0));
			slot_2_1.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 1));
			slot_2_2.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 2));
			slot_2_3.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 3));
			slot_2_4.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 4));
			slot_2_5.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 5));
			slot_2_6.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 6));
			slot_2_7.Initialize(m_activeGame, new SI3Backend.BoardFieldId(0, 7));

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
			m_activeGame.Start();
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
