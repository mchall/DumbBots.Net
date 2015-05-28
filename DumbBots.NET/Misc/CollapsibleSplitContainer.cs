using System;
using System.Drawing;
using System.Windows.Forms;

namespace Misc
{
    /// <summary>
    /// Based off code from http://www.codeproject.com/Articles/3025/Collapsible-Splitter-control-in-C
    /// </summary>
    public class CollapsibleSplitContainer : SplitContainer
    {
        private const int SplitterHeight = 8;

        private Rectangle rr;
        private bool _hot;
        private bool _collapsed;
        private int _previousSplitterDistance;

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle r = this.SplitterRectangle;
            e.Graphics.FillRectangle(new SolidBrush(this.BackColor), r);

            // create a new rectangle in the horizontal center of the splitter for our collapse control button
            rr = new Rectangle((int)r.X + ((r.Width - 115) / 2), r.Y, 115, 8);

            this.SplitterWidth = SplitterHeight;

            // draw the background color for our control image
            if (_hot)
            {
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Highlight), new Rectangle(rr.X, rr.Y + 1, 115, 6));
            }
            else
            {
                e.Graphics.FillRectangle(new SolidBrush(this.BackColor), new Rectangle(rr.X, rr.Y + 1, 115, 6));
            }

            // draw the left & right lines for our control image
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark, 1), rr.X, rr.Y + 1, rr.X, rr.Y + rr.Height - 2);
            e.Graphics.DrawLine(new Pen(SystemColors.ControlDark, 1), rr.X + rr.Width, rr.Y + 1, rr.X + rr.Width, rr.Y + rr.Height - 2);

            if (this.Enabled)
            {
                // draw the arrows for our control image
                // the ArrowPointArray is a point array that defines an arrow shaped polygon
                e.Graphics.FillPolygon(new SolidBrush(SystemColors.ControlDarkDark), ArrowPointArray(rr.X + 3, rr.Y + 2));
                e.Graphics.FillPolygon(new SolidBrush(SystemColors.ControlDarkDark), ArrowPointArray(rr.X + rr.Width - 9, rr.Y + 2));
            }

            // draw the dots for our control image using a loop
            int x = rr.X + 14;
            int y = rr.Y + 3;

            for (int i = 0; i < 30; i++)
            {
                // light dot
                e.Graphics.DrawLine(new Pen(SystemColors.ControlLightLight), x + (i * 3), y, x + 1 + (i * 3), y + 1);
                // dark dot
                e.Graphics.DrawLine(new Pen(SystemColors.ControlDarkDark), x + 1 + (i * 3), y + 1, x + 2 + (i * 3), y + 2);
                // overdraw the background color as we actually drew 2px diagonal lines, not just dots
                if (_hot)
                {
                    e.Graphics.DrawLine(new Pen(SystemColors.Highlight), x + 1 + (i * 3), y + 2, x + 2 + (i * 3), y + 2);
                }
                else
                {
                    e.Graphics.DrawLine(new Pen(this.BackColor), x + 1 + (i * 3), y + 2, x + 2 + (i * 3), y + 2);
                }
            }
        }

        private Point[] ArrowPointArray(int x, int y)
        {
            Point[] point = new Point[3];

            if (!_collapsed)
            {
                // up arrow
                point[0] = new Point(x + 3, y);
                point[1] = new Point(x + 6, y + 4);
                point[2] = new Point(x, y + 4);
            }
            else
            {
                // down arrow
                point[0] = new Point(x, y);
                point[1] = new Point(x + 6, y);
                point[2] = new Point(x + 3, y + 3);
            }

            return point;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            // check to see if the mouse cursor position is within the bounds of our control
            if (e.X >= rr.X && e.X <= rr.X + rr.Width && e.Y >= rr.Y && e.Y <= rr.Y + rr.Height)
            {
                if (!_hot)
                {
                    _hot = true;
                    Cursor = Cursors.Hand;
                    Invalidate();
                }
            }
            else
            {
                if (_hot)
                {
                    _hot = false;
                    Invalidate();
                }

                this.Cursor = Cursors.Default;

                if (_collapsed)
                    this.Cursor = Cursors.Default;
                else
                {
                    this.Cursor = Cursors.HSplit;
                }
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hot = false;
            this.Cursor = Cursors.Default;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!_hot)
            {
                base.OnMouseDown(e);
            }
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            if (_hot)
            {
                _collapsed = !_collapsed;

                if (_collapsed)
                {
                    _previousSplitterDistance = SplitterDistance;
                }

                IsSplitterFixed = _collapsed;
                SplitterDistance = _collapsed ? SplitterHeight : _previousSplitterDistance;
            }
        }
    }
}