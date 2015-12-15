using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace FPPaint
{
    /// <summary>
    /// Elipse Drawing Tool Drawable Class
    /// </summary>
    public class EllipseDrawable : IDrawable, ICommand
    {
        private Canvas _container;
        private double tempX;
        private double tempY;
        public Ellipse Circle { get; private set; }

        public EllipseDrawable(Point coordinates, Brush color, Canvas container, int strokeThickness,bool isFilled)
        {

            _container = container;
            tempX = coordinates.X;
            tempY = coordinates.Y;
            if (isFilled == true)
            {
                Circle = new Ellipse
                {
                    Stroke = color,
                    StrokeThickness = strokeThickness,
                    Margin = new Thickness(coordinates.X, coordinates.Y, 0, 0),
                    Fill = color
                };
            }
            else
            {
                Circle = new Ellipse
                {
                    Stroke = color,
                    StrokeThickness = strokeThickness,
                    Margin = new Thickness(coordinates.X, coordinates.Y, 0, 0)
                };
            }
        }

        public void Draw(Point coordinates)
        {
            if (Circle != null)
            {
                double width = coordinates.X - tempX;
                double height = coordinates.Y - tempY;

                if (width < 0 && height < 0)
                {
                    Circle.Margin = new Thickness(coordinates.X, coordinates.Y, 0, 0);
                    Circle.Width = Math.Abs(width);
                    Circle.Height = Math.Abs(height);
                }
                else if (width < 0 && height > 0)
                {
                    Circle.Margin = new Thickness(coordinates.X, tempY, 0, 0);
                    Circle.Width = Math.Abs(width);
                    Circle.Height = Math.Abs(height);
                }
                else if (width > 0 && height < 0)
                {
                    Circle.Margin = new Thickness(tempX, coordinates.Y, 0, 0);
                    Circle.Width = Math.Abs(width);
                    Circle.Height = Math.Abs(height);
                }
                else
                {
                    Circle.Margin = new Thickness(tempX, tempY, 0, 0);
                    Circle.Width = Math.Abs(width);
                    Circle.Height = Math.Abs(height);
                }
            }
        }

        public void Execute()
        {
            if (!_container.Children.Contains(Circle))
            {
                _container.Children.Add(Circle);
            }
        }

        public void UnExecute()
        {
            _container.Children.Remove(Circle);
        }
    }

}
