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
	public class Carte
	{
		int numeroDeCarte;
		
		public Carte(int _numeroDeCarte){
			if(numeroDeCarte > 6 || numeroDeCarte < 0)
				numeroDeCarte = 0;
			else
				numeroDeCarte = _numeroDeCarte;
		}
		
		public PictureBox imageCarte(){
			string[] filePaths = Directory.GetFiles(@"C:\Users\darty\Documents\SharpDevelop Projects\FadelCours\TestProjet\img");
			Image image = Image.FromFile(filePaths[numeroDeCarte]);
			PictureBox pictureBox = new PictureBox();
			pictureBox.Image = image;
			pictureBox.Height = image.Height;
			pictureBox.Width = image.Width;
			return pictureBox;
		}
	}
}
