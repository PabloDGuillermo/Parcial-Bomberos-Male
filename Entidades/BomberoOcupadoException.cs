using System.Runtime.Serialization;

namespace Entidades
{
    [Serializable]
    public class BomberoOcupadoException : Exception
    {
        public BomberoOcupadoException()
        {
        }

        public BomberoOcupadoException(string? message) : base(message)
        {
        }

        public BomberoOcupadoException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected BomberoOcupadoException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}