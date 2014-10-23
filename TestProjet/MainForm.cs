/*
 * Created by SharpDevelop.
 * User: darty
 * Date: 22/10/2014
 * Time: 14:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace TestProjet
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		static Carte carteEnCours;
		static bool isCarteEnCours = false;

		public MainForm()
		{
			InitializeComponent();
			this.Load += MainForm_Load;
		}
		
		private void MainForm_Load(object sender, EventArgs e){			
			GenererPlateau(Controls,4);
		}
		
		static void GenererPlateau(Control.ControlCollection controls,int caseParLigne){
			Plateau plateau = new Plateau(caseParLigne);
			plateau.MelangerPlateau();
			Carte[,] cartes = plateau.plateauCartes;
			int i,y;
			for(i=0;i<cartes.GetLength(0);i++)
				for(y=0;y<cartes.GetLength(0);y++)
				{
					cartes[i,y].Location = new Point(i*32, y*32);
					cartes[i,y].MouseClick += new MouseEventHandler(carte_Click);
					controls.Add(cartes[i,y]);
				}
		}

		static void carte_Click(object sender, EventArgs e)
		{
			Carte maCarte = (Carte)sender;
			System.Diagnostics.Debug.WriteLine ("Clické, numero de carte : "+maCarte.numeroDeCarte);
			if (!isCarteEnCours) {
				carteEnCours = maCarte;
				isCarteEnCours = true;
			}
			else {
				if (carteEnCours.numeroDeCarte == maCarte.numeroDeCarte) {
					maCarte.Visible = false;
					carteEnCours.Visible = false;
					isCarteEnCours = false;
				} else
					isCarteEnCours = false;
			}
		}
	}
}
