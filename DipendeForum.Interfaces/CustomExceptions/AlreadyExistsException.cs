using System;

namespace DipendeForum.Interfaces.CustomExceptions
{
    public class AlreadyExistsException : Exception
    {
        public AlreadyExistsException(string calledFor)
        {
            CalledFor = calledFor;
        }

        public string CalledFor { get; }

        public override string Message => $"{CalledFor} already exists";
    }
}