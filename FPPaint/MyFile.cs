using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using System.Resources;
using System.Reflection;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Media;

namespace FPPaint
{
    /// <summary>
    /// Basis class for image
    /// </summary>
    public abstract class MyFile
    {
        private string _physicalLocation = "";
        private bool _isDirty;
        private string _dialogFilter = "All Files (*.*)|*.*";
        private object _content;

        public object Content
        {
            get { return _content; }
            set { _content = value; }
        }
        public string DialogFilter
        {
            set { _dialogFilter = value; }
        }
        public string PhysicalLocation
        {
            get { return _physicalLocation; }
            set { _physicalLocation = value; }
        }
        public bool IsDirty {
            get { return _isDirty; }
            set { _isDirty = value; }
        }

        public MyFile()
        {
            IsDirty = false;
        }

        public bool NewFile()
        {
            if (Close())
            {
                PhysicalLocation = "";
                return true;
            }
            return false;
        }

        public bool Open()
        {
            if (Close())
            {
                OpenFileDialog openDialog = new OpenFileDialog();
                openDialog.Filter = _dialogFilter;
                openDialog.Multiselect = false;
                openDialog.CheckFileExists = true;
                try
                {
                    if (openDialog.ShowDialog().Value == true)
                    {
                        _physicalLocation = openDialog.FileName;
                    }
                    else
                    {
                        IsDirty = true;
                        return false;
                    }

                    if (_physicalLocation.Length > 0)
                    {
                        LoadContent();
                    }
                    else
                    {
                        return false;
                    }
                    IsDirty = false;
                    return true;
                }
                catch (NotSupportedException exc)
                {
                    string appTitle = Constants.AppTitle;
                    string message = Constants.CantDecodeFile;
                    MessageBox.Show(message, appTitle);
                }
                catch (Exception exc)
                {
                    string appTitle = Constants.AppTitle;
                    string message = Constants.CantOpenFile;
                    MessageBox.Show(message + exc, appTitle);
                }
            }
            return false;
        }

        public abstract void LoadContent();

        public bool Close()
        {
            bool result = true;
            if (_isDirty)
            {
                string message = string.Format(Constants.closeMsg,
                    (_physicalLocation.Length > 0 ? _physicalLocation : "untitled"));

                switch (MessageBox.Show(message, Constants.AppTitle, MessageBoxButton.YesNoCancel, MessageBoxImage.Exclamation))
                {
                    case MessageBoxResult.Yes:
                        if ((_physicalLocation.Length > 0))
                            result = Save(Content);
                        else
                            result = SaveAs(Content);
                        break;
                    case MessageBoxResult.No:
                        // do nothing...
                        break;
                    case MessageBoxResult.Cancel:
                        result = false;
                        break;
                }

                if (result)
                {
                    _isDirty = false;
                }
            }
            return result;
        }

        public abstract bool Save(Object content);

        public virtual bool SaveAs(Object content)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Filter = _dialogFilter;

            if (saveDialog.ShowDialog().Value == true)
            {
                try
                {
                    _physicalLocation = saveDialog.FileName;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString());
                }

                return Save(content);
            }
            else return false;
        }
    }
}