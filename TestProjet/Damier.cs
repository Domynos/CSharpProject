/*
 * Created by SharpDevelop.
 * User: darty
 * Date: 24/10/2014
 * Time: 11:34
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;

namespace TestProjet
{
	/// <summary>
	/// Description of Class1.
	/// </summary>
	public class Damier
	{
		public CaseDamier[,] casesDamier;
		
		public Damier()
		{
			casesDamier = new CaseDamier[10,10];
			int i,y;
			for(i=0;i<casesDamier.GetLength(0);i++)
				for(y=0;y<casesDamier.GetLength(0);y++){
					if(i%2 == 0 && y%2!=0)
						casesDamier[i,y] = new CaseDamier(0);
					else if(i%2 !=0 && y%2 == 0)
						casesDamier[i,y] = new CaseDamier(0);
					else
						casesDamier[i,y] = new CaseDamier(1);
				}
		}

        public void RestaurerBordures()
        {
            int i,y;
			for(i=0;i<casesDamier.GetLength(0);i++)
                for (y = 0; y < casesDamier.GetLength(0); y++)
                {
                    if (casesDamier[i, y].BackColor == Color.Red)
                    {
                        casesDamier[i, y].BackColor = Color.Black;
                    }
                }
        }
	}
}
