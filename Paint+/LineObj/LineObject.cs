using BaseObj;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace LineObj
{
    public class LineObject : BaseObject
    {
        public Line rect;
        public UIElement Create()
        {
            rect = new Line();
            rect.X1 = 0;
            rect.X2 = 300;
            rect.Y1 = 0;
            rect.Y2 = 300;
            Canvas.SetLeft(rect, 0);
            Canvas.SetTop(rect, 0);
            return rect;
        }
        public override void setColor(Color color)
        {
            rect.Stroke = new SolidColorBrush(color);
        }
        public override void setColorFill(Color color)
        {
            rect.Fill = new SolidColorBrush(color);
        }
        public override void setSize(double size)
        {
            rect.StrokeThickness = penWidth;
        }
        public override void setStyle(StyleLines style)
        {
            if (styleLine == StyleLines.Dash)
            {
                double[] dashes = { 4, 4 };
                rect.StrokeDashArray = new System.Windows.Media.DoubleCollection(dashes);
            }
            else if (styleLine == StyleLines.Dot)
            {
                double[] dashes = { 1, 1 };
                rect.StrokeDashArray = new System.Windows.Media.DoubleCollection(dashes);
            }
            else if (styleLine == StyleLines.DashDot)
            {
                double[] dashes = { 4, 1, 1, 1 };
                rect.StrokeDashArray = new System.Windows.Media.DoubleCollection(dashes);
            }
            else if (styleLine == StyleLines.DashDotDot)
            {
                double[] dashes = { 4, 1, 1, 1, 1, 1 };
                rect.StrokeDashArray = new System.Windows.Media.DoubleCollection(dashes);
            }
        }
    }
}
