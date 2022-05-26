using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_work5
{
    public partial class Form1 : Form
    {
        //public Circle circle;
        //public Graphics graphics;
        //public Color back=Color.Green;
       
        Circle[] balls = null;
        bool isPaint = false;
        bool stop = false;
        Thread[] threads = null;
        static object locker = new object();

        public Form1()
        {
            InitializeComponent();
            //Bitmap bmp = new Bitmap(panel1.Width, panel1.Height);
            //panel1.DrawToBitmap(bmp, panel1.Bounds);
            //bmp.Save(@"C:\Users\12\Desktop\bl.jpg");
            this.DoubleBuffered = true;
            label1.Text = "Шары";
            //this.BackColor = back;
            //this.Width = 850;
            //this.Height = 600;'
            Button button = new Button();
            button.BackColor.

        }
        private void moveBall(object obj)
        {
            Circle ball = (Circle)obj;
            while (ball.Speed > 0)
            {
                lock (locker)
                {
                    ball.Move();
                    panel1.Invalidate();
                }
                ball.Speed -= 0.05f;

                if (!stop)
                {
                    Thread.Sleep(20);
                }
                else
                    while (stop) ;
            }
        }

        void Start()
        {
            balls = new Circle[10];
            balls[0] = new Circle(1,  30, 10, Color.Black, 5, 5);
            balls[1] = new Circle(2,  30, 10, Color.Brown, 10, 8);
            balls[2] = new Circle(3,  30, 10, Color.DarkViolet, 15, 15);
            balls[3] = new Circle(4,  30, 10, Color.DimGray, 20, 6);
            balls[4] = new Circle(5, 30, 10, Color.DodgerBlue, 25, 10);
            balls[5] = new Circle(1, 30, 10, Color.Black, 5, 5);
            balls[6] = new Circle(2, 30, 10, Color.Brown, 10, 8);
            balls[7] = new Circle(3, 30, 10, Color.DarkViolet, 15, 15);
            balls[8] = new Circle(4, 30, 10, Color.DimGray, 20, 6);
            balls[9] = new Circle(5, 30, 10, Color.DodgerBlue, 25, 10);
            balls[9].MaxPoint = new PointF(panel1.ClientSize.Width, panel1.ClientSize.Height);

            isPaint = true;
            threads = new Thread[10];
            for (int i = 0; i < 10; ++i)
            {
                threads[i] = new Thread(moveBall);
                threads[i].Start(balls[i]);
            }
            for (int i = 0; i < 10; ++i)
                threads[i].Join();


            MessageBox.Show("Конец игры! Перезапусти приложение:)");
            balls = null;
            threads = null;
        }

        //public void AddCircle()
        //{
        //    circle = new Circle(Color.Blue);
        //    balls.Add(circle);
        //    graphics.Clear(BackColor);
        //}

        private void timer1_Tick(object sender, EventArgs e)
        {
           // circle.Move(graphics);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // graphics = CreateGraphics();
         
           // isPaint = false;
        }

        private void Form1_Click(object sender, EventArgs e)
        {
           
            //circle = new Circle(Color.Blue);
            //graphics.Clear(BackColor);
            //timer1.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (balls == null)
            {
                Thread myThread = new Thread(Start);
                myThread.Start();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        { 
            //stop = false;
            //if (balls != null)
            //{
            //    foreach (Circle b in balls)
            //        b.Speed = 0;
            //}
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!stop)
                stop = true;
            else
                stop = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (!isPaint)
                return;
            if (balls != null)
            {
                foreach (Circle ball in balls)
                {
                    lock (locker)
                    {
                        if (!ball.IsAlive)
                            e.Graphics.FillEllipse(new SolidBrush(Color.DarkKhaki), ball.Show().X, ball.Show().Y, ball.Radius, ball.Radius);
                       
                    }
                }
            }
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            if (isPaint && balls != null)
            {
                lock (locker)
                {
                    balls[0].MaxPoint = new PointF(panel1.ClientSize.Width, panel1.ClientSize.Height);
                }
            }
        }

      
        //?????????????
       
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            button2_Click(sender, e);

        }

        private void label1_Click(object sender, EventArgs e)
        {
            label1.Text = "Шары";
        }
    }
}
