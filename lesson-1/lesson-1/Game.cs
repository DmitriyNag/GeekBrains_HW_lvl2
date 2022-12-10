using Microsoft.VisualBasic.Logging;
using System.Diagnostics;
using System.Collections.Generic;

namespace lesson_1
{
    /// <summary>
    /// Класс игра в космический корабль
    /// </summary>
    static class Game
    {
        /// <summary>
        /// Переменная для геерации случайных чисел( например координат) в игре
        /// </summary>
        private static Random rnd;
        /// <summary>
        /// Переенная для отрисовки игры
        /// </summary>
        private static BufferedGraphicsContext _context;
        /// <summary>
        /// Переменная для буферизации игры
        /// </summary>
        internal static BufferedGraphics Buffer;
        /// <summary>
        /// Таймер, по которому происходит шаг обновления и отрисовки
        /// </summary>
        private static System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        /// <summary>
        /// Ширина поял игры
        /// </summary>
        public static int Width { get; set; }
        /// <summary>
        /// Высота поля игры
        /// </summary>
        public static int Height { get; set; }
        /// <summary>
        /// Лист объектов фона
        /// </summary>
        public static List<BackgroundObj> _bObjs;
        /// <summary>
        /// Лист Астероидов
        /// </summary>
        public static List<Asteroid> _asteroids;
        /// <summary>
        /// Лист Объектов пуля
        /// </summary>
        private static List<Bullet> _bullets;
        /// <summary>
        /// Переменная дляхранения объекта корабля
        /// </summary>
        private static Ship? _ship;
        /// <summary>
        /// Переменна для хранения объекта Аптечка
        /// </summary>
        private static Medkit _medkit;
        /// <summary>
        /// Action для уведомления о событиях игры
        /// </summary>
        private static event Action<string> gameEvent;
        /// <summary>
        /// Счет
        /// </summary>
        private static int Score;
        /// <summary>
        /// Уровень игры
        /// </summary>
        private static int Level;
        /// <summary>
        /// Количество астероидов на уровне
        /// </summary>
        private static int asterCount;
        /// <summary>
        /// количество крупных движущихся звезд на фоне игры
        /// </summary>
        const int starCount = 20;
        /// <summary>
        /// Количество мелких мерцающих звезд на фоне игры
        /// </summary>
        const int smallStarCount = 60;
        /// <summary>
        /// Задержка таймера обновления игры
        /// </summary>
        const int timerDelay = 50;
        /// <summary>
        /// Максимальная скорость Астероидов
        /// </summary>
        const int astSpeed = 10;
        /// <summary>
        /// Отступ от края поля игры, длягенерации объектов
        /// </summary>
        const int formPadding = 10;
        /// <summary>
        /// Вертикальная скорость корябля
        /// </summary>
        const int shipVSpeed = 5;
        /// <summary>
        /// Горизонтальная скорость корабля
        /// </summary>
        const int shipHSpeed = 5;
        /// <summary>
        /// Размер аптечки на экране
        /// </summary>
        const int medkidSize = 24;
        /// <summary>
        /// Кол-во HP которое восстанавливает аптечка
        /// </summary>
        const int medkitHP = 10;
        /// <summary>
        /// Кол-во HP которое отнимает Астероид при столкновении (отрицательное значение)
        /// </summary>
        const int asterHP = -25;
        /// <summary>
        /// Кол-во Hp корабля на старте игры
        /// </summary>
        const int shipHP = 75;

        /// <summary>
        /// Статический конструктор класса Game
        /// </summary>
        static Game()
        {
            _context = BufferedGraphicsManager.Current;
            _bObjs = new List<BackgroundObj>();
            _asteroids = new List<Asteroid>();
            _bullets = new List<Bullet>(); 
            rnd = new Random();
            ActiveObj.log += Log.LogToFile;
            gameEvent += Log.LogToFile;
            Score = 0;
            Level = 1;
            asterCount = 1;
        }
        /// <summary>
        /// Инициализация игры
        /// </summary>
        /// <param name="form">форма, вкоторой будем зпускать и рисовать игру</param>
        internal static void Init(Form form)
        {
            Graphics g;
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width; 
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g,new Rectangle(0,0,Width,Height));
            BackgroundLoad();
            Load();
            
