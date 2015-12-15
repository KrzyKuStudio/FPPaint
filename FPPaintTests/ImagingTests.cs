using NUnit.Framework;
using FPPaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FPPaintTests
{
    [TestFixture(), RequiresSTA()]
    public class ImagingTests
    {
        [Test()]
        public void BitmapSourse2Bitmap2BitmapSourceIsNotNullTest()
        {
            Canvas canvas = new Canvas();
            canvas.Width = 200;
            canvas.Height = 200;
            Rectangle rectangle = new Rectangle
            {
                Stroke = Brushes.Black,
                Margin = new Thickness(0, 0, 0, 0),
                Width = 200,
                Height = 200,
                Fill = Brushes.Black
            };
            canvas.Children.Add(rectangle);
            BitmapSource bitmapSource = Imaging.CreateBitmapSource(canvas);
            System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap((int)canvas.Width, (int)canvas.Height);
            bitmap = Imaging.BitmapFromSource(bitmapSource);
            var convertedBitmapSource = Imaging.ConvertBitmap(bitmap);

            Assert.IsNotNull(convertedBitmapSource);
            Assert.IsNotNull(bitmap);
            Assert.IsNotNull(bitmapSource);
        }

        [Test()]
        public void CreateBitmapSource_EqualSize_Test()
        {
            Canvas canvas = new Canvas();
            canvas.Width = 200;
            canvas.Height = 200;
            Rectangle rectangle = new Rectangle
            {
                Stroke = Brushes.Black,
                Margin = new Thickness(0, 0, 0, 0),
                Width = 200,
                Height = 200,
                Fill = Brushes.Black
            };
            canvas.Children.Add(rectangle);
            BitmapSource bitmapSource = Imaging.CreateBitmapSource(canvas);

            Assert.AreEqual(canvas.Width, bitmapSource.Width);
            Assert.AreEqual(canvas.Height, bitmapSource.Height);
        }
    }
}