using System;

namespace DipendeForum.Interfaces.CustomExceptions
{
    public class ObjectIsNullException : Exception
    {
        public ObjectIsNullException(string calledFor)
        {
            CalledFor = calledFor;
        }

        public string CalledFor { get; }

        public override string Message => $"{CalledFor} already exists";
    }
}