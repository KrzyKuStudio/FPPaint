using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.InteropServices;
using FPPaint;

namespace FPPaint
{
    /// <summary>
    /// MainWindow File Handling Partial Class
    /// </summary>
    public partial class MainWindow : Window
    {
        // Main storage file
        private ImageFile _imageFile;

        private void NewFile(object sender, RoutedEventArgs e)
        {
            if (_imageFile.NewFile())
            {
                _imageFile = new ImageFile();
                canvasDrawingSurface.Children.Clear();

                canvasDrawingSurface.Background = new SolidColorBrush(Colors.White);
                setDefaultZoom();
                UpdateMainLayout();
                SetUndoRedo();
            }
        }

        private void OpenFile(object sender, RoutedEventArgs e)
        {
            if (_imageFile.Open())
            {
                canvasDrawingSurface.Children.Clear();

                canvasDrawingSurface.Height = _imageFile.Canvas.Height;
                canvasDrawingSurface.Width = _imageFile.Canvas.Width;
                
                canvasDrawingSurface.Children.Add(_imageFile.Canvas);

                setDefaultZoom();
                UpdateMainLayout();
                SetUndoRedo();
            }
        }

        /// <summary>
        /// Menu Save File As
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAsFile(object sender, RoutedEventArgs e)
        {
            if (_imageFile.SaveAs(canvasDrawingSurface))
            {
                UpdateMainLayout();
            }
        }
    }
}
