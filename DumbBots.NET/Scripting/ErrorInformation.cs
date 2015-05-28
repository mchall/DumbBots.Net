using System;

namespace Scripting
{
    /// <summary>
    /// Interface which specifies which methods the user script must contain
    /// </summary>
    public class ErrorInformation
    {
        public string ErrorMessage { get; private set; }
        public int LineNumber { get; private set; }

        public ErrorInformation(string message, int lineNumber)
        {
            ErrorMessage = message;
            LineNumber = lineNumber;
        }

        public override string ToString()
        {
            return ErrorMessage;
        }
    }
}