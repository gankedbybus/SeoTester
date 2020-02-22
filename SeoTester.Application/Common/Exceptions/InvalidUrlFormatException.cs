using System;

namespace SeoTester.Application.Common.Exceptions
{
    public class InvalidUrlFormatException : Exception
    {
        public InvalidUrlFormatException(string invalidUrl) : base($"{invalidUrl} is not a valid url")
        {

        }
    }
}
