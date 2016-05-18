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

namespace potsGames
{

	public partial class GameForm
	{


		//selection params
		int GamePerson = 1;
		bool GamePersonSelected = false;
		Point FirstSelection;
		Point SecondSelection;
		Point center;


		bool Pause = false;


		LevelMap Pipes;
		Player Pots;
		int StepY = 5;
		int StepX = 0;
		int PipeWidth = 75;
		int PipeDifferentY = 200; // refact this shit;
		int PipeDifferentX = 320;
		

		private void Die()
		{
			Pots.Alive = false;
			timer2.Enabled = false;
			timer3.Enabled = false;
			mainBird.Visible = false;
			mainBird.Enabled = false;
			button2.Visible = true;
			button2.Enabled = true;
			Pots.Points = 0;
			mainBird.Location = Pots.StartLocation;
		}

		private void GamePause()
		{
			if (!Pause)
			{
				timer2.Enabled = false;
				timer3.Enabled = false;
				mainBird.Visible = false;
				mainBird.Enabled = false;
				FirstPots.Enabled = true;
				FirstPots.Visible = true;
				SecondPots.Enabled = true;
				SecondPots.Visible = true;
				Pause = true;

			}
			else
			{
				if (GamePerson == 1)
					FirstPots.Location = FirstSelection;
				else SecondPots.Location = SecondSelection;

				timer2.Enabled = true;
				timer3.Enabled = true;
				Pause = false;
			}
		}

		private void StartGame()
		{
			Pots = new Player(5, 5, mainBird.Location);
			timer1.Enabled = true;
			timer2.Enabled = true;
			timer3.Enabled = true;
			Pipes = new LevelMap(Height, Width, PipeWidth, PipeDifferentX, PipeDifferentY);
			button2.Visible = false;
			button2.Enabled = false;
			Focus();
		}

