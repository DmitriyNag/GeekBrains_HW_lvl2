namespace lesson_1
{
    /// <summary>
    /// Абстрактный класс базовый объект
    /// </summary>
    abstract class BaseObj
    {
        /// <summary>
        /// Позиция объекта
        /// </summary>
        protected Point Pos;
        /// <summary>
        /// Направление движения объекта и скорость
        /// </summary>
        protected Point Dir;
        /// <summary>
        /// Размер Объекта
        /// </summary>
        protected Point Size;

        /// <summary>
        /// Конструктор Базового объекта
        /// </summary>
        /// <param name="pos">Позиция объекта</param>
        /// <param name="dir">Направление движения объекта и скорость</param>
        /// <param name="size">Размер объекта</param>
        /// <exception cref="GameObjectException">Ошибка создания объекта за пределами допустимых параметров(отрицательный размер)</exception>
        protected BaseObj(Point pos, Point dir, Point size) : this (pos, dir)
        {
            if((size.X < 0) || (size.Y < 0)) throw new GameObjectException("error creating game object, size can't be 0 or negative");
            Size = size;
        }
        /// <summary>
        /// Конструктор Базового объекта
        /// </summary>
        /// <param name="pos">Позиция объекта</param>
        /// <param name="dir">Направление движения объекта и скорость</param>
        /// <exception cref="GameObjectException">Ошибка создания объекта за пределами допустимых параметров(отрицательный размер, направление и скорость движения более 1000,позиция объекта менее -1000б более 5000</exception>
        protected BaseObj(Point pos, Point dir)
        {
            if ((dir.X > 1000) || (dir.Y > 1000)) throw new GameObjectException("error creating game object, direction can't be more than 100");
            if ((pos.X < -1000) || (pos.Y < -1000) || (pos.X > 5000) || (pos.Y > 5000)) throw new GameObjectException("error creating game object, position can't be less -1000 and more 5000");
            Pos = pos;
            Dir = dir;
            Size = new Point(0,0);
        }
        /// <summary>
        /// Абстрактный метод отрисовки объекта
        /// </summary>
        public abstract void Draw();
        /// <summary>
        /// Абстрактный метод обновления состояния объекта
        /// </summary>
        public abstract void Update();
    }
}
