using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPPaint
{
    /// <summary>
    /// Common for app strings 
    /// </summary>
    public struct Constants
    {
        public const String UnauthorizedAccessWarningMessage = "Cannot create the {0}\r\nMake sure the path and filename are correct";
        public const String CantOpenFile = "Can't open the file";
        public const String CantDecodeFile = "Can't decode the file";
        public const String AppTitle = "FPPaint";
        public const String closeMsg = "{0} file has changed.\r\n\r\nDo you want to save the changes?";
        public const String Untitled = "untitled";

        public const String PathSyntaxError = "The filename, directory name, or volume label syntax is incorrect.";
        public const String PathNotFoundError = "The system cannot find the path specified.";
        public const String FileNotFoundWarning = "Cannot find the {0} file. \n\n Do you want to create it?";
        public const String saveException = "Save Exception";
    }
}

