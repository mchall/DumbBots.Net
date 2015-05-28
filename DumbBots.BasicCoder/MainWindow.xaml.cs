using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using Microsoft.Win32;

namespace DumbBots.BasicCoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            PopulateDragCommands();
        }

        private void PopulateDragCommands()
        {
            panelProgrammingCmds.Children.Add(new IfCommandBlock(true));

            foreach (var command in Commands.CommandList)
            {
                CommandBlock block = new CommandBlock(command, true);
                switch (command.Category)
                {
                    case (Category.Combat):
                        panelCombatCmds.Children.Add(block);
                        break;
                    case (Category.EnemyInformation):
                        panelEnemyInformationCmds.Children.Add(block);
                        break;
                    case (Category.Movement):
                        panelMovementCmds.Children.Add(block);
                        break;
                    case (Category.PlayerInformation):
                        panelPlayerInformationCmds.Children.Add(block);
                        break;
                    case (Category.WorldInformation):
                        panelWorldInformationCmds.Children.Add(block);
                        break;
                }
            }
        }

        private void New_Click(object sender, RoutedEventArgs e)
        {
            MainStatement.Clear();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            dialog.Filter = "XML|*.xml";
            if(dialog.ShowDialog() == true)
            {
                try
                {
                    var xml = File.ReadAllText(dialog.FileName);
                    var deserializedObj = (CodeSerializationObject)SerializationHelper.FromXml(xml, typeof(CodeSerializationObject));

                    MainStatement.Clear();
                    MainStatement.Populate(deserializedObj.Statements);
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occured loading and populating the data");
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            dialog.Filter = "XML|*.xml";
            if (dialog.ShowDialog() == true)
            {
                try
                {
                    CodeSerializationObject obj = new CodeSerializationObject()
                    {
                        Statements = MainStatement.GetSerializationObjects(),
                    };
                    var xml = SerializationHelper.ToXml(obj);

                    File.WriteAllText(dialog.FileName, xml);
                }
                catch (Exception)
                {
                    MessageBox.Show("An error occured saving the data");
                }
            }
        }

        private void CSharp_Click(object sender, RoutedEventArgs e)
        {
            string[] code;
            try
            {
                code = MainStatement.GetCode();
            }
            catch (Exception)
            {
                MessageBox.Show("Could not generate code, an IF statement is not complete");
                return;
            }

            StringBuilder sb = new StringBuilder();
            sb.AppendLine("using System;");
            sb.AppendLine("using System.Drawing;");
            sb.AppendLine("using DumbBotsNET.Api;");
            sb.AppendLine();
            sb.AppendLine("public class AIScript : ICommand");
            sb.AppendLine("{");
            sb.AppendLine("\tpublic void Think(IPlayerApi api)");
            sb.AppendLine("\t{");

            foreach (var line in code)
            {
                sb.AppendLine("\t\t" + line);
            }

            sb.AppendLine("\t}");
            sb.AppendLine("}");

            Clipboard.SetText(sb.ToString());
            MessageBox.Show("The code was copied to the clipboard.\nPlease paste the code (Ctrl-V) into the DumbBots.NET code editor and click 'Update Team 1'");
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(String.Format("{0}BasicCoder.htm", AppDomain.CurrentDomain.BaseDirectory));
        }
    }
}