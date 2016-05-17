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
			Originalx = mainBird.Location.X;
			Originaly = mainBird.Location.Y;
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
				case Keys.Space:
					Step = -5;
                   mainBird.Image = GetImage("bird_straight");
					break;
			}
		}

		private void Timer3Tick(object sender, EventArgs e) // ok
		{
			mainBird.Location = new Point(mainBird.Location.X, mainBird.Location.Y + Step);

			if (mainBird.Location.Y < 0)
				mainBird.Location = new Point(mainBird.Location.X, 0);

			if (mainBird.Location.Y + mainBird.Height > this.ClientSize.Height)
				mainBird.Location = new Point(mainBird.Location.X, this.ClientSize.Height - mainBird.Height);

			CollisionsChecker();

			if (Pots.Alive)
				PointsChecker();



			pointWindow.Text = Convert.ToString(points);
		}

		private void OnKeyUp(object sender, KeyEventArgs e) //ok
		{
			switch (e.KeyCode)
			{
				case Keys.Space:
					Step = 5;
                   mainBird.Image = GetImage("bird_down");
					break;
			}
		}

		private void label2_Click(object sender, EventArgs e)
		{

		}

	}
}