            timer.Interval = timerDelay;
            timer.Start();
            timer.Tick += Timer_Tick;
            if (_ship != null) _ship.MessageDie += Finish;
            form.KeyDown += Form_KeyDown;
            gameEvent?.Invoke("...Game initiated...");
        }
        /// <summary>
        /// Метод отследивае нажатие клавишь для игры
        /// </summary>
        /// <param name="sender">объект отправитель собятия</param>
        /// <param name="e"></param>
        private static void Form_KeyDown(object? sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Up) _ship?.Up();
            if (e.KeyCode == Keys.Down) _ship?.Down();
            if ((e.KeyCode == Keys.Space) && (_ship != null))
            {
                _bullets.Add(new Bullet(new Point(_ship.rect.X + _ship.XY.X, _ship.rect.Y + _ship.XY.Y / 2)));
            }
        }
        /// <summary>
        /// Загрузка обьектов в игру
        /// </summary>
        public static void Load()
        {
            ShipSpawn();
            AsteroidsSpawn(asterCount);
            MedKSpawn();
        }
        /// <summary>
        /// Загрузка объекто фона игры
        /// </summary>
        public static void BackgroundLoad()
        {
            for (int i = 0; i < starCount; i++)
            {
                _bObjs.Add(new Star(new Point(rnd.Next(1, Width), rnd.Next(1, Height)), new Point(1, 0), new Point(2, 2)));
            }

            for (int i = 0; i < smallStarCount; i++)
            {
                _bObjs.Add(new SmallStar(new Point(rnd.Next(1, Width), rnd.Next(1, Height))));
            }
        }

        /// <summary>
        /// Обновление картинки по таймеру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Timer_Tick(object? sender, EventArgs e)
        {
            try
            {
                Draw();
                Update();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /// <summary>
        /// Отрисовка картинки в буфер по каждому объекту игры
        /// </summary>
        internal static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            _medkit.Draw();
            foreach (var bullet in _bullets) bullet.Draw();
            foreach (var backgroundObj in _bObjs) backgroundObj.Draw();
            foreach (var asteroid in _asteroids) asteroid.Draw();
            _ship?.Draw();
            ScoreDraw();
            Buffer.Render();
        }
        /// <summary>
        /// Обновление каждого объекта игры
        /// </summary>
        internal static void Update()
        {
            foreach (var backgroundObj in _bObjs) backgroundObj.Update();
            for (int i = 0; i < _asteroids.Count; i++)
            {
                _asteroids[i].Update();
                if (_ship?.Collision(_asteroids[i]) ?? false)
                {
                    _ship?.EnergyChng(asterHP);
                    _asteroids.RemoveAt(i);
                    if (_asteroids.Count == 0) LevelUp();
                    if (_ship?.Energy <= 0) _ship?.Die();
                }
            }

            for (int i = 0; i < _bullets.Count; i++)
            {
                _bullets[i].Update();
                if (_bullets[i].Collision(_medkit))
                {
                    MedKSpawn();
                    _ship?.EnergyChng(medkitHP);
                    _bullets.RemoveAt(i);
                }

                for (int j = 0; j < _asteroids.Count; j++)
                {
                    if (_bullets[i].Collision(_asteroids[j]))
                    {
                        _asteroids.RemoveAt(j);
                        if (_asteroids.Count == 0) LevelUp();
                        _bullets.RemoveAt(i);
                        Score += 100;
                    }
                }
            }
        }
        /// <summary>
        /// Завершение игры
        /// </summary>
        internal static void Finish()
        {
            timer.Stop();
            Draw();
            Buffer.Graphics.DrawString("The End", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, Width / 2, Height / 2);
            Buffer.Render();
            gameEvent.Invoke("...Game is ended...");
        }
        /// <summary>
        /// Вывод Счета на экран
        /// </summary>
        internal static void ScoreDraw() => Game.Buffer.Graphics.DrawString($"Level: {Level}  | Score: {Score}", 
            new Font(FontFamily.GenericSansSerif, 20), Brushes.White, Width - 300, 0);
        /// <summary>
        /// создание корабля
        /// </summary>
        internal static void ShipSpawn() => _ship = new Ship(new Point(formPadding, Height / 2), 
            new Point(shipHSpeed, shipVSpeed), new Point(20, 10), shipHP);
        /// <summary>
        /// Создание аптечки
        /// </summary>
        internal static void MedKSpawn() => _medkit = new Medkit(new Point(rnd.Next(Width / 2, Width - formPadding), 
            rnd.Next(formPadding, Height - formPadding)), new Point(medkidSize, medkidSize), medkitHP);
        /// <summary>
        /// Создание астероидов
        /// </summary>
        /// <param name="asterCount"></param>
        internal static void AsteroidsSpawn(int asterCount)           
        {
            for (int i = 0; i < asterCount; i++)
            _asteroids.Add(new Asteroid(new Point(rnd.Next(Width / 2, Width - formPadding), rnd.Next(formPadding, Height - formPadding)), 
                new Point(rnd.Next(-astSpeed, astSpeed), rnd.Next(-astSpeed, astSpeed))));
        }
        /// <summary>
        /// Переход на следующий уровень игры
        /// </summary>
        internal static void LevelUp()
        {
            AsteroidsSpawn(++asterCount);
            Level++;
        }

}
}