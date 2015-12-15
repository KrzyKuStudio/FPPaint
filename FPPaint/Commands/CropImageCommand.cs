using FPPaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FPPaint.Commands
{
    /// <summary>
    /// Crop Image Command
    /// </summary>
    public class CropImageCommand : ICommand
    {
        private double _tempCanvasWidth;
        private double _tempCanvasHeight;
        private double _resizeToWidth;
        private double _resizeToHeight;
        private Image _image;

        public Image Image
        {
            get { return _image; }
            private set { _image = value; }
        }
        public Rectangle Rectangle { get; private set; }

        private Canvas _container;

        public CropImageCommand(Canvas container, double resizeToWidth, double resizeToHeight)
        {
            _container = container;
            Image = MakeTransformedImage(_container);
            Rectangle = new Rectangle
            {
                Fill = new SolidColorBrush(Colors.White),
                Stroke = new SolidColorBrush(Colors.White),
                Margin = new Thickness(0,0,0,0),
                Width = resizeToWidth,
                Height = resizeToHeight
            };

            _tempCanvasWidth = Image.Width;
            _tempCanvasHeight = Image.Height;
            _resizeToWidth = resizeToWidth;
            _resizeToHeight = resizeToHeight;
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

                _container.Children.Add(Rectangle);
                _container.Children.Add(Image);
                _container.Height = _resizeToHeight;
                _container.Width = _resizeToWidth;
                _container.Measure(new Size(_resizeToWidth, _resizeToHeight));
                _container.Arrange(new Rect(new Size(_resizeToWidth, _resizeToHeight)));

            }
        }

        public void UnExecute()
        {
            _container.Children.Remove(Image);
            _container.Children.Remove(Rectangle);
            _container.Width = _tempCanvasWidth;
            _container.Height = _tempCanvasHeight;
            _container.Measure(new Size(_tempCanvasWidth, _tempCanvasHeight));
            _container.Arrange(new Rect(new Size(_tempCanvasWidth, _tempCanvasHeight)));
        }
    }
}
