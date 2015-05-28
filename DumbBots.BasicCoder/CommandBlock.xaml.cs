using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DumbBots.BasicCoder
{
    /// <summary>
    /// Interaction logic for CommandBlock.xaml
    /// </summary>
    public partial class CommandBlock : UserControl, ICodeElement
    {
        private Point _startPoint;
        private bool _draggable;
        private bool _canDelete;

        public event Action Deleted;

        public Command Command { get; private set; }

        public CommandBlock(Command command, bool draggable = false, bool canDelete = false)
        {
            InitializeComponent();
            Command = command;
            _draggable = draggable;
            nameBlock.Text = command.Text;

            SetReturnTypeImage();

            _canDelete = canDelete;
            if (!_canDelete)
                imgDelete.Visibility = Visibility.Collapsed;
        }

        private void SetReturnTypeImage()
        {
            switch (Command.ReturnType)
            {
                case (SupportedType.Number):
                    imgReturnType.Source = new BitmapImage(new Uri("Resources/Numbers-icon.png", UriKind.Relative));
                    break;
                case (SupportedType.Position):
                    imgReturnType.Source = new BitmapImage(new Uri("Resources/map-icon.png", UriKind.Relative));
                    break;
                case (SupportedType.Text):
                    imgReturnType.Source = new BitmapImage(new Uri("Resources/Letters-icon.png", UriKind.Relative));
                    break;
                case (SupportedType.YesNo):
                    imgReturnType.Source = new BitmapImage(new Uri("Resources/bool-icon.png", UriKind.Relative));
                    break;
            }
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
                    DataObject dragData = new DataObject(typeof(Command), Command);
                    DragDrop.DoDragDrop(this, dragData, DragDropEffects.Copy);
                }
            }
        }

        private void Image_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.Salmon;
        }

        private void Image_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Background = Brushes.White;
        }

        private void Image_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_canDelete && Deleted != null)
            {
                Deleted();
            }
        }

        public string[] GetCode()
        {
            return new string[1] { Command.Code };
        }

        public object[] GetSerializationObjects()
        {
            return new object[1] { Command };
        }

        public void Clear()
        {
            //Do nothing
        }

        public void Populate(object[] objs)
        {
            //Do nothing
        }
    }
}