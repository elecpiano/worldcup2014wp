using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Windows.Input;
using WorldCup2014WP.Models;
using WorldCup2014WP.Utility;

namespace WorldCup2014WP.Controls
{
    public partial class GuessControl : UserControl
    {
        public event EventHandler<GuessMakingArgument> MakeGuess;

        public GuessGameOption SelectedOption
        {
            get
            {
                GuessGameOption option = null;
                GuessGame game = this.GetDataContext<GuessGame>();
                if (radio0.IsChecked == true)
                {
                    option = game.Option0;
                }
                else if (radio1.IsChecked == true)
                {
                    option = game.Option1;
                }
                else if (radio2.IsChecked == true)
                {
                    option = game.Option2;
                }

                return option;
            }
        }

        public GuessControl()
        {
            InitializeComponent();
        }

        private void Guess_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            if (MakeGuess != null)
            {
                GuessGame game = sender.GetDataContext<GuessGame>();

                

                GuessMakingArgument arg = new GuessMakingArgument();
                arg.GameID = game.ID;
                arg.Option = SelectedOption;
                arg.Gold = ((int)goldSlider.Value) * 500;
                MakeGuess(this, arg);
            }
        }

        private void slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (goldSlider != null)
            {
                bet.Text = (((int)goldSlider.Value) * 500).ToString();
            }
        }
    }
}
