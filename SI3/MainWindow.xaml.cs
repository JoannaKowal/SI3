
using SI3Backend;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using ComboBox = System.Windows.Controls.ComboBox;

namespace SI3
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly int HUMAN_DROPDOWN_NUMBER = 0;
        private static readonly int AI_DROPDOWN_NUMBER = 1;
        private static readonly int MIN_MAX_DROPDOWN_NUMBER = 0;
        private static readonly int ALPHA_BETA_DROPDOWN_NUMBER = 1;
        private static readonly int FAST_ALPHA_BETA_DROPDOWN_NUMBER = 2;

        private static Dictionary<int, Func<Heuristic>> heuristicDictionary;

        private GameEngine gameEngine = null;
        private PlayersController aiPlayersController = null;

        private float timePassed;
        private bool shouldLogToFile;
        private string firstPlayerType;
        private string secondPlayerType;
        private string numberOfMovesTemplateText = "Turns: {0}";
        private string timerTemplateText = "Time[s]: {0}";
        private string currentMovingPlayerTemplateText = "Turn: Player {0}";
        private string winningPlayerTextTemplate = "Won: ";

        private string[] playerTypes = new string[] { "Człowiek", "AI" };
        private string[] algorithmTypes = new string[] { "Min-Max", "Alfa-beta", "Alfa-Beta H" };
        private int[] depthPossibilities = new int[] { 1, 2, 3, 4, 5, 6 };
        private string[] heuristicType = new string[] { "Pionki", "Pionki+Młynki", "Pionki+Ruchy" };


        private GameField[] gameFieldButtons = null;

        static MainWindow()
        {
            heuristicDictionary = new Dictionary<int, Func<Heuristic>>();
            heuristicDictionary[0] = () => new SimplePawnNumberHeuristic();
            heuristicDictionary[1] = () => new PawnMillNumberHeuristic();
            heuristicDictionary[2] = () => new PawnMoveNumberHeuristic();
        }

        public MainWindow()
        {
            InitializeComponent();
            InitDropdowns();
            InitPawnButtonHandlers();
        }

        private void InitDropdowns()
        {
            first_player_type_dropdown.ItemsSource = playerTypes;
            first_player_type_dropdown.SelectedIndex = 0;
            second_player_type_dropdown.ItemsSource = playerTypes;
            second_player_type_dropdown.SelectedIndex = 0;

            first_player_algorithm_dropdown.ItemsSource = algorithmTypes;
            first_player_algorithm_dropdown.SelectedIndex = 0;
            second_player_algorithm_dropdown.ItemsSource = algorithmTypes;
            second_player_algorithm_dropdown.SelectedIndex = 0;

            first_player_depth_dropdown.ItemsSource = depthPossibilities;
            first_player_depth_dropdown.SelectedIndex = 0;
            second_player_depth_dropdown.ItemsSource = depthPossibilities;
            second_player_depth_dropdown.SelectedIndex = 0;

            first_player_heuristic_dropdown.ItemsSource = depthPossibilities;
            first_player_heuristic_dropdown.SelectedIndex = 0;
            second_player_heuristic_dropdown.ItemsSource = depthPossibilities;
            second_player_heuristic_dropdown.SelectedIndex = 0;
        }

        private void InitPawnButtonHandlers()
        {
            this.gameFieldButtons = new GameField[]
            {
                field_0, field_1, field_2, field_3, field_4, field_5,
                field_6, field_7, field_8, field_9, field_10, field_11,
                field_12, field_13, field_14, field_15, field_16, field_17,
                field_18, field_19, field_20, field_21, field_22, field_23
            };

            for (int i = 0; i < gameFieldButtons.Length; i++)
            {
                gameFieldButtons[i].Initialize(this, i);
            }
        }

        private void SetAIDropdownsActive(int playerType, PlayerNumber playerNumber)
        {
            if(playerNumber == PlayerNumber.FirstPlayer)
            {
                if (playerType == HUMAN_DROPDOWN_NUMBER)
                {
                    first_player_algorithm_dropdown.IsEnabled = false;
                    first_player_heuristic_dropdown.IsEnabled = false;
                    first_player_depth_dropdown.IsEnabled = false;
                }
                else
                {
                    first_player_algorithm_dropdown.IsEnabled = true;
                    first_player_heuristic_dropdown.IsEnabled = true;
                    first_player_depth_dropdown.IsEnabled = true;
                }
            }
            else
            {
                if (playerType == HUMAN_DROPDOWN_NUMBER)
                {
                    second_player_algorithm_dropdown.IsEnabled = false;
                    second_player_heuristic_dropdown.IsEnabled = false;
                    second_player_depth_dropdown.IsEnabled = false;
                }
                else
                {
                    second_player_algorithm_dropdown.IsEnabled = true;
                    second_player_heuristic_dropdown.IsEnabled = true;
                    second_player_depth_dropdown.IsEnabled = true;
                }
            }
        }

        private AiPlayer InitPlayer(PlayerNumber playerNumber)
        {
            ComboBox playerDropdown;
            ComboBox algorithmDropdown;
            ComboBox heuristicDropdown;
            ComboBox searchDepthDropdown;
            if (playerNumber == PlayerNumber.FirstPlayer)
            {
                playerDropdown = first_player_type_dropdown;
                algorithmDropdown = first_player_algorithm_dropdown;
                heuristicDropdown = first_player_heuristic_dropdown;
                searchDepthDropdown = first_player_depth_dropdown;
            }
            else
            {
                playerDropdown = second_player_type_dropdown;
                algorithmDropdown = second_player_algorithm_dropdown;
                heuristicDropdown = second_player_heuristic_dropdown;
                searchDepthDropdown = second_player_depth_dropdown;
            }
            if (playerDropdown.SelectedIndex == AI_DROPDOWN_NUMBER)
            {
                Heuristic heuristic = heuristicDictionary[heuristicDropdown.SelectedIndex]();
                int searchDepth = searchDepthDropdown.SelectedIndex + 1;
                if (algorithmDropdown.SelectedIndex == MIN_MAX_DROPDOWN_NUMBER)
                {
                    SavePlayerType(playerNumber, "Min-Max: " + heuristic.GetType().Name);
                    return new MinMaxAiPlayer(gameEngine, heuristic, playerNumber, searchDepth);
                }
                else if (algorithmDropdown.SelectedIndex == ALPHA_BETA_DROPDOWN_NUMBER)
                {
                    SavePlayerType(playerNumber, "Alfa-Beta: " + heuristic.GetType().Name);
                    return new AlphaBetaAiPlayer(gameEngine, heuristic, playerNumber, searchDepth);
                }
                else
                {
                    SavePlayerType(playerNumber, "Alfa-Beta H: " + heuristic.GetType().Name);
                    Heuristic sortHeuristic = new SimplePawnNumberHeuristic();
                    return new FastAlphaBetaAiPlayer(gameEngine, heuristic, playerNumber, searchDepth, sortHeuristic);
                }
            }
            else
            {
                SavePlayerType(playerNumber, "Człowiek");
            }
            return null;
        }

        private void SavePlayerType(PlayerNumber playerNumber, string type)
        {
            if (playerNumber == PlayerNumber.FirstPlayer)
            {
                firstPlayerType = type;
            }
            else
            {
                secondPlayerType = type;
            }
        }

        private void OnBoardUpdated(Board newBoard)
        {
            for (int i = 0; i < gameFieldButtons.Length; i++)
            {
                Field field = newBoard.GetField(i);
                gameFieldButtons[i].RefreshImage(field.PawnPlayerNumber);
            }
        }

        private void OnPlayerTurnChanged(PlayerNumber currentMovingPlayerNumber)
        {
            if (currentMovingPlayerNumber == PlayerNumber.FirstPlayer)
            {
                UpdateTurnText(1);
            }
            else
            {
                UpdateTurnText(2);
            }
        }

        private void UpdateTurnText(int playerNumber)
        {
            turn_label.Content = string.Format(currentMovingPlayerTemplateText, playerNumber);
        }

        private void OnGameFinished(PlayerNumber winningPlayer)
        {
            UpdateWinningPlayerText(winningPlayer);
            SaveLogs();
            gameEngine.OnBoardChanged -= OnBoardUpdated;
            gameEngine.OnGameFinished -= OnGameFinished;
            gameEngine.OnPlayerTurnChanged -= OnPlayerTurnChanged;
            gameEngine.OnPlayerTurnChanged -= aiPlayersController.OnPlayerTurnChanged;
            gameEngine = null;
            aiPlayersController = null;
        }

        private void SaveLogs()
        {
            if (shouldLogToFile)
            {
                string moves = gameEngine.GameState.MovesUntilNow;
                string gameLog = firstPlayerType + " vs. " + secondPlayerType + "\n";
                gameLog += "Won: " + (gameEngine.GameState.WinningPlayer == PlayerNumber.FirstPlayer ? "White" : "Black") + "\n";
                float firstPlayerTime = aiPlayersController.firstPlayerDecisionTimeMillis / 1000f;
                float secondPlayerTime = aiPlayersController.secondPlayerDecisionTimeMillis / 1000f;
                if (firstPlayerTime == 0)
                {
                    firstPlayerTime = timePassed - (secondPlayerTime / (1000f));
                }
                gameLog += "White moves: " + gameEngine.GameState.FirstPlayerMovesMade + "\n";
                gameLog += "Black moves: " + gameEngine.GameState.SecondPlayerMovesMade + "\n";
                gameLog += "White time: " + firstPlayerTime + "\n";
                gameLog += "Black time: " + secondPlayerTime + "\n";
                if (aiPlayersController.firstAiPlayer != null)
                {
                    gameLog += "White visited nodes: " + aiPlayersController.firstAiPlayer.visitedNodes + "\n";
                }
                if (aiPlayersController.secondAiPlayer != null)
                {
                    gameLog += "Black visited nodes: " + aiPlayersController.secondAiPlayer.visitedNodes + "\n";
                }
                gameLog += "Moves: \n";
                gameLog += moves;
                try
                {
                    File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"\" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".txt", gameLog);
                }
                catch (Exception e)
                {

                }
            }
        }

        private void UpdateWinningPlayerText(PlayerNumber winningPlayer)
        {
            string winningPlayerString = "Player 1";
            if (winningPlayer == PlayerNumber.SecondPlayer)
            {
                winningPlayerString = "Player 2";
            }
            else if (winningPlayer == PlayerNumber.None)
            {
                winningPlayerString = "";
            }
            winning_player_label.Content = winningPlayerTextTemplate + winningPlayerString;
        }

        public void HandleButtonClick(int fieldIndex)
        {
            if (gameEngine != null)
            {
                gameEngine.HandleSelection(fieldIndex);
            }
        }

        private void Update()
        {
            MakeAiControllerStep();
            UpdateGameStateData();
        }

        private void MakeAiControllerStep()
        {
            if (aiPlayersController != null)
            {
                long timeMilis = aiPlayersController.CheckStep();
                timePassed += timeMilis / 1000f;
            }
        }

        private void UpdateGameStateData()
        {
            if (gameEngine != null)
            {
                UpdateMoveNumberText();
                UpdateTime();
            }
        }

        private void UpdateTime()
        {
            if (!gameEngine.GameState.GameFinished)
            {
                //timePassed += Time.deltaTime;
                time_label.Content = string.Format(timerTemplateText, Math.Truncate(timePassed * 100) / 100);
            }
        }

        private void UpdateMoveNumberText()
        {
            turns_number_label.Content = string.Format(numberOfMovesTemplateText, gameEngine.GameState.MovesMade);
        }

        private void btnNewGame_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }
        private void StartGame()
        {
            gameEngine = new GameEngine();
            AiPlayer firstPlayer = InitPlayer(PlayerNumber.FirstPlayer);
            AiPlayer secondPlayer = InitPlayer(PlayerNumber.SecondPlayer);
            aiPlayersController = new PlayersController(firstPlayer, secondPlayer);
            timePassed = 0;
            OnBoardUpdated(gameEngine.GameState.CurrentBoard);
            gameEngine.OnBoardChanged += OnBoardUpdated;
            gameEngine.OnGameFinished += OnGameFinished;
            gameEngine.OnPlayerTurnChanged += OnPlayerTurnChanged;
            gameEngine.OnPlayerTurnChanged += aiPlayersController.OnPlayerTurnChanged;
            UpdateWinningPlayerText(PlayerNumber.None);
            shouldLogToFile = log_file_checkbox.IsChecked ?? false;
        }
        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void First_player_type_dropdown_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SetAIDropdownsActive(first_player_type_dropdown.SelectedIndex, PlayerNumber.FirstPlayer);
        }

        private void Second_player_type_dropdown_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            SetAIDropdownsActive(second_player_type_dropdown.SelectedIndex, PlayerNumber.SecondPlayer);
        }
    }
}
