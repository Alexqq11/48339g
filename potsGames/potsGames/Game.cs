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

	public class Obstacle {
		public int ObstacleWidth { get; set;}
		public int ObstacleVerticalInterval { get; set; }
		public int WindowHeight { get; set; }

		public int TopX { get; set; }
		public int TopLength { get; set; }
		public int BottomX { get; set; }
		public int BottomLength { get ; set; }
		// сильно зависим от параметров окна //

		public Obstacle (int windowHeight , int windowWidth, int pipeWidth, int distanceBetweenPipes) // To do make normal params to generate pipe 
		{
			Random random = new Random();
			TopX = windowWidth;//sreen possition
			TopLength = random.Next(40, (windowHeight - distanceBetweenPipes)); // refact 40 to pipe length min
			BottomX = windowWidth;//screen posirion
			BottomLength = TopLength + distanceBetweenPipes;
			ObstacleWidth = pipeWidth;
			ObstacleVerticalInterval = distanceBetweenPipes;
			WindowHeight = windowHeight;
		}

		public void DrawPipe(PaintEventArgs e)
{
				// Первый верхний
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(TopX, 0, ObstacleWidth, TopLength));
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(TopX - 10, BottomLength - ObstacleVerticalInterval, ObstacleWidth + 20, 15)); // потом надо реализовать масштабирование
				// первый нижний
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(BottomX, BottomLength, ObstacleWidth, WindowHeight - BottomLength));
				e.Graphics.FillRectangle(Brushes.DarkGreen, new Rectangle(BottomX - 10, BottomLength, ObstacleWidth + 20, 15));
		}		
		
	}
	
	public class ObstaclesMap {
		
		int ObstacleWidth {get; set; }         //= 55;
		int ObstacleVerticalInterval {get; set; }    //= 140;
		int ObstaclesHorizontInterval { get; set; }
		public int Count {get;  set;} //= 180;
		int WindowWidth {get; set;}
		int WindowHeight {get; set;}
		int Speed { get; set; }

		public List<Obstacle> LevelObstacles { get; set; }
		public ObstaclesMap(int windowHeight, int windowWidth , int obstracleWidth, int horizontInterval, int verticalInterval)
		{
			WindowHeight = windowHeight;
			WindowWidth  = windowWidth;
			Count = (windowWidth - obstracleWidth *( windowWidth / horizontInterval)) / horizontInterval + 1; // in the future write normal stabilization
			ObstacleWidth = obstracleWidth;
			ObstaclesHorizontInterval = horizontInterval;
			ObstacleVerticalInterval = verticalInterval;
			MakeObstacles();
		}
		private void MakeObstacles()//(int windowHeight, int windowWidth, int amount, int obstacleWidth, int horizontInterval, int distanseBetween)
		{
	
			LevelObstacles = new List<Obstacle>();

			for (var i = 0; i < Count; i++)
			{
				var pipe = new Obstacle(WindowHeight, WindowWidth + i * ObstaclesHorizontInterval, ObstacleWidth, ObstacleVerticalInterval);
				LevelObstacles.Add(pipe);
			} 
		}

		public Obstacle this[int i] // write exception
		{
			get{return LevelObstacles[i];}
			set{ LevelObstacles[i] = value ;}
		}
	}


	public class Player
	{
		public bool Alive {get; set;}
		public Point StartLocation {get; set;}
		public int SpeedY {get; set;}
		public int SpeedX { get; set;}
		public bool inPointZone {get; set;}
		public Player(int speedX, int speedY, Point startLocation)
		{
			Alive = true;
			SpeedX = speedX;
			SpeedY = speedY;
			inPointZone = false;
			StartLocation = startLocation;

		}
		
	}
	
	public partial class GameForm
	{

		Obstacle FirstPipe;
		Obstacle SecondPipe;
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

		private void StartGame()
		{
			Pots = new Player(3, 5, mainBird.Location);

			timer1.Enabled = true;
			timer2.Enabled = true;
			timer3.Enabled = true;
			Pipes = new ObstaclesMap(Height, Width, PipeWidth, PipeDifferentX, PipeDifferentY);
			FirstPipe = new Obstacle(Height, Width, PipeWidth, PipeDifferentY);
			SecondPipe = new Obstacle(Height, Width + PipeDifferentX, PipeWidth, PipeDifferentY);
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
						Rectangle topCollisionZone = new Rectangle(obstacle.TopX, 0, obstacle.ObstacleWidth, obstacle.TopLength +15); //watch pipe print
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
			Rectangle topCollisionZone = new Rectangle(obstacle.TopX, 0, obstacle.ObstacleWidth, obstacle.TopLength);
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
				if (Pipes[i].TopX < PipeWidth)
					CheckForPoint(Pipes[i]);
				}
			
		}
		private void CheckForPoint(Obstacle obstacle)
		{
			Rectangle playerLocation = mainBird.Bounds;
			Rectangle pointZone = new Rectangle(obstacle.BottomX + 20, obstacle.BottomLength - obstacle.ObstacleVerticalInterval, 15, obstacle.ObstacleVerticalInterval);
			Rectangle pointIntersectionZone = Rectangle.Intersect(playerLocation, pointZone);
			if (pointIntersectionZone != Rectangle.Empty)
			{//BUG WAS HERE WATCH A CHECKING ZONE AND AMOUNT OF CHECKING PIPES
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

