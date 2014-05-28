using System;

namespace EM.ApplicationServices.Infrastructure
{
    public class EMApplicationException : Exception
    {
        public string Key { get; private set; }
        public EMApplicationException(string key, string message)
            : base(message)
        {
            Key = key;
        }
    }
}
