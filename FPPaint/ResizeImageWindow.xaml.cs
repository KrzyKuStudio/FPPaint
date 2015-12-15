using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Globalization;

namespace FPPaint
{
    /// <summary>
    /// Interaction logic for ResizeWindow.xaml
    /// </summary>
    public partial class ResizeImageWindow : Window, INotifyPropertyChanged, IDataErrorInfo
    {

        private int _width;
        private int _height;
        private double _ratio;

        public event PropertyChangedEventHandler PropertyChanged;
        public static readonly DependencyProperty IsValidProperty = DependencyProperty.Register("IsValid", typeof(String), typeof(ResizeImageWindow));
        private string IsValid
        {
            set
            {
                if (value == "True")
                {
                    btnResizeOk.IsEnabled = true;
                }
                else
                {
                    btnResizeOk.IsEnabled = false;
                }
                OnPropertyChanged(new PropertyChangedEventArgs("IsValid"));
            }
        }
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        public double WidthGet
        {
            get
            {
                return _width;
            }
        }
        public double HeightGet
        {
            get
            {
                return _height;
            }
        }

        public int WidthGetSet
        {
            get { return _width; }
            set
            {
                if (checkBoxKeepAspectRatio.IsChecked == true && !textBoxHeight.IsFocused)
                {
                    HeightGetSet = (int)(value / _ratio);
                }
                _width = value;
                OnPropertyChanged(new PropertyChangedEventArgs("WidthGetSet"));
            }
        }
        public int HeightGetSet
        {
            get { return _height; }
            set
            {
                if (checkBoxKeepAspectRatio.IsChecked == true && !textBoxWidth.IsFocused )
                {
                    WidthGetSet = (int)(value * _ratio);
                }
                _height = value;
                OnPropertyChanged(new PropertyChangedEventArgs("HeightGetSet"));
            }
        }

        public string Error
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                string result = null;
                IsValid = "True";
                if(columnName == "WidthGetSet")
                {
                        int value;
                        try
                        {
                            value = Convert.ToInt32(WidthGetSet);
                            if (value == 0)
                            {
                                IsValid = "False";
                                result = "Number can't be 0";
                            }
                        }
                        catch (FormatException)
                        {
                            IsValid = "False";
                            result = "Please enter only numbers";
                        }

                }
                else if (columnName == "HeightGetSet")
                {
                        int value;
                        try
                        {
                            value = Convert.ToInt32(HeightGetSet);
                            if (value == 0)
                            {
                                IsValid = "False";
                                result = "Number can't be 0";
                            }
                        }
                        catch (FormatException)
                        {
                            IsValid = "False";
                            result = "Please enter only numbers";
                    }
                }
                return result;
            }
        }

        public ResizeImageWindow(double width, double height, string title)
        {
            InitializeComponent();
            _this.Title = title;
            this.ResizeMode = ResizeMode.NoResize;
            _ratio = width / height;
            WidthGetSet = (int)width;
            HeightGetSet = (int)height;
        }

        private void btnResizeOk_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void btnResizeCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
       
        private void EnableOnlyNumbersInTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
