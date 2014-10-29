using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows;
using System.Drawing.Imaging;
using System.Media;
namespace TestProjet
{
    public partial class MenuDemineur : Form
    {
        //Creation tableau de mines
        int[][] mineArray;
        int niveauJoue = 0;
        System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
        private Timer timer1;
        public void InitTimer()
        {
            timer1 = new Timer();
            timer1.Tick += new EventHandler(timer1_Tick);
            timer1.Interval = 1000; // in miliseconds
            timer1.Start();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan ts = stopWatch.Elapsed;
            string elapsedTime = String.Format("{1:00}:{2:00}",
            ts.Hours, ts.Minutes, ts.Seconds,
            ts.Milliseconds / 10);
            labelTime.Text = elapsedTime;
        }
        public MenuDemineur()
        {
            InitializeComponent();
        }
        public bool winPartie()
        {
            int nbButtonsOff = -1;
            int.TryParse(this.labelMine.Text, out nbButtonsOff);
            if (nbButtonsOff != -1)
            {
                int i = 0;
                Boolean existButton = false;
                foreach (object o in this.damier.Controls)
                {
                    if (o is Button)
                    {
                        Button btn = (Button)o;
                        if (btn.BackColor == Color.Goldenrod)
                        {
                            i++;
                        }
                        existButton = true;
                    }
                }
                if (i == 0 && existButton)
                    return true;
                if (i == nbButtonsOff && existButton &&nbButtonsOff!=0)
                    return true;
                return false;
            }
            return false;

        }
        public static Image resizeImage(Image imgToResize, Size size)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        protected void MyButtonClickPanel(object who, MouseEventArgs e)
        {
            stopWatch.Start();
            InitTimer();
            Button btn = (Button)who;
            String wichButton = btn.Name;
            int x = 0;
            int y = 0;
            int gameNbMine = 0;
            btn.Font = new Font("Georgia", 8);
            //Recupere coordonnées du boutton clické
            x= int.Parse(wichButton.Substring(0, wichButton.IndexOf('|')));
            y = int.Parse(wichButton.Substring(wichButton.IndexOf('|')+1, wichButton.Length-(wichButton.IndexOf('|')+1)));
            //Si click gauche
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                    //Si le boutton clicqué est rouge (marquage mine) il n'est pas clickable
                    if (btn.BackColor != Color.Red)
                    {

                            //On appele le caseResolver
                            //MessageBox.Show("boutton cliqué x " + x + " y : " + y + " mine : " + resolveCase(x, y), "titre du message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            if (resolveCase(x, y) == -1)
                            {
                                stopWatch.Stop();
                                //Si la case contient une mine
                                //Bruit de mine qui explose
                                String myPath = "..\\..\\ressources\\221.wav";
                                System.Media.SoundPlayer player = new SoundPlayer();
                                player.SoundLocation = myPath;
                                player.LoadAsync();
                                player.Play();   //asynchronous (loop)playing in new thread

                                btn.Text = "M";
                                Image img = System.Drawing.Image.FromFile("..\\..\\ressources\\mine.jpg");
                                img = resizeImage(img, new Size(20, 20));
                                btn.BackgroundImage = img;
                                //btn.BackgroundImage.Size = new Size(20, 20);
                                this.smiley.Image = imgList1.Images[2];
                                DialogResult v = MessageBox.Show("Vous avez perdu ! \n Recommencer ?", "Partie perdue", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                                if (v == DialogResult.Yes)
                                {
                                    stopWatch.Reset();
                                    this.damier.Dispose();
                                    this.labelMine.Dispose();
                                    this.labelTime.Dispose();
                                    this.smiley.Dispose();
                                    this.InitializeComponent();
                                }
                                else
                                {
                                    this.Dispose();
                                }
                            }
                            else
                            {
                                int nbMineFound = exploreNeighbourCase(x, y);
                                //Si elle n'en contient pas on explore les voisin
                                btn.Text = nbMineFound.ToString();
                                if (nbMineFound == 0)
                                {
                                    //On ajoute les noms des voisins dans une liste
                                    List<string> listNeighbourgButton = new List<string>();
                                    int aX = x - 1;
                                    int aY = y - 1;
                                    listNeighbourgButton.Add(string.Format("{0}|{1}", aX, aY));
                                    aX = x;
                                    aY = y - 1;
                                    listNeighbourgButton.Add(string.Format("{0}|{1}", aX, aY));
                                    aX = x + 1;
                                    aY = y - 1;
                                    listNeighbourgButton.Add(string.Format("{0}|{1}", aX, aY));
                                    aX = x + 1;
                                    aY = y;
                                    listNeighbourgButton.Add(string.Format("{0}|{1}", aX, aY));
                                    aX = x + 1;
                                    aY = y + 1;
                                    listNeighbourgButton.Add(string.Format("{0}|{1}", aX, aY));
                                    aX = x;
                                    aY = y + 1;
                                    listNeighbourgButton.Add(string.Format("{0}|{1}", aX, aY));
                                    aX = x - 1;
                                    aY = y + 1;
                                    listNeighbourgButton.Add(string.Format("{0}|{1}", aX, aY));
                                    aX = x - 1;
                                    aY = y;
                                    listNeighbourgButton.Add(string.Format("{0}|{1}", aX, aY));
                                    foreach (object o in this.damier.Controls)
                                    {
                                        if (o is Button)
                                        {
                                            Button btna = (Button)o;
                                            if (listNeighbourgButton.Contains(btna.Name))
                                            {
                                                if (btna.BackColor != Color.GreenYellow)//Si le boutton est pas deja resolvé
                                                {
                                                    btna.Font = new Font("Georgia", 8);
                                                    //On recupere les X et Y du boutton incriminé
                                                    int buttonX = int.Parse(btna.Name.Substring(0, btna.Name.IndexOf('|')));
                                                    int buttonY = int.Parse(btna.Name.Substring(btna.Name.IndexOf('|') + 1, btna.Name.Length - (btna.Name.IndexOf('|') + 1)));
                                                    int nba = exploreNeighbourCase(buttonX, buttonY);
                                                    Console.WriteLine(buttonX + " " + buttonY);
                                                    btna.Text = nba.ToString();
                                                    btna.BackColor = Color.GreenYellow;
                                                    this.MyButtonClickPanel(btna, e);//On rapelles la methode
                                                }
                                            }
                                        }
                                    }
                                }
                                btn.BackColor = Color.GreenYellow;
                            }
                    }
            }
            else
            {
                //Si case pas encore decouverte
                if (btn.Text == "")
                {
                    if (btn.BackColor == Color.Red)
                    {
                        btn.BackColor = Color.Goldenrod;
                        gameNbMine = int.Parse(labelMine.Text);
                        gameNbMine++;
                        labelMine.Text = gameNbMine.ToString();
                    }
                    else
                    {   
                        gameNbMine = int.Parse(labelMine.Text);
                        if (gameNbMine > 0)
                        {
                            btn.BackColor = Color.Red;
                            gameNbMine--;
                            labelMine.Text = gameNbMine.ToString();
                        }
                    }
                }
            }
            if (this.winPartie())
            {
                this.smiley.Image = imgList1.Images[0];
                //Arret du timer
                stopWatch.Stop();
                //Bruit de mine qui explose
                String myPath = "..\\..\\ressources\\3672.wav";
                System.Media.SoundPlayer player = new SoundPlayer();
                player.SoundLocation = myPath;
                player.LoadAsync();
                player.Play();   //asynchronous (loop)playing in new thread
               
                //On recupere tous les bouttons qui ont pas été découvert
                foreach (object o in this.damier.Controls)
                {
                    Button btn1 = new Button();
                    if (o is Button)
                    {
                        btn1 = (Button)o;
                        //On recupere les bouttons de type mines
                        if (btn1.BackColor == Color.Red || btn1.BackColor == Color.Goldenrod)
                        {
                            //On récupère les coordonés des boutons
                            String name = btn1.Name;
                            x = int.Parse(name.Substring(0, name.IndexOf('|')));
                            y = int.Parse(name.Substring(name.IndexOf('|') + 1, name.Length - (name.IndexOf('|') + 1)));
                            //On les resolve pour faire apparaitre les mines
                            Image img = System.Drawing.Image.FromFile(@"../../ressources/mine.jpg");
                            img = resizeImage(img, new Size(20, 20));
                            btn1.BackgroundImage = img;
                        }
                    }
                }
              //  MessageBox.Show("Félicitations : Partie gagnée en " + labelTime.Text + " minutes !", "Partie Gagnée", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                DialogResult v = MessageBox.Show("Félicitations : Partie gagnée en " + labelTime.Text + "\n Recommencer ?", "Partie gagnée", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (v == DialogResult.Yes)
                {
                    stopWatch.Reset();
                    this.damier.Dispose();
                    this.labelMine.Dispose();
                    this.labelTime.Dispose();
                    this.smiley.Dispose();
                    this.InitializeComponent();
                }
                else
                {
                    this.Dispose();
                }
                stopWatch.Reset();

            }
        }
        protected void MyButtonClick(object who, EventArgs e)
        {
            int width = 0;
            int height = 0;
            int nbmines = 0;
            Button btn = (Button)who;
            if (btn.Text == "Facile")
            {
                width = height = 9;
                nbmines = 10;
                niveauJoue = 1;
            }
            if (btn.Text == "Moyen")
            {
                width = height = 16;
                nbmines = 40;
                niveauJoue = 2;
            }
            if (btn.Text == "Difficile")
            {
                width = 16;
                height = 30;
                nbmines = 99;
                niveauJoue = 3;
            }
            this.startGame(width, height, nbmines);
        }
        //Retourne le nombre de mines dans les cases adjacentes 
        protected int exploreNeighbourCase(int x, int y)
        {
            int nb = 0;
            //On resoud toutes les cases voisines 
            if (resolveCase(x - 1, y - 1) == -1)
                nb++;
            if (resolveCase(x , y - 1) == -1)
                nb++;
            if (resolveCase(x + 1, y - 1) == -1)
                nb++;
            if (resolveCase(x + 1, y) == -1)
                nb++;
            if (resolveCase(x + 1, y + 1) == -1)
                nb++;
            if (resolveCase(x, y + 1) == -1)
                nb++;
            if (resolveCase(x - 1, y + 1) == -1)
                nb++;
            if (resolveCase(x - 1, y) == -1)
                nb++;
            return nb;
        }
        protected int resolveCase(int x, int y)
        {
            int clickedCase = -2;
            int Width = 0;
            int Height = 0;
            //Recupere le niveau de jeu
            //Test si la case existe bien dans le plateau de jeu
            if(niveauJoue == 1)
                Width = Height = 9;
            if(niveauJoue == 2)
                Width = Height = 16;
            if (niveauJoue == 3)
            {
                Height = 30;
                Width = 16;
            }
            if (Width > x && x>=0 && y >=0 && Height > y)
            {
                //Si la case contient une mine
                if (mineArray[x][y] == 1)
                {
                    clickedCase = -1;
                }
                else
                {
                    //Si pas de mine
                    clickedCase = 0;
                }
            }
            return clickedCase;
        }
        protected void startGame(int width, int height, int nbmines)
        {

            //Création des variables 
            String title = "Mon démineur - ";
            if (10 == nbmines)
                title += "Débutant : 10 mines";
            if (40 == nbmines)
                title += "Moyen : 40 mines";
            if (99 == nbmines)
                title += "Difficile : 99 mines";
            //On néttoies le panel
            this.Controls.Clear();
            //On set le titre du panel avec la difficulté de la partie
            this.Text = title;
            this.initialiseGamePanel(width, height, nbmines);
            mineArray = disposeMines(width, height, nbmines);
        }
        protected int[][] disposeMines(int width, int height, int nbmines)
        {
            //Création du tableau
            int[][] gameArray = new int[width][];
            int x = 0;
            int y = 0;
            Random randNum = new Random();
            for (int i = 0; i < width; i++)
            {
                gameArray[i] = new int[height];
            }
            //Initialisation du démineur
            for (int i = 0; i < width; i++)
                for (int j = 0; j < height; j++)
                    gameArray[i][j] = 0;

            while (nbmines > 0)
            {
                x = randNum.Next(width);
                y = randNum.Next(height);
                //verifie que la case comporte pas deja une mine
                if (gameArray[x][y] == 0)
                {
                    gameArray[x][y] = 1;
                    nbmines--;
                    //MessageBox.Show("mine"+x+" "+y, "titre du message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            return gameArray;
        }
        protected void initialiseGamePanel(int width, int height, int nbmines)
        {
                      
            int panelWidth = 0;
            int panelHeight = 0;
            int buttonLocationX = 0;
            int buttonLocationY = 0;
            this.components = new System.ComponentModel.Container();
            //Création du menu de jeu
            this.menu = new System.Windows.Forms.MenuStrip();
            //this.Controls.Add(menu);
            //Liste des smileys
            this.imgList1 = new System.Windows.Forms.ImageList(this.components);
            imgList1.Images.Add(Image.FromFile("..\\..\\ressources\\happySmiley.jpg"));
            imgList1.Images.Add(Image.FromFile("..\\..\\ressources\\anxiousSmiley.jpg"));
            imgList1.Images.Add(Image.FromFile("..\\..\\ressources\\destroyedSmiley.jpg"));
            imgList1.Images.SetKeyName(0, "happySmiley.jpg");
            imgList1.Images.SetKeyName(1, "anxiousSmiley.jpg");
            imgList1.Images.SetKeyName(2, "destroyedSmiley.jpg");
            imgList1.ImageSize = new Size(50, 50);
            Graphics theGraphics = Graphics.FromHwnd(this.Handle);

            //Damier de jeu
            this.damier = new System.Windows.Forms.Panel();
            this.damier.Size= new System.Drawing.Size(width*20, height*20);
            this.damier.BackColor = Color.Goldenrod;
            this.damier.Location = new System.Drawing.Point(0, 50);
            //Algorythme de création des bouttons
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    this.b = new System.Windows.Forms.Button();
                    b.Size = new Size(20, 20);
                    this.b.Padding = new Padding(1, -5, 1, 1);
                    this.b.Location = new System.Drawing.Point(buttonLocationX, buttonLocationY);
                    this.b.Name = i + "|" + j;
                    this.b.MouseDown += (s, e2) =>
                    {
                        if (e2.Button == System.Windows.Forms.MouseButtons.Left)
                        {
                            //Si case marqué mine on fait rien (click droit)
                            Button b2 = (Button)s;
                            //Smiley grimace que si case non decouvertes
                            if (b2.BackColor != Color.GreenYellow && b2.BackColor != Color.Red)
                            {
                                this.smiley.Image = imgList1.Images[1];
                            }
                        }
                    };
                    this.b.MouseUp += (s, e2) =>
                    {
                        this.smiley.Image = imgList1.Images[0];
                    };
                    this.b.MouseDown += new MouseEventHandler(MyButtonClickPanel);
                    this.damier.Controls.Add(this.b);
                    buttonLocationY += 20;
                }
                buttonLocationX += 20;
                buttonLocationY = 0;
            }
            
            this.Controls.Add(damier);
            this.damier.SuspendLayout();
            //refresh taille ecran
            this.ClientSize=new System.Drawing.Size(width*20, height*20+50);
            //Recupere la taille du panel
            panelWidth = width * 20;
            panelHeight = height * 20 + 50;
            //Smiley en cours
            this.smiley = new System.Windows.Forms.PictureBox();
            this.smiley.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.smiley.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.smiley.Location = new System.Drawing.Point((panelWidth/2)-25, 0);
            this.smiley.Name = "smiley";
            this.smiley.Size = new System.Drawing.Size(50, 50);
            this.smiley.Image = imgList1.Images[0];
            this.smiley.TabIndex = 0;
            this.smiley.TabStop = false;
            this.Controls.Add(smiley);
            //Positionement du label NBMines
            this.labelMine = new System.Windows.Forms.Label();
            this.labelMine.Size = new System.Drawing.Size(panelWidth/2, 48);
            this.labelMine.Location = new System.Drawing.Point(0, 0);
            int minePadding = 0;
            if (niveauJoue == 1)
                minePadding = 10;
            else
                minePadding = 45;
            this.labelMine.Padding = new Padding(minePadding, 9, minePadding, 9);
            this.labelMine.Font = new Font("Georgia", 18);
            this.labelMine.Text = nbmines.ToString();
            this.labelMine.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(labelMine);
            //Positionnement du label chronometre
            this.labelTime = new System.Windows.Forms.Label();
            this.labelTime.Size = new System.Drawing.Size((panelWidth/2)-25, 48);
            this.labelTime.Location = new System.Drawing.Point((panelWidth/2)+25, 0);
            int timePadding = 0;
            if (niveauJoue == 1)
                timePadding = 5;
            else
                timePadding = 35;
            this.labelTime.Padding = new Padding(timePadding, 13, timePadding, 13);
            this.labelTime.Font = new Font("Georgia", 12);
            this.labelTime.Text = "00:00";
            this.labelTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(labelTime);
            //Empeche le resise de fenetre
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false; 
        }
        private void InitializeComponent()
        {
            this.title = new System.Windows.Forms.Label();
            this.subtitle = new System.Windows.Forms.Label();
            this.easyButton = new System.Windows.Forms.Button();
            this.mediumButton = new System.Windows.Forms.Button();
            this.hardButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // title
            // 
            this.title.AutoSize = true;
            this.title.Location = new System.Drawing.Point(150, 10);
            this.title.Name = "title";
            this.title.Size = new System.Drawing.Size(296, 17);
            this.title.TabIndex = 0;
            this.title.Text = "Bienvenue dans mon application de démineur";
            // 
            // subtitle
            // 
            this.subtitle.AutoSize = true;
            this.subtitle.Location = new System.Drawing.Point(100, 41);
            this.subtitle.Name = "subtitle";
            this.subtitle.Size = new System.Drawing.Size(412, 17);
            this.subtitle.TabIndex = 1;
            this.subtitle.Text = "Pour commencer une partie, séléctionnez un niveau de difficulté";
            // 
            // easyButton
            // 
            this.easyButton.Location = new System.Drawing.Point(200, 77);
            this.easyButton.Name = "easyButton";
            this.easyButton.Size = new System.Drawing.Size(200, 50);
            this.easyButton.TabIndex = 2;
            this.easyButton.Text = "Facile";
            this.easyButton.UseVisualStyleBackColor = true;
            this.easyButton.Click += new System.EventHandler(this.MyButtonClick);
            // 
            // mediumButton
            // 
            this.mediumButton.Location = new System.Drawing.Point(200, 142);
            this.mediumButton.Name = "mediumButton";
            this.mediumButton.Size = new System.Drawing.Size(200, 50);
            this.mediumButton.TabIndex = 3;
            this.mediumButton.Text = "Moyen";
            this.mediumButton.UseVisualStyleBackColor = true;
            this.mediumButton.Click += new System.EventHandler(this.MyButtonClick);
            // 
            // hardButton
            // 
            this.hardButton.Location = new System.Drawing.Point(200, 205);
            this.hardButton.Name = "hardButton";
            this.hardButton.Size = new System.Drawing.Size(200, 50);
            this.hardButton.TabIndex = 4;
            this.hardButton.Text = "Difficile";
            this.hardButton.UseVisualStyleBackColor = true;
            this.hardButton.Click += new System.EventHandler(this.MyButtonClick);
            // 
            // MenuDemineur
            // 
            this.ClientSize = new System.Drawing.Size(600, 282);
            this.Controls.Add(this.hardButton);
            this.Controls.Add(this.mediumButton);
            this.Controls.Add(this.easyButton);
            this.Controls.Add(this.subtitle);
            this.Controls.Add(this.title);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MenuDemineur";
            this.Text = "Mon démineur";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

    }
}
