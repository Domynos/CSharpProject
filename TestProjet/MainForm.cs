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
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			this.Load += MainForm_Load;
		}
		
		private void MainForm_Load(object sender, EventArgs e){			
			GenererPlateau(Controls,6);
		}
		
		static void GenererPlateau(Control.ControlCollection controls,int caseParLigne){
			Plateau plateau = new Plateau(caseParLigne);
			plateau.MelangerPlateau();
			Carte[,] cartes = plateau.plateauCartes;
			int i,y;
			for(i=0;i<cartes.GetLength(0);i++)
				for(y=0;y<cartes.GetLength(0);y++)
				{
					PictureBox pb = cartes[i,y].imageCarte();
					pb.Location = new Point(i*32, y*32);
					controls.Add(pb);
				}
		}
	}
}
