using System;
using System.Runtime.Serialization;

namespace GetMusic
{
    [Serializable]
    internal class YouTubeClientException : Exception
    {
        public YouTubeClientException()
        {
        }

        public YouTubeClientException(string message) : base(message)
        {
        }

        public YouTubeClientException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected YouTubeClientException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}