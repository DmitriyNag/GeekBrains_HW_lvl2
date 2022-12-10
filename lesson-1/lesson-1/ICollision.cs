namespace lesson_1
{
    /// <summary>
    /// Интерфейс реализует отслеживание столкновения\пересечения объектов игры друг с другом
    /// </summary>
    interface ICollision
    {
        bool Collision(ICollision obj);
        Rectangle rect { get; }
    }
}
