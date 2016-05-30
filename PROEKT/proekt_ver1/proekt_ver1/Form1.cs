using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace proekt_ver1
{
    public partial class Form1 : Form
    {
        //Image myImg;
        Dokument doc;
        public Rectangle frame;
        public Timer timer;
        public Timer timerVreme;

        public String FileName;
        public Form1()
        {
            InitializeComponent();
           // Image myImg = Image.FromFile("C:\\Users\\Igor\\Desktop\\Vizuelno\\PROEKT\\proekt_ver1\\proekt_ver1\\Resources\\strela_1.jpg");
           /* Graphics g = this.CreateGraphics();
            g.DrawImageUnscaled(myImg,60,60); */
           
            /*Bitmap doubleBuffer = new Bitmap(this.Width,this.Height);
            Graphics g = this.CreateGraphics();
            Graphics gr = Graphics.FromImage(doubleBuffer);
            gr.DrawImageUnscaled(myImg,0,0);*/ 

            //myImg = Properties.Resources.strela_1;
            //e.Graphics.DrawImage(myImg, 0, 50);
            
            //Graphics g = this.CreateGraphics();
            //g.DrawImage(Bitmap.FromFile("strela_1.jpg"),new Point(0,0));

            this.Name = "Brz && Precizen";

            frame = new Rectangle (0, 60, this.Width, this.Height-60);
            doc = new Dokument(frame);
            

            timer = new Timer();
            timer.Interval = 50;
            timer.Tick += new EventHandler(timer_Tick);
            timer.Start();

            timerVreme = new Timer();
            timerVreme.Interval = 1000;
            timerVreme.Tick += new EventHandler(timerVreme_Tick);
            timerVreme.Start();
            this.DoubleBuffered = true;
        }

        private void timerVreme_Tick(object sender, EventArgs e)
        {
            if(toolStripProgressBar_PreostanatoVreme.Value < 90)
            {
                toolStripProgressBar_PreostanatoVreme.Value += 1;
                toolStripStatusLabel_PreostanantoVreme.Text = toolStripProgressBar_PreostanatoVreme.Value + "/90";
            }
            else
            {
                timerVreme.Enabled = false;
                timer.Enabled = false;
                //toolStripProgressBar_PreostanatoVreme.Value = 0;
                DialogResult result = MessageBox.Show("Osvoivte "+doc.vkupnoPoeni+" poeni.\n Sakate li nova igra?", "Igrata završi!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    frame = new Rectangle(0, 60, this.Width, this.Height - 60);
                    doc = new Dokument(frame);//sozdavanje novi podatoci
                    toolStripProgressBar_PreostanatoVreme.Value = 0;
                    doc.vkupnoPoeni = 0;
                    
                    timer.Enabled = true;
                    timerVreme.Enabled = true;
                    timerVreme.Interval = 1000;
                    Invalidate();//za da se preiscrta dokumentot t.e site novi podatoci
                }
                else
                {
                    Close();
                }
            }
            
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            doc.move();
            doc.checkCollisions();
            Invalidate();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(Color.White);
            doc.draw(e.Graphics);
            this.toolStripStatusLabel_BrojPoeni.Text = "Broj poeni: " + doc.vkupnoPoeni;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                doc.strelec.move(0, 5);
                Invalidate(true);
            }
            else if (e.KeyCode == Keys.Up)
            {
                doc.strelec.move(0, -5);
                Invalidate(true);
            }
            else if (e.KeyCode == Keys.A)//sozdavanje strela
            {
                if (doc.streli.Count <= doc.brojStreli)
                {
                    int x = doc.strelec.teme.X + 30;
                    int x2 = doc.strelec.teme.X + 50;
                    int y = doc.strelec.teme.Y + doc.strelec.slikaStrela.Height / 2;
                    Point prva = new Point(x, y);
                    Point vtora = new Point(x2, y);
                    Strela strela = new Strela(prva, vtora,doc.brzinaStrela);//NE TREBA DA E 10
                    doc.streli.Add(strela);
                }
                
            }
            else if (e.KeyCode == Keys.Right)
            {
                toolStripButton_BrzinaPlus_Click(null,null);
            }
            else if (e.KeyCode == Keys.Left)
            {
                toolStripButton_BrzinaMinus_Click(null,null);
            }
            else if (e.KeyCode == Keys.W)
            {
                toolStripButton_DodajTopcinja_Click(null,null);
            }
        }

        private void toolStripButton_BrzinaPlus_Click(object sender, EventArgs e)
        {
            if (doc.brzinaStrela < 30)
            {
                doc.brzinaStrela += 5;
                doc.brojStreli--;
            }
            
        }

        private void toolStripButton_BrzinaMinus_Click(object sender, EventArgs e)
        {
            if (doc.brzinaStrela > 10)
            {
                doc.brzinaStrela -= 5;
                doc.brojStreli++;
            }
                
        }

        private void toolStripButton_DodajTopcinja_Click(object sender, EventArgs e)
        {
            if (doc.balls.Count == 0)
            {
                doc.osnovajTopcinja();
                toolStripProgressBar_PreostanatoVreme.Value = 0;
                if (timerVreme.Interval >= 600)//za da na minimum 500ms se spusti intervalot na timerot
                //i od prvicnite 90 sek. na progres bar-ot min. dojde do 45 sek. postepeno
                {
                    timerVreme.Interval = timerVreme.Interval - 100;
                }
                doc.nivo += 1;
                toolStripStatusLabel_Nivo.Text = "Nivo: " + doc.nivo;
                Invalidate(true);
            }
        }

        private void toolStripButton_Pauza_Click(object sender, EventArgs e)
        {
            if (timer.Enabled == true)
            {
                timer.Enabled = false;
                timerVreme.Enabled = false;
            }
            else
            {
                timer.Enabled = true;
                timerVreme.Enabled = true;
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frame = new Rectangle(0, 60, this.Width, this.Height - 60);
            doc = new Dokument(frame);
            toolStripProgressBar_PreostanatoVreme.Value = 0;
            doc.vkupnoPoeni = 0;
            timerVreme.Interval = 1000;
        }

        private void saveFile()
        {
            if (FileName == null)
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Brz && Precizen doc file (*.bip)|*.bip";
                saveFileDialog.Title = "Save document";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FileName = saveFileDialog.FileName;
                }
            }
            if (FileName != null)
            {
                using (FileStream fileStream = new FileStream(FileName, FileMode.Create))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fileStream, doc);
                }
            }
        }
        private void openFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Brz && Precizen file (*.bip)|*.bip";
            openFileDialog.Title = "Open doc file";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                FileName = openFileDialog.FileName;
                try
                {
                    using (FileStream fileStream = new FileStream(FileName, FileMode.Open))
                    {
                        IFormatter formater = new BinaryFormatter();
                        doc = (Dokument)formater.Deserialize(fileStream);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Could not read file: " + FileName);
                    FileName = null;
                    return;
                }
                Invalidate(true);
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timerVreme.Enabled = false;
            openFile();
            timer.Enabled = true;
            timerVreme.Enabled = true;
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timerVreme.Enabled = false;
            saveFile();
            timer.Enabled = true;
            timerVreme.Enabled = true;
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timerVreme.Enabled = false;
            openFile();
            timer.Enabled = true;
            timerVreme.Enabled = true;
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timerVreme.Enabled = false;
            saveFile();
            timer.Enabled = true;
            timerVreme.Enabled = true;
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            frame = new Rectangle(0, 60, this.Width, this.Height - 60);
            doc = new Dokument(frame);
            toolStripProgressBar_PreostanatoVreme.Value = 0;
            doc.vkupnoPoeni = 0;
            timerVreme.Interval = 1000;

        }       
    }
}
