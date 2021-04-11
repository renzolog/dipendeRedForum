using System;

namespace DipendeForum.Interfaces.CustomExceptions
{
    public class InvalidInputException : Exception
    {
        public InvalidInputException(string calledFor)
        {
            CalledFor = calledFor;
        }

        public string CalledFor { get; }

        public override string Message => $"{CalledFor} is not valid";
    }
}