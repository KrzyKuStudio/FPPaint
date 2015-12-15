using FPPaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FPPaint.Commands
{
    /// <summary>
    /// Rotate Clockwise 90 Degrees Command
    /// </summary>
    public class RotateCw90Command : ICommand
    {
        private double _tempCanvasWidth;
        private double _tempCanvasHeight;
        private Image _image;

        public Image Image
        {
            get { return _image; }
            private set { _image = value; }
        }

        private Canvas _container;

        public RotateCw90Command(Canvas container)
        {
            _container = container;
            Image = MakeTransformedImage(_container);
            _tempCanvasWidth = Image.Width;
            _tempCanvasHeight = Image.Height;
        }

        private Image MakeTransformedImage(Object canvas)
        {
            ImageBrush brush = new ImageBrush();
            brush.ImageSource = CreateRenderBitmap(canvas as Canvas);

            Image MyBackground = new Image();
            MyBackground.Source = brush.ImageSource;
            MyBackground.Height = brush.ImageSource.Height;
            MyBackground.Width = brush.ImageSource.Width;

            return MyBackground;
        }

        /// <summary>
        /// Create From Canvas RenderTargetBitamp
        /// </summary>
        /// <param name="canvas"></param>
        /// <returns></returns>
        private RenderTargetBitmap CreateRenderBitmap(Canvas canvas)
        {
            //If image if angled before saving swap dimensions to prevent black areas
            double width, height;
            width = canvas.Width;
            height = canvas.Height;

            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
             (int)width, (int)height,
             96d, 96d, PixelFormats.Pbgra32);

            // needed otherwise the image output is black
            canvas.Measure(new Size(width, height));
            canvas.Arrange(new Rect(new Size(width, height)));
            renderBitmap.Render(canvas);

            return renderBitmap;
        }

        public void Execute()
        {
            if (!_container.Children.Contains(Image))
            {
                Image.LayoutTransform = new RotateTransform(90);
                _container.Children.Add(Image);
                _container.Height = _tempCanvasWidth;
                _container.Width = _tempCanvasHeight;
                _container.Measure(new Size(_tempCanvasHeight, _tempCanvasWidth));
                _container.Arrange(new Rect(new Size(_tempCanvasHeight, _tempCanvasWidth)));

            }
        }

        public void UnExecute()
        {
            _container.Children.Remove(Image);
            _container.Width = _tempCanvasWidth;
            _container.Height = _tempCanvasHeight;
            _container.Measure(new Size(_tempCanvasWidth, _tempCanvasHeight));
            _container.Arrange(new Rect(new Size(_tempCanvasWidth, _tempCanvasHeight)));
        }
    }
}
