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

            _objs = new BaseObject[30];
            for (int i = 0; i < _objs.Length/2; i++)
            {
                _objs[i] = new Star(new Point(Width, r.Next(0, Height)),new Point(i, 0),new Size(6,6)); 
            }
            for (int i = _objs.Length / 2; i < _objs.Length; i++)
            {
                _objs[i] = new BaseObject(new Point(Width, r.Next(0, Height)), new Point(-i, -i), new Size(10, 10));
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
