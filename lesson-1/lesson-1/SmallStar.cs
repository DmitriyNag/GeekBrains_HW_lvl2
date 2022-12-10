namespace lesson_1
{
    /// <summary>
    /// Класс фона - малая недвижимая (мерцающая) звезда
    /// </summary>
    internal class SmallStar: BackgroundObj
    {
        /// <summary>
        /// Переменная для генерации случайных адержек и цвета мерацющей звезды
        /// </summary>
        static internal Random rnd;
        /// <summary>
        /// Минимальная задержка в изменении яркости звезды
        /// </summary>
        internal const int minDelay = 5;
        /// <summary>
        /// Максимальная задержка в изменении яркости звезды
        /// </summary>
        internal const int MaxDelay = 10;
        /// <summary>
        /// Счетчик шагов, для изменения яркости звезды
        /// </summary>
        internal int brightStepCount = 0;
        /// <summary>
        /// Рандомная задержка перед измененим яркости звезды от minDelay до MaxDelay
        /// </summary>
        internal int brightStepDelay;
        /// <summary>
        /// Список цветов Звезды, которые она может принимать
        /// </summary>
        internal List<Brush> starBrushes = new List<Brush> { Brushes.Gray, Brushes.DarkGray, Brushes.WhiteSmoke, Brushes.White };
        /// <summary>
        /// Начальнй цвет звезды
        /// </summary>
        internal int starBrushIndex;

        /// <summary>
        /// Статический конструктор мерцающих звезд
        /// </summary>
        static SmallStar()
        {
            rnd = new Random();
        }
        /// <summary>
        /// Конструктор мерцающей звезды
        /// </summary>
        /// <param name="Pos">Позиция звезды</param>
        public SmallStar(Point Pos) : base(Pos, new Point(0, 0), new Point(2,2))
        {
            brightStepDelay = rnd.Next(minDelay, MaxDelay);
            starBrushIndex = rnd.Next(0, starBrushes.Count - 1);
        }
        /// <summary>
        /// Отрисовка малой звезды 
        /// </summary>
        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(starBrushes[starBrushIndex], new Rectangle(Pos.X, Pos.Y, Size.X, Size.Y));
        }
        /// <summary>
        /// Обновление состояния (яркости) малой звезды
        /// </summary>
        public override void Update()
        {
            if (brightStepCount++ >= brightStepDelay)
            {
                brightStepCount = 0;
                starBrushIndex = starBrushIndex == starBrushes.Count - 1 ? 0 : starBrushIndex + 1;
            }
        }
    }
}
