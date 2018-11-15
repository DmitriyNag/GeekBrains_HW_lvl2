using System;
using System.Drawing;
using System.Windows.Forms;
namespace MyGame
{
    //      1. Добавить космический корабль, как описано в уроке.
    //      2. Добработать игру «Астероиды».
    //       а) Добавить ведение журнала в консоль с помощью делегатов;
    //       б) * Добавить это и в файл.
    //      3. Разработать аптечки, которые добавляют энергию.
    //      4. Добавить подсчет очков за сбитые астероиды.
    public delegate void MyDelegate(string str);
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }

        public static BaseObject[] _objs;
        public static Asteroid[] _asteroids;
        private static Bullet _bullet;
        private static Ship _ship;
        private static Medkit _medkit;

        internal const int numOfAsters = 10;
        internal const int numOfSmallStars = 200;
        internal const int numOfMiddleStars = 10;
        internal const int shipMaxEnergy = 50;
        internal const int EnergyForMedkit = 5;
        internal const int EnergyForAsterCollision = 10;
        internal const int scoreForAster = 10;

        static Random r = new Random();
        private static Timer timer;
        public static event MyDelegate Msg;

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
                timer = new Timer { Interval = 25 };
                timer.Start();
                timer.Tick += Timer_Tick;
                form.KeyDown += Form_KeyDown;
                Ship.ShipDie += Finish;
                Logger.StartLog();
                Msg += Logger.Log;
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        /// <summary>
        /// Отслеживаем нажатие клавиш
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Form_KeyDown(object sender, KeyEventArgs  e)
        {
            if(e.KeyCode == Keys.Up) _ship.Up();
            if(e.KeyCode == Keys.Down) _ship.Down();
            if(e.KeyCode == Keys.Right) _ship.Right();
            if(e.KeyCode == Keys.Left) _ship.Left();
            if (e.KeyCode == Keys.Space)
            {
                _bullet = new Bullet(new Point(_ship.ShipNouse.X, _ship.ShipNouse.Y), new Point(10, 0),
                    new Size(10, 4));
                Msg?.Invoke($"Произведен выстрел c координатами X:{_ship.Rect.X} Y:{_ship.Rect.Y}");
                _bullet.BulletDelete += DeleteObj;
            } 
        }
        /// <summary>
        /// Создаем все объекты на форме
        /// </summary>
        public static void Load()
        {
            _ship = new Ship(new Point(10,Height/2),new Point(20,20));
            _medkit = new Medkit(new Point(Game.Width * 3, r.Next(20, Game.Height - 20)), new Point(r.Next(10, 20), 0));

            _asteroids = new Asteroid[numOfAsters];
            for (int i = 0; i < _asteroids.Length; i++)
                _asteroids[i] = new Asteroid(new Point(r.Next(0, Width), r.Next(0, Height)), new Point(r.Next(-8, 8), r.Next(-8, 8)));

            _objs = new BaseObject[numOfSmallStars + numOfMiddleStars];
            for (int i = 0; i < numOfSmallStars; i++)
                _objs[i] = new SmallStar(new Point(r.Next(0, Width), r.Next(0, Height)), new Point(r.Next(1, 3), 0));

            for (int i = numOfSmallStars; i < _objs.Length; i++)
                _objs[i] = new MediumStar(new Point(r.Next(0, Width), r.Next(0, Height)), new Point(r.Next(4, 5), 0));

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
                a?.Draw();
            _bullet?.Draw();
            _medkit?.Draw();
            _ship.Draw();
            TextDraw();
            Buffer.Render();
        }
        /// <summary>
        /// Обновляем положение объектов и ловим столкновения
        /// </summary>
        public static void Update()
        {
            foreach (var t in _objs)
                t.Update();
            for (int i = 0; i < _asteroids.Length; i++)
            {
                if(_asteroids[i]==null) continue;
                _asteroids[i].Update();
                if (_bullet != null && _bullet.Collision(_asteroids[i]))
                {
                    _asteroids[i].Renew();
                    DeleteObj(_bullet);
                    _ship.ScoreUp(scoreForAster);
                    Msg?.Invoke($"пуля попала в астероид {i} c координатами X:{_asteroids[i].Rect.X} Y:{_asteroids[i].Rect.Y} | Счет: {_ship.Score}");
                    continue;
                }

                if (_ship.Collision(_asteroids[i]))
                {
                    _asteroids[i].Renew();
                    _ship.EnergyLow(EnergyForAsterCollision);
                    Msg?.Invoke($"В корабль (Energy: {_ship.Energy})попал астероид {i} c координатами X:{_ship.Rect.X} Y:{_ship.Rect.Y}");

                    if (_ship.Energy <= 0)
                    {
                        Msg?.Invoke($"Корабль уничтожен");
                        _ship?.Die();
                    }
                }
            }

            if (_ship.Collision(_medkit))
            {
                _medkit.Regenerate();
                _ship.EnergyHigh(EnergyForMedkit);
                if (_ship.Energy > shipMaxEnergy) _ship.Energy = shipMaxEnergy;
                Msg?.Invoke($"Корабль (Energy: {_ship.Energy}) собрал аптечку c координатами X:{_ship.Rect.X} Y:{_ship.Rect.Y}");

            }
            _bullet?.Update();
            _medkit?.Update();
            _ship.Update();
        }
        /// <summary>
        /// Завершение игры
        /// </summary>
        public static void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 40, FontStyle.Underline), Brushes.White, Game.Width/ 2-100, Game.Height/2-50);
            Buffer.Graphics.DrawString($"Your score: {_ship.Score}", new Font(FontFamily.GenericSansSerif, 30), Brushes.White, Game.Width / 2-100, Game.Height / 2+50);
            Buffer.Render();
            Logger.CloseLog();
        }

        /// <summary>
        /// Метод для удаления объекта
        /// </summary>
        /// <param name="obj"></param>
        public static void DeleteObj(object obj)
        {
            obj = null;
        }
        /// <summary>
        /// Выаодим на экран счет и здоровье
        /// </summary>
        private static void TextDraw()
        {
            if (_ship != null)
                Buffer.Graphics.DrawString("Energy:" + _ship.Energy, new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold), Brushes.White, 0, 0);
            if (_ship != null)
                Buffer.Graphics.DrawString("Score:" + _ship.Score, new Font(FontFamily.GenericMonospace, 16, FontStyle.Bold), Brushes.White, Game.Width - 150, 0);
        }
    }
}
