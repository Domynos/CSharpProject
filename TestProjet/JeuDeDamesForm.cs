/*
 * Created by SharpDevelop.
 * User: darty
 * Date: 24/10/2014
 * Time: 11:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;


namespace TestProjet
{
	/// <summary>
	/// Description of JeuDeDames.
	/// </summary>
	public partial class JeuDeDamesForm : Form
	{
		Damier damier;
		Pion pionEnCours;
		CaseDamier caseCible;
        Player user,iA;
        bool isPionEnCours = false;
        int compteurTours = 0;
	
		public JeuDeDamesForm()
		{
			InitializeComponent();
			this.Load += JeuDeDamesForm_Load;
            Application.Idle += HandleApplicationIdle;
			this.AutoSize = true;
            GenererDamier(Controls);
            this.CenterToScreen();
		}

        private void JeuDeDamesForm_Load(object sender, EventArgs e)
        {
            user = new Player(false);
            iA = new Player(true);
            HandleApplicationIdle(null,null);

        }

        private void HandleApplicationIdle(object sender, EventArgs e){
            if (compteurTours % 2 != 0)
            {
                user.myTurn = false;
                BloquerDamier(true);
                iA.Jouer(this, damier);
                iA.myTurn = false;
            }
            else if(compteurTours % 2 == 0)
            {
                System.Diagnostics.Debug.WriteLine("user Joue");
                user.Jouer(this,damier);
                BloquerDamier(false);
            }
        }

        private void BloquerDamier(bool wantBlock)
        {
            if(wantBlock)
                this.SetStyle(ControlStyles.StandardClick, false);
            else
                this.SetStyle(ControlStyles.StandardClick, true);   
        }
		
		private void GenererDamier(Control.ControlCollection controls){
			damier = new Damier();
			CaseDamier[,] cases = damier.casesDamier;
			int i,y;
			for(i=0;i<cases.GetLength(0);i++)
				for(y=0;y<cases.GetLength(0);y++){
					Pion pion = null;
					if(i%2 == 0 && y%2==0){
						if(y<4)
							pion = new Pion(1);
						else if(y>5)
							pion = new Pion(0);
					}
					else if(i%2 !=0 && y%2 != 0){
						if(y<4)
							pion = new Pion(1);
						else if(y>5)
							pion = new Pion(0);
						
					}
					
					if(pion != null){
						pion.MouseClick += new MouseEventHandler(pion_Click);
                        pion.Location = new Point(0, 0);
						cases[i,y].Controls.Add(pion);
					}
					
					cases[i,y].Location = new Point(i*cases[i,y].Width, y*cases[i,y].Height);
					cases[i,y].MouseClick += new MouseEventHandler(caseDamier_Click);
					controls.Add(cases[i,y]);
				}
		}
		
		public void pion_Click(object sender,EventArgs e){
			pionEnCours = (Pion)sender;
            if (pionEnCours.couleurPion == 0 && user.myTurn)
            {
                damier.RestaurerBordures();
                isPionEnCours = true;
                BorderPossibilities();
            }
            else if (iA.myTurn)
                isPionEnCours = true;
            else
                pionEnCours = null;
		}
		
		public void caseDamier_Click(object sender, EventArgs e){
			caseCible = (CaseDamier) sender;
            if (isPionEnCours && caseCible.couleurCase == 1){
                damier.RestaurerBordures();
			    int cc_positionX = caseCible.Location.X/45;
			    int cc_positionY = caseCible.Location.Y/45;
                int pec_positionX = pionEnCours.Parent.Location.X/45;
                int pec_positionY = pionEnCours.Parent.Location.Y/45;
                int rapportX = (cc_positionX - pec_positionX);
                int rapportY = (cc_positionY - pec_positionY);
                int pRapportX = rapportX, pRapportY = rapportY;
                if (rapportX < 0) pRapportX *= -1;
                if (rapportY < 0) pRapportY *= -1;

                if (pRapportX == 1 && ((user.myTurn && rapportY < 0) || (iA.myTurn && rapportY > 0)))
                {
                    caseCible.Controls.Add(pionEnCours);
                    compteurTours++;
                }
                else if (pRapportX == 2 && pRapportY == 2)
                {
                    if (CheckMiddleLocation(damier, pionEnCours, rapportX, rapportY, pec_positionX, pec_positionY))
                    {
                        RemoveMiddleLocation(damier, pionEnCours, rapportX, rapportY, pec_positionX, pec_positionY);
                        caseCible.Controls.Add(pionEnCours);
                        compteurTours++;
                    }
                }
                isPionEnCours = false;
			}
		}

        static bool CheckMiddleLocation(Damier damier, Pion pionEnCours, int rapportX, int rapportY, int fromX, int fromY) {
            if(!damier.casesDamier[fromX + rapportX, fromY + rapportY].HaveChild()){
                if (rapportX < 0) rapportX += 1; else rapportX -= 1;
                if (rapportY < 0) rapportY += 1; else rapportY -= 1;
     
                var pion = (Pion) null;
                try
                {
                    pion = damier.casesDamier[fromX + rapportX, fromY + rapportY].Controls.OfType<Pion>().First();
                }
                catch (InvalidOperationException)
                {}

                if (pion != null && pion.couleurPion != pionEnCours.couleurPion)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        static bool RemoveMiddleLocation(Damier damier, Pion pionEnCours, int rapportX, int rapportY, int fromX, int fromY) {
            if (rapportX < 0) rapportX += 1; else rapportX -= 1;
            if (rapportY < 0) rapportY += 1; else rapportY -= 1;
     
            var pion = (Pion) null;
            try
            {
                pion = damier.casesDamier[fromX + rapportX, fromY + rapportY].Controls.OfType<Pion>().First();
            }
            catch (InvalidOperationException e)
            {
                System.Diagnostics.Debug.WriteLine(e.ToString());
            }

            if (pion != null && pion.couleurPion != pionEnCours.couleurPion)
            {
                damier.casesDamier[fromX + rapportX, fromY + rapportY].Controls.Remove(pion);
                return true;
            }
            else
                return false;

        }
	    
        private void BorderPossibilities(){
            if (isPionEnCours)
            {
                try
                {
                    CaseDamier caseDamier = (CaseDamier)pionEnCours.Parent;
                    if (!damier.casesDamier[caseDamier.Location.X / 45 + 1, caseDamier.Location.Y / 45 - 1].HaveChild())
                        damier.casesDamier[caseDamier.Location.X / 45 + 1, caseDamier.Location.Y / 45 - 1].BackColor = Color.Red;
                    if (!damier.casesDamier[caseDamier.Location.X / 45 - 1, caseDamier.Location.Y / 45 - 1].HaveChild())
                        damier.casesDamier[caseDamier.Location.X / 45 - 1, caseDamier.Location.Y / 45 - 1].BackColor = Color.Red;

                    if (CheckMiddleLocation(damier, pionEnCours, -2, -2, caseDamier.Location.X / 45, caseDamier.Location.Y / 45))
                        damier.casesDamier[caseDamier.Location.X / 45 - 2, caseDamier.Location.Y / 45 - 2].BackColor = Color.Red;
                    if (CheckMiddleLocation(damier, pionEnCours, 2, 2, caseDamier.Location.X / 45, caseDamier.Location.Y / 45))
                        damier.casesDamier[caseDamier.Location.X / 45 + 2, caseDamier.Location.Y / 45 + 2].BackColor = Color.Red;
                    if (CheckMiddleLocation(damier, pionEnCours, -2, 2, caseDamier.Location.X / 45, caseDamier.Location.Y / 45))
                        damier.casesDamier[caseDamier.Location.X / 45 - 2, caseDamier.Location.Y / 45 + 2].BackColor = Color.Red;
                    if (CheckMiddleLocation(damier, pionEnCours, 2, -2, caseDamier.Location.X / 45, caseDamier.Location.Y / 45))
                        damier.casesDamier[caseDamier.Location.X / 45 + 2, caseDamier.Location.Y / 45 - 2].BackColor = Color.Red;
                }
                catch (NullReferenceException) { }
                catch (IndexOutOfRangeException) { }
            }
        }
    }
}
