/*
* Created by SharpDevelop.
* User: darty
* Date: 22/10/2014
* Time: 14:47
* 
* To change this template use Tools | Options | Coding | Edit Standard Headers.
*/
using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TestProjet
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class JeuDoublonsForm : Form
	{
		static Carte carteEnCours;
		static bool isCarteEnCours = false;
		static int compteurDeDoublonsTrouve = 0;
		static int compteurNombreEssaies = 0;
		static Label labelScore = new Label();
		static Label labelNombreEssaies = new Label();
		static Button boutonMelanger = new Button();
		static Button boutonVoir = new Button();
		static Plateau plateau;
		
		public JeuDoublonsForm()
		{
			InitializeComponent();
			this.Load += JeuDoublonsForm_Load;
			
			this.AutoSize = true;
    		this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			
			labelScore.Text = "Score : "+compteurDeDoublonsTrouve;
			labelScore.Location = new System.Drawing.Point(150,0);
			labelScore.AutoSize = true;
			labelScore.TextAlign = ContentAlignment.TopRight;
			
			labelNombreEssaies.AutoSize = true;
			labelNombreEssaies.Text = "Nombre d'essais : "+compteurNombreEssaies;
			labelNombreEssaies.TextAlign = ContentAlignment.TopRight;
			
			boutonMelanger.Text = "Melanger";
			boutonMelanger.Size = new System.Drawing.Size (72, 25);
			boutonMelanger.Location = new System.Drawing.Point (0,22);
			boutonMelanger.Click += new System.EventHandler(BoutonMelanger_Click);
			
			boutonVoir.Text = "Voir";
			boutonVoir.Size = new System.Drawing.Size (72, 25);
			boutonVoir.Location = new System.Drawing.Point (125,22);
			boutonVoir.Click += new System.EventHandler(BoutonVoir_Click);
			
			Controls.Add(labelScore);
			Controls.Add(labelNombreEssaies);
			Controls.Add(boutonMelanger);
			Controls.Add(boutonVoir);
			//Mouse.SetPosition(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2);
		}
		
		private void JeuDoublonsForm_Load(object sender, EventArgs e){			
			GenererPlateau(Controls,4);
		}
		
		private void JeuDoublonsForm_Update(object sender, EventArgs e){		
			System.Diagnostics.Debug.WriteLine("ici");
		}
		
		static void GenererPlateau(Control.ControlCollection controls,int caseParLigne){
			plateau = new Plateau(caseParLigne);
			plateau.MelangerPlateau();
			Carte[,] cartes = plateau.plateauCartes;
			int i,y;
			for(i=0;i<cartes.GetLength(0);i++)
				for(y=0;y<cartes.GetLength(0);y++)
				{
					cartes[i,y].Location = new Point(i*32+30, y*32+70);
					cartes[i,y].MouseClick += new MouseEventHandler(carte_Click);
					controls.Add(cartes[i,y]);
				}
		}
		
		void UpdatePlateau(){
			Carte[,] cartes = plateau.plateauCartes;
			int i,y;
			for(i=0;i<cartes.GetLength(0);i++)
				for(y=0;y<cartes.GetLength(0);y++)
				{
					cartes[i,y].Location = new Point(i*32+30, y*32+70);
					cartes[i,y].MouseClick += new MouseEventHandler(carte_Click);
					Controls.Add(cartes[i,y]);
				}
		}
		
		static void CacherPlateau(){
			Carte[,] cartes = plateau.plateauCartes;
			int i,y;
			for(i=0;i<cartes.GetLength(0);i++)
				for(y=0;y<cartes.GetLength(0);y++)
				{
					if(!cartes[i,y].isHide)
						cartes[i,y].RetournerCarte();
				}
		}
		
		void BoutonMelanger_Click(object sender, System.EventArgs e){
			plateau.MelangerPlateau();
			UpdatePlateau();
		}
		
		void BoutonVoir_Click(object sender, System.EventArgs e){
			Carte[,] cartes = plateau.plateauCartes;
			int i,y;
			for(i=0;i<cartes.GetLength(0);i++)
				for(y=0;y<cartes.GetLength(0);y++)
				{
					if(cartes[i,y].isHide && boutonVoir.Text == "Voir")
						cartes[i,y].RetournerCarte();
					else if(!cartes[i,y].isHide && boutonVoir.Text == "Masquer")
						cartes[i,y].RetournerCarte();
				}
			if(boutonVoir.Text == "Voir")
				boutonVoir.Text = "Masquer";
			else
				boutonVoir.Text = "Voir";
		}
			
		static async Task PutTaskDelay()
		{
			await Task.Delay(800);
		}
		
		static async void carte_Click(object sender, EventArgs e)
		{
			Carte maCarte = (Carte)sender;
			if (!isCarteEnCours && !maCarte.isDiscover && maCarte.isHide) {
				maCarte.RetournerCarte();
				carteEnCours = maCarte;
				isCarteEnCours = true;
			}
			else if(isCarteEnCours && maCarte.isHide){
				compteurNombreEssaies++;				
				if(!maCarte.isDiscover){
					maCarte.RetournerCarte();
				}
				
				if (carteEnCours.numeroDeCarte == maCarte.numeroDeCarte && carteEnCours.numeroDeSerie != maCarte.numeroDeSerie) {
					maCarte.isDiscover = true;
					carteEnCours.isDiscover = true;
					isCarteEnCours = false;
					compteurDeDoublonsTrouve++;
				} else if(!maCarte.isDiscover){
					await PutTaskDelay();
					carteEnCours.RetournerCarte();
					maCarte.RetournerCarte();
					isCarteEnCours = false;
				}
			}
			labelScore.Text = "Score : "+compteurDeDoublonsTrouve;
			labelNombreEssaies.Text = "Nombre d'essais : "+compteurNombreEssaies;
			
			if(compteurDeDoublonsTrouve == plateau.caseParLigne*2){
				MessageBox.Show("Vous avez gagné ! Nombre d'essaies : "+compteurNombreEssaies);
				CacherPlateau();
				plateau.MelangerPlateau();
			}
		}
		
		
	}
}
