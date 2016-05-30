using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proekt_ver1
{
    [Serializable]
    public class Strela
    {
        public Point prva { get; set; }
        public Point vtora { get; set; }

        public float velocityStrelaX { get; set; } 
        public bool izlegla { get; set; }

        public Strela(Point prva, Point vtora, float velocityStrelaX)
        {
            this.prva = prva;
            this.vtora = vtora;
            this.izlegla = false;
            this.velocityStrelaX = velocityStrelaX;
        }

        public Strela()
        {
 
        }

        public void draw(Graphics g)
        {
            Pen p = new Pen(Color.Black, 2);
            g.DrawLine(p, this.prva, this.vtora);
            p.Dispose();
        }

        public void move(Rectangle bounds)
        {
            if (izlegla == false)
            {
                float nextX = vtora.X + velocityStrelaX;
                if (nextX > bounds.Right)
                    this.izlegla = true;
                else
                {
                    prva = new Point((int)(prva.X + velocityStrelaX), vtora.Y);
                    vtora = new Point((int)(prva.X + velocityStrelaX), vtora.Y);
                }
            }
        }
    }
}
