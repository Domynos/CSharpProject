/*
 * Created by SharpDevelop.
 * User: darty
 * Date: 24/10/2014
 * Time: 11:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;

namespace TestProjet
{
	/// <summary>
	/// Description of CaseDamier.
	/// </summary>
	public class CaseDamier : PictureBox
	{
		public int couleurCase;
		
		public CaseDamier(int _couleurCase)
		{
			couleurCase = _couleurCase;
			if(couleurCase == 0)
				this.BackColor = Color.FromArgb(255, 255, 255);
			else
				this.BackColor = Color.FromArgb(0, 0, 0);
			this.Height = 45;
			this.Width = 45;
		}

        public bool HaveChild()
        {
            var pion = (Pion)null;
            try
            {
                pion = this.Controls.OfType<Pion>().First();
            }
            catch (InvalidOperationException)
            { }

            if (pion != null)
                return true;
            else
                return false;
        }
	}
}
