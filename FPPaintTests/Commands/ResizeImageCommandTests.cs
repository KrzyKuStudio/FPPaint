using NUnit.Framework;
using FPPaint.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FPPaint.Commands.Tests
{
    [TestFixture(), RequiresSTA()]
    public class ResizeImageCommandTests
    {
        [Test()]
        public void ExecuteTest_ProperWidthResizedCanvas()
        {
            Canvas container = new Canvas();
            container.Width = 100;
            container.Height = 200;

            ICommand resizeCommand = new ResizeImageCommand(container, 200, 400);
            resizeCommand.Execute();
            
            Assert.AreEqual(200, container.Width);
        }
        [Test()]
        public void ExecuteTest_ProperHeightResizedCanvas()
        {
            Canvas container = new Canvas();
            container.Width = 100;
            container.Height = 200;

            ICommand resizeCommand = new ResizeImageCommand(container, 200, 400);
            resizeCommand.Execute();

            Assert.AreEqual(400, container.Height);
        }
        [Test()]
        public void UnExecuteTest_ProperWidthResizedCanvas()
        {
            Canvas container = new Canvas();
            container.Width = 150;
            container.Height = 250;

            ICommand resizeCommand = new ResizeImageCommand(container, 200, 400);
            resizeCommand.Execute();
            resizeCommand.UnExecute();

            Assert.AreEqual(150, container.Width);
        }
        [Test()]
        public void UnExecuteTest_ProperHeightResizedCanvas()
        {
            Canvas container = new Canvas();
            container.Width = 150;
            container.Height = 250;

            ICommand resizeCommand = new ResizeImageCommand(container, 200, 400);
            resizeCommand.Execute();
            resizeCommand.UnExecute();

            Assert.AreEqual(250, container.Height);
        }
    }
}