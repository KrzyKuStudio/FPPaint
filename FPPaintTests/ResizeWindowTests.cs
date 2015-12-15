using NUnit.Framework;
using FPPaint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FPPaintTests
{
    [TestFixture(), RequiresSTA()]
    public class ResizeWindowTests
    {
        [Test()]
        public void InputArgumentsToFieldsTest()
        {
            ResizeImageWindow resizeWindow = new ResizeImageWindow(300, 250, "Resize Image");

            Assert.AreEqual(300, resizeWindow.WidthGet);
            Assert.AreEqual(250, resizeWindow.HeightGet);
            
        }

        [Test()]
        public void WidthGetSetTest()
        {
            ResizeImageWindow resizeWindow = new ResizeImageWindow(300, 250, "Resize Image");
            Assert.AreEqual(300, resizeWindow.WidthGetSet);
            resizeWindow.WidthGetSet = 350;
            
            Assert.AreEqual(350, resizeWindow.WidthGetSet);
        }

        [Test()]
        public void HeightGetSetTest()
        {
            ResizeImageWindow resizeWindow = new ResizeImageWindow(300, 250, "Crop Image");
            Assert.AreEqual(250, resizeWindow.HeightGetSet);
            resizeWindow.HeightGetSet = 260;

            Assert.AreEqual(260, resizeWindow.HeightGetSet);
        }
    }
}
