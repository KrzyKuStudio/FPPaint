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
    /// Flip Vertically Command
    /// </summary>
    public class FlipVerticallyCommand : ICommand
    {
        public Image Image { get; private set; }
        private Canvas _container;

        public FlipVerticallyCommand(Canvas container)
        {
            _container = container;
            Image = MakeTransformedImage(container);
        }

        private Image MakeTransformedImage(Object canvas)
        {
            Canvas tempCanvas = new Canvas();
            tempCanvas = (Canvas)canvas;

            System.Windows.Media.ImageBrush brush = new ImageBrush();
            brush.ImageSource = CreateRenderBitmap(tempCanvas);

            Image MyBackground = new Image();
            MyBackground.Source = brush.ImageSource;
            MyBackground.LayoutTransform = new ScaleTransform(1, -1);
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
                _container.Children.Add(Image);
            }
        }

        public void UnExecute()
        {
            _container.Children.Remove(Image);
        }
    }
}
