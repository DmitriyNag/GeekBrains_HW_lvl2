namespace lesson_1
{
    /// <summary>
    /// Описывает объект Астероид
    /// </summary>
    class Asteroid : ActiveObj, ICloneable, IComparable<Asteroid>
    {
        /// <summary>
        /// Статический номер Астероида
        /// </summary>
        static private int aNumber = default(int); 
        /// <summary>
        /// Путь к изображению астероида
        /// </summary>
        static protected Image Aster = Image.FromFile("..\\..\\..\\aster.gif");
        /// <summary>
        /// Сила астероида
        /// </summary>
        public int Power { get; set; }
        /// <summary>
        /// конструктор, создает астериод
        /// </summary>
        /// <param name="pos">Позиция объекта</param>
        /// <param name="dir">Направление двжиения объекта и скорость</param>
        public Asteroid(Point pos, Point dir) : base(pos, dir) 
        {
            aNumber++;
            Size.X = Aster.Width;
            Size.Y = Aster.Height;
            Power = 3;
            OnLog($"Asterod #{aNumber} added in X:{Pos.X} Y: {Pos.Y}");
        }
        /// <summary>
        /// Отрисовка Астероида
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.DrawImage(Aster, Pos);
        }
        /// <summary>
        /// Обновление состояния Астероида
        /// </summary>
        public override void Update()
        {
            Pos.X += Dir.X;
            Pos.Y += Dir.Y;
            if (Pos.X <= 0 || Pos.X >= Game.Width - Size.X) Dir.X = -Dir.X;
            if (Pos.Y <= 0 || Pos.Y >= Game.Height - Size.Y) Dir.Y = -Dir.Y;
        }
        /// <summary>
        /// Реализация интерфейса IClonable, клонирует текущий объект
        /// </summary>
        /// <returns>Обьект Астероид</returns>
        public object Clone()
        {
            var asteroid = new Asteroid(Pos, Dir);
            asteroid.Power = Power; 
            return asteroid;
        }
        /// <summary>
        /// Реализация интерфейса IComparable
        /// </summary>
        /// <param name="a">Астероид с которым сравниваем текущий объект</param>
        /// <returns></returns>
        int IComparable<Asteroid>.CompareTo(Asteroid? a)
        {
            if(Power > a?.Power) return 1;
            if(Power < a?.Power) return -1;
            return 0;
        }
    }
}
