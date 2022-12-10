namespace lesson_1
{
    /// <summary>
    /// Абстрактный класс объект фона, не взаимодействующий с другими объектами
    /// </summary>
    abstract class BackgroundObj : BaseObj
    {
        /// <summary>
        /// Конструктор объекта фона
        /// </summary>
        /// <param name="pos">Позиция объекта</param>
        /// <param name="dir">Направление движения объекта</param>
        /// <param name="size">Размер объекта</param>
        protected BackgroundObj(Point pos, Point dir, Point size) : base(pos, dir, size)
        {
        }
    }
}
