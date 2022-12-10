namespace lesson_1
{
    /// <summary>
    /// Класс движущаяся звезда
    /// </summary>
    internal class Star: BackgroundObj
    {
        /// <summary>
        /// Конструктор объекта движущаяся звезда
        /// </summary>
        /// <param name="pos">Позиция звезды</param>
        /// <param name="dir">Направление движения звезды и скорость</param>
        /// <param name="size">размер звезды</param>
        public Star(Point pos, Point dir, Point size) : base(pos,dir,size)
        {

        }
        /// <summary>
        /// Отрисовка звезды
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawLine(Pens.White, Pos, new Point(Pos.X + Size.X, Pos.Y + Size.Y));
            Game.Buffer.Graphics.DrawLine(Pens.White, new Point(Pos.X + Size.X,Pos.Y), new Point(Pos.X,Pos.Y+ Size.Y));
        }
        /// <summary>
        /// Обновление состояния звезды
        /// </summary>
        public override void Update()
        {
            Pos.X -= Dir.X;
            if(Pos.X <= 0) Pos.X = Game.Width + Size.X;
        }
    }
}
