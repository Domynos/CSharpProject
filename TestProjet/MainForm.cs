/*
* Created by SharpDevelop.
* User: darty
* Date: 22/10/2014
* Time: 14:47
* 
* To change this template use Tools | Options | Coding | Edit Standard Headers.
*/
using System;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace TestProjet
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			this.Load += MainForm_Load;
            this.CenterToScreen();
		}
		
		private void MainForm_Load(object sender, EventArgs e){			
			
			//this.Hide();
		}
		
		private void BoutonJouerDoublons_Click(object sender, EventArgs e){
			JeuDoublonsForm jdForm = new JeuDoublonsForm();
			jdForm.Show();
		}
		
		private void BoutonJouerDames_Click(object sender, EventArgs e){
			JeuDeDamesForm jddForm = new JeuDeDamesForm();
			jddForm.Show();
		}

        private void boutonDemineur_Click(object sender, EventArgs e)
        {
            MenuDemineur demineurForm = new MenuDemineur();
            demineurForm.Show();
        }
		
	}
}
