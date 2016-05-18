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

		private void InitPosition(object sender, EventArgs e)
		
		{

		}

		private void GameStarter(object sender, EventArgs e)//ok
		{
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
			switch (e.KeyCode)
			{
				case Keys.W:
					StepY = -Pots.SpeedY;
                 mainBird.Image = GetImage("bird_straight");
					break;
				case Keys.Escape:
					GamePause();
					break;
				case Keys.Space:
					GamePause();
					break;
				case Keys.S:
					StepY = Pots.SpeedY + Pots.Gravity;
                  mainBird.Image = GetImage("bird_down");
				  break;
				case Keys.D:
				  StepX = Pots.SpeedX;
				  mainBird.Image = GetImage("bird_straight");
				  break;
				case Keys.A:
					 StepX = -Pots.SpeedX;
				  mainBird.Image = GetImage("bird_straight");
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

			if (Pots.Alive)
				PointsChecker();



			pointWindow.Text = Convert.ToString(Pots.Points); // Points: 
			Coins.Text = "Coins : " + Convert.ToString(Pots.Coins) + " 💰";//Coins :  xxx  💰
			LivesPanel.Text = "Lives : " + Convert.ToString(Pots.Lives) + " ♥ " + Convert.ToString((Pots.Armor) ? 1 : 0) + " 👕 ";  // :  5 ♥  1 👕
														//Coins :  xxx  💰
		} 

		private void OnKeyUp(object sender, KeyEventArgs e) //ok
		{
			switch (e.KeyCode)
			{
				case Keys.W:
					StepY = Pots.Gravity;
                  mainBird.Image = GetImage("bird_down");
					break;
				case Keys.S:
					StepY = Pots.Gravity;
                  mainBird.Image = GetImage("bird_down");
					break;
			}
		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

		//private void button1_KeyPress(object sender, KeyPressEventArgs e)
		//{
			//Console.WriteLine(e.KeyChar); 
		//}


	}
}