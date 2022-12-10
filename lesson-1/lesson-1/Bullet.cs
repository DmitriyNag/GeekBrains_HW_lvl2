namespace lesson_1
{
    /// <summary>
    /// Класс для описания объекта пуля
    /// </summary>
    class Bullet : ActiveObj
    {
        /// <summary>
        /// Конструктор объекта пуля
        /// </summary>
        /// <param name="pos">Позиция пули</param>
        public Bullet(Point pos) : base(pos, new Point(10, 0), new Point(6,2))
        {
            OnLog($"Bullet fired at X: {Pos.X}, Y: {Pos.Y}");
        }
        /// <summary>
        /// Обновление состояния пули
        /// </summary>
        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
        }
        /// <summary>
        /// Отрисовка пули
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillRectangle(Brushes.OrangeRed,Pos.X,Pos.Y, Size.X, Size.Y);
        }
    }
}
