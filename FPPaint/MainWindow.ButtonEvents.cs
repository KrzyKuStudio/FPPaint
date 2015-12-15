using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace FPPaint
{
	/// <summary>
    /// MainWindow Action Butons Partial Class
    /// </summary>
    public partial class MainWindow : Window
    {
        private void btnBrushColor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _brushColor = ColorDialog.PromptQestion();
                BrushColorLabelColorUpdate(_brushColor);
                UpdateMainLayout();
            }
            catch (Exception exc)
            {
                // Color not changed. Nothing to do
            }
        }

        private void BrushColorLabelColorUpdate(Brush brushColor)
        {
            String color = brushColor.ToString();
            int colorR = int.Parse(color.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            int colorG = int.Parse(color.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            int colorB = int.Parse(color.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);
            int multipliedColors = colorR * colorG * colorB;
            int halfMaxMultipleColorValue = 8000000;
            if (multipliedColors > halfMaxMultipleColorValue)
            {
                btnBrushColor.Foreground = new SolidColorBrush(Colors.Black);
            }
            else
            {
                btnBrushColor.Foreground = new SolidColorBrush(Colors.White);
            }
        }

        private void btnRotateCw90_Click(object sender, RoutedEventArgs e)
        {
            ICommand command = new Commands.RotateCw90Command(canvasDrawingSurface);
            UnDoObject.InsertInUnDoRedo(command);
            command.Execute();
            _imageFile.IsDirty = true;
        }

        private void btnRotateCcw90_Click(object sender, RoutedEventArgs e)
        {
            ICommand command = new Commands.RotateCcw90Command(canvasDrawingSurface);
            UnDoObject.InsertInUnDoRedo(command);
            command.Execute();
            _imageFile.IsDirty = true;
        }

        private void btnRotate180_Click(object sender, RoutedEventArgs e)
        {
            ICommand command = new Commands.Rotate180Command(canvasDrawingSurface);
            UnDoObject.InsertInUnDoRedo(command);
            command.Execute();
            _imageFile.IsDirty = true;
        }

        private void btnFlipHorizontally_Click(object sender, RoutedEventArgs e)
        {
            ICommand command = new Commands.FlipHorizontallyCommand(canvasDrawingSurface);
            UnDoObject.InsertInUnDoRedo(command);
            command.Execute();
            _imageFile.IsDirty = true;
        }

        private void btnFlipVertically_Click(object sender, RoutedEventArgs e)
        {
            ICommand command = new Commands.FlipVerticallyCommand(canvasDrawingSurface);
            UnDoObject.InsertInUnDoRedo(command);
            command.Execute();
            _imageFile.IsDirty = true;
        }

        private void btnResizeWindow_Click(object sender, RoutedEventArgs e)
        {
            ResizeImageWindow resizeWindow = new ResizeImageWindow(canvasDrawingSurface.Width, canvasDrawingSurface.Height, "Resize Image");
            if (resizeWindow.ShowDialog().Value)
            {
                ICommand command = new Commands.ResizeImageCommand(canvasDrawingSurface,resizeWindow.WidthGet, resizeWindow.HeightGet);
                UnDoObject.InsertInUnDoRedo(command);
                command.Execute();
                _imageFile.IsDirty = true;
            };
        }

        private void btnCropWindow_Click(object sender, RoutedEventArgs e)
        {
            ResizeImageWindow resizeWindow = new ResizeImageWindow(canvasDrawingSurface.Width, canvasDrawingSurface.Height, "Crop Image");
            if (resizeWindow.ShowDialog().Value)
            {
                ICommand command = new Commands.CropImageCommand(canvasDrawingSurface, resizeWindow.WidthGet, resizeWindow.HeightGet);
                UnDoObject.InsertInUnDoRedo(command);
                command.Execute();
                _imageFile.IsDirty = true;
            };
        }
    }   
}
