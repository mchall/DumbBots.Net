using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DumbBots.BasicCoder
{
    /// <summary>
    /// Interaction logic for ParameterWindow.xaml
    /// </summary>
    public partial class ParameterWindow : Window
    {
        private Command _command;

        public Command ParameterCommand { get; private set; }

        public ParameterWindow(Window owner, Command command)
        {
            InitializeComponent();
            Owner = owner;
            _command = command;
            PopulateCommands();
            UpdateUI();
        }

        private void UpdateUI()
        {
            switch (_command.ParameterType)
            {
                case (SupportedType.Number):
                case (SupportedType.Text):
                    break;
                default:
                    btnOK.Visibility = pnlManual.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void PopulateCommands()
        {
            pnlCurrentCmd.Children.Add(new CommandBlock(_command));

            foreach (var command in Commands.CommandList)
            {
                if (command.ReturnType == _command.ParameterType)
                {
                    Button button = new Button();
                    button.Content = command.Text;
                    button.Margin = new Thickness(5);
                    button.Click += (s, e) =>
                        {
                            ParameterCommand = command;
                            this.DialogResult = true;
                        };
                    commandPanel.Children.Add(button);
                }
            }
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            switch (_command.ParameterType)
            {
                case (SupportedType.Number):

                    int result;
                    if (int.TryParse(txtManualValue.Text, out result))
                    {
                        ParameterCommand = new ManualNumberCommand() { Value = result };
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("The manually entered value is not a valid number", "Error");
                    }
                    break;

                case (SupportedType.Text):
                    ParameterCommand = new ManualStringCommand() { Value = txtManualValue.Text };
                    this.DialogResult = true;
                    break;
            }
        }
    }
}