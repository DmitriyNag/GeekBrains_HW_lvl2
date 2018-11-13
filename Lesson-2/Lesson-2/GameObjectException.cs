namespace MyGame
{
    class GameObjectException : System.Exception
    {
        public GameObjectException(string message, params string[] args) : base(message)
        {

        }
    }

}
