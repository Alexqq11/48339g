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

		ObstaclesMap Pipes;
		Player Pots;
		int StepY = 5;
		int StepX = 0;
		int PipeWidth = 75;
		int PipeDifferentY = 200; // refact this shit;
		int PipeDifferentX = 280;
		//int Pots.Points;
		bool Pause = false;

		private void Die()
		{
			Pots.Alive = false;
			timer2.Enabled = false;
			timer3.Enabled = false;
			button2.Visible = true;
			button2.Enabled = true;
			Pots.Points = 0;
			mainBird.Location = Pots.StartLocation;
		}
		//private Force 
		private void GamePause()
		{
			if (!Pause)
			{
				timer2.Enabled = false;
				timer3.Enabled = false;
				Pause = true;

			}
			else
			{
				timer2.Enabled = true;
				timer3.Enabled = true;
				Pause = false;
			}
		}

		private void StartGame()
		{
			Pots = new Player(5, 5, mainBird.Location);
			//SpeedY = Pots.SpeedY;
			timer1.Enabled = true;
			timer2.Enabled = true;
			timer3.Enabled = true;
			Pipes = new ObstaclesMap(Height, Width, PipeWidth, PipeDifferentX, PipeDifferentY);
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
					/*if (Pipes[i].TopX + PipeWidth <= 0)// attention 
					{

						Pipes[i] = new Obstacle(Height, Width, PipeWidth, PipeDifferentY);
					}
					else
					{*/
						Pipes[i].TopX = Pipes[i].TopX - Pots.GameSpeed; // in future make a nomal interface of class
						Pipes[i].BottomX = Pipes[i].BottomX - Pots.GameSpeed;
						Pipes.LevelBonus[i].X = Pipes.LevelBonus[i].X - Pots.GameSpeed; // write move method in the map objects
						//Pipes.LevelBoxes[i].X = Pipes.LevelBoxes[i].X - Pots.GameSpeed;
						Pipes.LevelBoxes[i].Update(Pots.GameSpeed);
						//SpeedY = Pots.SpeedY;

					//}
				}
				/*for (int i = 0; i < Pipes.Count; i++)
				{
					Pipes.LevelBonus[i].X = Pipes.LevelBonus[i].X - Pots.GameSpeed;
				}*/
			}
		}
		public void CollisionsChecker()
		{
			if (Pipes != null)
			{
				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pipes[i].TopX < 2 * PipeDifferentX) // attention
					{
						CheckForCollision(Pipes[i]);
					}
				}
				for (var i = 0; i < Pipes.Count; i++)
				{
					CheckForCollision(Pipes.LevelBonus[i]);
				}
				for (var i = 0; i < Pipes.Count; i++)
				{
					CheckForCollision(Pipes.LevelBoxes[i]);
				}
			}
		}

		public void DrawSearchzone(PaintEventArgs e)
		{
			if (Pipes != null)
			{
				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pipes[i].BottomX < 2 * PipeDifferentX) // attention
					{
						var obstacle = Pipes[i];
						Rectangle playerLocation = mainBird.Bounds;
						Rectangle topCollisionZone = new Rectangle(obstacle.TopX, 0, obstacle.ObstacleWidth, obstacle.TopY + 15); //watch pipe print
						Rectangle bottomCollisionZone = new Rectangle(obstacle.BottomX, obstacle.BottomLength, obstacle.ObstacleWidth, obstacle.WindowHeight - obstacle.BottomLength);
						Rectangle topZoneIntersection = Rectangle.Intersect(playerLocation, topCollisionZone);
						Rectangle bottomZoneIntersection = Rectangle.Intersect(playerLocation, bottomCollisionZone);
						Rectangle pointZone = new Rectangle(obstacle.BottomX + 20, obstacle.BottomLength - obstacle.ObstacleVerticalInterval, 15, obstacle.ObstacleVerticalInterval);
						Rectangle pointZoneIntersection = Rectangle.Intersect(playerLocation, pointZone);

						e.Graphics.DrawRectangle(new Pen(Brushes.OrangeRed, 6), playerLocation);
						e.Graphics.DrawRectangle(new Pen(Brushes.DarkTurquoise, 5), topCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.DarkTurquoise, 5), bottomCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Khaki, 5), topZoneIntersection);
						e.Graphics.DrawRectangle(new Pen(Brushes.LightYellow, 5), bottomZoneIntersection);
						e.Graphics.DrawRectangle(new Pen(Brushes.Red, 5), pointZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Green, 10), pointZoneIntersection);
						//
					}
				}
				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pipes.LevelBonus[i].X < 2 * PipeDifferentX) // attention
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

				}


			}
			//for (var i = 0; i < )

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
					//PlaySound("collision");
					if (!Pots.Alive)
						Die();
				}
			}

		}



		public void CheckForCollision(Obstacle obstacle)
		{
			Rectangle playerLocation = mainBird.Bounds;
			Rectangle topCollisionZone = new Rectangle(obstacle.TopX, 0, obstacle.ObstacleWidth, obstacle.TopY);
			Rectangle bottomCollisionZone = new Rectangle(obstacle.BottomX, obstacle.BottomLength, obstacle.ObstacleWidth, obstacle.WindowHeight - obstacle.BottomLength);
			Rectangle topZoneIntersection = Rectangle.Intersect(playerLocation, topCollisionZone);
			Rectangle bottomZoneIntersection = Rectangle.Intersect(playerLocation, bottomCollisionZone);

			if (bottomZoneIntersection != Rectangle.Empty | topZoneIntersection != Rectangle.Empty)
			{
				PlaySound("collision");
				Die();
			}

		}
		public void PointsChecker()
		{
			if (Pipes != null)
				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pipes[i].TopX < PipeWidth) // for one tube isPoint == true but for another is false
						CheckForPoint(Pipes[i]); // we need to check one tube in epsilon(player); 
				}

		}
		private void CheckForPoint(Obstacle obstacle)
		{
			Rectangle playerLocation = mainBird.Bounds;
			Rectangle pointZone = new Rectangle(obstacle.BottomX + 20, obstacle.BottomLength - obstacle.ObstacleVerticalInterval, 15, obstacle.ObstacleVerticalInterval);
			Rectangle pointIntersectionZone = Rectangle.Intersect(playerLocation, pointZone);
			if (pointIntersectionZone != Rectangle.Empty)
			{
				if (!Pots.inPointZone)
				{
					Pots.Points++;
					PlaySound("point");
					Pots.inPointZone = true;
				}
			}
			else Pots.inPointZone = false;

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
					if (Pots != null && Pots.Alive && Pipes[i] != null && Pipes[i].TopX < Width + 1)
					{
						Pipes[i].DrawPipe(e);
					}
				}
				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pots != null && Pots.Alive && !Pipes.LevelBonus[i].Taked && ObstacleInScreen(Pipes.LevelBonus[i]))
					{
						Pipes.LevelBonus[i].DrawBonus(e);
					}


				}

				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pots != null && Pots.Alive && ObstacleInScreen(Pipes.LevelBoxes[i]))
					{
						Pipes.LevelBoxes[i].DrawTexture(e);
					}

				}
			}

		}
	}
}

