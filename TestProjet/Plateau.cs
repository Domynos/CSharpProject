/*
* Created by SharpDevelop.
* User: darty
* Date: 22/10/2014
* Time: 14:50
* 
* To change this template use Tools | Options | Coding | Edit Standard Headers.
*/
using System;

namespace TestProjet
{
	/// <summary>
	/// Description of Palteau.
	/// </summary>
	public class Plateau
	{
		public Carte[,] plateauCartes {get; set;}
		private int caseParLigne;
		
		public Plateau(){}
		
		public Plateau(int _caseParLigne)
		{
			int i,j;
			plateauCartes = new Carte[_caseParLigne, _caseParLigne];
			
			if(_caseParLigne % 2 == 0 && _caseParLigne < 7){
				caseParLigne = _caseParLigne;
				Boolean next = false;
				int y = 0;
				for(i=0;i<caseParLigne;i++)
					for(j=0;j<caseParLigne;j++)
					{
						if(!next){
							plateauCartes[i,j] = new Carte(y);
							next = true;
						}
						else{
							plateauCartes[i,j] = new Carte(y);
							next = false;
							y++;
						}
					}
			}
		}
		
		public void MelangerPlateau(){
			int a,b;
			int i,y;
			
			Carte[,] cartes = plateauCartes;
			plateauCartes = new Carte[caseParLigne,caseParLigne];
			for(a=0;a<caseParLigne;a++)
				for(b=0;b<caseParLigne;b++)
					plateauCartes[a,b] = null;
			
			Random rnd = new Random();
			for(a=0;a<caseParLigne;a++)
				for(b=0;b<caseParLigne;b++){
					i = rnd.Next(0,caseParLigne);
					y = rnd.Next(0,caseParLigne);
						
					if(plateauCartes[i,y] == null)
						plateauCartes[i,y] = cartes[a,b];
					else
						b--;
				}				
		}
	}
}
