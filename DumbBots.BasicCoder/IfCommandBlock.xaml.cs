using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DumbBots.BasicCoder
{
    /// <summary>
    /// Interaction logic for IfCommandBlock.xaml
    /// </summary>
    public partial class IfCommandBlock : UserControl
    {
        private Point _startPoint;
        private bool _draggable;

        public IfCommandBlock(bool draggable = false)
        {
            InitializeComponent();
            _draggable = draggable;
            nameBlock.Text = "IF Block";
        }

        private void UserControl_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_draggable)
            {
                _startPoint = e.GetPosition(null);
            }
        }

        private void UserControl_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (_draggable)
            {
                Point mousePos = e.GetPosition(null);
                Vector diff = _startPoint - mousePos;

                if (e.LeftButton == MouseButtonState.Pressed &&
                    Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    DataObject dragData = new DataObject(typeof(IfCommandBlock), this);
                    DragDrop.DoDragDrop(this, dragData, DragDropEffects.Copy);
                }
            }
        }
    }
}