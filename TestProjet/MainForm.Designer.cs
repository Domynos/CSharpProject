/*
 * Created by SharpDevelop.
 * User: darty
 * Date: 22/10/2014
 * Time: 14:47
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace TestProjet
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
            this.label1 = new System.Windows.Forms.Label();
            this.bouton_jeuDoublons = new System.Windows.Forms.Button();
            this.bouton_jeuDames = new System.Windows.Forms.Button();
            this.boutonDemineur = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(92, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Choisissez votre jeu :";
            // 
            // bouton_jeuDoublons
            // 
            this.bouton_jeuDoublons.Location = new System.Drawing.Point(41, 63);
            this.bouton_jeuDoublons.Name = "bouton_jeuDoublons";
            this.bouton_jeuDoublons.Size = new System.Drawing.Size(202, 23);
            this.bouton_jeuDoublons.TabIndex = 1;
            this.bouton_jeuDoublons.Text = "Jeu des doublons";
            this.bouton_jeuDoublons.UseVisualStyleBackColor = true;
            this.bouton_jeuDoublons.Click += new System.EventHandler(this.BoutonJouerDoublons_Click);
            // 
            // bouton_jeuDames
            // 
            this.bouton_jeuDames.Location = new System.Drawing.Point(41, 119);
            this.bouton_jeuDames.Name = "bouton_jeuDames";
            this.bouton_jeuDames.Size = new System.Drawing.Size(202, 23);
            this.bouton_jeuDames.TabIndex = 2;
            this.bouton_jeuDames.Text = "Jeu de dames";
            this.bouton_jeuDames.UseVisualStyleBackColor = true;
            this.bouton_jeuDames.Click += new System.EventHandler(this.BoutonJouerDames_Click);
            // 
            // boutonDemineur
            // 
            this.boutonDemineur.Location = new System.Drawing.Point(41, 174);
            this.boutonDemineur.Name = "boutonDemineur";
            this.boutonDemineur.Size = new System.Drawing.Size(202, 23);
            this.boutonDemineur.TabIndex = 3;
            this.boutonDemineur.Text = "Démineur";
            this.boutonDemineur.UseVisualStyleBackColor = true;
            this.boutonDemineur.Click += new System.EventHandler(this.boutonDemineur_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 214);
            this.Controls.Add(this.boutonDemineur);
            this.Controls.Add(this.bouton_jeuDames);
            this.Controls.Add(this.bouton_jeuDoublons);
            this.Controls.Add(this.label1);
            this.Name = "MainForm";
            this.Text = "TestProjet";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bouton_jeuDoublons;
        private System.Windows.Forms.Button bouton_jeuDames;
        private System.Windows.Forms.Button boutonDemineur;
	}
}