		public void InitialPipes()
		{
			if (Pipes != null)
			{
				for (int i = 0; i < Pipes.Count; i++)
				{
					Pipes.LevelDoors[i].Top.Update(Pots.GameSpeed);
						Pipes.LevelDoors[i].Bottom.Update(Pots.GameSpeed);
						Pipes.LevelBonus[i].X = Pipes.LevelBonus[i].X - Pots.GameSpeed; 
						Pipes.LevelBoxes[i].Update(Pots.GameSpeed);

				}

			}
		}
		public void CollisionsChecker()
		{
			if (Pipes != null)
			{
				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pipes.LevelDoors[i].Top.X < Width) // attention making from person x y
					{ 
						CheckForCollision(Pipes.LevelDoors[i].Top);
						CheckForCollision(Pipes.LevelDoors[i].Bottom);
						CheckForCollision(Pipes.LevelBoxes[i]);
						CheckForCollision(Pipes.LevelBonus[i]);
					}
				}

			}
		}

		public void DrawSearchzone(PaintEventArgs e)
		{
			if (Pipes != null)
			{
				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pipes.LevelBonus[i].X < 2 * PipeDifferentX) 
					{
						var bonus = Pipes.LevelBonus[i];
						Rectangle playerLocation = mainBird.Bounds;
						Rectangle bonusCollisionZone = new Rectangle(bonus.X, bonus.Y, bonus.Width, bonus.Height);
						Rectangle intersection = Rectangle.Intersect(playerLocation, bonusCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Violet, 8), bonusCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Black, 8), intersection);
					}

				}
				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pipes.LevelBoxes[i].X < 2 * PipeDifferentX) // attention
					{
						var bonus = Pipes.LevelBoxes[i];
						Rectangle playerLocation = mainBird.Bounds;
						Rectangle bonusCollisionZone = new Rectangle(bonus.X, bonus.Y, bonus.Width, bonus.Height);
						Rectangle intersection = Rectangle.Intersect(playerLocation, bonusCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Yellow, 8), bonusCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Black, 8), intersection);
					}
					if (Pipes.LevelDoors[i].Top.X < 2 * PipeDifferentX) // attention
					{
						var bonus = Pipes.LevelDoors[i].Top;
						Rectangle playerLocation = mainBird.Bounds;
						Rectangle bonusCollisionZone = new Rectangle(bonus.X, bonus.Y, bonus.Width, bonus.Height);
						Rectangle intersection = Rectangle.Intersect(playerLocation, bonusCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Yellow, 8), bonusCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Black, 8), intersection);
					}


					if (Pipes.LevelDoors[i].Bottom.X < 2 * PipeDifferentX) // attention
					{
						var bonus = Pipes.LevelDoors[i].Bottom;
						Rectangle playerLocation = mainBird.Bounds;
						Rectangle bonusCollisionZone = new Rectangle(bonus.X, bonus.Y, bonus.Width, bonus.Height);
						Rectangle intersection = Rectangle.Intersect(playerLocation, bonusCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Yellow, 8), bonusCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Black, 8), intersection);
					}

				}


			}


		}
		public void CheckForCollision(Bonus bonus)
		{
			Rectangle playerLocation = mainBird.Bounds;
			Rectangle bonusCollisionZone = new Rectangle(bonus.X, bonus.Y, bonus.Width, bonus.Height);
			Rectangle intersection = Rectangle.Intersect(playerLocation, bonusCollisionZone);
			if (intersection != Rectangle.Empty)
			{
				if (!bonus.Taked)
				{
					Pots.Lives += bonus.Lives;
					Pots.Armor = (0 != bonus.Armor);
					Pots.Coins += bonus.Coins;
					Pots.SpeedY += bonus.SpeedBuf;
					bonus.Taked = true;
					bonus.Taken();
				}

			}

		}

		public void CheckForCollision(ObstacleV2 obstacle)
		{
			Rectangle playerLocation = mainBird.Bounds;
			Rectangle CollisionZone = new Rectangle(obstacle.X, obstacle.Y, obstacle.Width, obstacle.Height);
			Rectangle Intersection = Rectangle.Intersect(playerLocation, CollisionZone);

			if (Intersection != Rectangle.Empty)
			{
				if (obstacle.Exist)
				{
					Pots.GameSpeed = -Pots.GameSpeed - 20;
					InitialPipes();
					Pots.GameSpeed = -Pots.GameSpeed - 20;
					PlaySound("collision");
					Pots = obstacle.Collision(Pots);
					if (!Pots.Alive)
						Die();
				}
			}

		}


		public bool ObstacleInScreen(ObstacleV2 obstacle)
		{
			return (obstacle.X + obstacle.Width > -1) && (obstacle.X < Width + 1) && (obstacle.Y + obstacle.Height > -1) && (obstacle.Y < Height + 1); 

		}
		public bool ObstacleInScreen(Bonus obstacle)
		{
			return (obstacle.X + obstacle.Width > -1) && (obstacle.X < Width + 1) && (obstacle.Y + obstacle.Height > -1) && (obstacle.Y < Height + 1);

		}
		public void DrawPipes(PaintEventArgs e)
		{
			if (Pipes != null)
			{
				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pots != null && Pots.Alive && Pipes != null && ObstacleInScreen(Pipes.LevelDoors[i].Top))
					{
						Pipes.LevelDoors[i].Top.DrawTexture(e);
					}
					if (Pots != null && Pots.Alive && Pipes != null && ObstacleInScreen(Pipes.LevelDoors[i].Bottom))
					{
						Pipes.LevelDoors[i].Bottom.DrawTexture(e);
					}
					if (Pots != null && Pots.Alive && !Pipes.LevelBonus[i].Taked && ObstacleInScreen(Pipes.LevelBonus[i]))
					{
						Pipes.LevelBonus[i].DrawBonus(e);
					}
					if (Pots != null && Pots.Alive && ObstacleInScreen(Pipes.LevelBoxes[i]))
					{
						Pipes.LevelBoxes[i].DrawTexture(e);
					}
				}

				
			}

		}
	}
}

