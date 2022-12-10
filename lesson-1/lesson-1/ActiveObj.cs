namespace lesson_1
{
    /// <summary>
    /// Абстратный класс  - объект который может взаимодействовать с окружением
    /// </summary>
    abstract class ActiveObj : BaseObj, ICollision
    {
        /// <summary>
        /// Фиксирует события с Активным объектом
        /// </summary>
        public static event Action<string>? log; 
        /// <summary>
        /// Абстрактный конструктор Активного объекта
        /// </summary>
        /// <param name="pos">Позиция объекта</param>
        /// <param name="dir">Направление движения объекта и скорость</param>
        /// <param name="size">Размер объекта</param>
        protected ActiveObj(Point pos, Point dir, Point size) : base(pos, dir, size)
        {
        }
        /// <summary>
        /// Абстрактный конструктор Активного объекта
        /// </summary>
        /// <param name="pos">Позиция объекта</param>
        /// <param name="dir">Направление движения объекта и скорость</param>
        protected ActiveObj(Point pos, Point dir) : base(pos, dir)
        {
        }
        /// <summary>
        /// Прямоугольник с координатами, в который вписан объект
        /// </summary>
        public Rectangle rect => new Rectangle(Pos, (Size)Size);
        /// <summary>
        /// Метод возвращает размер объекта
        /// </summary>
        public Point XY => Size;
        /// <summary>
        /// Реализация интерфейса ICollision, проверка пересечения объекта с объектом obj
        /// </summary>
        /// <param name="obj">Объект</param>
        /// <returns>bool есть ли пересечение с объектом obj</returns>
        public bool Collision(ICollision obj)
        {
            if(obj.rect.IntersectsWith(this.rect))
            {
                OnLog($"Collision {obj.GetType()} and {this.GetType()} detected at X: {this.Pos.X} Y: {this.Pos.Y}");
                return true;
            }
            return false;
        }
        /// <summary>
        /// Метод вызывает Action log 
        /// </summary>
        /// <param name="s">Строка которая пердается в Action</param>
        protected virtual void OnLog(string s) => log?.Invoke(s);
    }
}
