using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DumbBots.BasicCoder
{
    /// <summary>
    /// Interaction logic for ConditionWindow.xaml
    /// </summary>
    public partial class ConditionWindow : Window
    {
        private Command _command;

        public Operator Operator { get; private set; }
        public Command ConditionalCommand { get; private set; }

        public ConditionWindow(Window owner, Command command)
        {
            InitializeComponent();
            Owner = owner;
            _command = command;
            PopulateCommands();
            UpdateUI();
        }

        private void UpdateUI()
        {
            switch (_command.ReturnType)
            {
                case (SupportedType.YesNo):
                    btnOK.Visibility = pnlManual.Visibility = rbLesser.Visibility = rbGreater.Visibility = Visibility.Collapsed;
                    break;
                case (SupportedType.Number):
                    break;
            }
        }

        private void PopulateCommands()
        {
            pnlCurrentCmd.Children.Add(new CommandBlock(_command));

            foreach (var command in Commands.CommandList)
            {
                if (command.GetType() != _command.GetType() && command.ReturnType == _command.ReturnType)
                {
                    Button button = new Button();
                    button.Content = command.Text;
                    button.Margin = new Thickness(5);
                    button.Click += (s, e) =>
                        {
                            ConditionalCommand = command;
                            SetOperator();
                            this.DialogResult = true;
                        };
                    commandPanel.Children.Add(button);
                }
            }
        }

        private void SetOperator()
        {
            if (rbEqual.IsChecked == true)
                Operator = Operator.Equal;
            else if (rbNotEqual.IsChecked == true)
                Operator = Operator.NotEqual;
            else if (rbLesser.IsChecked == true)
                Operator = Operator.LessThan;
            else if (rbGreater.IsChecked == true)
                Operator = Operator.GreaterThan;
        }

        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            switch (_command.ReturnType)
            {
                case (SupportedType.Number):

                    int result;
                    if (int.TryParse(txtManualValue.Text, out result))
                    {
                        ConditionalCommand = new ManualNumberCommand() { Value = result };
                        SetOperator();
                        this.DialogResult = true;
                    }
                    else
                    {
                        MessageBox.Show("The manually entered value is not a valid number", "Error");
                    }
                    break;
            }
        }
    }
}