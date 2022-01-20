﻿using Contract;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ProjectPaint
{
    [Serializable()]
    enum TextStyle : int
    {
        BOLD = 1,
        ITALIC = 2,
        UNDERLINE = 3,
        NONE=0
    }
    [Serializable]
    class TextBox2D : IShape
    {
        private Point2D _leftTop = new Point2D();
        private Point2D _rightBottom = new Point2D();
        private Point2D _leftTopForDraw = new Point2D();
        private Point2D _rightBottomForDraw = new Point2D();
        public double width { get => rect.Width; }
        public double height { get => rect.Height; }

        public string _fontFamily { get; set; }
        
        public string _textString { get; set; }

        public TextStyle _textStyle { get; set; }

        double _sizeText { get; set; }

        [NonSerialized]
        Rectangle rect = null;
        [NonSerialized]
        TextBox textBox = null;
        public Point2D GetPoint1()
        {
            return _leftTop;
        }
        public Point2D GetPoint2()
        {
            return _rightBottom;
        }
        public void setStyle(TextStyle style,double size,string font)
        {
            _sizeText = size;
            _fontFamily = font;
            _textStyle=style;
        }
        public string Name => "TextBox";
        public UIElement Draw()
        {
            var textBoxx = new TextBox()
            {
                Width = Math.Abs(_rightBottomForDraw.X - _leftTopForDraw.X),
                Height = Math.Abs(_rightBottomForDraw.Y - _leftTopForDraw.Y),
                TextWrapping = TextWrapping.Wrap,
                BorderBrush = Brushes.Transparent,
                FontFamily = new FontFamily(_fontFamily),
                FontSize = _sizeText,
            };
        
            var textBlock = new TextBlock()
            {
                Text = _textString,
                Width = Math.Abs(_rightBottomForDraw.X- _leftTopForDraw.X),
                Height = Math.Abs(_rightBottomForDraw.Y - _leftTopForDraw.Y),
                TextWrapping = TextWrapping.Wrap,
                FontFamily = new FontFamily(_fontFamily),
                FontSize = _sizeText,
                RenderTransform = textBoxx.RenderTransform,
                RenderTransformOrigin = textBoxx.RenderTransformOrigin,
            };

            if (_textStyle == TextStyle.ITALIC)
            {
                textBlock.FontStyle = FontStyles.Italic;
            }
            else if (_textStyle == TextStyle.UNDERLINE)
            {
                textBlock.TextDecorations = TextDecorations.Underline;
            }
            else if (_textStyle == TextStyle.BOLD)
            {
                textBlock.FontWeight = FontWeights.Bold;
            }

            Canvas.SetLeft(textBlock, _leftTopForDraw.X);
            Canvas.SetTop(textBlock, _leftTopForDraw.Y);

            return textBlock;
        }
        public UIElement DrawRect()
        {
            rect = new Rectangle()
            {
                Width = Math.Abs(_rightBottom.X - _leftTop.X),
                Height = Math.Abs(_rightBottom.Y - _leftTop.Y),
                
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 1,
                StrokeDashArray = new System.Windows.Media.DoubleCollection(new double[] { 4, 4 })
            };

            System.Windows.Controls.Canvas.SetLeft(rect, _leftTop.X);
            Canvas.SetTop(rect, _leftTop.Y);
            return rect;
        }

        public void HandleStart(double x, double y)
        {
            _leftTop = new Point2D() { X = x, Y = y };
        }

        public void HandleEnd(double x, double y)
        {
            _rightBottom = new Point2D() { X = x, Y = y };
        }

        public IShape Clone()
        {
            return new TextBox2D();
        }
        public String toString()
        {
            return Name + " " + _leftTop.toString() + " " + _rightBottom.toString();
        }

        public void setColor(Color color)
        {
        }

        public void setWidth(int widthStroke)
        {
        }

        public void setStyle(string style)
        {

        }

        public void DrawMove(Canvas canvas)
        {
            if (rect == null)
            {
                this.DrawRect();
                canvas.Children.Add(rect);
            }

            var x = Math.Min(_rightBottom.X, _leftTop.X);
            var y = Math.Min(_rightBottom.Y, _leftTop.Y);

            var w = Math.Max(_rightBottom.X, _leftTop.X) - x;
            var h = Math.Max(_rightBottom.Y, _leftTop.Y) - y;

            rect.Width = w;
            rect.Height = h;

            Canvas.SetLeft(rect, x);
            Canvas.SetTop(rect, y);
        }
        public void RemoveOutline(Canvas canvas)
        {
            if (rect != null)
            {
                canvas.Children.Remove(rect);
            }
        }

       

        internal TextBox DrawTextBox(Canvas canvas)
        {
            textBox = new TextBox()
            {
                Width = rect.Width,
                Height = rect.Height,
                TextWrapping = TextWrapping.Wrap,
                BorderBrush = Brushes.Transparent,
                FontFamily = new FontFamily(_fontFamily),
                FontSize = _sizeText,
            };
            if (_textStyle == TextStyle.ITALIC)
            {
                textBox.FontStyle = FontStyles.Italic;
            }
            else if (_textStyle == TextStyle.UNDERLINE)
            {
                textBox.TextDecorations = TextDecorations.Underline;
            }
            else if (_textStyle == TextStyle.BOLD)
            {
                textBox.FontWeight = FontWeights.Bold;
            }
            return textBox;
        }

        internal void setNew(TextStyle style, double fontSize, string v, string text, Point2D point1, Point2D point2)
        {
            _textString = text;
            _sizeText = fontSize;
            _fontFamily = v;
            _textStyle = style;
            _leftTopForDraw = point1;
            _rightBottomForDraw = point2;
        }
    }
}