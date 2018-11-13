using System;
using System.Drawing;
using System.Windows.Forms;
namespace MyGame
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] _objs;
        public static Asteroid[] _asteroids;
        private static Bullet _bullet;
        internal const int numOfAsters = 10;
        internal const int numOfSmallStars = 90;
        internal const int numOfMiddleStars = 10;
        static Random r = new Random();
        static Game()
        {
        }
        /// <summary>
        /// Инициализируем форму игры
        /// </summary>
        /// <param name="form"></param>
        public static void Init(Form form)
        {
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            if (Width > 1600 || Height > 1600 || Width <0 || Height <0)
            {
                throw new ArgumentOutOfRangeException();
            }
            else
            {
                Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

                Timer timer = new Timer { Interval = 50 };
                timer.Start();
                timer.Tick += Timer_Tick;
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
        /// <summary>
        /// Создаем все объекты на форме
        /// </summary>
        public static void Load()
        {
            //Random r = new Random();
            _bullet = new Bullet(new Point(0, 200), new Point(5, 0));
            _asteroids = new Asteroid[numOfAsters];
            for (int i = 0; i < _asteroids.Length; i++)
            {
                _asteroids[i] = new Asteroid(new Point(r.Next(0, Width), r.Next(0, Height)), new Point(r.Next(-16, 16), r.Next(-16, 16)));
            }
            _objs = new BaseObject[numOfSmallStars + numOfMiddleStars];
            for (int i = 0; i < numOfSmallStars; i++)
            {
                _objs[i] = new SmallStar(new Point(r.Next(0, Width), r.Next(0, Height)), new Point(r.Next(3, 6), 0), 0, 4);
            }
            for (int i = numOfSmallStars; i < _objs.Length; i++)
            {
                _objs[i] = new MediumStar(new Point(r.Next(0, Width), r.Next(0, Height)), new Point(r.Next(9, 12), 0), 7,8);
            }
        }
        /// <summary>
        /// Отрисовываем объекты
        /// </summary>
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (var t in _objs)
                t.Draw();
            foreach (var a in _asteroids)
                a.Draw();
            _bullet.Draw();
            Buffer.Render();
        }
        /// <summary>
        /// Обновляем положение объектов
        /// </summary>
        public static void Update()
        {
            foreach (var t in _objs)
                t.Update();
            foreach (var a in _asteroids)
            {
                a.Update();
                if (a.Collision(_bullet))
                {
                    _bullet.Renew();
                    a.Renew();
                    //System.Media.SystemSounds.Hand.Play();
                }
            }
            _bullet.Update();
        }
    }

}
