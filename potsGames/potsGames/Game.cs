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
		private bool exist = false;
		
		private int _TopX; // X COORDINATE OF PIPE TOP
		private int _TopLength;
		private int _BottomX; // X CORDINATE OF PIPE BOTTOM
		private int _BottomLength;
		public int ObstacleWidth { get; set;}
		public int ObstacleVerticalInterval { get; set; }
		public int WindowHeight { get; set; }

		public int TopX { get { return _TopX; } set { _TopX = value; PropitiesInit(); Propities[0] = value; } }
		public int TopLength { get { return _TopLength; } set { _TopLength = value; PropitiesInit(); Propities[1] = value; } }
		public int BottomX { get { return _BottomX; } set { _BottomX = value; PropitiesInit(); Propities[2] = value; } }
		public int BottomLength { get { return _BottomLength; } set { _BottomLength = value; PropitiesInit(); Propities[3] = value; } }
		private List<int> Propities;// /{ get; set;}
		// сильно зависим от параметров окна //
		private void PropitiesInit() {
			if (!exist)
				Propities = new List<int>();
			for (var i = 0; i < 4; i++)
				Propities.Add(0);
			exist = true;
		}

		public Obstacle (int windowHeight , int windowWidth, int pipeWidth, int distanceBetweenPipes) // To do make normal params to generate pipe 
		{
			PropitiesInit();
			Random random = new Random();
			_TopX = windowWidth;//sreen possition
			_TopLength = random.Next(40, (windowHeight - distanceBetweenPipes)); // refact 40 to pipe length min
			_BottomX = windowWidth;//screen posirion
			_BottomLength = _TopLength + distanceBetweenPipes;
			Propities[0] = _TopX;
			Propities[1] = _TopLength;
			Propities[2] = _BottomX;
			Propities[3] = _BottomLength;
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
		

		public bool Any()
		{
			return Propities.Any();
		}

		public int this[int i]
		{
			get{return Propities[i];}
			set
			{
				Propities[i] = value;
				switch (i)
				{
					case 0 :
						_TopX = value;
						break;
					case 1 :
						_TopLength = value;
						break;
					case 2 :
						_BottomX = value;
						break;
					case 3 :
						_BottomLength = value;
						break;
 
				}
			}
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


		/////////////////////////////////////////////////
		
		//bool Pots.inPointZone = false;

	public void oInitialPipes()
		{
			if(Pipes != null) 
			for (int i = 0; i < Pipes.Count; i++){
				if (Pipes[i][0] + PipeWidth <= 0)// attention 
				{

					Pipes[i] = new Obstacle(Height, Width, PipeWidth, PipeDifferentY);
				}
				else{

					Pipes[i][0] = Pipes[i][0] - Pots.SpeedX; // in future make a nomal interface of class
					Pipes[i][2] = Pipes[i][2] - Pots.SpeedX;
			}
			}
		}
		public void preCheck()
		{
			if (Pipes != null)
			for (var i = 0; i < Pipes.Count; i++)
			{
				if (Pipes[i][0] < 2 * PipeDifferentX) // attention
				{
					oCheckForCollision(Pipes[i]);
				}
			}
		}
		public void DrawSearchzone(PaintEventArgs e)
		{
			if (Pipes != null)
				for (var i = 0; i < Pipes.Count; i++)
				{
					if (Pipes[i][0] < 2 * PipeDifferentX) // attention
					{
						var obstacle  =  Pipes[i];
									Rectangle playerLocation = mainBird.Bounds;
									Rectangle topCollisionZone = new Rectangle(obstacle[0], 0, obstacle.ObstacleWidth, obstacle.TopLength +15); //watch pipe print
									Rectangle bottomCollisionZone = new Rectangle(obstacle[2], obstacle[3], obstacle.ObstacleWidth, obstacle.WindowHeight - obstacle[3]);
									Rectangle topZoneIntersection = Rectangle.Intersect(playerLocation, topCollisionZone);
									Rectangle bottomZoneIntersection = Rectangle.Intersect(playerLocation, bottomCollisionZone);
									Rectangle pointZone = new Rectangle(obstacle[2] + 20, obstacle[3] - obstacle.ObstacleVerticalInterval, 15, obstacle.ObstacleVerticalInterval);
					
									e.Graphics.DrawRectangle( new Pen(Brushes.DarkTurquoise, 5), topCollisionZone);
									e.Graphics.DrawRectangle(new Pen(Brushes.Gold, 5), bottomCollisionZone);
									e.Graphics.DrawRectangle(new Pen(Brushes.Khaki, 5), topZoneIntersection);
									e.Graphics.DrawRectangle(new Pen(Brushes.LightYellow, 5), bottomZoneIntersection);
									e.Graphics.DrawRectangle(new Pen(Brushes.Red, 5), pointZone);
			
			

					}
				}

		}
		public void oCheckForCollision(Obstacle obstacle) { 
			Rectangle playerLocation = mainBird.Bounds;
			Rectangle topCollisionZone = new Rectangle(obstacle[0], 0, obstacle.ObstacleWidth, obstacle.TopLength);
			Rectangle bottomCollisionZone = new Rectangle(obstacle[2], obstacle[3], obstacle.ObstacleWidth, obstacle.WindowHeight - obstacle[3]);
			Rectangle topZoneIntersection = Rectangle.Intersect(playerLocation, topCollisionZone);
			Rectangle bottomZoneIntersection = Rectangle.Intersect(playerLocation, bottomCollisionZone);
			if (bottomZoneIntersection != Rectangle.Empty | topZoneIntersection != Rectangle.Empty)
			{
					PlaySound("collision");
					Die();
			}

		}
		public void PrePoint()
		{
			if(Pipes != null) 
			for (var i = 0; i < Pipes.Count; i++)
			{
					oCheckForPoint(Pipes[i]);
				}
			
		}
		private void oCheckForPoint(Obstacle obstacle)
		{
			Rectangle playerLocation = mainBird.Bounds;
			Rectangle pointZone = new Rectangle(obstacle[2] + 20, obstacle[3] - obstacle.ObstacleVerticalInterval, 15, obstacle.ObstacleVerticalInterval);
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
				if (Pots != null && Pots.Alive && Pipes[i] != null && Pipes[i].Any())
				{
					Pipes[i].DrawPipe(e);
				}
			}
		}
 
		
		

		/// <summary>
		/// //////////////////////////////////////////////////////////////////////////
		/// </summary>

		/*public void InitialPipes()
		{
			if (FirstPipe[0] + PipeWidth <= 0 )
			{

				FirstPipe = new Obstacle(Height, Width, PipeWidth, PipeDifferentY);
			}
			else
			{
				FirstPipe[0] = FirstPipe[0] - 2;
				FirstPipe[2] = FirstPipe[2] - 2;
			}
			if (SecondPipe[0] + PipeWidth <= 0)
			{
				SecondPipe = new Obstacle(Height, Width, PipeWidth, PipeDifferentY);
			}
			else
			{
				SecondPipe[0] = SecondPipe[0] - 2;
				SecondPipe[2] = SecondPipe[2] - 2;
			}

			//if (Start)
				//Start = false;
		}
		



	


		private void CheckForPoint()
		{
			Rectangle rec = mainBird.Bounds;
			Rectangle rec1 = new Rectangle(FirstPipe[2] + 20, FirstPipe[3] - PipeDifferentY, 15, PipeDifferentY);
			Rectangle rec2 = new Rectangle(SecondPipe[2] + 20, SecondPipe[3] - PipeDifferentY, 15, PipeDifferentY);
			Rectangle intersect1 = Rectangle.Intersect(rec, rec1);
			Rectangle intersect2 = Rectangle.Intersect(rec, rec2);
			if (Pots.Alive | Start)
			{
				if (intersect1 != Rectangle.Empty | intersect2 != Rectangle.Empty)
				{
					if (!Pots.inPointZone)
					{
						points++;
						PlaySound("point");
						Pots.inPointZone = true;
					}
				}
				else
				{
					Pots.inPointZone = false;
				}
			}
		}

		private void CheckForCollision()
		{
			Rectangle rec = mainBird.Bounds;
			Rectangle rec1 = new Rectangle(FirstPipe[0], 0, PipeWidth, FirstPipe[1]);
			Rectangle rec2 = new Rectangle(FirstPipe[2], FirstPipe[3], PipeWidth, this.Height - FirstPipe[3]);
			Rectangle rec3 = new Rectangle(SecondPipe[0], 0, PipeWidth, SecondPipe[1]);
			Rectangle rec4 = new Rectangle(SecondPipe[2], SecondPipe[3], PipeWidth, this.Height - SecondPipe[3]);
			Rectangle intersect1 = Rectangle.Intersect(rec, rec1);
			Rectangle intersect2 = Rectangle.Intersect(rec, rec2);
			Rectangle intersect3 = Rectangle.Intersect(rec, rec3);
			Rectangle intersect4 = Rectangle.Intersect(rec, rec4);
			if (Pots.Alive | Start)
				if (intersect1 != Rectangle.Empty |
					intersect2 != Rectangle.Empty |
					intersect3 != Rectangle.Empty |
					intersect4 != Rectangle.Empty
					)
				{
					PlaySound("collision");
					Die();
				}
		}
		
		private void DrawPipes(PaintEventArgs e)
		{
			if (Pots != null && Pots.Alive && FirstPipe != null &&  SecondPipe != null && FirstPipe.Any() && SecondPipe.Any())
			{
				FirstPipe.DrawPipe(e);
				SecondPipe.DrawPipe(e);

			}

		}*/
	}
}

