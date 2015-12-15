using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace FPPaint
{
    /// <summary>
    /// Undo Redo Class Using Command Pattern
    /// </summary>
    #region UndoRedo
    public class UnDoRedo
    {

        private Stack<ICommand> _Undocommands = new Stack<ICommand>();
        private Stack<ICommand> _Redocommands = new Stack<ICommand>();

        public event EventHandler EnableDisableUndoRedoFeature;


        private Canvas _Container;

        public Canvas Container
        {
            get { return _Container; }
            set { _Container = value; }
        }

        public void Redo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (_Redocommands.Count != 0)
                {
                    ICommand command = _Redocommands.Pop();
                    command.Execute();
                    _Undocommands.Push(command);
                }

            }
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }

        public void Undo(int levels)
        {
            for (int i = 1; i <= levels; i++)
            {
                if (_Undocommands.Count != 0)
                {
                    ICommand command = _Undocommands.Pop();
                    command.UnExecute();
                    _Redocommands.Push(command);
                }

            }
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }

        #region UndoHelperFunctions

        public void InsertInUnDoRedo(ICommand command)
        {
            _Undocommands.Push(command); _Redocommands.Clear();
            if (EnableDisableUndoRedoFeature != null)
            {
                EnableDisableUndoRedoFeature(null, null);
            }
        }

        #endregion

        public  bool IsUndoPossible()
        {
            if (_Undocommands.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }             
        }

        public bool IsRedoPossible()
        {

            if (_Redocommands.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }    
        }
    }
    #endregion
}
