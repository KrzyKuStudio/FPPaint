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
    /// Erasor Drawing Tool Drawable Class
    /// </summary>
    public class ErasorDrawable : IDrawable, ICommand
    {
        private Canvas _container;
        private PointCollection _pointsCollection;

        public Polyline Polyline { get; private set; }

        public ErasorDrawable(Point coordinates, Canvas container, int strokeThickness)
        {
            _container = container;

            _pointsCollection = new PointCollection();
            _pointsCollection.Add(coordinates);
            Polyline = new Polyline
            {
                Stroke = new SolidColorBrush(Colors.White),
                StrokeThickness = strokeThickness,
                Points = _pointsCollection
            };
        }

        public void Draw(Point coordinates)
        {
            _pointsCollection.Add(coordinates);
        }
        public void Execute()
        {
            if (!_container.Children.Contains(Polyline))
            {
                _container.Children.Add(Polyline);
            }
        }
        public void UnExecute()
        {
            _container.Children.Remove(Polyline);
        }
    }
}
