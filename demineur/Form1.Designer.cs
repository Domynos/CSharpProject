namespace demineur
{
    partial class MainWindow
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private System.Windows.Forms.Label title;
        private System.Windows.Forms.Label subtitle;
        private System.Windows.Forms.Button easyButton;
        private System.Windows.Forms.Button mediumButton;
        private System.Windows.Forms.Button hardButton;
        private System.Windows.Forms.Button b;
        private System.Windows.Forms.Panel damier;
        private System.Windows.Forms.PictureBox smiley;
        private System.Windows.Forms.ImageList imgList1;
        private System.Windows.Forms.Label labelMine;
        private System.Windows.Forms.Label labelTime;
        private System.Windows.Forms.MenuStrip menu;
    }
}

