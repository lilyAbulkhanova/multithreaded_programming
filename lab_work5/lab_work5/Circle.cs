using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab_work5
{
  public class Circle
    {
        private float x;
        private float y;
        private float radius;
        private Color color;
        private float way;
        private float speed;
        static PointF maxPoint;
        private bool isAlive = false;
        private float dx;
        private float dy;

        public float X
        {
            set { x = value; }
            get { return x; }
        }
         public float Y
        {
            set { y = value; }
            get { return y; }
        }
        
        
        public float DX
        {
            set { dx = value; }
            get { return dx; }
        }

       public float DY
        {
            set { dy = value; }
            get { return dy; }
        }
        public Color COLOR
        {
            set { color = value; }
            get { return color; }
        }
        public float Speed
        {
            get { return speed; }
            set { speed = value; }
        }
        public PointF MaxPoint
        {
            get { return maxPoint;}
            set { maxPoint = value;}
        }
        public PointF Locate
        { get { return new PointF(x, y);}
            set
            {
                x = value.X;
                y = value.Y;
            }
        }
        public void Alive()
        {
            if (isAlive)
            {
                isAlive = false;
            }
            else isAlive = true;
        }

        public bool IsAlive
        {
            get { return isAlive; }
        }

      
        public float Radius
        {
            get { return radius; }
        }
        public void Next()
        {
            if (x >= Form1.ActiveForm.Width - 2 * (radius + dx))
                dx = -dx;
            if (x <= 0) dx = -dx; x += dx;
            if (y >= Form1.ActiveForm.Height - SystemInformation.CaptionHeight - 2 * (radius + dy))
                dy = -dy;
            if (y <= 0)
                dy = -dy;
            y += dy;
        }

        public Circle ( float x, float y, float r, Color colour, float way, float speed)
        {
            this.x = x;
            this.y = y;
            this.radius = r;
            this.color = colour;
            this.way = way;
            this.speed = speed;


        }
        public void Collide() 
        {
            if (x + radius > maxPoint.X)
            {
                x = x - (x + radius - maxPoint.X);
                way = (float)Math.PI - way;
                Next();
            }
            if (y + radius >= maxPoint.Y) //!!!!
            {
                y = y - (y + radius - maxPoint.Y) * 2;
                way = (float)Math.PI * 2 - way;
                Next();
            }
            if (x < 0)
            {
                x = -x;
                way = (float)Math.PI - way;
                Next();
            }
            if (y < 0)
            {
                y = -y;
                way = (float)Math.PI * 2 - way;
                Next();
            }
        }
       
        public void Move()
        {
            x += (float)Math.Cos(way) * speed;
            y += (float)Math.Sin(way) * speed;
            Collide();
        }

      


    }
}
