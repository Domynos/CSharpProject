/*
 * Created by SharpDevelop.
 * User: darty
 * Date: 22/10/2014
 * Time: 12:05
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
namespace FadelCours{
	
	delegate void Trace (string message);
	
	class Meteo {
		double temperature = 13.5;
		
		public void PlusChaud(){
			temperature += 1.1;
			
			if(temperature > 37.2){
				if(TropChaud != null) TropChaud(String.Format("Trop Chaud T = {0}",temperature));
			}
		}
		
		public void PlusFroid(){
			temperature -= 0.9;
			
			if(temperature < -10){
				if(TropFroid != null) TropFroid(String.Format("Trop Froid T = {0}",temperature));
			}
		}
		
		public event Trace TropChaud;
		public event Trace TropFroid;
		
	}
	
	class Program{
			
		static void F (string s){
			Console.WriteLine("Dans la fonction F s = '{0:F2}'",s);
		}
		
		static void G (string s){
			Console.WriteLine("Dans la fonction G s = '{0:F2}'",s);
		}
		
		static void Invock (Trace t, string message){
			if(message != null) t(message);
		}
		
		static void Main(string[] args){
			/*Trace t = F;
			t+=G;
			t+=G;
			Invock(t,"coucou");*/
			
			var meteoParis = new Meteo();
			
			meteoParis.TropChaud +=F;
			meteoParis.TropFroid +=G;
			
			for(var n=0; n<26; n++) meteoParis.PlusChaud();
			for(var n=0; n<60; n++) meteoParis.PlusFroid();			
			
			Console.Read();
		}
	}
}
