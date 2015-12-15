using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FPPaint
{
    /// <summary>
    /// MainWindow History Action Partial Class
    /// </summary>
    public partial class MainWindow : Window
    {
        private UnDoRedo _unDoObject;
        public UnDoRedo UnDoObject
        {
            get { return _unDoObject; }
            set
            {
                _unDoObject = value;
            }
        }

        /// <summary>
        /// Undo Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            UnDoObject.Undo(1);
        }

        /// <summary>
        /// Redo Action
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// </summary>
        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            UnDoObject.Redo(1);
        }

        private void SetUndoRedo()
        {
            UnDoObject = null;
            UnDoObject = new UnDoRedo();
            UnDoObject.Container = canvasDrawingSurface;
            UnDoObject.EnableDisableUndoRedoFeature += new EventHandler(UnDoObject_EnableDisableUndoRedoFeature);
        }

        private void UnDoObject_EnableDisableUndoRedoFeature(object sender, EventArgs e)
        {
            if (UnDoObject.IsUndoPossible())
            {
                undoBtn.IsEnabled = true;
            }
            else
            {
                undoBtn.IsEnabled = false;

            }

            if (UnDoObject.IsRedoPossible())
            {
                redoBtn.IsEnabled = true;
            }
            else
            {
                redoBtn.IsEnabled = false;
            }

        }
    }
}
