/*
 * Created by SharpDevelop.
 * User: darty
 * Date: 22/10/2014
 * Time: 15:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;

namespace TestProjet
{
	public class Melangeur : IComparer {
	   private static Random rnd;
	      static Melangeur() {
	         rnd = new Random();
	   }
	
	   public int Compare(object x, object y) {
	      if (x.Equals(y))
	         return 0;
	      else {
	         return Melangeur.rnd.Next(-1, 1);
	      }
	   }   
	}
}
