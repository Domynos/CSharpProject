/*
 * Created by SharpDevelop.
 * User: darty
 * Date: 26/10/2014
 * Time: 21:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
using System.Linq;

namespace TestProjet
{
	/// <summary>
	/// Description of Player.
	/// </summary>
	public class Player
	{
		bool isIA;
		public int nombreDePionsGagne, i = 1, y = 3;
        public bool myTurn = false;
		
		
		public Player(bool _isIA)
		{
			isIA = _isIA;
		}
		
		public void Jouer(JeuDeDamesForm form, Damier damier){
            myTurn = true;
			if(isIA){

                CaseDamier caseDamier = damier.casesDamier[i, y];
                var pion = (Pion)null;
                try
                {
                    pion = caseDamier.Controls.OfType<Pion>().First();
                }
                catch (InvalidOperationException e)
                {
                    System.Diagnostics.Debug.WriteLine(e.ToString());
                }
                if(pion != null){
                    form.pion_Click(pion,null);
                    form.caseDamier_Click(damier.casesDamier[i+1,y+1],null);
                    i++;
                    y++;
                    
                }
			}else{
				
			}
		}
	}
}
