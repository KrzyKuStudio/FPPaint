using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace FPPaint
{
    /// <summary>
    /// My Image class responsible for opening/saving files 
    /// </summary>
    public class ImageFile : MyFile
    {
        private Canvas _canvas;
        private string _supportedFormats = "Bitmap Image(*.BMP)|*.bmp|JPeg Image(*.JPG)|*.jpg|Png Image(*.PNG)|*.png|Gif Image(*.GIF)|*.gif";

        public Canvas Canvas
        {
            get { return _canvas; }
            set { _canvas = value; }
        }
        public ImageFile() : base()
        {
            base.DialogFilter = _supportedFormats;
            _canvas = new Canvas();
        }

        /// <summary>
        /// Load image data from File
        /// </summary>
        /// 
        public override void LoadContent()
        {
            string appTitle = Constants.AppTitle;
            try
            {
                ImageBrush brush = new ImageBrush();
                MemoryStream ms = new MemoryStream();
                BitmapImage bi = new BitmapImage();
                byte[] bytArray = File.ReadAllBytes(PhysicalLocation);
                ms.Write(bytArray, 0, bytArray.Length); ms.Position = 0;
                bi.BeginInit();
                bi.StreamSource = ms;
                bi.EndInit();
                brush.ImageSource = bi;

                _canvas.Children.Clear();
                _canvas.Width = brush.ImageSource.Width;
                _canvas.Height = brush.ImageSource.Height;
                _canvas.Background = brush;

                Image MyBackground = new Image();
                MyBackground.Source = brush.ImageSource;
                _canvas.Children.Add(MyBackground);

                return;
            }
            catch (System.ArgumentException ae)
            {
                string message = Constants.PathSyntaxError;
                MessageBox.Show(message, appTitle);
            }
            catch (DirectoryNotFoundException dnfe)
            {
                string message = Constants.PathNotFoundError;
                MessageBox.Show(message, appTitle);
            }
            catch (DriveNotFoundException drivenotfoundexception)
            {
                string message = Constants.PathNotFoundError;
                MessageBox.Show(message, appTitle);
            }
            catch (OutOfMemoryException oome)
            {
                MessageBox.Show("File too large for " + appTitle + " to open");
                return;
            }

            PhysicalLocation = "";
        }

        /// <summary>
        /// Save canvas into instance canvas
        /// </summary>
        /// <param name="canvas"></param>
        public override bool Save(Object canvas)
        {
            if (PhysicalLocation.Length > 0)
            {
                try
                {
                    SaveContent(canvas);
                    SaveRenderTargetBitmap(CreateRenderBitmap(Canvas), PhysicalLocation);
                    IsDirty = false;
                    return true;
                }
                //This catch handles the exception raised when trying to save a read only file
                catch (System.UnauthorizedAccessException e)
                {
                    string message = string.Format(Constants.UnauthorizedAccessWarningMessage,
                                                  PhysicalLocation);
                    MessageBox.Show(message, Constants.AppTitle, MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    SaveAs(canvas);
                }
                catch (Exception e)
                {
                    MessageBox.Show(Constants.saveException + " " + e.Message, Constants.AppTitle);
                }
            }
            else
            {
                return SaveAs(canvas);
            }
            return false;
        }

        public override bool SaveAs(Object canvas)
        {
            Microsoft.Win32.SaveFileDialog saveDialog = new Microsoft.Win32.SaveFileDialog();
            saveDialog.Filter = _supportedFormats;

            if (saveDialog.ShowDialog().Value == true)
            {
                try
                {
                    PhysicalLocation = saveDialog.FileName;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }
                return Save(canvas);
            }
            else return false;
        }

        private void SaveContent(Object canvas)
        {
            Canvas tempCanvas = new Canvas();
            tempCanvas = (Canvas)canvas;
            this._canvas = new Canvas();

            ImageBrush brush = new ImageBrush();
            brush.ImageSource = CreateRenderBitmap(tempCanvas);
            this._canvas.Width = brush.ImageSource.Width;
            this._canvas.Height = brush.ImageSource.Height;

            this._canvas.Background = brush;

            Image MyBackground = new Image();
            MyBackground.Source = brush.ImageSource;
            this._canvas.Children.Add(MyBackground);
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
            canvas.Measure(new Size(width ,height));
            canvas.Arrange(new Rect(new Size(width, height)));
            renderBitmap.Render(canvas);

            return renderBitmap;
        }
        
        /// <summary>
        /// Save RenerTargetBitmap to file using encoders
        /// </summary>
        /// <param name="renderBitmap"></param>
        /// <param name="filename"></param>
        private void SaveRenderTargetBitmap(RenderTargetBitmap renderBitmap, string filename)
        {
            BitmapEncoder encoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            string extension = filename.Substring(filename.LastIndexOf('.'));
            switch (extension.ToLower())
            {
                case ".jpg":
                    encoder = new JpegBitmapEncoder();
                    break;
                case ".bmp":
                    encoder = new BmpBitmapEncoder();
                    break;
                case ".gif":
                    encoder = new GifBitmapEncoder();
                    break;
                case ".png":
                    encoder = new PngBitmapEncoder();
                    break;
            }
            // push the rendered bitmap to it
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
            // Create a file stream for saving image
            using (FileStream fs = File.Open(filename, FileMode.OpenOrCreate))
            {
                encoder.Save(fs);
            }
        }
    }
}
