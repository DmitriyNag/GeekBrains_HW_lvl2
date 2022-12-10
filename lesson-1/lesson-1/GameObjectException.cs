using System.Runtime.Serialization;

namespace lesson_1
{
    /// <summary>
    /// Исключение при создании объекта игры с некорректными параметрами
    /// </summary>
    [Serializable]
    internal class GameObjectException : Exception
    {
        public GameObjectException()
        {
        }

        public GameObjectException(string? message) : base(message)
        {
        }

        public GameObjectException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected GameObjectException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}