using NUnit.Framework;
using FPPaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;

namespace FPPaint.Tests
{
    [TestFixture(),RequiresSTA()]
    public class EllipseDrawableTests
    {
        [Test()]
        public void EllipseDrawableTest_IsFilled()
        {
            Canvas canvas = new Canvas();
            Point point = new Point(10, 10);
            Brush color = new SolidColorBrush(Colors.Black);
            bool isFilled = true;
            EllipseDrawable ellipseDrawableFilled = new EllipseDrawable(point, color, canvas, 1, isFilled);
            EllipseDrawable ellipseDrawableNotFilled = new EllipseDrawable(point, color, canvas, 1, false);
            Assert.AreEqual(color, ellipseDrawableFilled.Circle.Fill);
            Assert.AreNotEqual(color, ellipseDrawableNotFilled.Circle.Fill);
        }
    }
}