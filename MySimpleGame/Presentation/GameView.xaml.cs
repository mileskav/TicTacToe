using System.Windows;
using System.Windows.Controls;

namespace Demo_Wpf_TheSimpleGame.Presentation
{
    /// <summary>
    /// Interaction logic for TheSimpleGameView.xaml
    /// </summary>
    public partial class GameView : Window
    {
        GameViewModel _gameViewModel;

        public GameView(GameViewModel gameViewModel)
        {
            _gameViewModel = gameViewModel;
            InitializeComponent();
        }

        private void GameBoard_Click(object sender, RoutedEventArgs e)
        {
            Button boardPositionButton = sender as Button;
            int row = int.Parse(boardPositionButton.Tag.ToString().Substring(0, 1));
            int column = int.Parse(boardPositionButton.Tag.ToString().Substring(1, 1));

            _gameViewModel.PlayerMove(row, column);
        }

        private void WindowButton_Click(object sender, RoutedEventArgs e)
        {
            Button windowButton = sender as Button;

            switch (windowButton.Name)
            {
                case "NewGame":
                case "ResetGame":
                    _gameViewModel.GameCommand(windowButton.Name);
                    break;
                case "Help":
                    _gameViewModel.GameCommand(windowButton.Name);
                    break;
                case "Quit":
                    _gameViewModel.GameCommand(windowButton.Name);
                    Close();
                    break;
            }
        }
    }
}
