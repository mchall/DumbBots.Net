using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DumbBots.BasicCoder
{
    /// <summary>
    /// Interaction logic for IfElseBlock.xaml
    /// </summary>
    public partial class IfElseBlock : UserControl, ICodeElement
    {
        public event Action Deleted;

        public IfElseBlock()
        {
            InitializeComponent();
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.Salmon;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.Transparent;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Deleted != null)
            {
                Deleted();
            }
        }

        public string[] GetCode()
        {
            List<string> code = new List<string>();
            code.Add(String.Format("if({0})", IfCondition.GetCode()[0]));
            code.Add("{");
            code.AddRange(IfStatement.GetCode().Select(s => "\t" + s));
            code.Add("}");
            code.Add("else");
            code.Add("{");
            code.AddRange(ElseStatement.GetCode().Select(s => "\t" + s));
            code.Add("}");
            return code.ToArray();
        }

        public object[] GetSerializationObjects()
        {
            return new object[1] { new IfElseSerializationObject()
            {
                Condition = IfCondition.Condition,
                IfStatements = IfStatement.GetSerializationObjects(),
                ElseStatements = ElseStatement.GetSerializationObjects(),
            }};
        }

        public void Clear()
        {
            IfCondition.Clear();
            IfStatement.Clear();
            ElseStatement.Clear();
        }

        public void Populate(object[] objs)
        {
            foreach (var obj in objs)
            {
                var ifElse = obj as IfElseSerializationObject;
                if (ifElse != null)
                {
                    IfCondition.Populate(new object[1] { ifElse.Condition });
                    IfStatement.Populate(ifElse.IfStatements ?? new object[0]);
                    ElseStatement.Populate(ifElse.ElseStatements ?? new object[0]);
                }
            }
        }
    }
}