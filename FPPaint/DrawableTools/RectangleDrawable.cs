using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FPPaint
{
    /// <summary>
    /// Rectangle Drawing Tool Drawable Class
    /// </summary>
    public class RectangleDrawable : IDrawable, ICommand
    {
        private Canvas _container;
        private double tempX;
        private double tempY;
        public Rectangle Rectangle { get; private set; }

        public RectangleDrawable(Point coordinates, Brush color, Canvas container, int strokeThickness, bool isFilled)
        {
            _container = container;
            tempX = coordinates.X;
            tempY = coordinates.Y;
            if (isFilled)
            {
                Rectangle = new Rectangle
                {
                    Stroke = color,
                    StrokeThickness = strokeThickness,
                    Margin = new Thickness(coordinates.X, coordinates.Y, 0, 0),
                    Fill = color
                };
            }
            else
            {
                Rectangle = new Rectangle
                {
                    Stroke = color,
                    StrokeThickness = strokeThickness,
                    Margin = new Thickness(coordinates.X, coordinates.Y, 0, 0),
                };
            }

        }

        public void Draw(Point coordinates)
        {
            if (Rectangle != null)
            {
                double width = coordinates.X - tempX;
                double height = coordinates.Y - tempY;

                if(width < 0 && height <0)
                {
                    Rectangle.Margin = new Thickness(coordinates.X, coordinates.Y,0,0);
                    Rectangle.Width = Math.Abs(width);
                    Rectangle.Height = Math.Abs(height);
                }
                else if(width < 0 && height > 0)
                {
                    Rectangle.Margin = new Thickness(coordinates.X, tempY, 0, 0);
                    Rectangle.Width = Math.Abs(width);
                    Rectangle.Height = Math.Abs(height);
                }
                else if(width > 0 && height < 0)
                {
                    Rectangle.Margin = new Thickness(tempX, coordinates.Y, 0, 0);
                    Rectangle.Width = Math.Abs(width);
                    Rectangle.Height = Math.Abs(height);
                }
                else
                {
                    Rectangle.Margin = new Thickness(tempX, tempY, 0, 0);
                    Rectangle.Width = Math.Abs(width);
                    Rectangle.Height = Math.Abs(height);
                }
            }
        }

        public void Execute()
        {
            if (!_container.Children.Contains(Rectangle))
            {
                _container.Children.Add(Rectangle);
            }
        }

        public void UnExecute()
        {
            _container.Children.Remove(Rectangle);
        }
    }
}
