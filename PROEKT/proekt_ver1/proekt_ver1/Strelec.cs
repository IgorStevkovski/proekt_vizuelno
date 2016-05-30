using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace proekt_ver1
{
    [Serializable]
    public class Strelec
    {
        public Point teme { get; set; }
        public Image slikaStrela;
        
        public Strelec()
        {
            teme = new Point(0, 100);
            slikaStrela = Properties.Resources.strela_1;
        }

        public void draw(Graphics g)
        {
            g.DrawImage(slikaStrela, teme);
        }

        public void move(int x, int y)
        {
            teme = new Point(teme.X + x, teme.Y + y);
        }
    }
}
