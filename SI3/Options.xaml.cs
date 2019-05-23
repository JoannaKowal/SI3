
using System.Windows;


namespace SI3
{
    /// <summary>
    /// Logika interakcji dla klasy Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        public delegate void OnOK(GameParameters gameParameters);
        public event OnOK OnOKEvent;
        public GameParameters parameters;
        public struct GameParameters
        {
            public int selectedPlayer1;
            public int selectedPlayer2;
            public int selectedAlgorithm1;
            public int selectedAlgorithm2;
            public int selectedHeuristic1;
            public int selectedHeuristic2;
        }
        
        public Options()
        {
            InitializeComponent();
            ComboBoxPlayer1.SelectedIndex = 0;
            ComboBoxPlayer2.SelectedIndex = 0;
            parameters = new GameParameters();
        }
        private void cmb_Player1SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (ComboBoxPlayer1.SelectedIndex == 1)
            {
                parameters.selectedPlayer1 = 1;
                parameters.selectedAlgorithm1 = ComboBoxAlgorithm1.SelectedIndex;
                parameters.selectedHeuristic1 = ComboBoxHeuristic1.SelectedIndex;
                LabelAlgorithm1.IsEnabled = true;
                ComboBoxAlgorithm1.IsEnabled = true;
                LabelHeuristic1.IsEnabled = true;
                ComboBoxHeuristic1.IsEnabled = true;
                ComboBoxAlgorithm1.SelectedIndex = 0;
                ComboBoxHeuristic1.SelectedIndex = 0;
            }
            else
            {
                parameters.selectedPlayer1 = 0;
                parameters.selectedAlgorithm1 = -1;
                parameters.selectedHeuristic1 = -1;
                LabelAlgorithm1.IsEnabled = false;
                ComboBoxAlgorithm1.IsEnabled = false;
                LabelHeuristic1.IsEnabled = false;
                ComboBoxHeuristic1.IsEnabled = false;
                ComboBoxAlgorithm1.SelectedIndex = -1;
                ComboBoxHeuristic1.SelectedIndex = -1;
            }
        }
        private void cmb_Player2SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (ComboBoxPlayer2.SelectedIndex == 1)
            {
                parameters.selectedPlayer2 = 1;
                parameters.selectedAlgorithm2 = ComboBoxAlgorithm2.SelectedIndex;
                parameters.selectedHeuristic2 = ComboBoxHeuristic2.SelectedIndex;
                LabelAlgorithm2.IsEnabled = true;
                ComboBoxAlgorithm2.IsEnabled = true;
                LabelHeuristic2.IsEnabled = true;
                ComboBoxHeuristic2.IsEnabled = true;
                ComboBoxAlgorithm2.SelectedIndex = 0;
                ComboBoxHeuristic2.SelectedIndex = 0;
            }
            else
            {
                parameters.selectedPlayer2 = 0;
                parameters.selectedAlgorithm2 = -1;
                parameters.selectedHeuristic2 = -1;
                LabelAlgorithm2.IsEnabled = false;
                ComboBoxAlgorithm2.IsEnabled = false;
                LabelHeuristic2.IsEnabled = false;
                ComboBoxHeuristic2.IsEnabled = false;
                ComboBoxAlgorithm2.SelectedIndex = -1;
                ComboBoxHeuristic2.SelectedIndex = -1;
            }
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnOK_Click(object sender, RoutedEventArgs e)
        {
            OnOKEvent(this.parameters);
     
            this.Close();
        }
    }
}

