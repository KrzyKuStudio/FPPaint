using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPPaint
{
    /// <summary>
    /// ICommand Interface
    /// </summary>
    public interface ICommand
    {
        void Execute();
        void UnExecute();
    }
}
