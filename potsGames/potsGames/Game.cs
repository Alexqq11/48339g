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
		int Step = 5 ;
		int PipeWidth = 55;
		int PipeDifferentY = 140;
		int PipeDifferentX = 180;

		int Originalx, Originaly;
		int points;

		private void Die()
		{
			Pots.Alive = false;
			timer2.Enabled = false;
			timer3.Enabled = false;
			button2.Visible = true;
			button2.Enabled = true;
			points = 0;
			mainBird.Location = Pots.StartLocation;
		}
		//private Force 
		private void StartGame()
		{
			Pots = new Player(3, 5, mainBird.Location);

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
			if(Pipes != null) 
			for (int i = 0; i < Pipes.Count; i++){
				if (Pipes[i].TopX + PipeWidth <= 0)// attention 
				{

					Pipes[i] = new Obstacle(Height, Width, PipeWidth, PipeDifferentY);
				}
				else{

					Pipes[i].TopX = Pipes[i].TopX - Pots.SpeedX; // in future make a nomal interface of class
					Pipes[i].BottomX = Pipes[i].BottomX - Pots.SpeedX;
			}
			}
		}
		public void CollisionsChecker()
		{
			if (Pipes != null)
			for (var i = 0; i < Pipes.Count; i++)
			{
				if (Pipes[i].TopX < 2 * PipeDifferentX) // attention
				{
					CheckForCollision(Pipes[i]);
				}
			}
		}

		public void DrawSearchzone(PaintEventArgs e)
		{
			if (Pipes != null)
				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pipes[i].BottomX < 2 * PipeDifferentX) // attention
					{
						var obstacle  =  Pipes[i];
						Rectangle playerLocation = mainBird.Bounds;
						Rectangle topCollisionZone = new Rectangle(obstacle.TopX, 0, obstacle.ObstacleWidth, obstacle.TopY +15); //watch pipe print
						Rectangle bottomCollisionZone = new Rectangle(obstacle.BottomX, obstacle.BottomLength, obstacle.ObstacleWidth, obstacle.WindowHeight - obstacle.BottomLength);
						Rectangle topZoneIntersection = Rectangle.Intersect(playerLocation, topCollisionZone);
						Rectangle bottomZoneIntersection = Rectangle.Intersect(playerLocation, bottomCollisionZone);
						Rectangle pointZone = new Rectangle(obstacle.BottomX + 20, obstacle.BottomLength - obstacle.ObstacleVerticalInterval, 15, obstacle.ObstacleVerticalInterval);
				
						e.Graphics.DrawRectangle( new Pen(Brushes.DarkTurquoise, 5), topCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Gold, 5), bottomCollisionZone);
						e.Graphics.DrawRectangle(new Pen(Brushes.Khaki, 5), topZoneIntersection);
						e.Graphics.DrawRectangle(new Pen(Brushes.LightYellow, 5), bottomZoneIntersection);
						e.Graphics.DrawRectangle(new Pen(Brushes.Red, 5), pointZone);
					}
				}

		}
		public void CheckForCollision(Obstacle obstacle) { 
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
			if(Pipes != null ) 
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
					points++;
					PlaySound("point");
					Pots.inPointZone = true;
				}
			}
			else Pots.inPointZone = false;

		}

		public void DrawPipes(PaintEventArgs e)
		{	
			if (Pipes != null)
			for (var i = 0; i < Pipes.Count; i++)
			{
				if (Pots != null && Pots.Alive && Pipes[i] != null)
				{
					Pipes[i].DrawPipe(e);
				}
			}
		}
 
	}
}

