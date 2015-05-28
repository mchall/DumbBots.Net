using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DumbBots.BasicCoder
{
    /// <summary>
    /// Interaction logic for ConditionBlock.xaml
    /// </summary>
    public partial class ConditionBlock : UserControl, ICodeElement
    {
        public Condition Condition { get; private set; }

        public ConditionBlock()
        {
            InitializeComponent();
        }

        private void rootPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Command)))
            {
                Command cmd = e.Data.GetData(typeof(Command)) as Command;
                switch (cmd.ReturnType)
                {
                    case (SupportedType.Number):
                    case (SupportedType.YesNo):
                        break;

                    default:
                        e.Handled = true;

                        string toolTipText;
                        if (cmd.ReturnType == SupportedType.Nothing)
                            toolTipText = "Commands with no return type are not supported in IF statements";
                        else
                            toolTipText = "Sorry - the return type of this command is not currently supported :(";
                        ToolTip tooltip = new ToolTip { Content = toolTipText };
                        this.ToolTip = tooltip;
                        tooltip.IsOpen = true;

                        return;
                }

                ConditionWindow conditionWindow = new ConditionWindow(Application.Current.Windows[0], cmd);
                conditionWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (conditionWindow.ShowDialog() == true)
                {
                    Condition = new Condition();
                    Condition.LeftCommand = cmd;
                    Condition.Operator = conditionWindow.Operator;
                    Condition.RightCommand = conditionWindow.ConditionalCommand;

                    if (Condition.LeftCommand.ParameterType != SupportedType.Nothing)
                    {
                        //Need to fetch parameter
                        ParameterWindow paramWindow = new ParameterWindow(Application.Current.Windows[0], Condition.LeftCommand);
                        paramWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        if (paramWindow.ShowDialog() == true)
                        {
                            Condition.LeftCommand.ParameterCommand = paramWindow.ParameterCommand;
                        }
                        else
                        {
                            e.Handled = true;
                            return;
                        }
                    }

                    if (Condition.RightCommand.ParameterType != SupportedType.Nothing)
                    {
                        //Need to fetch parameter
                        ParameterWindow paramWindow = new ParameterWindow(Application.Current.Windows[0], Condition.RightCommand);
                        paramWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        if (paramWindow.ShowDialog() == true)
                        {
                            Condition.RightCommand.ParameterCommand = paramWindow.ParameterCommand;
                        }
                        else
                        {
                            e.Handled = true;
                            return;
                        }
                    }

                    Populate(Condition);
                }

                e.Handled = true;
            }
        }

        private void rootPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }

            if (!e.Data.GetDataPresent(typeof(Command)))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public string[] GetCode()
        {
            return new string[1]
            {
                String.Format("{0} {1} {2}", Condition.LeftCommand.Code, Condition.Operator.GetAttribute<CodeAttribute>().Code, Condition.RightCommand.Code).Replace(";", "")
            };
        }

        public object[] GetSerializationObjects()
        {
            return new object[1] { Condition };
        }

        public void Clear()
        {
            Condition = null;
            rootPanel.Children.Clear();
        }

        public void Populate(object[] objs)
        {
            foreach (var obj in objs)
            {
                var condition = obj as Condition;
                if (condition != null)
                {
                    Condition = condition;
                    Populate(Condition);
                }
            }
        }

        private void Populate(Condition condition)
        {
            rootPanel.Children.Clear();

            var block = new CommandBlock(Condition.LeftCommand);
            rootPanel.Children.Add(block);

            var opBlock = new TextBlock();
            opBlock.Text = Condition.Operator.GetDescription();
            opBlock.TextAlignment = TextAlignment.Center;
            opBlock.VerticalAlignment = VerticalAlignment.Center;
            rootPanel.Children.Add(opBlock);

            var blockOther = new CommandBlock(Condition.RightCommand);
            rootPanel.Children.Add(blockOther);
        }
    }
}