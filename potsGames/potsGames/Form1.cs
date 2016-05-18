using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Media;
using potsGames.Properties;

namespace potsGames
{

	public partial class GameForm : Form
	{ 
		public GameForm()
		{
			InitializeComponent();
		}

		private void GameStarter(object sender, EventArgs e)//ok
		{
			SecondPots.Visible = false;
			SecondPots.Enabled = false;
			FirstPots.Visible = false;
			FirstPots.Enabled = false;
			mainBird.Visible = true;
			mainBird.Enabled = true;
			GamePersonSelected = false;
			if (GamePerson == 1)
				FirstPots.Location = FirstSelection;
			else SecondPots.Location = SecondSelection;
			StartGame();
		}

		private void OnTick(object sender, EventArgs e)//ok
		{
			this.Invalidate();
		}
		
		private void timer2_Tick(object sender, EventArgs e)//ok
		{
			InitialPipes();
		}
		

		private void GamePainter(object sender, PaintEventArgs e)//ok painting pipes
		{
			//DrawTest(e);
			DrawPipes(e);
			DrawSearchzone(e);
		}

		private void OnKeyDown(object sender, KeyEventArgs e) // ok
		{
			int mode;
			if (Pots.Armor) mode = 1;
			else mode = 0;
			var modes = new Dictionary<int, string>();
			if (GamePerson == 2)
			{
				modes[0] = "pots_2_1";
				modes[1] = "pots_2_1_protected";
				modes[2] = "pots_2_2";
				modes[3] = "pots_2_2_protected";
			}
			else
			{
					modes[0] = "pots_1_1";
					modes[1] = "pots_1_1_protected";
					modes[2] = "pots_1_2";
					modes[3] = "pots_1_2_protected";
				
			}

			switch (e.KeyCode)
			{
				case Keys.W:
					StepY = -Pots.SpeedY;
                 mainBird.Image = GetImage(modes[2 + mode]);
					break;
				case Keys.Escape:
					GamePause();
					break;
				case Keys.Space:
					GamePause();
					break;
				case Keys.S:
					StepY = Pots.SpeedY + Pots.Gravity;
					mainBird.Image = GetImage(modes[0 + mode]);
				  break;
				case Keys.D:
				  StepX = Pots.SpeedX;
				  mainBird.Image = GetImage(modes[0 + mode]);
				  break;
				case Keys.A:
					 StepX = -Pots.SpeedX - (Pots.SpeedX / 2);
					 mainBird.Image = GetImage(modes[2 + mode]);
				  break;

					
			}
			
		}

		private void Timer3Tick(object sender, EventArgs e) // ok
		{
			mainBird.Location = new Point(mainBird.Location.X + StepX, mainBird.Location.Y + StepY);

			if (mainBird.Location.Y < 0)
				mainBird.Location = new Point(mainBird.Location.X, 0);
			if (mainBird.Location.X < 0)
				mainBird.Location = new Point(0, mainBird.Location.Y);

			if (mainBird.Location.Y + mainBird.Height > this.ClientSize.Height)
				mainBird.Location = new Point(mainBird.Location.X, this.ClientSize.Height - mainBird.Height);

			CollisionsChecker();

			pointWindow.Text ="Points : "+ Convert.ToString(Pots.Points); // Points: 
			Coins.Text = "Coins : " + Convert.ToString(Pots.Coins) + " 💰";//Coins :  xxx  💰
			LivesPanel.Text = "Lives : " + Convert.ToString(Pots.Lives) + " ♥ " + Convert.ToString((Pots.Armor) ? 1 : 0) + " 👕 ";  // :  5 ♥  1 👕
														//Coins :  xxx  💰
		} 

		private void OnKeyUp(object sender, KeyEventArgs e) //ok
		{
			int mode;
			if (Pots.Armor) mode = 1;
			else mode = 0;
			var modes = new Dictionary<int, string>();
			if (GamePerson == 2)
			{
				modes[0] = "pots_2_1";
				modes[1] = "pots_2_1_protected";
				modes[2] = "pots_2_2";
				modes[3] = "pots_2_2_protected";
			}
			else
			{
				modes[0] = "pots_1_1";
				modes[1] = "pots_1_1_protected";
				modes[2] = "pots_1_2";
				modes[3] = "pots_1_2_protected";

			}
			switch (e.KeyCode)
			{
				case Keys.W:
					StepY = Pots.Gravity;
					mainBird.Image = GetImage(modes[0 + mode]);
					break;
				case Keys.S:
					StepY = Pots.Gravity;
					mainBird.Image = GetImage(modes[0 + mode]);
					break;
			}
		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		private void SelectFirstPots(object sender, EventArgs e)
		{
			center = new Point(Width / 2 - FirstPots.Width / 2, Height / 2 - FirstPots.Height / 2);
			if (!Pause)
			{
				if (!GamePersonSelected)
				{
					GamePersonSelected = true;
					GamePerson = 1;
					button2.Visible = true;
					button2.Enabled = true;
					SecondPots.Visible = false;
					SecondPots.Enabled = false;
					FirstSelection = FirstPots.Location;
					FirstPots.Location = center;
					FirstPots.Text = "Selected";
				}
				else
				{
					GamePersonSelected = false;
					button2.Visible = false;
					button2.Enabled = false;
					SecondPots.Visible = true;
					SecondPots.Enabled = true;
					FirstPots.Location = FirstSelection;
					FirstPots.Text = "Select";
				}
			}
			else
			{
				GamePerson = 1;
				SecondPots.Visible = false;
				SecondPots.Enabled = false;
				FirstPots.Visible = false;
				FirstPots.Enabled = false;
				mainBird.Visible = true;
				mainBird.Enabled = true;
				GamePersonSelected = false;
				GamePause();
			}
		}

		private void SelectSecondPots(object sender, EventArgs e)
		{
			center = new Point(Width / 2 - FirstPots.Width / 2, Height / 2 - FirstPots.Height / 2);
			if (!Pause)
			{
				if (!GamePersonSelected)
				{
					GamePersonSelected = true;
					GamePerson = 2;
					button2.Visible = true;
					button2.Enabled = true;
					FirstPots.Visible = false;
					FirstPots.Enabled = false;
					SecondSelection = SecondPots.Location;
					SecondPots.Location = center;
					SecondPots.Text = "Selected";
				}
				else
				{
					GamePersonSelected = false;
					button2.Visible = false;
					button2.Enabled = false;
					FirstPots.Visible = true;
					FirstPots.Enabled = true;
					SecondPots.Location = SecondSelection;
					SecondPots.Text = "Select";
				}
			}
			else
			{
				GamePerson = 2;
				SecondPots.Visible = false;
				SecondPots.Enabled = false;
				FirstPots.Visible = false;
				FirstPots.Enabled = false;
				mainBird.Visible = true;
				mainBird.Enabled = true;
				GamePersonSelected = false;
				GamePause();
			}
		}
	}
}