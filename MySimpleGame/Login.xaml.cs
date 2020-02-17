using Demo_Wpf_TheSimpleGame.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Demo_Wpf_TheSimpleGame.Business;

namespace MySimpleGame
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GameBusiness gameBusiness = new GameBusiness();
            gameBusiness.SaveAllPlayers();
            Close();
        }

        private void PlayerX_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (PlayerX.Text == "")
            {
                feedback.Text = "Name field must not be blank.";
            }
            else
            {
                feedback.Text = "";
            }
        }

        private void PlayerO_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (PlayerO.Text == "")
            {
                feedback.Text = "Name field must not be blank.";
            }
            else
            {
                feedback.Text = "";
            }
        }
    }
}
