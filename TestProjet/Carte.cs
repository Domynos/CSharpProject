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
		
		public Carte(int _numeroDeCarte){
			if(numeroDeCarte > 6 || numeroDeCarte < 0)
				numeroDeCarte = 0;
			else
				numeroDeCarte = _numeroDeCarte;

			string[] filePaths = Directory.GetFiles(@"/Users/lelongclement/Documents/C#_workspace/CSharpProject/TestProjet/img");
			Image image = Image.FromFile(filePaths[numeroDeCarte]);
			this.Image = image;
			this.Height = image.Height;
			this.Width = image.Width;
		}
	}
}
