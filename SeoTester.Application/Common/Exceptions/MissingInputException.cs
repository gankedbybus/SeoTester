using System;

namespace SeoTester.Application.Common.Exceptions
{
    public class MissingInputException : Exception
    {
        public MissingInputException(string missingField) : base($"{missingField} can't be empty")
        {

        }
    }
}
