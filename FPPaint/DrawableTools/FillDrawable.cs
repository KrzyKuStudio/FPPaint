using FPPaint;
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
    /// Fill Drawing Tool Drawable Class
    /// </summary>
    class FillDrawable :IDrawable, ICommand
    {
        private Canvas _container;
        public Image Image { get; private set; }
        private System.Drawing.Bitmap _bitmap;
        public FillDrawable(Point coordinates, Brush color, Canvas canvas)
        {
            _container = canvas;
            Transform tempTransform = canvas.LayoutTransform;
            canvas.LayoutTransform = null;
            Image = FloodFill(coordinates.X, coordinates.Y, canvas, color);
            canvas.LayoutTransform = tempTransform;
        }

        public void Draw(Point coordinates)
        {
        }

        private Image FloodFill(double x, double y, Canvas canvas, Brush color)
        {
            System.Drawing.Bitmap bm = _bitmap=  new System.Drawing.Bitmap((int)canvas.Width, (int)canvas.Height);
            bm = _bitmap = Imaging.BitmapFromSource(Imaging.CreateBitmapSource(canvas));
            
            Bitmap32 bm32 = new Bitmap32(bm); 
            
            bm32.LockBitmap();
            bm32.FloodFill((int)x, (int)y, WpfBrushToDrawingColor((SolidColorBrush)color));
            bm32.UnlockBitmap();

            var source = Imaging.ConvertBitmap(bm);
            ImageBrush ib = new ImageBrush();
            ib.ImageSource = source;

            Image MyBackground = new Image();
            MyBackground.Source = ib.ImageSource;

            return MyBackground;
        }

        private System.Drawing.Color WpfBrushToDrawingColor(System.Windows.Media.SolidColorBrush br)
        {
            return System.Drawing.Color.FromArgb(
                br.Color.A,
                br.Color.R,
                br.Color.G,
                br.Color.B);
        }

        public void Execute()
        {
            if (!_container.Children.Contains(Image))
            {
                _container.Children.Add(Image);
            }
        }

        public void UnExecute()
        {
            _container.Children.Remove(Image);
        }

    }
}
