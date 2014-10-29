/*
 * Created by SharpDevelop.
 * User: darty
 * Date: 24/10/2014
 * Time: 11:39
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Drawing;

namespace TestProjet
{
	/// <summary>
	/// Description of Pion.
	/// </summary>
	public class Pion : PictureBox
	{
		public int couleurPion;
		
		public Pion(int _playerNumber)
		{
			couleurPion = _playerNumber;
			Image image;
			if(couleurPion == 0)
				image = Image.FromFile(@"../../img/pionNoir.png");
			else
				image = Image.FromFile(@"../../img/pionBlanc.png");
				
			this.Image = image;
			this.Height = image.Height;
			this.Width = image.Width;
			this.BackColor = Color.Transparent;
		}
	}
}
