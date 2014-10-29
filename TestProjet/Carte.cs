/*
 * Created by SharpDevelop.
 * User: darty
 * Date: 22/10/2014
 * Time: 14:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace TestProjet
{
	/// <summary>
	/// Description of Carte.
	/// </summary>
	public class Carte : PictureBox
	{
		public int numeroDeCarte;
		public int numeroDeSerie;
		public bool isHide = false;
		public bool isDiscover = false;
		
		public Carte(int _numeroDeCarte, int _numeroDeSerie){
			if(numeroDeCarte > 6 || numeroDeCarte < 0)
				numeroDeCarte = 0;
			else
				numeroDeCarte = _numeroDeCarte;
			
			numeroDeSerie = _numeroDeSerie;

			string[] filePaths = Directory.GetFiles(@"..\..\img_cartes");
			Image image = Image.FromFile(filePaths[numeroDeCarte]);
			this.Image = image;
			this.Height = image.Height;
			this.Width = image.Width;
			this.RetournerCarte();
		}
		
		public void RetournerCarte(){
			if(!isHide){
				Image image = Image.FromFile(@"..\..\img\hidden.png");
				this.Image = image;
				this.Height = image.Height;
				this.Width = image.Width;
				isHide = true;
			}
			else if(isHide){
				string[] filePaths = Directory.GetFiles(@"..\..\img_cartes");
				Image image = Image.FromFile(filePaths[numeroDeCarte]);
				this.Image = image;
				this.Height = image.Height;
				this.Width = image.Width;
				isHide = false;
			}
			
		}
	}
}
