using System;

namespace DipendeForum.Interfaces.CustomExceptions
{
    public class SearchedNotFoundException : Exception
    {
        public SearchedNotFoundException(string calledFor)
        {
            CalledFor = calledFor;
        }

        public string CalledFor { get; }

        public override string Message => $"{CalledFor} not found";
    }
}