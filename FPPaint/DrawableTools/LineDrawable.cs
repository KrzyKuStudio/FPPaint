using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;

namespace FPPaint
{
    /// <summary>
    /// Line Drawing Tool Drawable Class
    /// </summary>
    public class LineDrawable : IDrawable, ICommand
    {
        private Canvas _container;
        public Line Line { get; private set; }

        public LineDrawable(Point coordinates, Brush color, Canvas container, int strokeThickness)
        {
            _container = container;
            Line = new Line
            {
                Stroke = color,
                StrokeThickness = strokeThickness,
                X1 = coordinates.X,
                X2 = coordinates.X,
                Y1 = coordinates.Y,
                Y2 = coordinates.Y
            };
        }

        public void Draw(Point coordinates)
        {
            Line.X2 = coordinates.X;
            Line.Y2 = coordinates.Y;
        }
        public void Execute()
        {
            if (!_container.Children.Contains(Line))
            {
                _container.Children.Add(Line);
            }
        }

        public void UnExecute()
        {
            _container.Children.Remove(Line);
        }
    }
}
