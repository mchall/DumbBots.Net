using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DumbBots.BasicCoder
{
    /// <summary>
    /// Interaction logic for StatementBlock.xaml
    /// </summary>
    public partial class StatementBlock : UserControl, ICodeElement
    {
        public StatementBlock()
        {
            InitializeComponent();
        }

        private void rootPanel_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(Command)))
            {
                Command cmd = e.Data.GetData(typeof(Command)) as Command;

                if (cmd.ParameterType != SupportedType.Nothing)
                {
                    ParameterWindow paramWindow = new ParameterWindow(Application.Current.Windows[0], cmd);
                    paramWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    if (paramWindow.ShowDialog() == true)
                    {
                        cmd.ParameterCommand = paramWindow.ParameterCommand;
                    }
                    else
                    {
                        e.Handled = true;
                        return;
                    }
                }

                Populate(cmd);
                e.Handled = true;
            }

            if (e.Data.GetDataPresent(typeof(IfCommandBlock)))
            {
                IfCommandBlock cmd = e.Data.GetData(typeof(IfCommandBlock)) as IfCommandBlock;
                if (cmd != null)
                {
                    Populate(new IfElseSerializationObject());
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

            if (!e.Data.GetDataPresent(typeof(Command)) && !e.Data.GetDataPresent(typeof(IfCommandBlock)))
            {
                e.Effects = DragDropEffects.None;
            }
        }

        public string[] GetCode()
        {
            List<string> code = new List<string>();
            foreach (UIElement child in rootPanel.Children)
            {
                var codeElement = child as ICodeElement;
                if (codeElement != null)
                {
                    code.AddRange(codeElement.GetCode());
                }
            }
            return code.ToArray();
        }

        public object[] GetSerializationObjects()
        {
            List<object> objList = new List<object>();
            foreach (UIElement child in rootPanel.Children)
            {
                var codeElement = child as ICodeElement;
                if (codeElement != null)
                {
                    objList.AddRange(codeElement.GetSerializationObjects());
                }
            }
            return objList.ToArray();
        }

        public void Clear()
        {
            rootPanel.Children.Clear();
        }

        public void Populate(object[] objs)
        {
            foreach (var obj in objs)
            {
                var command = obj as Command;
                if (command != null)
                {
                    Populate(command);
                }

                var ifElseObj = obj as IfElseSerializationObject;
                if (ifElseObj != null)
                {
                    Populate(ifElseObj);
                }
            }
        }

        private void Populate(Command command)
        {
            var block = new CommandBlock(command, canDelete: true);
            block.Margin = new Thickness(5, 2, 5, 13);
            block.Deleted += () =>
            {
                rootPanel.Children.Remove(block);
            };
            rootPanel.Children.Add(block);
        }

        private void Populate(IfElseSerializationObject ifElse)
        {
            var ifBlock = new IfElseBlock();
            ifBlock.Deleted += () =>
            {
                rootPanel.Children.Remove(ifBlock);
            };
            rootPanel.Children.Add(ifBlock);
            ifBlock.Populate(new object[1] { ifElse });
        }
    }
}