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
using System.ComponentModel;

// TODO - kolejnym pokręconym obszarem jest obsługa plików: jest interfejs IMyFile, implementowany przez klasę MyFile, a po tej klasie dziedziczy jeszcze klasa MyImage i ciężko w ogóle wymyślić co jest odpowiedzialnością której klasy. Wygląda to trochę tak, jakby Autor chciał się pochwalić znajomością słów "abstract" i "virtual", nie zastanawiając się nad tym czy ten kod ma sens.
// TODO radio butons better syle

namespace FPPaint
{
    /// <summary>
    /// MainWindow Maind Class thats starts application
    /// Divided in four parial parts
    /// Fields and Main Logic, File Handling, Button Events, Action History
    /// </summary>
    public partial class MainWindow : Window
    {
        //Left Mouse Button Flag
        private bool _isMouseDown;
        //user color
        private Brush _brushColor;
        private int _strokeThickness;
        // selected drawing tool
        private DrawableToolsEnum _selectedShape;
        //Drawing tools objects interface and class
        private IDrawable _drawable;
        //temporarty point location for pencil
        private Point _currentTemporaryPoint = new Point();

        // Binded Commands for buttons
        public static readonly RoutedCommand NewFileCommand = new RoutedCommand();
        public static readonly RoutedCommand OpenFileCommand = new RoutedCommand();
        public static readonly RoutedCommand SaveFileAsCommand = new RoutedCommand();
        public static readonly RoutedCommand UndoCommand = new RoutedCommand();
        public static readonly RoutedCommand RedoCommand = new RoutedCommand();
        // Binded Commands to radio buttons
        public static readonly RoutedCommand SelectedPencil = new RoutedCommand();
        public static readonly RoutedCommand SelectedLine = new RoutedCommand();
        public static readonly RoutedCommand SelectedEllipse = new RoutedCommand();
        public static readonly RoutedCommand SelectedRectangle = new RoutedCommand();
        public static readonly RoutedCommand SelectedErasor = new RoutedCommand();
        public static readonly RoutedCommand SelectedFill = new RoutedCommand();
        public static readonly RoutedCommand SelectedEllipseFilled = new RoutedCommand();
        public static readonly RoutedCommand SelectedRectangleFilled = new RoutedCommand();

        public MainWindow()
        {
            InitializeComponent();
            _imageFile = new ImageFile();
            SetUndoRedo();

            NewFile(null, null);
            _brushColor = new SolidColorBrush(Colors.Black);
            _strokeThickness = 2;
            _selectedShape = DrawableToolsEnum.Pencil;
            UpdateMainLayout();
        }

        private void ExitApp(object sender, RoutedEventArgs e)
        {
            //before exit check if file need to be saved
            if (_imageFile.Close())
                this.Close();
        }

        private void canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;

            if (_selectedShape == DrawableToolsEnum.Pencil)
            {
                _drawable = new PencilDrawable(Mouse.GetPosition(canvasDrawingSurface), _brushColor, canvasDrawingSurface, _strokeThickness);
            }
            else if (_selectedShape == DrawableToolsEnum.Line)
            {
                _drawable = new LineDrawable(Mouse.GetPosition(canvasDrawingSurface), _brushColor, canvasDrawingSurface, _strokeThickness);
            }
            else if (_selectedShape == DrawableToolsEnum.Ellipse)
            {
                _drawable = new EllipseDrawable(Mouse.GetPosition(canvasDrawingSurface), _brushColor, canvasDrawingSurface,_strokeThickness, false);
            }
            else if (_selectedShape == DrawableToolsEnum.Rectangle)
            {
                _drawable = new RectangleDrawable(Mouse.GetPosition(canvasDrawingSurface), _brushColor, canvasDrawingSurface, _strokeThickness,false);
            }
            else if (_selectedShape == DrawableToolsEnum.Fill)
            {
                _drawable = new FillDrawable(Mouse.GetPosition(canvasDrawingSurface), _brushColor, canvasDrawingSurface);
            }
            else if (_selectedShape == DrawableToolsEnum.Erasor)
            {
                _drawable = new ErasorDrawable(Mouse.GetPosition(canvasDrawingSurface), canvasDrawingSurface, _strokeThickness);
            }
            else if (_selectedShape == DrawableToolsEnum.EllipseFilled)
            {
                _drawable = new EllipseDrawable(Mouse.GetPosition(canvasDrawingSurface), _brushColor, canvasDrawingSurface, _strokeThickness, true);
            }
            else if (_selectedShape == DrawableToolsEnum.RectangleFilled)
            {
                _drawable = new RectangleDrawable(Mouse.GetPosition(canvasDrawingSurface), _brushColor, canvasDrawingSurface, _strokeThickness, true);
            }

            if (_drawable != null)
            {
                UnDoObject.InsertInUnDoRedo(_drawable as ICommand);
                (_drawable as ICommand).Execute();
                _imageFile.IsDirty = true;
            }
        }

        private void canvas_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = false;
            _drawable = null;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            statusBarTextCurrentPxUpdate(e);
            if (_isMouseDown && _drawable != null)
            {
                _drawable.Draw(Mouse.GetPosition(canvasDrawingSurface));
            }
        }

        private void canvas_MouseLeave(object sender, MouseEventArgs e)
        {
            statusBarTextCurrentPxUpdate(null);
        }

        private void CommonCommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBindingShape_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == SelectedPencil)
            {
                _selectedShape = DrawableToolsEnum.Pencil;
            }
            else if (e.Command == SelectedLine)
            {
                _selectedShape = DrawableToolsEnum.Line;
            }
            else if (e.Command == SelectedEllipse)
            {
                _selectedShape = DrawableToolsEnum.Ellipse;
            }
            else if (e.Command == SelectedRectangle)
            {
                _selectedShape = DrawableToolsEnum.Rectangle;
            }
            else if (e.Command == SelectedErasor)
            {
                _selectedShape = DrawableToolsEnum.Erasor;
            }
            else if (e.Command == SelectedFill)
            {
                _selectedShape = DrawableToolsEnum.Fill;
            }
            else if (e.Command == SelectedEllipseFilled)
            {
                _selectedShape = DrawableToolsEnum.EllipseFilled;
            }
            else if (e.Command == SelectedRectangleFilled)
            {
                _selectedShape = DrawableToolsEnum.RectangleFilled;
            }
        }

        private void UpdateMainLayout()
        {
            btnBrushColor.Background = _brushColor;
            mainWindow.Title = Constants.AppTitle + " " + _imageFile.PhysicalLocation;
        }

        private void statusBarTextCurrentPxUpdate(MouseEventArgs e)
        {
            if (e != null)
                statusBarTextCurrentPx.Text = "X,Y: " + (int)(Mouse.GetPosition(canvasDrawingSurface).X) + "," + (int)Mouse.GetPosition(canvasDrawingSurface).Y;
            else
                statusBarTextCurrentPx.Text = "X,Y: ";
        }

        private void canvasDrawingSurface_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            statusBarText_CanvasSize.Text = "Size: " + (int)e.NewSize.Width + " x " + (int)e.NewSize.Height;
        }

        private void sliderBrushThicknessValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            _strokeThickness = (int)e.NewValue;
        }

        private void sliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            labelZoom.Content = ((int)e.NewValue).ToString() + "%";
            double scale = 1 * ( e.NewValue / 100);
            gridParentCanvasDrawingSurface.LayoutTransform = new ScaleTransform(scale,scale);
        }

        private void setDefaultZoom()
        {
            sliderZoom.Value = 100;
        }

    }
}



