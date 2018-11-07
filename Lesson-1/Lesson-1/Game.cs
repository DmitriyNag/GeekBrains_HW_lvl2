using System;
using System.Drawing;
using System.Windows.Forms;
namespace Lesson_1
{
    static class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        public static int Width { get; set; }
        public static int Height { get; set; }
        public static BaseObject[] _objs;
        internal const int numOfAsters = 10;
        internal const int numOfSmallStars = 50;
        internal const int numOfMiddleStars = 20;
        static Game()
        {
        }
        public static void Init(Form form)
        {
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего приложения
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics();
            Width = form.ClientSize.Width;
            Height = form.ClientSize.Height;
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            Timer timer = new Timer { Interval = 50 };
            timer.Start();
            timer.Tick += Timer_Tick;
        }
        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        private static void Load()
        {
            Random r = new Random();
            _objs = new BaseObject[numOfAsters + numOfSmallStars + numOfMiddleStars];
            for (int i = 0; i < numOfAsters; i++)
            {
                _objs[i] = new Asteroid(new Point(r.Next(0, Width), r.Next(0, Height)), new Point(r.Next(-16, 16), r.Next(-16, 16)));
            }
            for (int i = numOfAsters; i < numOfAsters + numOfSmallStars; i++)
            {
                _objs[i] = new SmallStar(new Point(r.Next(Width, 2 * Width), r.Next(0, Height)), new Point(r.Next(3, 6), 0));
            }
            for (int i = numOfAsters + numOfSmallStars; i < _objs.Length; i++)
            {
                _objs[i] = new MediumStar(new Point(r.Next(Width, 2 * Width), r.Next(0, Height)), new Point(r.Next(9, 12), 0));
            }
        }
        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);
            foreach (var t in _objs)
                t.Draw();
            Buffer.Render();
        }
        public static void Update()
        {
            foreach (var t in _objs)
                t.Update();
        }
    }

}
