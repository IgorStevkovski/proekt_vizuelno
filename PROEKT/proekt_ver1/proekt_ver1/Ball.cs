using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proekt_ver1
{
    [Serializable]
    public class Ball
    {
        public static readonly int RADIUS = 15;
        public Point teme { get; set; }
        public Color color { get; set; }

        public float velocityY { get; set; }
        public bool pogodeno { get; set; }
        public int poeni { get; set; }

        public Rectangle frame;
        public Ball(Point teme, Color color)
        {
            this.teme = teme;
            this.color = color;
            this.pogodeno = false;
        }

        public Ball()
        {
 
        }
        public void draw(Graphics g)
        {
            Brush b = new SolidBrush(this.color);
            Brush b2 = new SolidBrush(Color.White);
            Font font = new Font("Arial",15);
            g.FillEllipse(b, teme.X - RADIUS, teme.Y - RADIUS, RADIUS*2, RADIUS*2);
            g.DrawString(String.Format(""+this.poeni),font, b2, teme.X - 10, teme.Y - 10);
            b.Dispose();
            b2.Dispose();
        }

        public void move(Rectangle bounds)
        {
            float nextY = teme.Y + velocityY;
            if (nextY - RADIUS <= bounds.Top)
            {
                this.teme = new Point(teme.X, bounds.Bottom);
            }
            else
            {
                this.teme = new Point(teme.X, (int)(teme.Y+velocityY));
            }
        }

        public bool isHit(Point tocka)
        {
            double d = Math.Sqrt((this.teme.X - tocka.X) * (this.teme.X - tocka.X) + (this.teme.Y - tocka.Y) * (this.teme.Y - tocka.Y));
            return d <= RADIUS;
        }
    }
}
