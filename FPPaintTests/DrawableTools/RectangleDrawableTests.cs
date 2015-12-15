using NUnit.Framework;
using FPPaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;

namespace FPPaint.Tests
{
    [TestFixture(),RequiresSTA()]
    public class RectangleDrawableTests
    {
        [Test()]
        public void RectangleDrawableTest_IsFilled()
        {
            Canvas canvas = new Canvas();
            Point point = new Point(10, 10);
            Brush color = new SolidColorBrush(Colors.Black);
            bool isFilled = true;
            RectangleDrawable rectangleDrawableFilled = new RectangleDrawable(point, color, canvas, 1, isFilled);
            RectangleDrawable rectangleDrawableNotFilled = new RectangleDrawable(point, color, canvas, 1, false);
            Assert.AreEqual(color, rectangleDrawableFilled.Rectangle.Fill);
            Assert.AreNotEqual(color, rectangleDrawableNotFilled.Rectangle.Fill);
        }
    }
}